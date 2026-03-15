using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Models.Requests;
public class IppRequest<TOperationAttributes> : IIppRequest where TOperationAttributes : OperationAttributes
{
    /// <summary>
    /// The version IPP attribute.
    /// See: RFC 8011 Section 5.1.1
    /// </summary>
    /// <code>version</code>
    public IppVersion Version { get; set; } = new();
    /// <summary>
    /// The request ID. The client MUST supply this value. The Printer object MUST return this value in the response
    /// See: pwg5100.18 - IPP Shared Infrastructure Extensions v1.1 Section 5.10
    /// </summary>
    /// <code>request-id</code>
    public int RequestId { get; set; } = 1;
    /// <summary>
    /// The operation-attributes IPP attribute.
    /// See: rfc8010 Section 3.1.1
    /// </summary>
    /// <code>operation-attributes</code>
    public TOperationAttributes? OperationAttributes { get; set; }

    OperationAttributes? IIppRequest.OperationAttributes => OperationAttributes;
}
