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
        /// defines new "finishings" and "finishings-col" Job Template attribute values to specify additional finishing intent, including the placement of finishings with respect to the corners and edges of portrait and landscape documents.
        /// See: pwg5100.1-2022 Section 2.2
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
