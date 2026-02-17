using System;
using System.Collections.Generic;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests
{
    /// <summary>
    ///     <a href="https://tools.ietf.org/html/rfc2911#section-3.3.3">Cancel-Job Operation</a>
    ///     This REQUIRED operation allows a client to cancel a Print Job from
    ///     the time the job is created up to the time it is completed, canceled,
    ///     or aborted.  Since a Job might already be printing by the time a
    ///     Cancel-Job is received, some media sheet pages might be printed
    ///     before the job is actually terminated.
    /// </summary>
    public class CancelJobRequest : IppRequest<CancelJobOperationAttributes>, IIppJobRequest
    {

    }
}
