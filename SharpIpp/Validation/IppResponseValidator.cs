using System;
using SharpIpp.Protocol;

namespace SharpIpp.Validation;

/// <summary>
/// Validates high-level IPP responses using custom validation attributes recursively.
/// </summary>
public class IppResponseValidator : IIppResponseValidator
{
    /// <summary>
    /// Gets the default instance of the validator.
    /// </summary>
    public static IppResponseValidator Default => new();

    /// <inheritdoc />
    public void Validate<T>(T response) where T : IIppResponse
    {
        if (response == null)
            throw new ArgumentNullException(nameof(response));

        var encoding = IppEncodingHelper.GetEncoding(response.OperationAttributes?.AttributesCharset);
        IppModelValidator.Validate(response, encoding);
    }
}
