using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Models.Requests;
/// <summary>
/// The operation attributes for the Get-Printer-Attributes operation.
/// See: RFC 2911 Section 3.2.5
/// </summary>
public class GetPrinterAttributesOperationAttributes : OperationAttributes
{
    /// <summary>
    /// The limit IPP attribute.
    /// See: PWG 5100.13-2023 Section 6.1.4
    /// </summary>
    /// <code>limit</code>
    [System.ComponentModel.DataAnnotations.Range(1, int.MaxValue)]
    public int? Limit { get; set; }

    /// <summary>
    /// The ID of the target Printer object.
    /// See: PWG 5100.22-2025 Section 7.1.5
    /// </summary>
    /// <code>printer-id</code>
    [System.ComponentModel.DataAnnotations.Range(1, 65535)]
    public int? PrinterId { get; set; }

    /// <summary>
    /// The first-index IPP attribute.
    /// See: PWG 5100.13-2023 Section 6.1.3 and Section 8.2
    /// </summary>
    /// <code>first-index</code>
    [System.ComponentModel.DataAnnotations.Range(1, int.MaxValue)]
    public int? FirstIndex { get; set; }

    /// <summary>
    /// The client OPTIONALLY supplies this attribute. The Printer object MUST support this attribute. It is a set of Printer attribute names and/or attribute groups names in whose values the requester is interested
    /// See: RFC 8011 Section 4.2.5.1
    /// See: RFC 8011 Section 4.2.6.1
    /// See: RFC 8011 Section 4.3.4.1
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

    /// <summary>
    /// The <c>output-device-uuid</c> operation attribute.
    /// See: PWG 5100.18-2025 Section 7.1.8
    /// See: PWG 5100.18-2025 Section 8.4
    /// </summary>
    /// <code>output-device-uuid</code>
    public Uri? OutputDeviceUuid { get; set; }
}
