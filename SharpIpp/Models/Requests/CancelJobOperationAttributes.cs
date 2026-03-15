using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Models.Requests;
public class CancelJobOperationAttributes : JobOperationAttributes
{
    /// <summary>
    /// The client OPTIONALLY supplies this attribute. The Printer object MUST support this attribute. It is a message to the operator
    /// See: pwg5100.1-2022 Section 6.9.2
    /// </summary>
    /// <code>message</code>
    public string? Message { get; set; }

}
