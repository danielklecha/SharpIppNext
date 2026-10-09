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
    [TestMethod]
    [DataRow("en", "hello", Tag.TextWithoutLanguage, Tag.TextWithLanguage)]
    [DataRow(null, "hello", Tag.TextWithoutLanguage, Tag.TextWithoutLanguage)]
    [DataRow("en", "hello", Tag.TextWithLanguage, Tag.TextWithLanguage)]
    [DataRow(null, "hello", Tag.TextWithLanguage, Tag.TextWithoutLanguage)]
    [DataRow("", "hello-empty-lang", Tag.TextWithoutLanguage, Tag.TextWithoutLanguage)]
    [DataRow("", "hello-empty-lang", Tag.TextWithLanguage, Tag.TextWithoutLanguage)]
    [DataRow("de", "printer", Tag.NameWithoutLanguage, Tag.NameWithLanguage)]
    [DataRow(null, "printer", Tag.NameWithoutLanguage, Tag.NameWithoutLanguage)]
    [DataRow("de", "printer", Tag.NameWithLanguage, Tag.NameWithLanguage)]
    [DataRow(null, "printer", Tag.NameWithLanguage, Tag.NameWithoutLanguage)]
    [DataRow("", "printer-empty-lang", Tag.NameWithoutLanguage, Tag.NameWithoutLanguage)]
    [DataRow("", "printer-empty-lang", Tag.NameWithLanguage, Tag.NameWithoutLanguage)]
    public void ToIppTag_ShouldReturnExpectedTag(string? language, string text, Tag suggestedTag, Tag expected)
    {
        var value = new StringWithLanguage(language, text);
        var result = value.ToIppTag(suggestedTag);
        result.Should().Be(expected);
    }

    [TestMethod]
    [DataRow(Tag.TextWithoutLanguage)]
    [DataRow(Tag.NameWithoutLanguage)]
    [DataRow(Tag.TextWithLanguage)]
    [DataRow(Tag.NameWithLanguage)]
    public void ToIppTag_NoValue_ShouldReturnNoValue(Tag suggestedTag)
    {
        default(StringWithLanguage).ToIppTag(suggestedTag).Should().Be(Tag.NoValue);
        ((StringWithLanguage)NoValue.Instance).ToIppTag(suggestedTag).Should().Be(Tag.NoValue);
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

        var defaultSwl = default(StringWithLanguage);
        Action actDefault = () => defaultSwl.ToIppTag(unsupportedTag);
        actDefault.Should().Throw<ArgumentException>();
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
