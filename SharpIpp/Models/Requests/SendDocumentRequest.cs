using System;
using System.Collections.Generic;
using System.IO;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests
{
    /// <summary>
    /// Send-Document Operation
    /// This OPTIONAL operation allows a client to create a multi-document
    /// Job object that is initially "empty" (contains no documents).  In the
    /// Create-Job response, the Printer object returns the Job object's URI
    /// (the "job-uri" attribute) and the Job object's 32-bit identifier (the
    /// "job-id" attribute).  For each new document that the client desires
    /// to add, the client uses a Send-Document operation.  Each Send-
    /// Document Request contains the entire stream of document data for one
    /// document.
    /// See: RFC 2911 Section 3.3.1
    /// </summary>
    public class SendDocumentRequest : IppRequest<SendDocumentOperationAttributes>, IIppJobRequest
    {
    /// <summary>
    /// defines new "finishings" and "finishings-col" Job Template attribute values to specify additional finishing intent, including the placement of finishings with respect to the corners and edges of portrait and landscape documents.
    /// See: pwg5100.1-2022 Section 2.2
    /// </summary>
        /// <code>document</code>
        public Stream? Document { get; set; }
    /// <summary>
    /// The document-template-attributes IPP attribute.
    /// See: IPP
    /// </summary>
        /// <code>document-template-attributes</code>
        public DocumentTemplateAttributes? DocumentTemplateAttributes { get; set; }
    }
}
