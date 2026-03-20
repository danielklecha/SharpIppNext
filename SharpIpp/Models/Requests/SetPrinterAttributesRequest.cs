using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Set-Printer-Attributes operation request.
/// </summary>
public class SetPrinterAttributesRequest : IppRequest<SetPrinterAttributesOperationAttributes>, IIppPrinterRequest
{
    /// <summary>
    /// Printer Description attributes to set for the target Printer.
    /// </summary>
    public PrinterDescriptionAttributes? PrinterAttributes { get; set; }
}
