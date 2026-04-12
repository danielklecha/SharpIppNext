using System;

namespace SharpIpp.Protocol.Models
{
    public class JobDescriptionAttributes
    {
        /// <summary>
        /// This REQUIRED attribute contains the ID of the job.  The Printer, on
        /// receipt of a new job, generates an ID which identifies the new Job on
        /// that Printer.  The Printer returns the value of the "job-id"
        /// attribute as part of the response to a create request.  The 0 value
        /// is not included to allow for compatibility with SNMP index values
        /// which also cannot be 0.
        /// See: RFC 8011 Section 5.3.1
        /// </summary>
        /// <example>63</example>
        /// <code>job-id</code>
        public int? JobId { get; set; }

        /// <summary>
        /// This REQUIRED attribute contains the URI for the Job object.
        /// See: RFC 8011 Section 5.3.2
        /// </summary>
        public string? JobUri { get; set; }

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
        public string? JobPrinterUri { get; set; }

        /// <summary>
        /// List of resource IDs allocated to this job.
        /// See: PWG 5100.22-2025 Section 7.1.15
        /// </summary>
        /// <code>job-resource-ids</code>
        public int[]? JobResourceIds { get; set; }

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
        public string? JobName { get; set; }

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
        public string? JobOriginatingUserName { get; set; }

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
        public int? JobKOctetsProcessed { get; set; }

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
        public int? JobImpressions { get; set; }

        /// <summary>
        /// This attribute specifies detailed impression counters for the Job.
        /// See: PWG 5100.7-2023 Section 6.6.1
        /// </summary>
        /// <code>job-impressions-col</code>
        public JobCounter? JobImpressionsCol { get; set; }

        /// <summary>
        /// This job attribute specifies the number of impressions completed for
        /// the job so far.  For printing devices, the impressions completed
        /// includes interpreting, marking, and stacking the output.
        /// See: RFC 8011 Section 5.3.18.2
        /// </summary>
        /// <example>0</example>
        /// <code>job-impressions-completed</code>
        public int? JobImpressionsCompleted { get; set; }

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
        public int? JobMediaSheets { get; set; }

        /// <summary>
        /// This attribute specifies detailed media sheet counters for the Job.
        /// See: PWG 5100.7-2023 Section 6.6.2
        /// </summary>
        /// <code>job-media-sheets-col</code>
        public JobCounter? JobMediaSheetsCol { get; set; }

        /// <summary>
        /// This attribute contains a URI used to obtain additional
        /// information about the Job object.
        /// See: RFC 8011 Section 5.3.4
        /// </summary>
        public string? JobMoreInfo { get; set; }

        /// <summary>
        /// The <c>job-charge-info</c> Job Description attribute.
        /// See: PWG 5100.11-2024 Section 5.4.2
        /// </summary>
        public string? JobChargeInfo { get; set; }

        /// <summary>
        /// This attribute specifies details about the source of the Document data.
        /// DEPRECATED.
        /// See: PWG 5100.7-2023 Section 6.2.1
        /// </summary>
        /// <code>document-format-details</code>
        public DocumentFormatDetails? DocumentFormatDetails { get; set; }

        /// <summary>
        /// This attribute specifies details about the source of the Document data as detected by the Printer.
        /// DEPRECATED.
        /// See: PWG 5100.7-2023 Section 6.2.2
        /// </summary>
        /// <code>document-format-details-detected</code>
        public DocumentFormatDetails? DocumentFormatDetailsDetected { get; set; }

        /// <summary>
        /// This attribute indicates the number of documents in the Job.
        /// See: RFC 8011 Section 5.3.12
        /// </summary>
        public int? NumberOfDocuments { get; set; }

        /// <summary>
        /// This attribute indicates the number of jobs that are
        /// "ahead" of this job in the relative chronological order of
        /// expected time to complete.
        /// See: RFC 8011 Section 5.3.15
        /// </summary>
        public int? NumberOfInterveningJobs { get; set; }

        /// <summary>
        /// This attribute identifies the output device to which the
        /// Printer object has assigned this job.
        /// See: RFC 8011 Section 5.3.13
        /// </summary>
        public string? OutputDeviceAssigned { get; set; }

        /// <summary>
        /// This job attribute specifies the media-sheets completed marking and
        /// stacking for the entire job so far whether those sheets have been
        /// processed on one side or on both.
        /// See: RFC 8011 Section 5.3.18.3
        /// </summary>
        /// <example>0</example>
        /// <code>job-media-sheets-completed</code>
        public int? JobMediaSheetsCompleted { get; set; }

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
        public JobState? JobState { get; set; }

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
        public string? JobStateMessage { get; set; }

        /// <summary>
        /// The Printer object MUST return the Job object's REQUIRED "job-
        /// state-reasons" attribute.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        /// <example>job-completed-successfully</example>
        /// <code>job-state-reasons</code>
        public JobStateReason[]? JobStateReasons { get; set; }

        /// <summary>
        /// This attribute indicates the date and time at which the Job object
        /// was created.
        /// See: RFC 8011 Section 5.3.14.5
        /// </summary>
        /// <example>22.04.2021 20:13:21 +03:00</example>
        /// <code>date-time-at-creation</code>
        public DateTimeOffset? DateTimeAtCreation { get; set; }

        /// <summary>
        /// This attribute indicates the date and time at which the Job object
        /// first began processing after the create operation or the most recent
        /// Restart-Job operation.
        /// See: RFC 8011 Section 5.3.14.6
        /// </summary>
        /// <example>22.04.2021 20:13:22 +03:00</example>
        /// <code>date-time-at-processing</code>
        public DateTimeOffset? DateTimeAtProcessing { get; set; }

        /// <summary>
        /// This attribute indicates the date and time at which the Job object
        /// completed (or was canceled or aborted).
        /// See: RFC 8011 Section 5.3.14.7
        /// </summary>
        /// <example>22.04.2021 20:13:22 +03:00</example>
        /// <code>date-time-at-completed</code>
        public DateTimeOffset? DateTimeAtCompleted { get; set; }

        /// <summary>
        /// This REQUIRED attribute indicates the time at which the Job object
        /// was created.
        /// See: RFC 8011 Section 5.3.14.1
        /// </summary>
        /// <example>197753</example>
        /// <code>time-at-creation</code>
        public int? TimeAtCreation { get; set; }

        /// <summary>
        /// This REQUIRED attribute indicates the time at which the Job object
        /// first began processing after the create operation or the most recent
        /// Restart-Job operation.  The out-of-band 'no-value' value is returned
        /// if the job has not yet been in the 'processing' state
        /// See: RFC 8011 Section 5.3.14.2
        /// </summary>
        /// <example>197754</example>
        /// <code>time-at-processing</code>
        public int? TimeAtProcessing { get; set; }

        /// <summary>
        /// This REQUIRED attribute indicates the time at which the Job object
        /// completed (or was canceled or aborted).  The out-of-band 'no-value'
        /// value is returned if the job has not yet completed, been canceled, or
        /// aborted
        /// See: RFC 8011 Section 5.3.14.3
        /// </summary>
        /// <example>197754</example>
        /// <code>time-at-completed</code>
        public int? TimeAtCompleted { get; set; }

        /// <summary>
        /// This REQUIRED Job Description attribute indicates the amount of time
        /// (in seconds) that the Printer implementation has been up and running.
        /// This attribute is an alias for the "printer-up-time" Printer
        /// Description attribute (see Section 4.4.29).
        /// See: RFC 8011 Section 5.3.14.4
        /// </summary>
        /// <example>197775</example>
        /// <code>job-printer-up-time</code>
        public int? JobPrinterUpTime { get; set; }

        /// <summary>
        /// This attribute specifies the total size of the document(s) in K
        /// octets, i.e., in units of 1024 octets requested to be processed in
        /// the job. The value MUST be rounded up, so that a job between 1 and
        /// 1024 octets MUST be indicated as being 1, 1025 to 2048 MUST be 2,
        /// etc.
        /// See: RFC 8011 Section 5.3.17.1
        /// </summary>
        public int? JobKOctets { get; set; }

        /// <summary>
        /// This attribute specifies additional detailed and technical
        /// information about the job. The Printer NEED NOT localize the
        /// message(s), since they are intended for use by the system
        /// administrator or other experienced technical persons.  Localization
        /// might obscure the technical meaning of such messages. Clients MUST
        /// NOT attempt to parse the value of this attribute.
        /// See: RFC 8011 Section 5.3.10
        /// </summary>
        public string[]? JobDetailedStatusMessages { get; set; }

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
        public string[]? JobDocumentAccessErrors { get; set; }

        /// <summary>
        /// This attribute provides a message from an operator, system
        /// administrator or "intelligent" process to indicate to the end user
        /// the reasons for modification or other management action taken on a
        /// job.
        /// See: RFC 8011 Section 5.3.16
        /// </summary>
        public string? JobMessageFromOperator { get; set; }

        /// <summary>
        /// This attribute specifies the total number of pages in the Job.
        /// See: PWG 5100.7-2023 Section 6.6.3
        /// </summary>
        public int? JobPages { get; set; }

        /// <summary>
        /// This attribute specifies detailed page counters for the Job.
        /// See: PWG 5100.7-2023 Section 6.6.4
        /// </summary>
        /// <code>job-pages-col</code>
        public JobCounter? JobPagesCol { get; set; }

        /// <summary>
        /// This attribute specifies the number of pages completed for the
        /// Job so far.
        /// See: PWG 5100.7-2023 Section 6.7.4
        /// </summary>
        public int? JobPagesCompleted { get; set; }

        /// <summary>
        /// This attribute specifies detailed impression counters completed for the Job so far.
        /// See: PWG 5100.7-2023 Section 6.7.2
        /// </summary>
        /// <code>job-impressions-completed-col</code>
        public JobCounter? JobImpressionsCompletedCol { get; set; }

        /// <summary>
        /// This attribute specifies detailed media sheet counters completed for the Job so far.
        /// See: PWG 5100.7-2023 Section 6.7.3
        /// </summary>
        /// <code>job-media-sheets-completed-col</code>
        public JobCounter? JobMediaSheetsCompletedCol { get; set; }

        /// <summary>
        /// This attribute specifies detailed page counters completed for the Job so far.
        /// See: PWG 5100.7-2023 Section 6.7.5
        /// </summary>
        /// <code>job-pages-completed-col</code>
        public JobCounter? JobPagesCompletedCol { get; set; }

        /// <summary>
        /// This attribute lists the name and version information for the Client that created the Job.
        /// See: PWG 5100.7-2023 Section 6.7.1
        /// </summary>
        /// <code>client-info</code>
        public ClientInfo[]? ClientInfo { get; set; }

        /// <summary>
        /// This attribute augments the "job-sheets" Job Template attribute and allows specifying distinct media.
        /// See: PWG 5100.7-2023 Section 6.8.11
        /// </summary>
        /// <code>job-sheets-col</code>
        public JobSheetsCol? JobSheetsCol { get; set; }

        /// <summary>
        /// This attribute specifies the total number of seconds that the
        /// Job has been processing.
        /// See: PWG 5100.7-2023 Section 6.7.6
        /// </summary>
        public int? JobProcessingTime { get; set; }

        /// <summary>
        /// This attribute specifies the number of errors that were detected
        /// while processing the Job.
        /// See: PWG 5100.7-2023 Section 6.2.1
        /// </summary>
        public int? ErrorsCount { get; set; }

        /// <summary>
        /// This attribute specifies the number of warnings that were detected
        /// while processing the Job.
        /// See: PWG 5100.7-2023 Section 6.2.3
        /// </summary>
        public int? WarningsCount { get; set; }

        /// <summary>
        /// This attribute specifies the actual content optimization
        /// value(s) used by the Printer.
        /// See: PWG 5100.7-2023 Section 6.2.2
        /// </summary>
        public PrintContentOptimize[]? PrintContentOptimizeActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual number of copies
        /// that were produced for the Job.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public int[]? CopiesActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual finishing operations
        /// that were applied to the Job.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public Finishings[]? FinishingsActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual cover-back
        /// collection(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public Cover[]? CoverBackActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual cover-front
        /// collection(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public Cover[]? CoverFrontActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual job-hold-until
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public JobHoldUntil[]? JobHoldUntilActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual job priority
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public int[]? JobPriorityActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual job sheets
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public JobSheets[]? JobSheetsActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual media
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public Media[]? MediaActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual imposition-template
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public ImpositionTemplate[]? ImpositionTemplateActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual insert-sheet
        /// collection(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public InsertSheet[]? InsertSheetActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual job-account-id
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public string[]? JobAccountIdActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual job-accounting-sheets
        /// collection(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public JobAccountingSheets[]? JobAccountingSheetsActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual job-accounting-user-id
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public string[]? JobAccountingUserIdActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual job-error-sheet
        /// collection(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public JobErrorSheet[]? JobErrorSheetActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual job-message-to-operator
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public string[]? JobMessageToOperatorActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual job-sheet-message
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public string[]? JobSheetMessageActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual media-col
        /// collection(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public MediaCol[]? MediaColActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual multiple-document-handling
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public MultipleDocumentHandling[]? MultipleDocumentHandlingActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual number-up
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public int[]? NumberUpActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual orientation
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public Orientation[]? OrientationRequestedActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual output-bin
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public OutputBin[]? OutputBinActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual media-input-tray-check
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public MediaInputTrayCheck[]? MediaInputTrayCheckActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual page-delivery
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public PageDelivery[]? PageDeliveryActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual page-order-received
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public PageOrderReceived[]? PageOrderReceivedActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual page-ranges
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public Range[]? PageRangesActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual print quality
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public PrintQuality[]? PrintQualityActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual presentation-direction-number-up
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public PresentationDirectionNumberUp[]? PresentationDirectionNumberUpActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual printer resolution
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public Resolution[]? PrinterResolutionActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual sides
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public Sides[]? SidesActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual separator-sheets
        /// collection(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public SeparatorSheets[]? SeparatorSheetsActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual x-image-position
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public XImagePosition[]? XImagePositionActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual x-image-shift
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public int[]? XImageShiftActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual x-side1-image-shift
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public int[]? XSide1ImageShiftActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual x-side2-image-shift
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public int[]? XSide2ImageShiftActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual y-image-position
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public YImagePosition[]? YImagePositionActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual y-image-shift
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public int[]? YImageShiftActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual y-side1-image-shift
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public int[]? YSide1ImageShiftActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual y-side2-image-shift
        /// value(s) used by the Printer.
        /// See: PWG 5100.8-2003 Section 3
        /// </summary>
        public int[]? YSide2ImageShiftActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual page overrides
        /// used by the Printer.
        /// See: PWG 5100.6-2003 Section 5.1
        /// </summary>
        public OverrideInstruction[]? OverridesActual { get; set; }

        /// <summary>
        /// This attribute specifies the actual finishings-col
        /// collection(s) used by the Printer.
        /// See: PWG 5100.1-2022 Section 11.2
        /// </summary>
        public FinishingsCol[]? FinishingsColActual { get; set; }

        public DateTimeOffset? DateTimeAtCompletedEstimated { get; set; }
        public DateTimeOffset? DateTimeAtProcessingEstimated { get; set; }
        public int? TimeAtCompletedEstimated { get; set; }
        public int? TimeAtProcessingEstimated { get; set; }

        /// <summary>
        /// The output-device-job-state attribute reports the job state on the output device.
        /// See: PWG 5100.18-2025
        /// </summary>
        /// <code>output-device-job-state</code>
        public JobState? OutputDeviceJobState { get; set; }

        /// <summary>
        /// The actual materials used to process the 3D job.
        /// See: PWG 5100.21-2019 Section 7.1
        /// </summary>
        /// <code>materials-col-actual</code>
        public Material[]? MaterialsColActual { get; set; }
    }
}
