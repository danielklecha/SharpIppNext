using System;
using System.Collections.Generic;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests
{
    /// <summary>
    /// Send-URI Operation
    /// This OPTIONAL operation is identical to the Send-Document operation
    /// except that a client MUST supply a URI reference
    /// ("document-uri" operation attribute) rather than the document data
    /// itself.  If a Printer object supports this operation, clients can use
    /// both Send-URI or Send-Document operations to add new documents to an
    /// existing multi-document Job object.  However, if a client needs to
    /// indicate that the previous Send-URI or Send-Document was the last
    /// document,  the client MUST use the Send-Document operation with no
    /// document data and the "last-document" flag set to 'true' (rather than
    /// using a Send-URI operation with no "document-uri" operation
    /// attribute).
    /// See: RFC 2911 Section 3.3.2
    /// </summary>
    public class SendUriRequest : IppRequest<SendUriOperationAttributes>, IIppJobRequest
    {
        /// <summary>
        /// The document-template-attributes IPP attribute.
        /// See: PWG 5100.5-2024 Section 8.5.1
        /// </summary>
        /// <code>document-template-attributes</code>
        public DocumentTemplateAttributes? DocumentTemplateAttributes { get; set; }
    }
}
