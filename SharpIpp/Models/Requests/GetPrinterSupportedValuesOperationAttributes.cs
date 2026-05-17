using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Get-Printer-Supported-Values Operation Attributes.
/// See: RFC 3380 Section 4.3
/// </summary>
public class GetPrinterSupportedValuesOperationAttributes : OperationAttributes
{
    /// <summary>
    /// The client OPTIONALLY supplies this attribute. The Printer object MUST support this attribute. It is a set of Printer attribute names and/or attribute groups names in whose values the requester is interested
    /// See: RFC 8011 Section 3.2.5.1
    /// </summary>
    /// <code>requested-attributes</code>
    public string[]? RequestedAttributes { get; set; }

    /// <summary>
    /// The client OPTIONALLY supplies this attribute. The Printer object MUST support this attribute. The value of this attribute identifies the format of the supplied document data
    /// See: RFC 8011 Section 3.2.5.1
    /// </summary>
    /// <code>document-format</code>
    /// <example>application/octet-stream</example>
    public string? DocumentFormat { get; set; }

    /// <summary>
    /// The first-index IPP attribute.
    /// See: PWG 5100.13-2023 Section 6.1.3
    /// </summary>
    /// <code>first-index</code>
    public int? FirstIndex { get; set; }

    /// <summary>
    /// The limit IPP attribute.
    /// See: PWG 5100.13-2023 Section 6.1.4
    /// </summary>
    /// <code>limit</code>
    public int? Limit { get; set; }
}
