using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;
using SharpIpp.Validation;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Get-Printer-Supported-Values Operation Attributes.
/// See: RFC 3380 Section 4.3
/// </summary>
[IppAttribute]
public class GetPrinterSupportedValuesOperationAttributes : OperationAttributes
{
    /// <summary>
    /// The client OPTIONALLY supplies this attribute. The Printer object MUST support this attribute. It is a set of Printer attribute names and/or attribute groups names in whose values the requester is interested
    /// See: RFC 8011 Section 3.2.5.1
    /// </summary>
    /// <code>requested-attributes</code>
    [IppAttribute(IppAttributeNames.RequestedAttributes, Tag = Tag.Keyword)]
    public string[]? RequestedAttributes { get; set; }

    /// <summary>
    /// The client OPTIONALLY supplies this attribute. The Printer object MUST support this attribute. The value of this attribute identifies the format of the supplied document data
    /// See: RFC 8011 Section 3.2.5.1
    /// </summary>
    /// <code>document-format</code>
    /// <example>application/octet-stream</example>
    [IppAttribute(IppAttributeNames.DocumentFormat, Tag = Tag.MimeMediaType)]
    public DocumentFormat? DocumentFormat { get; set; }

    /// <summary>
    /// The first-index IPP attribute.
    /// See: PWG 5100.13-2023 Section 6.1.3
    /// </summary>
    /// <code>first-index</code>
    [Range(1, int.MaxValue)]
    [IppAttribute(IppAttributeNames.FirstIndex, Tag = Tag.Integer)]
    public int? FirstIndex { get; set; }

    /// <summary>
    /// The limit IPP attribute.
    /// See: PWG 5100.13-2023 Section 6.1.4
    /// </summary>
    /// <code>limit</code>
    [Range(1, int.MaxValue)]
    [IppAttribute(IppAttributeNames.Limit, Tag = Tag.Integer)]
    public int? Limit { get; set; }
}
