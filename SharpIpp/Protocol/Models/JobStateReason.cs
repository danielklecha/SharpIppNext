using System;

namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// Specifies values for the <c>job-state-reasons</c> Job Status attribute.
    /// See: RFC 8011 Section 5.3.8
    /// Additional value from: RFC 2911 Section 4.3.8
    /// Additional value from: PWG 5100.3-2023 Section 6.1
    /// Additional values from: PWG 5100.7-2023 Section 8.2
    /// Additional values from: PWG 5100.13-2023 Section 9.1
    /// Additional values from: PWG 5100.18-2025 Sections 9.3, 4.1.6
    /// </summary>
    public readonly record struct JobStateReason(string Value, bool IsValue = true) : ISmartEnum
    {
        /// <summary>
        /// No job state reasons apply.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason None = new("none");

        /// <summary>
        /// The Job is being received by the Printer from the Client.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobIncoming = new("job-incoming");

        /// <summary>
        /// The Printer has created the Job but is waiting for the document data before it can move the Job into the 'processing' state.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobDataInsufficient = new("job-data-insufficient");

        /// <summary>
        /// The Printer could not access one or more documents passed by reference (e.g., via a URI).
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason DocumentAccessError = new("document-access-error");

        /// <summary>
        /// The Job was not completely submitted for some unforeseen reason.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason SubmissionInterrupted = new("submission-interrupted");

        /// <summary>
        /// The Printer is transmitting the Job to the output device.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobOutgoing = new("job-outgoing");

        /// <summary>
        /// The value of the Job's "job-hold-until" attribute was specified with a time period that is still in the future.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobHoldUntilSpecified = new("job-hold-until-specified");

        /// <summary>
        /// RFC 2911 Âş4.3.8 original value, renamed to <see cref="ResourcesAreNotSupported"/> in RFC 8011 Âş5.3.8.
        /// See: RFC 2911 Section 4.3.8
        /// </summary>
        public static readonly JobStateReason ResourcesAreNotReady = new("resources-are-not-ready");

        /// <summary>
        /// At least one of the resources needed by the Job, such as media, fonts, resource objects, etc., is not supported on any of the physical printers for which the Job is a candidate.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason ResourcesAreNotSupported = new("resources-are-not-supported");

        /// <summary>
        /// The Printer has stopped partly.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason PrinterStoppedPartly = new("printer-stopped-partly");

        /// <summary>
        /// The Printer has stopped completely.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason PrinterStopped = new("printer-stopped");

        /// <summary>
        /// The Printer is interpreting the document data.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobInterpreting = new("job-interpreting");

        /// <summary>
        /// The Job has been placed in the print queue.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobQueued = new("job-queued");

        /// <summary>
        /// The Printer is transforming the document data to another format.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobTransforming = new("job-transforming");

        /// <summary>
        /// The Printer is transferring the Job to the output device.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobTransferring = new("job-transferring");

        /// <summary>
        /// The Printer is connecting to the destination URI.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason ConnectingToDestination = new("connecting-to-destination");

        /// <summary>
        /// The Printer has connected to the destination URI.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason ConnectedToDestination = new("connected-to-destination");

        /// <summary>
        /// The Printer could not connect to the destination URI.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason DestinationUriFailed = new("destination-uri-failed");

        /// <summary>
        /// The Printer is waiting for user action, such as loading media.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason WaitingForUserAction = new("waiting-for-user-action");

        /// <summary>
        /// The Job has been placed in the queue for the marker.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobQueuedForMarker = new("job-queued-for-marker");

        /// <summary>
        /// The Printer is currently printing the Job.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobPrinting = new("job-printing");

        /// <summary>
        /// The Job was canceled by the user who submitted it.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobCanceledByUser = new("job-canceled-by-user");

        /// <summary>
        /// The Job was canceled by an operator.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobCanceledByOperator = new("job-canceled-by-operator");

        /// <summary>
        /// The Job was canceled at the device.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobCanceledAtDevice = new("job-canceled-at-device");

        /// <summary>
        /// The Job was aborted by the system.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason AbortedBySystem = new("aborted-by-system");

        /// <summary>
        /// The Job was aborted because the document data used a compression algorithm that is not supported by the Printer.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason UnsupportedCompression = new("unsupported-compression");

        /// <summary>
        /// The Job was aborted because the Printer encountered an error in the document data while decompressing it.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason CompressionError = new("compression-error");

        /// <summary>
        /// The Job was aborted because the document data is in a format not supported by the Printer.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason UnsupportedDocumentFormat = new("unsupported-document-format");

        /// <summary>
        /// The Job was aborted because the Printer encountered an error in the document data.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason DocumentFormatError = new("document-format-error");

        /// <summary>
        /// The Printer is stopping the current Job at a convenient point, such as at the end of a page or column.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason ProcessingToStopPoint = new("processing-to-stop-point");

        /// <summary>
        /// The Printer is off-line and accepting no Jobs.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason ServiceOffLine = new("service-off-line");

        /// <summary>
        /// The Job completed successfully.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobCompletedSuccessfully = new("job-completed-successfully");

        /// <summary>
        /// The Job completed with warnings.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobCompletedWithWarnings = new("job-completed-with-warnings");

        /// <summary>
        /// The Job completed with errors.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobCompletedWithErrors = new("job-completed-with-errors");

        /// <summary>
        /// The Job is in the 'completed' state, but is restartable using the Restart-Job operation.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobRestartable = new("job-restartable");

        /// <summary>
        /// The Job has been forwarded to a device or print system that is unable to send back status.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason QueuedInDevice = new("queued-in-device");

        /// <summary>
        /// One or more documents in the Job contains a digital signature that could not be verified by the Printer.
        /// See: PWG 5100.7-2023 Section 8.2
        /// </summary>
        public static readonly JobStateReason DigitalSignatureDidNotVerify = new("digital-signature-did-not-verify");

        /// <summary>
        /// The Printer has detected one or more errors during processing of the Job.
        /// See: PWG 5100.7-2023 Section 8.2
        /// </summary>
        public static readonly JobStateReason ErrorsDetected = new("errors-detected");

        /// <summary>
        /// The Job's output is delayed because the "job-delay-output-until" or "job-delay-output-until-time" Job Template attribute was specified.
        /// See: RFC 8011 Section 5.3.8
        /// See: PWG 5100.7-2023 Section 6.8.4
        /// </summary>
        public static readonly JobStateReason JobDelayOutputUntilSpecified = new("job-delay-output-until-specified");

        /// <summary>
        /// The Job's output is delayed because the "job-delay-output-until" or "job-delay-output-until-time" Job Template attribute was specified.
        /// This keyword is defined in the PWG 5100.7-2023 Section 8.2 keyword table (the normative keyword string listed there).
        /// See: PWG 5100.7-2023 Section 8.2
        /// </summary>
        public static readonly JobStateReason JobDelayUntilSpecified = new("job-delay-until-specified");

        /// <summary>
        /// The Printer is spooling the Document data before processing it.
        /// See: PWG 5100.7-2023 Section 8.2
        /// </summary>
        public static readonly JobStateReason JobSpooling = new("job-spooling");

        /// <summary>
        /// The Printer is processing the Document data as it is being received (streaming, not spooling).
        /// See: PWG 5100.7-2023 Section 8.2
        /// </summary>
        public static readonly JobStateReason JobStreaming = new("job-streaming");

        /// <summary>
        /// The Printer has detected one or more warnings during processing of the Job.
        /// See: PWG 5100.7-2023 Section 8.2
        /// </summary>
        public static readonly JobStateReason WarningsDetected = new("warnings-detected");

        /// <summary>
        /// The Job contains conflicting attributes that cannot be resolved.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason ConflictingAttributes = new("conflicting-attributes");

        /// <summary>
        /// The Job was canceled because it was not fetched within the timeout period.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobCanceledAfterTimeout = new("job-canceled-after-timeout");

        /// <summary>
        /// The Job is being held for review by an operator or administrator.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobHeldForReview = new("job-held-for-review");

        /// <summary>
        /// The Job is being held because the user's account authorization failed.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason AccountAuthorizationFailed = new("account-authorization-failed");

        /// <summary>
        /// The Job is being held because the user's account has been closed.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason AccountClosed = new("account-closed");

        /// <summary>
        /// The Job is being held because account information is needed.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason AccountInfoNeeded = new("account-info-needed");

        /// <summary>
        /// The Job is being held because the user's account has reached its limit.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason AccountLimitReached = new("account-limit-reached");

        /// <summary>
        /// The Job is being held pending authorization by an operator or administrator.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobHeldForAuthorization = new("job-held-for-authorization");

        /// <summary>
        /// The Job is being held until the user presses a button on the device.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobHeldForButtonPress = new("job-held-for-button-press");

        /// <summary>
        /// The Job is being held until explicitly released.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobHeldForRelease = new("job-held-for-release");

        /// <summary>
        /// The Job is waiting for the user to enter a password.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobPasswordWait = new("job-password-wait");

        /// <summary>
        /// The Job printed successfully.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobPrintedSuccessfully = new("job-printed-successfully");

        /// <summary>
        /// The Job printed with errors.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobPrintedWithErrors = new("job-printed-with-errors");

        /// <summary>
        /// The Job printed with warnings.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobPrintedWithWarnings = new("job-printed-with-warnings");

        /// <summary>
        /// The Job is resuming from a suspended state.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobResuming = new("job-resuming");

        /// <summary>
        /// The Job is waiting to be released.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobReleaseWait = new("job-release-wait");

        /// <summary>
        /// The Job has been stored successfully.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobStored = new("job-stored");

        /// <summary>
        /// The Job was stored with errors.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobStoredWithErrors = new("job-stored-with-errors");

        /// <summary>
        /// The Job was stored with warnings.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobStoredWithWarnings = new("job-stored-with-warnings");

        /// <summary>
        /// The Job is currently being stored.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobStoring = new("job-storing");

        /// <summary>
        /// The Job is ready to be fetched by a Proxy using the Fetch-Job operation.
        /// See: PWG 5100.18-2025 Section 9.3
        /// </summary>
        public static readonly JobStateReason JobFetchable = new("job-fetchable");

        /// <summary>
        /// Obsolete legacy compatibility value.
        /// See: IPP Enterprise Printing Extensions v2.0 (PWG 5100.11-2024) Section 9.2.
        /// </summary>
        [Obsolete("See IPP Enterprise Printing Extensions v2.0 (PWG 5100.11-2024) Section 9.2.")]
        public static readonly JobStateReason JobSavedSuccessfully = new("job-saved-successfully");

        /// <summary>
        /// Obsolete legacy compatibility value.
        /// See: IPP Enterprise Printing Extensions v2.0 (PWG 5100.11-2024) Section 9.2.
        /// </summary>
        [Obsolete("See IPP Enterprise Printing Extensions v2.0 (PWG 5100.11-2024) Section 9.2.")]
        public static readonly JobStateReason JobSavedWithErrors = new("job-saved-with-errors");

        /// <summary>
        /// Obsolete legacy compatibility value.
        /// See: IPP Enterprise Printing Extensions v2.0 (PWG 5100.11-2024) Section 9.2.
        /// </summary>
        [Obsolete("See IPP Enterprise Printing Extensions v2.0 (PWG 5100.11-2024) Section 9.2.")]
        public static readonly JobStateReason JobSavedWithWarnings = new("job-saved-with-warnings");

        /// <summary>
        /// Obsolete legacy compatibility value.
        /// See: IPP Enterprise Printing Extensions v2.0 (PWG 5100.11-2024) Section 9.2.
        /// </summary>
        [Obsolete("See IPP Enterprise Printing Extensions v2.0 (PWG 5100.11-2024) Section 9.2.")]
        public static readonly JobStateReason JobSaving = new("job-saving");

        /// <summary>
        /// The Job has been suspended by an operator.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobSuspendedByOperator = new("job-suspended-by-operator");

        /// <summary>
        /// The Job has been suspended by the system.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobSuspendedBySystem = new("job-suspended-by-system");

        /// <summary>
        /// The Job has been suspended by the user.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobSuspendedByUser = new("job-suspended-by-user");

        /// <summary>
        /// The Job has been suspended pending approval.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobSuspendedForApproval = new("job-suspended-for-approval");

        /// <summary>
        /// The Job is in the process of being suspended.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason JobSuspending = new("job-suspending");

        /// <summary>
        /// The Job has been suspended by the Infrastructure Printer pending a Resume-Job request from the Proxy.
        /// See: PWG 5100.18-2025 Section 4.1.6
        /// </summary>
        public static readonly JobStateReason JobSuspended = new("job-suspended");

        /// <summary>
        /// The Job contains attributes or values that are not supported by the Printer.
        /// See: RFC 8011 Section 5.3.8
        /// </summary>
        public static readonly JobStateReason UnsupportedAttributesOrValues = new("unsupported-attributes-or-values");

        /// <summary>
        /// The Printer detected an incorrect Document content password and was unable to unlock the Document for printing.
        /// This value MUST be supported if the "document-password" operation attribute is supported.
        /// See: PWG 5100.13-2023 Section 9.1
        /// </summary>
        public static readonly JobStateReason DocumentPasswordError = new("document-password-error");

        /// <summary>
        /// The Printer was able to unlock the Document but the Document permissions do not allow for printing.
        /// This value MUST be supported if the "document-password" operation attribute is supported.
        /// See: PWG 5100.13-2023 Section 9.1
        /// </summary>
        public static readonly JobStateReason DocumentPermissionError = new("document-permission-error");

        /// <summary>
        /// The Printer detected security issues (virus, trojan horse, or other malicious software) embedded within the Document.
        /// See: PWG 5100.13-2023 Section 9.1
        /// </summary>
        public static readonly JobStateReason DocumentSecurityError = new("document-security-error");

        /// <summary>
        /// The Printer determined that the Document was unprintable due to issues of file size, format version, or complexity.
        /// See: PWG 5100.13-2023 Section 9.1
        /// </summary>
        public static readonly JobStateReason DocumentUnprintableError = new("document-unprintable-error");

        public override string ToString() => Value;
        public static implicit operator string(JobStateReason bin) => bin.Value;
        public static implicit operator JobStateReason(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
    }
}
