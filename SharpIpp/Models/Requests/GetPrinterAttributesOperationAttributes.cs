using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Models.Requests;
public class GetPrinterAttributesOperationAttributes : OperationAttributes
{
    /// <summary>
    /// The first-index IPP attribute.
    /// See: PWG 5100.13-2023 Section 6.1.3 and Section 8.2
    /// </summary>
    /// <code>first-index</code>
    public int? FirstIndex { get; set; }

    /// <summary>
    /// The client OPTIONALLY supplies this attribute. The Printer object MUST support this attribute. It is a set of Printer attribute names and/or attribute groups names in whose values the requester is interested
    /// See: pwg5100.1-2022 Section 6.9.4
    /// </summary>
    /// <code>requested-attributes</code>
    public string[]? RequestedAttributes { get; set; }
    /// <summary>
    /// The client OPTIONALLY supplies this attribute. The Printer object MUST support this attribute. The value of this attribute identifies the format of the supplied document data
    /// See: RFC 8011
    /// </summary>
    /// <code>document-format</code>
    /// <example>application/octet-stream</example>
    public string? DocumentFormat { get; set; }
}
