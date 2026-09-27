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
    /// <summary>
    /// Gets the validation context containing supported printer attributes and configurations.
    /// </summary>
    IppRequestValidationContext Context { get; }

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
    /// Gets or sets a value indicating whether capability validation (e.g., media, finishings, sides, output-bin)
    /// respects the request's 'ipp-attribute-fidelity' operation attribute.
    /// When true, capability validation against printer supported attributes is only enforced if the request
    /// specifies 'ipp-attribute-fidelity' = true.
    /// When false, capability checks are applied independently of the client-supplied fidelity attribute.
    /// Spec: RFC 8011 Section 3.2.1.1, PWG 5100.x.
    /// </summary>
    bool UseIppAttributeFidelityForCapabilityValidation { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether mutual exclusivity between 'media' and 'media-col'
    /// at the job level is enforced. When false (default), both attributes are tolerated (e.g. for Chromebook/CUPS compatibility).
    /// </summary>
    bool EnforceMediaMutualExclusivity { get; set; }

    /// <summary>
    /// Validates the request message by executing enabled core, job, document, printer, and operation-specific rules.
    /// Spec: RFC 8011, PWG 5100.x.
    /// </summary>
    /// <param name="request">The IPP request message to validate.</param>
    /// <param name="context">The validation context, or <c>null</c> to use <see cref="Context"/>.</param>
    void Validate(IIppRequestMessage? request, IppRequestValidationContext? context = null);
}
