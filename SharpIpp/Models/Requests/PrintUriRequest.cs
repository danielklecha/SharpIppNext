using System;
using System.Collections.Generic;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests
{
    /// <summary>
    /// Print-URI Operation
    /// This OPTIONAL operation is identical to the Print-Job operation
    /// except that a client supplies a URI reference to the
    /// document data using the "document-uri" (uri) operation attribute (in
    /// Group 1) rather than including the document data itself.  Before
    /// returning the response, the Printer MUST validate that the Printer
    /// supports the retrieval method (e.g., http, ftp, etc.) implied by the
    /// URI, and MUST check for valid URI syntax.  If the client-supplied URI
    /// scheme is not supported, i.e. the value is not in the Printer
    /// object's "referenced-uri-scheme-supported" attribute, the Printer
    /// object MUST reject the request and return the 'client-error-uri-
    /// scheme-not-supported' status code.
    /// See: RFC 2911 Section 3.2.2
    /// </summary>
    public class PrintUriRequest : IppRequest<PrintUriOperationAttributes>, IIppPrinterRequest
    {
        /// <summary>
        /// The job-template-attributes IPP attribute.
        /// See: IPP
        /// </summary>
        /// <code>job-template-attributes</code>
        public JobTemplateAttributes? JobTemplateAttributes { get; set; }
    }
}
