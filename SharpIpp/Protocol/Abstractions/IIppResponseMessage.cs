using System.Collections.Generic;

using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol
{
    public interface IIppResponseMessage : IIppResponse
    {
        List<IppSection> Sections { get; }
    }
}
