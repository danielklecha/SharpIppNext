using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Protocol.Models;

[TestClass]
[ExcludeFromCodeCoverage]
public class OctetStringTests
{
    [TestMethod]
    public void ImplicitConversionToByteArray_ShouldReturnUnderlyingValue()
    {
        // Arrange
        var bytes = new byte[] { 1, 2, 3 };
        var octetString = new OctetString(bytes);

        // Act
        byte[]? result = octetString;

        // Assert
        result.Should().BeSameAs(bytes);
    }

    [TestMethod]
    public void Constructor_WithIsValueFalse_ShouldHaveIsValueFalse()
    {
        // Arrange
        var bytes = new byte[] { 1, 2, 3 };
        var octetString = new OctetString(bytes, false);

        // Assert
        octetString.IsValue.Should().BeFalse();
        octetString.Value.Should().BeSameAs(bytes);
    }

    [TestMethod]
    public void Constructor_WithNullValue_ShouldHaveIsValueFalse()
    {
        // Arrange
        var octetString = new OctetString(null!, true);

        // Assert
        octetString.IsValue.Should().BeFalse();
        octetString.Value.Should().BeNull();
    }

    [TestMethod]
    public void ToString_WhenValueIsNull_ShouldReturnEmptyString()
    {
        // Arrange
        var octetString = new OctetString((byte[]?)null!);

        // Act
        var result = octetString.ToString();

        // Assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void ToString_WhenValueIsNotNull_ShouldReturnString()
    {
        // Arrange
        var value = "test-value";
        var octetString = new OctetString(value);

        // Act
        var result = octetString.ToString();

        // Assert
        result.Should().Be(value);
    }

    [TestMethod]
    public void Equals_WhenOneValueIsNull_ShouldReturnTrueOnlyIfBothAreNull()
    {
        // Arrange
        var nullOctetString = new OctetString((byte[]?)null!);
        var nonNullOctetString = new OctetString(new byte[] { 1 });

        // Act & Assert
        nullOctetString.Equals(nullOctetString).Should().BeTrue("null == null");
        nullOctetString.Equals(nonNullOctetString).Should().BeFalse("null == non-null");
        nonNullOctetString.Equals(nullOctetString).Should().BeFalse("non-null == null");
    }

    [TestMethod]
    public void EqualsObject_WithOctetString_ShouldReturnTrueIfEqual()
    {
        // Arrange
        var a = new OctetString("test");
        object b = new OctetString("test");
        object c = new OctetString("other");

        // Act & Assert
        a.Equals(b).Should().BeTrue();
        a.Equals(c).Should().BeFalse();
    }

    [TestMethod]
    public void EqualsObject_WithDifferentObjectType_ShouldReturnFalse()
    {
        // Arrange
        var octetString = new OctetString("test");
        var other = new object();

        // Act
        var result = octetString.Equals(other);

        // Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void GetHashCode_WhenValueIsNull_ShouldReturnZero()
    {
        // Arrange
        var octetString = new OctetString((byte[]?)null!);

        // Act
        var result = octetString.GetHashCode();

        // Assert
        result.Should().Be(0);
    }

    [TestMethod]
    public void GetHashCode_WhenValueIsNotNull_ShouldReturnNonZero()
    {
        // Arrange
        var octetString = new OctetString("test");

        // Act
        var result = octetString.GetHashCode();

        // Assert
        result.Should().NotBe(0);
    }

    [TestMethod]
    public void EqualityOperator_ShouldWorkCorrectly()
    {
        // Arrange
        var a = new OctetString("test");
        var b = new OctetString("test");
        var c = new OctetString("other");

        // Act & Assert
        (a == b).Should().BeTrue("a == b");
        (a == c).Should().BeFalse("a == c");
    }

    [TestMethod]
    public void InequalityOperator_ShouldWorkCorrectly()
    {
        // Arrange
        var a = new OctetString("test");
        var b = new OctetString("test");
        var c = new OctetString("other");

        // Act & Assert
        (a != b).Should().BeFalse("a != b");
        (a != c).Should().BeTrue("a != c");
    }
}
