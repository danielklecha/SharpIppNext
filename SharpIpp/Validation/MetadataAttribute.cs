using System;
using System.Collections;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class MetadataAttribute : IppValidationAttribute
{
    /// <summary>
    /// Validates the specified value with respect to the current validation attribute.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="validationContext">The context information about the validation operation.</param>
    /// <returns>A ValidationResult value indicating whether validation succeeded or failed.</returns>
    public override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }

        if (value is IppStructuredString metadata)
        {
            return ValidateItem(metadata, validationContext);
        }

        if (value is IEnumerable enumerable && !(value is string))
        {
            foreach (var item in enumerable)
            {
                if (item == null)
                {
                    continue;
                }

                var itemResult = ValidateItem(item, validationContext);
                if (!itemResult.IsSuccess)
                {
                    return itemResult;
                }
            }

            return ValidationResult.Success;
        }

        return ValidateItem(value, validationContext);
    }

    private static ValidationResult ValidateItem(object value, ValidationContext validationContext)
    {
        if (value is not IppStructuredString metadata)
        {
            return new ValidationResult($"The field {validationContext.MemberName} must derive from IppStructuredString.");
        }

        if (!metadata.TryValidate(out var error))
        {
            return new ValidationResult($"The field {validationContext.MemberName} {error}");
        }

        return ValidationResult.Success;
    }
}

