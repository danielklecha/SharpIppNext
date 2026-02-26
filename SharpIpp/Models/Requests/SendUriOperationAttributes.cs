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
    ///     The client MUST supply this attribute.  The Printer object MUST
    ///     support this attribute.  It contains the URI of the document
    ///     to be printed.
    /// </summary>
    public Uri? DocumentUri { get; set; }

}
