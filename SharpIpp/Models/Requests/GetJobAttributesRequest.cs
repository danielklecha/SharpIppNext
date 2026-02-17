using System;
using System.Collections.Generic;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests
{
    /// <summary>
    ///     <a href="https://datatracker.ietf.org/doc/html/rfc2911#section-3.3.4">Get-Job-Attributes Operation</a>
    ///     This REQUIRED operation allows a client to request the values of
    ///     attributes of a Job object and it is almost identical to the Get-
    ///     Printer-Attributes operation.  The only
    ///     differences are that the operation is directed at a Job object rather
    ///     than a Printer object, there is no "document-format" operation
    ///     attribute used when querying a Job object, and the returned attribute
    ///     group is a set of Job object attributes rather than a set of Printer
    ///     object attributes.
    /// </summary>
    public class GetJobAttributesRequest : IppRequest<GetJobAttributesOperationAttributes>, IIppJobRequest
    {

    }
}
