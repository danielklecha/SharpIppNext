using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace SharpIpp.Validation;

/// <summary>
/// Validates that each element of an enumerable (or a single value) is within the specified range.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class ItemRangeAttribute : ValidationAttribute
{
    public int Minimum { get; }
    public int Maximum { get; }

    public ItemRangeAttribute(int minimum, int maximum)
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
        int intValue;
        try
        {
            intValue = Convert.ToInt32(value);
        }
        catch (Exception)
        {
            return new ValidationResult($"The field {validationContext.DisplayName} must contain only numbers.");
        }

        if (intValue < Minimum || intValue > Maximum)
        {
            return new ValidationResult(ErrorMessage ?? $"The field {validationContext.DisplayName} must contain values between {Minimum} and {Maximum}.");
        }

        return ValidationResult.Success;
    }
}
