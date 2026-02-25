using SharpIpp.Models.Responses;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol;

public interface IIppResponse
{
    IppVersion Version { get; set; }

    IppStatusCode StatusCode { get; set; }

    int RequestId { get; set; }

    OperationAttributes? OperationAttributes { get; set; }
}
