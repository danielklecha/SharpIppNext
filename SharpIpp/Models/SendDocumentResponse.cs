using SharpIpp.Protocol.Models;

namespace SharpIpp.Models
{
    /// <summary>
    ///     https://tools.ietf.org/html/rfc2911#section-3.3.1.2
    /// </summary>
    public class SendDocumentResponse : IppResponse, IIppJobResponse
    {
        public string JobUri { get; set; } = null!;

        public int JobId { get; set; }

        public JobState JobState { get; set; }

        public JobStateReason[] JobStateReasons { get; set; } = null!;

        public string? JobStateMessage { get; set; }

        public int? NumberOfInterveningJobs { get; set; }
    }
}
