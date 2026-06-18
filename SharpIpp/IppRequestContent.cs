using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp;

/// <summary>
/// Represents the HTTP content for an IPP request, encapsulating the serialization
/// of an <see cref="IIppRequestMessage"/> to a stream using an <see cref="IIppProtocol"/> instance.
/// </summary>
internal class IppRequestContent : HttpContent
{
    private readonly IIppRequestMessage _request;
    private readonly IIppProtocol _protocol;
    private readonly CancellationToken _cancellationToken;

    public IppRequestContent(IIppRequestMessage request, IIppProtocol protocol, CancellationToken cancellationToken)
    {
        _request = request;
        _protocol = protocol;
        _cancellationToken = cancellationToken;
        Headers.ContentType = new MediaTypeHeaderValue("application/ipp");
    }

    protected override Task SerializeToStreamAsync(Stream stream, TransportContext? context)
    {
        return _protocol.WriteIppRequestAsync(_request, stream, _cancellationToken);
    }

    protected override bool TryComputeLength(out long length)
    {
        length = -1;
        return false;
    }
}
