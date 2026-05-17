namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// See: RFC 2911 Section 4.4.11
    /// </summary>
    public enum PrinterState
    {
        /// <summary>
        /// The Printer is idle and ready to accept new jobs.
        /// See: RFC 8011 Section 5.4.11
        /// </summary>
        Idle = 3,
        /// <summary>
        /// The Printer is processing one or more jobs.
        /// See: RFC 8011 Section 5.4.11
        /// </summary>
        Processing = 4,
        /// <summary>
        /// The Printer has stopped and is not processing jobs.
        /// See: RFC 8011 Section 5.4.11
        /// </summary>
        Stopped = 5,
    }
}
