using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Set-Resource-Attributes operation.
/// See: PWG 5100.22-2025 Section 6.2.6
/// </summary>
[IppRequest(IppOperation.SetResourceAttributes)]
public class SetResourceAttributesRequest : IppRequest<SetResourceAttributesOperationAttributes>, IIppSystemRequest
{
}
