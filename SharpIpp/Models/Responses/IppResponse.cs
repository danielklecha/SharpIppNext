using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

public abstract class IppResponse<TOperationAttributes> : IIppResponse where TOperationAttributes : OperationAttributes
{
    /// <summary>
    /// The version IPP attribute.
    /// See: RFC 8011 Section 5.1.1
    /// </summary>
    /// <code>version</code>
    public IppVersion Version { get; set; } = new();

    /// <summary>
    /// The status-code IPP attribute.
    /// See: RFC 8011 Section 5.1.2
    /// </summary>
    /// <code>status-code</code>
    public IppStatusCode StatusCode { get; set; }

    /// <summary>
    /// The request ID. The Printer object MUST return the value of the "request-id" supplied by the client in the request
    /// See: pwg5100.18 - IPP Shared Infrastructure Extensions v1.1 Section 5.10
    /// </summary>
    /// <code>request-id</code>
    public int RequestId { get; set; }

    /// <summary>
    /// The operation-attributes IPP attribute.
    /// See: rfc8010 Section 3.1.1
    /// </summary>
    /// <code>operation-attributes</code>
    public TOperationAttributes? OperationAttributes { get; set; }

    OperationAttributes? IIppResponse.OperationAttributes
    {
        get => OperationAttributes;
        set => OperationAttributes = (TOperationAttributes?)value;
    }
}
