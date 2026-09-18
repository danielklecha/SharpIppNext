using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Get-Resource-Attributes request.
/// See: PWG 5100.22-2025 Section 6.2.3
/// </summary>
[IppRequest(IppOperation.GetResourceAttributes)]
public class GetResourceAttributesRequest : IppRequest<GetResourceAttributesOperationAttributes>, IIppSystemRequest
{
}
