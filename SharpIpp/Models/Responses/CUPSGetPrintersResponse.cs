using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

public class CUPSGetPrintersResponse : IppResponse<OperationAttributes>
{
    public CUPSGetPrintersResponse()
    {
        Version = IppVersion.CUPS10;
    }
    /// <summary>
    /// The printers-attributes IPP attribute.
    /// See: IPP
    /// </summary>
    /// <code>printers-attributes</code>
    public PrinterDescriptionAttributes[]? PrintersAttributes { get; set; }
}
