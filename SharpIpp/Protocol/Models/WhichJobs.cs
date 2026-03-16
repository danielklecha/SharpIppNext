namespace SharpIpp.Protocol.Models
{
    public readonly record struct WhichJobs(string Value)
    {
        /// <summary>
        /// This includes any Job object whose state is
        /// 'completed', 'canceled', or 'aborted'.
        /// </summary>
        public static readonly WhichJobs Completed = new("completed");

        /// <summary>
        /// This includes any Job object whose state is
        /// 'pending', 'processing', 'processing-stopped', or 'pending-
        /// held'.
        /// </summary>
        public static readonly WhichJobs NotCompleted = new("not-completed");

        public static readonly WhichJobs Aborted = new("aborted");
        public static readonly WhichJobs All = new("all");
        public static readonly WhichJobs Canceled = new("canceled");
        public static readonly WhichJobs Pending = new("pending");
        public static readonly WhichJobs PendingHeld = new("pending-held");
        public static readonly WhichJobs Processing = new("processing");
        public static readonly WhichJobs ProcessingStopped = new("processing-stopped");

        public override string ToString() => Value;
        public static implicit operator string(WhichJobs bin) => bin.Value;
        public static explicit operator WhichJobs(string value) => new(value);
    }
}
