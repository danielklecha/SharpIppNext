using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Models.Requests;
[IppAttribute]
public class CancelJobOperationAttributes : JobOperationAttributes
{
    /// <summary>
    /// The client OPTIONALLY supplies this attribute. The Printer object MAY support this attribute. It is a message to the operator.
    /// See: RFC 8011 Section 4.3.3.1
    /// See: RFC 2911 Section 3.3.3.1
    /// </summary>
    /// <code>message</code>
    [IppAttribute(IppAttributeNames.Message, Tag = Tag.TextWithoutLanguage)]
    public string? Message { get; set; }

}
