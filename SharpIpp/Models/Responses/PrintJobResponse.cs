using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

public class PrintJobResponse : IppResponse<OperationAttributes>, IIppJobResponse
{
    public JobAttributes? JobAttributes { get; set; }
}
