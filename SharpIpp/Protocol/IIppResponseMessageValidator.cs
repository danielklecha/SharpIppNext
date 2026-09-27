using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol;

/// <summary>
/// Defines a validator for IPP response messages.
/// This is a low-end validator focusing on syntactic, structural, and protocol-level constraint validation
/// (such as versioning, character sets, attribute uniqueness, and basic response rules)
/// rather than high-end business logic.
/// </summary>
public interface IIppResponseMessageValidator
{
    /// <summary>
    /// Gets or sets a value indicating whether core IPP constraints (versioning, request-id, attribute uniqueness, operation attribute positions) are validated.
    /// </summary>
    bool ValidateCoreRules { get; set; }

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
    /// Validates the response message.
    /// </summary>
    /// <param name="response">The response message to validate.</param>
    void Validate(IIppResponseMessage? response);
}
