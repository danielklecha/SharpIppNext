using System;
using System.Collections;
using System.Linq;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Validation;

/// <summary>
/// Specifies numeric or range constraints for a data field.
/// Supports single values, <see cref="SharpIpp.Protocol.Models.Range"/>, and collections thereof.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class RangeAttribute : IppValidationAttribute
{
    public int[] Ranges { get; }
    public int Minimum => Ranges[0];
    public int Maximum => Ranges[1];

    public RangeAttribute(int minimum, int maximum) : this([minimum, maximum])
    {
    }

    public RangeAttribute(params int[] ranges)
    {
        if (ranges == null)
            throw new ArgumentNullException(nameof(ranges));
        if (ranges.Length == 0 || ranges.Length % 2 != 0)
            throw new ArgumentException("Ranges must contain an even number of elements.", nameof(ranges));

        Ranges = ranges;
    }

    public override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IIppValue ippValue)
        {
            if (!ippValue.IsValue)
            {
                return ValidationResult.Success;
            }
            value = ippValue.ValueAsObject;
        }

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
                if (!itemResult.IsSuccess)
                {
                    return itemResult;
                }
            }

            return ValidationResult.Success;
        }

        return ValidateValue(value, validationContext);
    }

    private ValidationResult ValidateValue(object value, ValidationContext validationContext)
    {
        if (value is IIppValue ippValue)
        {
            if (!ippValue.IsValue)
            {
                return ValidationResult.Success;
            }
            value = ippValue.ValueAsObject!;
            if (value == null)
            {
                return ValidationResult.Success;
            }
        }

        if (value is SharpIpp.Protocol.Models.Range range)
        {
            if (range.Lower > range.Upper)
            {
                return new ValidationResult(ErrorMessage ?? $"The field {validationContext.MemberName} lower bound must be less than or equal to upper bound.");
            }

            bool rangeMatch = false;
            for (int i = 0; i < Ranges.Length; i += 2)
            {
                if (range.Lower >= Ranges[i] && range.Upper <= Ranges[i + 1])
                {
                    rangeMatch = true;
                    break;
                }
            }

            if (!rangeMatch)
            {
                if (Ranges.Length == 2)
                {
                    return new ValidationResult(ErrorMessage ?? $"The field {validationContext.MemberName} must be within the range of {Ranges[0]} and {Ranges[1]}.");
                }

                var rangeStrings = Enumerable.Range(0, Ranges.Length / 2)
                    .Select(i => $"{Ranges[i * 2]}-{Ranges[i * 2 + 1]}");
                var allowedRanges = string.Join(", ", rangeStrings);
                return new ValidationResult(ErrorMessage ?? $"The field {validationContext.MemberName} must be within one of the following ranges: {allowedRanges}.");
            }

            return ValidationResult.Success;
        }

        long numValue;
        if (value is int iVal) numValue = iVal;
        else if (value is long lVal) numValue = lVal;
        else if (value is short sVal) numValue = sVal;
        else if (value is byte bVal) numValue = bVal;
        else if (value is sbyte sbVal) numValue = sbVal;
        else if (value is uint uiVal) numValue = uiVal;
        else if (value is ushort usVal) numValue = usVal;
        else
        {
            try
            {
                numValue = Convert.ToInt64(value);
            }
            catch (Exception)
            {
                return new ValidationResult($"The field {validationContext.MemberName} must be a number or a range.");
            }
        }

        bool match = false;
        for (int i = 0; i < Ranges.Length; i += 2)
        {
            if (numValue >= Ranges[i] && numValue <= Ranges[i + 1])
            {
                match = true;
                break;
            }
        }

        if (!match)
        {
            if (Ranges.Length == 2)
            {
                return new ValidationResult(ErrorMessage ?? $"The field {validationContext.MemberName} must contain values between {Ranges[0]} and {Ranges[1]}.");
            }

            var rangeStrings = Enumerable.Range(0, Ranges.Length / 2)
                .Select(i => $"{Ranges[i * 2]}-{Ranges[i * 2 + 1]}");
            var allowedRanges = string.Join(", ", rangeStrings);
            return new ValidationResult(ErrorMessage ?? $"The field {validationContext.MemberName} must be within one of the following ranges: {allowedRanges}.");
        }

        return ValidationResult.Success;
    }
}
