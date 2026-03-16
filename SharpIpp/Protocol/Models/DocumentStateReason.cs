namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// PWG 5100.5-2024 Section 6.2 - document-state-reasons
    /// Reuses the same keyword vocabulary as job-state-reasons.
    /// </summary>
    public readonly record struct DocumentStateReason(string Value)
    {
        public static readonly DocumentStateReason None = new("none");
        public static readonly DocumentStateReason DocumentIncoming = new("document-incoming");
        public static readonly DocumentStateReason DocumentDataInsufficient = new("document-data-insufficient");
        public static readonly DocumentStateReason DocumentAccessError = new("document-access-error");
        public static readonly DocumentStateReason SubmissionInterrupted = new("submission-interrupted");
        public static readonly DocumentStateReason DocumentOutgoing = new("document-outgoing");
        public static readonly DocumentStateReason ResourcesAreNotReady = new("resources-are-not-ready");
        public static readonly DocumentStateReason PrinterStoppedPartly = new("printer-stopped-partly");
        public static readonly DocumentStateReason PrinterStopped = new("printer-stopped");
        public static readonly DocumentStateReason DocumentInterpreting = new("document-interpreting");
        public static readonly DocumentStateReason DocumentQueued = new("document-queued");
        public static readonly DocumentStateReason DocumentTransforming = new("document-transforming");
        public static readonly DocumentStateReason DocumentQueuedForMarker = new("document-queued-for-marker");
        public static readonly DocumentStateReason DocumentPrinting = new("document-printing");
        public static readonly DocumentStateReason DocumentCanceledByUser = new("document-canceled-by-user");
        public static readonly DocumentStateReason DocumentCanceledByOperator = new("document-canceled-by-operator");
        public static readonly DocumentStateReason DocumentCanceledAtDevice = new("document-canceled-at-device");
        public static readonly DocumentStateReason AbortedBySystem = new("aborted-by-system");
        public static readonly DocumentStateReason UnsupportedCompression = new("unsupported-compression");
        public static readonly DocumentStateReason CompressionError = new("compression-error");
        public static readonly DocumentStateReason UnsupportedDocumentFormat = new("unsupported-document-format");
        public static readonly DocumentStateReason DocumentFormatError = new("document-format-error");
        public static readonly DocumentStateReason ProcessingToStopPoint = new("processing-to-stop-point");
        public static readonly DocumentStateReason ServiceOffLine = new("service-off-line");
        public static readonly DocumentStateReason DocumentCompletedSuccessfully = new("document-completed-successfully");
        public static readonly DocumentStateReason DocumentCompletedWithWarnings = new("document-completed-with-warnings");
        public static readonly DocumentStateReason DocumentCompletedWithErrors = new("document-completed-with-errors");
        public static readonly DocumentStateReason CanceledByUser = new("canceled-by-user");
        public static readonly DocumentStateReason CanceledByOperator = new("canceled-by-operator");

        public override string ToString() => Value;
        public static implicit operator string(DocumentStateReason bin) => bin.Value;
        public static explicit operator DocumentStateReason(string value) => new(value);
    }
}
