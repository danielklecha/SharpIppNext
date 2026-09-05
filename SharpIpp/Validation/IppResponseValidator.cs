using System;
using System.Text;
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

        var charset = response.OperationAttributes?.AttributesCharset ?? "utf-8";
        Encoding encoding;
        try
        {
            encoding = Encoding.GetEncoding(charset);
        }
        catch
        {
            encoding = Encoding.UTF8;
        }

        IppModelValidator.Validate(response, encoding);
    }
}
