using System;
using System.Collections.Generic;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests
{
    /// <summary>
    ///     <a href="https://tools.ietf.org/html/rfc2911#section-3.3.6">Release-Job Operation</a>
    ///     This OPTIONAL operation allows a client to release a previously held
    ///     job so that it is again eligible for scheduling.  If the Hold-Job
    ///     operation is supported, then the Release-Job operation MUST be
    ///     supported, and vice-versa.
    ///     This operation removes the "job-hold-until" job attribute, if
    ///     present, from the job object that had been supplied in the create or
    ///     most recent Hold-Job or Restart-Job operation and removes its effect
    ///     on the job.  The IPP object MUST remove the 'job-hold-until-
    ///     specified' value from the job's "job-state-reasons" attribute, if
    ///     present.
    /// </summary>
    public class ReleaseJobRequest : IppRequest<ReleaseJobOperationAttributes>, IIppJobRequest
    {
        
    }
}
