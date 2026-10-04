using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Protocol.Extensions;

[TestClass]
[ExcludeFromCodeCoverage]
public class KeywordOrNameEnumExtensionsTests
{
    public static IEnumerable<object[]> ToIppTagTestData
    {
        get
        {
            yield return new object[] { new Media("iso_a4_210x297mm", true), Tag.Keyword };
            yield return new object[] { new Media("Accounting Team", false), Tag.NameWithoutLanguage };
            yield return new object[] { new OutputBin("vendor-bin-42", true), Tag.Keyword };
            yield return new object[] { new OutputBin("custom-finisher-bin", false), Tag.NameWithoutLanguage };
            yield return new object[] { new Media("iso_a4_210x297mm"), Tag.Keyword };
        }
    }

    [TestMethod]
    [DynamicData(nameof(ToIppTagTestData))]
    public void ToIppTag_ShouldReturnExpectedTag(IKeywordOrNameEnum value, Tag expected)
    {
        // Act
        var result = value.ToIppTag();

        // Assert
        result.Should().Be(expected);
    }

    [TestMethod]
    public void IsName_ShouldReflectKeywordStatus()
    {
        var kw = new Media("iso_a4", true);
        var name = new Media("custom-tray", false);

        kw.IsName().Should().BeFalse();
        name.IsName().Should().BeTrue();
    }
}
