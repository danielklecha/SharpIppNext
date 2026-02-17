using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Models.Requests;
public class IppRequest<TOperationAttributes> : IIppRequest where TOperationAttributes : OperationAttributes
{
    public IppVersion Version { get; set; } = IppVersion.V1_1;

    public int RequestId { get; set; } = 1;


    public TOperationAttributes? OperationAttributes { get; set; }

    OperationAttributes? IIppRequest.OperationAttributes => OperationAttributes;
}
