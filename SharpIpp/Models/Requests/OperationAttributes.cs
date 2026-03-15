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
    /// The client MUST supply this attribute and the Printer object MUST support this attribute. It identifies the charset (coded character set and encoding method) used by any 'name' and 'text' attributes that the client is supplying in this request. Defaults to "utf-8"
    /// See: RFC 8011 Section 5.3.19
    /// </summary>
    /// <code>attributes-charset</code>
    public string? AttributesCharset { get; set; } = "utf-8";
    /// <summary>
    /// The client MUST supply this attribute and the Printer object MUST support this attribute. It identifies the natural language used by any 'name' and 'text' attributes that the client is supplying in this request. Defaults to "en"
    /// See: RFC 8011 Section 5.3.20
    /// </summary>
    /// <code>attributes-natural-language</code>
    public string? AttributesNaturalLanguage { get; set; } = "en";
    /// <summary>
    /// The client MUST supply this attribute and the Printer object MUST support this attribute. It contains the URI of the Printer object which is the target of this operation
    /// See: RFC 8011
    /// </summary>
    /// <code>printer-uri</code>
    public Uri? PrinterUri { get; set; }
    /// <summary>
    /// The client SHOULD supply this attribute. The Printer object MUST support this attribute. It contains the name of the user who submitted the request
    /// See: pwg5100.11 - IPP Enterprise Printing Extensions v2.0 Section 9.3
    /// </summary>
    /// <code>requesting-user-name</code>
    public string? RequestingUserName { get; set; }

}
