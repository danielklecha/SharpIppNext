using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Tests.Unit.Protocol.Extensions;

[TestClass]
[ExcludeFromCodeCoverage]
public class TagExtensionsTests
{
    public static IEnumerable<object[]> TagTestData
    {
        get
        {
            yield return new object[] { Tag.Unsupported, true };
            yield return new object[] { Tag.Unknown, true };
            yield return new object[] { Tag.NoValue, true };
            yield return new object[] { Tag.Integer, false };
            yield return new object[] { Tag.Boolean, false };
            yield return new object[] { Tag.Enum, false };
            yield return new object[] { Tag.DateTime, false };
            yield return new object[] { Tag.Resolution, false };
            yield return new object[] { Tag.TextWithLanguage, false };
            yield return new object[] { Tag.NameWithLanguage, false };
            yield return new object[] { Tag.TextWithoutLanguage, false };
            yield return new object[] { Tag.NameWithoutLanguage, false };
            yield return new object[] { Tag.Keyword, false };
            yield return new object[] { Tag.Uri, false };
            yield return new object[] { Tag.UriScheme, false };
            yield return new object[] { Tag.Charset, false };
            yield return new object[] { Tag.NaturalLanguage, false };
            yield return new object[] { Tag.MimeMediaType, false };
        }
    }

    [TestMethod]
    [DynamicData(nameof(TagTestData))]
    public void IsOutOfBand_ShouldReturnExpectedResult(Tag tag, bool expected)
    {
        // Act
        var result = tag.IsOutOfBand();

        // Assert
        result.Should().Be(expected);
    }
}
