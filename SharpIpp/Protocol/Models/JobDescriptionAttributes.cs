using SharpIpp.Mapping;
using System;
using SharpIpp.Validation;

namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// Attributes describing a Job object, including its identity, state, and processing details.
    /// See: RFC 8011
    /// See: PWG 5100.1-2022
    /// See: PWG 5100.3-2023
    /// See: PWG 5100.5-2024
    /// See: PWG 5100.7-2023
    /// See: PWG 5100.8-2003
    /// See: PWG 5100.11-2024
    /// See: PWG 5100.13-2023
    /// See: PWG 5100.15-2013
    /// See: PWG 5100.18-2025
    /// See: PWG 5100.21-2019
    /// See: PWG 5100.22-2025
    /// </summary>
    [IppAttribute]
public class JobDescriptionAttributes
    {
        /// <summary>
        /// This REQUIRED attribute contains the ID of the job.  The Printer, on
        /// receipt of a new job, generates an ID which identifies the new Job on
        /// that Printer.  The Printer returns the value of the "job-id"
        /// attribute as part of the response to a create request.  The 0 value
        /// is not included to allow for compatibility with SNMP index values
        /// which also cannot be 0.
        /// Type: integer(1:MAX)
        /// See: RFC 8011 Section 5.3.1
        /// </summary>
        /// <example>63</example>
        /// <code>job-id</code>
        [IppAttribute(IppAttributeNames.JobId, Tag = Tag.Integer)]
    public IppValue<int>? JobId { get; set; }

        /// <summary>
        /// This REQUIRED attribute contains the URI for the Job object.
        /// See: RFC 8011 Section 5.3.2
        /// </summary>
        [Obsolete("The 'job-uri' attribute is deprecated in favor of 'job-id'. See RFC 8011 Section 5.3.2.")]
        [IppAttribute(IppAttributeNames.JobUri, Tag = Tag.Uri)]
    public IppValue<Uri>? JobUri { get; set; }

        /// <summary>
        /// This REQUIRED attribute identifies the Printer object that created
        /// this Job object.  When a Printer object creates a Job object, it
        /// populates this attribute with the Printer object URI that was used in
        /// the create request.  This attribute permits a client to identify the
        /// Printer object that created this Job object when only the Job
        /// object's URI is available to the client.  The client queries the
        /// creating Printer object to determine which languages, charsets,
        /// operations, are supported for this Job.
        /// See: RFC 8011 Section 5.3.3
        /// </summary>
        /// <example>ipp://10.30.254.250:631/ipp/print</example>
        /// <code>job-printer-uri</code>
        [IppAttribute(IppAttributeNames.JobPrinterUri, Tag = Tag.Uri)]
    public IppValue<Uri>? JobPrinterUri { get; set; }

        /// <summary>
        /// List of resource IDs allocated to this job.
        /// See: PWG 5100.22-2025 Section 7.1.15
        /// </summary>
        /// <code>job-resource-ids</code>
        [Range(1, int.MaxValue)]
        [IppAttribute(IppAttributeNames.JobResourceIds, Tag = Tag.Integer)]
    public IppValue<int[]>? JobResourceIds { get; set; }

        /// <summary>
        /// This REQUIRED attribute is the name of the job.  It is a name that is
        /// more user friendly than the "job-uri" attribute value.  It does not
        /// need to be unique between Jobs.  The Job's "job-name" attribute is
        /// set to the value supplied by the client in the "job-name" operation
        /// attribute in the create request (see Section 3.2.1.1).   If, however,
        /// the "job-name" operation attribute is not supplied by the client in
        /// the create request, the Printer object, on creation of the Job, MUST
        /// generate a name.
        /// See: RFC 8011 Section 5.3.5
        /// </summary>
        /// <example>job63</example>
        /// <code>job-name</code>
        [IppAttribute(IppAttributeNames.JobName, Tag = Tag.NameWithoutLanguage)]
    public StringWithLanguage? JobName { get; set; }

        /// <summary>
        /// This REQUIRED attribute contains the name of the end user that
        /// submitted the print job.  The Printer object sets this attribute to
        /// the most authenticated printable name that it can obtain from the
        /// authentication service over which the IPP operation was received.
        /// Only if such is not available, does the Printer object use the value
        /// supplied by the client in the "requesting-user-name" operation
        /// attribute of the create operation (see Sections 4.4.2, 4.4.3, and 8).
        /// See: RFC 8011 Section 5.3.6
        /// </summary>
        /// <example>anonymous (en)</example>
        /// <code>job-originating-user-name</code>
        [IppAttribute(IppAttributeNames.JobOriginatingUserName, Tag = Tag.NameWithoutLanguage)]
    public StringWithLanguage? JobOriginatingUserName { get; set; }

        /// <summary>
        /// This attribute specifies the total number of octets processed in K
        /// octets, i.e., in units of 1024 octets so far.  The value MUST be
        /// rounded up, so that a job between 1 and 1024 octets inclusive MUST be
        /// indicated as being 1, 1025 to 2048 inclusive MUST be 2, etc.
        /// For implementations where multiple copies are produced by the
        /// interpreter with only a single pass over the data, the final value
        /// MUST be equal to the value of the "job-k-octets" attribute.  For
        /// implementations where multiple copies are produced by the interpreter
        /// by processing the data for each copy, the final value MUST be a
        /// multiple of the value of the "job-k-octets" attribute.
        /// See: RFC 8011 Section 5.3.18.1
        /// </summary>
        /// <example>26</example>
        /// <code>job-k-octets-processed</code>
        [IppAttribute(IppAttributeNames.JobKOctetsProcessed, Tag = Tag.Integer)]
    public IppValue<int>? JobKOctetsProcessed { get; set; }

        /// <summary>
        /// This attribute specifies the total size in number of impressions of
        /// the document(s) being submitted.
        /// As with "job-k-octets", this value MUST NOT include the
        /// multiplicative factors contributed by the number of copies specified
        /// by the "copies" attribute, independent of whether the device can
        /// process multiple copies without making multiple passes over the job
        /// or document data and independent of whether the output is collated or
        /// not.  Thus the value is independent of the implementation and
        /// reflects the size of the document(s) measured in impressions
        /// independent of the number of copies.
        /// As with "job-k-octets", this value MUST also not include the
        /// multiplicative factor due to a copies instruction embedded in the
        /// document data.  If the document data actually includes replications
        /// of the document data, this value will include such replication.  In
        /// other words, this value is always the number of impressions in the
        /// source document data, rather than a measure of the number of
        /// impressions to be produced by the job.
        /// See: RFC 8011 Section 5.3.17.2
        /// </summary>
        /// <example>no value</example>
        /// <code>job-impressions</code>
        [IppAttribute(IppAttributeNames.JobImpressions, Tag = Tag.Integer)]
    public IppValue<int>? JobImpressions { get; set; }

        /// <summary>
        /// This attribute specifies detailed impression counters for the Job.
        /// See: PWG 5100.7-2023 Section 6.6.1
        /// </summary>
        /// <code>job-impressions-col</code>
        [IppAttribute(IppAttributeNames.JobImpressionsCol)]
    public IppValue<JobCounter>? JobImpressionsCol { get; set; }

        /// <summary>
        /// This job attribute specifies the number of impressions completed for
        /// the job so far.  For printing devices, the impressions completed
        /// includes interpreting, marking, and stacking the output.
        /// See: RFC 8011 Section 5.3.18.2
        /// </summary>
        /// <example>0</example>
        /// <code>job-impressions-completed</code>
        [IppAttribute(IppAttributeNames.JobImpressionsCompleted, Tag = Tag.Integer)]
    public IppValue<int>? JobImpressionsCompleted { get; set; }

        /// <summary>
        /// This attribute specifies the total number of media sheets to be
        /// produced for this job.
        /// Unlike the "job-k-octets" and the "job-impressions" attributes, this
        /// value MUST include the multiplicative factors contributed by the
        /// number of copies specified by the "copies" attribute and a 'number of
        /// copies' instruction embedded in the document data, if any.  This
        /// difference allows the system administrator to control the lower and
        /// upper bounds of both (1) the size of the document(s) with "job-k-
        /// octets-supported" and "job-impressions-supported" and (2) the size of
        /// the job with "job-media-sheets-supported".
        /// See: RFC 8011 Section 5.3.17.3
        /// </summary>
        /// <example>no value</example>
        /// <code>job-media-sheets</code>
        [IppAttribute(IppAttributeNames.JobMediaSheets, Tag = Tag.Integer)]
    public IppValue<int>? JobMediaSheets { get; set; }

        /// <summary>
        /// This attribute specifies detailed media sheet counters for the Job.
        /// See: PWG 5100.7-2023 Section 6.6.2
        /// </summary>
        /// <code>job-media-sheets-col</code>
        [IppAttribute(IppAttributeNames.JobMediaSheetsCol)]
    public IppValue<JobCounter>? JobMediaSheetsCol { get; set; }

        /// <summary>
        /// This attribute contains a URI used to obtain additional
        /// information about the Job object.
        /// See: RFC 8011 Section 5.3.4
        /// </summary>
        [IppAttribute(IppAttributeNames.JobMoreInfo, Tag = Tag.Uri)]
    public IppValue<Uri>? JobMoreInfo { get; set; }

        /// <summary>
        /// The <c>job-charge-info</c> Job Description attribute.
        /// See: PWG 5100.11-2024 Section 5.4.2
        /// </summary>
        [IppAttribute(IppAttributeNames.JobChargeInfo, Tag = Tag.TextWithoutLanguage)]
    public StringWithLanguage? JobChargeInfo { get; set; }

        /// <summary>
        /// This attribute specifies details about the source of the Document data.
        /// DEPRECATED.
        /// See: PWG 5100.7-2023 Section 6.2.1
        /// </summary>
        /// <code>document-format-details</code>
        [Obsolete("The 'document-format-details' attribute is deprecated. See PWG 5100.7-2023 Section 6.2.1.")]
        [IppAttribute(IppAttributeNames.DocumentFormatDetails)]
    public IppValue<DocumentFormatDetails>? DocumentFormatDetails { get; set; }

        /// <summary>
        /// This attribute specifies details about the source of the Document data as detected by the Printer.
        /// DEPRECATED.
        /// See: PWG 5100.7-2023 Section 6.2.2
        /// </summary>
        /// <code>document-format-details-detected</code>
        [Obsolete("The 'document-format-details-detected' attribute is deprecated. See PWG 5100.7-2023 Section 6.2.2.")]
        [IppAttribute(IppAttributeNames.DocumentFormatDetailsDetected)]
    public IppValue<DocumentFormatDetails>? DocumentFormatDetailsDetected { get; set; }

        /// <summary>
        /// This attribute indicates the number of documents in the Job.
        /// See: RFC 8011 Section 5.3.12
        /// </summary>
        [IppAttribute(IppAttributeNames.NumberOfDocuments, Tag = Tag.Integer)]
    public IppValue<int>? NumberOfDocuments { get; set; }

        /// <summary>
        /// This attribute indicates the number of jobs that are
        /// "ahead" of this job in the relative chronological order of
        /// expected time to complete.
        /// See: RFC 8011 Section 5.3.15
        /// </summary>
        [IppAttribute(IppAttributeNames.NumberOfInterveningJobs, Tag = Tag.Integer)]
    public IppValue<int>? NumberOfInterveningJobs { get; set; }

        /// <summary>
        /// This attribute identifies the output device to which the
        /// Printer object has assigned this job.
        /// See: RFC 8011 Section 5.3.13
        /// </summary>
        [IppAttribute(IppAttributeNames.OutputDeviceAssigned, Tag = Tag.NameWithoutLanguage)]
    public StringWithLanguage? OutputDeviceAssigned { get; set; }

        /// <summary>
        /// This job attribute specifies the media-sheets completed marking and
        /// stacking for the entire job so far whether those sheets have been
        /// processed on one side or on both.
        /// See: RFC 8011 Section 5.3.18.3
        /// </summary>
        /// <example>0</example>
        /// <code>job-media-sheets-completed</code>
        [IppAttribute(IppAttributeNames.JobMediaSheetsCompleted, Tag = Tag.Integer)]
    public IppValue<int>? JobMediaSheetsCompleted { get; set; }

        /// <summary>
        /// This REQUIRED attribute identifies the current state of the job.
        /// Even though the IPP protocol defines seven values for job states
        /// (plus the out-of-band 'unknown' value - see Section 4.1),
        /// implementations only need to support those states which are
        /// appropriate for the particular implementation.  In other words, a
        /// Printer supports only those job states implemented by the output
        /// device and available to the Printer object implementation.
        /// See: RFC 8011 Section 5.3.7
        /// </summary>
        /// <example>9</example>
        /// <code>job-state</code>
        [IppAttribute(IppAttributeNames.JobState, Tag = Tag.Enum)]
    public IppValue<JobState>? JobState { get; set; }

        /// <summary>
        /// The Printer object OPTIONALLY returns the Job object's OPTIONAL
        /// "job-state-message" attribute.  If the Printer object supports
        /// this attribute then it MUST be returned in the response.  If
        /// this attribute is not returned in the response, the client can
        /// assume that the "job-state-message" attribute is not supported
        /// and will not be returned in a subsequent Job object query.
        /// See: RFC 8011 Section 5.3.9
        /// </summary>
        /// <example>The job completed successfully</example>
        /// <code>job-state-message</code>
        [IppAttribute(IppAttributeNames.JobStateMessage, Tag = Tag.TextWithoutLanguage)]
    public StringWithLanguage? JobStateMessage { get; set; }

        /// <summary>
        /// The Printer object MUST return the Job object's REQUIRED "job-
        /// state-reasons" attribute.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        /// <example>job-completed-successfully</example>
        /// <code>job-state-reasons</code>
        [IppAttribute(IppAttributeNames.JobStateReasons, Tag = Tag.Keyword)]
    public IppValue<JobStateReason[]>? JobStateReasons { get; set; }

        /// <summary>
        /// This attribute indicates the date and time at which the Job object
        /// was created.
        /// See: RFC 8011 Section 5.3.14.5
        /// </summary>
        /// <example>22.04.2021 20:13:21 +03:00</example>
        /// <code>date-time-at-creation</code>
        [IppAttribute(IppAttributeNames.DateTimeAtCreation, Tag = Tag.DateTime)]
    public IppValue<DateTimeOffset>? DateTimeAtCreation { get; set; }

        /// <summary>
        /// This attribute indicates the date and time at which the Job object
        /// first began processing after the create operation or the most recent
        /// Restart-Job operation.
        /// See: RFC 8011 Section 5.3.14.6
        /// </summary>
        /// <example>22.04.2021 20:13:22 +03:00</example>
        /// <code>date-time-at-processing</code>
        [IppAttribute(IppAttributeNames.DateTimeAtProcessing, Tag = Tag.DateTime)]
    public IppValue<DateTimeOffset>? DateTimeAtProcessing { get; set; }

        /// <summary>
        /// This attribute indicates the date and time at which the Job object
        /// completed (or was canceled or aborted).
        /// See: RFC 8011 Section 5.3.14.7
        /// </summary>
        /// <example>22.04.2021 20:13:22 +03:00</example>
        /// <code>date-time-at-completed</code>
        [IppAttribute(IppAttributeNames.DateTimeAtCompleted, Tag = Tag.DateTime)]
    public IppValue<DateTimeOffset>? DateTimeAtCompleted { get; set; }

        /// <summary>
        /// This REQUIRED attribute indicates the time at which the Job object
        /// was created.
        /// Type: integer(MIN:MAX)
        /// See: RFC 8011 Section 5.3.14.1
        /// </summary>
        /// <example>197753</example>
        /// <code>time-at-creation</code>
        [IppAttribute(IppAttributeNames.TimeAtCreation, Tag = Tag.Integer)]
    public IppValue<int>? TimeAtCreation { get; set; }

        /// <summary>
        /// This REQUIRED attribute indicates the time at which the Job object
        /// first began processing after the create operation or the most recent
        /// Restart-Job operation.  The out-of-band 'no-value' value is returned
        /// if the job has not yet been in the 'processing' state
        /// See: RFC 8011 Section 5.3.14.2
        /// </summary>
        /// <example>197754</example>
        /// <code>time-at-processing</code>
        [IppAttribute(IppAttributeNames.TimeAtProcessing, Tag = Tag.Integer)]
    public IppValue<int>? TimeAtProcessing { get; set; }

        /// <summary>
        /// This REQUIRED attribute indicates the time at which the Job object
        /// completed (or was canceled or aborted).  The out-of-band 'no-value'
        /// value is returned if the job has not yet completed, been canceled, or
        /// aborted
        /// See: RFC 8011 Section 5.3.14.3
        /// </summary>
        /// <example>197754</example>
        /// <code>time-at-completed</code>
        [IppAttribute(IppAttributeNames.TimeAtCompleted, Tag = Tag.Integer)]
    public IppValue<int>? TimeAtCompleted { get; set; }

        /// <summary>
        /// This REQUIRED Job Description attribute indicates the amount of time
        /// (in seconds) that the Printer implementation has been up and running.
        /// This attribute is an alias for the "printer-up-time" Printer
        /// Description attribute (see Section 4.4.29).
        /// See: RFC 8011 Section 5.3.14.4
        /// </summary>
        /// <example>197775</example>
        /// <code>job-printer-up-time</code>
        [IppAttribute(IppAttributeNames.JobPrinterUpTime, Tag = Tag.Integer)]
    public IppValue<int>? JobPrinterUpTime { get; set; }

        /// <summary>
        /// This attribute specifies the total size of the document(s) in K
        /// octets, i.e., in units of 1024 octets requested to be processed in
        /// the job. The value MUST be rounded up, so that a job between 1 and
        /// 1024 octets MUST be indicated as being 1, 1025 to 2048 MUST be 2,
        /// etc.
        /// See: RFC 8011 Section 5.3.17.1
        /// </summary>
        [IppAttribute(IppAttributeNames.JobKOctets, Tag = Tag.Integer)]
    public IppValue<int>? JobKOctets { get; set; }

        /// <summary>
        /// This attribute specifies additional detailed and technical
        /// information about the job. The Printer NEED NOT localize the
        /// message(s), since they are intended for use by the system
        /// administrator or other experienced technical persons.  Localization
        /// might obscure the technical meaning of such messages. Clients MUST
        /// NOT attempt to parse the value of this attribute.
        /// See: RFC 8011 Section 5.3.10
        /// </summary>
        [IppAttribute(IppAttributeNames.JobDetailedStatusMessages, Tag = Tag.NameWithoutLanguage)]
    public IppValue<string[]>? JobDetailedStatusMessages { get; set; }

        /// <summary>
        /// This attribute provides additional information about each document
        /// access error for this job encountered by the Printer after it
        /// returned a response to the Print-URI or Send-URI operation and
        /// subsequently attempted to access document(s) supplied in the Print-
        /// URI or Send-URI operation. For errors in the protocol that is
        /// identified by the URI scheme in the "document-uri" operation
        /// attribute, such as 'http:' or 'ftp:', the error code is returned in
        /// parentheses, followed by the URI.
        /// See: RFC 8011 Section 5.3.11
        /// </summary>
        [IppAttribute(IppAttributeNames.JobDocumentAccessErrors, Tag = Tag.NameWithoutLanguage)]
    public IppValue<string[]>? JobDocumentAccessErrors { get; set; }

        /// <summary>
        /// This attribute provides a message from an operator, system
        /// administrator or "intelligent" process to indicate to the end user
        /// the reasons for modification or other management action taken on a
        /// job.
        /// See: RFC 8011 Section 5.3.16
        /// </summary>
        [IppAttribute(IppAttributeNames.JobMessageFromOperator, Tag = Tag.NameWithoutLanguage)]
    public StringWithLanguage? JobMessageFromOperator { get; set; }

        /// <summary>
        /// This attribute specifies the total number of pages in the Job.
        /// See: PWG 5100.7-2023 Section 6.6.3
        /// </summary>
        [IppAttribute(IppAttributeNames.JobPages, Tag = Tag.Integer)]
    public IppValue<int>? JobPages { get; set; }

        /// <summary>
        /// This attribute specifies detailed page counters for the Job.
        /// See: PWG 5100.7-2023 Section 6.6.4
        /// </summary>
        /// <code>job-pages-col</code>
        [IppAttribute(IppAttributeNames.JobPagesCol)]
    public IppValue<JobCounter>? JobPagesCol { get; set; }

        /// <summary>
        /// This attribute specifies the number of pages completed for the
        /// Job so far.
        /// See: PWG 5100.7-2023 Section 6.7.4
        /// </summary>
        [IppAttribute(IppAttributeNames.JobPagesCompleted, Tag = Tag.Integer)]
    public IppValue<int>? JobPagesCompleted { get; set; }

        /// <summary>
        /// This attribute specifies detailed impression counters completed for the Job so far.
        /// See: PWG 5100.7-2023 Section 6.7.2
        /// </summary>
        /// <code>job-impressions-completed-col</code>
        [IppAttribute(IppAttributeNames.JobImpressionsCompletedCol)]
    public IppValue<JobCounter>? JobImpressionsCompletedCol { get; set; }

        /// <summary>
        /// This attribute specifies detailed media sheet counters completed for the Job so far.
        /// See: PWG 5100.7-2023 Section 6.7.3
        /// </summary>
        /// <code>job-media-sheets-completed-col</code>
        [IppAttribute(IppAttributeNames.JobMediaSheetsCompletedCol)]
    public IppValue<JobCounter>? JobMediaSheetsCompletedCol { get; set; }

        /// <summary>
        /// This attribute specifies detailed page counters completed for the Job so far.
        /// See: PWG 5100.7-2023 Section 6.7.5
        /// </summary>
        /// <code>job-pages-completed-col</code>
        [IppAttribute(IppAttributeNames.JobPagesCompletedCol)]
    public IppValue<JobCounter>? JobPagesCompletedCol { get; set; }

        /// <summary>
        /// This attribute lists the name and version information for the Client that created the Job.
        /// See: PWG 5100.7-2023 Section 6.7.1
        /// </summary>
        /// <code>client-info</code>
        [IppAttribute(IppAttributeNames.ClientInfo)]
    public IppValue<ClientInfo[]>? ClientInfo { get; set; }

        /// <summary>
        /// This attribute augments the "job-sheets" Job Template attribute and allows specifying distinct media.
        /// See: PWG 5100.7-2023 Section 6.8.11
        /// </summary>
        /// <code>job-sheets-col</code>
        [IppAttribute(IppAttributeNames.JobSheetsCol)]
    public IppValue<JobSheetsCol>? JobSheetsCol { get; set; }

        /// <summary>
        /// This attribute specifies the total number of seconds that the
        /// Job has been processing.
        /// See: PWG 5100.7-2023 Section 6.7.6
        /// </summary>
        [IppAttribute(IppAttributeNames.JobProcessingTime, Tag = Tag.Integer)]
    public IppValue<int>? JobProcessingTime { get; set; }

        /// <summary>
        /// This attribute specifies the number of errors that were detected
        /// while processing the Job.
        /// See: PWG 5100.7-2023 Section 6.2.1
        /// </summary>
        [IppAttribute(IppAttributeNames.ErrorsCount, Tag = Tag.Integer)]
    public IppValue<int>? ErrorsCount { get; set; }

        /// <summary>
        /// This attribute specifies the number of warnings that were detected
        /// while processing the Job.
        /// See: PWG 5100.7-2023 Section 6.2.3
        /// </summary>
        [IppAttribute(IppAttributeNames.WarningsCount, Tag = Tag.Integer)]
    public IppValue<int>? WarningsCount { get; set; }

        /// <summary>
        /// This attribute specifies the actual content optimization
        /// value(s) used by the Printer.
        /// See: PWG 5100.7-2023 Section 6.2.2
        /// </summary>
        [IppAttribute(IppAttributeNames.PrintContentOptimizeActual, Tag = Tag.Keyword)]
    public IppValue<PrintContentOptimize[]>? PrintContentOptimizeActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual number of copies
        /// that were produced for the Job.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [Range(1, int.MaxValue)]
        [IppAttribute(IppAttributeNames.CopiesActual, Tag = Tag.Integer)]
    public IppValue<int[]>? CopiesActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual finishing operations
        /// that were applied to the Job.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [IppAttribute(IppAttributeNames.FinishingsActual, Tag = Tag.Enum)]
    public IppValue<Finishings[]>? FinishingsActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual cover-back
        /// collection(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [IppAttribute(IppAttributeNames.CoverBackActual)]
    public IppValue<Cover[]>? CoverBackActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual cover-front
        /// collection(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [IppAttribute(IppAttributeNames.CoverFrontActual)]
    public IppValue<Cover[]>? CoverFrontActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual job-hold-until
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [IppAttribute(IppAttributeNames.JobHoldUntilActual, Tag = Tag.Keyword)]
    public IppValue<JobHoldUntil[]>? JobHoldUntilActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual job priority
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [Range(1, 100)]
        [IppAttribute(IppAttributeNames.JobPriorityActual, Tag = Tag.Integer)]
    public IppValue<int[]>? JobPriorityActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual job sheets
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [IppAttribute(IppAttributeNames.JobSheetsActual, Tag = Tag.Keyword)]
    public IppValue<JobSheets[]>? JobSheetsActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual media
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [IppAttribute(IppAttributeNames.MediaActual)]
    public IppValue<Media[]>? MediaActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual imposition-template
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [IppAttribute(IppAttributeNames.ImpositionTemplateActual)]
    public IppValue<ImpositionTemplate[]>? ImpositionTemplateActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual insert-sheet
        /// collection(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [IppAttribute(IppAttributeNames.InsertSheetActual)]
    public IppValue<InsertSheet[]>? InsertSheetActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual job-account-id
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [IppAttribute(IppAttributeNames.JobAccountIdActual, Tag = Tag.NameWithoutLanguage)]
    public IppValue<string[]>? JobAccountIdActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual job-accounting-sheets
        /// collection(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [IppAttribute(IppAttributeNames.JobAccountingSheetsActual)]
    public IppValue<JobAccountingSheets[]>? JobAccountingSheetsActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual job-accounting-user-id
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [IppAttribute(IppAttributeNames.JobAccountingUserIdActual, Tag = Tag.NameWithoutLanguage)]
    public IppValue<string[]>? JobAccountingUserIdActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual job-error-sheet
        /// collection(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [IppAttribute(IppAttributeNames.JobErrorSheetActual)]
    public IppValue<JobErrorSheet[]>? JobErrorSheetActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual job-message-to-operator
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [IppAttribute(IppAttributeNames.JobMessageToOperatorActual, Tag = Tag.TextWithoutLanguage)]
    public IppValue<string[]>? JobMessageToOperatorActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual job-sheet-message
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [IppAttribute(IppAttributeNames.JobSheetMessageActual, Tag = Tag.TextWithoutLanguage)]
    public IppValue<string[]>? JobSheetMessageActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual media-col
        /// collection(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [IppAttribute(IppAttributeNames.MediaColActual)]
    public IppValue<MediaCol[]>? MediaColActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual multiple-document-handling
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [IppAttribute(IppAttributeNames.MultipleDocumentHandlingActual, Tag = Tag.Keyword)]
    public IppValue<MultipleDocumentHandling[]>? MultipleDocumentHandlingActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual number-up
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [Range(1, int.MaxValue)]
        [IppAttribute(IppAttributeNames.NumberUpActual, Tag = Tag.Integer)]
    public IppValue<int[]>? NumberUpActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual orientation
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [IppAttribute(IppAttributeNames.OrientationRequestedActual, Tag = Tag.Enum)]
    public IppValue<Orientation[]>? OrientationRequestedActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual output-bin
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [IppAttribute(IppAttributeNames.OutputBinActual)]
    public IppValue<OutputBin[]>? OutputBinActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual media-input-tray-check
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [IppAttribute(IppAttributeNames.MediaInputTrayCheckActual, Tag = Tag.Keyword)]
    public IppValue<MediaInputTrayCheck[]>? MediaInputTrayCheckActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual page-delivery
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [IppAttribute(IppAttributeNames.PageDeliveryActual, Tag = Tag.Keyword)]
    public IppValue<PageDelivery[]>? PageDeliveryActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual page-order-received
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [IppAttribute(IppAttributeNames.PageOrderReceivedActual, Tag = Tag.Keyword)]
    public IppValue<PageOrderReceived[]>? PageOrderReceivedActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual page-ranges
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [IppAttribute(IppAttributeNames.PageRangesActual, Tag = Tag.RangeOfInteger)]
    public IppValue<Range[]>? PageRangesActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual print quality
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [IppAttribute(IppAttributeNames.PrintQualityActual, Tag = Tag.Enum)]
    public IppValue<PrintQuality[]>? PrintQualityActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual presentation-direction-number-up
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [IppAttribute(IppAttributeNames.PresentationDirectionNumberUpActual, Tag = Tag.Keyword)]
    public IppValue<PresentationDirectionNumberUp[]>? PresentationDirectionNumberUpActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual printer resolution
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [IppAttribute(IppAttributeNames.PrinterResolutionActual, Tag = Tag.Resolution)]
    public IppValue<Resolution[]>? PrinterResolutionActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual sides
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [IppAttribute(IppAttributeNames.SidesActual, Tag = Tag.Keyword)]
    public IppValue<Sides[]>? SidesActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual separator-sheets
        /// collection(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [IppAttribute(IppAttributeNames.SeparatorSheetsActual)]
    public IppValue<SeparatorSheets[]>? SeparatorSheetsActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual x-image-position
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [IppAttribute(IppAttributeNames.XImagePositionActual, Tag = Tag.Keyword)]
    public IppValue<XImagePosition[]>? XImagePositionActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual x-image-shift
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [Range(int.MinValue, int.MaxValue)]
        [IppAttribute(IppAttributeNames.XImageShiftActual, Tag = Tag.Integer)]
    public IppValue<int[]>? XImageShiftActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual x-side1-image-shift
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [Range(int.MinValue, int.MaxValue)]
        [IppAttribute(IppAttributeNames.XSide1ImageShiftActual, Tag = Tag.Integer)]
    public IppValue<int[]>? XSide1ImageShiftActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual x-side2-image-shift
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [Range(int.MinValue, int.MaxValue)]
        [IppAttribute(IppAttributeNames.XSide2ImageShiftActual, Tag = Tag.Integer)]
    public IppValue<int[]>? XSide2ImageShiftActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual y-image-position
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [IppAttribute(IppAttributeNames.YImagePositionActual, Tag = Tag.Keyword)]
    public IppValue<YImagePosition[]>? YImagePositionActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual y-image-shift
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [Range(int.MinValue, int.MaxValue)]
        [IppAttribute(IppAttributeNames.YImageShiftActual, Tag = Tag.Integer)]
    public IppValue<int[]>? YImageShiftActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual y-side1-image-shift
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [Range(int.MinValue, int.MaxValue)]
        [IppAttribute(IppAttributeNames.YSide1ImageShiftActual, Tag = Tag.Integer)]
    public IppValue<int[]>? YSide1ImageShiftActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual y-side2-image-shift
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        [Range(int.MinValue, int.MaxValue)]
        [IppAttribute(IppAttributeNames.YSide2ImageShiftActual, Tag = Tag.Integer)]
    public IppValue<int[]>? YSide2ImageShiftActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual page overrides
        /// used by the Printer.
        /// See: PWG 5100.6-2003 Section 5.1
        /// </summary>
        [IppAttribute(IppAttributeNames.OverridesActual)]
    public IppValue<OverrideInstruction[]>? OverridesActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual finishings-col
        /// collection(s) used by the Printer.
        /// See: PWG 5100.1-2022 Section 11.2
        /// </summary>
        [IppAttribute(IppAttributeNames.FinishingsColActual)]
    public IppValue<FinishingsCol[]>? FinishingsColActual { get; set; }

        /// <summary>
        /// The estimated date and time at which the Job will be completed. See: PWG 5100.3-2023 Section 5.1.1
        /// </summary>
        [IppAttribute(IppAttributeNames.DateTimeAtCompletedEstimated, Tag = Tag.DateTime)]
    public IppValue<DateTimeOffset>? DateTimeAtCompletedEstimated { get; set; }

        /// <summary>
        /// The estimated date and time at which the Job will enter the 'processing' state. See: PWG 5100.3-2023 Section 5.1.2
        /// </summary>
        [IppAttribute(IppAttributeNames.DateTimeAtProcessingEstimated, Tag = Tag.DateTime)]
    public IppValue<DateTimeOffset>? DateTimeAtProcessingEstimated { get; set; }

        /// <summary>
        /// The estimated time (in printer up-time seconds) at which the Job will be completed. See: PWG 5100.3-2023 Section 5.1.3
        /// </summary>
        [IppAttribute(IppAttributeNames.TimeAtCompletedEstimated, Tag = Tag.Integer)]
    public IppValue<int>? TimeAtCompletedEstimated { get; set; }

        /// <summary>
        /// The estimated time (in printer up-time seconds) at which the Job will enter the 'processing' state. See: PWG 5100.3-2023 Section 5.1.4
        /// </summary>
        [IppAttribute(IppAttributeNames.TimeAtProcessingEstimated, Tag = Tag.Integer)]
    public IppValue<int>? TimeAtProcessingEstimated { get; set; }

        /// <summary>
        /// The document-format-ready attribute reports the fetchable document MIME types.
        /// See: PWG 5100.18-2025 Section 7.3.1
        /// </summary>
        /// <code>document-format-ready</code>
        [IppAttribute(IppAttributeNames.DocumentFormatReady, Tag = Tag.MimeMediaType)]
    public IppValue<string[]>? DocumentFormatReady { get; set; }

        /// <summary>
        /// The output-device-job-state attribute reports the job state on the output device.
        /// See: PWG 5100.18-2025
        /// </summary>
        /// <code>output-device-job-state</code>
        [IppAttribute(IppAttributeNames.OutputDeviceJobState, Tag = Tag.Enum)]
    public IppValue<JobState>? OutputDeviceJobState { get; set; }

        /// <summary>
        /// The output-device-job-state-message attribute reports the job state message on the output device.
        /// See: PWG 5100.18-2025 Section 7.3.3
        /// </summary>
        /// <code>output-device-job-state-message</code>
        [IppAttribute(IppAttributeNames.OutputDeviceJobStateMessage, Tag = Tag.TextWithoutLanguage)]
    public StringWithLanguage? OutputDeviceJobStateMessage { get; set; }

        /// <summary>
        /// The output-device-job-state-reasons attribute reports job state reasons on the output device.
        /// See: PWG 5100.18-2025 Section 7.3.4
        /// </summary>
        /// <code>output-device-job-state-reasons</code>
        [IppAttribute(IppAttributeNames.OutputDeviceJobStateReasons, Tag = Tag.Keyword)]
    public IppValue<JobStateReason[]>? OutputDeviceJobStateReasons { get; set; }

        /// <summary>
        /// The output-device-uuid-assigned attribute reports the assigned output device UUID.
        /// See: PWG 5100.18-2025 Section 7.3.5
        /// </summary>
        /// <code>output-device-uuid-assigned</code>
        [IppAttribute(IppAttributeNames.OutputDeviceUuidAssigned, Tag = Tag.Uri)]
    public IppValue<Uri>? OutputDeviceUuidAssigned { get; set; }

        /// <summary>
        /// The actual materials used to process the 3D job.
        /// See: PWG 5100.21-2019 Section 8.2.3
        /// </summary>
        /// <code>materials-col-actual</code>
        [IppAttribute(IppAttributeNames.MaterialsColActual)]
    public IppValue<Material[]>? MaterialsColActual { get; set; }

        /// <summary>
        /// The actual chamber humidity values used to process the 3D job.
        /// Type: integer(0:100)
        /// See: PWG 5100.21-2019 Section 8.2.1
        /// </summary>
        /// <code>chamber-humidity-actual</code>
        [Range(0, 100)]
        [IppAttribute(IppAttributeNames.ChamberHumidityActual, Tag = Tag.Integer)]
    public IppValue<int[]>? ChamberHumidityActual { get; set; }

        /// <summary>
        /// The actual chamber temperature values used to process the 3D job.
        /// Type: integer(-273:MAX)
        /// See: PWG 5100.21-2019 Section 8.2.2
        /// </summary>
        /// <code>chamber-temperature-actual</code>
        [Range(-273, int.MaxValue)]
        [IppAttribute(IppAttributeNames.ChamberTemperatureActual, Tag = Tag.Integer)]
    public IppValue<int[]>? ChamberTemperatureActual { get; set; }

        /// <summary>
        /// The actual multiple object handling value used to process the 3D job.
        /// See: PWG 5100.21-2019 Section 8.2.4
        /// </summary>
        /// <code>multiple-object-handling-actual</code>
        [IppAttribute(IppAttributeNames.MultipleObjectHandlingActual3d, Tag = Tag.Keyword)]
    public IppValue<MultipleObjectHandling>? MultipleObjectHandlingActual3d { get; set; }

        /// <summary>
        /// The actual print accuracy used to process the 3D job.
        /// See: PWG 5100.21-2019 Section 8.2.5
        /// </summary>
        /// <code>print-accuracy-actual</code>
        [IppAttribute(IppAttributeNames.PrintAccuracyActual3d)]
    public IppValue<PrintAccuracy>? PrintAccuracyActual3d { get; set; }

        /// <summary>
        /// The actual platform temperature values used to process the 3D job.
        /// Type: integer(-273:MAX)
        /// See: PWG 5100.21-2019 Section 8.2.6
        /// </summary>
        /// <code>platform-temperature-actual</code>
        [Range(-273, int.MaxValue)]
        [IppAttribute(IppAttributeNames.PlatformTemperatureActual, Tag = Tag.Integer)]
    public IppValue<int[]>? PlatformTemperatureActual { get; set; }

        /// <summary>
        /// The actual print base values used to process the 3D job.
        /// See: PWG 5100.21-2019 Section 8.2.8
        /// </summary>
        /// <code>print-base-actual</code>
        [IppAttribute(IppAttributeNames.PrintBaseActual3d, Tag = Tag.Keyword)]
    public IppValue<PrintBase[]>? PrintBaseActual3d { get; set; }

        /// <summary>
        /// The actual printed object collections used to process the 3D job.
        /// See: PWG 5100.21-2019 Section 8.2.9
        /// </summary>
        /// <code>print-objects-actual</code>
        [IppAttribute(IppAttributeNames.PrintObjectsActual3d)]
    public IppValue<PrintObject[]>? PrintObjectsActual3d { get; set; }

        /// <summary>
        /// The actual print supports values used to process the 3D job.
        /// See: PWG 5100.21-2019 Section 8.2.10
        /// </summary>
        /// <code>print-supports-actual</code>
        [IppAttribute(IppAttributeNames.PrintSupportsActual3d, Tag = Tag.Keyword)]
    public IppValue<PrintSupports[]>? PrintSupportsActual3d { get; set; }

        /// <summary>
        /// This attribute specifies the status of destinations.
        /// See: PWG 5100.15-2014
        /// </summary>
        /// <code>destination-statuses</code>
        [IppAttribute(IppAttributeNames.DestinationStatuses)]
    public IppValue<DestinationStatus[]>? DestinationStatuses { get; set; }

        /// <summary>
        /// This attribute specifies the actual number of job copies produced for the Job,
        /// as reported by the Printer after processing. This is the job-level counterpart
        /// to the document-level "copies-actual" attribute.
        /// See: PWG 5100.7-2023 Section 10.3
        /// </summary>
        /// <code>job-copies-actual</code>
        [Range(1, int.MaxValue)]
        [IppAttribute(IppAttributeNames.JobCopiesActual, Tag = Tag.Integer)]
    public IppValue<int[]>? JobCopiesActual { get; set; }

        /// <summary>
        /// This attribute specifies the total number of octets (in K octets, i.e., units
        /// of 1024 octets) that have been sent to the output device for this job.
        /// See: PWG 5100.18-2025 Section 5.9.1
        /// </summary>
        /// <code>job-k-octets-completed</code>
        [IppAttribute(IppAttributeNames.JobKOctetsCompleted, Tag = Tag.Integer)]
    public IppValue<int>? JobKOctetsCompleted { get; set; }

        /// <summary>
        /// This attribute specifies the password for the job, encoded as an octet string.
        /// The encoding algorithm is specified by the "job-password-encryption" attribute.
        /// See: PWG 5100.11-2024 Section 5.3.7
        /// </summary>
        /// <code>job-password</code>
        [IppAttribute(IppAttributeNames.JobPassword, Tag = Tag.OctetStringWithAnUnspecifiedFormat)]
    public OctetString? JobPassword { get; set; }

        /// <summary>
        /// This attribute specifies the encryption algorithm used to encode the
        /// "job-password" attribute value.
        /// See: PWG 5100.11-2024 Section 5.3.7
        /// </summary>
        /// <code>job-password-encryption</code>
        [IppAttribute(IppAttributeNames.JobPasswordEncryption, Tag = Tag.Keyword)]
    public IppValue<JobPasswordEncryption>? JobPasswordEncryption { get; set; }

        /// <summary>
        /// This attribute specifies the list of Job Template attributes that the client
        /// requires the Printer to honor. If the Printer cannot honor all of the listed
        /// attributes, it MUST reject the job.
        /// See: PWG 5100.7-2023 Section 6.1
        /// </summary>
        /// <code>job-mandatory-attributes</code>
        [IppAttribute(IppAttributeNames.JobMandatoryAttributes, Tag = Tag.Keyword)]
    public IppValue<string[]>? JobMandatoryAttributes { get; set; }

        /// <summary>
        /// This attribute specifies the list of job IDs associated with this job
        /// (e.g., for multi-document jobs or job fan-out scenarios).
        /// See: PWG 5100.7-2023 Section 6.1
        /// </summary>
        /// <code>job-ids</code>
        [Range(1, int.MaxValue)]
        [IppAttribute(IppAttributeNames.JobIds, Tag = Tag.Integer)]
    public IppValue<int[]>? JobIds { get; set; }

        /// <summary>
        /// This attribute specifies the URI of the user that submitted the job.
        /// It is the URI counterpart to the "job-originating-user-name" attribute.
        /// See: PWG 5100.7-2023 Section 5.1.1
        /// </summary>
        /// <code>requesting-user-uri</code>
        [IppAttribute(IppAttributeNames.RequestingUserUri, Tag = Tag.Uri)]
    public IppValue<Uri>? RequestingUserUri { get; set; }

        /// <summary>
        /// This attribute specifies a URI that identifies the charge account or
        /// billing entity associated with this job.
        /// See: PWG 5100.7-2023 Section 6.8.13
        /// </summary>
        /// <code>job-charge-info-uri</code>
        [IppAttribute(IppAttributeNames.JobChargeInfoUri, Tag = Tag.Uri)]
    public IppValue<Uri>? JobChargeInfoUri { get; set; }

        /// <summary>
        /// The job-pages-completed-current-copy attribute.
        /// See: PWG 5100.13-2023 (obsolete)
        /// </summary>
        [Obsolete("The 'job-pages-completed-current-copy' attribute is obsolete. See PWG 5100.13-2023 Section 7.1.")]
        [IppAttribute(IppAttributeNames.JobPagesCompletedCurrentCopy, Tag = Tag.Integer)]
    public IppValue<int>? JobPagesCompletedCurrentCopy { get; set; }

        /// <summary>
        /// The pages-completed-current-copy attribute.
        /// See: PWG 5100.13-2023 (obsolete)
        /// </summary>
        [Obsolete("The 'pages-completed-current-copy' attribute is obsolete. See PWG 5100.13-2023 Section 7.1.")]
        [IppAttribute(IppAttributeNames.PagesCompletedCurrentCopy, Tag = Tag.Integer)]
    public IppValue<int>? PagesCompletedCurrentCopy { get; set; }

        /// <summary>
        /// The chamber-humidity-current 3D status attribute.
        /// See: PWG 5100.21-2019 Section 8.4.1
        /// </summary>
        /// <code>chamber-humidity-current</code>
        [IppAttribute(IppAttributeNames.ChamberHumidityCurrent, Tag = Tag.Integer)]
    public IppValue<int>? ChamberHumidityCurrent { get; set; }

        /// <summary>
        /// The chamber-temperature-current 3D status attribute.
        /// See: PWG 5100.21-2019 Section 8.4.2
        /// </summary>
        /// <code>chamber-temperature-current</code>
        [IppAttribute(IppAttributeNames.ChamberTemperatureCurrent, Tag = Tag.Integer)]
    public IppValue<int>? ChamberTemperatureCurrent { get; set; }

        /// <summary>
        /// The pages-per-subset-actual Job Description attribute.
        /// See: PWG 5100.8-2003 Section 4.2
        /// </summary>
        [Obsolete("The 'pages-per-subset-actual' attribute is obsolete. See PWG 5100.13-2023 Section 7.1.")]
        [Range(1, int.MaxValue)]
        [IppAttribute(IppAttributeNames.PagesPerSubsetActual, Tag = Tag.Integer)]
    public IppValue<int[]>? PagesPerSubsetActual { get; set; }
    }
}
