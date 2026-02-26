using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SharpIpp.Models.Requests;
public class OperationAttributes
{
    /// <summary>
    ///     The client MUST supply this attribute and the Printer object MUST
    ///     support this attribute.  It identifies the charset (coded character
    ///     set and encoding method) used by any 'name' and 'text' attributes
    ///     that the client is supplying in this request.
    ///     Defaults to "utf-8".
    /// </summary>
    public string? AttributesCharset { get; set; } = "utf-8";

    /// <summary>
    ///     The client MUST supply this attribute and the Printer object MUST
    ///     support this attribute.  It identifies the natural language used by
    ///     any 'name' and 'text' attributes that the client is supplying in this
    ///     request.
    ///     Defaults to "en".
    /// </summary>
    public string? AttributesNaturalLanguage { get; set; } = "en";

    /// <summary>
    ///     The client MUST supply this attribute and the Printer object MUST
    ///     support this attribute.  It contains the URI of the Printer object
    ///     which is the target of this operation.
    /// </summary>
    public Uri? PrinterUri { get; set; }

    /// <summary>
    ///     The client SHOULD supply this attribute.  The Printer object MUST
    ///     support this attribute.  It contains the name of the user who
    ///     submitted the request.
    /// </summary>
    public string? RequestingUserName { get; set; }

}
