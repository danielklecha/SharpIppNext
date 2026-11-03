using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol;

/// <summary>
/// Defines a validator for IPP request messages.
/// This is a low-end validator focusing on syntactic, structural, and protocol-level constraint validation
/// (such as versioning, character sets, attribute uniqueness, range bounds, and basic operation rules)
/// rather than high-end business logic.
/// </summary>
public interface IIppRequestMessageValidator : IIppMessageValidator
{
    /// <summary>
    /// Gets the validation context containing supported printer attributes and configurations.
    /// </summary>
    IppRequestValidationContext Context { get; }

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
    /// Gets or sets a value indicating whether target attribute order in the Operation Attributes group is strictly validated.
    /// When true, verifies that the target URI (printer-uri, job-uri, or system-uri) immediately follows attributes-charset and attributes-natural-language,
    /// followed by target secondary identifiers (job-id, document-number, resource-id, etc.) in the exact order required by RFC 8011 Section 4.1.5 and PWG 5100.5 Section 5.1.
    /// </summary>
    bool ValidateTargetAttributeOrder { get; set; }

    /// <summary>
    /// Validates the request message by executing enabled core, job, document, printer, and operation-specific rules.
    /// Spec: RFC 8011, PWG 5100.x.
    /// </summary>
    /// <param name="request">The IPP request message to validate.</param>
    /// <param name="context">The validation context, or <c>null</c> to use <see cref="Context"/>.</param>
    void Validate(IIppRequestMessage? request, IppRequestValidationContext? context = null);
}
