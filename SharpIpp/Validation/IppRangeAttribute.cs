using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Validation;

/// <summary>
/// Validates that value(s) of type integer, Range, or collections thereof are within the specified bounds.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class IppRangeAttribute : ValidationAttribute
{
    public int Minimum { get; }
    public int Maximum { get; }

    public IppRangeAttribute(int minimum, int maximum)
    {
        Minimum = minimum;
        Maximum = maximum;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }

        if (value is IEnumerable enumerable && !(value is string))
        {
            foreach (var item in enumerable)
            {
                if (item == null)
                {
                    continue;
                }

                var itemResult = ValidateValue(item, validationContext);
                if (itemResult != ValidationResult.Success)
                {
                    return itemResult;
                }
            }

            return ValidationResult.Success;
        }

        return ValidateValue(value, validationContext);
    }

    private ValidationResult? ValidateValue(object value, ValidationContext validationContext)
    {
        if (value is SharpIpp.Protocol.Models.Range range)
        {
            if (range.Lower < Minimum || range.Upper > Maximum)
            {
                return new ValidationResult(ErrorMessage ?? $"The field {validationContext.DisplayName} must contain dimensions within the range of {Minimum} and {Maximum}.");
            }
            return ValidationResult.Success;
        }

        int intValue;
        try
        {
            intValue = Convert.ToInt32(value);
        }
        catch (Exception)
        {
            return new ValidationResult($"The field {validationContext.DisplayName} must be a number or a range.");
        }

        if (intValue < Minimum || intValue > Maximum)
        {
            return new ValidationResult(ErrorMessage ?? $"The field {validationContext.DisplayName} must contain values between {Minimum} and {Maximum}.");
        }

        return ValidationResult.Success;
    }
}
