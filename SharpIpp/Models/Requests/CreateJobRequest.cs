using System;
using System.Collections.Generic;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests
{
    /// <summary>
    ///     <a href="https://datatracker.ietf.org/doc/html/rfc2911#section-3.2.4">Create-Job Operation</a>
    ///     This OPTIONAL operation is similar to the Print-Job operation
    ///     except that in the Create-Job request, a client does
    ///     not supply document data or any reference to document data.  Also,
    ///     the client does not supply any of the "document-name", "document-
    ///     format", "compression", or "document-natural-language" operation
    ///     attributes.  This operation is followed by one or more Send-Document
    ///     or Send-URI operations.  In each of those operation requests, the
    ///     client OPTIONALLY supplies the "document-name", "document-format",
    ///     and "document-natural-language" attributes for each document in the
    ///     multi-document Job object.
    /// </summary>
    public class CreateJobRequest : IppRequest<CreateJobOperationAttributes>, IIppPrinterRequest
    {
        public JobTemplateAttributes? JobTemplateAttributes { get; set; }
    }
}
