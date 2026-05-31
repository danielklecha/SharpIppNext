using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Validation;
using Range = SharpIpp.Protocol.Models.Range;

namespace SharpIpp.Tests.Unit.Validation;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppRangeAttributeTests
{
    [TestMethod]
    public void Constructor_SetsMinimumAndMaximum()
    {
        var attribute = new IppRangeAttribute(1, 10);
        attribute.Minimum.Should().Be(1);
        attribute.Maximum.Should().Be(10);
    }

    [TestMethod]
    public void IsValid_WhenValueIsNull_ReturnsSuccess()
    {
        var attribute = new IppRangeAttribute(1, 10);
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateValue(null!, context, results, new[] { attribute });

        isValid.Should().BeTrue();
        results.Should().BeEmpty();
    }

    [TestMethod]
    public void IsValid_WhenSingleValueInRange_ReturnsSuccess()
    {
        var attribute = new IppRangeAttribute(1, 10);
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateValue(5, context, results, new[] { attribute });

        isValid.Should().BeTrue();
        results.Should().BeEmpty();
    }

    [TestMethod]
    public void IsValid_WhenSingleValueOutOfRange_ReturnsValidationError()
    {
        var attribute = new IppRangeAttribute(1, 10);
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateValue(15, context, results, new[] { attribute });

        isValid.Should().BeFalse();
        results.Should().ContainSingle();
        results.Single().ErrorMessage.Should().Be("The field TestField must contain values between 1 and 10.");
    }

    [TestMethod]
    public void IsValid_WhenRangeInRange_ReturnsSuccess()
    {
        var attribute = new IppRangeAttribute(1, 10);
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();
        var range = new Range(2, 9);

        var isValid = Validator.TryValidateValue(range, context, results, new[] { attribute });

        isValid.Should().BeTrue();
        results.Should().BeEmpty();
    }

    [TestMethod]
    public void IsValid_WhenRangeLowerOutOfRange_ReturnsValidationError()
    {
        var attribute = new IppRangeAttribute(1, 10);
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();
        var range = new Range(0, 5);

        var isValid = Validator.TryValidateValue(range, context, results, new[] { attribute });

        isValid.Should().BeFalse();
        results.Should().ContainSingle();
        results.Single().ErrorMessage.Should().Be("The field TestField must contain dimensions within the range of 1 and 10.");
    }

    [TestMethod]
    public void IsValid_WhenRangeUpperOutOfRange_ReturnsValidationError()
    {
        var attribute = new IppRangeAttribute(1, 10);
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();
        var range = new Range(2, 11);

        var isValid = Validator.TryValidateValue(range, context, results, new[] { attribute });

        isValid.Should().BeFalse();
        results.Should().ContainSingle();
        results.Single().ErrorMessage.Should().Be("The field TestField must contain dimensions within the range of 1 and 10.");
    }

    [TestMethod]
    public void IsValid_WhenCollectionOfIntsInRange_ReturnsSuccess()
    {
        var attribute = new IppRangeAttribute(1, 10);
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();
        var collection = new[] { 1, 5, 10 };

        var isValid = Validator.TryValidateValue(collection, context, results, new[] { attribute });

        isValid.Should().BeTrue();
        results.Should().BeEmpty();
    }

    [TestMethod]
    public void IsValid_WhenCollectionOfIntsContainsOutOfRange_ReturnsValidationError()
    {
        var attribute = new IppRangeAttribute(1, 10);
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();
        var collection = new[] { 1, 5, 11 };

        var isValid = Validator.TryValidateValue(collection, context, results, new[] { attribute });

        isValid.Should().BeFalse();
        results.Should().ContainSingle();
        results.Single().ErrorMessage.Should().Be("The field TestField must contain values between 1 and 10.");
    }

    [TestMethod]
    public void IsValid_WhenCollectionOfRangesInRange_ReturnsSuccess()
    {
        var attribute = new IppRangeAttribute(1, 10);
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();
        var collection = new[] { new Range(2, 8), new Range(1, 10) };

        var isValid = Validator.TryValidateValue(collection, context, results, new[] { attribute });

        isValid.Should().BeTrue();
        results.Should().BeEmpty();
    }

    [TestMethod]
    public void IsValid_WhenCollectionOfRangesContainsOutOfRange_ReturnsValidationError()
    {
        var attribute = new IppRangeAttribute(1, 10);
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();
        var collection = new[] { new Range(2, 8), new Range(1, 11) };

        var isValid = Validator.TryValidateValue(collection, context, results, new[] { attribute });

        isValid.Should().BeFalse();
        results.Should().ContainSingle();
        results.Single().ErrorMessage.Should().Be("The field TestField must contain dimensions within the range of 1 and 10.");
    }

    [TestMethod]
    public void IsValid_WhenValueIsInvalidType_ReturnsValidationError()
    {
        var attribute = new IppRangeAttribute(1, 10);
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateValue("not-a-number", context, results, new[] { attribute });

        isValid.Should().BeFalse();
        results.Should().ContainSingle();
        results.Single().ErrorMessage.Should().Be("The field TestField must be a number or a range.");
    }
}
