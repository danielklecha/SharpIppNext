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
    bool ValidateCoreRules { get; set; }

    bool ValidateOperationAttributesGroup { get; set; }

    bool ValidateJobAttributesGroup { get; set; }

    bool ValidatePrinterAttributesGroup { get; set; }

    bool ValidateUnsupportedAttributesGroup { get; set; }

    bool ValidateSubscriptionAttributesGroup { get; set; }

    bool ValidateEventNotificationAttributesGroup { get; set; }

    bool ValidateResourceAttributesGroup { get; set; }

    bool ValidateDocumentAttributesGroup { get; set; }

    bool ValidateSystemAttributesGroup { get; set; }

    /// <summary>
    /// Validates the response message.
    /// </summary>
    /// <param name="response">The response message to validate.</param>
    void Validate(IIppResponseMessage? response);
}
