using System;
using System.Collections.Generic;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests
{
    /// <summary>
    /// Cancel-Job Operation
    /// This REQUIRED operation allows a client to cancel a Print Job from
    /// the time the job is created up to the time it is completed, canceled,
    /// or aborted.  Since a Job might already be printing by the time a
    /// Cancel-Job is received, some media sheet pages might be printed
    /// before the job is actually terminated.
    /// See: RFC 2911 Section 3.3.3
    /// </summary>
    public class CancelJobRequest : IppRequest<CancelJobOperationAttributes>, IIppJobRequest
    {

    }
}
