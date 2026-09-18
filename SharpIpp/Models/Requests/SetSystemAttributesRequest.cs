using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Set-System-Attributes operation.
/// See: PWG 5100.22-2025 Section 6.3.15
/// </summary>
[IppRequest(IppOperation.SetSystemAttributes)]
public class SetSystemAttributesRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest
{
}
