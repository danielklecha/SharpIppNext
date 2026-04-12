namespace SharpIpp.Protocol;

public interface IIppRequestValidator
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

    void Validate(IIppRequestMessage? request);
}
