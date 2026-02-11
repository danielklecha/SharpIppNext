using SharpIpp.Protocol.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace SharpIppTests.Protocol.Extensions
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BinaryWriterExtensionsTests
    {
        [TestMethod]
        public void WriteBigEndian_Int16_ShouldWriteCorrectBytes()
        {
            var value = (short)0x1234;
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);

            writer.WriteBigEndian(value);

            ms.ToArray().Should().Equal(new byte[] { 0x12, 0x34 });
        }

        [TestMethod]
        public void WriteBigEndian_Int32_ShouldWriteCorrectBytes()
        {
            var value = 0x12345678;
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);

            writer.WriteBigEndian(value);

            ms.ToArray().Should().Equal(new byte[] { 0x12, 0x34, 0x56, 0x78 });
        }
    }
}
