using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Restart-System operation.
/// See: PWG 5100.22-2025 Section 6.3.13
/// </summary>
[IppRequest(IppOperation.RestartSystem)]
public class RestartSystemRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest
{
}
