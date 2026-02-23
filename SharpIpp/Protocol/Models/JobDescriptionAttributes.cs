using System;

namespace SharpIpp.Protocol.Models
{
    public class JobDescriptionAttributes
    {
        /// <summary>
        ///     This REQUIRED attribute contains the ID of the job.  The Printer, on
        ///     receipt of a new job, generates an ID which identifies the new Job on
        ///     that Printer.  The Printer returns the value of the "job-id"
        ///     attribute as part of the response to a create request.  The 0 value
        ///     is not included to allow for compatibility with SNMP index values
        ///     which also cannot be 0.
        ///     https://tools.ietf.org/html/rfc2911#section-4.3.2
        /// </summary>
        /// <example>63</example>
        /// <code>job-id</code>
        public int? JobId { get; set; }

        /// <summary>
        /// https://tools.ietf.org/html/rfc2911#section-4.3.1
        /// </summary>
        public string? JobUri { get; set; }

        /// <summary>
        ///     This REQUIRED attribute identifies the Printer object that created
        ///     this Job object.  When a Printer object creates a Job object, it
        ///     populates this attribute with the Printer object URI that was used in
        ///     the create request.  This attribute permits a client to identify the
        ///     Printer object that created this Job object when only the Job
        ///     object's URI is available to the client.  The client queries the
        ///     creating Printer object to determine which languages, charsets,
        ///     operations, are supported for this Job.
        ///     https://tools.ietf.org/html/rfc2911#section-4.3.3
        /// </summary>
        /// <example>ipp://10.30.254.250:631/ipp/print</example>
        /// <code>job-printer-uri</code>
        public string? JobPrinterUri { get; set; }

        /// <summary>
        ///     This REQUIRED attribute is the name of the job.  It is a name that is
        ///     more user friendly than the "job-uri" attribute value.  It does not
        ///     need to be unique between Jobs.  The Job's "job-name" attribute is
        ///     set to the value supplied by the client in the "job-name" operation
        ///     attribute in the create request (see Section 3.2.1.1).   If, however,
        ///     the "job-name" operation attribute is not supplied by the client in
        ///     the create request, the Printer object, on creation of the Job, MUST
        ///     generate a name.
        ///     https://tools.ietf.org/html/rfc2911#section-4.3.5
        /// </summary>
        /// <example>job63</example>
        /// <code>job-name</code>
        public string? JobName { get; set; }

        /// <summary>
        ///     This REQUIRED attribute contains the name of the end user that
        ///     submitted the print job.  The Printer object sets this attribute to
        ///     the most authenticated printable name that it can obtain from the
        ///     authentication service over which the IPP operation was received.
        ///     Only if such is not available, does the Printer object use the value
        ///     supplied by the client in the "requesting-user-name" operation
        ///     attribute of the create operation (see Sections 4.4.2, 4.4.3, and 8).
        ///     https://tools.ietf.org/html/rfc2911#section-4.3.6
        /// </summary>
        /// <example>anonymous (en)</example>
        /// <code>job-originating-user-name</code>
        public string? JobOriginatingUserName { get; set; }

        /// <summary>
        ///     This attribute specifies the total number of octets processed in K
        ///     octets, i.e., in units of 1024 octets so far.  The value MUST be
        ///     rounded up, so that a job between 1 and 1024 octets inclusive MUST be
        ///     indicated as being 1, 1025 to 2048 inclusive MUST be 2, etc.
        ///     For implementations where multiple copies are produced by the
        ///     interpreter with only a single pass over the data, the final value
        ///     MUST be equal to the value of the "job-k-octets" attribute.  For
        ///     implementations where multiple copies are produced by the interpreter
        ///     by processing the data for each copy, the final value MUST be a
        ///     multiple of the value of the "job-k-octets" attribute.
        ///     https://tools.ietf.org/html/rfc2911#section-4.3.18.1
        /// </summary>
        /// <example>26</example>
        /// <code>job-k-octets-processed</code>
        public int? JobKOctetsProcessed { get; set; }

        /// <summary>
        ///     This attribute specifies the total size in number of impressions of
        ///     the document(s) being submitted.
        ///     As with "job-k-octets", this value MUST NOT include the
        ///     multiplicative factors contributed by the number of copies specified
        ///     by the "copies" attribute, independent of whether the device can
        ///     process multiple copies without making multiple passes over the job
        ///     or document data and independent of whether the output is collated or
        ///     not.  Thus the value is independent of the implementation and
        ///     reflects the size of the document(s) measured in impressions
        ///     independent of the number of copies.
        ///     As with "job-k-octets", this value MUST also not include the
        ///     multiplicative factor due to a copies instruction embedded in the
        ///     document data.  If the document data actually includes replications
        ///     of the document data, this value will include such replication.  In
        ///     other words, this value is always the number of impressions in the
        ///     source document data, rather than a measure of the number of
        ///     impressions to be produced by the job.
        ///     https://tools.ietf.org/html/rfc2911#section-4.3.17.2
        /// </summary>
        /// <example>no value</example>
        /// <code>job-impressions</code>
        public int? JobImpressions { get; set; }

        /// <summary>
        ///     This job attribute specifies the number of impressions completed for
        ///     the job so far.  For printing devices, the impressions completed
        ///     includes interpreting, marking, and stacking the output.
        ///     https://tools.ietf.org/html/rfc2911#section-4.3.18.2
        /// </summary>
        /// <example>0</example>
        /// <code>job-impressions-completed</code>
        public int? JobImpressionsCompleted { get; set; }

        /// <summary>
        ///     This attribute specifies the total number of media sheets to be
        ///     produced for this job.
        ///     Unlike the "job-k-octets" and the "job-impressions" attributes, this
        ///     value MUST include the multiplicative factors contributed by the
        ///     number of copies specified by the "copies" attribute and a 'number of
        ///     copies' instruction embedded in the document data, if any.  This
        ///     difference allows the system administrator to control the lower and
        ///     upper bounds of both (1) the size of the document(s) with "job-k-
        ///     octets-supported" and "job-impressions-supported" and (2) the size of
        ///     the job with "job-media-sheets-supported".
        ///     https://tools.ietf.org/html/rfc2911#section-4.3.17.3
        /// </summary>
        /// <example>no value</example>
        /// <code>job-media-sheets</code>
        public int? JobMediaSheets { get; set; }

        public string? JobMoreInfo { get; set; }

        public int? NumberOfDocuments { get; set; }

        public int? NumberOfInterveningJobs { get; set; }

        public string? OutputDeviceAssigned { get; set; }

        /// <summary>
        ///     This job attribute specifies the media-sheets completed marking and
        ///     stacking for the entire job so far whether those sheets have been
        ///     processed on one side or on both.
        ///     https://tools.ietf.org/html/rfc2911#section-4.3.18.3
        /// </summary>
        /// <example>0</example>
        /// <code>job-media-sheets-completed</code>
        public int? JobMediaSheetsCompleted { get; set; }

        /// <summary>
        ///     This REQUIRED attribute identifies the current state of the job.
        ///     Even though the IPP protocol defines seven values for job states
        ///     (plus the out-of-band 'unknown' value - see Section 4.1),
        ///     implementations only need to support those states which are
        ///     appropriate for the particular implementation.  In other words, a
        ///     Printer supports only those job states implemented by the output
        ///     device and available to the Printer object implementation.
        ///     https://tools.ietf.org/html/rfc2911#section-4.3.7
        /// </summary>
        /// <example>9</example>
        /// <code>job-state</code>
        public JobState? JobState { get; set; }

        /// <summary>
        ///     The Printer object OPTIONALLY returns the Job object's OPTIONAL
        ///     "job-state-message" attribute.  If the Printer object supports
        ///     this attribute then it MUST be returned in the response.  If
        ///     this attribute is not returned in the response, the client can
        ///     assume that the "job-state-message" attribute is not supported
        ///     and will not be returned in a subsequent Job object query.
        /// </summary>
        /// <example>The job completed successfully</example>
        /// <code>job-state-message</code>
        public string? JobStateMessage { get; set; }

        /// <summary>
        ///     The Printer object MUST return the Job object's REQUIRED "job-
        ///     state-reasons" attribute.
        /// </summary>
        /// <example>job-completed-successfully</example>
        /// <code>job-state-reasons</code>
        public JobStateReason[]? JobStateReasons { get; set; }

        /// <summary>
        ///     This attribute indicates the date and time at which the Job object
        ///     was created.
        ///     https://tools.ietf.org/html/rfc2911#section-4.3.14.5
        /// </summary>
        /// <example>22.04.2021 20:13:21 +03:00</example>
        /// <code>date-time-at-creation</code>
        public DateTimeOffset? DateTimeAtCreation { get; set; }

        /// <summary>
        ///     This attribute indicates the date and time at which the Job object
        ///     first began processing after the create operation or the most recent
        ///     Restart-Job operation.
        ///     https://tools.ietf.org/html/rfc2911#section-4.3.14.6
        /// </summary>
        /// <example>22.04.2021 20:13:22 +03:00</example>
        /// <code>date-time-at-processing</code>
        public DateTimeOffset? DateTimeAtProcessing { get; set; }

        /// <summary>
        ///     This attribute indicates the date and time at which the Job object
        ///     completed (or was canceled or aborted).
        ///     https://tools.ietf.org/html/rfc2911#section-4.3.14.7
        /// </summary>
        /// <example>22.04.2021 20:13:22 +03:00</example>
        /// <code>date-time-at-completed</code>
        public DateTimeOffset? DateTimeAtCompleted { get; set; }

        /// <summary>
        ///     This REQUIRED attribute indicates the time at which the Job object
        ///     was created.
        ///     https://tools.ietf.org/html/rfc2911#section-4.3.14.1
        /// </summary>
        /// <example>197753</example>
        /// <code>time-at-creation</code>
        public int? TimeAtCreation { get; set; }

        /// <summary>
        ///     This REQUIRED attribute indicates the time at which the Job object
        ///     first began processing after the create operation or the most recent
        ///     Restart-Job operation.  The out-of-band 'no-value' value is returned
        ///     if the job has not yet been in the 'processing' state
        ///     https://tools.ietf.org/html/rfc2911#section-4.3.14.2
        /// </summary>
        /// <example>197754</example>
        /// <code>time-at-processing</code>
        public int? TimeAtProcessing { get; set; }

        /// <summary>
        ///     This REQUIRED attribute indicates the time at which the Job object
        ///     completed (or was canceled or aborted).  The out-of-band 'no-value'
        ///     value is returned if the job has not yet completed, been canceled, or
        ///     aborted
        ///     https://tools.ietf.org/html/rfc2911#section-4.3.14.3
        /// </summary>
        /// <example>197754</example>
        /// <code>time-at-completed</code>
        public int? TimeAtCompleted { get; set; }

        /// <summary>
        ///     This REQUIRED Job Description attribute indicates the amount of time
        ///     (in seconds) that the Printer implementation has been up and running.
        ///     This attribute is an alias for the "printer-up-time" Printer
        ///     Description attribute (see Section 4.4.29).
        ///     https://tools.ietf.org/html/rfc2911#section-4.3.14.4
        /// </summary>
        /// <example>197775</example>
        /// <code>job-printer-up-time</code>
        public int? JobPrinterUpTime { get; set; }

        /// <summary>
        /// This attribute specifies the total size of the document(s) in K
        /// octets, i.e., in units of 1024 octets requested to be processed in
        /// the job.The value MUST be rounded up, so that a job between 1 and
        /// 1024 octets MUST be indicated as being 1, 1025 to 2048 MUST be 2,
        /// etc.
        /// https://tools.ietf.org/html/rfc2911#section-4.3.17.1
        /// </summary>
        public int? JobKOctets { get; set; }

        /// <summary>
        /// This attribute specifies additional detailed and technical
        /// information about the job.The Printer NEED NOT localize the
        /// message(s), since they are intended for use by the system
        /// administrator or other experienced technical persons.  Localization
        /// might obscure the technical meaning of such messages.Clients MUST
        /// NOT attempt to parse the value of this attribute.
        /// https://datatracker.ietf.org/doc/html/rfc2911#section-4.3.10
        /// </summary>
        public string[]? JobDetailedStatusMessages { get; set; }

        /// <summary>
        /// This attribute provides additional information about each document
        /// access error for this job encountered by the Printer after it
        /// returned a response to the Print-URI or Send-URI operation and
        /// subsequently attempted to access document(s) supplied in the Print-
        /// URI or Send-URI operation.For errors in the protocol that is
        /// identified by the URI scheme in the "document-uri" operation
        /// attribute, such as 'http:' or 'ftp:', the error code is returned in
        /// parentheses, followed by the URI.
        /// https://datatracker.ietf.org/doc/html/rfc2911#section-4.3.11
        /// </summary>
        public string[]? JobDocumentAccessErrors { get; set; }

        /// <summary>
        /// This attribute provides a message from an operator, system
        /// administrator or "intelligent" process to indicate to the end user
        /// the reasons for modification or other management action taken on a
        /// job.
        /// https://datatracker.ietf.org/doc/html/rfc2911#section-4.3.16
        /// </summary>
        public string? JobMessageFromOperator { get; set; }
    }
}
