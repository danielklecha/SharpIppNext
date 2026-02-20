using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Models;
using System.Diagnostics.CodeAnalysis;

namespace SharpIppTests.Protocol.Models;

[TestClass]
[ExcludeFromCodeCoverage]
public class ResolutionTests
{
    [TestMethod]
    public void Equals_SameValues_ShouldReturnTrue()
    {
        var res1 = new Resolution(300, 300, ResolutionUnit.DotsPerInch);
        var res2 = new Resolution(300, 300, ResolutionUnit.DotsPerInch);

        (res1 == res2).Should().BeTrue();
        (res1 != res2).Should().BeFalse();
        res1.Equals(res2).Should().BeTrue();
        res1.Equals((object)res2).Should().BeTrue();
    }

    [TestMethod]
    public void Equals_DifferentValues_ShouldReturnFalse()
    {
        var res1 = new Resolution(300, 300, ResolutionUnit.DotsPerInch);
        var res2 = new Resolution(600, 600, ResolutionUnit.DotsPerInch);

        (res1 == res2).Should().BeFalse();
        (res1 != res2).Should().BeTrue();
        res1.Equals(res2).Should().BeFalse();
        res1.Equals((object)res2).Should().BeFalse();
    }

    [TestMethod]
    public void Equals_NullOrDifferentType_ShouldReturnFalse()
    {
        var res = new Resolution(300, 300, ResolutionUnit.DotsPerInch);

        res.Equals(null).Should().BeFalse();
        res.Equals(new object()).Should().BeFalse();
    }

    [TestMethod]
    public void Deconstruct_ShouldReturnCorrectValues()
    {
        var res = new Resolution(300, 600, ResolutionUnit.DotsPerInch);
        var (width, height, units) = res;

        width.Should().Be(300);
        height.Should().Be(600);
        units.Should().Be(ResolutionUnit.DotsPerInch);
    }

    [TestMethod]
    public void GetHashCode_SameValues_ShouldReturnSameHashCode()
    {
        var res1 = new Resolution(300, 300, ResolutionUnit.DotsPerInch);
        var res2 = new Resolution(300, 300, ResolutionUnit.DotsPerInch);

        res1.GetHashCode().Should().Be(res2.GetHashCode());
    }

    [TestMethod]
    public void GetHashCode_DifferentValues_ShouldReturnDifferentHashCode()
    {
        var res1 = new Resolution(300, 300, ResolutionUnit.DotsPerInch);
        var res2 = new Resolution(600, 600, ResolutionUnit.DotsPerInch);

        res1.GetHashCode().Should().NotBe(res2.GetHashCode());
    }

    [TestMethod]
    public void ToString_WithDotsPerInch_ShouldReturnExpectedFormat()
    {
        var res = new Resolution(300, 300, ResolutionUnit.DotsPerInch);
        res.ToString().Should().Be("300x300 (dpi)");
    }

    [TestMethod]
    public void ToString_WithDotsPerCm_ShouldReturnExpectedFormat()
    {
        var res = new Resolution(300, 300, ResolutionUnit.DotsPerCm);
        res.ToString().Should().Be("300x300 (dpcm)");
    }

    [TestMethod]
    public void ToString_WithUnknownUnit_ShouldReturnExpectedFormat()
    {
        var res = new Resolution(300, 300, (ResolutionUnit)999);
        res.ToString().Should().Be("300x300 (unknown)");
    }
}
