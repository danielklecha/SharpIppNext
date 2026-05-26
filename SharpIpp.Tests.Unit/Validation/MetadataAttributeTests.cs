using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Models;
using SharpIpp.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

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
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateValue(null!, context, results, new[] { attribute });

        isValid.Should().BeTrue();
        results.Should().BeEmpty();
    }

    [TestMethod]
    public void IsValid_WhenValueIsNotIppStructuredString_ReturnsValidationError()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateValue("not-metadata", context, results, new[] { attribute });

        isValid.Should().BeFalse();
        results.Should().ContainSingle();
        results.Single().ErrorMessage.Should().Be("The field TestField must derive from IppStructuredString.");
    }

    [TestMethod]
    public void IsValid_WhenKeywordIsEmpty_ReturnsValidationError()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();
        var metadata = new DocumentMetadata
        {
            { "", "some-value" }
        };

        var isValid = Validator.TryValidateValue(metadata, context, results, new[] { attribute });

        isValid.Should().BeFalse();
        results.Should().ContainSingle();
        results.Single().ErrorMessage.Should().Be("The field TestField has an empty keyword.");
    }

    [TestMethod]
    public void IsValid_WhenKeywordIsInvalid_ReturnsValidationError()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();
        var metadata = new DocumentMetadata
        {
            { "invalidKeyword", "some-value" }
        };

        var isValid = Validator.TryValidateValue(metadata, context, results, new[] { attribute });

        isValid.Should().BeFalse();
        results.Should().ContainSingle();
        results.Single().ErrorMessage.Should().Be("The field TestField has invalid keyword 'invalidKeyword'.");
    }

    [TestMethod]
    public void IsValid_WhenVendorKeywordIsMissingSuffix_ReturnsValidationError()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();
        var metadata = new DocumentMetadata
        {
            { "x-", "some-value" }
        };

        var isValid = Validator.TryValidateValue(metadata, context, results, new[] { attribute });

        isValid.Should().BeFalse();
        results.Should().ContainSingle();
        results.Single().ErrorMessage.Should().Be("The field TestField has invalid keyword 'x-'.");
    }

    [TestMethod]
    public void IsValid_WhenVendorKeywordHasInvalidCharacters_ReturnsValidationError()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();
        var metadata = new DocumentMetadata
        {
            { "x-invalid*char", "some-value" }
        };

        var isValid = Validator.TryValidateValue(metadata, context, results, new[] { attribute });

        isValid.Should().BeFalse();
        results.Should().ContainSingle();
        results.Single().ErrorMessage.Should().Be("The field TestField has invalid keyword 'x-invalid*char'.");
    }

    [TestMethod]
    public void IsValid_WhenValueContainsControlCharacters_ReturnsValidationError()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();
        var metadata = new DocumentMetadata
        {
            { "title", "some\nvalue" }
        };

        var isValid = Validator.TryValidateValue(metadata, context, results, new[] { attribute });

        isValid.Should().BeFalse();
        results.Should().ContainSingle();
        results.Single().ErrorMessage.Should().Be("The field TestField has invalid value for keyword 'title': must be valid UTF-8 and contain no control characters.");
    }

    [TestMethod]
    public void IsValid_WhenValidDublinCoreKeywords_ReturnsSuccess()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();
        var metadata = new DocumentMetadata
        {
            { "title", "My Document" },
            { "creator", "John Doe" },
            { "abstract", "An abstract description" }
        };

        var isValid = Validator.TryValidateValue(metadata, context, results, new[] { attribute });

        isValid.Should().BeTrue();
        results.Should().BeEmpty();
    }

    [TestMethod]
    public void IsValid_WhenValidVendorKeywords_ReturnsSuccess()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();
        var metadata = new DocumentMetadata
        {
            { "x-company-id", "12345" },
            { "x-custom.name", "Test" },
            { "x-another_prop", "Val" }
        };

        var isValid = Validator.TryValidateValue(metadata, context, results, new[] { attribute });

        isValid.Should().BeTrue();
        results.Should().BeEmpty();
    }

    [TestMethod]
    public void IsValid_WithCustomIppStructuredStringSubclass_ReturnsValidationErrorIfInvalid()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();
        var metadata = new TestMetadata();
        metadata.Add("invalid", "value");

        var isValid = Validator.TryValidateValue(metadata, context, results, new[] { attribute });

        isValid.Should().BeFalse();
        results.Should().ContainSingle();
        results.Single().ErrorMessage.Should().Be("The field TestField has invalid entry.");
    }

    [TestMethod]
    public void IsValid_WithCustomIppStructuredStringSubclass_ReturnsSuccessIfValid()
    {
        var attribute = new MetadataAttribute();
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();
        var metadata = new TestMetadata();
        metadata.Add("testkey", "value");

        var isValid = Validator.TryValidateValue(metadata, context, results, new[] { attribute });

        isValid.Should().BeTrue();
        results.Should().BeEmpty();
    }
}
