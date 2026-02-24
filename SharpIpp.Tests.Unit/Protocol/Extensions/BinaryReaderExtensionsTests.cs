using SharpIpp.Protocol.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Tests.Unit.Protocol.Extensions;
[TestClass]
[ExcludeFromCodeCoverage]
public class BinaryReaderExtensionsTests
{
    [TestMethod]
    public void ReadInt16BigEndian_ShouldReadCorrectValue()
    {
        // 0x1234 in big endian is [0x12, 0x34]
        var bytes = new byte[] { 0x12, 0x34 };
        using var ms = new MemoryStream(bytes);
        using var reader = new BinaryReader(ms);

        var result = reader.ReadInt16BigEndian();

        result.Should().Be(0x1234);
    }

    [TestMethod]
    public void ReadInt32BigEndian_ShouldReadCorrectValue()
    {
        // 0x12345678 in big endian is [0x12, 0x34, 0x56, 0x78]
        var bytes = new byte[] { 0x12, 0x34, 0x56, 0x78 };
        using var ms = new MemoryStream(bytes);
        using var reader = new BinaryReader(ms);

        var result = reader.ReadInt32BigEndian();

        result.Should().Be(0x12345678);
    }
}
