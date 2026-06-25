using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SharpIpp.Protocol.Extensions;

namespace SharpIpp.Protocol.Models;

public class IppBinaryReader : IDisposable
{
    private readonly Stream _stream;
    private readonly byte[] _buffer;

    public IppBinaryReader(Stream stream)
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _buffer = new byte[8];
    }

    public Stream BaseStream => _stream;

    public void Dispose()
    {
    }

    public async Task<short> ReadInt16BigEndianAsync(CancellationToken cancellationToken = default)
    {
        await ReadExactlyAsync(_buffer, 0, 2, cancellationToken).ConfigureAwait(false);
        return (short)((_buffer[0] << 8) | _buffer[1]);
    }

    public async Task<int> ReadInt32BigEndianAsync(CancellationToken cancellationToken = default)
    {
        await ReadExactlyAsync(_buffer, 0, 4, cancellationToken).ConfigureAwait(false);
        return (_buffer[0] << 24) | (_buffer[1] << 16) | (_buffer[2] << 8) | _buffer[3];
    }

    public async Task<byte> ReadByteAsync(CancellationToken cancellationToken = default)
    {
        await ReadExactlyAsync(_buffer, 0, 1, cancellationToken).ConfigureAwait(false);
        return _buffer[0];
    }

    public virtual async Task<byte[]> ReadBytesAsync(int count, CancellationToken cancellationToken = default)
    {
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
#if NETSTANDARD2_0
        if (count == 0) return new byte[0];
#else
        if (count == 0) return Array.Empty<byte>();
#endif
        byte[] result = new byte[count];
        await ReadExactlyAsync(result, 0, count, cancellationToken).ConfigureAwait(false);
        return result;
    }

    private async Task ReadExactlyAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        int totalRead = 0;
        while (totalRead < count)
        {
            int read = await _stream.ReadAsync(buffer, offset + totalRead, count - totalRead, cancellationToken).ConfigureAwait(false);
            if (read == 0)
            {
                throw new EndOfStreamException();
            }
            totalRead += read;
        }
    }
}
