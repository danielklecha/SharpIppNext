using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

public class GetJobsResponse : IppResponse<OperationAttributes>
{
    public JobDescriptionAttributes[]? JobsAttributes { get; set; }
}
