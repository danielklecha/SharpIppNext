using System;

namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// Specifies which jobs to return in a Get-Jobs response.
    /// See: RFC 8011 Section 4.2.6.1
    /// See: PWG 5100.7-2023 Section 5.2
    /// </summary>
    public readonly record struct WhichJobs(string Value, bool IsValue = true) : ISmartEnum
    {
        /// <summary>
        /// This includes any Job object whose state is
        /// 'completed', 'canceled', or 'aborted'.
        /// See: RFC 8011 Section 4.2.6.1
        /// </summary>
        public static readonly WhichJobs Completed = new("completed");

        /// <summary>
        /// This includes any Job object whose state is
        /// 'pending', 'processing', 'processing-stopped', or 'pending-held'.
        /// See: RFC 8011 Section 4.2.6.1
        /// </summary>
        public static readonly WhichJobs NotCompleted = new("not-completed");

        /// <summary>
        /// This includes any Job object whose state is 'aborted'.
        /// See: PWG 5100.7-2023 Section 5.2
        /// </summary>
        public static readonly WhichJobs Aborted = new("aborted");

        /// <summary>
        /// This includes all Job objects regardless of state.
        /// See: PWG 5100.7-2023 Section 5.2
        /// </summary>
        public static readonly WhichJobs All = new("all");

        /// <summary>
        /// This includes any Job object whose state is 'canceled'.
        /// See: PWG 5100.7-2023 Section 5.2
        /// </summary>
        public static readonly WhichJobs Canceled = new("canceled");

        /// <summary>
        /// This includes any Job object that is fetchable by a Proxy using the Fetch-Job operation.
        /// See: PWG 5100.18-2025 Section 9.3
        /// </summary>
        public static readonly WhichJobs Fetchable = new("fetchable");

        /// <summary>
        /// This includes any Job object whose state is 'pending'.
        /// See: PWG 5100.7-2023 Section 5.2
        /// </summary>
        public static readonly WhichJobs Pending = new("pending");

        /// <summary>
        /// This includes any Job object whose state is 'pending-held'.
        /// See: PWG 5100.7-2023 Section 5.2
        /// </summary>
        public static readonly WhichJobs PendingHeld = new("pending-held");

        /// <summary>
        /// This includes any Job object whose state is 'processing'.
        /// See: PWG 5100.7-2023 Section 5.2
        /// </summary>
        public static readonly WhichJobs Processing = new("processing");

        /// <summary>
        /// This includes any Job object whose state is 'processing-stopped'.
        /// See: PWG 5100.7-2023 Section 5.2
        /// </summary>
        public static readonly WhichJobs ProcessingStopped = new("processing-stopped");

        /// <summary>
        /// This includes any Job object that is in the proof-print state.
        /// See: PWG 5100.7-2023 Section 5.2
        /// </summary>
        public static readonly WhichJobs ProofPrint = new("proof-print");

        /// <summary>
        /// This includes any Job object that is in the proof-and-suspend state.
        /// See: PWG 5100.7-2023 Section 5.2
        /// </summary>
        public static readonly WhichJobs ProofAndSuspend = new("proof-and-suspend");

        /// <summary>
        /// This includes any Job object that is stored in a group.
        /// See: PWG 5100.11-2024 Section 9.2
        /// </summary>
        public static readonly WhichJobs StoredGroup = new("stored-group");

        /// <summary>
        /// This includes any Job object that is stored and owned by the requesting user.
        /// See: PWG 5100.11-2024 Section 9.2
        /// </summary>
        public static readonly WhichJobs StoredOwner = new("stored-owner");

        /// <summary>
        /// This includes any Job object that is stored and publicly accessible.
        /// See: PWG 5100.11-2024 Section 9.2
        /// </summary>
        public static readonly WhichJobs StoredPublic = new("stored-public");

        /// <summary>
        /// Obsolete legacy compatibility value.
        /// See: IPP Enterprise Printing Extensions v2.0 (PWG 5100.11-2024) Section 9.2.
        /// </summary>
        [Obsolete("See IPP Enterprise Printing Extensions v2.0 (PWG 5100.11-2024) Section 9.2.")]
        public static readonly WhichJobs Saved = new("saved");

        public override string ToString() => Value;
        public static implicit operator string(WhichJobs bin) => bin.Value;
        public static explicit operator WhichJobs(string value) => new(value);
    }
}
