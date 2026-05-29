using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Validation;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SharpIpp;

public interface ISharpIppServer
{
    IIppRequestMessageValidator? RequestMessageValidator { get; set; }
    IIppRequestValidator? RequestValidator { get; set; }
    IIppResponseMessageValidator? ResponseMessageValidator { get; set; }
    IIppResponseValidator? ResponseValidator { get; set; }
    Task<IIppResponseMessage> CreateRawResponseAsync<T>(T ippResponsMessage, CancellationToken cancellationToken = default) where T : IIppResponse;
    Task<IIppRequestMessage> ReceiveRawRequestAsync(Stream stream, CancellationToken cancellationToken = default);
    Task<IIppRequest> ReceiveRequestAsync(IIppRequestMessage request, CancellationToken cancellationToken = default);
    Task<IIppRequest> ReceiveRequestAsync(Stream stream, CancellationToken cancellationToken = default);
    Task SendRawResponseAsync(IIppResponseMessage ippResponseMessage, Stream stream, CancellationToken cancellationToken = default);
    Task SendResponseAsync<T>(T ippResponsMessage, Stream stream, CancellationToken cancellationToken = default) where T : IIppResponse;
}
