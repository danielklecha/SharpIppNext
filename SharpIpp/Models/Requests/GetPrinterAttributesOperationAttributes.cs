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
    ///     The client OPTIONALLY supplies this attribute.  The Printer
    ///     object MUST support this attribute.  It is a set of Printer
    ///     attribute names and/or attribute groups names in whose values
    ///     the requester is interested.
    /// </summary>
    public string[]? RequestedAttributes { get; set; }

    /// <summary>
    ///     The client OPTIONALLY supplies this attribute.  The Printer
    ///     object MUST support this attribute.  The value of this
    ///     attribute identifies the format of the supplied document data.
    /// </summary>
    /// <example>application/octet-stream</example>
    public string? DocumentFormat { get; set; }
}
