using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models
{
    public interface IIppJobResponse : IIppResponse
    {
        /// <summary>
        ///     job-uri
        /// </summary>
        public string JobUri { get; set; }

        /// <summary>
        ///     job-id
        /// </summary>
        public int JobId { get; set; }

        /// <summary>
        ///     job-state
        /// </summary>
        public JobState JobState { get; set; }

        /// <summary>
        ///     job-state-reasons
        /// </summary>
        public JobStateReason[] JobStateReasons { get; set; }

        /// <summary>
        ///     job-state-message
        /// </summary>
        public string? JobStateMessage { get; set; }

        /// <summary>
        ///     number-of-intervening-jobs
        /// </summary>
        public int? NumberOfInterveningJobs { get; set; }
    }
}
