using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SharpIpp.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class MultiRangeAttribute : ValidationAttribute
{
    public int[] Ranges { get; }

    public MultiRangeAttribute(params int[] ranges)
    {
        if (ranges == null)
            throw new ArgumentNullException(nameof(ranges));
        if (ranges.Length % 2 != 0)
            throw new ArgumentException("Ranges must contain an even number of elements.", nameof(ranges));

        Ranges = ranges;
    }

    /// <summary>
    /// Validates the specified value with respect to the current validation attribute.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="validationContext">The context information about the validation operation.</param>
    /// <returns>A ValidationResult value indicating whether validation succeeded or failed.</returns>
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }

        int intValue;
        try
        {
            intValue = Convert.ToInt32(value);
        }
        catch (Exception)
        {
            return new ValidationResult($"The field {validationContext.DisplayName} must be a number.");
        }

        for (int i = 0; i < Ranges.Length; i += 2)
        {
            int min = Ranges[i];
            int max = Ranges[i + 1];
            if (intValue >= min && intValue <= max)
            {
                return ValidationResult.Success;
            }
        }

        var rangeStrings = Enumerable.Range(0, Ranges.Length / 2)
            .Select(i => $"{Ranges[i * 2]}-{Ranges[i * 2 + 1]}");
        var allowedRanges = string.Join(", ", rangeStrings);

        return new ValidationResult(ErrorMessage ?? $"The field {validationContext.DisplayName} must be within one of the following ranges: {allowedRanges}.");
    }
}
