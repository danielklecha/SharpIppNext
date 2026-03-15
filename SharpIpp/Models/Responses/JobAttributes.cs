using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

public class JobAttributes
{
    /// <summary>
    /// The job-uri IPP attribute.
    /// See: RFC 8011 Section 5.3.2
    /// </summary>
    /// <code>job-uri</code>
    public string? JobUri { get; set; }
    /// <summary>
    /// The job-id IPP attribute.
    /// See: RFC 8011 Section 5.3.1
    /// </summary>
    /// <code>job-id</code>
    public int JobId { get; set; }
    /// <summary>
    /// The job-state IPP attribute.
    /// See: RFC 8011 Section 5.3.7
    /// </summary>
    /// <code>job-state</code>
    public JobState JobState { get; set; }
    /// <summary>
    /// The job-state-reasons IPP attribute.
    /// See: RFC 8011 Section 5.3.8
    /// </summary>
    /// <code>job-state-reasons</code>
    public JobStateReason[]? JobStateReasons { get; set; }
    /// <summary>
    /// The job-state-message IPP attribute.
    /// See: pwg5100.15 - IPP FaxOut Service Section 7.4.18
    /// </summary>
    /// <code>job-state-message</code>
    public string? JobStateMessage { get; set; }
    /// <summary>
    /// The number-of-intervening-jobs IPP attribute.
    /// See: pwg5100.7-2023 Section 6.1.1
    /// </summary>
    /// <code>number-of-intervening-jobs</code>
    public int? NumberOfInterveningJobs { get; set; }
}
