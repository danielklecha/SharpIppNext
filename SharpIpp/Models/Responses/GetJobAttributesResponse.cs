using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

public class GetJobAttributesResponse : IppResponse<OperationAttributes>
{
    /// <summary>
    /// The job-attributes IPP attribute.
    /// See: pwg5100.13 - IPP Driver Replacement Extensions v2.0
    /// </summary>
    /// <code>job-attributes</code>
    public JobDescriptionAttributes? JobAttributes { get; set; }
}
