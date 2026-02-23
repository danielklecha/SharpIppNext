using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

public interface IIppJobResponse : IIppResponse
{
    JobAttributes? JobAttributes { get; set; }
}
