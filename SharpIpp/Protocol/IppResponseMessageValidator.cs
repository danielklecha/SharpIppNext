using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Exceptions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol;

/// <inheritdoc />
public class IppResponseMessageValidator : IIppResponseMessageValidator
{
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
    };

    public bool ValidateCoreRules { get; set; } = true;

    public bool ValidateOperationAttributesGroup { get; set; } = true;

    public bool ValidateJobAttributesGroup { get; set; } = true;

    public bool ValidatePrinterAttributesGroup { get; set; } = true;

    public bool ValidateUnsupportedAttributesGroup { get; set; } = true;

    public bool ValidateSubscriptionAttributesGroup { get; set; } = true;

    public bool ValidateEventNotificationAttributesGroup { get; set; } = true;

    public bool ValidateResourceAttributesGroup { get; set; } = true;

    public bool ValidateDocumentAttributesGroup { get; set; } = true;

    public bool ValidateSystemAttributesGroup { get; set; } = true;

    /// <summary>
    /// Validates the response message by executing enabled core, group, and attribute uniqueness rules.
    /// </summary>
    public void Validate(IIppResponseMessage? response)
    {
        if (response is null)
            throw new ArgumentNullException(nameof(response));

        if (ValidateCoreRules)
            ValidateCore(response);
    }

    /// <summary>
    /// Validates that the response conforms to basic IPP constraints (valid version, request-id, unique attributes within groups, first/second operation attributes).
    /// </summary>
    private void ValidateCore(IIppResponseMessage response)
    {
        if (response.RequestId <= 0)
            throw new IppResponseException("Bad request-id value", response);

        if (response.Version < new IppVersion(1, 0))
            throw new IppResponseException("Unsupported IPP version", response);

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

        var operationAttributes = response.OperationAttributes.FirstOrDefault();

        if (operationAttributes == null || operationAttributes.Count == 0)
            throw new IppResponseException("No Operation Attributes", response);

        if (operationAttributes[0].Name != IppAttributeNames.AttributesCharset)
            throw new IppResponseException("attributes-charset MUST be the first attribute", response);

        var charsetValue = operationAttributes[0].Value?.ToString();

        if (!string.Equals(charsetValue, "utf-8", StringComparison.OrdinalIgnoreCase))
            throw new IppResponseException("attributes-charset MUST be 'utf-8'", response);

        if (operationAttributes.Count < 2 || operationAttributes[1].Name != IppAttributeNames.AttributesNaturalLanguage)
            throw new IppResponseException("attributes-natural-language MUST be the second attribute", response);
    }

    /// <summary>
    /// Validates that attributes are not duplicated (non-consecutive instances of the same name) within an attribute group list.
    /// </summary>
    private void ValidateUniqueAttributes(List<List<IppAttribute>> attributeGroups, string groupName, IIppResponseMessage response)
    {
        if (attributeGroups == null)
            return;

        foreach (var attributes in attributeGroups)
        {
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
                        throw new IppResponseException($"Duplicate attribute '{attr.Name}' in group '{groupName}'", response);
                    }
                    seenNames.Add(attr.Name);
                    currentName = attr.Name;
                }
            }
        }
    }
}
