using System;
using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol.Extensions;

/// <summary>
/// Helpers for PWG 5100.5-2024 Section 8 document attribute creation from document-creation requests.
/// These helpers are opt-in and do not alter the default client or server pipeline behavior.
/// </summary>
public static class DocumentCreationRequestExtensions
{
    public static DocumentAttributes CreateSection8DocumentAttributes(
        this PrintJobRequest request,
        int? documentNumber = null,
        DocumentState? documentState = null,
        DocumentStateReason[]? documentStateReasons = null,
        string? documentStateMessage = null)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        return CreateDocumentAttributes(
            request.OperationAttributes,
            documentUri: null,
            lastDocument: null,
            documentNumber,
            documentState,
            documentStateReasons,
            documentStateMessage);
    }

    public static DocumentAttributes CreateSection8DocumentAttributes(
        this PrintUriRequest request,
        int? documentNumber = null,
        DocumentState? documentState = null,
        DocumentStateReason[]? documentStateReasons = null,
        string? documentStateMessage = null)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        return CreateDocumentAttributes(
            request.OperationAttributes,
            documentUri: request.OperationAttributes?.DocumentUri?.ToString(),
            lastDocument: null,
            documentNumber,
            documentState,
            documentStateReasons,
            documentStateMessage);
    }

    public static DocumentAttributes CreateSection8DocumentAttributes(
        this SendDocumentRequest request,
        int? documentNumber = null,
        DocumentState? documentState = null,
        DocumentStateReason[]? documentStateReasons = null,
        string? documentStateMessage = null)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        return CreateDocumentAttributes(
            request.OperationAttributes,
            documentUri: null,
            lastDocument: request.OperationAttributes?.LastDocument,
            documentNumber,
            documentState,
            documentStateReasons,
            documentStateMessage);
    }

    public static DocumentAttributes CreateSection8DocumentAttributes(
        this SendUriRequest request,
        int? documentNumber = null,
        DocumentState? documentState = null,
        DocumentStateReason[]? documentStateReasons = null,
        string? documentStateMessage = null)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        return CreateDocumentAttributes(
            request.OperationAttributes,
            documentUri: request.OperationAttributes?.DocumentUri?.ToString(),
            lastDocument: request.OperationAttributes?.LastDocument,
            documentNumber,
            documentState,
            documentStateReasons,
            documentStateMessage);
    }

    private static DocumentAttributes CreateDocumentAttributes(
        PrintJobOperationAttributes? operationAttributes,
        string? documentUri,
        bool? lastDocument,
        int? documentNumber,
        DocumentState? documentState,
        DocumentStateReason[]? documentStateReasons,
        string? documentStateMessage)
    {
        if (operationAttributes == null)
            throw new ArgumentNullException(nameof(operationAttributes));

        return new DocumentAttributes
        {
            AttributesCharset = operationAttributes.AttributesCharset,
            AttributesNaturalLanguage = operationAttributes.AttributesNaturalLanguage,
            DocumentCharset = operationAttributes.DocumentCharset,
            DocumentFormat = operationAttributes.DocumentFormat,
            DocumentFormatDetected = operationAttributes.DocumentFormat,
            DocumentMessage = operationAttributes.DocumentMessage,
            DocumentName = operationAttributes.DocumentName,
            DocumentNaturalLanguage = operationAttributes.DocumentNaturalLanguage,
            DocumentUri = documentUri,
            Impressions = operationAttributes.JobImpressions,
            KOctets = operationAttributes.JobKOctets,
            MediaSheets = operationAttributes.JobMediaSheets,
            LastDocument = lastDocument,
            DocumentNumber = documentNumber,
            DocumentState = documentState,
            DocumentStateReasons = documentStateReasons,
            DocumentStateMessage = documentStateMessage,
        };
    }

    private static DocumentAttributes CreateDocumentAttributes(
        SendDocumentOperationAttributes? operationAttributes,
        string? documentUri,
        bool? lastDocument,
        int? documentNumber,
        DocumentState? documentState,
        DocumentStateReason[]? documentStateReasons,
        string? documentStateMessage)
    {
        if (operationAttributes == null)
            throw new ArgumentNullException(nameof(operationAttributes));

        return new DocumentAttributes
        {
            AttributesCharset = operationAttributes.AttributesCharset,
            AttributesNaturalLanguage = operationAttributes.AttributesNaturalLanguage,
            DocumentCharset = operationAttributes.DocumentCharset,
            DocumentFormat = operationAttributes.DocumentFormat,
            DocumentFormatDetected = operationAttributes.DocumentFormat,
            DocumentMessage = operationAttributes.DocumentMessage,
            DocumentName = operationAttributes.DocumentName,
            DocumentNaturalLanguage = operationAttributes.DocumentNaturalLanguage,
            DocumentUri = documentUri,
            LastDocument = lastDocument,
            DocumentNumber = documentNumber,
            DocumentState = documentState,
            DocumentStateReasons = documentStateReasons,
            DocumentStateMessage = documentStateMessage,
        };
    }
}
