using System;
using SharpIpp.Models.Responses;

namespace SharpIpp.Protocol.Extensions;

/// <summary>
/// Helpers for PWG 5100.5-2024 Section 8 response expectations for job-related responses.
/// These helpers are opt-in and do not alter the default client or server pipeline behavior.
/// </summary>
public static class IppJobResponseExtensions
{
    public static void Validate(this IIppJobResponse? response)
    {
        if (response is null)
            throw new ArgumentNullException(nameof(response));

        switch (response)
        {
            case PrintJobResponse printJobResponse:
                ValidateOptionalDocumentAttributes(printJobResponse.DocumentAttributes, nameof(response));
                break;

            case PrintUriResponse printUriResponse:
                ValidateOptionalDocumentAttributes(printUriResponse.DocumentAttributes, nameof(response));
                break;

            case SendDocumentResponse sendDocumentResponse:
                ValidateRequiredDocumentAttributes(sendDocumentResponse.DocumentAttributes, nameof(response));
                break;

            case SendUriResponse sendUriResponse:
                ValidateRequiredDocumentAttributes(sendUriResponse.DocumentAttributes, nameof(response));
                break;
        }
    }

    private static void ValidateOptionalDocumentAttributes(DocumentAttributes? documentAttributes, string paramName)
    {
        if (documentAttributes == null)
            return;

        ValidateCommonDocumentAttributes(documentAttributes, paramName);
    }

    private static void ValidateRequiredDocumentAttributes(DocumentAttributes? documentAttributes, string paramName)
    {
        if (documentAttributes == null)
            throw new InvalidOperationException($"{paramName} must include document attributes for Section 8 send operation responses.");

        ValidateCommonDocumentAttributes(documentAttributes, paramName);

        if (documentAttributes.DocumentNumber is null || documentAttributes.DocumentNumber <= 0)
            throw new InvalidOperationException($"{paramName} must include a positive document-number.");
        if (documentAttributes.DocumentState is null)
            throw new InvalidOperationException($"{paramName} must include document-state.");
        if (documentAttributes.DocumentStateReasons == null || documentAttributes.DocumentStateReasons.Length == 0)
            throw new InvalidOperationException($"{paramName} must include document-state-reasons.");
    }

    private static void ValidateCommonDocumentAttributes(DocumentAttributes documentAttributes, string paramName)
    {
        if (documentAttributes.DocumentNumber is <= 0)
            throw new InvalidOperationException($"{paramName} contains an invalid document-number.");
        if (documentAttributes.DocumentStateReasons != null && documentAttributes.DocumentStateReasons.Length == 0)
            throw new InvalidOperationException($"{paramName} contains empty document-state-reasons.");
    }
}
