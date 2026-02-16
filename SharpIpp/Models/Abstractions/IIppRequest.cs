using System;
using System.Collections.Generic;

using SharpIpp.Protocol.Models;

namespace SharpIpp.Models
{
    public interface IIppRequest
    {
        IppVersion Version { get; set; }

        int RequestId { get; set; }

        OperationAttributes? OperationAttributes { get; }


    }
}
