using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Protocol.Models;

[TestClass]
[ExcludeFromCodeCoverage]
public class OctetStringTests
{
    [TestMethod]
    public void Constructor_WithBytes_SetsPropertiesCorrectly()
    {
        var bytes = new byte[] { 1, 2, 3 };
        var octetString = new OctetString(bytes);

        octetString.IsValue.Should().BeTrue();
        octetString.Value.Should().BeSameAs(bytes);
        octetString.ToString().Should().Be(Encoding.UTF8.GetString(bytes));
    }

    [TestMethod]
    public void Constructor_WithString_SetsPropertiesCorrectly()
    {
        var value = "hello-world";
        var octetString = new OctetString(value);

        octetString.IsValue.Should().BeTrue();
        octetString.Value.Should().Equal(Encoding.UTF8.GetBytes(value));
        octetString.ToString().Should().Be(value);
    }

    [TestMethod]
    public void Default_RepresentsNoValue()
    {
        var octetString = default(OctetString);

        octetString.IsValue.Should().BeFalse();
        octetString.Value.Should().BeNull();
        octetString.ToString().Should().Be("no value");
    }

    [TestMethod]
    public void ImplicitConversion_FromByteArray_ProducesExpectedState()
    {
        byte[]? nonNullBytes = [1, 2, 3];
        OctetString fromNonNull = nonNullBytes;
        fromNonNull.IsValue.Should().BeTrue();
        fromNonNull.Value.Should().Equal(nonNullBytes);

        byte[]? nullBytes = null;
        OctetString fromNull = nullBytes;
        fromNull.IsValue.Should().BeFalse();
        fromNull.Value.Should().BeNull();
    }

    [TestMethod]
    public void ImplicitConversion_FromString_ProducesExpectedState()
    {
        string? nonNullStr = "test-string";
        OctetString fromNonNull = nonNullStr;
        fromNonNull.IsValue.Should().BeTrue();
        fromNonNull.ToString().Should().Be(nonNullStr);

        string? nullStr = null;
        OctetString fromNull = nullStr;
        fromNull.IsValue.Should().BeFalse();
        fromNull.Value.Should().BeNull();
    }

    [TestMethod]
    public void ImplicitConversion_FromNoValue_ProducesNoValueState()
    {
        OctetString octetString = NoValue.Instance;

        octetString.IsValue.Should().BeFalse();
        octetString.Value.Should().BeNull();
        octetString.ToString().Should().Be("no value");
    }

    [TestMethod]
    public void ImplicitConversion_ToByteArray_ReturnsExpectedValue()
    {
        var bytes = new byte[] { 1, 2, 3 };
        var concrete = new OctetString(bytes);
        byte[]? result = concrete;
        result.Should().BeSameAs(bytes);

        var noVal = default(OctetString);
        byte[]? noValResult = noVal;
        noValResult.Should().BeNull();
    }

    [TestMethod]
    public void ExplicitConversion_ToString_ReturnsExpectedValue()
    {
        var concrete = new OctetString("hello");
        var strResult = (string?)concrete;
        strResult.Should().Be("hello");

        var noVal = default(OctetString);
        var noValStrResult = (string?)noVal;
        noValStrResult.Should().BeNull();
    }

    [TestMethod]
    public void NullableOctetString_ThreeStates_BehaveCorrectly()
    {
        // 1. null — unset/absent
        OctetString? unset = null;
        unset.HasValue.Should().BeFalse();

        // 2. default / NoValue — present with Tag.NoValue
        OctetString? noVal = NoValue.Instance;
        noVal.HasValue.Should().BeTrue();
        noVal.Value.IsValue.Should().BeFalse();
        ((byte[]?)noVal.Value).Should().BeNull();
        ((string?)noVal.Value).Should().BeNull();
        noVal.Value.Should().Be(default(OctetString));

        // 3. Concrete value
        OctetString? concrete = (OctetString)"my-secret";
        concrete.HasValue.Should().BeTrue();
        concrete.Value.IsValue.Should().BeTrue();
        concrete.Value.ToString().Should().Be("my-secret");
    }

    [TestMethod]
    public void ToString_WhenValueIsNull_ShouldReturnEmptyString()
    {
        var octetString = new OctetString((byte[]?)null!);
        octetString.ToString().Should().BeEmpty();
    }

    [TestMethod]
    public void ToString_WhenNoValue_ShouldReturnNoValueString()
    {
        default(OctetString).ToString().Should().Be("no value");
        ((OctetString)NoValue.Instance).ToString().Should().Be("no value");
    }

    [TestMethod]
    public void Equals_OctetString_Scenarios()
    {
        var concrete1 = new OctetString("test");
        var concrete2 = new OctetString("test");
        var otherConcrete = new OctetString("other");
        var noVal1 = default(OctetString);
        var noVal2 = (OctetString)NoValue.Instance;
        var nullBytes1 = new OctetString((byte[]?)null!);
        var nullBytes2 = new OctetString((byte[]?)null!);

        // Concrete == Concrete
        concrete1.Equals(concrete2).Should().BeTrue();
        concrete1.Equals(otherConcrete).Should().BeFalse();

        // Concrete vs NoValue
        concrete1.Equals(noVal1).Should().BeFalse();
        noVal1.Equals(concrete1).Should().BeFalse();

        // NoValue == NoValue
        noVal1.Equals(noVal2).Should().BeTrue();

        // Null value bytes
        nullBytes1.Equals(nullBytes2).Should().BeTrue();
        nullBytes1.Equals(concrete1).Should().BeFalse();
        concrete1.Equals(nullBytes1).Should().BeFalse();
    }

    [TestMethod]
    public void Equals_ByteArray_Scenarios()
    {
        var bytes = Encoding.UTF8.GetBytes("test");
        var otherBytes = Encoding.UTF8.GetBytes("other");
        var concrete = new OctetString(bytes);
        var nullVal = new OctetString((byte[]?)null!);
        var noVal = default(OctetString);

        // When NoValue
        noVal.Equals((byte[]?)null).Should().BeTrue();
        noVal.Equals(bytes).Should().BeFalse();

        // When Concrete with null Value
        nullVal.Equals((byte[]?)null).Should().BeTrue();
        nullVal.Equals(bytes).Should().BeFalse();

        // When Concrete with non-null Value
        concrete.Equals((byte[]?)null).Should().BeFalse();
        concrete.Equals(bytes).Should().BeTrue();
        concrete.Equals(otherBytes).Should().BeFalse();
    }

    [TestMethod]
    public void Equals_String_Scenarios()
    {
        var concrete = new OctetString("test");
        var noVal = default(OctetString);

        // When NoValue
        noVal.Equals((string?)null).Should().BeTrue();
        noVal.Equals("test").Should().BeFalse();

        // When Concrete
        concrete.Equals((string?)null).Should().BeFalse();
        concrete.Equals("test").Should().BeTrue();
        concrete.Equals("other").Should().BeFalse();
    }

    [TestMethod]
    public void Equals_NoValue_Scenarios()
    {
        var concrete = new OctetString("test");
        var noVal = default(OctetString);

        concrete.Equals(NoValue.Instance).Should().BeFalse();
        noVal.Equals(NoValue.Instance).Should().BeTrue();
    }

    [TestMethod]
    public void Equals_Object_Scenarios()
    {
        var concrete = new OctetString("test");
        var noVal = default(OctetString);

        // With OctetString
        concrete.Equals((object)new OctetString("test")).Should().BeTrue();
        concrete.Equals((object)new OctetString("other")).Should().BeFalse();
        concrete.Equals((object)default(OctetString)).Should().BeFalse();
        noVal.Equals((object)default(OctetString)).Should().BeTrue();

        // With byte[]
        concrete.Equals((object)Encoding.UTF8.GetBytes("test")).Should().BeTrue();
        concrete.Equals((object)Encoding.UTF8.GetBytes("other")).Should().BeFalse();
        noVal.Equals((object)Encoding.UTF8.GetBytes("test")).Should().BeFalse();

        // With string
        concrete.Equals((object)"test").Should().BeTrue();
        concrete.Equals((object)"other").Should().BeFalse();
        noVal.Equals((object)"test").Should().BeFalse();

        // With INoValue
        var customNoValueFalse = new CustomNoValue(isValue: false);
        var customNoValueTrue = new CustomNoValue(isValue: true);

        concrete.Equals((object)customNoValueFalse).Should().BeFalse();
        concrete.Equals((object)customNoValueTrue).Should().BeFalse();
        noVal.Equals((object)customNoValueFalse).Should().BeTrue();
        noVal.Equals((object)customNoValueTrue).Should().BeFalse();

        // With unrelated object / null
        concrete.Equals((object?)null).Should().BeFalse();
        concrete.Equals(new object()).Should().BeFalse();
        noVal.Equals((object?)null).Should().BeFalse();
        noVal.Equals(new object()).Should().BeFalse();
    }

    [TestMethod]
    public void GetHashCode_Scenarios()
    {
        // When NoValue
        default(OctetString).GetHashCode().Should().Be(0);

        // When null value
        new OctetString((byte[]?)null!).GetHashCode().Should().Be(0);

        // When non-null value <= 32 bytes
        var shortOctet = new OctetString("short-value");
        shortOctet.GetHashCode().Should().NotBe(0);
        shortOctet.GetHashCode().Should().Be(new OctetString("short-value").GetHashCode());

        // When non-null value > 32 bytes (tests loop constraint)
        var longBytes = new byte[64];
        for (int i = 0; i < longBytes.Length; i++) longBytes[i] = (byte)i;
        var longOctet = new OctetString(longBytes);
        longOctet.GetHashCode().Should().NotBe(0);
    }

    [TestMethod]
    public void EqualityOperators_OctetString_WorkCorrectly()
    {
        var a = new OctetString("test");
        var b = new OctetString("test");
        var c = new OctetString("other");

        (a == b).Should().BeTrue();
        (a == c).Should().BeFalse();
        (a != b).Should().BeFalse();
        (a != c).Should().BeTrue();
    }

    [TestMethod]
    public void EqualityOperators_NoValue_WorkCorrectly()
    {
        var concrete = new OctetString("test");
        var noVal = default(OctetString);

        // OctetString == NoValue
        (concrete == NoValue.Instance).Should().BeFalse();
        (concrete != NoValue.Instance).Should().BeTrue();
        (noVal == NoValue.Instance).Should().BeTrue();
        (noVal != NoValue.Instance).Should().BeFalse();

        // NoValue == OctetString
        (NoValue.Instance == concrete).Should().BeFalse();
        (NoValue.Instance != concrete).Should().BeTrue();
        (NoValue.Instance == noVal).Should().BeTrue();
        (NoValue.Instance != noVal).Should().BeFalse();
    }

    private sealed class CustomNoValue(bool isValue) : INoValue
    {
        public bool IsValue => isValue;
    }
}
