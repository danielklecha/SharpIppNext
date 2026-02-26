namespace SharpIpp.Protocol.Models
{
    /// <summary>
    ///     This attribute determines which job start/end sheet(s), if any, MUST
    ///     be printed with a job.
    ///     https://tools.ietf.org/html/rfc2911#section-4.2.3
    /// </summary>
    public enum JobSheets
    {

        /// <summary>
        ///     no job sheet is printed
        /// </summary>
        None,

        /// <summary>
        ///     one or more site specific standard job sheets are
        ///     printed, e.g. a single start sheet or both start and end sheet is
        ///     printed
        /// </summary>
        Standard,
    }
}
