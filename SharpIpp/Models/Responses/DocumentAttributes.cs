using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

/// <summary>
///     PWG 5100.5-2024 Section 6.2 / Section 8.3-8.5
///     Document Status attributes returned in Document Creation responses.
/// </summary>
public class DocumentAttributes
{
    /// <summary>
    ///     The document-number attribute is the ordinal number of the Document within the Job.
    /// </summary>
    public int DocumentNumber { get; set; }

    /// <summary>
    ///     The document-state attribute indicates the current state of the Document.
    /// </summary>
    public DocumentState DocumentState { get; set; }

    /// <summary>
    ///     The document-state-reasons attribute provides additional information about the document's current state.
    /// </summary>
    public DocumentStateReason[]? DocumentStateReasons { get; set; }

    /// <summary>
    ///     The document-state-message attribute provides a localized description of the document-state and document-state-reasons.
    /// </summary>
    public string? DocumentStateMessage { get; set; }
}
