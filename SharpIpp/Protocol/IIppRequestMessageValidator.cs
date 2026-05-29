using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol;

/// <summary>
/// Defines a validator for IPP request messages.
/// This is a low-end validator focusing on syntactic, structural, and protocol-level constraint validation
/// (such as versioning, character sets, attribute uniqueness, range bounds, and basic operation rules)
/// rather than high-end business logic.
/// </summary>
public interface IIppRequestMessageValidator
{
    IppRequestValidationContext Context { get; }

    bool ValidateCoreRules { get; set; }

    bool ValidateOperationSpecificRules { get; set; }

    bool ValidateOperationAttributesGroup { get; set; }

    bool ValidateJobAttributesGroup { get; set; }

    bool ValidatePrinterAttributesGroup { get; set; }

    bool ValidateUnsupportedAttributesGroup { get; set; }

    bool ValidateSubscriptionAttributesGroup { get; set; }

    bool ValidateEventNotificationAttributesGroup { get; set; }

    bool ValidateResourceAttributesGroup { get; set; }

    bool ValidateDocumentAttributesGroup { get; set; }

    bool ValidateSystemAttributesGroup { get; set; }

    bool UseIppAttributeFidelityForCapabilityValidation { get; set; }

    void Validate(IIppRequestMessage? request, IppRequestValidationContext? context = null);
}
