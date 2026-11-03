using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Exceptions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol;

/// <inheritdoc />
public partial class IppRequestMessageValidator : IppMessageValidatorBase, IIppRequestMessageValidator
{
    public static IppRequestMessageValidator ServerDefault => new()
    {
        ValidateCoreRules = true,
        ValidateOperationSpecificRules = true,
        ValidateOperationAttributesGroup = true,
        ValidateJobAttributesGroup = true,
        ValidatePrinterAttributesGroup = true,
        ValidateUnsupportedAttributesGroup = true,
        ValidateSubscriptionAttributesGroup = true,
        ValidateEventNotificationAttributesGroup = true,
        ValidateResourceAttributesGroup = true,
        ValidateDocumentAttributesGroup = true,
        ValidateSystemAttributesGroup = true,
        UseIppAttributeFidelityForCapabilityValidation = false,
        ValidateStringLengthLimits = true,
        EnforceMediaMutualExclusivity = false,
        ValidateTargetAttributeOrder = false,
    };

    public static IppRequestMessageValidator ClientDefault => new()
    {
        ValidateCoreRules = true,
        ValidateOperationSpecificRules = true,
        ValidateOperationAttributesGroup = true,
        ValidateJobAttributesGroup = true,
        ValidatePrinterAttributesGroup = true,
        ValidateUnsupportedAttributesGroup = true,
        ValidateSubscriptionAttributesGroup = true,
        ValidateEventNotificationAttributesGroup = true,
        ValidateResourceAttributesGroup = true,
        ValidateDocumentAttributesGroup = true,
        ValidateSystemAttributesGroup = true,
        UseIppAttributeFidelityForCapabilityValidation = false,
        ValidateStringLengthLimits = true,
        EnforceMediaMutualExclusivity = true,
        ValidateTargetAttributeOrder = false,
    };

    /// <inheritdoc />
    public IppRequestValidationContext Context { get; } = new();

    /// <inheritdoc />
    public bool UseIppAttributeFidelityForCapabilityValidation { get; set; } = false;

    /// <inheritdoc />
    public bool EnforceMediaMutualExclusivity { get; set; } = false;

    /// <inheritdoc />
    public bool ValidateTargetAttributeOrder { get; set; } = false;

    /// <inheritdoc />
    public virtual void Validate(IIppRequestMessage? request, IppRequestValidationContext? context = null)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var ctx = context ?? Context;

        if (ValidateCoreRules)
            ValidateCore(request);

        if (ValidateJobAttributesGroup)
            ValidateJobAttributes(request, ctx);

        if (ValidateDocumentAttributesGroup)
            ValidateDocumentAttributes(request, ctx);

        if (ValidatePrinterAttributesGroup)
            ValidatePrinterAttributes(request, ctx);

        if (ValidateOperationSpecificRules)
            ValidateOperationRules(request, ctx);

        if (ValidateStringLengthLimits)
            ValidateStringLengthLimitsRule(request);
    }

    /// <summary>
    /// Validates that the request conforms to basic IPP constraints (valid version, request-id, unique attributes within groups, first/second operation attributes).
    /// Spec: RFC 8011 Section 4.1 (IPP Message Structure & Encoding), PWG 5100.22.
    /// </summary>
    protected virtual void ValidateCore(IIppRequestMessage request)
    {
        if (!TryValidateCoreHeader(request.RequestId, request.Version, out var error, out var statusCode))
            throw new IppRequestException(error!, request, statusCode);

        if (ValidateOperationAttributesGroup)
            ValidateUniqueAttributes(request.OperationAttributes, "operation-attributes", request);

        if (ValidateJobAttributesGroup)
            ValidateUniqueAttributes(request.JobAttributes, "job-attributes", request);

        if (ValidatePrinterAttributesGroup)
            ValidateUniqueAttributes(request.PrinterAttributes, "printer-attributes", request);

        if (ValidateUnsupportedAttributesGroup)
            ValidateUniqueAttributes(request.UnsupportedAttributes, "unsupported-attributes", request);

        if (ValidateSubscriptionAttributesGroup)
            ValidateUniqueAttributes(request.SubscriptionAttributes, "subscription-attributes", request);

        if (ValidateEventNotificationAttributesGroup)
            ValidateUniqueAttributes(request.EventNotificationAttributes, "event-notification-attributes", request);

        if (ValidateResourceAttributesGroup)
            ValidateUniqueAttributes(request.ResourceAttributes, "resource-attributes", request);

        if (ValidateDocumentAttributesGroup)
            ValidateUniqueAttributes(request.DocumentAttributes, "document-attributes", request);

        if (ValidateSystemAttributesGroup)
            ValidateUniqueAttributes(request.SystemAttributes, "system-attributes", request);

        ValidateDocumentMetadata(request);

        if (!ValidateOperationAttributesGroup)
            return;

        var operationAttributes = request.OperationAttributes;
        if (!TryValidateOperationAttributesPrefix(operationAttributes, out error, out statusCode))
            throw new IppRequestException(error!, request, statusCode);

        if (ValidateTargetAttributeOrder)
            ValidateTargetAttributeOrderRule(request, operationAttributes);

        var hasPrinterUri = false;
        var hasSystemUri = false;
        var hasJobUri = false;
        foreach (var attr in operationAttributes)
        {
            if (attr.Name == IppAttributeNames.PrinterUri) hasPrinterUri = true;
            else if (attr.Name == IppAttributeNames.SystemUri) hasSystemUri = true;
            else if (attr.Name == IppAttributeNames.JobUri) hasJobUri = true;
        }

        if (!hasPrinterUri && !hasSystemUri && !(hasJobUri && request.IppOperation.SupportsJobUriTarget()))
            throw new IppRequestException("No printer-uri or system-uri operation attribute", request, IppStatusCode.ClientErrorBadRequest);

        if (request.IppOperation.IsStrictSystemServiceOperation() && !hasSystemUri)
            throw new IppRequestException("No system-uri operation attribute", request, IppStatusCode.ClientErrorBadRequest);
    }

    /// <summary>
    /// Validates that string-based attributes in the request do not exceed the octet (byte) length limits
    /// specified in RFC 8011.
    /// Characters are converted to octets (bytes) using UTF-8 encoding. Too-long strings are not
    /// automatically truncated, but instead reject the request with a ClientErrorBadRequest status.
    /// </summary>
    protected virtual void ValidateStringLengthLimitsRule(IIppRequestMessage request)
    {
        IppAttribute? charsetAttr = null;
        if (request.OperationAttributes != null)
        {
            foreach (var attr in request.OperationAttributes)
            {
                if (attr.Name == IppAttributeNames.AttributesCharset)
                {
                    charsetAttr = attr;
                    break;
                }
            }
        }
        var allAttributes = request.GetAllAttributes();

        if (!TryValidateStringLengthLimitsCore(allAttributes, charsetAttr, out var error))
        {
            throw new IppRequestException(error!, request, IppStatusCode.ClientErrorBadRequest);
        }
    }

    /// <summary>
    /// Checks whether the ipp-attribute-fidelity attribute is set to true in the request.
    /// </summary>
    internal static bool IsIppAttributeFidelityTrue(IIppRequestMessage request)
    {
        if (request.OperationAttributes != null)
        {
            foreach (var attr in request.OperationAttributes)
            {
                if (attr.Name == IppAttributeNames.IppAttributeFidelity)
                    return attr.Value is bool val && val;
            }
        }
        return false;
    }

    /// <summary>
    /// Helper method to enumerate collections of attributes matching a specific attribute name.
    /// Preserved directly on IppRequestMessageValidator for reflection compatibility.
    /// </summary>
    private static new IEnumerable<IReadOnlyList<IppAttribute>> EnumerateNamedCollections(
        IEnumerable<IppAttribute> attributes,
        string collectionName)
    {
        return IppMessageValidatorBase.EnumerateNamedCollections(attributes, collectionName);
    }

    /// <summary>
    /// Validates that attributes are not duplicated (non-consecutive instances of the same name) within an attribute group.
    /// Spec: RFC 8011 Section 4.1.3 (attribute group uniqueness).
    /// </summary>
    protected virtual void ValidateUniqueAttributes(List<IppAttribute>? attributes, string groupName, IIppRequestMessage request)
    {
        if (!TryValidateUniqueAttributesCore(attributes, groupName, out var error, out var status))
            throw new IppRequestException(error!, request, status);
    }

    /// <summary>
    /// Validates that an attribute is present in the specified collection, throwing an IppRequestException if absent.
    /// Avoids closure lambda allocations for .NET Native / nutc_driver compiler.
    /// </summary>
    protected static void RequireAttribute(
        IEnumerable<IppAttribute> attributes,
        string name,
        IIppRequestMessage request,
        string? customMessage = null,
        IppStatusCode statusCode = IppStatusCode.ClientErrorBadRequest)
    {
        if (!HasNamedAttribute(attributes, name))
        {
            throw new IppRequestException(customMessage ?? $"missing {name}", request, statusCode);
        }
    }

    /// <summary>
    /// Validates that an integer attribute, if present, falls within the inclusive range [min, max].
    /// Avoids closure lambda allocations for .NET Native / nutc_driver compiler.
    /// </summary>
    protected static void ValidateIntegerRange(
        IReadOnlyCollection<IppAttribute> attributes,
        string name,
        int min,
        int max,
        IIppRequestMessage request,
        string? customMessage = null,
        IppStatusCode statusCode = IppStatusCode.ClientErrorBadRequest)
    {
        if (!attributes.TryGetIppValue<int>(name, out var value))
        {
            return;
        }

        if (value < min || value > max)
        {
            var msg = customMessage ?? (max == int.MaxValue
                ? $"'{name}' must be >= {min}"
                : $"'{name}' must be in the range {min}-{max}");
            throw new IppRequestException(msg, request, statusCode);
        }
    }

    /// <summary>
    /// Validates that two attributes are mutually exclusive (cannot both be present).
    /// Avoids closure lambda allocations for .NET Native / nutc_driver compiler.
    /// </summary>
    protected static void ValidateMutuallyExclusive(
        IReadOnlyCollection<IppAttribute> attributes,
        string name1,
        string name2,
        IIppRequestMessage request,
        string? customMessage = null,
        IppStatusCode statusCode = IppStatusCode.ClientErrorConflictingAttributes)
    {
        if (HasNamedAttribute(attributes, name1) && HasNamedAttribute(attributes, name2))
        {
            throw new IppRequestException(customMessage ?? $"'{name1}' and '{name2}' are mutually exclusive", request, statusCode);
        }
    }

    /// <summary>
    /// Validates that two attributes are co-dependent (must be either both present or both absent).
    /// Avoids closure lambda allocations for .NET Native / nutc_driver compiler.
    /// </summary>
    protected static void ValidateCoDependent(
        IReadOnlyCollection<IppAttribute> attributes,
        string name1,
        string name2,
        IIppRequestMessage request,
        string? customMessage = null,
        IppStatusCode statusCode = IppStatusCode.ClientErrorBadRequest)
    {
        if (HasNamedAttribute(attributes, name1) != HasNamedAttribute(attributes, name2))
        {
            throw new IppRequestException(customMessage ?? $"'{name1}' and '{name2}' must be either both present or both absent", request, statusCode);
        }
    }
}
