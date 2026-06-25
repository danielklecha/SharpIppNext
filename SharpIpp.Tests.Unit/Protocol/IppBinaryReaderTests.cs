using SharpIpp.Protocol.Models;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace SharpIpp.Tests.Unit.Protocol.Models;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppBinaryReaderTests
{
    [TestMethod]
    public async Task ReadInt16BigEndianAsync_ShouldReadCorrectValue()
    {
        var bytes = new byte[] { 0x12, 0x34 };
        using var ms = new MemoryStream(bytes);
        using var reader = new IppBinaryReader(ms);

        var result = await reader.ReadInt16BigEndianAsync();

        result.Should().Be(0x1234);
    }

    [TestMethod]
    public async Task ReadInt32BigEndianAsync_ShouldReadCorrectValue()
    {
        var bytes = new byte[] { 0x12, 0x34, 0x56, 0x78 };
        using var ms = new MemoryStream(bytes);
        using var reader = new IppBinaryReader(ms);

        var result = await reader.ReadInt32BigEndianAsync();

        result.Should().Be(0x12345678);
    }

    [TestMethod]
    public async Task ReadBytesAsync_WithNegativeCount_ShouldThrowArgumentOutOfRangeException()
    {
        using var ms = new MemoryStream();
        using var reader = new IppBinaryReader(ms);

        var action = async () => await reader.ReadBytesAsync(-1);

        await action.Should().ThrowAsync<ArgumentOutOfRangeException>();
    }

    [TestMethod]
    public async Task ReadBytesAsync_WithZeroCount_ShouldReturnEmptyArray()
    {
        using var ms = new MemoryStream();
        using var reader = new IppBinaryReader(ms);

        var result = await reader.ReadBytesAsync(0);

        result.Should().BeEmpty();
    }

    [TestMethod]
    public void Constructor_WithNullStream_ShouldThrowArgumentNullException()
    {
        var action = () => new IppBinaryReader(null!);

        action.Should().Throw<System.ArgumentNullException>();
    }
}