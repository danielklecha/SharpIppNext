using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Models.Requests;
public class CUPSGetPrintersOperationAttributes : OperationAttributes
{
    /// <summary>
    /// The client OPTIONALLY supplies this attribute to select the first printer that is returned
    /// See: IPP
    /// </summary>
    /// <code>first-printer-name</code>
    public string? FirstPrinterName { get; set; }
    /// <summary>
    /// The client OPTIONALLY supplies this attribute. The Printer object MUST support this attribute. It is an integer value that determines the maximum number of jobs that a client will receive from the Printer even if "which-jobs" or "my-jobs" constrain which jobs are returned. The limit is a "stateless limit" in that if the value supplied by the client is 'N', then only the first 'N' jobs are returned in the Get-Jobs Response. There is no mechanism to allow for the next 'M' jobs after the first 'N' jobs. If the client does not supply this attribute, the Printer object responds with all applicable jobs
    /// See: pwg5100.1-2022 Section 2.2
    /// </summary>
    /// <code>limit</code>
    public int? Limit { get; set; }
    /// <summary>
    /// The client OPTIONALLY supplies this attribute to select which printer is returned
    /// See: PWG 5100.22-2025 Section 7.1.5
    /// </summary>
    /// <code>printer-id</code>
    public int? PrinterId { get; set; }
    /// <summary>
    /// The client OPTIONALLY supplies this attribute to select which printers are returned
    /// See: pwg5100.15 - IPP FaxOut Service Section 7.4.18
    /// </summary>
    /// <code>printer-location</code>
    public string? PrinterLocation { get; set; }
    /// <summary>
    /// The client OPTIONALLY supplies a printer type enumeration to select which printers are returned
    /// See: IPP
    /// </summary>
    /// <code>printer-type</code>
    public PrinterType? PrinterType { get; set; }
    /// <summary>
    /// The client OPTIONALLY supplies a printer type mask enumeration to select which bits are used in the "printer-type" attribute
    /// See: IPP
    /// </summary>
    /// <code>printer-type-mask</code>
    public PrinterType? PrinterTypeMask { get; set; }
    /// <summary>
    /// The client OPTIONALLY supplies this attribute. The Printer object MUST support this attribute. It is a set of Job attribute names and/or attribute groups names in whose values the requester is interested. This set of attributes is returned for each Job object that is returned. The allowed attribute group names are the same as those defined in the Get-Job-Attributes operation in section 3.3.4. If the client does not supply this attribute, the Printer MUST respond as if the client had supplied this attribute with two values: 'job- uri' and 'job-id'
    /// See: pwg5100.1-2022 Section 6.9.4
    /// </summary>
    /// <code>requested-attributes</code>
    public string[]? RequestedAttributes { get; set; }
}
