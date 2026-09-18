using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Get-System-Attributes operation.
/// See: PWG 5100.22-2025 Section 6.3.8
/// </summary>
[IppRequest(IppOperation.GetSystemAttributes)]
public class GetSystemAttributesRequest : IppRequest<GetSystemAttributesOperationAttributes>, IIppSystemRequest
{
}
