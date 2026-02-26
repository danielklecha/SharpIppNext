using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

public class JobAttributes
{
    /// <summary>
    ///     The job-uri attribute identifies the Job object.
    /// </summary>
    public string? JobUri { get; set; }

    /// <summary>
    ///     The job-id attribute identifies the Job object.
    /// </summary>
    public int JobId { get; set; }

    /// <summary>
    ///     The job-state attribute identifies the current state of the job.
    /// </summary>
    public JobState JobState { get; set; }

    /// <summary>
    ///     The job-state-reasons attribute provides additional information about the job's current state.
    /// </summary>
    public JobStateReason[]? JobStateReasons { get; set; }

    /// <summary>
    ///     The job-state-message attribute provides a message from an operator, system administrator or "intelligent" process to indicate to the end user the reasons for modification or other management action taken on a job.
    /// </summary>
    public string? JobStateMessage { get; set; }

    /// <summary>
    ///     The number-of-intervening-jobs attribute indicates the number of jobs that are expected to complete before this job completes.
    /// </summary>
    public int? NumberOfInterveningJobs { get; set; }
}
