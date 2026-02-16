using System.Collections.Generic;

using SharpIpp.Models;

namespace SharpIpp.Protocol.Models
{
    public class IppResponseMessage : IppResponse, IIppResponseMessage
    {
        public List<IppSection> Sections { get; } = new List<IppSection>();
    }
}
