using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Exceptions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol;

public class IppRequestValidator : IIppRequestValidator
{
    private const string NotifyEventsAttributeName = "notify-events";
    private const string AllRequestedAttributesGroup = "all";
    private const string OverridePagesMemberName = "pages";
    private const string OverrideDocumentNumbersMemberName = "document-numbers";
    private const string OverrideDocumentCopiesMemberName = "document-copies";

    private static readonly HashSet<string> Pwg51005DocumentRequestedAttributeGroupKeywords =
    [
        AllRequestedAttributesGroup,
        DocumentAttribute.DocumentDescription,
        DocumentAttribute.DocumentTemplate,
    ];

    private static readonly HashSet<string> Pwg51008JobRequestedAttributeGroupKeywords =
    [
        AllRequestedAttributesGroup,
        "job-description",
        "job-template",
        "job-actual",
    ];

    private static readonly string[] Pwg51003MediaExclusiveCollections =
    [
        JobAttribute.CoverBack,
        JobAttribute.CoverFront,
        JobAttribute.InsertSheet,
        JobAttribute.JobAccountingSheets,
        JobAttribute.JobErrorSheet,
        JobAttribute.SeparatorSheets,
    ];

    private static readonly string[] OverrideSelectorMemberNames =
    [
        OverridePagesMemberName,
        OverrideDocumentNumbersMemberName,
        OverrideDocumentCopiesMemberName,
    ];

    private static readonly HashSet<IppOperation> Pwg51006OverridesOperations =
    [
        IppOperation.PrintJob,
        IppOperation.PrintUri,
        IppOperation.ValidateJob,
        IppOperation.CreateJob,
        IppOperation.SetJobAttributes,
        IppOperation.SendDocument,
        IppOperation.SendUri,
    ];

    private static readonly HashSet<string> Pwg51006NonOverrideScopeMembers =
    [
        JobAttribute.Copies,
        JobAttribute.CoverBack,
        JobAttribute.CoverFront,
        JobAttribute.InsertSheet,
        JobAttribute.JobAccountId,
        JobAttribute.JobAccountingSheets,
        JobAttribute.JobAccountingUserId,
        JobAttribute.JobErrorSheet,
        JobAttribute.JobHoldUntil,
        JobAttribute.JobMessageToOperator,
        JobAttribute.JobPriority,
        JobAttribute.JobSheets,
        JobAttribute.JobSheetsCol,
        JobAttribute.MediaInputTrayCheck,
        JobAttribute.MultipleDocumentHandling,
        JobAttribute.OutputBin,
        JobAttribute.PageDelivery,
        JobAttribute.PageRanges,
        JobAttribute.SeparatorSheets,
        "sheet-collate",
    ];

    private static readonly HashSet<string> Pwg51017ForbiddenDestinationUriSchemes =
    [
        "tel",
        "fax",
        "sip",
        "sips",
    ];

    private static readonly HashSet<string> Pwg51017ForbiddenDestinationUriMembers =
    [
        "post-dial-string",
        "pre-dial-string",
        "t33-subaddress",
    ];

    private static readonly HashSet<string> Pwg51017ForbiddenDestinationAttributesMembers =
    [
        JobAttribute.DocumentPassword,
        JobAttribute.JobPassword,
        JobAttribute.JobPasswordEncryption,
    ];

    public static IppRequestValidator Default => new()
    {
        ValidateCoreRules = true,
        ValidateOperationSpecificRules = true,
        ValidateOperationAttributesGroup = true,
        ValidateJobAttributesGroup = true,
        ValidatePrinterAttributesGroup = true,
        ValidateUnsupportedAttributesGroup = true,
        ValidateSubscriptionAttributesGroup = true,
        ValidateEventNotificationAttributesGroup = true,
        ValidateResourceAttributesGroup = true,
        ValidateDocumentAttributesGroup = true,
        ValidateSystemAttributesGroup = true,
        UseIppAttributeFidelityForCapabilityValidation = false,
    };

    public IppRequestValidationContext Context { get; } = new();

    public bool ValidateCoreRules { get; set; } = true;

    public bool ValidateOperationSpecificRules { get; set; } = true;

    public bool ValidateOperationAttributesGroup { get; set; } = true;

    public bool ValidateJobAttributesGroup { get; set; } = true;

    public bool ValidatePrinterAttributesGroup { get; set; } = true;

    public bool ValidateUnsupportedAttributesGroup { get; set; } = true;

    public bool ValidateSubscriptionAttributesGroup { get; set; } = true;

    public bool ValidateEventNotificationAttributesGroup { get; set; } = true;

    public bool ValidateResourceAttributesGroup { get; set; } = true;

    public bool ValidateDocumentAttributesGroup { get; set; } = true;

    public bool ValidateSystemAttributesGroup { get; set; } = true;

    public bool UseIppAttributeFidelityForCapabilityValidation { get; set; }

    public void Validate(IIppRequestMessage? request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        if (ValidateCoreRules)
            ValidateCore(request);

        if (ValidateJobAttributesGroup)
            ValidateJobAttributes(request);

        if (ValidateDocumentAttributesGroup)
            ValidateDocumentAttributes(request);

        if (ValidatePrinterAttributesGroup)
            ValidatePrinterAttributes(request);

        if (ValidateOperationSpecificRules)
            ValidateOperationRules(request);
    }

    private void ValidateCore(IIppRequestMessage request)
    {
        if (request.RequestId <= 0)
            throw new IppRequestException("Bad request-id value", request, IppStatusCode.ClientErrorBadRequest);

        if (request.Version < new IppVersion(1, 0))
            throw new IppRequestException("Unsupported IPP version", request, IppStatusCode.ServerErrorVersionNotSupported);

        if (!ValidateOperationAttributesGroup)
            return;

        var operationAttributes = request.OperationAttributes;

        if (!operationAttributes.Any())
            throw new IppRequestException("No Operation Attributes", request, IppStatusCode.ClientErrorBadRequest);
        if (operationAttributes.First().Name != JobAttribute.AttributesCharset)
            throw new IppRequestException("attributes-charset MUST be the first attribute", request, IppStatusCode.ClientErrorBadRequest);
        if (operationAttributes.ElementAtOrDefault(1).Name != JobAttribute.AttributesNaturalLanguage)
            throw new IppRequestException("attributes-natural-language MUST be the second attribute", request, IppStatusCode.ClientErrorBadRequest);

        var hasPrinterUri = HasNamedAttribute(operationAttributes, JobAttribute.PrinterUri);
        var hasSystemUri = HasNamedAttribute(operationAttributes, SystemAttribute.SystemUri);
        var hasJobUri = HasNamedAttribute(operationAttributes, JobAttribute.JobUri);
        if (!hasPrinterUri && !hasSystemUri && !(hasJobUri && request.IppOperation.IsPwg51005DocumentTargetOperation()))
            throw new IppRequestException("No printer-uri or system-uri operation attribute", request, IppStatusCode.ClientErrorBadRequest);

        if (request.IppOperation.IsSystemServiceOperation() && !hasSystemUri)
            throw new IppRequestException("No system-uri operation attribute", request, IppStatusCode.ClientErrorBadRequest);
    }

    private void ValidateJobAttributes(IIppRequestMessage request)
    {
        ValidateFinishingsMutualExclusivity(request.JobAttributes, request);

        ValidateOutputBinAttributes(request.JobAttributes, request);
        ValidateCollectionMediaSelectionRules(request.JobAttributes, request);

        ValidateOverridesRules(request);
    }

    private void ValidateDocumentAttributes(IIppRequestMessage request)
    {
        ValidateFinishingsMutualExclusivity(request.DocumentAttributes, request);

        ValidateOutputBinAttributes(request.DocumentAttributes, request);
        ValidateCollectionMediaSelectionRules(request.DocumentAttributes, request);
    }

    private void ValidatePrinterAttributes(IIppRequestMessage request)
    {
        var destinationUriReadyCollections = EnumerateNamedCollections(request.PrinterAttributes, PrinterAttribute.DestinationUriReady).ToArray();
        if (!destinationUriReadyCollections.Any())
            return;

        foreach (var destinationUriReadyCollection in destinationUriReadyCollections)
        {
            if (destinationUriReadyCollection.Count == 1 && destinationUriReadyCollection[0].Tag.IsOutOfBand())
                continue;

            var destinationUriReadyMembers = destinationUriReadyCollection
                .FromBegCollection()
                .ToArray();

            var hasDestinationOAuthToken = destinationUriReadyMembers.Any(x => x.Name == "destination-oauth-token" && x.Tag != Tag.EndCollection);
            if (destinationUriReadyMembers.Any(x => x.Name == "destination-oauth-scope" && x.Tag != Tag.EndCollection) && !hasDestinationOAuthToken)
                throw new IppRequestException("invalid destination-uri-ready: destination-oauth-scope requires destination-oauth-token", request, IppStatusCode.ClientErrorBadRequest);

            if (destinationUriReadyMembers.Any(x => x.Name == "destination-oauth-uri" && x.Tag != Tag.EndCollection) && !hasDestinationOAuthToken)
                throw new IppRequestException("invalid destination-uri-ready: destination-oauth-uri requires destination-oauth-token", request, IppStatusCode.ClientErrorBadRequest);

            var unsupportedForbiddenMembers = destinationUriReadyMembers
                .Where(x => x.Name == "destination-attributes-supported" && x.Tag != Tag.EndCollection)
                .Select(x => x.Value?.ToString())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Where(x => x != null && Pwg51017ForbiddenDestinationAttributesMembers.Contains(x))
                .ToArray();

            if (unsupportedForbiddenMembers.Any())
                throw new IppRequestException("invalid destination-uri-ready: destination-attributes-supported MUST NOT include password attributes", request, IppStatusCode.ClientErrorBadRequest);

            var destinationAttributesCollections = EnumerateNamedCollections(destinationUriReadyMembers, "destination-attributes").ToArray();
            foreach (var destinationAttributesCollection in destinationAttributesCollections)
            {
                if (destinationAttributesCollection.Count == 1 && destinationAttributesCollection[0].Tag.IsOutOfBand())
                    continue;

                var destinationAttributesMembers = destinationAttributesCollection
                    .FromBegCollection()
                    .Where(x => x.Tag != Tag.EndCollection)
                    .ToArray();

                if (destinationAttributesMembers.Any(x => !string.IsNullOrWhiteSpace(x.Name) && Pwg51017ForbiddenDestinationAttributesMembers.Contains(x.Name)))
                    throw new IppRequestException("invalid destination-uri-ready: destination-attributes MUST NOT include password attributes", request, IppStatusCode.ClientErrorBadRequest);
            }
        }
    }

    private static void ValidateFinishingsMutualExclusivity(IReadOnlyCollection<IppAttribute> attributes, IIppRequestMessage request)
    {
        var hasFinishings = HasNamedAttribute(attributes, JobAttribute.Finishings);
        var hasFinishingsCol = HasNamedAttribute(attributes, JobAttribute.FinishingsCol);
        if (hasFinishings && hasFinishingsCol)
            throw new IppRequestException("'finishings' and 'finishings-col' are conflicting attributes and cannot be supplied together.", request, IppStatusCode.ClientErrorBadRequest);
    }

    private void ValidateCollectionMediaSelectionRules(IReadOnlyCollection<IppAttribute> attributes, IIppRequestMessage request)
    {
        foreach (var collectionName in Pwg51003MediaExclusiveCollections)
        {
            var collections = EnumerateNamedCollections(attributes, collectionName).ToArray();
            foreach (var collection in collections)
            {
                if (collection.Count == 0 || collection[0].Tag.IsOutOfBand())
                    continue;

                try
                {
                    var members = collection
                        .FromBegCollection()
                        .Where(x => x.Tag != Tag.EndCollection)
                        .ToArray();

                    ValidateCollectionMemberConflicts(collectionName, members, request);
                }
                catch (ArgumentException)
                {
                    throw new IppRequestException(
                        $"invalid {collectionName} collection encoding",
                        request,
                        IppStatusCode.ClientErrorBadRequest);
                }
            }
        }
    }

    private static void ValidateCollectionMemberConflicts(string collectionName, IReadOnlyCollection<IppAttribute> members, IIppRequestMessage request)
    {
        var hasMedia = members.Any(x => x.Name == JobAttribute.Media);
        var hasMediaCol = members.Any(x => x.Name == JobAttribute.MediaCol);

        if (!hasMedia || !hasMediaCol)
            return;

        throw new IppRequestException(
            $"invalid {collectionName}: 'media' and 'media-col' member attributes are mutually exclusive",
            request,
            IppStatusCode.ClientErrorConflictingAttributes);
    }

    private void ValidateOutputBinAttributes(IReadOnlyCollection<IppAttribute> attributes, IIppRequestMessage request)
    {
        var outputBins = attributes.Where(x => x.Name == JobAttribute.OutputBin).ToArray();

        if (outputBins.Length <= 0)
            return;

        if (outputBins.Length > 1)
            throw new IppRequestException("'output-bin' MUST be single-valued.", request, IppStatusCode.ClientErrorBadRequest);

        var outputBinTag = outputBins[0].Tag;
        if (outputBinTag != Tag.Keyword && outputBinTag != Tag.NameWithoutLanguage)
            throw new IppRequestException("'output-bin' MUST use 'keyword' or 'nameWithoutLanguage' syntax.", request, IppStatusCode.ClientErrorBadRequest);

        ValidateOutputBinAgainstSupportedValues(outputBins[0], request);
    }

    private void ValidateOutputBinAgainstSupportedValues(IppAttribute outputBin, IIppRequestMessage request)
    {
        var outputBinSupported = Context.OutputBinSupported;
        if (outputBinSupported is null || outputBinSupported.Count == 0)
            return;

        if (UseIppAttributeFidelityForCapabilityValidation && !IsIppAttributeFidelityTrue(request))
            return;

        var value = outputBin.Value?.ToString();
        if (string.IsNullOrWhiteSpace(value))
            return;

        var isKeyword = outputBin.Tag == Tag.Keyword;
        var isSupported = outputBinSupported.Any(x =>
            x.IsKeyword == isKeyword &&
            string.Equals(x.Value, value, StringComparison.Ordinal));

        if (isSupported)
            return;

        throw new IppRequestException(
            $"invalid output-bin: value is not supported by target printer: {value}",
            request,
            IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    private void ValidateOperationRules(IIppRequestMessage request)
    {
        var operationAttributes = request.OperationAttributes;
        var hasDocumentNumber = ValidateOperationAttributesGroup && HasNamedAttribute(operationAttributes, DocumentAttribute.DocumentNumber);
        var hasLastDocument = ValidateOperationAttributesGroup && HasNamedAttribute(operationAttributes, JobAttribute.LastDocument);
        var hasDocumentUri = ValidateOperationAttributesGroup && HasNamedAttribute(operationAttributes, JobAttribute.DocumentUri);

        var documentNumber = ReadOperationAttributeAs<int>(operationAttributes, DocumentAttribute.DocumentNumber);
        var lastDocument = ReadOperationAttributeAs<bool>(operationAttributes, JobAttribute.LastDocument);

        switch (request.IppOperation)
        {
            case IppOperation.CancelDocument:
            case IppOperation.GetDocumentAttributes:
                ValidateRequiredDocumentNumber(hasDocumentNumber, documentNumber, request);

                if (request.IppOperation == IppOperation.CancelDocument && documentNumber is int cancelDocumentNumber)
                    ValidateCancelDocumentStateRules(cancelDocumentNumber, request);

                break;

            case IppOperation.SetDocumentAttributes:
                ValidateRequiredDocumentNumber(hasDocumentNumber, documentNumber, request);

                if (ValidateDocumentAttributesGroup && !request.DocumentAttributes.Any())
                    throw new IppRequestException("missing document attributes", request, IppStatusCode.ClientErrorBadRequest);

                if (documentNumber is int setDocumentNumber)
                    ValidateSetDocumentAttributesStateRules(setDocumentNumber, request);

                break;

            case IppOperation.PrintJob:
                if (request.Document == null)
                    throw new IppRequestException("document stream required", request, IppStatusCode.ClientErrorBadRequest);
                break;

            case IppOperation.PrintUri:
                if (ValidateOperationAttributesGroup && !hasDocumentUri)
                    throw new IppRequestException("missing document-uri", request, IppStatusCode.ClientErrorBadRequest);
                break;

            case IppOperation.SendDocument:
                ValidateRequiredLastDocument(hasLastDocument, lastDocument, request);
                if (ValidateOperationAttributesGroup && lastDocument == false && request.Document == null)
                    throw new IppRequestException("document stream required when last-document=false", request, IppStatusCode.ClientErrorBadRequest);
                break;

            case IppOperation.SendUri:
                ValidateRequiredLastDocument(hasLastDocument, lastDocument, request);
                if (ValidateOperationAttributesGroup && lastDocument == false && !hasDocumentUri)
                    throw new IppRequestException("missing document-uri", request, IppStatusCode.ClientErrorBadRequest);
                break;

            case IppOperation.ValidateJob:
            case IppOperation.ValidateDocument:
                if (ValidateOperationAttributesGroup && HasNamedAttribute(operationAttributes, JobAttribute.DocumentPassword))
                    throw new IppRequestException("document-password is not allowed for validate operations", request, IppStatusCode.ClientErrorBadRequest);
                break;

            case IppOperation.CreateJob:
                ValidateCreateJobDestinationRules(operationAttributes, request);
                break;
        }

        ValidateJobRequestedAttributesGroupKeywords(request);
        ValidateDocumentRequestedAttributesGroupKeywords(request);
        ValidateNotifyEventsValues(request);
    }

    private void ValidateCreateJobDestinationRules(IReadOnlyCollection<IppAttribute> operationAttributes, IIppRequestMessage request)
    {
        if (!ValidateOperationAttributesGroup)
            return;

        var destinationUriCollections = EnumerateNamedCollections(operationAttributes, JobAttribute.DestinationUris).ToArray();
        if (!destinationUriCollections.Any())
            return;

        foreach (var destinationUriCollection in destinationUriCollections)
        {
            if (destinationUriCollection.Count == 1 && destinationUriCollection[0].Tag.IsOutOfBand())
                continue;

            var destinationUriMembers = destinationUriCollection
                .FromBegCollection()
                .Where(x => x.Tag != Tag.EndCollection)
                .ToArray();

            if (destinationUriMembers.Any(x => Pwg51017ForbiddenDestinationUriMembers.Contains(x.Name)))
                throw new IppRequestException("invalid destination-uris: reserved fax member attributes are not allowed for Scan", request, IppStatusCode.ClientErrorBadRequest);

            foreach (var destinationUriMember in destinationUriMembers.Where(x => x.Name == "destination-uri"))
            {
                var destinationUri = destinationUriMember.Value?.ToString();
                if (destinationUri is null || destinationUri.Length == 0)
                    continue;

                string? scheme = null;
                Uri? parsedUri;
                if (Uri.TryCreate(destinationUri, UriKind.Absolute, out parsedUri))
                {
                    scheme = parsedUri?.Scheme;
                }
                else
                {
                    var schemeSeparatorIndex = destinationUri.IndexOf(':');
                    if (schemeSeparatorIndex > 0)
                        scheme = destinationUri.Substring(0, schemeSeparatorIndex);
                }

                string? normalizedScheme = null;
                if (scheme != null && scheme.Length > 0)
                    normalizedScheme = scheme.ToLowerInvariant();
                if (normalizedScheme != null && Pwg51017ForbiddenDestinationUriSchemes.Contains(normalizedScheme))
                    throw new IppRequestException("invalid destination-uri scheme for Scan", request, IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
            }
        }

        var destinationAccessesCollections = EnumerateNamedCollections(operationAttributes, JobAttribute.DestinationAccesses).ToArray();
        if (destinationAccessesCollections.Length > 0 && destinationAccessesCollections.Length != destinationUriCollections.Length)
            throw new IppRequestException("destination-accesses cardinality MUST match destination-uris", request, IppStatusCode.ClientErrorBadRequest);
    }

    private void ValidateJobRequestedAttributesGroupKeywords(IIppRequestMessage request)
    {
        if (!ValidateOperationAttributesGroup)
            return;

        if (request.IppOperation is not (IppOperation.GetJobAttributes or IppOperation.GetJobs))
            return;

        var supported = Context.JobRequestedAttributeGroupKeywordsSupported;
        if (supported is null || supported.Count == 0)
            return;

        var requestedAttributes = request.OperationAttributes
            .Where(x => x.Name == JobAttribute.RequestedAttributes)
            .Select(x => x.Value?.ToString())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Cast<string>()
            .ToArray();

        if (requestedAttributes.Length == 0)
            return;

        var supportedSet = new HashSet<string>(supported, StringComparer.Ordinal);
        var unsupportedGroupKeywords = requestedAttributes
            .Where(Pwg51008JobRequestedAttributeGroupKeywords.Contains)
            .Where(x => !supportedSet.Contains(x))
            .Distinct(StringComparer.Ordinal)
            .ToArray();

        if (unsupportedGroupKeywords.Length == 0)
            return;

        if (UseIppAttributeFidelityForCapabilityValidation && !IsIppAttributeFidelityTrue(request))
            return;

        throw new IppRequestException(
            $"requested-attributes group value(s) not supported: {string.Join(", ", unsupportedGroupKeywords)}",
            request,
            IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    private void ValidateRequiredDocumentNumber(bool hasDocumentNumber, int? documentNumber, IIppRequestMessage request)
    {
        if (!ValidateOperationAttributesGroup)
            return;

        if (!hasDocumentNumber)
            throw new IppRequestException("missing document-number", request, IppStatusCode.ClientErrorBadRequest);
        if (documentNumber is null || documentNumber <= 0)
            throw new IppRequestException("invalid document-number", request, IppStatusCode.ClientErrorBadRequest);
    }

    private void ValidateRequiredLastDocument(bool hasLastDocument, bool? lastDocument, IIppRequestMessage request)
    {
        if (!ValidateOperationAttributesGroup)
            return;

        if (!hasLastDocument)
            throw new IppRequestException("missing last-document", request, IppStatusCode.ClientErrorBadRequest);
        if (lastDocument is null)
            throw new IppRequestException("invalid last-document", request, IppStatusCode.ClientErrorBadRequest);
    }

    private static bool HasNamedAttribute(IEnumerable<IppAttribute> attributes, string name)
    {
        return attributes.Any(x => x.Name == name);
    }

    private static T? ReadOperationAttributeAs<T>(IEnumerable<IppAttribute> operationAttributes, string name) where T : struct
    {
        var attribute = operationAttributes.FirstOrDefault(x => x.Name == name);
        if (attribute.Value is T typed)
            return typed;

        return null;
    }

    private void ValidateDocumentRequestedAttributesGroupKeywords(IIppRequestMessage request)
    {
        if (!ValidateOperationAttributesGroup)
            return;

        if (request.IppOperation is not (IppOperation.GetDocumentAttributes or IppOperation.GetDocuments))
            return;

        var supported = Context.DocumentRequestedAttributeGroupKeywordsSupported;
        if (supported is null || supported.Count == 0)
            return;

        var requestedAttributes = request.OperationAttributes
            .Where(x => x.Name == JobAttribute.RequestedAttributes)
            .Select(x => x.Value?.ToString())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Cast<string>()
            .ToArray();

        if (requestedAttributes.Length == 0)
            return;

        var supportedSet = new HashSet<string>(supported, StringComparer.Ordinal);
        var unsupportedGroupKeywords = requestedAttributes
            .Where(Pwg51005DocumentRequestedAttributeGroupKeywords.Contains)
            .Where(x => !supportedSet.Contains(x))
            .Distinct(StringComparer.Ordinal)
            .ToArray();

        if (unsupportedGroupKeywords.Length == 0)
            return;

        if (UseIppAttributeFidelityForCapabilityValidation && !IsIppAttributeFidelityTrue(request))
            return;

        throw new IppRequestException(
            $"requested-attributes group value(s) not supported: {string.Join(", ", unsupportedGroupKeywords)}",
            request,
            IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    private void ValidateNotifyEventsValues(IIppRequestMessage request)
    {
        if (!ValidateSubscriptionAttributesGroup)
            return;

        var supported = Context.NotifyEventsSupported;
        if (supported is null || supported.Count == 0)
            return;

        var supportedSet = new HashSet<string>(supported, StringComparer.Ordinal);
        var notifyEvents = request.OperationAttributes
            .Concat(request.SubscriptionAttributes)
            .Where(x => x.Name == NotifyEventsAttributeName)
            .Select(x => x.Value?.ToString())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Cast<string>()
            .ToArray();

        if (notifyEvents.Length == 0)
            return;

        var unsupportedNotifyEvents = notifyEvents
            .Where(x => !supportedSet.Contains(x))
            .Distinct(StringComparer.Ordinal)
            .ToArray();

        if (unsupportedNotifyEvents.Length == 0)
            return;

        if (UseIppAttributeFidelityForCapabilityValidation && !IsIppAttributeFidelityTrue(request))
            return;

        throw new IppRequestException(
            $"notify-events value(s) not supported: {string.Join(", ", unsupportedNotifyEvents)}",
            request,
            IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    private void ValidateCancelDocumentStateRules(int documentNumber, IIppRequestMessage request)
    {
        if (Context.DocumentStatesByNumber == null)
            return;

        if (!Context.DocumentStatesByNumber.TryGetValue(documentNumber, out var documentState))
            return;

        if (documentState is DocumentState.Completed or DocumentState.Canceled or DocumentState.Aborted)
            throw new IppRequestException("invalid document-state for Cancel-Document", request, IppStatusCode.ClientErrorNotPossible);

        if (documentState != DocumentState.Processing)
            return;

        if (Context.DocumentStateReasonsByNumber == null)
            return;

        if (!Context.DocumentStateReasonsByNumber.TryGetValue(documentNumber, out var stateReasons) || stateReasons == null)
            return;

        if (stateReasons.Contains(DocumentStateReason.ProcessingToStopPoint))
            throw new IppRequestException("invalid document-state-reasons for Cancel-Document", request, IppStatusCode.ClientErrorNotPossible);
    }

    private void ValidateSetDocumentAttributesStateRules(int documentNumber, IIppRequestMessage request)
    {
        if (Context.DocumentStatesByNumber == null)
            return;

        if (!Context.DocumentStatesByNumber.TryGetValue(documentNumber, out var documentState))
            return;

        if (documentState is DocumentState.Completed or DocumentState.Canceled or DocumentState.Aborted)
            throw new IppRequestException("invalid document-state for Set-Document-Attributes", request, IppStatusCode.ClientErrorNotPossible);

        if (documentState == DocumentState.Processing && !Context.AllowSetDocumentAttributesWhenProcessing)
            throw new IppRequestException("invalid document-state for Set-Document-Attributes", request, IppStatusCode.ClientErrorNotPossible);
    }

    private void ValidateOverridesRules(IIppRequestMessage request)
    {
        var overrideCollections = EnumerateNamedCollections(request.JobAttributes, JobAttribute.Overrides).ToArray();
        if (!overrideCollections.Any())
            return;

        var supportedOperations = Context.OverridesSupportedOperations;
        if (supportedOperations is not null && supportedOperations.Count > 0)
        {
            if (!supportedOperations.Contains(request.IppOperation))
            {
                if (UseIppAttributeFidelityForCapabilityValidation && !IsIppAttributeFidelityTrue(request))
                    return;

                throw new IppRequestException("invalid overrides: attribute is not supported for this operation by target printer", request, IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
            }
        }
        else if (!Pwg51006OverridesOperations.Contains(request.IppOperation))
        {
            throw new IppRequestException("invalid overrides: attribute is not allowed for this operation", request, IppStatusCode.ClientErrorBadRequest);
        }

        try
        {
            var effectiveDocumentRangesByCollection = new List<SharpIpp.Protocol.Models.Range[]>();

            foreach (var collection in overrideCollections)
            {
                var first = collection.FirstOrDefault();
                if (first.Tag.IsOutOfBand())
                    continue;

                var members = collection
                    .FromBegCollection()
                    .Where(x => x.Tag != Tag.EndCollection)
                    .ToArray();

                var pageRanges = ReadSelectorRanges(members, OverridePagesMemberName, required: true, request);
                var documentNumberRanges = ReadSelectorRanges(members, OverrideDocumentNumbersMemberName, required: false, request);
                var documentCopyRanges = ReadSelectorRanges(members, OverrideDocumentCopiesMemberName, required: false, request);

                ValidateSelectorOrder(members, hasDocumentNumbers: documentNumberRanges != null, hasDocumentCopies: documentCopyRanges != null, request);
                ValidateRangesAscendingNonOverlapping(pageRanges!, OverridePagesMemberName, request);

                if (documentNumberRanges != null)
                    ValidateRangesAscendingNonOverlapping(documentNumberRanges, OverrideDocumentNumbersMemberName, request);

                if (documentCopyRanges != null)
                    ValidateRangesAscendingNonOverlapping(documentCopyRanges, OverrideDocumentCopiesMemberName, request);

                var hasOverrideAttributes = members.Any(x => !IsSelectorMember(x.Name));
                if (!hasOverrideAttributes)
                    throw new IppRequestException("invalid overrides: each collection must contain at least one overriding Job Template attribute", request, IppStatusCode.ClientErrorBadRequest);

                ValidateOverrideMembersAgainstSupportedValues(members, request);

                effectiveDocumentRangesByCollection.Add(documentNumberRanges ?? [new SharpIpp.Protocol.Models.Range(1, int.MaxValue)]);
            }

            ValidateOverrideDocumentNumbersAcrossCollections(effectiveDocumentRangesByCollection, request);
        }
        catch (ArgumentException)
        {
            throw new IppRequestException("invalid overrides collection encoding", request, IppStatusCode.ClientErrorBadRequest);
        }
    }

    private void ValidateOverrideMembersAgainstSupportedValues(IReadOnlyCollection<IppAttribute> members, IIppRequestMessage request)
    {
        var memberNames = EnumerateTopLevelOverrideMemberNames(members)
            .Distinct(StringComparer.Ordinal)
            .ToArray();

        var unsupportedMembers = new List<string>();

        var overrideMemberScopesByName = Context.OverrideMemberScopesByName;
        if (overrideMemberScopesByName is not null && overrideMemberScopesByName.Count > 0)
        {
            unsupportedMembers.AddRange(memberNames.Where(x =>
                overrideMemberScopesByName.TryGetValue(x, out var scope) &&
                (scope == OverrideMemberScope.Job || scope == OverrideMemberScope.Document)));
        }
        else
        {
            unsupportedMembers.AddRange(memberNames.Where(Pwg51006NonOverrideScopeMembers.Contains));
        }

        var overridesSupported = Context.OverridesSupported;
        if (overridesSupported is not null && overridesSupported.Count > 0)
        {
            var supportedMemberNames = new HashSet<string>(overridesSupported.Select(x => x.Value), StringComparer.Ordinal);
            if (supportedMemberNames.Count > 0)
            {
                unsupportedMembers.AddRange(memberNames.Where(x => !supportedMemberNames.Contains(x)));
            }
        }

        var distinctUnsupportedMembers = unsupportedMembers
            .Distinct(StringComparer.Ordinal)
            .ToArray();

        if (distinctUnsupportedMembers.Length == 0)
            return;

        if (UseIppAttributeFidelityForCapabilityValidation && !IsIppAttributeFidelityTrue(request))
            return;

        throw new IppRequestException(
            $"invalid overrides: member(s) not supported by target printer: {string.Join(", ", distinctUnsupportedMembers)}",
            request,
            IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    private static bool IsIppAttributeFidelityTrue(IIppRequestMessage request)
    {
        var fidelityAttribute = request.OperationAttributes.FirstOrDefault(x => x.Name == JobAttribute.IppAttributeFidelity);
        return fidelityAttribute.Value is bool value && value;
    }

    private static IEnumerable<string> EnumerateTopLevelOverrideMemberNames(IEnumerable<IppAttribute> members)
    {
        var nestedCollectionDepth = 0;
        foreach (var member in members)
        {
            if (member.Tag == Tag.EndCollection)
            {
                if (nestedCollectionDepth > 0)
                    nestedCollectionDepth--;

                continue;
            }

            if (nestedCollectionDepth == 0 && !string.IsNullOrEmpty(member.Name))
                yield return member.Name;

            if (member.Tag == Tag.BegCollection)
                nestedCollectionDepth++;
        }
    }

    private static IEnumerable<IReadOnlyList<IppAttribute>> EnumerateNamedCollections(IEnumerable<IppAttribute> attributes, string collectionName)
    {
        List<IppAttribute>? current = null;
        var level = 0;

        foreach (var attribute in attributes)
        {
            if (current == null)
            {
                if (attribute.Name != collectionName)
                    continue;

                if (attribute.Tag.IsOutOfBand())
                {
                    yield return [attribute];
                    continue;
                }

                if (attribute.Tag != Tag.BegCollection)
                    continue;

                current = [attribute];
                level = 1;
                continue;
            }

            current.Add(attribute);

            if (attribute.Tag == Tag.BegCollection)
            {
                level++;
            }
            else if (attribute.Tag == Tag.EndCollection)
            {
                level--;
                if (level == 0)
                {
                    yield return current.ToArray();
                    current = null;
                }
            }
        }
    }

    private static bool IsSelectorMember(string memberName)
    {
        return OverrideSelectorMemberNames.Contains(memberName);
    }

    private static SharpIpp.Protocol.Models.Range[]? ReadSelectorRanges(IReadOnlyCollection<IppAttribute> members, string memberName, bool required, IIppRequestMessage request)
    {
        var selector = members.Where(x => x.Name == memberName).ToArray();
        if (selector.Length == 0)
        {
            if (required)
                throw new IppRequestException($"invalid overrides: missing required '{memberName}' member", request, IppStatusCode.ClientErrorBadRequest);
            return null;
        }

        var ranges = new List<SharpIpp.Protocol.Models.Range>(selector.Length);
        foreach (var attribute in selector)
        {
            if (attribute.Tag != Tag.RangeOfInteger || attribute.Value is not SharpIpp.Protocol.Models.Range range)
                throw new IppRequestException($"invalid overrides: '{memberName}' must be 1setOf rangeOfInteger", request, IppStatusCode.ClientErrorBadRequest);
            ranges.Add(range);
        }

        return ranges.ToArray();
    }

    private static void ValidateSelectorOrder(IReadOnlyCollection<IppAttribute> members, bool hasDocumentNumbers, bool hasDocumentCopies, IIppRequestMessage request)
    {
        var selectorOrder = members
            .Select(x => x.Name)
            .Where(IsSelectorMember)
            .Distinct()
            .ToArray();

        if (selectorOrder.Length == 0 || selectorOrder[0] != OverridePagesMemberName)
            throw new IppRequestException("invalid overrides: 'pages' must be the first member attribute", request, IppStatusCode.ClientErrorBadRequest);

        if (hasDocumentNumbers)
        {
            if (Array.IndexOf(selectorOrder, OverrideDocumentNumbersMemberName) != 1)
                throw new IppRequestException("invalid overrides: 'document-numbers' must be second when present", request, IppStatusCode.ClientErrorBadRequest);
        }

        if (hasDocumentCopies)
        {
            var expectedIndex = hasDocumentNumbers ? 2 : 1;
            if (Array.IndexOf(selectorOrder, OverrideDocumentCopiesMemberName) != expectedIndex)
                throw new IppRequestException("invalid overrides: 'document-copies' is in an invalid position", request, IppStatusCode.ClientErrorBadRequest);
        }

        var hasSeenOverrideAttribute = false;
        foreach (var member in members)
        {
            var isSelector = IsSelectorMember(member.Name);
            if (!isSelector)
            {
                hasSeenOverrideAttribute = true;
                continue;
            }

            if (hasSeenOverrideAttribute)
                throw new IppRequestException("invalid overrides: selector members must precede overriding Job Template attributes", request, IppStatusCode.ClientErrorBadRequest);
        }
    }

    private static void ValidateRangesAscendingNonOverlapping(IReadOnlyList<SharpIpp.Protocol.Models.Range> ranges, string memberName, IIppRequestMessage request)
    {
        for (var i = 0; i < ranges.Count; i++)
        {
            var current = ranges[i];
            if (current.Lower < 1)
                throw new IppRequestException($"invalid overrides: '{memberName}' ranges must be within 1:MAX", request, IppStatusCode.ClientErrorBadRequest);

            if (current.Lower > current.Upper)
                throw new IppRequestException($"invalid overrides: '{memberName}' range lower bound cannot exceed upper bound", request, IppStatusCode.ClientErrorBadRequest);

            if (i == 0)
                continue;

            var previous = ranges[i - 1];
            if (current.Lower <= previous.Upper)
                throw new IppRequestException($"invalid overrides: '{memberName}' ranges must be ascending and non-overlapping", request, IppStatusCode.ClientErrorBadRequest);
        }
    }

    private static void ValidateOverrideDocumentNumbersAcrossCollections(IReadOnlyList<SharpIpp.Protocol.Models.Range[]> effectiveDocumentRangesByCollection, IIppRequestMessage request)
    {
        if (effectiveDocumentRangesByCollection.Count <= 1)
            return;

        SharpIpp.Protocol.Models.Range? previousRange = null;
        foreach (var ranges in effectiveDocumentRangesByCollection)
        {
            foreach (var current in ranges)
            {
                if (previousRange.HasValue && current.Lower <= previousRange.Value.Upper)
                    throw new IppRequestException("invalid overrides: override collections must have ascending, non-overlapping document-numbers", request, IppStatusCode.ClientErrorBadRequest);
                previousRange = current;
            }
        }
    }
}

internal static class IppOperationExtensions
{
    internal static bool IsSystemServiceOperation(this IppOperation operation)
    {
        return operation switch
        {
            IppOperation.AllocatePrinterResources
                or IppOperation.CreatePrinter
                or IppOperation.DeallocatePrinterResources
                or IppOperation.DeletePrinter
                or IppOperation.GetPrinters
                or IppOperation.GetPrinterResources
                or IppOperation.ShutdownOnePrinter
                or IppOperation.StartupOnePrinter
                or IppOperation.RestartOnePrinter
                or IppOperation.CancelResource
                or IppOperation.CreateResource
                or IppOperation.InstallResource
                or IppOperation.SendResourceData
                or IppOperation.SetResourceAttributes
                or IppOperation.CreateResourceSubscriptions
                or IppOperation.CreateSystemSubscriptions
                or IppOperation.DisableAllPrinters
                or IppOperation.EnableAllPrinters
                or IppOperation.GetResources
                or IppOperation.GetSystemAttributes
                or IppOperation.GetSystemSupportedValues
                or IppOperation.PauseAllPrinters
                or IppOperation.PauseAllPrintersAfterCurrentJob
                or IppOperation.RegisterOutputDevice
                or IppOperation.RestartSystem
                or IppOperation.ResumeAllPrinters
                or IppOperation.SetSystemAttributes
                or IppOperation.ShutdownAllPrinters
                or IppOperation.StartupAllPrinters
                or IppOperation.CancelSubscription
                or IppOperation.GetNotifications
                or IppOperation.GetSubscriptionAttributes
                or IppOperation.GetSubscriptions
                or IppOperation.RenewSubscription
                => true,
            _ => false
        };
    }

    internal static bool IsPwg51005DocumentTargetOperation(this IppOperation operation)
    {
        return operation switch
        {
            IppOperation.CancelDocument
                or IppOperation.GetDocumentAttributes
                or IppOperation.GetDocuments
                or IppOperation.SetDocumentAttributes
                => true,
            _ => false
        };
    }
}
