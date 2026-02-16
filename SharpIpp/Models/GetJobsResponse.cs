using SharpIpp.Protocol.Models;

namespace SharpIpp.Models
{
    public class GetJobsResponse : IppResponse
    {
        public JobDescriptionAttributes[]? Jobs { get; set; }
    }
}
