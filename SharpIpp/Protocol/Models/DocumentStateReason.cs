namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// PWG 5100.5-2024 Section 6.2 - document-state-reasons
    /// Reuses the same keyword vocabulary as job-state-reasons.
    /// </summary>
    public readonly record struct DocumentStateReason(string Value, bool IsValue = true) : ISmartEnum
    {
        /// <summary>
        /// No document state reasons apply.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        public static readonly DocumentStateReason None = new("none");

        /// <summary>
        /// The Document is being received by the Printer from the Client.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        public static readonly DocumentStateReason DocumentIncoming = new("document-incoming");

        /// <summary>
        /// The Printer has created the Document but is waiting for the document data before it can move the Document into the 'processing' state.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        public static readonly DocumentStateReason DocumentDataInsufficient = new("document-data-insufficient");

        /// <summary>
        /// The Printer could not access one or more documents passed by reference (e.g., via a URI).
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        public static readonly DocumentStateReason DocumentAccessError = new("document-access-error");

        /// <summary>
        /// The Document was not completely submitted for some unforeseen reason.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        public static readonly DocumentStateReason SubmissionInterrupted = new("submission-interrupted");

        /// <summary>
        /// The Printer is transmitting the Document to the output device.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        public static readonly DocumentStateReason DocumentOutgoing = new("document-outgoing");

        /// <summary>
        /// At least one of the resources needed by the Document is not ready.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        public static readonly DocumentStateReason ResourcesAreNotReady = new("resources-are-not-ready");

        /// <summary>
        /// The Printer has stopped partly.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        public static readonly DocumentStateReason PrinterStoppedPartly = new("printer-stopped-partly");

        /// <summary>
        /// The Printer has stopped completely.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        public static readonly DocumentStateReason PrinterStopped = new("printer-stopped");

        /// <summary>
        /// The Printer is interpreting the document data.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        public static readonly DocumentStateReason DocumentInterpreting = new("document-interpreting");

        /// <summary>
        /// The Document has been placed in the print queue.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        public static readonly DocumentStateReason DocumentQueued = new("document-queued");

        /// <summary>
        /// The Printer is transforming the document data to another format.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        public static readonly DocumentStateReason DocumentTransforming = new("document-transforming");

        /// <summary>
        /// The Document has been placed in the queue for the marker.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        public static readonly DocumentStateReason DocumentQueuedForMarker = new("document-queued-for-marker");

        /// <summary>
        /// The Printer is currently printing the Document.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        public static readonly DocumentStateReason DocumentPrinting = new("document-printing");

        /// <summary>
        /// The Document was canceled by the user who submitted it.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        public static readonly DocumentStateReason DocumentCanceledByUser = new("document-canceled-by-user");

        /// <summary>
        /// The Document was canceled by an operator.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        public static readonly DocumentStateReason DocumentCanceledByOperator = new("document-canceled-by-operator");

        /// <summary>
        /// The Document was canceled at the device.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        public static readonly DocumentStateReason DocumentCanceledAtDevice = new("document-canceled-at-device");

        /// <summary>
        /// The Document was aborted by the system.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        public static readonly DocumentStateReason AbortedBySystem = new("aborted-by-system");

        /// <summary>
        /// The Document was aborted because the document data used a compression algorithm that is not supported by the Printer.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        public static readonly DocumentStateReason UnsupportedCompression = new("unsupported-compression");

        /// <summary>
        /// The Document was aborted because the Printer encountered an error in the document data while decompressing it.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        public static readonly DocumentStateReason CompressionError = new("compression-error");

        /// <summary>
        /// The Document was aborted because the document data is in a format not supported by the Printer.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        public static readonly DocumentStateReason UnsupportedDocumentFormat = new("unsupported-document-format");

        /// <summary>
        /// The Document was aborted because the Printer encountered an error in the document data.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        public static readonly DocumentStateReason DocumentFormatError = new("document-format-error");

        /// <summary>
        /// The Printer is stopping the current Document at a convenient point, such as at the end of a page or column.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        public static readonly DocumentStateReason ProcessingToStopPoint = new("processing-to-stop-point");

        /// <summary>
        /// The Printer is off-line and accepting no Jobs.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        public static readonly DocumentStateReason ServiceOffLine = new("service-off-line");

        /// <summary>
        /// The Document completed successfully.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        public static readonly DocumentStateReason DocumentCompletedSuccessfully = new("document-completed-successfully");

        /// <summary>
        /// The Document completed with warnings.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        public static readonly DocumentStateReason DocumentCompletedWithWarnings = new("document-completed-with-warnings");

        /// <summary>
        /// The Document completed with errors.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        public static readonly DocumentStateReason DocumentCompletedWithErrors = new("document-completed-with-errors");

        /// <summary>
        /// The Document was canceled by the user.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        public static readonly DocumentStateReason CanceledByUser = new("canceled-by-user");

        /// <summary>
        /// The Document was canceled by an operator.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        public static readonly DocumentStateReason CanceledByOperator = new("canceled-by-operator");

        public override string ToString() => Value;
        public static implicit operator string(DocumentStateReason bin) => bin.Value;
        public static implicit operator DocumentStateReason(string value) => new(value);
    }
}
