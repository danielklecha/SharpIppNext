using System;
using System.Collections.Generic;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

public class GetPrinterAttributesResponse : IppResponse<OperationAttributes>
{
    /// <summary>
    /// The printer-attributes IPP attribute.
    /// See: RFC 8011 Section 4.2.5.2
    /// </summary>
    /// <code>printer-attributes</code>
    public PrinterDescriptionAttributes? PrinterAttributes { get; set; }
}
