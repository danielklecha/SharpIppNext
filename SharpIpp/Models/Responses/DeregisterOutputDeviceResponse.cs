using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

/// <summary>
/// Deregister-Output-Device response.
/// See: PWG 5100.18-2025 Section 5.4.2
/// </summary>
[IppResponse]
public class DeregisterOutputDeviceResponse : IppResponse<OperationAttributes>
{
}
