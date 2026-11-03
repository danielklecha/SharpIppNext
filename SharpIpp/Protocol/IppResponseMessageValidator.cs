using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Exceptions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol;

/// <inheritdoc cref="IIppResponseMessageValidator" />
public class IppResponseMessageValidator : IppMessageValidatorBase, IIppResponseMessageValidator
{
    /// <summary>
    /// Gets the default instance of the response message validator.
    /// </summary>
    public static IppResponseMessageValidator Default => new()
    {
        ValidateCoreRules = true,
        ValidateOperationAttributesGroup = true,
        ValidateJobAttributesGroup = true,
        ValidatePrinterAttributesGroup = true,
        ValidateUnsupportedAttributesGroup = true,
        ValidateSubscriptionAttributesGroup = true,
        ValidateEventNotificationAttributesGroup = true,
        ValidateResourceAttributesGroup = true,
        ValidateDocumentAttributesGroup = true,
        ValidateSystemAttributesGroup = true,
        ValidateStringLengthLimits = true,
    };

    /// <summary>
    /// Validates the response message by executing enabled core, group, and attribute uniqueness rules,
    /// optionally against operation-specific expectations.
    /// </summary>
    /// <param name="response">The response message to validate.</param>
    /// <param name="operation">The IPP operation that triggered this response, or <c>null</c>.</param>
    public virtual void Validate(IIppResponseMessage? response, IppOperation? operation = null)
    {
        if (response is null)
            throw new ArgumentNullException(nameof(response));

        if (ValidateCoreRules)
            ValidateCore(response);

        if (ValidateOperationSpecificRules)
            ValidateOperationRules(response, operation);

        if (ValidateStringLengthLimits)
            ValidateStringLengthLimitsRule(response);
    }

    /// <summary>
    /// Executes response-status validation rules.
    /// Can be overridden in derived validators to enforce operation/response attributes.
    /// </summary>
    /// <param name="response">The response message to validate.</param>
    protected virtual void ValidateOperationRules(IIppResponseMessage response)
    {
        if (ValidateUnsupportedAttributesGroup)
        {
            if (response.StatusCode == IppStatusCode.ClientErrorAttributesOrValuesNotSupported ||
                response.StatusCode == IppStatusCode.SuccessfulOkIgnoredOrSubstitutedAttributes ||
                response.StatusCode == IppStatusCode.SuccessfulOkConflictingAttributes)
            {
                if (!AnyGroupHasAttributes(response.UnsupportedAttributes))
                    throw new IppResponseException($"Status code '{response.StatusCode}' requires 'unsupported-attributes' group to be present and non-empty", response);
            }
        }
    }

    /// <summary>
    /// Executes operation-specific or response-status validation rules.
    /// Spec: RFC 8011 Section 3, Section 4.
    /// </summary>
    /// <param name="response">The response message to validate.</param>
    /// <param name="operation">The optional IPP operation that triggered this response.</param>
    protected virtual void ValidateOperationRules(IIppResponseMessage response, IppOperation? operation)
    {
        ValidateOperationRules(response);

        if (!operation.HasValue || response.StatusCode != IppStatusCode.SuccessfulOk)
            return;

        switch (operation.Value)
        {
            case IppOperation.PrintJob:
            case IppOperation.CreateJob:
            case IppOperation.SendDocument:
            case IppOperation.SendUri:
                if (ValidateJobAttributesGroup)
                {
                    var hasJobId = AnyGroupHasNamedAttribute(response.JobAttributes, IppAttributeNames.JobId);
                    var hasJobUri = AnyGroupHasNamedAttribute(response.JobAttributes, IppAttributeNames.JobUri);
                    if (!hasJobId && !hasJobUri)
                        throw new IppResponseException("Successful print/create job response must contain 'job-id' or 'job-uri' in job-attributes", response);

                    var hasJobState = AnyGroupHasNamedAttribute(response.JobAttributes, IppAttributeNames.JobState);
                    if (!hasJobState)
                        throw new IppResponseException("Successful print/create job response must contain 'job-state' in job-attributes", response);
                }
                break;

            case IppOperation.GetPrinterAttributes:
                if (ValidatePrinterAttributesGroup)
                {
                    if (!AnyGroupHasAttributes(response.PrinterAttributes))
                        throw new IppResponseException("Successful Get-Printer-Attributes response must contain 'printer-attributes' group", response);
                }
                break;

            case IppOperation.GetJobAttributes:
                if (ValidateJobAttributesGroup)
                {
                    if (!AnyGroupHasAttributes(response.JobAttributes))
                        throw new IppResponseException("Successful Get-Job-Attributes response must contain 'job-attributes' group", response);
                }
                break;

            case IppOperation.GetSubscriptionAttributes:
                if (ValidateSubscriptionAttributesGroup)
                {
                    if (!AnyGroupHasAttributes(response.SubscriptionAttributes))
                        throw new IppResponseException("Successful Get-Subscription-Attributes response must contain 'subscription-attributes' group", response);
                }
                break;
        }
    }

    /// <summary>
    /// Validates that the response conforms to basic IPP constraints (valid version, request-id, unique attributes within groups, first/second operation attributes).
    /// </summary>
    protected virtual void ValidateCore(IIppResponseMessage response)
    {
        if (!TryValidateCoreHeader(response.RequestId, response.Version, out var error, out _))
            throw new IppResponseException(error!, response);

        if (!Enum.IsDefined(typeof(IppStatusCode), response.StatusCode))
            throw new IppResponseException($"Invalid or undefined IPP status code: {(short)response.StatusCode}", response);

        if (ValidateOperationAttributesGroup)
            ValidateUniqueAttributes(response.OperationAttributes, "operation-attributes", response);

        if (ValidateJobAttributesGroup)
            ValidateUniqueAttributes(response.JobAttributes, "job-attributes", response);

        if (ValidatePrinterAttributesGroup)
            ValidateUniqueAttributes(response.PrinterAttributes, "printer-attributes", response);

        if (ValidateUnsupportedAttributesGroup)
            ValidateUniqueAttributes(response.UnsupportedAttributes, "unsupported-attributes", response);

        if (ValidateSubscriptionAttributesGroup)
            ValidateUniqueAttributes(response.SubscriptionAttributes, "subscription-attributes", response);

        if (ValidateEventNotificationAttributesGroup)
            ValidateUniqueAttributes(response.EventNotificationAttributes, "event-notification-attributes", response);

        if (ValidateResourceAttributesGroup)
            ValidateUniqueAttributes(response.ResourceAttributes, "resource-attributes", response);

        if (ValidateDocumentAttributesGroup)
            ValidateUniqueAttributes(response.DocumentAttributes, "document-attributes", response);

        if (ValidateSystemAttributesGroup)
            ValidateUniqueAttributes(response.SystemAttributes, "system-attributes", response);

        if (!ValidateOperationAttributesGroup)
            return;

        var operationAttributes = response.OperationAttributes?.FirstOrDefault();
        if (!TryValidateOperationAttributesPrefix(operationAttributes, out error, out _))
            throw new IppResponseException(error!, response);
    }

    /// <summary>
    /// Validates that attributes are not duplicated (non-consecutive instances of the same name) within an attribute group list.
    /// </summary>
    protected virtual void ValidateUniqueAttributes(List<List<IppAttribute>>? attributeGroups, string groupName, IIppResponseMessage response)
    {
        if (attributeGroups is null)
            return;

        foreach (var group in attributeGroups)
        {
            if (!TryValidateUniqueAttributesCore(group, groupName, out var error, out _))
                throw new IppResponseException(error!, response);
        }
    }

    /// <summary>
    /// Validates that string-based attributes in the response do not exceed the octet (byte) length limits
    /// specified in RFC 8011.
    /// </summary>
    protected virtual void ValidateStringLengthLimitsRule(IIppResponseMessage response)
    {
        var operationAttributes = response.OperationAttributes?.FirstOrDefault();
        IppAttribute? charsetAttr = null;
        if (operationAttributes != null)
        {
            foreach (var attr in operationAttributes)
            {
                if (attr.Name == IppAttributeNames.AttributesCharset)
                {
                    charsetAttr = attr;
                    break;
                }
            }
        }
        var allAttributes = response.GetAllAttributes();

        if (!TryValidateStringLengthLimitsCore(allAttributes, charsetAttr, out var error))
        {
            throw new IppResponseException(error!, response);
        }
    }

    private static bool AnyGroupHasAttributes(List<List<IppAttribute>>? groups)
    {
        if (groups == null)
            return false;

        foreach (var group in groups)
        {
            if (group.Count > 0)
                return true;
        }

        return false;
    }

    private static bool AnyGroupHasNamedAttribute(List<List<IppAttribute>>? groups, string name)
    {
        if (groups == null)
            return false;

        foreach (var group in groups)
        {
            if (HasNamedAttribute(group, name))
                return true;
        }

        return false;
    }
}
