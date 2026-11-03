using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;
namespace SharpIpp.Models.Responses;

/// <summary>
/// Set-Job-Attributes operation response.
/// See: RFC 3380 Section 4.2
/// </summary>
[IppResponse(IppOperation.SetJobAttributes)]
public class SetJobAttributesResponse : IppResponse<OperationAttributes>
{
}
