using System;
using System.Collections.Generic;
using System.IO;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests
{
    /// <summary>
    /// Print-Job Operation
    /// This REQUIRED operation allows a client to submit a print job with
    /// only one document and supply the document data (rather than just a
    /// reference to the data).
    /// See: RFC 2911 Section 3.2.1
    /// </summary>
    public class PrintJobRequest : IppRequest<PrintJobOperationAttributes>, IIppPrinterRequest
    {
        /// <summary>
        /// The document data.
        /// </summary>
        /// <code>document</code>
        public Stream? Document { get; set; }
        
        /// <summary>
        /// The job-template-attributes IPP attribute.
        /// See: IPP
        /// </summary>
        /// <code>job-template-attributes</code>
        public JobTemplateAttributes? JobTemplateAttributes { get; set; }
    }
}
