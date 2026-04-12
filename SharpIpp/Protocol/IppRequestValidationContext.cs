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
    public string? Source { get; set; }

    public IReadOnlyCollection<OverrideSupported>? OverridesSupported { get; set; }

    public IReadOnlyCollection<OutputBin>? OutputBinSupported { get; set; }

    /// <summary>
    /// Optional supported job requested-attributes group keyword values (for example: all, job-description, job-template, job-actual).
    /// Applied to Get-Job-Attributes and Get-Jobs validation only.
    /// </summary>
    public IReadOnlyCollection<string>? JobRequestedAttributeGroupKeywordsSupported { get; set; }

    /// <summary>
    /// Optional supported document requested-attributes group keyword values (for example: all, document-description, document-template).
    /// Applied to Get-Document-Attributes and Get-Documents validation only.
    /// </summary>
    public IReadOnlyCollection<string>? DocumentRequestedAttributeGroupKeywordsSupported { get; set; }

    /// <summary>
    /// Optional supported notify-events keyword values for subscription-related requests.
    /// </summary>
    public IReadOnlyCollection<string>? NotifyEventsSupported { get; set; }

    /// <summary>
    /// Optional current document state by document-number for state-aware validation.
    /// </summary>
    public IReadOnlyDictionary<int, DocumentState>? DocumentStatesByNumber { get; set; }

    /// <summary>
    /// Optional current document-state-reasons by document-number for state-aware validation.
    /// </summary>
    public IReadOnlyDictionary<int, IReadOnlyCollection<DocumentStateReason>>? DocumentStateReasonsByNumber { get; set; }

    /// <summary>
    /// Optional list of operations where <c>overrides</c> is supported by the target printer.
    /// If not provided, validator uses baseline PWG 5100.6 operation constraints.
    /// </summary>
    public IReadOnlyCollection<IppOperation>? OverridesSupportedOperations { get; set; }

    /// <summary>
    /// Optional map of override member name to override scope.
    /// When present, validator enforces section 4.1.5 by rejecting members with Job or Document scope.
    /// </summary>
    public IReadOnlyDictionary<string, OverrideMemberScope>? OverrideMemberScopesByName { get; set; }

    /// <summary>
    /// Controls Set-Document-Attributes behavior when a target document is in the 'processing' state.
    /// PWG 5100.5 allows either successful-ok or client-error-not-possible in this case.
    /// </summary>
    public bool AllowSetDocumentAttributesWhenProcessing { get; set; } = true;
}
