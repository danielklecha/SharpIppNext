using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

/// <summary>
/// Acknowledge-Job response.
/// See: PWG 5100.18-2025 Section 5.3.2
/// </summary>
[IppResponse]
public class AcknowledgeJobResponse : IppResponse<OperationAttributes>
{
}
