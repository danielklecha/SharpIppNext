using System;
using System.Collections.Generic;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

public class GetPrinterAttributesResponse : IppResponse<OperationAttributes>
{
    public PrinterDescriptionAttributes? PrinterAttributes { get; set; }
}
