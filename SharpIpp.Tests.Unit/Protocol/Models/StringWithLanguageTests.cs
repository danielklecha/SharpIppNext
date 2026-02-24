using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Models;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Tests.Unit.Protocol.Models;

[TestClass]
[ExcludeFromCodeCoverage]
public class StringWithLanguageTests
{
    [TestMethod]
    public void Constructor_SetsPropertiesAndReturnsToString()
    {
        var strWithLang = new StringWithLanguage("en", "Test Value");

        strWithLang.Language.Should().Be("en");
        strWithLang.Value.Should().Be("Test Value");
        strWithLang.ToString().Should().Be("Test Value (en)");
    }

    [TestMethod]
    public void Equals_SameValues_ShouldReturnTrue()
    {
        var str1 = new StringWithLanguage("en", "Test Value");
        var str2 = new StringWithLanguage("en", "Test Value");

        str1.Equals(str2).Should().BeTrue();
        str1.Equals((object)str2).Should().BeTrue();
        (str1 == str2).Should().BeTrue();
        (str1 != str2).Should().BeFalse();
    }

    [TestMethod]
    public void Equals_DifferentLanguage_ShouldReturnFalse()
    {
        var str1 = new StringWithLanguage("en", "Test Value");
        var str2 = new StringWithLanguage("fr", "Test Value");

        str1.Equals(str2).Should().BeFalse();
        str1.Equals((object)str2).Should().BeFalse();
        (str1 == str2).Should().BeFalse();
        (str1 != str2).Should().BeTrue();
    }

    [TestMethod]
    public void Equals_DifferentValue_ShouldReturnFalse()
    {
        var str1 = new StringWithLanguage("en", "Test Value");
        var str2 = new StringWithLanguage("en", "Other Value");

        str1.Equals(str2).Should().BeFalse();
        str1.Equals((object)str2).Should().BeFalse();
        (str1 == str2).Should().BeFalse();
        (str1 != str2).Should().BeTrue();
    }

    [TestMethod]
    public void Equals_NullOrDifferentType_ShouldReturnFalse()
    {
        var str = new StringWithLanguage("en", "Test Value");

        str.Equals(null).Should().BeFalse();
        str.Equals(new object()).Should().BeFalse();
    }

    [TestMethod]
    public void GetHashCode_SameValues_ShouldReturnSameHashCode()
    {
        var str1 = new StringWithLanguage("en", "Test Value");
        var str2 = new StringWithLanguage("en", "Test Value");

        str1.GetHashCode().Should().Be(str2.GetHashCode());
    }

    [TestMethod]
    public void GetHashCode_DifferentValues_ShouldReturnDifferentHashCode()
    {
        var str1 = new StringWithLanguage("en", "Test Value");
        var str2 = new StringWithLanguage("fr", "Test Value");

        str1.GetHashCode().Should().NotBe(str2.GetHashCode());
    }

    [TestMethod]
    public void GetHashCode_NullValues_ShouldReturnExpectedHashCode()
    {
        var str1 = new StringWithLanguage(null!, null!);
        
#pragma warning disable CS1718 // Comparison made to same variable
        (str1 == str1).Should().BeTrue();
#pragma warning restore CS1718 // Comparison made to same variable
        str1.GetHashCode().Should().Be(0);

        var str2 = new StringWithLanguage("en", null!);
        var str3 = new StringWithLanguage(null!, "Test");
        
        str2.GetHashCode().Should().NotBe(0);
        str3.GetHashCode().Should().NotBe(0);
        str2.GetHashCode().Should().NotBe(str3.GetHashCode());
    }
}
