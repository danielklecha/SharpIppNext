using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class ByteMultiRangeAttribute : ValidationAttribute
{
    public int[] Ranges { get; }

    public ByteMultiRangeAttribute(params int[] ranges)
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

        var encoding = Encoding.UTF8;
        if (validationContext.Items.TryGetValue("Encoding", out var encObj) && encObj is Encoding enc)
        {
            encoding = enc;
        }

        List<int> lengths;
        try
        {
            lengths = GetByteLengths(value, encoding).ToList();
        }
        catch (ArgumentException ex)
        {
            return new ValidationResult(ex.Message);
        }

        foreach (var length in lengths)
        {
            bool match = false;
            for (int i = 0; i < Ranges.Length; i += 2)
            {
                int min = Ranges[i];
                int max = Ranges[i + 1];
                if (length >= min && length <= max)
                {
                    match = true;
                    break;
                }
            }
            if (!match)
            {
                if (Ranges.Length == 2)
                {
                    return new ValidationResult(ErrorMessage ?? $"The field {validationContext.DisplayName} must be between {Ranges[0]} and {Ranges[1]} bytes.");
                }
                var rangeStrings = Enumerable.Range(0, Ranges.Length / 2)
                    .Select(i => $"{Ranges[i * 2]}-{Ranges[i * 2 + 1]}");
                var allowedRanges = string.Join(", ", rangeStrings);
                return new ValidationResult(ErrorMessage ?? $"The field {validationContext.DisplayName} must be within one of the following ranges: {allowedRanges} bytes.");
            }
        }

        return ValidationResult.Success;
    }

    protected static IEnumerable<int> GetByteLengths(object? value, Encoding encoding)
    {
        if (value == null)
        {
            yield break;
        }

        if (value is string s)
        {
            yield return encoding.GetByteCount(s);
        }
        else if (value is StringWithLanguage swl)
        {
            var languageBytesCount = Encoding.ASCII.GetByteCount(swl.Language ?? string.Empty);
            var valueBytesCount = encoding.GetByteCount(swl.Value ?? string.Empty);
            yield return languageBytesCount + valueBytesCount + 4;
        }
        else if (value is OctetString os)
        {
            yield return os.Value?.Length ?? 0;
        }
        else if (value is byte[] bytes)
        {
            yield return bytes.Length;
        }
        else if (value is IEnumerable enumerable)
        {
            foreach (var item in enumerable)
            {
                if (item != null)
                {
                    foreach (var len in GetByteLengths(item, encoding))
                    {
                        yield return len;
                    }
                }
            }
        }
        else
        {
            throw new ArgumentException($"Unsupported type for byte length validation: {value.GetType()}");
        }
    }
}
