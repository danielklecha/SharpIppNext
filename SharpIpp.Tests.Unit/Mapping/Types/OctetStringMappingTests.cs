using System.Diagnostics.CodeAnalysis;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class OctetStringProfileTests : MapperTestBase
{
    [TestMethod]
    public void Map_OctetString_To_OctetString_Should_Work()
    {
        var source = new OctetString("test");
        var result = _mapper.Map<OctetString, OctetString>(source);
        result.Should().Be(source);
    }

    [TestMethod]
    public void Map_OctetString_To_ByteArray_Should_Work()
    {
        var bytes = new byte[] { 1, 2, 3 };
        var source = new OctetString(bytes);
        var result = _mapper.Map<OctetString, byte[]>(source);
        result.Should().BeEquivalentTo(bytes);
    }

    [TestMethod]
    public void Map_ByteArray_To_OctetString_Should_Work()
    {
        var bytes = new byte[] { 1, 2, 3 };
        var result = _mapper.Map<byte[], OctetString>(bytes);
        result.Value.Should().BeEquivalentTo(bytes);
    }

    [TestMethod]
    public void Map_OctetString_To_String_Should_Work()
    {
        var text = "test string";
        var source = new OctetString(text);
        var result = _mapper.Map<OctetString, string>(source);
        result.Should().Be(text);
    }

    [TestMethod]
    public void Map_String_To_OctetString_Should_Work()
    {
        var text = "test string";
        var result = _mapper.Map<string, OctetString>(text);
        result.ToString().Should().Be(text);
        result.Value.Should().BeEquivalentTo(Encoding.UTF8.GetBytes(text));
    }

    [TestMethod]
    public void Map_NoValue_To_OctetString_Should_Return_NoValue()
    {
        var source = NoValue.Instance;
        var result = _mapper.Map<NoValue, OctetString>(source);
        result.IsValue.Should().BeFalse();
        result.Value.Should().BeNull();
    }
}
