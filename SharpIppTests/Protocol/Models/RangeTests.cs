using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using Range = SharpIpp.Protocol.Models.Range;

namespace SharpIppTests.Protocol.Models;

[TestClass]
[ExcludeFromCodeCoverage]
public class RangeTests
{
    [TestMethod]
    public void Constructor_ShouldSetProperties()
    {
        var range = new Range(1, 10);
        range.Lower.Should().Be(1);
        range.Upper.Should().Be(10);
    }

    [TestMethod]
    public void ToString_ShouldReturnExpectedFormat()
    {
        var range = new Range(5, 15);
        range.ToString().Should().Be("5 - 15");
    }

    [TestMethod]
    public void Deconstruct_ShouldReturnLowerAndUpper()
    {
        var range = new Range(3, 7);
        var (lower, upper) = range;

        lower.Should().Be(3);
        upper.Should().Be(7);
    }

    [TestMethod]
    public void Equals_WithSameRange_ShouldReturnTrue()
    {
        var range1 = new Range(2, 5);
        var range2 = new Range(2, 5);

        range1.Equals(range2).Should().BeTrue();
    }

    [TestMethod]
    public void Equals_WithDifferentRange_ShouldReturnFalse()
    {
        var range1 = new Range(2, 5);
        var range2 = new Range(3, 5);

        range1.Equals(range2).Should().BeFalse();
    }

    [TestMethod]
    public void Equals_WithObjectOfSameRange_ShouldReturnTrue()
    {
        var range = new Range(1, 2);
        object obj = new Range(1, 2);

        range.Equals(obj).Should().BeTrue();
    }

    [TestMethod]
    public void Equals_WithObjectOfDifferentRange_ShouldReturnFalse()
    {
        var range = new Range(1, 2);
        object obj = new Range(2, 3);

        range.Equals(obj).Should().BeFalse();
    }

    [TestMethod]
    public void Equals_WithDifferentObjectType_ShouldReturnFalse()
    {
        var range = new Range(1, 2);
        object obj = new object();

        range.Equals(obj).Should().BeFalse();
    }

    [TestMethod]
    public void Equals_WithNullObject_ShouldReturnFalse()
    {
        var range = new Range(1, 2);
        object? obj = null;

        range.Equals(obj!).Should().BeFalse();
    }

    [TestMethod]
    public void GetHashCode_SameRange_ShouldBeEqual()
    {
        var range1 = new Range(4, 9);
        var range2 = new Range(4, 9);

        range1.GetHashCode().Should().Be(range2.GetHashCode());
    }

    [TestMethod]
    public void GetHashCode_DifferentRange_ShouldNotBeEqual()
    {
        var range1 = new Range(4, 9);
        var range2 = new Range(9, 4);

        range1.GetHashCode().Should().NotBe(range2.GetHashCode());
    }

    [TestMethod]
    public void OperatorEquality_SameRange_ShouldReturnTrue()
    {
        var range1 = new Range(2, 8);
        var range2 = new Range(2, 8);

        (range1 == range2).Should().BeTrue();
    }

    [TestMethod]
    public void OperatorEquality_DifferentRange_ShouldReturnFalse()
    {
        var range1 = new Range(2, 8);
        var range2 = new Range(2, 9);

        (range1 == range2).Should().BeFalse();
    }

    [TestMethod]
    public void OperatorInequality_SameRange_ShouldReturnFalse()
    {
        var range1 = new Range(2, 8);
        var range2 = new Range(2, 8);

        (range1 != range2).Should().BeFalse();
    }

    [TestMethod]
    public void OperatorInequality_DifferentRange_ShouldReturnTrue()
    {
        var range1 = new Range(2, 8);
        var range2 = new Range(2, 9);

        (range1 != range2).Should().BeTrue();
    }
}
