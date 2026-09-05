using System;

namespace SharpIpp.Validation;

/// <summary>
/// Represents the exception that is thrown when a validation check fails.
/// </summary>
public class ValidationException : Exception
{
    public ValidationException(string message) : base(message)
    {
    }

    public ValidationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
