using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Deregister-Output-Device operation.
/// See: PWG 5100.18-2025 Section 5.4
/// </summary>
[IppRequest(IppOperation.DeregisterOutputDevice)]
public class DeregisterOutputDeviceRequest : IppRequest<DeregisterOutputDeviceOperationAttributes>, IIppPrinterRequest
{
}
