using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Models;
using SharpIpp.Validation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SharpIpp.Tests.Unit.Validation;

[TestClass]
[ExcludeFromCodeCoverage]
public class MetadataAttributeTests
{
    private class TestMetadata : IppStructuredString
    {
        public override HashSet<string> StandardKeys { get; } = new() { "testkey" };

        public override void Validate()
        {
            if (ContainsKey("invalid"))
            {
                throw new ValidationException("has invalid entry.");
            }
        }
    }

    [TestMethod]
    public void IsValid_WhenValueIsNull_ReturnsSuccess()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(Encoding.UTF8, "TestField");

        var result = attribute.IsValid(null, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void IsValid_WhenValueIsNotIppStructuredString_ReturnsValidationError()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(Encoding.UTF8, "TestField");

        var result = attribute.IsValid("not-metadata", context);

        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField must derive from IppStructuredString.");
    }

    [TestMethod]
    public void IsValid_WhenKeywordIsEmpty_ReturnsValidationError()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var metadata = new DocumentMetadata
        {
            { "", "some-value" }
        };

        var result = attribute.IsValid(metadata, context);

        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField has an empty keyword.");
    }

    [TestMethod]
    public void IsValid_WhenKeywordIsInvalid_ReturnsValidationError()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var metadata = new DocumentMetadata
        {
            { "invalidKeyword", "some-value" }
        };

        var result = attribute.IsValid(metadata, context);

        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField has invalid keyword 'invalidKeyword'.");
    }

    [TestMethod]
    public void IsValid_WhenVendorKeywordIsMissingSuffix_ReturnsValidationError()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var metadata = new DocumentMetadata
        {
            { "x-", "some-value" }
        };

        var result = attribute.IsValid(metadata, context);

        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField has invalid keyword 'x-'.");
    }

    [TestMethod]
    public void IsValid_WhenVendorKeywordHasInvalidCharacters_ReturnsValidationError()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var metadata = new DocumentMetadata
        {
            { "x-invalid*char", "some-value" }
        };

        var result = attribute.IsValid(metadata, context);

        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField has invalid keyword 'x-invalid*char'.");
    }

    [TestMethod]
    public void IsValid_WhenValueContainsControlCharacters_ReturnsValidationError()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var metadata = new DocumentMetadata
        {
            { "title", "some\nvalue" }
        };

        var result = attribute.IsValid(metadata, context);

        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField has invalid value for keyword 'title': must be valid UTF-8 and contain no control characters.");
    }

    [TestMethod]
    public void IsValid_WhenValidDublinCoreKeywords_ReturnsSuccess()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var metadata = new DocumentMetadata
        {
            { "title", "My Document" },
            { "creator", "John Doe" },
            { "abstract", "An abstract description" }
        };

        var result = attribute.IsValid(metadata, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void IsValid_WhenValidVendorKeywords_ReturnsSuccess()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var metadata = new DocumentMetadata
        {
            { "x-company-id", "12345" },
            { "x-custom.name", "Test" },
            { "x-another_prop", "Val" }
        };

        var result = attribute.IsValid(metadata, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void IsValid_WithCustomIppStructuredStringSubclass_ReturnsValidationErrorIfInvalid()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var metadata = new TestMetadata();
        metadata.Add("invalid", "value");

        var result = attribute.IsValid(metadata, context);

        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField has invalid entry.");
    }

    [TestMethod]
    public void IsValid_WithCustomIppStructuredStringSubclass_ReturnsSuccessIfValid()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var metadata = new TestMetadata();
        metadata.Add("testkey", "value");

        var result = attribute.IsValid(metadata, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void IsValid_WhenCollectionContainsValidItems_ReturnsSuccess()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var collection = new[]
        {
            new DocumentMetadata { { "title", "Doc 1" } },
            new DocumentMetadata { { "title", "Doc 2" } }
        };

        var result = attribute.IsValid(collection, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void IsValid_WhenCollectionContainsNullItems_SkipsNullAndReturnsSuccess()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var collection = new DocumentMetadata?[]
        {
            new DocumentMetadata { { "title", "Doc 1" } },
            null,
            new DocumentMetadata { { "title", "Doc 2" } }
        };

        var result = attribute.IsValid(collection, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void IsValid_WhenCollectionContainsInvalidItem_ReturnsValidationError()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var collection = new[]
        {
            new DocumentMetadata { { "title", "Doc 1" } },
            new DocumentMetadata { { "invalidKeyword", "value" } }
        };

        var result = attribute.IsValid(collection, context);

        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField has invalid keyword 'invalidKeyword'.");
    }

    [TestMethod]
    public void IsValid_WhenCollectionContainsNonIppStructuredStringItem_ReturnsValidationError()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var collection = new object[]
        {
            new DocumentMetadata { { "title", "Doc 1" } },
            "not-metadata"
        };

        var result = attribute.IsValid(collection, context);

        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField must derive from IppStructuredString.");
    }

    [TestMethod]
    public void IsValid_WhenValueIsNonEnumerableInvalidType_ReturnsValidationError()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(Encoding.UTF8, "TestField");

        var result = attribute.IsValid(123, context);

        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField must derive from IppStructuredString.");
    }

    [TestMethod]
    public void IsValid_WhenValueIsIppValueNoValue_ReturnsSuccess()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        IppValue<DocumentMetadata> value = IppValue<DocumentMetadata>.NoValue;

        var result = attribute.IsValid(value, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void IsValid_WhenValueIsIppValueWithValidValue_ReturnsSuccess()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        IppValue<DocumentMetadata> value = new DocumentMetadata
        {
            { "title", "Test Title" }
        };

        var result = attribute.IsValid(value, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void IsValid_WhenValueIsIppValueWithInvalidValue_ReturnsValidationError()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        IppValue<DocumentMetadata> value = new DocumentMetadata
        {
            { "invalidKeyword", "value" }
        };

        var result = attribute.IsValid(value, context);

        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField has invalid keyword 'invalidKeyword'.");
    }

    [TestMethod]
    public void IsValid_WhenValueIsIppValueWithNull_ReturnsSuccess()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        IppValue<DocumentMetadata?> value = new(null);

        var result = attribute.IsValid(value, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void IsValid_WhenValueIsIppValueWithCollection_ReturnsSuccess()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        IppValue<DocumentMetadata[]> value = new[]
        {
            new DocumentMetadata { { "title", "Doc 1" } }
        };

        var result = attribute.IsValid(value, context);

        result.Should().Be(ValidationResult.Success);
    }
}
