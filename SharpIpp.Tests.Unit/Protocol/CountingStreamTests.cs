using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SharpIpp.Tests.Unit.Protocol;

[TestClass]
[ExcludeFromCodeCoverage]
public class CountingStreamTests
{
    [TestMethod]
    public void Constructor_NullStream_ShouldThrowArgumentNullException()
    {
        Action act = () => new CountingStream(null!);
        act.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void PassThroughProperties_ShouldReturnInnerStreamValues()
    {
        using var ms = new MemoryStream(new byte[] { 1, 2, 3 });
        using var countingStream = new CountingStream(ms);

        countingStream.CanRead.Should().Be(ms.CanRead);
        countingStream.CanSeek.Should().Be(ms.CanSeek);
        countingStream.CanWrite.Should().Be(ms.CanWrite);
        countingStream.Length.Should().Be(ms.Length);
        
        countingStream.Position = 1;
        countingStream.Position.Should().Be(1);
        ms.Position.Should().Be(1);
    }

    [TestMethod]
    public void Flush_ShouldInvokeInnerStreamFlush()
    {
        using var ms = new MemoryStream();
        using var countingStream = new CountingStream(ms);
        countingStream.Flush(); // should not throw
    }

    [TestMethod]
    public void SeekAndSetLength_ShouldInvokeInnerStreamMethods()
    {
        using var ms = new MemoryStream();
        using var countingStream = new CountingStream(ms);
        
        countingStream.SetLength(10);
        ms.Length.Should().Be(10);

        countingStream.Seek(5, SeekOrigin.Begin);
        ms.Position.Should().Be(5);
    }

    [TestMethod]
    public void Write_ShouldWriteToInnerStreamWithoutIncrementingBytesRead()
    {
        using var ms = new MemoryStream();
        using var countingStream = new CountingStream(ms);

        countingStream.Write(new byte[] { 1, 2, 3 }, 0, 3);
        countingStream.BytesRead.Should().Be(0);
        ms.ToArray().Should().Equal(1, 2, 3);
    }

    [TestMethod]
    public void Read_ShouldIncrementBytesRead()
    {
        using var ms = new MemoryStream(new byte[] { 10, 20, 30 });
        using var countingStream = new CountingStream(ms);
        byte[] buffer = new byte[3];

        int read = countingStream.Read(buffer, 0, 2);
        read.Should().Be(2);
        countingStream.BytesRead.Should().Be(2);
        buffer[0].Should().Be(10);
        buffer[1].Should().Be(20);
    }

    [TestMethod]
    public async Task ReadAsync_ShouldIncrementBytesRead()
    {
        using var ms = new MemoryStream(new byte[] { 10, 20, 30 });
        using var countingStream = new CountingStream(ms);
        byte[] buffer = new byte[3];

        int read = await countingStream.ReadAsync(buffer, 0, 2, CancellationToken.None);
        read.Should().Be(2);
        countingStream.BytesRead.Should().Be(2);
    }

    [TestMethod]
    public void ReadByte_ShouldIncrementBytesRead()
    {
        using var ms = new MemoryStream(new byte[] { 10, 20 });
        using var countingStream = new CountingStream(ms);

        countingStream.ReadByte().Should().Be(10);
        countingStream.BytesRead.Should().Be(1);

        countingStream.ReadByte().Should().Be(20);
        countingStream.BytesRead.Should().Be(2);

        countingStream.ReadByte().Should().Be(-1);
        countingStream.BytesRead.Should().Be(2); // Should not increment on EOF
    }

#if !NETSTANDARD2_0
    [TestMethod]
    public void ReadSpan_ShouldIncrementBytesRead()
    {
        using var ms = new MemoryStream(new byte[] { 10, 20, 30 });
        using var countingStream = new CountingStream(ms);
        Span<byte> buffer = stackalloc byte[3];

        int read = countingStream.Read(buffer);
        read.Should().Be(3);
        countingStream.BytesRead.Should().Be(3);
        buffer[0].Should().Be(10);
        buffer[1].Should().Be(20);
        buffer[2].Should().Be(30);
    }

    [TestMethod]
    public async Task ReadAsyncMemory_ShouldIncrementBytesRead()
    {
        using var ms = new MemoryStream(new byte[] { 10, 20, 30 });
        using var countingStream = new CountingStream(ms);
        Memory<byte> buffer = new byte[3];

        int read = await countingStream.ReadAsync(buffer, CancellationToken.None);
        read.Should().Be(3);
        countingStream.BytesRead.Should().Be(3);
        buffer.Span[0].Should().Be(10);
        buffer.Span[1].Should().Be(20);
        buffer.Span[2].Should().Be(30);
    }
#endif
}
