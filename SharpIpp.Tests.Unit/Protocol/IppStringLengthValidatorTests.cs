using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Protocol;

[TestClass]
public class IppStringLengthValidatorTests
{
    [TestMethod]
    public void Validate_WhenOctetStringObjectExceedsLimit_InvokesOnErrorAndCompletes()
    {
        var errors = new List<string>();
        var longBytes = new byte[1024];
        var attr = new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, "test-octet", new OctetString(longBytes));

        IppStringLengthValidator.Validate(new[] { attr }, Encoding.UTF8, err => errors.Add(err));

        errors.Should().ContainSingle()
            .Which.Should().Be("Attribute 'test-octet' of tag 'OctetStringWithAnUnspecifiedFormat' length (1024 octets) exceeds RFC 8011 limit of 1023 octets");
    }

    [TestMethod]
    public void Validate_WhenByteArrayExceedsLimit_InvokesOnErrorAndCompletes()
    {
        var errors = new List<string>();
        var longBytes = new byte[1024];
        var attr = new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, "test-raw-bytes", longBytes);

        IppStringLengthValidator.Validate(new[] { attr }, Encoding.UTF8, err => errors.Add(err));

        errors.Should().ContainSingle()
            .Which.Should().Be("Attribute 'test-raw-bytes' of tag 'OctetStringWithAnUnspecifiedFormat' length (1024 octets) exceeds RFC 8011 limit of 1023 octets");
    }

    [TestMethod]
    public void Validate_WhenOctetStringOtherObjectExceedsLimit_InvokesOnErrorAndCompletes()
    {
        var errors = new List<string>();
        var longString = new string('x', 1024);
        var attr = new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, "test-string-octet", longString);

        IppStringLengthValidator.Validate(new[] { attr }, Encoding.UTF8, err => errors.Add(err));

        errors.Should().ContainSingle()
            .Which.Should().Be("Attribute 'test-string-octet' of tag 'OctetStringWithAnUnspecifiedFormat' length (1024 octets) exceeds RFC 8011 limit of 1023 octets");
    }

    [TestMethod]
    public void Validate_WhenOctetStringWithinLimit_DoesNotInvokeOnError()
    {
        var errors = new List<string>();
        var validBytes = new byte[1023];
        var attr = new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, "test-octet", new OctetString(validBytes));

        IppStringLengthValidator.Validate(new[] { attr }, Encoding.UTF8, err => errors.Add(err));

        errors.Should().BeEmpty();
    }

    [TestMethod]
    public void Validate_WhenStringLengthExceedsLimit_InvokesOnErrorAndCompletes()
    {
        var errors = new List<string>();
        var longText = new string('a', 1024);
        var attr = new IppAttribute(Tag.TextWithoutLanguage, "test-text", longText);

        IppStringLengthValidator.Validate(new[] { attr }, Encoding.UTF8, err => errors.Add(err));

        errors.Should().ContainSingle()
            .Which.Should().Be("Attribute 'test-text' of tag 'TextWithoutLanguage' length (1024 octets) exceeds RFC 8011 limit of 1023 octets");
    }

    [TestMethod]
    public void Validate_WhenStringWithLanguageValueExceedsLimit_InvokesOnErrorAndCompletes()
    {
        var errors = new List<string>();
        var longText = new string('a', 1024);
        var attr = new IppAttribute(Tag.TextWithLanguage, "test-text-lang", new StringWithLanguage("en", longText));

        IppStringLengthValidator.Validate(new[] { attr }, Encoding.UTF8, err => errors.Add(err));

        errors.Should().ContainSingle()
            .Which.Should().Be("Attribute 'test-text-lang' of tag 'TextWithLanguage' value length (1024 octets) exceeds RFC 8011 limit of 1023 octets");
    }

    [TestMethod]
    public void Validate_WhenStringWithLanguageLanguageExceedsLimit_InvokesOnErrorAndCompletes()
    {
        var errors = new List<string>();
        var longLang = new string('a', 64);
        var attr = new IppAttribute(Tag.TextWithLanguage, "test-text-lang", new StringWithLanguage(longLang, "valid value"));

        IppStringLengthValidator.Validate(new[] { attr }, Encoding.UTF8, err => errors.Add(err));

        errors.Should().ContainSingle()
            .Which.Should().Be("Attribute 'test-text-lang' of tag 'TextWithLanguage' language length (64 octets) exceeds RFC 8011 limit of 63 octets");
    }

    [TestMethod]
    [DataRow(Tag.UriScheme, 63)]
    [DataRow(Tag.Charset, 63)]
    [DataRow(Tag.NaturalLanguage, 63)]
    [DataRow(Tag.Keyword, 255)]
    [DataRow(Tag.MimeMediaType, 255)]
    [DataRow(Tag.Uri, 1023)]
    public void Validate_WhenStandardTagsExceedLimit_InvokesOnErrorAndCompletes(Tag tag, int maxLimit)
    {
        var errors = new List<string>();
        var longValue = new string('c', maxLimit + 1);
        var attr = new IppAttribute(tag, "test-attr", longValue);

        IppStringLengthValidator.Validate(new[] { attr }, Encoding.UTF8, err => errors.Add(err));

        errors.Should().ContainSingle()
            .Which.Should().Contain($"exceeds RFC 8011 limit of {maxLimit} octets");
    }

    [TestMethod]
    public void Validate_WhenAttributeValueIsNull_DoesNotInvokeOnError()
    {
        var errors = new List<string>();
        var attr = default(IppAttribute);

        IppStringLengthValidator.Validate(new[] { attr }, Encoding.UTF8, err => errors.Add(err));

        errors.Should().BeEmpty();
    }

    [TestMethod]
    public void Validate_WhenAttributeValueIsNoValue_DoesNotInvokeOnError()
    {
        var errors = new List<string>();
        var attr = new IppAttribute(Tag.Keyword, "test-attr", NoValue.Instance);

        IppStringLengthValidator.Validate(new[] { attr }, Encoding.UTF8, err => errors.Add(err));

        errors.Should().BeEmpty();
    }

    [TestMethod]
    public void Validate_WhenTagIsIgnored_DoesNotInvokeOnError()
    {
        var errors = new List<string>();
        var attr = new IppAttribute(Tag.Integer, "test-int", 42);

        IppStringLengthValidator.Validate(new[] { attr }, Encoding.UTF8, err => errors.Add(err));

        errors.Should().BeEmpty();
    }

    [TestMethod]
    public void Validate_WhenOctetStringToStringIsNull_DoesNotThrow()
    {
        var errors = new List<string>();
        var attr = new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, "test-null-to-string", new NullToStringObject());

        IppStringLengthValidator.Validate(new[] { attr }, Encoding.UTF8, err => errors.Add(err));

        errors.Should().BeEmpty();
    }

    [TestMethod]
    public void Validate_WhenTextWithoutLanguageWithStringWithLanguageValue_ValidatesCorrectly()
    {
        var errors = new List<string>();
        var swl = new StringWithLanguage(null!, "valid value");
        var attr = new IppAttribute(Tag.TextWithoutLanguage, "test-text", swl);

        IppStringLengthValidator.Validate(new[] { attr }, Encoding.UTF8, err => errors.Add(err));

        errors.Should().BeEmpty();
    }

    [TestMethod]
    public void Validate_WhenTextWithLanguageWithNullStringValue_DoesNotThrow()
    {
        var errors = new List<string>();
        var swl = new StringWithLanguage("en", null!);
        var attr = new IppAttribute(Tag.TextWithLanguage, "test-text", swl);

        IppStringLengthValidator.Validate(new[] { attr }, Encoding.UTF8, err => errors.Add(err));

        errors.Should().BeEmpty();
    }

    [TestMethod]
    public void Validate_WhenTextWithoutLanguageWithNullToString_DoesNotThrow()
    {
        var errors = new List<string>();
        var attr = new IppAttribute(Tag.TextWithoutLanguage, "test-text", new NullToStringObject());

        IppStringLengthValidator.Validate(new[] { attr }, Encoding.UTF8, err => errors.Add(err));

        errors.Should().BeEmpty();
    }

    [TestMethod]
    [DataRow(Tag.UriScheme)]
    [DataRow(Tag.Charset)]
    [DataRow(Tag.NaturalLanguage)]
    [DataRow(Tag.Keyword)]
    [DataRow(Tag.MimeMediaType)]
    [DataRow(Tag.Uri)]
    public void Validate_WhenTagWithNullToString_DoesNotThrow(Tag tag)
    {
        var errors = new List<string>();
        var attr = new IppAttribute(tag, "test-attr", new NullToStringObject());

        IppStringLengthValidator.Validate(new[] { attr }, Encoding.UTF8, err => errors.Add(err));

        errors.Should().BeEmpty();
    }

    [TestMethod]
    public void Validate_WhenNameWithoutLanguageWithStringWithLanguage_ValidatesCorrectly()
    {
        var errors = new List<string>();
        var swl = new StringWithLanguage(null!, "valid name");
        var attr = new IppAttribute(Tag.NameWithoutLanguage, "test-name", swl);

        IppStringLengthValidator.Validate(new[] { attr }, Encoding.UTF8, err => errors.Add(err));

        errors.Should().BeEmpty();
    }

    [TestMethod]
    public void Validate_WhenNameWithLanguageWithNullLanguage_DoesNotThrow()
    {
        var errors = new List<string>();
        var swl = new StringWithLanguage(null!, "valid name");
        var attr = new IppAttribute(Tag.NameWithLanguage, "test-name", swl);

        IppStringLengthValidator.Validate(new[] { attr }, Encoding.UTF8, err => errors.Add(err));

        errors.Should().BeEmpty();
    }

    private class NullToStringObject
    {
        public override string? ToString() => null;
    }

    [TestMethod]
    public void GetStringOrOctetValues_WithNullOrNoValue_YieldsNothing()
    {
        IppStringLengthValidator.GetStringOrOctetValues(null).Should().BeEmpty();
        IppStringLengthValidator.GetStringOrOctetValues(NoValue.Instance).Should().BeEmpty();
    }

    [TestMethod]
    public void GetStringOrOctetValues_WithSupportedTypes_YieldsDirectly()
    {
        IppStringLengthValidator.GetStringOrOctetValues("hello").Should().ContainSingle().Which.Should().Be("hello");
        var swl = new StringWithLanguage("en", "text");
        IppStringLengthValidator.GetStringOrOctetValues(swl).Should().ContainSingle().Which.Should().Be(swl);
        var os = new OctetString(new byte[] { 1, 2, 3 });
        IppStringLengthValidator.GetStringOrOctetValues(os).Should().ContainSingle().Which.Should().Be(os);
        var bytes = new byte[] { 4, 5, 6 };
        IppStringLengthValidator.GetStringOrOctetValues(bytes).Should().ContainSingle().Which.Should().Be(bytes);
    }

    [TestMethod]
    public void GetStringOrOctetValues_WithNestedEnumerable_FlattensValues()
    {
        var nested = new object[]
        {
            "first",
            new object[] { "second", new byte[] { 1 } }
        };

        var result = IppStringLengthValidator.GetStringOrOctetValues(nested);

        result.Should().HaveCount(3);
    }

    [TestMethod]
    public void GetStringOrOctetValues_WithOtherType_YieldsValue()
    {
        IppStringLengthValidator.GetStringOrOctetValues(12345).Should().ContainSingle().Which.Should().Be(12345);
    }
}
