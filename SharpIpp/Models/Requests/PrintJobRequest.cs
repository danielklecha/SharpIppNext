using System;
using System.Collections.Generic;
using System.IO;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests
{
    /// <summary>
    ///     <a href="https://datatracker.ietf.org/doc/html/rfc2911#section-3.2.1">Print-Job Operation</a>
    ///     This REQUIRED operation allows a client to submit a print job with
    ///     only one document and supply the document data (rather than just a
    ///     reference to the data).
    /// </summary>
    public class PrintJobRequest : IppRequest<PrintJobOperationAttributes>, IIppPrinterRequest
    {
        public Stream? Document { get; set; }

        public JobTemplateAttributes? JobTemplateAttributes { get; set; }
    }
}
