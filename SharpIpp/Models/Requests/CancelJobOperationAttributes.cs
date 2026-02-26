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
    ///     The client OPTIONALLY supplies this attribute. The Printer object MUST
    ///     support this attribute. It is a message to the operator.
    /// </summary>
    public string? Message { get; set; }

}
