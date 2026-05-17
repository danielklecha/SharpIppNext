namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// PWG 5100.5-2024 Section 6.2 - Document Status Attributes
    /// The "document-state" attribute supports all of the "job-state" values except '4' (pending-held).
    /// </summary>
    public enum DocumentState
    {
        /// <summary>
        /// The document is a candidate to start processing, but is not yet processing.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        Pending = 3,

        /// <summary>
        /// The document is being processed.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        Processing = 5,

        /// <summary>
        /// The document has stopped while processing.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        ProcessingStopped = 6,

        /// <summary>
        /// The document has been canceled.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        Canceled = 7,

        /// <summary>
        /// The document has been aborted by the system.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        Aborted = 8,

        /// <summary>
        /// The document has completed successfully or with warnings or errors.
        /// See: PWG 5100.5-2024 Section 6.2
        /// </summary>
        Completed = 9,
    }
}
