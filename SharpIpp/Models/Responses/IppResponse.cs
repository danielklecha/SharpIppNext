using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

public abstract class IppResponse<TOperationAttributes> : IIppResponse where TOperationAttributes : OperationAttributes
{
    /// <summary>
    ///     The IPP version number.
    /// </summary>
    public IppVersion Version { get; set; } = new();

    /// <summary>
    ///     The response status code.
    /// </summary>
    public IppStatusCode StatusCode { get; set; }

    /// <summary>
    ///     The request ID.
    ///     The Printer object MUST return the value of the "request-id"
    ///     supplied by the client in the request.
    /// </summary>
    public int RequestId { get; set; }

    /// <summary>
    ///     The operation attributes group.
    /// </summary>
    public TOperationAttributes? OperationAttributes { get; set; }

    OperationAttributes? IIppResponse.OperationAttributes
    {
        get => OperationAttributes;
        set => OperationAttributes = (TOperationAttributes?)value;
    }
}
