using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SharpIpp.Models.Requests;
[IppAttribute]
public class PrintUriOperationAttributes : PrintJobOperationAttributes
{
    /// <summary>
    /// The client MUST supply this attribute. The Printer object MUST support this attribute. It contains the URI of the document to be printed
    /// See: RFC 8011 Section 3.2.2
    /// </summary>
    /// <code>document-uri</code>
    [IppAttribute(IppAttributeNames.DocumentUri, Tag = Tag.Uri)]
    public IppValue<Uri>? DocumentUri { get; set; }

    /// <summary>
    /// The <c>document-access</c> operation attribute.
    /// DEPRECATED.
    /// See: PWG 5100.18-2025 Section 7.1.5
    /// </summary>
    [IppAttribute(IppAttributeNames.DocumentAccess)]
    [Obsolete("The 'document-access' attribute is deprecated in favor of URI authentication. See PWG 5100.18-2025 Section 7.1.5.")]
    public IppValue<DocumentAccess>? DocumentAccess { get; set; }

}
