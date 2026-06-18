using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SharpIpp.Tests.Unit.Validation;

[TestClass]
[ExcludeFromCodeCoverage]
public class ItemRangeAttributeTests
{
    [TestMethod]
    public void Constructor_SetsMinimumAndMaximum()
    {
        var attribute = new ItemRangeAttribute(1, 10);
        attribute.Minimum.Should().Be(1);
        attribute.Maximum.Should().Be(10);
    }

    [TestMethod]
    public void IsValid_WhenValueIsNull_ReturnsSuccess()
    {
        var attribute = new ItemRangeAttribute(1, 10);
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateValue(null!, context, results, new[] { attribute });

        isValid.Should().BeTrue();
        results.Should().BeEmpty();
    }

    [TestMethod]
    public void IsValid_WhenSingleValueInRange_ReturnsSuccess()
    {
        var attribute = new ItemRangeAttribute(1, 10);
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateValue(5, context, results, new[] { attribute });

        isValid.Should().BeTrue();
        results.Should().BeEmpty();
    }

    [TestMethod]
    public void IsValid_WhenSingleValueOutOfRange_ReturnsValidationError()
    {
        var attribute = new ItemRangeAttribute(1, 10);
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateValue(15, context, results, new[] { attribute });

        isValid.Should().BeFalse();
        results.Should().ContainSingle();
        results.Single().ErrorMessage.Should().Be("The field TestField must contain values between 1 and 10.");
    }

    [TestMethod]
    public void IsValid_WhenCollectionItemsAllInRange_ReturnsSuccess()
    {
        var attribute = new ItemRangeAttribute(1, 10);
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();
        var collection = new[] { 1, 5, 10 };

        var isValid = Validator.TryValidateValue(collection, context, results, new[] { attribute });

        isValid.Should().BeTrue();
        results.Should().BeEmpty();
    }

    [TestMethod]
    public void IsValid_WhenCollectionItemsContainOutOfRangeItem_ReturnsValidationError()
    {
        var attribute = new ItemRangeAttribute(1, 10);
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();
        var collection = new[] { 1, 5, 11 };

        var isValid = Validator.TryValidateValue(collection, context, results, new[] { attribute });

        isValid.Should().BeFalse();
        results.Should().ContainSingle();
        results.Single().ErrorMessage.Should().Be("The field TestField must contain values between 1 and 10.");
    }

    [TestMethod]
    public void IsValid_WhenCollectionContainsNullItems_SkipsNullAndReturnsSuccess()
    {
        var attribute = new ItemRangeAttribute(1, 10);
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();
        var collection = new int?[] { 1, null, 10 };

        var isValid = Validator.TryValidateValue(collection, context, results, new[] { attribute });

        isValid.Should().BeTrue();
        results.Should().BeEmpty();
    }

    [TestMethod]
    public void IsValid_WhenValueIsNotANumber_ReturnsValidationError()
    {
        var attribute = new ItemRangeAttribute(1, 10);
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateValue("not-a-number", context, results, new[] { attribute });

        isValid.Should().BeFalse();
        results.Should().ContainSingle();
        results.Single().ErrorMessage.Should().Be("The field TestField must contain only numbers.");
    }

    [TestMethod]
    public void IsValid_WhenCollectionContainsNonNumericItem_ReturnsValidationError()
    {
        var attribute = new ItemRangeAttribute(1, 10);
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();
        var collection = new object[] { 1, "invalid", 10 };

        var isValid = Validator.TryValidateValue(collection, context, results, new[] { attribute });

        isValid.Should().BeFalse();
        results.Should().ContainSingle();
        results.Single().ErrorMessage.Should().Be("The field TestField must contain only numbers.");
    }

    [TestMethod]
    public void IsValid_WhenValueIsLessThanMinimum_ReturnsValidationError()
    {
        var attribute = new ItemRangeAttribute(1, 10);
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateValue(0, context, results, new[] { attribute });

        isValid.Should().BeFalse();
        results.Should().ContainSingle();
        results.Single().ErrorMessage.Should().Be("The field TestField must contain values between 1 and 10.");
    }

    [TestMethod]
    public void IsValid_WhenValueOutOfRangeAndCustomErrorMessageSet_ReturnsCustomErrorMessage()
    {
        var attribute = new ItemRangeAttribute(1, 10) { ErrorMessage = "Custom error message" };
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateValue(15, context, results, new[] { attribute });

        isValid.Should().BeFalse();
        results.Should().ContainSingle();
        results.Single().ErrorMessage.Should().Be("Custom error message");
    }
}

