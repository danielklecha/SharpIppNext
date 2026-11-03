using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol;

/// <summary>
/// Abstract base class providing common configuration, core message constraint validation,
/// attribute uniqueness checking, and helper methods for IPP message validators.
/// </summary>
public abstract class IppMessageValidatorBase : IIppMessageValidator
{
    /// <inheritdoc />
    public virtual bool ValidateCoreRules { get; set; } = true;

    /// <inheritdoc />
    public virtual bool ValidateOperationSpecificRules { get; set; } = true;

    /// <inheritdoc />
    public virtual bool ValidateOperationAttributesGroup { get; set; } = true;

    /// <inheritdoc />
    public virtual bool ValidateJobAttributesGroup { get; set; } = true;

    /// <inheritdoc />
    public virtual bool ValidatePrinterAttributesGroup { get; set; } = true;

    /// <inheritdoc />
    public virtual bool ValidateUnsupportedAttributesGroup { get; set; } = true;

    /// <inheritdoc />
    public virtual bool ValidateSubscriptionAttributesGroup { get; set; } = true;

    /// <inheritdoc />
    public virtual bool ValidateEventNotificationAttributesGroup { get; set; } = true;

    /// <inheritdoc />
    public virtual bool ValidateResourceAttributesGroup { get; set; } = true;

    /// <inheritdoc />
    public virtual bool ValidateDocumentAttributesGroup { get; set; } = true;

    /// <inheritdoc />
    public virtual bool ValidateSystemAttributesGroup { get; set; } = true;

    /// <inheritdoc />
    public virtual bool ValidateStringLengthLimits { get; set; } = true;

    /// <summary>
    /// Validates core message header fields: RequestId &gt; 0 and Version &gt;= 1.0.
    /// Spec: RFC 8011 Section 4.1.
    /// </summary>
    protected static bool TryValidateCoreHeader(
        int requestId,
        IppVersion version,
        out string? error,
        out IppStatusCode statusCode)
    {
        if (requestId <= 0)
        {
            error = "Bad request-id value";
            statusCode = IppStatusCode.ClientErrorBadRequest;
            return false;
        }

        if (version < new IppVersion(1, 0))
        {
            error = "Unsupported IPP version";
            statusCode = IppStatusCode.ServerErrorVersionNotSupported;
            return false;
        }

        error = null;
        statusCode = default;
        return true;
    }

    /// <summary>
    /// Validates core message header fields: RequestId &gt; 0 and Version &gt;= 1.0.
    /// Spec: RFC 8011 Section 4.1.
    /// </summary>
    protected static void ValidateCoreHeader(
        int requestId,
        IppVersion version,
        Action<string, IppStatusCode> onError)
    {
        if (!TryValidateCoreHeader(requestId, version, out var error, out var statusCode))
            onError(error!, statusCode);
    }

    /// <summary>
    /// Validates the Operation Attributes prefix constraints:
    /// non-empty, 1st attribute is attributes-charset (utf-8), and 2nd attribute is attributes-natural-language.
    /// Spec: RFC 8011 Section 4.1.4.
    /// </summary>
    protected static bool TryValidateOperationAttributesPrefix(
        IReadOnlyList<IppAttribute>? operationAttributes,
        out string? error,
        out IppStatusCode statusCode)
    {
        if (operationAttributes == null || operationAttributes.Count == 0)
        {
            error = "No Operation Attributes";
            statusCode = IppStatusCode.ClientErrorBadRequest;
            return false;
        }

        if (operationAttributes[0].Name != IppAttributeNames.AttributesCharset)
        {
            error = "attributes-charset MUST be the first attribute";
            statusCode = IppStatusCode.ClientErrorBadRequest;
            return false;
        }

        var charsetValue = operationAttributes[0].Value?.ToString();
        if (!string.Equals(charsetValue, "utf-8", StringComparison.OrdinalIgnoreCase))
        {
            error = "attributes-charset MUST be 'utf-8'";
            statusCode = IppStatusCode.ClientErrorBadRequest;
            return false;
        }

        if (operationAttributes.Count < 2 || operationAttributes[1].Name != IppAttributeNames.AttributesNaturalLanguage)
        {
            error = "attributes-natural-language MUST be the second attribute";
            statusCode = IppStatusCode.ClientErrorBadRequest;
            return false;
        }

        error = null;
        statusCode = default;
        return true;
    }

    /// <summary>
    /// Validates the Operation Attributes prefix constraints:
    /// non-empty, 1st attribute is attributes-charset (utf-8), and 2nd attribute is attributes-natural-language.
    /// Spec: RFC 8011 Section 4.1.4.
    /// </summary>
    protected static void ValidateOperationAttributesPrefix(
        IReadOnlyList<IppAttribute>? operationAttributes,
        Action<string, IppStatusCode> onError)
    {
        if (!TryValidateOperationAttributesPrefix(operationAttributes, out var error, out var statusCode))
            onError(error!, statusCode);
    }

    /// <summary>
    /// Validates that attributes are not duplicated (non-consecutive instances of the same name) within an attribute group.
    /// Spec: RFC 8011 Section 4.1.3 (attribute group uniqueness).
    /// </summary>
    protected static bool TryValidateUniqueAttributesCore(
        IEnumerable<IppAttribute>? attributes,
        string groupName,
        out string? error,
        out IppStatusCode statusCode)
    {
        if (attributes == null)
        {
            error = null;
            statusCode = default;
            return true;
        }

        var seenNames = new HashSet<string>(StringComparer.Ordinal);
        string? currentName = null;
        var level = 0;

        foreach (var attr in attributes)
        {
            if (attr.Tag == Tag.BegCollection)
            {
                level++;
                continue;
            }

            if (attr.Tag == Tag.EndCollection)
            {
                level--;
                continue;
            }

            if (level > 0)
                continue;

            if (string.IsNullOrEmpty(attr.Name))
                continue;

            if (attr.Name != currentName)
            {
                if (seenNames.Contains(attr.Name))
                {
                    error = $"Duplicate attribute '{attr.Name}' in group '{groupName}'";
                    statusCode = IppStatusCode.ClientErrorBadRequest;
                    return false;
                }

                seenNames.Add(attr.Name);
                currentName = attr.Name;
            }
        }

        error = null;
        statusCode = default;
        return true;
    }

    /// <summary>
    /// Validates that attributes are not duplicated (non-consecutive instances of the same name) within an attribute group.
    /// Spec: RFC 8011 Section 4.1.3 (attribute group uniqueness).
    /// </summary>
    protected static void ValidateUniqueAttributesCore(
        IEnumerable<IppAttribute>? attributes,
        string groupName,
        Action<string, IppStatusCode> onError)
    {
        if (!TryValidateUniqueAttributesCore(attributes, groupName, out var error, out var statusCode))
            onError(error!, statusCode);
    }

    /// <summary>
    /// Validates string and octet attribute length limits as defined by RFC 8011.
    /// </summary>
    protected static bool TryValidateStringLengthLimitsCore(
        IEnumerable<IppAttribute>? allAttributes,
        IppAttribute? charsetAttr,
        out string? error)
    {
        if (allAttributes == null)
        {
            error = null;
            return true;
        }

        var encoding = IppEncodingHelper.GetEncoding(charsetAttr);
        return IppStringLengthValidator.TryValidate(allAttributes, encoding, out error);
    }

    /// <summary>
    /// Validates string and octet attribute length limits as defined by RFC 8011.
    /// </summary>
    protected static void ValidateStringLengthLimitsCore(
        IEnumerable<IppAttribute>? allAttributes,
        IppAttribute? charsetAttr,
        Action<string> onError)
    {
        if (!TryValidateStringLengthLimitsCore(allAttributes, charsetAttr, out var error))
        {
            onError(error!);
        }
    }

    /// <summary>
    /// Validates string and octet attribute length limits as defined by RFC 8011.
    /// </summary>
    protected static void ValidateStringLengthLimitsCore(
        IEnumerable<IppAttribute>? allAttributes,
        IppAttribute? charsetAttr,
        Action<string, IppStatusCode> onError,
        IppStatusCode statusCode = IppStatusCode.ClientErrorBadRequest)
    {
        if (!TryValidateStringLengthLimitsCore(allAttributes, charsetAttr, out var error))
        {
            onError(error!, statusCode);
        }
    }

    /// <summary>
    /// Checks whether an attribute with the specified name exists in the collection.
    /// </summary>
    protected static bool HasNamedAttribute(IEnumerable<IppAttribute>? attributes, string name)
    {
        if (attributes == null)
            return false;

        foreach (var attr in attributes)
        {
            if (attr.Name == name)
                return true;
        }

        return false;
    }

    /// <summary>
    /// Gets the first attribute matching the specified name, or null if not found.
    /// </summary>
    protected static IppAttribute? GetNamedAttribute(IEnumerable<IppAttribute>? attributes, string name)
    {
        if (attributes == null)
            return null;

        foreach (var attr in attributes)
        {
            if (attr.Name == name)
                return attr;
        }

        return null;
    }

    /// <summary>
    /// Counts the attributes matching the specified name in the collection.
    /// </summary>
    protected static int CountNamedAttributes(IEnumerable<IppAttribute>? attributes, string name)
    {
        if (attributes == null)
            return 0;

        var count = 0;
        foreach (var attr in attributes)
        {
            if (attr.Name == name)
                count++;
        }

        return count;
    }

    /// <summary>
    /// Helper method to enumerate collections of attributes matching a specific attribute name.
    /// </summary>
    protected static IEnumerable<IReadOnlyList<IppAttribute>> EnumerateNamedCollections(
        IEnumerable<IppAttribute> attributes,
        string collectionName)
    {
        return attributes.EnumerateNamedCollections(collectionName);
    }

    /// <summary>
    /// Validates that an attribute is present in the specified collection.
    /// </summary>
    protected static void RequireAttribute(
        IEnumerable<IppAttribute> attributes,
        string name,
        Action<string, IppStatusCode> onError,
        string? customMessage = null,
        IppStatusCode statusCode = IppStatusCode.ClientErrorBadRequest)
    {
        if (!HasNamedAttribute(attributes, name))
        {
            onError(customMessage ?? $"missing {name}", statusCode);
        }
    }

    /// <summary>
    /// Validates that two attributes are mutually exclusive (cannot both be present).
    /// </summary>
    protected static void ValidateMutuallyExclusive(
        IReadOnlyCollection<IppAttribute> attributes,
        string name1,
        string name2,
        Action<string, IppStatusCode> onError,
        string? customMessage = null,
        IppStatusCode statusCode = IppStatusCode.ClientErrorConflictingAttributes)
    {
        var hasFirst = HasNamedAttribute(attributes, name1);
        var hasSecond = HasNamedAttribute(attributes, name2);

        if (hasFirst && hasSecond)
        {
            onError(customMessage ?? $"'{name1}' and '{name2}' are mutually exclusive", statusCode);
        }
    }

    /// <summary>
    /// Validates that two attributes are co-dependent (must be either both present or both absent).
    /// </summary>
    protected static void ValidateCoDependent(
        IReadOnlyCollection<IppAttribute> attributes,
        string name1,
        string name2,
        Action<string, IppStatusCode> onError,
        string? customMessage = null,
        IppStatusCode statusCode = IppStatusCode.ClientErrorBadRequest)
    {
        var hasFirst = HasNamedAttribute(attributes, name1);
        var hasSecond = HasNamedAttribute(attributes, name2);

        if (hasFirst != hasSecond)
        {
            onError(customMessage ?? $"'{name1}' and '{name2}' must be either both present or both absent", statusCode);
        }
    }

    /// <summary>
    /// Validates that an integer attribute, if present, falls within the inclusive range [min, max].
    /// </summary>
    protected static void ValidateIntegerRange(
        IReadOnlyCollection<IppAttribute> attributes,
        string name,
        int min,
        int max,
        Action<string, IppStatusCode> onError,
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
            onError(msg, statusCode);
        }
    }
}

