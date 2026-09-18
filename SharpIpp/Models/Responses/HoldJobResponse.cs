using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;
/// <summary>
/// Hold-Job Response
/// See: RFC 2911 Section 3.3.5
/// </summary>
[IppResponse]
public class HoldJobResponse : IppResponse<OperationAttributes>
{
}
