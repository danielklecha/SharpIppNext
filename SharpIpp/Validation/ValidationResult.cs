namespace SharpIpp.Validation;

/// <summary>
/// Represents the result of a validation check.
/// </summary>
public class ValidationResult
{
    public static readonly ValidationResult Success = new(null);

    public string? ErrorMessage { get; }

    public bool IsSuccess => ErrorMessage == null;

    public ValidationResult(string? errorMessage)
    {
        ErrorMessage = errorMessage;
    }
}
