using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Update-Output-Device-Attributes operation.
/// See: PWG 5100.18-2025 Section 5.10
/// </summary>
[IppRequest(IppOperation.UpdateOutputDeviceAttributes)]
public class UpdateOutputDeviceAttributesRequest : IppRequest<UpdateOutputDeviceAttributesOperationAttributes>, IIppPrinterRequest
{
    /// <summary>
    /// Printer attributes supplied by the Proxy.
    /// See: PWG 5100.18-2025 Section 5.10.1
    /// </summary>
    public PrinterDescriptionAttributes? PrinterAttributes { get; set; }
}
