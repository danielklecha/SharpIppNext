using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SharpIpp.Tests.Unit;

[TestClass]
[ExcludeFromCodeCoverage]
public class ConcatenatedReadStreamTests
{
    [TestMethod]
    public void Read_MultipleStreams_ReadsSequentially()
    {
        // Arrange
        var stream1 = new MemoryStream(new byte[] { 1, 2, 3 });
        var stream2 = new MemoryStream(new byte[] { 4, 5 });
        var stream3 = new MemoryStream(new byte[] { 6, 7, 8, 9 });
        using var concatenated = new ConcatenatedReadStream(false, stream1, stream2, stream3);

        // Act
        var buffer = new byte[20];
        int totalRead = 0;
        int bytesRead;
        while ((bytesRead = concatenated.Read(buffer, totalRead, buffer.Length - totalRead)) > 0)
        {
            totalRead += bytesRead;
        }

        // Assert
        Assert.AreEqual(9, totalRead);
        CollectionAssert.AreEqual(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, buffer[..9]);
    }

    [TestMethod]
    public async Task ReadAsync_MultipleStreams_ReadsSequentially()
    {
        // Arrange
        var stream1 = new MemoryStream(new byte[] { 10, 20 });
        var stream2 = new MemoryStream(new byte[] { 30, 40, 50 });
        using var concatenated = new ConcatenatedReadStream(false, stream1, stream2);

        // Act
        var buffer = new byte[10];
        int totalRead = 0;
        int bytesRead;
        while ((bytesRead = await concatenated.ReadAsync(buffer, totalRead, buffer.Length - totalRead, CancellationToken.None)) > 0)
        {
            totalRead += bytesRead;
        }

        // Assert
        Assert.AreEqual(5, totalRead);
        CollectionAssert.AreEqual(new byte[] { 10, 20, 30, 40, 50 }, buffer[..5]);
    }

    [TestMethod]
    public void Read_EmptyStreams_ReturnsZero()
    {
        // Arrange
        var stream1 = new MemoryStream(Array.Empty<byte>());
        var stream2 = new MemoryStream(Array.Empty<byte>());
        using var concatenated = new ConcatenatedReadStream(false, stream1, stream2);

        // Act
        var buffer = new byte[10];
        int bytesRead = concatenated.Read(buffer, 0, buffer.Length);

        // Assert
        Assert.AreEqual(0, bytesRead);
    }

    [TestMethod]
    public void Read_NoStreams_ReturnsZero()
    {
        // Arrange
        using var concatenated = new ConcatenatedReadStream(false);

        // Act
        var buffer = new byte[10];
        int bytesRead = concatenated.Read(buffer, 0, buffer.Length);

        // Assert
        Assert.AreEqual(0, bytesRead);
    }

    [TestMethod]
    public void Read_SmallBuffer_ReadsIncrementally()
    {
        // Arrange
        var stream1 = new MemoryStream(new byte[] { 1, 2, 3, 4 });
        var stream2 = new MemoryStream(new byte[] { 5, 6 });
        using var concatenated = new ConcatenatedReadStream(false, stream1, stream2);

        // Act - read 2 bytes at a time
        var all = new byte[6];
        int offset = 0;
        int bytesRead;
        while (offset < all.Length && (bytesRead = concatenated.Read(all, offset, Math.Min(2, all.Length - offset))) > 0)
        {
            offset += bytesRead;
        }

        // Assert
        Assert.AreEqual(6, offset);
        CollectionAssert.AreEqual(new byte[] { 1, 2, 3, 4, 5, 6 }, all);
    }

    [TestMethod]
    public void Length_AllSeekableStreams_ReturnsTotalLength()
    {
        // Arrange
        var stream1 = new MemoryStream(new byte[] { 1, 2, 3 });
        var stream2 = new MemoryStream(new byte[] { 4, 5 });
        using var concatenated = new ConcatenatedReadStream(false, stream1, stream2);

        // Act & Assert
        Assert.AreEqual(5, concatenated.Length);
    }

    [TestMethod]
    public void CanRead_ReturnsTrue()
    {
        using var concatenated = new ConcatenatedReadStream(false);
        Assert.IsTrue(concatenated.CanRead);
    }

    [TestMethod]
    public void CanSeek_ReturnsFalse()
    {
        using var concatenated = new ConcatenatedReadStream(false);
        Assert.IsFalse(concatenated.CanSeek);
    }

    [TestMethod]
    public void CanWrite_ReturnsFalse()
    {
        using var concatenated = new ConcatenatedReadStream(false);
        Assert.IsFalse(concatenated.CanWrite);
    }

    [TestMethod]
    public void Seek_ThrowsNotSupportedException()
    {
        using var concatenated = new ConcatenatedReadStream(false);
        Assert.ThrowsExactly<NotSupportedException>(() => concatenated.Seek(0, SeekOrigin.Begin));
    }

    [TestMethod]
    public void SetLength_ThrowsNotSupportedException()
    {
        using var concatenated = new ConcatenatedReadStream(false);
        Assert.ThrowsExactly<NotSupportedException>(() => concatenated.SetLength(0));
    }

    [TestMethod]
    public void Write_ThrowsNotSupportedException()
    {
        using var concatenated = new ConcatenatedReadStream(false);
        Assert.ThrowsExactly<NotSupportedException>(() => concatenated.Write(new byte[1], 0, 1));
    }

    [TestMethod]
    public void Position_Get_ThrowsNotSupportedException()
    {
        using var concatenated = new ConcatenatedReadStream(false);
        Assert.ThrowsExactly<NotSupportedException>(() => _ = concatenated.Position);
    }

    [TestMethod]
    public void Position_Set_ThrowsNotSupportedException()
    {
        using var concatenated = new ConcatenatedReadStream(false);
        Assert.ThrowsExactly<NotSupportedException>(() => concatenated.Position = 0);
    }

    [TestMethod]
    public void Dispose_DisposesInnerStreams_WhenLeaveOpenIsFalse()
    {
        // Arrange
        var stream1 = new MemoryStream(new byte[] { 1 });
        var stream2 = new MemoryStream(new byte[] { 2 });
        var concatenated = new ConcatenatedReadStream(false, stream1, stream2);

        // Act
        concatenated.Dispose();

        // Assert — disposed MemoryStreams throw ObjectDisposedException on read
        Assert.ThrowsExactly<ObjectDisposedException>(() => stream1.ReadByte());
        Assert.ThrowsExactly<ObjectDisposedException>(() => stream2.ReadByte());
    }

    [TestMethod]
    public void Dispose_LeavesInnerStreamsOpen_WhenLeaveOpenIsTrue()
    {
        // Arrange
        var stream1 = new MemoryStream(new byte[] { 1 });
        var stream2 = new MemoryStream(new byte[] { 2 });
        var concatenated = new ConcatenatedReadStream(true, stream1, stream2);

        // Act
        concatenated.Dispose();

        // Assert — streams should still be usable
        stream1.Position = 0;
        Assert.AreEqual(1, stream1.ReadByte());
        stream2.Position = 0;
        Assert.AreEqual(2, stream2.ReadByte());

        stream1.Dispose();
        stream2.Dispose();
    }

    [TestMethod]
    public void Constructor_NullStreams_ThrowsArgumentNullException()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() => new ConcatenatedReadStream(false, null!));
    }

    [TestMethod]
    public void Flush_DoesNotThrow()
    {
        using var concatenated = new ConcatenatedReadStream(false, new MemoryStream());
        concatenated.Flush(); // should be a no-op
    }

    [TestMethod]
    public void Read_ZeroCount_ReturnsZero()
    {
        var stream1 = new MemoryStream(new byte[] { 1, 2, 3 });
        using var concatenated = new ConcatenatedReadStream(false, stream1);
        var buffer = new byte[10];
        Assert.AreEqual(0, concatenated.Read(buffer, 0, 0));
    }

    [TestMethod]
    public async Task ReadAsync_ZeroCount_ReturnsZero()
    {
        var stream1 = new MemoryStream(new byte[] { 1, 2, 3 });
        using var concatenated = new ConcatenatedReadStream(false, stream1);
        var buffer = new byte[10];
        Assert.AreEqual(0, await concatenated.ReadAsync(buffer, 0, 0, CancellationToken.None));
    }

    [TestMethod]
    public void Length_NoStreams_ReturnsZero()
    {
        using var concatenated = new ConcatenatedReadStream(false);
        Assert.AreEqual(0, concatenated.Length);
    }

    [TestMethod]
    public void Read_Span_MultipleStreams_ReadsSequentially()
    {
        // Arrange
        var stream1 = new MemoryStream(new byte[] { 1, 2, 3 });
        var stream2 = new MemoryStream(new byte[] { 4, 5 });
        var stream3 = new MemoryStream(new byte[] { 6, 7, 8, 9 });
        using var concatenated = new ConcatenatedReadStream(false, stream1, stream2, stream3);

        // Act
        Span<byte> buffer = stackalloc byte[20];
        int totalRead = 0;
        int bytesRead;
        while ((bytesRead = concatenated.Read(buffer[totalRead..])) > 0)
        {
            totalRead += bytesRead;
        }

        // Assert
        Assert.AreEqual(9, totalRead);
        CollectionAssert.AreEqual(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, buffer[..9].ToArray());
    }

    [TestMethod]
    public void Read_Span_EmptyBuffer_ReturnsZero()
    {
        var stream1 = new MemoryStream(new byte[] { 1, 2, 3 });
        using var concatenated = new ConcatenatedReadStream(false, stream1);
        Assert.AreEqual(0, concatenated.Read(Span<byte>.Empty));
    }

    [TestMethod]
    public void Read_Span_EmptyStreams_ReturnsZero()
    {
        var stream1 = new MemoryStream(Array.Empty<byte>());
        var stream2 = new MemoryStream(Array.Empty<byte>());
        using var concatenated = new ConcatenatedReadStream(false, stream1, stream2);
        Span<byte> buffer = stackalloc byte[10];
        Assert.AreEqual(0, concatenated.Read(buffer));
    }

    [TestMethod]
    public void Read_Span_NoStreams_ReturnsZero()
    {
        using var concatenated = new ConcatenatedReadStream(false);
        Span<byte> buffer = stackalloc byte[10];
        Assert.AreEqual(0, concatenated.Read(buffer));
    }

    [TestMethod]
    public void Read_Span_SmallBuffer_ReadsIncrementally()
    {
        var stream1 = new MemoryStream(new byte[] { 1, 2, 3, 4 });
        var stream2 = new MemoryStream(new byte[] { 5, 6 });
        using var concatenated = new ConcatenatedReadStream(false, stream1, stream2);

        Span<byte> all = stackalloc byte[6];
        int offset = 0;
        int bytesRead;
        while (offset < all.Length && (bytesRead = concatenated.Read(all.Slice(offset, Math.Min(2, all.Length - offset)))) > 0)
        {
            offset += bytesRead;
        }

        Assert.AreEqual(6, offset);
        CollectionAssert.AreEqual(new byte[] { 1, 2, 3, 4, 5, 6 }, all.ToArray());
    }

    [TestMethod]
    public void Read_Span_AfterAllStreamsExhausted_ReturnsZero()
    {
        var stream = new MemoryStream(new byte[] { 1 });
        using var concatenated = new ConcatenatedReadStream(false, stream);
        Span<byte> buffer = stackalloc byte[10];
        Assert.AreEqual(1, concatenated.Read(buffer));
        Assert.AreEqual(0, concatenated.Read(buffer));
    }

    [TestMethod]
    public async Task ReadAsync_Memory_MultipleStreams_ReadsSequentially()
    {
        // Arrange
        var stream1 = new MemoryStream(new byte[] { 10, 20 });
        var stream2 = new MemoryStream(new byte[] { 30, 40, 50 });
        using var concatenated = new ConcatenatedReadStream(false, stream1, stream2);

        // Act
        Memory<byte> buffer = new byte[10];
        int totalRead = 0;
        int bytesRead;
        while ((bytesRead = await concatenated.ReadAsync(buffer[totalRead..], CancellationToken.None)) > 0)
        {
            totalRead += bytesRead;
        }

        // Assert
        Assert.AreEqual(5, totalRead);
        CollectionAssert.AreEqual(new byte[] { 10, 20, 30, 40, 50 }, buffer[..5].ToArray());
    }

    [TestMethod]
    public async Task ReadAsync_Memory_EmptyBuffer_ReturnsZero()
    {
        var stream1 = new MemoryStream(new byte[] { 10, 20 });
        using var concatenated = new ConcatenatedReadStream(false, stream1);
        Assert.AreEqual(0, await concatenated.ReadAsync(Memory<byte>.Empty));
    }

    [TestMethod]
    public async Task ReadAsync_Memory_EmptyStreams_ReturnsZero()
    {
        var stream1 = new MemoryStream(Array.Empty<byte>());
        var stream2 = new MemoryStream(Array.Empty<byte>());
        using var concatenated = new ConcatenatedReadStream(false, stream1, stream2);
        Memory<byte> buffer = new byte[10];
        Assert.AreEqual(0, await concatenated.ReadAsync(buffer));
    }

    [TestMethod]
    public async Task ReadAsync_Memory_NoStreams_ReturnsZero()
    {
        using var concatenated = new ConcatenatedReadStream(false);
        Memory<byte> buffer = new byte[10];
        Assert.AreEqual(0, await concatenated.ReadAsync(buffer));
    }

    [TestMethod]
    public async Task ReadAsync_Memory_SmallBuffer_ReadsIncrementally()
    {
        var stream1 = new MemoryStream(new byte[] { 1, 2, 3, 4 });
        var stream2 = new MemoryStream(new byte[] { 5, 6 });
        using var concatenated = new ConcatenatedReadStream(false, stream1, stream2);

        Memory<byte> all = new byte[6];
        int offset = 0;
        int bytesRead;
        while (offset < all.Length && (bytesRead = await concatenated.ReadAsync(all.Slice(offset, Math.Min(2, all.Length - offset)))) > 0)
        {
            offset += bytesRead;
        }

        Assert.AreEqual(6, offset);
        CollectionAssert.AreEqual(new byte[] { 1, 2, 3, 4, 5, 6 }, all.ToArray());
    }

    [TestMethod]
    public async Task ReadAsync_Memory_AfterAllStreamsExhausted_ReturnsZero()
    {
        var stream = new MemoryStream(new byte[] { 1 });
        using var concatenated = new ConcatenatedReadStream(false, stream);
        Memory<byte> buffer = new byte[10];
        Assert.AreEqual(1, await concatenated.ReadAsync(buffer));
        Assert.AreEqual(0, await concatenated.ReadAsync(buffer));
    }

    [TestMethod]
    public void Dispose_CalledMultipleTimes_DoesNotThrow()
    {
        var stream = new MemoryStream(new byte[] { 1 });
        var concatenated = new ConcatenatedReadStream(false, stream);
        concatenated.Dispose();
        concatenated.Dispose(); // second call when _disposed is already true
    }

    [TestMethod]
    public void Dispose_WithPerStreamLeaveOpen_DisposesOnlyNonLeaveOpenStreams()
    {
        // Arrange
        var stream1 = new MemoryStream(new byte[] { 1 });
        var stream2 = new MemoryStream(new byte[] { 2 });
        var concatenated = new ConcatenatedReadStream((stream1, false), (stream2, true));

        // Act
        concatenated.Dispose();

        // Assert — stream1 was leaveOpen: false so it should be disposed
        Assert.ThrowsExactly<ObjectDisposedException>(() => stream1.ReadByte());

        // stream2 was leaveOpen: true so it should remain open
        stream2.Position = 0;
        Assert.AreEqual(2, stream2.ReadByte());

        stream2.Dispose();
    }

    [TestMethod]
    public void Constructor_NullPerStreamArray_ThrowsArgumentNullException()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() => new ConcatenatedReadStream(((Stream, bool)[])null!));
    }

    [TestMethod]
    public void Read_WithPerStreamConstructor_ReadsSequentially()
    {
        var stream1 = new MemoryStream(new byte[] { 1, 2 });
        var stream2 = new MemoryStream(new byte[] { 3, 4 });
        using var concatenated = new ConcatenatedReadStream((stream1, false), (stream2, false));

        var buffer = new byte[10];
        int bytesRead = concatenated.Read(buffer, 0, buffer.Length);

        Assert.AreEqual(4, bytesRead);
        CollectionAssert.AreEqual(new byte[] { 1, 2, 3, 4 }, buffer[..4]);
    }
}
