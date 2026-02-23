using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Protocol.Models
{
    public enum JobStateReason
    {
        Unsupported,
        None,
        JobIncoming,
        JobDataInsufficient,
        DocumentAccessError,
        SubmissionInterrupted,
        JobOutgoing,
        JobHoldUntilSpecified,
        ResourcesAreNotReady,
        PrinterStoppedPartly,
        PrinterStopped,
        JobInterpreting,
        JobQueued,
        JobTransforming,
        JobQueuedForMarker,
        JobPrinting,
        JobCanceledByUser,
        JobCanceledByOperator,
        JobCanceledAtDevice,
        AbortedBySystem,
        UnsupportedCompression,
        CompressionError,
        UnsupportedDocumentFormat,
        DocumentFormatError,
        ProcessingToStopPoint,
        ServiceOffLine,
        JobCompletedSuccessfully,
        JobCompletedWithWarnings,
        JobCompletedWithErrors,
        JobRestartable,
        QueuedInDevice
    }
}
