using System;
using System.Collections.Generic;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol;

public enum OverrideMemberScope
{
    Job,
    Document,
    Sheet,
    Impression,
    Cell,
    Page,
}

public sealed class IppRequestValidationContext
{
    /// <summary>
    /// Optional supported override attributes (e.g. pages, document-numbers, sides).
    /// See: PWG 5100.6 Section 3.2.1 (overrides-supported)
    /// </summary>
    public IReadOnlyCollection<OverrideSupported>? OverridesSupported { get; set; }

    /// <summary>
    /// Optional supported output bins.
    /// See: PWG 5100.2 Section 4.1.2 (output-bin-supported)
    /// </summary>
    public IReadOnlyCollection<OutputBin>? OutputBinSupported { get; set; }

    /// <summary>
    /// Optional supported job requested-attributes group keyword values (for example: all, job-description, job-template, job-actual).
    /// Applied to Get-Job-Attributes and Get-Jobs validation only.
    /// See: RFC 8011 Section 3.2.5.1 / 3.2.6.1 and PWG 5100.8 Section 5.1
    /// </summary>
    public IReadOnlyCollection<string>? JobRequestedAttributeGroupKeywordsSupported { get; set; }

    /// <summary>
    /// Optional supported document requested-attributes group keyword values (for example: all, document-description, document-template).
    /// Applied to Get-Document-Attributes and Get-Documents validation only.
    /// See: PWG 5100.5 Section 6.2.1 / 6.3.1
    /// </summary>
    public IReadOnlyCollection<string>? DocumentRequestedAttributeGroupKeywordsSupported { get; set; }

    /// <summary>
    /// Optional supported notify-events keyword values for subscription-related requests.
    /// See: RFC 3995 Section 5.3.3 (notify-events-supported)
    /// </summary>
    public IReadOnlyCollection<string>? NotifyEventsSupported { get; set; }

    /// <summary>
    /// Optional current document state by document-number for state-aware validation.
    /// See: PWG 5100.5 Section 6.1.4 and Section 6.1.5
    /// </summary>
    public IReadOnlyDictionary<int, DocumentState>? DocumentStatesByNumber { get; set; }

    /// <summary>
    /// Optional current document-state-reasons by document-number for state-aware validation.
    /// See: PWG 5100.5 Section 6.1.4
    /// </summary>
    public IReadOnlyDictionary<int, IReadOnlyCollection<DocumentStateReason>>? DocumentStateReasonsByNumber { get; set; }

    /// <summary>
    /// Optional list of operations where <c>overrides</c> is supported by the target printer.
    /// If not provided, validator uses baseline PWG 5100.6 operation constraints.
    /// See: PWG 5100.6 Section 3.2 (overrides-supported)
    /// </summary>
    public IReadOnlyCollection<IppOperation>? OverridesSupportedOperations { get; set; }

    /// <summary>
    /// Optional map of override member name to override scope.
    /// When present, validator enforces section 4.1.5 by rejecting members with Job or Document scope.
    /// See: PWG 5100.6 Section 4.1.5 and Table 1
    /// </summary>
    public IReadOnlyDictionary<string, OverrideMemberScope>? OverrideMemberScopesByName { get; set; }

    /// <summary>
    /// Controls Set-Document-Attributes behavior when a target document is in the 'processing' state.
    /// PWG 5100.5 allows either successful-ok or client-error-not-possible in this case.
    /// See: PWG 5100.5 Section 6.1.5
    /// </summary>
    public bool AllowSetDocumentAttributesWhenProcessing { get; set; } = true;

    /// <summary>
    /// Optional supported media keyword values for fidelity-based validation.
    /// When non-empty and ipp-attribute-fidelity is true, the validator rejects unsupported media values.
    /// See: RFC 8011 Section 4.1.6.1 (ipp-attribute-fidelity) and Section 4.2.11 (media-supported)
    /// </summary>
    public IReadOnlyList<Media>? MediaSupported { get; set; }

    /// <summary>
    /// Optional supported finishings enum values for fidelity-based validation.
    /// When non-empty and ipp-attribute-fidelity is true, the validator rejects unsupported finishings values.
    /// See: RFC 8011 Section 4.1.6.1 (ipp-attribute-fidelity) and Section 4.2.6 (finishings-supported)
    /// </summary>
    public IReadOnlyList<Finishings>? FinishingsSupported { get; set; }

    /// <summary>
    /// Optional supported sides keyword values for fidelity-based validation.
    /// When non-empty and ipp-attribute-fidelity is true, the validator rejects unsupported sides values.
    /// See: RFC 8011 Section 4.1.6.1 (ipp-attribute-fidelity) and Section 4.2.8 (sides-supported)
    /// </summary>
    public IReadOnlyList<Sides>? SidesSupported { get; set; }

    /// <summary>
    /// Optional supported print-quality enum values for fidelity-based validation.
    /// When non-empty and ipp-attribute-fidelity is true, the validator rejects unsupported print-quality values.
    /// See: RFC 8011 Section 4.1.6.1 (ipp-attribute-fidelity) and Section 4.2.13 (print-quality-supported)
    /// </summary>
    public IReadOnlyList<PrintQuality>? PrintQualitySupported { get; set; }

    /// <summary>
    /// Optional supported orientation-requested enum values for fidelity-based validation.
    /// When non-empty and ipp-attribute-fidelity is true, the validator rejects unsupported orientation-requested values.
    /// See: RFC 8011 Section 4.1.6.1 (ipp-attribute-fidelity) and Section 4.2.10 (orientation-requested-supported)
    /// </summary>
    public IReadOnlyList<Orientation>? OrientationRequestedSupported { get; set; }

    /// <summary>
    /// Optional supported print-color-mode keyword values for fidelity-based validation.
    /// When non-empty and ipp-attribute-fidelity is true, the validator rejects unsupported print-color-mode values.
    /// See: RFC 8011 Section 4.1.6.1 (ipp-attribute-fidelity) and PWG 5100.13 Section 5.1.3 (print-color-mode-supported)
    /// </summary>
    public IReadOnlyList<PrintColorMode>? PrintColorModeSupported { get; set; }
}
