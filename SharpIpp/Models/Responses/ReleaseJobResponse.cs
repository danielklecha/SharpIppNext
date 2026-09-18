using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;
/// <summary>
/// Release-Job Response
/// See: RFC 2911 Section 3.3.6
/// </summary>
[IppResponse]
public class ReleaseJobResponse : IppResponse<OperationAttributes>
{
}
