using System;
using System.Collections.Generic;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests
{
    /// <summary>
    ///     <a href="https://datatracker.ietf.org/doc/html/rfc2911#section-3.2.7">Pause-Printer Operation</a>
    ///     This OPTIONAL operation allows a client to stop the Printer object
    ///     from scheduling jobs on all its devices.  Depending on
    ///     implementation, the Pause-Printer operation MAY also stop the Printer
    ///     from processing the current job or jobs.  Any job that is currently
    ///     being printed is either stopped as soon as the implementation permits
    ///     or is completed, depending on implementation.  The Printer object
    ///     MUST still accept create operations to create new jobs, but MUST
    ///     prevent any jobs from entering the 'processing' state.
    /// </summary>
    public class PausePrinterRequest : IppRequest<PausePrinterOperationAttributes>, IIppPrinterRequest
    {

    }
}
