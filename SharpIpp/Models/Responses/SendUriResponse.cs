using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

/// <summary>
///     https://tools.ietf.org/html/rfc2911#section-3.3.2
/// </summary>
public class SendUriResponse : IppResponse<OperationAttributes>, IIppJobResponse
{
    public JobAttributes? JobAttributes { get; set; }
}
