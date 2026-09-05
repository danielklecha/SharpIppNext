using System;

namespace SharpIpp.Validation;

/// <summary>
/// Serves as the base class for custom validation attributes in SharpIpp.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public abstract class IppValidationAttribute : Attribute
{
    /// <summary>
    /// Gets or sets an error message to associate with a validation failure.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Validates the specified value with respect to the current validation attribute.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="validationContext">The context information about the validation operation.</param>
    /// <returns>A <see cref="ValidationResult"/> value indicating whether validation succeeded or failed.</returns>
    public abstract ValidationResult? IsValid(object? value, ValidationContext validationContext);
}
