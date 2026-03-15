namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// PWG 5100.5-2024 Section 6.2 - document-state-reasons
    /// Reuses the same keyword vocabulary as job-state-reasons.
    /// </summary>
    public enum DocumentStateReason
    {
        None,
        DocumentIncoming,
        DocumentDataInsufficient,
        DocumentAccessError,
        SubmissionInterrupted,
        DocumentOutgoing,
        ResourcesAreNotReady,
        PrinterStoppedPartly,
        PrinterStopped,
        DocumentInterpreting,
        DocumentQueued,
        DocumentTransforming,
        DocumentQueuedForMarker,
        DocumentPrinting,
        DocumentCanceledByUser,
        DocumentCanceledByOperator,
        DocumentCanceledAtDevice,
        AbortedBySystem,
        UnsupportedCompression,
        CompressionError,
        UnsupportedDocumentFormat,
        DocumentFormatError,
        ProcessingToStopPoint,
        ServiceOffLine,
        DocumentCompletedSuccessfully,
        DocumentCompletedWithWarnings,
        DocumentCompletedWithErrors,
        CanceledByUser,
        CanceledByOperator,
    }
}
