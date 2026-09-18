using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;
/// <summary>
/// Cancel-Job Response
/// See: RFC 2911 Section 3.3.3
/// </summary>
[IppResponse]
public class CancelJobResponse : IppResponse<OperationAttributes>
{
}
