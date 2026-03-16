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

        public override string ToString() => Value;
        public static implicit operator string(WhichJobs bin) => bin.Value;
        public static explicit operator WhichJobs(string value) => new(value);
    }
}
