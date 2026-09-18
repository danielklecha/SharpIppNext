using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Get-Resources request.
/// See: PWG 5100.22-2025 Section 6.3.7
/// </summary>
[IppRequest(IppOperation.GetResources)]
public class GetResourcesRequest : IppRequest<GetResourcesOperationAttributes>, IIppSystemRequest
{
}
