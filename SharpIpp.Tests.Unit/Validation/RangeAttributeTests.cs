using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Validation;
using SharpIpp.Protocol.Models;
using Range = SharpIpp.Protocol.Models.Range;

namespace SharpIpp.Tests.Unit.Validation;

[TestClass]
[ExcludeFromCodeCoverage]
public class RangeAttributeTests
{
    [TestMethod]
    public void Constructor_SetsMinimumAndMaximum()
    {
        var attribute = new RangeAttribute(1, 10);
        attribute.Minimum.Should().Be(1);
        attribute.Maximum.Should().Be(10);
    }

    [TestMethod]
    public void IsValid_WhenValueIsNull_ReturnsSuccess()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");

        var result = attribute.IsValid(null, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void IsValid_WhenSingleValueInRange_ReturnsSuccess()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");

        var result = attribute.IsValid(5, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void IsValid_WhenSingleValueOutOfRange_ReturnsValidationError()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");

        var result = attribute.IsValid(15, context);

        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField must contain values between 1 and 10.");
    }

    [TestMethod]
    public void IsValid_WhenRangeInRange_ReturnsSuccess()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var range = new Range(2, 9);

        var result = attribute.IsValid(range, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void IsValid_WhenRangeLowerOutOfRange_ReturnsValidationError()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var range = new Range(0, 5);

        var result = attribute.IsValid(range, context);

        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField must be within the range of 1 and 10.");
    }

    [TestMethod]
    public void IsValid_WhenRangeUpperOutOfRange_ReturnsValidationError()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var range = new Range(2, 11);

        var result = attribute.IsValid(range, context);

        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField must be within the range of 1 and 10.");
    }

    [TestMethod]
    public void IsValid_WhenCollectionOfIntsInRange_ReturnsSuccess()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var collection = new[] { 1, 5, 10 };

        var result = attribute.IsValid(collection, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void IsValid_WhenCollectionOfIntsContainsOutOfRange_ReturnsValidationError()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var collection = new[] { 1, 5, 11 };

        var result = attribute.IsValid(collection, context);

        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField must contain values between 1 and 10.");
    }

    [TestMethod]
    public void IsValid_WhenCollectionOfRangesInRange_ReturnsSuccess()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var collection = new[] { new Range(2, 8), new Range(1, 10) };

        var result = attribute.IsValid(collection, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void IsValid_WhenCollectionOfRangesContainsOutOfRange_ReturnsValidationError()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var collection = new[] { new Range(2, 8), new Range(1, 11) };

        var result = attribute.IsValid(collection, context);

        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField must be within the range of 1 and 10.");
    }

    [TestMethod]
    public void IsValid_WhenValueIsInvalidType_ReturnsValidationError()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");

        var result = attribute.IsValid("not-a-number", context);

        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField must be a number or a range.");
    }

    [TestMethod]
    public void IsValid_WhenCollectionContainsNullElement_SkipsNullAndReturnsSuccess()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var collection = new object?[] { 5, null, 8 };

        var result = attribute.IsValid(collection, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void IsValid_WhenSingleValueLessThanMinimum_ReturnsValidationError()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");

        var result = attribute.IsValid(0, context);

        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField must contain values between 1 and 10.");
    }

    [TestMethod]
    public void IsValid_WhenRangeLowerGreaterThanUpper_ReturnsValidationError()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var range = new Range(8, 3);

        var result = attribute.IsValid(range, context);

        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField lower bound must be less than or equal to upper bound.");
    }

    [TestMethod]
    public void IsValid_WhenMultiRange_ValidatesCorrectly()
    {
        var attribute = new RangeAttribute(1, 5, 10, 15);
        var context = new ValidationContext(Encoding.UTF8, "TestField");

        attribute.IsValid(3, context).Should().Be(ValidationResult.Success);
        attribute.IsValid(12, context).Should().Be(ValidationResult.Success);

        var result = attribute.IsValid(7, context);
        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField must be within one of the following ranges: 1-5, 10-15.");
    }

    [TestMethod]
    public void Constructor_WhenRangesOddLength_ThrowsArgumentException()
    {
        Action act = () => new RangeAttribute(1, 2, 3);
        act.Should().Throw<ArgumentException>().WithParameterName("ranges");
    }

    [TestMethod]
    public void Constructor_WhenRangesNull_ThrowsArgumentNullException()
    {
        Action act = () => new RangeAttribute(null!);
        act.Should().Throw<ArgumentNullException>().WithParameterName("ranges");
    }

    [TestMethod]
    public void Constructor_WhenRangesEmpty_ThrowsArgumentException()
    {
        Action act = () => new RangeAttribute(Array.Empty<int>());
        act.Should().Throw<ArgumentException>().WithParameterName("ranges");
    }

    [TestMethod]
    public void IsValid_WhenRangeInMultiRange_ReturnsSuccess()
    {
        var attribute = new RangeAttribute(1, 5, 10, 15);
        var context = new ValidationContext(Encoding.UTF8, "TestField");

        attribute.IsValid(new Range(2, 4), context).Should().Be(ValidationResult.Success);
        attribute.IsValid(new Range(11, 14), context).Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void IsValid_WhenRangeOutOfMultiRange_ReturnsValidationError()
    {
        var attribute = new RangeAttribute(1, 5, 10, 15);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var range = new Range(6, 8);

        var result = attribute.IsValid(range, context);

        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField must be within one of the following ranges: 1-5, 10-15.");
    }

    [TestMethod]
    public void IsValid_WhenRangeOutOfMultiRangeWithCustomErrorMessage_ReturnsCustomErrorMessage()
    {
        var attribute = new RangeAttribute(1, 5, 10, 15) { ErrorMessage = "Custom range error" };
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var range = new Range(6, 8);

        var result = attribute.IsValid(range, context);

        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("Custom range error");
    }

    [TestMethod]
    public void IsValid_WhenLongValue_ValidatesCorrectly()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");

        attribute.IsValid(5L, context).Should().Be(ValidationResult.Success);

        var result = attribute.IsValid(15L, context);
        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
    }

    [TestMethod]
    public void IsValid_WhenShortValue_ValidatesCorrectly()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");

        attribute.IsValid((short)5, context).Should().Be(ValidationResult.Success);

        var result = attribute.IsValid((short)15, context);
        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
    }

    [TestMethod]
    public void IsValid_WhenByteValue_ValidatesCorrectly()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");

        attribute.IsValid((byte)5, context).Should().Be(ValidationResult.Success);

        var result = attribute.IsValid((byte)15, context);
        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
    }

    [TestMethod]
    public void IsValid_WhenSByteValue_ValidatesCorrectly()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");

        attribute.IsValid((sbyte)5, context).Should().Be(ValidationResult.Success);

        var result = attribute.IsValid((sbyte)15, context);
        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
    }

    [TestMethod]
    public void IsValid_WhenUIntValue_ValidatesCorrectly()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");

        attribute.IsValid(5u, context).Should().Be(ValidationResult.Success);

        var result = attribute.IsValid(15u, context);
        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
    }

    [TestMethod]
    public void IsValid_WhenUShortValue_ValidatesCorrectly()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");

        attribute.IsValid((ushort)5, context).Should().Be(ValidationResult.Success);

        var result = attribute.IsValid((ushort)15, context);
        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
    }

    [TestMethod]
    public void IsValid_WhenConvertibleValue_ValidatesCorrectly()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");

        attribute.IsValid(5UL, context).Should().Be(ValidationResult.Success);

        var result = attribute.IsValid(15UL, context);
        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
    }

    [TestMethod]
    public void IsValid_WhenNumericString_ConvertsAndValidatesCorrectly()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");

        attribute.IsValid("5", context).Should().Be(ValidationResult.Success);

        var result = attribute.IsValid("15", context);
        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
    }

    [TestMethod]
    public void IsValid_WhenValueThrowsOnConvert_ReturnsValidationError()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");

        var result = attribute.IsValid(ulong.MaxValue, context);
        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField must be a number or a range.");
    }

    [TestMethod]
    public void IsValid_WhenCollectionContainsNonNumericItem_ReturnsValidationError()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var collection = new object[] { 1, "invalid", 10 };

        var result = attribute.IsValid(collection, context);

        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField must be a number or a range.");
    }

    [TestMethod]
    public void IsValid_WhenValueOutOfRangeAndCustomErrorMessageSet_ReturnsCustomErrorMessage()
    {
        var attribute = new RangeAttribute(1, 10) { ErrorMessage = "Custom error message" };
        var context = new ValidationContext(Encoding.UTF8, "TestField");

        var result = attribute.IsValid(15, context);

        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("Custom error message");
    }
[TestMethod]
    public void IsValid_WhenValueIsIppValueNoValue_ReturnsSuccess()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        IppValue<int> value = IppValue<int>.NoValue;

        var result = attribute.IsValid(value, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void IsValid_WhenValueIsIppValueWithValidScalar_ReturnsSuccess()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        IppValue<int> value = new(5);

        var result = attribute.IsValid(value, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void IsValid_WhenValueIsIppValueWithInvalidScalar_ReturnsValidationError()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        IppValue<int> value = new(15);

        var result = attribute.IsValid(value, context);

        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField must contain values between 1 and 10.");
    }

    [TestMethod]
    public void IsValid_WhenValueIsIppValueWithNull_ReturnsSuccess()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        IppValue<Range?> value = new((Range?)null);

        var result = attribute.IsValid(value, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void IsValid_WhenValueIsIppValueWithValidRange_ReturnsSuccess()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        IppValue<Range> value = new(new Range(2, 8));

        var result = attribute.IsValid(value, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void IsValid_WhenValueIsIppValueWithInvalidRange_ReturnsValidationError()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        IppValue<Range> value = new(new Range(0, 15));

        var result = attribute.IsValid(value, context);

        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField must be within the range of 1 and 10.");
    }

    [TestMethod]
    public void IsValid_WhenCollectionContainsIppValueNoValue_ReturnsSuccess()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var collection = new IIppValue[]
        {
            IppValue<int>.NoValue,
            new IppValue<int>(5)
        };

        var result = attribute.IsValid(collection, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void IsValid_WhenCollectionContainsIppValueWithNull_ReturnsSuccess()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var collection = new IIppValue[]
        {
            new IppValue<Range?>((Range?)null),
            new IppValue<Range>(new Range(2, 8))
        };

        var result = attribute.IsValid(collection, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void IsValid_WhenCollectionContainsIppValueWithValidValue_ReturnsSuccess()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var collection = new IppValue<int>[]
        {
            new(3),
            new(7)
        };

        var result = attribute.IsValid(collection, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void IsValid_WhenCollectionContainsIppValueWithOutOfRangeValue_ReturnsValidationError()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var collection = new IppValue<int>[]
        {
            new(3),
            new(15)
        };

        var result = attribute.IsValid(collection, context);

        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField must contain values between 1 and 10.");
    }

    [TestMethod]
    public void IsValid_WhenCollectionContainsIppValueRanges_ValidatesCorrectly()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var validCollection = new IppValue<Range>[]
        {
            new(new Range(2, 5)),
            IppValue<Range>.NoValue
        };

        var validResult = attribute.IsValid(validCollection, context);
        validResult.Should().Be(ValidationResult.Success);

        var invalidCollection = new IppValue<Range>[]
        {
            new(new Range(0, 5))
        };

        var invalidResult = attribute.IsValid(invalidCollection, context);
        invalidResult.Should().NotBeNull();
        invalidResult!.IsSuccess.Should().BeFalse();
        invalidResult.ErrorMessage.Should().Be("The field TestField must be within the range of 1 and 10.");
    }

    [TestMethod]
    public void IsValid_WhenValueIsIppValueWithCollection_ReturnsSuccess()
    {
        var attribute = new RangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        IppValue<int[]> value = new(new[] { 2, 5, 8 });

        var result = attribute.IsValid(value, context);

        result.Should().Be(ValidationResult.Success);
    }
}
