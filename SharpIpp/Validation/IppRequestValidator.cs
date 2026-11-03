using System;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;

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

        var encoding = IppEncodingHelper.GetEncoding(request.OperationAttributes?.AttributesCharset);
        IppModelValidator.Validate(request, encoding);
    }
}
