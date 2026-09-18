using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Acknowledge-Job operation.
/// See: PWG 5100.18-2025 Section 5.3
/// </summary>
[IppRequest(IppOperation.AcknowledgeJob)]
public class AcknowledgeJobRequest : IppRequest<AcknowledgeJobOperationAttributes>, IIppJobRequest
{
}
