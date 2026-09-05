using System;
using System.Text;
using SharpIpp.Models.Requests;

namespace SharpIpp.Validation;

/// <summary>
/// Validates high-level IPP requests using custom validation attributes recursively.
/// </summary>
public class IppRequestValidator : IIppRequestValidator
{
    /// <summary>
    /// Gets the default instance of the validator.
    /// </summary>
    public static IppRequestValidator Default => new();

    /// <inheritdoc />
    public void Validate<T>(T request) where T : IIppRequest
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var charset = request.OperationAttributes?.AttributesCharset ?? "utf-8";
        Encoding encoding;
        try
        {
            encoding = Encoding.GetEncoding(charset);
        }
        catch
        {
            encoding = Encoding.UTF8;
        }

        IppModelValidator.Validate(request, encoding);
    }
}
