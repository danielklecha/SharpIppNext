using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SharpIpp;

/// <summary>
/// A read-only stream that sequentially reads from an ordered list of inner streams.
/// When one stream is exhausted, reading continues from the next stream in the list.
/// </summary>
internal sealed class ConcatenatedReadStream : Stream
{
    private readonly Stream[] _streams;
    private readonly bool[] _leaveOpen;
    private int _currentIndex;
    private bool _disposed;

    /// <summary>
    /// Creates a new <see cref="ConcatenatedReadStream"/> that reads sequentially from the given streams.
    /// </summary>
    /// <param name="leaveOpen">If true, inner streams are not disposed when this stream is disposed.</param>
    /// <param name="streams">The streams to concatenate, read in order.</param>
    public ConcatenatedReadStream(bool leaveOpen, params Stream[] streams)
    {
        if (streams is null)
            throw new ArgumentNullException(nameof(streams));
        _streams = streams;
        _leaveOpen = new bool[streams.Length];
        if (leaveOpen)
        {
            for (int i = 0; i < _leaveOpen.Length; i++)
            {
                _leaveOpen[i] = true;
            }
        }
    }

    /// <summary>
    /// Creates a new <see cref="ConcatenatedReadStream"/> that reads sequentially from the given streams with individual disposal settings.
    /// </summary>
    /// <param name="streams">The streams to concatenate along with their leaveOpen configuration.</param>
    public ConcatenatedReadStream(params (Stream Stream, bool LeaveOpen)[] streams)
    {
        if (streams is null)
            throw new ArgumentNullException(nameof(streams));
        _streams = new Stream[streams.Length];
        _leaveOpen = new bool[streams.Length];
        for (int i = 0; i < streams.Length; i++)
        {
            _streams[i] = streams[i].Stream;
            _leaveOpen[i] = streams[i].LeaveOpen;
        }
    }

    /// <inheritdoc />
    public override bool CanRead => true;

    /// <inheritdoc />
    public override bool CanSeek => false;

    /// <inheritdoc />
    public override bool CanWrite => false;

    /// <inheritdoc />
    public override long Length
    {
        get
        {
            long total = 0;
            for (int i = 0; i < _streams.Length; i++)
            {
                total += _streams[i].Length;
            }
            return total;
        }
    }

    /// <inheritdoc />
    public override long Position
    {
        get => throw new NotSupportedException();
        set => throw new NotSupportedException();
    }

    /// <inheritdoc />
    public override int Read(byte[] buffer, int offset, int count)
    {
        int totalRead = 0;
        while (count > 0 && _currentIndex < _streams.Length)
        {
            int bytesRead = _streams[_currentIndex].Read(buffer, offset, count);
            if (bytesRead == 0)
            {
                _currentIndex++;
                continue;
            }
            totalRead += bytesRead;
            offset += bytesRead;
            count -= bytesRead;
        }
        return totalRead;
    }

    /// <inheritdoc />
    public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        int totalRead = 0;
        while (count > 0 && _currentIndex < _streams.Length)
        {
            int bytesRead = await _streams[_currentIndex].ReadAsync(buffer, offset, count, cancellationToken).ConfigureAwait(false);
            if (bytesRead == 0)
            {
                _currentIndex++;
                continue;
            }
            totalRead += bytesRead;
            offset += bytesRead;
            count -= bytesRead;
        }
        return totalRead;
    }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP || NET5_0_OR_GREATER
    /// <inheritdoc />
    public override int Read(Span<byte> buffer)
    {
        int totalRead = 0;
        while (!buffer.IsEmpty && _currentIndex < _streams.Length)
        {
            int bytesRead = _streams[_currentIndex].Read(buffer);
            if (bytesRead == 0)
            {
                _currentIndex++;
                continue;
            }
            totalRead += bytesRead;
            buffer = buffer.Slice(bytesRead);
        }
        return totalRead;
    }

    /// <inheritdoc />
    public override async ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
    {
        int totalRead = 0;
        while (!buffer.IsEmpty && _currentIndex < _streams.Length)
        {
            int bytesRead = await _streams[_currentIndex].ReadAsync(buffer, cancellationToken).ConfigureAwait(false);
            if (bytesRead == 0)
            {
                _currentIndex++;
                continue;
            }
            totalRead += bytesRead;
            buffer = buffer.Slice(bytesRead);
        }
        return totalRead;
    }
#endif

    /// <inheritdoc />
    public override void Flush()
    {
    }

    /// <inheritdoc />
    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();

    /// <inheritdoc />
    public override void SetLength(long value) => throw new NotSupportedException();

    /// <inheritdoc />
    public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            _disposed = true;
            for (int i = 0; i < _streams.Length; i++)
            {
                if (!_leaveOpen[i])
                {
                    _streams[i].Dispose();
                }
            }
        }
        base.Dispose(disposing);
    }
}
