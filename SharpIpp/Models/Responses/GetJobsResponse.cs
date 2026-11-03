using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

[IppResponse(IppOperation.GetJobs)]
public class GetJobsResponse : IppResponse<OperationAttributes>
{
    /// <summary>
    /// The jobs-attributes IPP attribute.
    /// See: IPP
    /// </summary>
    /// <code>jobs-attributes</code>
    public JobDescriptionAttributes[]? JobsAttributes { get; set; }
}
