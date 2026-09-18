using System;
using SharpIpp.Mapping;
using SharpIpp.Protocol;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Attributes describing the current status of a Job object.
/// This is a subset of <see cref="JobDescriptionAttributes"/> containing only the
/// status-related attributes returned in job status responses.
/// See: RFC 8011
/// See: PWG 5100.7-2023
/// </summary>
public class JobStatusAttributes
{
    /// <summary>
    /// This REQUIRED attribute contains the ID of the job. The Printer, on
    /// receipt of a new job, generates an ID which identifies the new Job on
    /// that Printer. The Printer returns the value of the "job-id"
    /// attribute as part of the response to a create request. The 0 value
    /// is not included to allow for compatibility with SNMP index values
    /// which also cannot be 0.
    /// Type: integer(1:MAX)
    /// See: RFC 8011 Section 5.3.1
    /// </summary>
    /// <code>job-id</code>
    [IppAttribute(IppAttributeNames.JobId, Tag.Integer)]
    public int? JobId { get; set; }

    /// <summary>
    /// This REQUIRED attribute contains the URI for the Job object.
    /// See: RFC 8011 Section 5.3.2
    /// </summary>
    /// <code>job-uri</code>
    [Obsolete("The 'job-uri' attribute is deprecated in favor of 'job-id'. See RFC 8011 Section 5.3.2.")]
    [IppAttribute(IppAttributeNames.JobUri, Tag.Uri)]
    public Uri? JobUri { get; set; }

    /// <summary>
    /// This REQUIRED attribute identifies the Printer object that created
    /// this Job object. When a Printer object creates a Job object, it
    /// populates this attribute with the Printer object URI that was used in
    /// the create request. This attribute permits a client to identify the
    /// Printer object that created this Job object when only the Job
    /// object's URI is available to the client.
    /// See: RFC 8011 Section 5.3.3
    /// </summary>
    /// <code>job-printer-uri</code>
    [IppAttribute(IppAttributeNames.JobPrinterUri, Tag.Uri)]
    public Uri? JobPrinterUri { get; set; }

    /// <summary>
    /// This REQUIRED attribute is the name of the job. It is a name that is
    /// more user friendly than the "job-uri" attribute value. It does not
    /// need to be unique between Jobs.
    /// See: RFC 8011 Section 5.3.5
    /// </summary>
    /// <code>job-name</code>
    [IppAttribute(IppAttributeNames.JobName, Tag.NameWithoutLanguage)]
    public string? JobName { get; set; }

    /// <summary>
    /// This REQUIRED attribute contains the name of the end user that
    /// submitted the print job. The Printer object sets this attribute to
    /// the most authenticated printable name that it can obtain from the
    /// authentication service over which the IPP operation was received.
    /// See: RFC 8011 Section 5.3.6
    /// </summary>
    /// <code>job-originating-user-name</code>
    [IppAttribute(IppAttributeNames.JobOriginatingUserName, Tag.NameWithoutLanguage)]
    public string? JobOriginatingUserName { get; set; }

    /// <summary>
    /// This REQUIRED attribute identifies the current state of the job.
    /// Even though the IPP protocol defines seven values for job states
    /// (plus the out-of-band 'unknown' value), implementations only need to
    /// support those states which are appropriate for the particular
    /// implementation.
    /// See: RFC 8011 Section 5.3.7
    /// </summary>
    /// <code>job-state</code>
    [IppAttribute(IppAttributeNames.JobState, Tag.Enum)]
    public JobState? JobState { get; set; }

    /// <summary>
    /// The Printer object MUST return the Job object's REQUIRED
    /// "job-state-reasons" attribute.
    /// See: RFC 8011 Section 5.3.8
    /// </summary>
    /// <code>job-state-reasons</code>
    [IppAttribute(IppAttributeNames.JobStateReasons)]
    public JobStateReason[]? JobStateReasons { get; set; }

    /// <summary>
    /// The Printer object OPTIONALLY returns the Job object's OPTIONAL
    /// "job-state-message" attribute. If the Printer object supports
    /// this attribute then it MUST be returned in the response.
    /// See: RFC 8011 Section 5.3.9
    /// </summary>
    /// <code>job-state-message</code>
    [IppAttribute(IppAttributeNames.JobStateMessage, Tag.TextWithoutLanguage)]
    public string? JobStateMessage { get; set; }

    /// <summary>
    /// This job attribute specifies the number of impressions completed for
    /// the job so far. For printing devices, the impressions completed
    /// includes interpreting, marking, and stacking the output.
    /// See: RFC 8011 Section 5.3.18.2
    /// </summary>
    /// <code>job-impressions-completed</code>
    [IppAttribute(IppAttributeNames.JobImpressionsCompleted)]
    public int? JobImpressionsCompleted { get; set; }

    /// <summary>
    /// This job attribute specifies the media-sheets completed marking and
    /// stacking for the entire job so far whether those sheets have been
    /// processed on one side or on both.
    /// See: RFC 8011 Section 5.3.18.3
    /// </summary>
    /// <code>job-media-sheets-completed</code>
    [IppAttribute(IppAttributeNames.JobMediaSheetsCompleted)]
    public int? JobMediaSheetsCompleted { get; set; }

    /// <summary>
    /// This attribute specifies the total size in K octets processed so far.
    /// The value MUST be rounded up, so that a job between 1 and 1024 octets
    /// inclusive MUST be indicated as being 1, 1025 to 2048 inclusive MUST be 2, etc.
    /// See: RFC 8011 Section 5.3.18.1
    /// </summary>
    /// <code>job-k-octets-processed</code>
    [IppAttribute(IppAttributeNames.JobKOctetsProcessed)]
    public int? JobKOctetsProcessed { get; set; }

    /// <summary>
    /// This attribute specifies the number of pages completed for the
    /// Job so far.
    /// See: PWG 5100.7-2023 Section 6.7.4
    /// </summary>
    /// <code>job-pages-completed</code>
    [IppAttribute(IppAttributeNames.JobPagesCompleted)]
    public int? JobPagesCompleted { get; set; }

    /// <summary>
    /// This attribute specifies detailed page counters completed for the Job so far.
    /// See: PWG 5100.7-2023 Section 6.7.5
    /// </summary>
    /// <code>job-pages-completed-col</code>
    [IppAttribute(IppAttributeNames.JobPagesCompletedCol)]
    public JobCounter? JobPagesCompletedCol { get; set; }

    /// <summary>
    /// This attribute specifies detailed impression counters completed for the Job so far.
    /// See: PWG 5100.7-2023 Section 6.7.2
    /// </summary>
    /// <code>job-impressions-completed-col</code>
    [IppAttribute(IppAttributeNames.JobImpressionsCompletedCol)]
    public JobCounter? JobImpressionsCompletedCol { get; set; }

    /// <summary>
    /// This attribute specifies detailed media sheet counters completed for the Job so far.
    /// See: PWG 5100.7-2023 Section 6.7.3
    /// </summary>
    /// <code>job-media-sheets-completed-col</code>
    [IppAttribute(IppAttributeNames.JobMediaSheetsCompletedCol)]
    public JobCounter? JobMediaSheetsCompletedCol { get; set; }

    /// <summary>
    /// This attribute specifies the total number of seconds that the
    /// Job has been processing.
    /// See: PWG 5100.7-2023 Section 6.7.6
    /// </summary>
    /// <code>job-processing-time</code>
    [IppAttribute(IppAttributeNames.JobProcessingTime)]
    public int? JobProcessingTime { get; set; }

    /// <summary>
    /// This attribute specifies the number of errors that were detected
    /// while processing the Job.
    /// See: PWG 5100.7-2023 Section 6.2.1
    /// </summary>
    /// <code>errors-count</code>
    [IppAttribute(IppAttributeNames.ErrorsCount)]
    public int? ErrorsCount { get; set; }

    /// <summary>
    /// This attribute specifies the number of warnings that were detected
    /// while processing the Job.
    /// See: PWG 5100.7-2023 Section 6.2.3
    /// </summary>
    /// <code>warnings-count</code>
    [IppAttribute(IppAttributeNames.WarningsCount)]
    public int? WarningsCount { get; set; }

    /// <summary>
    /// This attribute indicates the number of jobs that are
    /// "ahead" of this job in the relative chronological order of
    /// expected time to complete.
    /// See: RFC 8011 Section 5.3.15
    /// </summary>
    /// <code>number-of-intervening-jobs</code>
    [IppAttribute(IppAttributeNames.NumberOfInterveningJobs)]
    public int? NumberOfInterveningJobs { get; set; }

    /// <summary>
    /// This attribute identifies the output device to which the
    /// Printer object has assigned this job.
    /// See: RFC 8011 Section 5.3.13
    /// </summary>
    /// <code>output-device-assigned</code>
    [IppAttribute(IppAttributeNames.OutputDeviceAssigned, Tag.NameWithoutLanguage)]
    public string? OutputDeviceAssigned { get; set; }

    /// <summary>
    /// This REQUIRED Job Description attribute indicates the amount of time
    /// (in seconds) that the Printer implementation has been up and running.
    /// This attribute is an alias for the "printer-up-time" Printer
    /// Description attribute.
    /// See: RFC 8011 Section 5.3.14.4
    /// </summary>
    /// <code>job-printer-up-time</code>
    [IppAttribute(IppAttributeNames.JobPrinterUpTime)]
    public int? JobPrinterUpTime { get; set; }

    /// <summary>
    /// This REQUIRED attribute indicates the time at which the Job object
    /// was created.
    /// See: RFC 8011 Section 5.3.14.1
    /// </summary>
    /// <code>time-at-creation</code>
    [IppAttribute(IppAttributeNames.TimeAtCreation)]
    public int? TimeAtCreation { get; set; }

    /// <summary>
    /// This REQUIRED attribute indicates the time at which the Job object
    /// first began processing after the create operation or the most recent
    /// Restart-Job operation. The out-of-band 'no-value' value is returned
    /// if the job has not yet been in the 'processing' state.
    /// See: RFC 8011 Section 5.3.14.2
    /// </summary>
    /// <code>time-at-processing</code>
    [IppAttribute(IppAttributeNames.TimeAtProcessing)]
    public int? TimeAtProcessing { get; set; }

    /// <summary>
    /// This REQUIRED attribute indicates the time at which the Job object
    /// completed (or was canceled or aborted). The out-of-band 'no-value'
    /// value is returned if the job has not yet completed, been canceled, or
    /// aborted.
    /// See: RFC 8011 Section 5.3.14.3
    /// </summary>
    /// <code>time-at-completed</code>
    [IppAttribute(IppAttributeNames.TimeAtCompleted)]
    public int? TimeAtCompleted { get; set; }

    /// <summary>
    /// This attribute indicates the date and time at which the Job object
    /// was created.
    /// See: RFC 8011 Section 5.3.14.5
    /// </summary>
    /// <code>date-time-at-creation</code>
    [IppAttribute(IppAttributeNames.DateTimeAtCreation)]
    public DateTimeOffset? DateTimeAtCreation { get; set; }

    /// <summary>
    /// This attribute indicates the date and time at which the Job object
    /// first began processing after the create operation or the most recent
    /// Restart-Job operation.
    /// See: RFC 8011 Section 5.3.14.6
    /// </summary>
    /// <code>date-time-at-processing</code>
    [IppAttribute(IppAttributeNames.DateTimeAtProcessing)]
    public DateTimeOffset? DateTimeAtProcessing { get; set; }

    /// <summary>
    /// This attribute indicates the date and time at which the Job object
    /// completed (or was canceled or aborted).
    /// See: RFC 8011 Section 5.3.14.7
    /// </summary>
    /// <code>date-time-at-completed</code>
    [IppAttribute(IppAttributeNames.DateTimeAtCompleted)]
    public DateTimeOffset? DateTimeAtCompleted { get; set; }

    /// <summary>
    /// The job-pages-completed-current-copy attribute.
    /// See: PWG 5100.13-2023 (obsolete)
    /// </summary>
    [Obsolete("The 'job-pages-completed-current-copy' attribute is obsolete. See PWG 5100.13-2023 Section 7.1.")]
    [IppAttribute(IppAttributeNames.JobPagesCompletedCurrentCopy)]
    public int? JobPagesCompletedCurrentCopy { get; set; }

    /// <summary>
    /// The pages-completed-current-copy attribute.
    /// See: PWG 5100.13-2023 (obsolete)
    /// </summary>
    [Obsolete("The 'pages-completed-current-copy' attribute is obsolete. See PWG 5100.13-2023 Section 7.1.")]
    [IppAttribute(IppAttributeNames.PagesCompletedCurrentCopy)]
    public int? PagesCompletedCurrentCopy { get; set; }
}
