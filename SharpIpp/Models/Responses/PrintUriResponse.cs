using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

public class PrintUriResponse : IppResponse<OperationAttributes>, IIppJobResponse
{
    public JobAttributes? JobAttributes { get; set; }
}
