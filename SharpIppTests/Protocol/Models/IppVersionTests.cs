using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Models;
using System;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Protocol.Models.Tests;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppVersionTests
{
    [TestMethod]
    public void Constructor_Int16BigEndian_SetsMajorMinorCorrectly()
    {
        short bigEndian = BitConverter.ToInt16(new byte[] { 2, 1 }, 0);
        var version = new IppVersion(bigEndian);
        version.Major.Should().Be(1);
        version.Minor.Should().Be(2);
    }

    [TestMethod]
    public void Constructor_MajorMinor_SetsPropertiesCorrectly()
    {
        var version = new IppVersion(1, 1);
        version.Major.Should().Be(1);
        version.Minor.Should().Be(1);
    }

    [TestMethod]
    public void Constructor_StringVersion_SetsPropertiesCorrectly()
    {
        var version = new IppVersion("1.1");
        version.Major.Should().Be(1);
        version.Minor.Should().Be(1);

        var version2 = new IppVersion("2.0");
        version2.Major.Should().Be(2);
        version2.Minor.Should().Be(0);
    }

    [TestMethod]
    public void Constructor_EmptyString_ThrowsArgumentNullException()
    {
        Action action = () => new IppVersion("");
        action.Should().Throw<ArgumentNullException>().WithParameterName("version");
    }

    [TestMethod]
    public void Constructor_NullString_ThrowsArgumentNullException()
    {
        Action action = () => new IppVersion(null!);
        action.Should().Throw<ArgumentNullException>().WithParameterName("version");
    }

    [TestMethod]
    public void StaticProperties_HaveCorrectValues()
    {
        IppVersion.CUPS10.Major.Should().Be(1);
        IppVersion.CUPS10.Minor.Should().Be(2);
    }

    [TestMethod]
    public void ToString_ReturnsFormattedVersion()
    {
        var version = new IppVersion(1, 2);
        version.ToString().Should().Be("1.2");
    }

    [TestMethod]
    public void ToDecimal_ReturnsCorrectDecimalValue()
    {
        var version = new IppVersion(1, 2);
        version.ToDecimal().Should().Be(1.02m);
        
        var version2 = new IppVersion(2, 0);
        version2.ToDecimal().Should().Be(2.00m);
    }

    [TestMethod]
    public void ToInt16BigEndian_ReturnsCorrectValue()
    {
        var version = new IppVersion(1, 2);
        short encoded = version.ToInt16BigEndian();
        var decoded = new IppVersion(encoded);
        decoded.Major.Should().Be(1);
        decoded.Minor.Should().Be(2);
    }

    [TestMethod]
    public void Equals_SameValues_ReturnsTrue()
    {
        var version1 = new IppVersion(1, 1);
        var version2 = new IppVersion(1, 1);
        version1.Equals(version2).Should().BeTrue();
    }

    [TestMethod]
    public void Equals_DifferentValues_ReturnsFalse()
    {
        var version1 = new IppVersion(1, 1);
        var version2 = new IppVersion(1, 2);
        var version3 = new IppVersion(2, 1);
        var version4 = new IppVersion(2, 2);

        version1.Equals(version2).Should().BeFalse();
        version1.Equals(version3).Should().BeFalse();
        version1.Equals(version4).Should().BeFalse();
    }

    [TestMethod]
    public void Equals_Object_ReturnsExpectedResult()
    {
        var version1 = new IppVersion(1, 1);
        object version2 = new IppVersion(1, 1);
        object version3 = new IppVersion(1, 2);

        version1.Equals(version2).Should().BeTrue();
        version1.Equals(version3).Should().BeFalse();
        version1.Equals(null).Should().BeFalse();
        version1.Equals("1.1").Should().BeFalse();
    }

    [TestMethod]
    public void GetHashCode_SameValues_ReturnsSameHashCode()
    {
        var version1 = new IppVersion(1, 2);
        var version2 = new IppVersion(1, 2);
        version1.GetHashCode().Should().Be(version2.GetHashCode());
    }

    [TestMethod]
    public void CompareTo_ReturnsExpectedResults()
    {
        var v1_0 = new IppVersion(1, 0);
        var v1_1 = new IppVersion(1, 1);
        var v1_2 = new IppVersion(1, 2);
        var v2_0 = new IppVersion(2, 0);

        v1_1.CompareTo(v1_1).Should().Be(0);
        v1_1.CompareTo(v1_2).Should().BeNegative();
        v1_2.CompareTo(v1_1).Should().BePositive();
        
        v1_0.CompareTo(v2_0).Should().BeNegative();
        v2_0.CompareTo(v1_0).Should().BePositive();
    }

    [TestMethod]
    public void Operators_Equality_ReturnsExpectedResults()
    {
        var v1 = new IppVersion(1, 1);
        var v2 = new IppVersion(1, 1);
        var v3 = new IppVersion(1, 2);
        var v4 = new IppVersion(2, 1);
        var v5 = new IppVersion(2, 2);

        (v1 == v2).Should().BeTrue();
        (v1 != v2).Should().BeFalse();

        (v1 == v3).Should().BeFalse();
        (v1 != v3).Should().BeTrue();

        (v1 == v4).Should().BeFalse();
        (v1 != v4).Should().BeTrue();

        (v1 == v5).Should().BeFalse();
        (v1 != v5).Should().BeTrue();
    }

    [TestMethod]
    public void Operators_Relational_ReturnsExpectedResults()
    {
        var v1_1 = new IppVersion(1, 1);
        var v1_2 = new IppVersion(1, 2);
        var v2_0 = new IppVersion(2, 0);

        (v1_1 < v1_2).Should().BeTrue();
        (v1_1 <= v1_2).Should().BeTrue();
        
        (v1_2 > v1_1).Should().BeTrue();
        (v1_2 >= v1_1).Should().BeTrue();

#pragma warning disable CS1718 // Comparison made to same variable
        (v1_1 <= v1_1).Should().BeTrue();
        (v1_1 >= v1_1).Should().BeTrue();
#pragma warning restore CS1718 // Comparison made to same variable

        (v2_0 > v1_2).Should().BeTrue();
        (v1_2 < v2_0).Should().BeTrue();
    }
}
