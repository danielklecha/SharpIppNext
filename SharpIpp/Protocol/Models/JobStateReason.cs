namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// Specifies values for the <c>job-state-reasons</c> Job Status attribute.
    /// See: RFC 8011 Section 5.3.8
    /// Additional value from: RFC 2911 Section 4.3.8
    /// Additional value from: PWG 5100.3-2023 Section 6.1
    /// </summary>
    public readonly record struct JobStateReason(string Value, bool IsValue = true) : ISmartEnum
    {
        public static readonly JobStateReason None = new("none");
        public static readonly JobStateReason JobIncoming = new("job-incoming");
        public static readonly JobStateReason JobDataInsufficient = new("job-data-insufficient");
        public static readonly JobStateReason DocumentAccessError = new("document-access-error");
        public static readonly JobStateReason SubmissionInterrupted = new("submission-interrupted");
        public static readonly JobStateReason JobOutgoing = new("job-outgoing");
        public static readonly JobStateReason JobHoldUntilSpecified = new("job-hold-until-specified");
        /// <summary>RFC 2911 º4.3.8 original value, renamed to <see cref="ResourcesAreNotSupported"/> in RFC 8011 º5.3.8.</summary>
        public static readonly JobStateReason ResourcesAreNotReady = new("resources-are-not-ready");
        /// <summary>RFC 8011 º5.3.8 rename of <see cref="ResourcesAreNotReady"/>.</summary>
        public static readonly JobStateReason ResourcesAreNotSupported = new("resources-are-not-supported");
        public static readonly JobStateReason PrinterStoppedPartly = new("printer-stopped-partly");
        public static readonly JobStateReason PrinterStopped = new("printer-stopped");
        public static readonly JobStateReason JobInterpreting = new("job-interpreting");
        public static readonly JobStateReason JobQueued = new("job-queued");
        public static readonly JobStateReason JobTransforming = new("job-transforming");
        public static readonly JobStateReason JobQueuedForMarker = new("job-queued-for-marker");
        public static readonly JobStateReason JobPrinting = new("job-printing");
        public static readonly JobStateReason JobCanceledByUser = new("job-canceled-by-user");
        public static readonly JobStateReason JobCanceledByOperator = new("job-canceled-by-operator");
        public static readonly JobStateReason JobCanceledAtDevice = new("job-canceled-at-device");
        public static readonly JobStateReason AbortedBySystem = new("aborted-by-system");
        public static readonly JobStateReason UnsupportedCompression = new("unsupported-compression");
        public static readonly JobStateReason CompressionError = new("compression-error");
        public static readonly JobStateReason UnsupportedDocumentFormat = new("unsupported-document-format");
        public static readonly JobStateReason DocumentFormatError = new("document-format-error");
        public static readonly JobStateReason ProcessingToStopPoint = new("processing-to-stop-point");
        public static readonly JobStateReason ServiceOffLine = new("service-off-line");
        public static readonly JobStateReason JobCompletedSuccessfully = new("job-completed-successfully");
        public static readonly JobStateReason JobCompletedWithWarnings = new("job-completed-with-warnings");
        public static readonly JobStateReason JobCompletedWithErrors = new("job-completed-with-errors");
        public static readonly JobStateReason JobRestartable = new("job-restartable");
        public static readonly JobStateReason QueuedInDevice = new("queued-in-device");
        public static readonly JobStateReason DigitalSignatureDidNotVerify = new("digital-signature-did-not-verify");
        public static readonly JobStateReason ErrorsDetected = new("errors-detected");
        public static readonly JobStateReason JobDelayOutputUntilSpecified = new("job-delay-output-until-specified");
        public static readonly JobStateReason JobSpooling = new("job-spooling");
        public static readonly JobStateReason JobStreaming = new("job-streaming");
        public static readonly JobStateReason WarningsDetected = new("warnings-detected");
        public static readonly JobStateReason ConflictingAttributes = new("conflicting-attributes");
        public static readonly JobStateReason JobCanceledAfterTimeout = new("job-canceled-after-timeout");
        public static readonly JobStateReason JobHeldForAuthorization = new("job-held-for-authorization");
        public static readonly JobStateReason JobHeldForButtonPress = new("job-held-for-button-press");
        public static readonly JobStateReason JobHeldForRelease = new("job-held-for-release");
        public static readonly JobStateReason JobPasswordWait = new("job-password-wait");
        public static readonly JobStateReason JobPrintedSuccessfully = new("job-printed-successfully");
        public static readonly JobStateReason JobPrintedWithErrors = new("job-printed-with-errors");
        public static readonly JobStateReason JobPrintedWithWarnings = new("job-printed-with-warnings");
        public static readonly JobStateReason JobResuming = new("job-resuming");
        public static readonly JobStateReason JobReleaseWait = new("job-release-wait");
        public static readonly JobStateReason JobStored = new("job-stored");
        public static readonly JobStateReason JobStoredWithErrors = new("job-stored-with-errors");
        public static readonly JobStateReason JobStoredWithWarnings = new("job-stored-with-warnings");
        public static readonly JobStateReason JobStoring = new("job-storing");
        /// <summary>
        /// Obsolete legacy compatibility value.
        /// See: IPP Enterprise Printing Extensions v2.0 (PWG 5100.11-2024) Section 9.2.
        /// </summary>
        public static readonly JobStateReason JobSavedSuccessfully = new("job-saved-successfully");
        /// <summary>
        /// Obsolete legacy compatibility value.
        /// See: IPP Enterprise Printing Extensions v2.0 (PWG 5100.11-2024) Section 9.2.
        /// </summary>
        public static readonly JobStateReason JobSavedWithErrors = new("job-saved-with-errors");
        /// <summary>
        /// Obsolete legacy compatibility value.
        /// See: IPP Enterprise Printing Extensions v2.0 (PWG 5100.11-2024) Section 9.2.
        /// </summary>
        public static readonly JobStateReason JobSavedWithWarnings = new("job-saved-with-warnings");
        /// <summary>
        /// Obsolete legacy compatibility value.
        /// See: IPP Enterprise Printing Extensions v2.0 (PWG 5100.11-2024) Section 9.2.
        /// </summary>
        public static readonly JobStateReason JobSaving = new("job-saving");
        public static readonly JobStateReason JobSuspendedByOperator = new("job-suspended-by-operator");
        public static readonly JobStateReason JobSuspendedBySystem = new("job-suspended-by-system");
        public static readonly JobStateReason JobSuspendedByUser = new("job-suspended-by-user");
        public static readonly JobStateReason JobSuspendedForApproval = new("job-suspended-for-approval");
        public static readonly JobStateReason JobSuspending = new("job-suspending");
        public static readonly JobStateReason UnsupportedAttributesOrValues = new("unsupported-attributes-or-values");

        public override string ToString() => Value;
        public static implicit operator string(JobStateReason bin) => bin.Value;
        public static explicit operator JobStateReason(string value) => new(value);
    }
}
