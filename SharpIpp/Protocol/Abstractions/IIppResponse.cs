using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol
{
    public interface IIppResponse
    {
        IppVersion Version { get; set; }

        IppStatusCode StatusCode { get; set; }

        int RequestId { get; set; }

        string? StatusMessage { get; set; }

        string[]? DetailedStatusMessage { get; set; }

        string? DocumentAccessError { get; set; }
    }
}
