using System;
using System.ComponentModel.DataAnnotations;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class MetadataAttribute : ValidationAttribute
{
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

        if (value is not IppStructuredString metadata)
        {
            return new ValidationResult($"The field {validationContext.DisplayName} must derive from IppStructuredString.");
        }

        try
        {
            metadata.Validate();
            return ValidationResult.Success;
        }
        catch (ValidationException ex)
        {
            return new ValidationResult($"The field {validationContext.DisplayName} {ex.Message}");
        }
    }
}
