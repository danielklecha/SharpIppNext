using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

public class CUPSGetPrintersResponse : IppResponse<OperationAttributes>
{
    public CUPSGetPrintersResponse()
    {
        Version = IppVersion.CUPS10;
    }

    public PrinterDescriptionAttributes[]? PrintersAttributes { get; set; }
}
