using System;
using System.Collections.Generic;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests
{
    /// <summary>
    ///     <a href="https://datatracker.ietf.org/doc/html/rfc2911#section-3.2.6">Get-Jobs Operation</a>
    ///     This REQUIRED operation allows a client to retrieve the list of Job
    ///     objects belonging to the target Printer object.  The client may also
    ///     supply a list of Job attribute names and/or attribute group names.  A
    ///     group of Job object attributes will be returned for each Job object
    ///     that is returned.
    ///     This operation is similar to the Get-Job-Attributes operation, except
    ///     that this Get-Jobs operation returns attributes from possibly more
    ///     than one object.
    /// </summary>
    public class GetJobsRequest : IppRequest<GetJobsOperationAttributes>, IIppPrinterRequest
    {

    }
}
