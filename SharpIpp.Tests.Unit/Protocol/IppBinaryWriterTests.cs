using SharpIpp.Protocol.Models;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace SharpIpp.Tests.Unit.Protocol.Models;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppBinaryWriterTests
{
    [TestMethod]
    public void Constructor_WithNullStream_ShouldThrowArgumentNullException()
    {
        var action = () => new IppBinaryWriter(null!);
        action.Should().Throw<ArgumentNullException>().WithParameterName("stream");
    }

    [TestMethod]
    public void BaseStream_ShouldReturnConstructorStream()
    {
        using var ms = new MemoryStream();
        using var writer = new IppBinaryWriter(ms);
        writer.BaseStream.Should().BeSameAs(ms);
    }

    [TestMethod]
    public void Dispose_ShouldNotThrow()
    {
        using var ms = new MemoryStream();
        var writer = new IppBinaryWriter(ms);
        var action = () => writer.Dispose();
        action.Should().NotThrow();
    }

    [TestMethod]
    public async Task WriteInt16BigEndianAsync_ShouldWriteCorrectValue()
    {
        using var ms = new MemoryStream();
        using var writer = new IppBinaryWriter(ms);

        await writer.WriteBigEndianAsync((short)0x1234);

        ms.ToArray().Should().Equal(new byte[] { 0x12, 0x34 });
    }

    [TestMethod]
    public async Task WriteInt32BigEndianAsync_ShouldWriteCorrectValue()
    {
        using var ms = new MemoryStream();
        using var writer = new IppBinaryWriter(ms);

        await writer.WriteBigEndianAsync(0x12345678);

        ms.ToArray().Should().Equal(new byte[] { 0x12, 0x34, 0x56, 0x78 });
    }

    [TestMethod]
    public async Task WriteAsync_WithByteArray_ShouldWriteCorrectValue()
    {
        using var ms = new MemoryStream();
        using var writer = new IppBinaryWriter(ms);
        var bytes = new byte[] { 0x01, 0x02, 0x03 };
        await writer.WriteAsync(bytes);
        ms.ToArray().Should().Equal(bytes);
    }

    [TestMethod]
    public async Task WriteAsync_WithNullByteArray_ShouldThrowArgumentNullException()
    {
        using var ms = new MemoryStream();
        using var writer = new IppBinaryWriter(ms);
        var action = async () => await writer.WriteAsync((byte[])null!);
        await action.Should().ThrowAsync<ArgumentNullException>().WithParameterName("buffer");
    }

    [TestMethod]
    public async Task WriteAsync_WithByte_ShouldWriteCorrectValue()
    {
        using var ms = new MemoryStream();
        using var writer = new IppBinaryWriter(ms);
        await writer.WriteAsync((byte)0x42);
        ms.ToArray().Should().Equal(new byte[] { 0x42 });
    }
}