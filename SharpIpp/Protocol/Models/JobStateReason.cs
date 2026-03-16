using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Protocol.Models
{
    public readonly record struct JobStateReason(string Value)
    {
        public static readonly JobStateReason None = new("none");
        public static readonly JobStateReason JobIncoming = new("job-incoming");
        public static readonly JobStateReason JobDataInsufficient = new("job-data-insufficient");
        public static readonly JobStateReason DocumentAccessError = new("document-access-error");
        public static readonly JobStateReason SubmissionInterrupted = new("submission-interrupted");
        public static readonly JobStateReason JobOutgoing = new("job-outgoing");
        public static readonly JobStateReason JobHoldUntilSpecified = new("job-hold-until-specified");
        public static readonly JobStateReason ResourcesAreNotReady = new("resources-are-not-ready");
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

        public override string ToString() => Value;
        public static implicit operator string(JobStateReason bin) => bin.Value;
        public static explicit operator JobStateReason(string value) => new(value);
    }
}
