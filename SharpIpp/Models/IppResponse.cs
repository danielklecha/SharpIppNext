using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models
{
    public abstract class IppResponse : IIppResponse
    {
        public IppVersion Version { get; set; } = IppVersion.V1_1;

        public IppStatusCode StatusCode { get; set; }

        public int RequestId { get; set; }

        public string? StatusMessage { get; set; }

        public string[]? DetailedStatusMessage { get; set; }

        public string? DocumentAccessError { get; set; }
    }
}
