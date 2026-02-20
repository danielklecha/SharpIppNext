using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Models;
using System;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Protocol.Models.Tests;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppAttributeTests
{
    [TestMethod]
    public void Constructor_IntMinValue_SetsNoValue()
    {
        var attr = new IppAttribute(Tag.Integer, "test", int.MinValue);
        attr.Tag.Should().Be(Tag.NoValue);
        attr.Value.Should().Be(NoValue.Instance);
    }

    [TestMethod]
    public void Constructor_DefaultDateTimeOffset_SetsNoValue()
    {
        var attr = new IppAttribute(Tag.DateTime, "test", default(DateTimeOffset));
        attr.Tag.Should().Be(Tag.NoValue);
        attr.Value.Should().Be(NoValue.Instance);
    }

    [TestMethod]
    public void Constructor_DefaultRange_SetsNoValue()
    {
        var attr = new IppAttribute(Tag.RangeOfInteger, "test", default(SharpIpp.Protocol.Models.Range));
        attr.Tag.Should().Be(Tag.NoValue);
        attr.Value.Should().Be(NoValue.Instance);
    }

    [TestMethod]
    public void Constructor_DefaultResolution_SetsNoValue()
    {
        var attr = new IppAttribute(Tag.Resolution, "test", default(Resolution));
        attr.Tag.Should().Be(Tag.NoValue);
        attr.Value.Should().Be(NoValue.Instance);
    }

    [TestMethod]
    public void Constructor_StringWithLanguage_SetsNoValue()
    {
        var attr = new IppAttribute(Tag.Resolution, "test", default(StringWithLanguage));
        attr.Tag.Should().Be(Tag.NoValue);
        attr.Value.Should().Be(NoValue.Instance);
    }

    [TestMethod]
    public void Constructor_EmptyStringAndKeywordTag_SetsNoValue()
    {
        var attr = new IppAttribute(Tag.Keyword, "test", string.Empty);
        attr.Tag.Should().Be(Tag.NoValue);
        attr.Value.Should().Be(NoValue.Instance);
    }

    [TestMethod]
    public void Constructor_ValidString_SetsValue()
    {
        var attr = new IppAttribute(Tag.Keyword, "test", "valid");
        attr.Tag.Should().Be(Tag.Keyword);
        attr.Value.Should().Be("valid");
    }

    [TestMethod]
    public void Constructor_ValidInt_SetsValue()
    {
        var attr = new IppAttribute(Tag.Integer, "test", 1);
        attr.Tag.Should().Be(Tag.Integer);
        attr.Value.Should().Be(1);
    }

    [TestMethod]
    public void Constructor_Parameterless_SetsNoValueAndEmptyName()
    {
        var attr = new IppAttribute();
        attr.Tag.Should().Be(Tag.NoValue);
        attr.Name.Should().Be(string.Empty);
        attr.Value.Should().Be(NoValue.Instance);
    }

    [TestMethod]
    public void Constructor_NullName_ThrowsArgumentNullException()
    {
        Action action = () => new IppAttribute(Tag.Integer, null!, 1);
        action.Should().Throw<ArgumentNullException>().WithParameterName("name");
    }

    [TestMethod]
    public void Constructor_NullValue_ThrowsArgumentNullException()
    {
        Action action = () => new IppAttribute(Tag.Keyword, "test", null!);
        action.Should().Throw<ArgumentNullException>().WithParameterName("value");
    }

    [TestMethod]
    public void Equals_SameAttributes_ReturnsTrue()
    {
        var attr1 = new IppAttribute(Tag.Integer, "test", 1);
        var attr2 = new IppAttribute(Tag.Integer, "test", 1);
        attr1.Equals(attr2).Should().BeTrue();
    }

    [TestMethod]
    public void Equals_DifferentAttributes_ReturnsFalse()
    {
        var attr1 = new IppAttribute(Tag.Integer, "test", 1);
        var attr2 = new IppAttribute(Tag.Integer, "test", 2);
        attr1.Equals(attr2).Should().BeFalse();
    }

    [TestMethod]
    public void EqualsObject_SameAttributes_ReturnsTrue()
    {
        var attr1 = new IppAttribute(Tag.Integer, "test", 1);
        object attr2 = new IppAttribute(Tag.Integer, "test", 1);
        attr1.Equals(attr2).Should().BeTrue();
    }

    [TestMethod]
    public void EqualsObject_DifferentType_ReturnsFalse()
    {
        var attr1 = new IppAttribute(Tag.Integer, "test", 1);
        attr1.Equals(new object()).Should().BeFalse();
    }

    [TestMethod]
    public void EqualsObject_Null_ReturnsFalse()
    {
        var attr1 = new IppAttribute(Tag.Integer, "test", 1);
        attr1.Equals(null).Should().BeFalse();
    }

    [TestMethod]
    public void GetHashCode_SameAttributes_ReturnsSameHashCode()
    {
        var attr1 = new IppAttribute(Tag.Integer, "test", 1);
        var attr2 = new IppAttribute(Tag.Integer, "test", 1);
        attr1.GetHashCode().Should().Be(attr2.GetHashCode());
    }

    [TestMethod]
    public void ToString_ReturnsFormattedString()
    {
        var attr = new IppAttribute(Tag.Integer, "test", 1);
        attr.ToString().Should().Be("(Integer) test: 1");
    }
}
