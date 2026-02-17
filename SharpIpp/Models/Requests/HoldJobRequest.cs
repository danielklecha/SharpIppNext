namespace SharpIpp.Models.Requests
{
    /// <summary>
    ///     <a href="https://datatracker.ietf.org/doc/html/rfc2911#section-3.3.5">Hold-Job Operation</a>
    ///     This OPTIONAL operation allows a client to hold a pending job in the
    ///     queue so that it is not eligible for scheduling.  If the Hold-Job
    ///     operation is supported, then the Release-Job operation MUST be
    ///     supported, and vice-versa.  The OPTIONAL "job-hold-until" operation
    ///     attribute allows a client to specify whether to hold the job
    ///     indefinitely or until a specified time period, if supported.
    /// </summary>
    public class HoldJobRequest : IppRequest<HoldJobOperationAttributes>, IIppJobRequest
    {

    }
}
