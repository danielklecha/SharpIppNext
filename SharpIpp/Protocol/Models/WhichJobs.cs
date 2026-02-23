namespace SharpIpp.Protocol.Models
{
    public enum WhichJobs
    {
        Unsupported,

        /// <summary>
        ///     This includes any Job object whose state is
        ///     'completed', 'canceled', or 'aborted'.
        /// </summary>
        Completed,

        /// <summary>
        ///     This includes any Job object whose state is
        ///     'pending', 'processing', 'processing-stopped', or 'pending-
        ///     held'.
        /// </summary>
        NotCompleted,
    }
}
