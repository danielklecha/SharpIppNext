using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

public class GetJobAttributesResponse : IppResponse<OperationAttributes>
{
    public JobDescriptionAttributes? JobAttributes { get; set; }
}
