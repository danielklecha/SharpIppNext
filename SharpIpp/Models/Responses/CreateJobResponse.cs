using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

public class CreateJobResponse : IppResponse<OperationAttributes>, IIppJobResponse
{
    public JobAttributes? JobAttributes { get; set; }
}
