using SharpIpp.Protocol.Models;

namespace SharpIpp.Models
{
    public class GetJobAttributesResponse : IppResponse
    {
        public JobDescriptionAttributes? JobAttributes { get; set; }
    }
}
