using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

public abstract class IppResponse<TOperationAttributes> : IIppResponse where TOperationAttributes : OperationAttributes
{
    public IppVersion Version { get; set; } = new();

    public IppStatusCode StatusCode { get; set; }

    public int RequestId { get; set; }

    public TOperationAttributes? OperationAttributes { get; set; }

    OperationAttributes? IIppResponse.OperationAttributes
    {
        get => OperationAttributes;
        set => OperationAttributes = (TOperationAttributes?)value;
    }
}
