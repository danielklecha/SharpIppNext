using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class ByteRangeAttribute : IppValidationAttribute
{
    public int[] Ranges { get; }
    public int Minimum => Ranges[0];
    public int Maximum => Ranges[1];

    public ByteRangeAttribute(int minimum, int maximum) : this([minimum, maximum])
    {
    }

    public ByteRangeAttribute(params int[] ranges)
    {
        if (ranges == null)
            throw new ArgumentNullException(nameof(ranges));
        if (ranges.Length == 0 || ranges.Length % 2 != 0)
            throw new ArgumentException("Ranges must contain an even number of elements.", nameof(ranges));

        Ranges = ranges;
    }

    public override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is INoValue { IsValue: false })
        {
            return ValidationResult.Success;
        }

        if (value is IIppValue ippValue)
        {
            value = ippValue.ValueAsObject;
        }

        if (value == null)
        {
            return ValidationResult.Success;
        }

        var encoding = validationContext.Encoding;
        return ValidateRecursive(value, encoding, validationContext);
    }

    private ValidationResult ValidateRecursive(object value, Encoding encoding, ValidationContext validationContext)
    {
        if (value is INoValue { IsValue: false })
        {
            return ValidationResult.Success;
        }

        if (value is IIppValue ippValue)
        {
            value = ippValue.ValueAsObject!;
            if (value == null)
            {
                return ValidationResult.Success;
            }
        }

        if (TryGetSingleByteLength(value, encoding, out int length))
        {
            return ValidateLength(length, validationContext);
        }

        if (value is IEnumerable enumerable && !(value is string))
        {
            foreach (var item in enumerable)
            {
                if (item == null)
                    continue;

                var res = ValidateRecursive(item, encoding, validationContext);
                if (!res.IsSuccess)
                    return res;
            }

            return ValidationResult.Success;
        }

        return new ValidationResult($"Unsupported type for byte length validation: {value.GetType()}");
    }

    private static bool TryGetSingleByteLength(object value, Encoding encoding, out int length)
    {
        if (value is string s)
        {
            length = encoding.GetByteCount(s);
            return true;
        }
        if (value is IKeywordEnum ke)
        {
            length = encoding.GetByteCount(ke.Value ?? string.Empty);
            return true;
        }
        if (value is StringWithLanguage swl)
        {
            var languageBytesCount = Encoding.ASCII.GetByteCount(swl.Language ?? string.Empty);
            var valueBytesCount = encoding.GetByteCount(swl.Value ?? string.Empty);
            length = languageBytesCount + valueBytesCount + 4;
            return true;
        }
        if (value is OctetString os)
        {
            length = os.Value?.Length ?? 0;
            return true;
        }
        if (value is byte[] bytes)
        {
            length = bytes.Length;
            return true;
        }

        length = 0;
        return false;
    }

    private ValidationResult ValidateLength(int length, ValidationContext validationContext)
    {
        bool match = false;
        for (int i = 0; i < Ranges.Length; i += 2)
        {
            if (length >= Ranges[i] && length <= Ranges[i + 1])
            {
                match = true;
                break;
            }
        }

        if (!match)
        {
            if (Ranges.Length == 2)
            {
                return new ValidationResult(ErrorMessage ?? $"The field {validationContext.MemberName} must be between {Ranges[0]} and {Ranges[1]} bytes.");
            }

            var allowedRanges = FormatAllowedRanges(Ranges);
            return new ValidationResult(ErrorMessage ?? $"The field {validationContext.MemberName} must be within one of the following ranges: {allowedRanges} bytes.");
        }

        return ValidationResult.Success;
    }

    private static string FormatAllowedRanges(int[] ranges)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < ranges.Length / 2; i++)
        {
            if (sb.Length > 0)
                sb.Append(", ");
            sb.Append(ranges[i * 2]).Append('-').Append(ranges[i * 2 + 1]);
        }
        return sb.ToString();
    }

    protected static IEnumerable<int> GetByteLengths(object? value, Encoding encoding)
    {
        if (value == null)
        {
            yield break;
        }

        if (TryGetSingleByteLength(value, encoding, out int length))
        {
            yield return length;
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

