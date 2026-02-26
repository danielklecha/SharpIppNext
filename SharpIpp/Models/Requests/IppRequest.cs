using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Models.Requests;
public class IppRequest<TOperationAttributes> : IIppRequest where TOperationAttributes : OperationAttributes
{
    /// <summary>
    ///     The IPP version number.
    /// </summary>
    public IppVersion Version { get; set; } = new();

    /// <summary>
    ///     The request ID.
    ///     The client MUST supply this value.  The Printer object MUST return
    ///     this value in the response.
    /// </summary>
    public int RequestId { get; set; } = 1;

    /// <summary>
    ///     The operation attributes group.
    /// </summary>
    public TOperationAttributes? OperationAttributes { get; set; }

    OperationAttributes? IIppRequest.OperationAttributes => OperationAttributes;
}
