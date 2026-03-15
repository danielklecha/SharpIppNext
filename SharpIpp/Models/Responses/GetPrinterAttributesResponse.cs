using System;
using System.Collections.Generic;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

public class GetPrinterAttributesResponse : IppResponse<OperationAttributes>
{
    /// <summary>
    /// The printer-attributes IPP attribute.
    /// See: pwg5100.1-2022 Section 6.9.4
    /// </summary>
    /// <code>printer-attributes</code>
    public PrinterDescriptionAttributes? PrinterAttributes { get; set; }
}
