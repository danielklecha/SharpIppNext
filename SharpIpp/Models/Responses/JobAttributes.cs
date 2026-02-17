using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

public class JobAttributes
{
    /// <summary>
    ///     job-uri
    /// </summary>
    public string JobUri { get; set; } = null!;

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
    public JobStateReason[] JobStateReasons { get; set; } = null!;

    /// <summary>
    ///     job-state-message
    /// </summary>
    public string? JobStateMessage { get; set; }

    /// <summary>
    ///     number-of-intervening-jobs
    /// </summary>
    public int? NumberOfInterveningJobs { get; set; }
}
