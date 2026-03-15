using System;
using System.Collections.Generic;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests
{
    /// <summary>
    /// Create-Job Operation
    /// This OPTIONAL operation is similar to the Print-Job operation
    /// except that in the Create-Job request, a client does
    /// not supply document data or any reference to document data.  Also,
    /// the client does not supply any of the "document-name", "document-
    /// format", "compression", or "document-natural-language" operation
    /// attributes.  This operation is followed by one or more Send-Document
    /// or Send-URI operations.  In each of those operation requests, the
    /// client OPTIONALLY supplies the "document-name", "document-format",
    /// and "document-natural-language" attributes for each document in the
    /// multi-document Job object.
    /// See: RFC 2911 Section 3.2.4
    /// </summary>
    public class CreateJobRequest : IppRequest<CreateJobOperationAttributes>, IIppPrinterRequest
    {
        /// <summary>
        /// The job-template-attributes IPP attribute.
        /// See: IPP
        /// </summary>
        /// <code>job-template-attributes</code>
        public JobTemplateAttributes? JobTemplateAttributes { get; set; }
    }
}
