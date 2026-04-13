using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

/// <summary>
/// Get-Next-Document-Data response operation attributes.
/// See: PWG 5100.17-2014 Section 6.1.2
/// </summary>
public class GetNextDocumentDataResponseOperationAttributes : OperationAttributes
{
    /// <summary>
    /// The <c>compression</c> operation attribute.
    /// </summary>
    public Compression? Compression { get; set; }

    /// <summary>
    /// The <c>document-data-get-interval</c> operation attribute.
    /// </summary>
    public int? DocumentDataGetInterval { get; set; }

    /// <summary>
    /// The <c>last-document</c> operation attribute.
    /// </summary>
    public bool? LastDocument { get; set; }

    /// <summary>
    /// The <c>document-number</c> operation attribute.
    /// </summary>
    public int? DocumentNumber { get; set; }
}
