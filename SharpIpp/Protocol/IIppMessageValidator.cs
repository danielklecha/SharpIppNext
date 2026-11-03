namespace SharpIpp.Protocol;

/// <summary>
/// Defines common configuration properties for low-level IPP message validators.
/// </summary>
public interface IIppMessageValidator
{
    /// <summary>
    /// Gets or sets a value indicating whether core IPP constraints (versioning, request-id, attribute uniqueness, operation attribute positions) are validated.
    /// </summary>
    bool ValidateCoreRules { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether operation-specific validation rules are enforced.
    /// </summary>
    bool ValidateOperationSpecificRules { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the Operation Attributes group is validated.
    /// </summary>
    bool ValidateOperationAttributesGroup { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the Job Attributes group is validated.
    /// </summary>
    bool ValidateJobAttributesGroup { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the Printer Attributes group is validated.
    /// </summary>
    bool ValidatePrinterAttributesGroup { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the Unsupported Attributes group is validated.
    /// </summary>
    bool ValidateUnsupportedAttributesGroup { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the Subscription Attributes group is validated.
    /// </summary>
    bool ValidateSubscriptionAttributesGroup { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the Event Notification Attributes group is validated.
    /// </summary>
    bool ValidateEventNotificationAttributesGroup { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the Resource Attributes group is validated.
    /// </summary>
    bool ValidateResourceAttributesGroup { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the Document Attributes group is validated.
    /// </summary>
    bool ValidateDocumentAttributesGroup { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the System Attributes group is validated.
    /// </summary>
    bool ValidateSystemAttributesGroup { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether string length limits defined in RFC 8011 are validated.
    /// </summary>
    bool ValidateStringLengthLimits { get; set; }
}

