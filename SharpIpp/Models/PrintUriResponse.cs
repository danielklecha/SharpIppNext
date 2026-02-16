using SharpIpp.Protocol.Models;

namespace SharpIpp.Models
{
    public class PrintUriResponse : IppResponse, IIppJobResponse
    {
        public string JobUri { get; set; } = null!;

        public int JobId { get; set; }

        public JobState JobState { get; set; }

        public JobStateReason[] JobStateReasons { get; set; } = null!;

        public string? JobStateMessage { get; set; }

        public int? NumberOfInterveningJobs { get; set; }
    }
}
