using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;
/// <summary>
/// PWG 5100.5-2024 Section 5.1.1.1
/// </summary>
public class CancelDocumentOperationAttributes : JobOperationAttributes
{
    /// <summary>
    /// The document-number IPP attribute.
    /// See: PWG 5100.5-2024 Section 6.2.4
    /// </summary>
    /// <code>document-number</code>
    public int DocumentNumber { get; set; }
    /// <summary>
    /// The document-message IPP attribute.
    /// See: pwg5100.5-2024 Section 6.2.3
    /// </summary>
    /// <code>document-message</code>
    public string? DocumentMessage { get; set; }
}
