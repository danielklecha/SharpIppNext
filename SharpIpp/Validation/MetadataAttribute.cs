using System;
using System.ComponentModel.DataAnnotations;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class MetadataAttribute : ValidationAttribute
{
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
