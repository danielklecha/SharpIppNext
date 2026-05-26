using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SharpIpp.Tests.Unit.Protocol.Models;

[TestClass]
[ExcludeFromCodeCoverage]
public class DocumentMetadataTests
{
    [TestMethod]
    public void Properties_ShouldGetAndSetCorrectKeys()
    {
        var metadata = new DocumentMetadata();

        // Standard DC Elements
        metadata.Title = "My Title";
        metadata.Creator = "John Doe";
        metadata.Date = "2026-05-21";

        metadata.Title.Should().Be("My Title");
        metadata.Creator.Should().Be("John Doe");
        metadata.Date.Should().Be("2026-05-21");

        metadata["title"].Should().Be("My Title");
        metadata["creator"].Should().Be("John Doe");
        metadata["date"].Should().Be("2026-05-21");

        // Standard DC Terms
        metadata.Abstract = "An abstract";
        metadata.AccessRights = "Public";

        metadata.Abstract.Should().Be("An abstract");
        metadata.AccessRights.Should().Be("Public");

        metadata["abstract"].Should().Be("An abstract");
        metadata["accessRights"].Should().Be("Public");
    }

    [TestMethod]
    public void Properties_SetNull_ShouldRemoveKey()
    {
        var metadata = new DocumentMetadata
        {
            Title = "My Title"
        };
        metadata.Title.Should().Be("My Title");

        metadata.Title = null;

        metadata.Title.Should().BeNull();
        metadata.ContainsKey("title").Should().BeFalse();
    }

    [TestMethod]
    public void Enumerator_ShouldYieldKeyValueStrings()
    {
        var metadata = new DocumentMetadata
        {
            Title = "My Title",
            Creator = "John Doe"
        };

        var list = metadata.ToList();
        list.Should().Contain("title=My Title");
        list.Should().Contain("creator=John Doe");
    }

    [TestMethod]
    public void Validate_WhenValidStandardAndCustomKeywords_ShouldPass()
    {
        var metadata = new DocumentMetadata
        {
            Title = "Valid Title",
            Creator = "Valid Creator"
        };
        metadata.Add("x-company-id", "12345");
        metadata["x-custom-key"] = "value";

        Action act = () => metadata.Validate();
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_WhenKeywordIsEmpty_ShouldThrowValidationException()
    {
        var metadata = new DocumentMetadata();
        metadata.Add("", "value");

        Action act = () => metadata.Validate();
        act.Should().Throw<ValidationException>().WithMessage("has an empty keyword.");
    }

    [TestMethod]
    public void Validate_WhenKeywordIsInvalid_ShouldThrowValidationException()
    {
        var metadata = new DocumentMetadata();
        metadata.Add("invalidKeyword", "value");

        Action act = () => metadata.Validate();
        act.Should().Throw<ValidationException>().WithMessage("has invalid keyword 'invalidKeyword'.");
    }

    [TestMethod]
    public void Validate_WhenVendorKeywordIsMissingSuffix_ShouldThrowValidationException()
    {
        var metadata = new DocumentMetadata();
        metadata.Add("x-", "value");

        Action act = () => metadata.Validate();
        act.Should().Throw<ValidationException>().WithMessage("has invalid keyword 'x-'.");
    }

    [TestMethod]
    public void Validate_WhenVendorKeywordHasInvalidCharacters_ShouldThrowValidationException()
    {
        var metadata = new DocumentMetadata();
        metadata.Add("x-invalid*char", "value");

        Action act = () => metadata.Validate();
        act.Should().Throw<ValidationException>().WithMessage("has invalid keyword 'x-invalid*char'.");
    }

    [TestMethod]
    public void Validate_WhenValueContainsControlCharacters_ShouldThrowValidationException()
    {
        var metadata = new DocumentMetadata
        {
            Title = "some\nvalue"
        };

        Action act = () => metadata.Validate();
        act.Should().Throw<ValidationException>().WithMessage("has invalid value for keyword 'title': must be valid UTF-8 and contain no control characters.");
    }

    [TestMethod]
    public void DateTimeOffsetProperties_ShouldParseAndFormatCorrectly()
    {
        var metadata = new DocumentMetadata();

        // 1. Set property, check reading back and dictionary formatting
        var testDate = new DateTimeOffset(2026, 5, 21, 14, 0, 0, TimeSpan.FromHours(2));
        metadata.Created = testDate;

        metadata.Created.Should().Be(testDate);
        metadata["created"].Should().Be("2026-05-21T14:00:00+02:00");

        // 2. Set string in dictionary directly, check parsing
        metadata["created"] = "2026-05-21T12:00:00Z";
        metadata.Created.Should().Be(new DateTimeOffset(2026, 5, 21, 12, 0, 0, TimeSpan.Zero));

        // 3. Set invalid string, should return null (fail-safe reading)
        metadata["created"] = "not-a-date";
        metadata.Created.Should().BeNull();

        // 4. Set to null, should remove key
        metadata.Created = null;
        metadata.Created.Should().BeNull();
        metadata.ContainsKey("created").Should().BeFalse();

        // 5. Test other Date properties to ensure they work as expected
        var dateVal = new DateTimeOffset(2026, 6, 1, 10, 30, 0, TimeSpan.FromHours(-5));
        metadata.DateAccepted = dateVal;
        metadata.DateCopyrighted = dateVal;
        metadata.DateSubmitted = dateVal;
        metadata.Available = dateVal;
        metadata.Issued = dateVal;
        metadata.Modified = dateVal;
        metadata.Valid = dateVal;

        metadata.DateAccepted.Should().Be(dateVal);
        metadata.DateCopyrighted.Should().Be(dateVal);
        metadata.DateSubmitted.Should().Be(dateVal);
        metadata.Available.Should().Be(dateVal);
        metadata.Issued.Should().Be(dateVal);
        metadata.Modified.Should().Be(dateVal);
        metadata.Valid.Should().Be(dateVal);

        metadata["dateAccepted"].Should().Be("2026-06-01T10:30:00-05:00");
    }

    [TestMethod]
    public void UriProperties_ShouldParseAndFormatCorrectly()
    {
        var metadata = new DocumentMetadata();

        // 1. Set property, check reading back and dictionary formatting
        var testUri = new Uri("http://example.com/part1");
        metadata.HasPart = testUri;

        metadata.HasPart.Should().Be(testUri);
        metadata["hasPart"].Should().Be("http://example.com/part1");

        // 2. Set relative URI in dictionary directly
        metadata["hasPart"] = "/relative/path";
        metadata.HasPart.Should().Be(new Uri("/relative/path", UriKind.RelativeOrAbsolute));

        // 3. Set to null, should remove key
        metadata.HasPart = null;
        metadata.HasPart.Should().BeNull();
        metadata.ContainsKey("hasPart").Should().BeFalse();

        // 4. Test other URI properties
        metadata.HasVersion = testUri;
        metadata.HasVersion.Should().Be(testUri);
        metadata["hasVersion"].Should().Be("http://example.com/part1");
    }

    [TestMethod]
    public void AddCustom_ShouldValidateAndAdd()
    {
        var metadata = new DocumentMetadata();
        
        // 1. Valid custom key
        metadata.AddCustom("x-custom-key", "value");
        metadata["x-custom-key"].Should().Be("value");

        // 2. Invalid prefix (should throw ArgumentException)
        Action act1 = () => metadata.AddCustom("abc-custom", "value");
        act1.Should().Throw<ArgumentException>().WithMessage("Custom metadata keyword must start with 'x-'.*");

        // 3. Missing suffix (should throw ArgumentException)
        Action act2 = () => metadata.AddCustom("x-", "value");
        act2.Should().Throw<ArgumentException>().WithMessage("Custom metadata keyword 'x-' is invalid.*");

        // 4. Invalid characters (should throw ArgumentException)
        Action act3 = () => metadata.AddCustom("x-invalid*char", "value");
        act3.Should().Throw<ArgumentException>().WithMessage("Custom metadata keyword 'x-invalid*char' is invalid.*");

        // 5. Null key (should throw ArgumentNullException)
        Action act4 = () => metadata.AddCustom(null!, "value");
        act4.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void Constructor_WithDictionary_ShouldCopyValues()
    {
        var dict = new Dictionary<string, string>
        {
            { "title", "Test Title" },
            { "creator", "Test Creator" }
        };
        var metadata = new DocumentMetadata(dict);
        metadata.Title.Should().Be("Test Title");
        metadata.Creator.Should().Be("Test Creator");
    }

    [TestMethod]
    public void AllProperties_ShouldGetAndSetCorrectKeysUsingReflection()
    {
        var metadata = new DocumentMetadata();
        var properties = typeof(DocumentMetadata)
            .GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
            .Where(p => p.CanRead && p.CanWrite && p.GetIndexParameters().Length == 0 && p.Name != "Extensions" && p.Name != "IsValue")
            .ToList();

        foreach (var prop in properties)
        {
            var expectedKey = char.ToLowerInvariant(prop.Name[0]) + prop.Name.Substring(1);

            if (prop.PropertyType == typeof(string))
            {
                var testValue = $"test-{expectedKey}";
                prop.SetValue(metadata, testValue);
                prop.GetValue(metadata).Should().Be(testValue);
                metadata[expectedKey].Should().Be(testValue);

                prop.SetValue(metadata, null);
                prop.GetValue(metadata).Should().BeNull();
                metadata.ContainsKey(expectedKey).Should().BeFalse();
            }
            else if (prop.PropertyType == typeof(DateTimeOffset?))
            {
                var testValue = new DateTimeOffset(2026, 5, 25, 12, 0, 0, TimeSpan.Zero);
                prop.SetValue(metadata, testValue);
                prop.GetValue(metadata).Should().Be(testValue);

                prop.SetValue(metadata, null);
                prop.GetValue(metadata).Should().BeNull();
                metadata.ContainsKey(expectedKey).Should().BeFalse();
            }
            else if (prop.PropertyType == typeof(Uri))
            {
                var testValue = new Uri($"http://example.com/{expectedKey}");
                prop.SetValue(metadata, testValue);
                prop.GetValue(metadata).Should().Be(testValue);
                metadata[expectedKey].Should().Be(testValue.OriginalString);

                prop.SetValue(metadata, null);
                prop.GetValue(metadata).Should().BeNull();
                metadata.ContainsKey(expectedKey).Should().BeFalse();
            }
            else
            {
                Assert.Fail($"Unsupported property type: {prop.PropertyType} on property {prop.Name}");
            }
        }
    }

    [TestMethod]
    public void Extensions_Properties_And_Extensions_ShouldSynchronizeWithDictionary()
    {
        var metadata = new DocumentMetadata
        {
            Title = "My Title",
            Creator = "John Doe",
            Extensions = new Dictionary<string, string> { { "x-custom", "value" } }
        };

        // 1. Check properties and Extensions are correct
        metadata.Title.Should().Be("My Title");
        metadata.Creator.Should().Be("John Doe");
        metadata.Extensions.Should().ContainKey("x-custom").WhoseValue.Should().Be("value");

        // 2. Change via properties
        metadata.Title = "New Title";
        metadata.Extensions.Should().ContainKey("x-custom").WhoseValue.Should().Be("value");
        metadata.Extensions.Should().NotContainKey("title");

        // 3. Clear extensions
        metadata.Extensions = null;
        metadata.Extensions.Should().BeNull();
        metadata.Title.Should().Be("New Title");
    }
}
