using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;
using SharpIpp.Validation;

namespace SharpIpp.Models.Requests;
/// <summary>
/// PWG 5100.5-2024 Section 5.1.1.1
/// </summary>
[IppAttribute]
public class CancelDocumentOperationAttributes : JobOperationAttributes
{
    /// <summary>
    /// The document-number IPP attribute.
    /// See: PWG 5100.5-2024 Section 5.1.1.1
    /// See: PWG 5100.5-2024 Section 6.2.4
    /// See: PWG 5100.5-2024 Section 6.4.2
    /// See: PWG 5100.18-2025 Section 4.6
    /// </summary>
    /// <code>document-number</code>
    [Range(1, int.MaxValue)]
    [IppAttribute(IppAttributeNames.DocumentNumber, Tag = Tag.Integer)]
    public int DocumentNumber { get; set; }
    /// <summary>
    /// The document-message IPP attribute.
    /// See: PWG 5100.5-2024 Section 5.1.1.1
    /// See: PWG 5100.5-2024 Section 6.2.3
    /// </summary>
    /// <code>document-message</code>
    [IppAttribute(IppAttributeNames.DocumentMessage, Tag = Tag.TextWithoutLanguage)]
    public string? DocumentMessage { get; set; }
}
