using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models
{
    public class CUPSGetPrintersResponse : IppResponse
    {
        public CUPSGetPrintersResponse()
        {
            Version = IppVersion.CUPS10;
        }

        public PrinterDescriptionAttributes[]? Printers { get; set; }
    }
}
