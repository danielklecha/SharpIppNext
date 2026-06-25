using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SharpIpp.Protocol.Extensions;

namespace SharpIpp.Protocol.Models;

public class IppBinaryWriter : IDisposable
{
    private readonly Stream _stream;
    private readonly byte[] _buffer;

    public IppBinaryWriter(Stream stream)
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _buffer = new byte[8];
    }

    public Stream BaseStream => _stream;

    public void Dispose()
    {
    }

    public async Task WriteBigEndianAsync(short value, CancellationToken cancellationToken = default)
    {
        _buffer[0] = (byte)(value >> 8);
        _buffer[1] = (byte)value;
        await _stream.WriteAsync(_buffer, 0, 2, cancellationToken).ConfigureAwait(false);
    }

    public async Task WriteBigEndianAsync(int value, CancellationToken cancellationToken = default)
    {
        _buffer[0] = (byte)(value >> 24);
        _buffer[1] = (byte)(value >> 16);
        _buffer[2] = (byte)(value >> 8);
        _buffer[3] = (byte)value;
        await _stream.WriteAsync(_buffer, 0, 4, cancellationToken).ConfigureAwait(false);
    }

    public async Task WriteAsync(byte[] buffer, CancellationToken cancellationToken = default)
    {
        if (buffer == null) throw new ArgumentNullException(nameof(buffer));
        await _stream.WriteAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);
    }

    public async Task WriteAsync(byte value, CancellationToken cancellationToken = default)
    {
        _buffer[0] = value;
        await _stream.WriteAsync(_buffer, 0, 1, cancellationToken).ConfigureAwait(false);
    }
}
