using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SharpIpp.Protocol;

public interface IIppProtocol
{
    Task<IIppRequestMessage> ReadIppRequestAsync(Stream stream, CancellationToken cancellationToken = default);
    Task<IIppResponseMessage> ReadIppResponseAsync(Stream stream, CancellationToken cancellationToken = default);
    Task WriteIppRequestAsync(IIppRequestMessage ippRequestMessage, Stream stream, CancellationToken cancellationToken = default);
    Task WriteIppResponseAsync(IIppResponseMessage message, Stream stream, CancellationToken cancellationToken = default);
}
