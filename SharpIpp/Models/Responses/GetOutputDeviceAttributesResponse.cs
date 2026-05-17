using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

/// <summary>
/// Get-Output-Device-Attributes response.
/// See: PWG 5100.18-2025 Section 6.1.2
/// </summary>
public class GetOutputDeviceAttributesResponse : IppResponse<OperationAttributes>
{
}
