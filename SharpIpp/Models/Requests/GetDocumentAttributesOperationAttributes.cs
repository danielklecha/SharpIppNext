using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;
/// <summary>
/// PWG 5100.5-2024 Section 5.1.2.1
/// </summary>
public class GetDocumentAttributesOperationAttributes : JobOperationAttributes
{
    /// <summary>
    /// The document-number IPP attribute.
    /// See: PWG 5100.5-2024 Section 6.2.4
    /// </summary>
    /// <code>document-number</code>
    [System.ComponentModel.DataAnnotations.Range(1, int.MaxValue)]
    public int DocumentNumber { get; set; }
    /// <summary>
    /// The requested-attributes IPP attribute.
    /// See: PWG 5100.5-2024 Section 5.1.2.1
    /// See: RFC 8011 Section 4.2.5.1
    /// </summary>
    /// <code>requested-attributes</code>
    public string[]? RequestedAttributes { get; set; }
}
