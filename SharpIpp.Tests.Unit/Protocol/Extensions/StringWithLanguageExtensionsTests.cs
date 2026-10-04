using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Protocol.Extensions;

[TestClass]
[ExcludeFromCodeCoverage]
public class StringWithLanguageExtensionsTests
{
    public static IEnumerable<object[]> ToIppTagTestData
    {
        get
        {
            // Text tags
            yield return new object[] { new StringWithLanguage("en", "hello"), Tag.TextWithoutLanguage, Tag.TextWithLanguage };
            yield return new object[] { new StringWithLanguage(null, "hello"), Tag.TextWithoutLanguage, Tag.TextWithoutLanguage };
            yield return new object[] { new StringWithLanguage("en", "hello"), Tag.TextWithLanguage, Tag.TextWithLanguage };
            yield return new object[] { new StringWithLanguage(null, "hello"), Tag.TextWithLanguage, Tag.TextWithoutLanguage };
            yield return new object[] { new StringWithLanguage(string.Empty, "hello-empty-lang"), Tag.TextWithoutLanguage, Tag.TextWithoutLanguage };

            // Name tags
            yield return new object[] { new StringWithLanguage("de", "printer"), Tag.NameWithoutLanguage, Tag.NameWithLanguage };
            yield return new object[] { new StringWithLanguage(null, "printer"), Tag.NameWithoutLanguage, Tag.NameWithoutLanguage };
            yield return new object[] { new StringWithLanguage("de", "printer"), Tag.NameWithLanguage, Tag.NameWithLanguage };
            yield return new object[] { new StringWithLanguage(null, "printer"), Tag.NameWithLanguage, Tag.NameWithoutLanguage };
            yield return new object[] { new StringWithLanguage(string.Empty, "printer-empty-lang"), Tag.NameWithoutLanguage, Tag.NameWithoutLanguage };

            // NoValue state (IsValue == false)
            yield return new object[] { default(StringWithLanguage), Tag.TextWithoutLanguage, Tag.NoValue };
            yield return new object[] { default(StringWithLanguage), Tag.NameWithoutLanguage, Tag.NoValue };
            yield return new object[] { (StringWithLanguage)NoValue.Instance, Tag.TextWithLanguage, Tag.NoValue };
            yield return new object[] { (StringWithLanguage)NoValue.Instance, Tag.NameWithLanguage, Tag.NoValue };
        }
    }

    [TestMethod]
    [DynamicData(nameof(ToIppTagTestData))]
    public void ToIppTag_ShouldReturnExpectedTag(StringWithLanguage value, Tag suggestedTag, Tag expected)
    {
        var result = value.ToIppTag(suggestedTag);
        result.Should().Be(expected);
    }

    [TestMethod]
    [DataRow(Tag.Keyword)]
    [DataRow(Tag.Uri)]
    [DataRow(Tag.Integer)]
    [DataRow(Tag.Boolean)]
    [DataRow(Tag.Unsupported)]
    [DataRow(Tag.Unknown)]
    [DataRow(Tag.Charset)]
    [DataRow(Tag.NaturalLanguage)]
    [DataRow(Tag.MimeMediaType)]
    public void ToIppTag_UnsupportedTag_ThrowsArgumentException(Tag unsupportedTag)
    {
        var swl = new StringWithLanguage("test");
        Action act = () => swl.ToIppTag(unsupportedTag);
        act.Should().Throw<ArgumentException>();
    }

    [TestMethod]
    public void ToIppTag_DefaultSuggestedTag_UsesTextWithoutLanguage()
    {
        var withLang = new StringWithLanguage("en", "test");
        withLang.ToIppTag().Should().Be(Tag.TextWithLanguage);

        var withoutLang = new StringWithLanguage("test");
        withoutLang.ToIppTag().Should().Be(Tag.TextWithoutLanguage);
    }
}
