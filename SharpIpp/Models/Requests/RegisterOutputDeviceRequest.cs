using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Register-Output-Device operation.
/// See: PWG 5100.22-2025 Section 6.3.12
/// </summary>
[IppRequest(IppOperation.RegisterOutputDevice)]
public class RegisterOutputDeviceRequest : IppRequest<RegisterOutputDeviceOperationAttributes>, IIppSystemRequest
{
}
