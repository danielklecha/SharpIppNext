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
public class MultiRangeAttributeTests
{
    [TestMethod]
    public void Constructor_WhenRangesIsNull_ThrowsArgumentNullException()
    {
        Action act = () => new MultiRangeAttribute(null!);
        act.Should().Throw<ArgumentNullException>().WithParameterName("ranges");
    }

    [TestMethod]
    public void Constructor_WhenRangesLengthIsOdd_ThrowsArgumentException()
    {
        Action act = () => new MultiRangeAttribute(1, 2, 3);
        act.Should().Throw<ArgumentException>().WithParameterName("ranges").WithMessage("Ranges must contain an even number of elements.*");
    }

    [TestMethod]
    public void Constructor_WhenRangesAreValid_CreatesInstance()
    {
        var attribute = new MultiRangeAttribute(1, 2, 3, 4);
        attribute.Ranges.Should().Equal(new[] { 1, 2, 3, 4 });
    }

    [TestMethod]
    public void IsValid_WhenValueIsNotANumber_ReturnsValidationError()
    {
        var attribute = new MultiRangeAttribute(1, 10);
        var context = new ValidationContext(new object()) { DisplayName = "TestField" };
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateValue("not-a-number", context, results, new[] { attribute });

        isValid.Should().BeFalse();
        results.Should().ContainSingle();
        results.Single().ErrorMessage.Should().Be("The field TestField must be a number.");
    }
}
