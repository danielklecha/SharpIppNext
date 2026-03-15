using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Models.Requests;
public class SendUriOperationAttributes : SendDocumentOperationAttributes
{
    /// <summary>
    /// The client MUST supply this attribute. The Printer object MUST support this attribute. It contains the URI of the document to be printed
    /// See: pwg5100.18 - IPP Shared Infrastructure Extensions v1.1 Section 7.1.4
    /// </summary>
    /// <code>document-uri</code>
    public Uri? DocumentUri { get; set; }

}
