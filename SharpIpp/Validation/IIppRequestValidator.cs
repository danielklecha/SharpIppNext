using SharpIpp.Models.Requests;

namespace SharpIpp.Validation;

/// <summary>
/// Defines a validator for high-level IPP requests using property attributes.
/// </summary>
public interface IIppRequestValidator
{
    /// <summary>
    /// Validates the high-level request object recursively.
    /// </summary>
    /// <typeparam name="T">The request type.</typeparam>
    /// <param name="request">The request to validate.</param>
    void Validate<T>(T request) where T : IIppRequest;
}
