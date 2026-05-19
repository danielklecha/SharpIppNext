using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Exceptions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol;

/// <inheritdoc />
public class IppRequestMessageValidator : IIppRequestMessageValidator
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

    private static readonly HashSet<string> Pwg51017ForbiddenDestinationUriSchemes = new(StringComparer.OrdinalIgnoreCase)
    {
        "tel",
        "fax",
        "sip",
        "sips",
    };

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

    public static IppRequestMessageValidator Default => new()
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

    /// <summary>
    /// Validates the request message by executing enabled core, job, document, printer, and operation-specific rules.
    /// Spec: RFC 8011, PWG 5100.x.
    /// </summary>
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

    /// <summary>
    /// Validates that the request conforms to basic IPP constraints (valid version, request-id, unique attributes within groups, first/second operation attributes).
    /// Spec: RFC 8011 Section 4.1 (IPP Message Structure & Encoding), PWG 5100.22.
    /// </summary>
    private void ValidateCore(IIppRequestMessage request)
    {
        if (request.RequestId <= 0)
            throw new IppRequestException("Bad request-id value", request, IppStatusCode.ClientErrorBadRequest);

        if (request.Version < new IppVersion(1, 0))
            throw new IppRequestException("Unsupported IPP version", request, IppStatusCode.ServerErrorVersionNotSupported);

        ValidateUniqueAttributes(request.OperationAttributes, "operation-attributes", request);
        ValidateUniqueAttributes(request.JobAttributes, "job-attributes", request);
        ValidateUniqueAttributes(request.PrinterAttributes, "printer-attributes", request);
        ValidateUniqueAttributes(request.UnsupportedAttributes, "unsupported-attributes", request);
        ValidateUniqueAttributes(request.SubscriptionAttributes, "subscription-attributes", request);
        ValidateUniqueAttributes(request.EventNotificationAttributes, "event-notification-attributes", request);
        ValidateUniqueAttributes(request.ResourceAttributes, "resource-attributes", request);
        ValidateUniqueAttributes(request.DocumentAttributes, "document-attributes", request);
        ValidateUniqueAttributes(request.SystemAttributes, "system-attributes", request);

        ValidateDocumentMetadata(request);

        if (!ValidateOperationAttributesGroup)
            return;

        var operationAttributes = request.OperationAttributes;

        if (!operationAttributes.Any())
            throw new IppRequestException("No Operation Attributes", request, IppStatusCode.ClientErrorBadRequest);

        if (operationAttributes.First().Name != JobAttribute.AttributesCharset)
            throw new IppRequestException("attributes-charset MUST be the first attribute", request, IppStatusCode.ClientErrorBadRequest);

        var charsetValue = operationAttributes.First().Value.ToString();

        if (!string.Equals(charsetValue, "utf-8", StringComparison.OrdinalIgnoreCase))
            throw new IppRequestException("attributes-charset MUST be 'utf-8'", request, IppStatusCode.ClientErrorBadRequest);

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

    /// <summary>
    /// Validates job-level attributes (mutual exclusivity of finishings and media collection, valid copies/priority/number-up ranges).
    /// Spec: RFC 8011 Section 4.2 (Job Template Attributes), PWG 5100.3 (Production Printing).
    /// </summary>
    private void ValidateJobAttributes(IIppRequestMessage request)
    {
        ValidateFinishingsMutualExclusivity(request.JobAttributes, request);

        ValidateOutputBinAttributes(request.JobAttributes, request);
        ValidateCollectionMediaSelectionRules(request.JobAttributes, request);

        ValidateOverridesRules(request);

        if (request.IppOperation is IppOperation.PrintJob or IppOperation.CreateJob or IppOperation.ValidateJob)
        {
            var hasJobPassword = HasNamedAttribute(request.JobAttributes, JobAttribute.JobPassword);
            var hasJobPasswordEncryption = HasNamedAttribute(request.JobAttributes, JobAttribute.JobPasswordEncryption);
            if (hasJobPassword != hasJobPasswordEncryption)
                throw new IppRequestException("'job-password' and 'job-password-encryption' must be either both present or both absent", request, IppStatusCode.ClientErrorBadRequest);
        }

        {
            var depth = 0;
            var hasTopLevelMedia = false;
            var hasTopLevelMediaCol = false;
            foreach (var attr in request.JobAttributes)
            {
                if (attr.Tag == Tag.BegCollection)
                {
                    if (depth == 0 && attr.Name == JobAttribute.MediaCol)
                        hasTopLevelMediaCol = true;
                    depth++;
                }
                else if (attr.Tag == Tag.EndCollection)
                {
                    if (depth > 0) depth--;
                }
                else if (depth == 0 && attr.Name == JobAttribute.Media)
                {
                    hasTopLevelMedia = true;
                }
            }
            if (hasTopLevelMedia && hasTopLevelMediaCol)
                throw new IppRequestException("'media' and 'media-col' are mutually exclusive at the job level", request, IppStatusCode.ClientErrorBadRequest);
        }

        var pageRangeAttributes = request.JobAttributes
            .Where(x => x.Name == JobAttribute.PageRanges && x.Tag == Tag.RangeOfInteger && x.Value is SharpIpp.Protocol.Models.Range)
            .Select(x => (SharpIpp.Protocol.Models.Range)x.Value)
            .ToArray();
        if (pageRangeAttributes.Length > 0)
            ValidateRangesAscendingNonOverlapping(pageRangeAttributes, JobAttribute.PageRanges, request);

        if (request.JobAttributes.TryGetIppValue<int>(JobAttribute.Copies, out var copiesValue) && copiesValue <= 0)
            throw new IppRequestException("'copies' must be >= 1", request, IppStatusCode.ClientErrorBadRequest);

        if (request.JobAttributes.TryGetIppValue<int>(JobAttribute.JobPriority, out var jobPriorityValue) && (jobPriorityValue < 1 || jobPriorityValue > 100))
            throw new IppRequestException("'job-priority' must be in the range 1-100", request, IppStatusCode.ClientErrorBadRequest);

        if (request.JobAttributes.TryGetIppValue<int>(JobAttribute.NumberUp, out var numberUpValue) && numberUpValue <= 0)
            throw new IppRequestException("'number-up' must be >= 1", request, IppStatusCode.ClientErrorBadRequest);

        ValidateFidelityBasedJobAttributes(request);
    }

    /// <summary>
    /// Enforces capability matching for job template attributes (media, finishings, sides, etc.) against printer capabilities when ipp-attribute-fidelity is enabled.
    /// Spec: RFC 8011 Section 3.2.1.1 (Fidelity validation).
    /// </summary>
    private void ValidateFidelityBasedJobAttributes(IIppRequestMessage request)
    {
        if (!UseIppAttributeFidelityForCapabilityValidation && !IsIppAttributeFidelityTrue(request))
            return;

        var mediaSupported = Context.MediaSupported;
        if (mediaSupported is { Count: > 0 }
            && request.JobAttributes.TryGetIppValue<string>(JobAttribute.Media, out var media)
            && !mediaSupported.Contains((Media)media))
        {
            throw new IppRequestException($"'media' value '{media}' is not supported by target printer", request, IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
        }

        var finishingsSupported = Context.FinishingsSupported;
        if (finishingsSupported is { Count: > 0 })
        {
            foreach (var finishingsInt in request.JobAttributes.Where(x => x.Name == JobAttribute.Finishings).Select(x => x.Value).OfType<int>())
            {
                if (!finishingsSupported.Contains((Finishings)finishingsInt))
                    throw new IppRequestException($"'finishings' value '{finishingsInt}' is not supported by target printer", request, IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
            }
        }

        var sidesSupported = Context.SidesSupported;
        if (sidesSupported is { Count: > 0 } && request.JobAttributes.TryGetIppValue<string>(JobAttribute.Sides, out var sidesStr))
        {
            var sides = (Sides)sidesStr;
            if (!sidesSupported.Contains(sides))
                throw new IppRequestException($"'sides' value '{sides}' is not supported by target printer", request, IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
        }

        var printQualitySupported = Context.PrintQualitySupported;
        if (printQualitySupported is { Count: > 0 })
        {
            if (request.JobAttributes.TryGetIppValue<int>(JobAttribute.PrintQuality, out var pqInt) && !printQualitySupported.Contains((PrintQuality)pqInt))
                throw new IppRequestException($"'print-quality' value '{pqInt}' is not supported by target printer", request, IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
        }

        var orientationSupported = Context.OrientationRequestedSupported;
        if (orientationSupported is { Count: > 0 })
        {
            if (request.JobAttributes.TryGetIppValue<int>(JobAttribute.OrientationRequested, out var orientInt) && !orientationSupported.Contains((Orientation)orientInt))
                throw new IppRequestException($"'orientation-requested' value '{orientInt}' is not supported by target printer", request, IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
        }

        var printColorModeSupported = Context.PrintColorModeSupported;
        if (printColorModeSupported is { Count: > 0 } && request.JobAttributes.TryGetIppValue<string>(JobAttribute.PrintColorMode, out var pcmStr))
        {
            var pcm = (PrintColorMode)pcmStr;
            if (!printColorModeSupported.Contains(pcm))
                throw new IppRequestException($"'print-color-mode' value '{pcm}' is not supported by target printer", request, IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
        }
    }

    /// <summary>
    /// Validates document-level attributes (mutual exclusivity of finishings, output-bin, media collection).
    /// Spec: PWG 5100.5 (Document Object).
    /// </summary>
    private void ValidateDocumentAttributes(IIppRequestMessage request)
    {
        ValidateFinishingsMutualExclusivity(request.DocumentAttributes, request);

        ValidateOutputBinAttributes(request.DocumentAttributes, request);
        ValidateCollectionMediaSelectionRules(request.DocumentAttributes, request);
    }

    /// <summary>
    /// Validates printer-level attributes, ensuring destination-uri-ready OAuth dependency rules and forbidden password properties.
    /// Spec: PWG 5101.7 / PWG 5100.17 (Scan Service).
    /// </summary>
    public void ValidatePrinterAttributes(IIppRequestMessage request)
    {
        ValidateDefaultAgainstSupportedAttributes(request.PrinterAttributes, request);

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
                .Select(x => x.Value.ToString())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Where(x => Pwg51017ForbiddenDestinationAttributesMembers.Contains(x!))
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

    /// <summary>
    /// Validates that 'finishings' and 'finishings-col' are not both specified in the same group.
    /// Spec: PWG 5100.3 Section 6.2 (Finishings and Finishings-Col Mutual Exclusivity).
    /// </summary>
    private static void ValidateFinishingsMutualExclusivity(IReadOnlyCollection<IppAttribute> attributes, IIppRequestMessage request)
    {
        var hasFinishings = HasNamedAttribute(attributes, JobAttribute.Finishings);
        var hasFinishingsCol = HasNamedAttribute(attributes, JobAttribute.FinishingsCol);
        if (hasFinishings && hasFinishingsCol)
            throw new IppRequestException("'finishings' and 'finishings-col' are conflicting attributes and cannot be supplied together.", request, IppStatusCode.ClientErrorBadRequest);
    }

    /// <summary>
    /// Validates media collection attributes such as cover-back, insert-sheet, separator-sheets for mutual exclusivity rules.
    /// Spec: PWG 5100.3 (Production Printing).
    /// </summary>
    public void ValidateCollectionMediaSelectionRules(IReadOnlyCollection<IppAttribute> attributes, IIppRequestMessage request)
    {
        foreach (var collectionName in Pwg51003MediaExclusiveCollections)
        {
            var collections = EnumerateNamedCollections(attributes, collectionName).ToArray();
            foreach (var collection in collections)
            {
                if (collection.Count == 1 && collection[0].Tag.IsOutOfBand())
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

    /// <summary>
    /// Ensures that nested media collection attributes do not specify both 'media' and 'media-col'.
    /// Spec: PWG 5100.3 Section 3.1 (Media Selection).
    /// </summary>
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

    /// <summary>
    /// Validates that the 'output-bin' attribute is single-valued and is represented using keyword or name syntax.
    /// Spec: PWG 5100.2 Section 4.1 (output-bin).
    /// </summary>
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

    /// <summary>
    /// Validates that the value of the 'output-bin' attribute matches one of the printer-supported output bins.
    /// Spec: PWG 5100.2 Section 4.1 (output-bin-supported).
    /// </summary>
    public void ValidateOutputBinAgainstSupportedValues(IppAttribute outputBin, IIppRequestMessage request)
    {
        var outputBinSupported = Context.OutputBinSupported;
        if (outputBinSupported is null || outputBinSupported.Count == 0)
            return;

        if (UseIppAttributeFidelityForCapabilityValidation && !IsIppAttributeFidelityTrue(request))
            return;

        var value = outputBin.Value.ToString();
        if (string.IsNullOrWhiteSpace(value))
            return;

        var isKeyword = outputBin.Tag == Tag.Keyword;
        var isSupported = outputBinSupported.Any(x =>
            x.IsMarked == isKeyword &&
            string.Equals(x.Value, value, StringComparison.Ordinal));

        if (isSupported)
            return;

        throw new IppRequestException(
            $"invalid output-bin: value is not supported by target printer: {value}",
            request,
            IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    /// <summary>
    /// Executes operation-specific validation rules for all supported IPP/PWG operations.
    /// Spec: RFC 8011, PWG 5100.5, PWG 5100.22.
    /// </summary>
    public void ValidateOperationRules(IIppRequestMessage request)
    {
        var operationAttributes = request.OperationAttributes;
        var hasDocumentNumber = ValidateOperationAttributesGroup && HasNamedAttribute(operationAttributes, DocumentAttribute.DocumentNumber);
        var hasLastDocument = ValidateOperationAttributesGroup && HasNamedAttribute(operationAttributes, JobAttribute.LastDocument);
        var hasDocumentUri = ValidateOperationAttributesGroup && HasNamedAttribute(operationAttributes, JobAttribute.DocumentUri);
        var hasOutputDeviceUuid = ValidateOperationAttributesGroup && HasNamedAttribute(operationAttributes, JobAttribute.OutputDeviceUuid);

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

            case IppOperation.AcknowledgeDocument:
                ValidateRequiredDocumentNumber(hasDocumentNumber, documentNumber, request);
                ValidateRequiredOutputDeviceUuid(hasOutputDeviceUuid, request);
                ValidateFetchStatusCodeNotSuccessful(operationAttributes, request);
                break;

            case IppOperation.AcknowledgeIdentifyPrinter:
            case IppOperation.DeregisterOutputDevice:
            case IppOperation.FetchJob:
            case IppOperation.GetOutputDeviceAttributes:
            case IppOperation.UpdateJobStatus:
            case IppOperation.UpdateOutputDeviceAttributes:
            case IppOperation.RegisterOutputDevice:
                ValidateRequiredOutputDeviceUuid(hasOutputDeviceUuid, request);
                if (request.IppOperation == IppOperation.RegisterOutputDevice && ValidateOperationAttributesGroup)
                {
                    var hasCertificate = HasNamedAttribute(operationAttributes, JobAttribute.OutputDeviceX509Certificate);
                    var hasRequest = HasNamedAttribute(operationAttributes, JobAttribute.OutputDeviceX509Request);
                    if (hasCertificate && hasRequest)
                        throw new IppRequestException("'output-device-x509-certificate' and 'output-device-x509-request' are mutually exclusive", request, IppStatusCode.ClientErrorConflictingAttributes);
                }
                break;
            case IppOperation.UpdateActiveJobs:
                ValidateRequiredOutputDeviceUuid(hasOutputDeviceUuid, request);
                if (ValidateOperationAttributesGroup)
                {
                    var hasJobIds = HasNamedAttribute(operationAttributes, JobAttribute.JobIds);
                    var hasJobStates = HasNamedAttribute(operationAttributes, JobAttribute.OutputDeviceJobStates);
                    if (hasJobIds != hasJobStates)
                        throw new IppRequestException("'job-ids' and 'output-device-job-states' must be either both present or both absent", request, IppStatusCode.ClientErrorBadRequest);

                    if (hasJobIds)
                    {
                        var jobIdsCount = operationAttributes.Count(x => x.Name == JobAttribute.JobIds);
                        var jobStatesCount = operationAttributes.Count(x => x.Name == JobAttribute.OutputDeviceJobStates);
                        if (jobIdsCount != jobStatesCount)
                            throw new IppRequestException("'job-ids' and 'output-device-job-states' must have the same number of values", request, IppStatusCode.ClientErrorBadRequest);
                    }
                }
                break;

            case IppOperation.AcknowledgeJob:
                ValidateRequiredOutputDeviceUuid(hasOutputDeviceUuid, request);
                ValidateFetchStatusCodeNotSuccessful(operationAttributes, request);
                break;

            case IppOperation.FetchDocument:
            case IppOperation.UpdateDocumentStatus:
                ValidateRequiredDocumentNumber(hasDocumentNumber, documentNumber, request);
                ValidateRequiredOutputDeviceUuid(hasOutputDeviceUuid, request);
                break;

            case IppOperation.ValidateJob:
            case IppOperation.ValidateDocument:
                if (ValidateOperationAttributesGroup && HasNamedAttribute(operationAttributes, JobAttribute.DocumentPassword))
                    throw new IppRequestException("document-password is not allowed for validate operations", request, IppStatusCode.ClientErrorBadRequest);
                break;

            case IppOperation.CreateJob:
                ValidateCreateJobDestinationRules(operationAttributes, request);
                break;

            case IppOperation.GetNotifications:
                if (ValidateOperationAttributesGroup)
                {
                    var notifyPullMethodAttr = operationAttributes.FirstOrDefault(x => x.Name == SystemAttribute.NotifyPullMethod);
                    var notifyPullMethodValue = notifyPullMethodAttr.Value?.ToString();
                    if (!string.Equals(notifyPullMethodValue, "ippget", StringComparison.OrdinalIgnoreCase))
                        throw new IppRequestException("'notify-pull-method' must be 'ippget'", request, IppStatusCode.ClientErrorBadRequest);
                }
                break;

            case IppOperation.CancelSubscription:
            case IppOperation.GetSubscriptionAttributes:
            case IppOperation.RenewSubscription:
                if (ValidateOperationAttributesGroup && !HasNamedAttribute(operationAttributes, SystemAttribute.NotifySubscriptionId))
                    throw new IppRequestException("missing notify-subscription-id", request, IppStatusCode.ClientErrorBadRequest);
                break;

            case IppOperation.CancelResource:
            case IppOperation.GetResourceAttributes:
            case IppOperation.InstallResource:
            case IppOperation.SendResourceData:
            case IppOperation.SetResourceAttributes:
                if (ValidateOperationAttributesGroup && !HasNamedAttribute(operationAttributes, SystemAttribute.ResourceId))
                    throw new IppRequestException("missing resource-id", request, IppStatusCode.ClientErrorBadRequest);
                break;

            case IppOperation.AllocatePrinterResources:
            case IppOperation.DeallocatePrinterResources:
            case IppOperation.DeletePrinter:
            case IppOperation.GetPrinterResources:
            case IppOperation.ShutdownOnePrinter:
            case IppOperation.StartupOnePrinter:
            case IppOperation.RestartOnePrinter:
                if (ValidateOperationAttributesGroup && !HasNamedAttribute(operationAttributes, JobAttribute.PrinterId))
                    throw new IppRequestException("missing printer-id", request, IppStatusCode.ClientErrorBadRequest);
                break;

            case IppOperation.CreatePrinterSubscriptions:
            case IppOperation.CreateSystemSubscriptions:
                if (!request.SubscriptionAttributes.Any())
                    throw new IppRequestException("subscription-attributes-group is required and must be non-empty", request, IppStatusCode.ClientErrorBadRequest);
                break;
        }

        ValidateJobRequestedAttributesGroupKeywords(request);
        ValidateDocumentRequestedAttributesGroupKeywords(request);
        ValidateNotifyEventsValues(request);
    }

    /// <summary>
    /// Validates destination-uri rules for create-job operations, rejecting forbidden fax attributes/schemes for Scan.
    /// Spec: PWG 5100.17 / PWG 5100.22.
    /// </summary>
    public void ValidateCreateJobDestinationRules(IReadOnlyCollection<IppAttribute> operationAttributes, IIppRequestMessage request)
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
                var destinationUri = destinationUriMember.Value.ToString();
                if (string.IsNullOrEmpty(destinationUri))
                    continue;

                string? scheme = null;
                if (Uri.TryCreate(destinationUri, UriKind.Absolute, out var parsedUri))
                    scheme = parsedUri.Scheme;
                else if (destinationUri.IndexOf(':') is int i and > 0)
                    scheme = destinationUri.Substring(0, i);

                if (scheme != null && Pwg51017ForbiddenDestinationUriSchemes.Contains(scheme))
                    throw new IppRequestException("invalid destination-uri scheme for Scan", request, IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
            }
        }

        var destinationAccessesCollections = EnumerateNamedCollections(operationAttributes, JobAttribute.DestinationAccesses).ToArray();
        if (destinationAccessesCollections.Length > 0 && destinationAccessesCollections.Length != destinationUriCollections.Length)
            throw new IppRequestException("destination-accesses cardinality MUST match destination-uris", request, IppStatusCode.ClientErrorBadRequest);
    }

    /// <summary>
    /// Validates that requested attribute group keywords for Job query operations are supported by the target.
    /// Spec: RFC 8011 Section 3.2.5.1 / PWG 5100.8.
    /// </summary>
    public void ValidateJobRequestedAttributesGroupKeywords(IIppRequestMessage request)
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
            .Select(x => x.Value.ToString())
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

    /// <summary>
    /// Ensures that a required document-number is present and greater than zero.
    /// Spec: PWG 5100.5 Section 5 (Document Object).
    /// </summary>
    private void ValidateRequiredDocumentNumber(bool hasDocumentNumber, int? documentNumber, IIppRequestMessage request)
    {
        if (!ValidateOperationAttributesGroup)
            return;

        if (!hasDocumentNumber)
            throw new IppRequestException("missing document-number", request, IppStatusCode.ClientErrorBadRequest);
        if (documentNumber is null || documentNumber <= 0)
            throw new IppRequestException("invalid document-number", request, IppStatusCode.ClientErrorBadRequest);
    }

    /// <summary>
    /// Ensures that a last-document parameter is present for multi-document operations.
    /// Spec: RFC 8011 Section 3.3.1 (Send-Document).
    /// </summary>
    public void ValidateRequiredLastDocument(bool hasLastDocument, bool? lastDocument, IIppRequestMessage request)
    {
        if (!ValidateOperationAttributesGroup)
            return;

        if (!hasLastDocument)
            throw new IppRequestException("missing last-document", request, IppStatusCode.ClientErrorBadRequest);
        if (lastDocument is null)
            throw new IppRequestException("invalid last-document", request, IppStatusCode.ClientErrorBadRequest);
    }

    /// <summary>
    /// Ensures that a target output-device-uuid is present in coordination operations.
    /// Spec: PWG 5100.22 (IPP System Service).
    /// </summary>
    public void ValidateRequiredOutputDeviceUuid(bool hasOutputDeviceUuid, IIppRequestMessage request)
    {
        if (!ValidateOperationAttributesGroup)
            return;

        if (!hasOutputDeviceUuid)
            throw new IppRequestException("missing output-device-uuid", request, IppStatusCode.ClientErrorBadRequest);
    }

    /// <summary>
    /// Ensures that a proxy fetch-status-code parameter is not successful-ok (must be a warning or error code).
    /// Spec: PWG 5100.22 (IPP System Service).
    /// </summary>
    public static void ValidateFetchStatusCodeNotSuccessful(IEnumerable<IppAttribute> operationAttributes, IIppRequestMessage request)
    {
        var fetchStatusAttributes = operationAttributes
            .Where(x => x.Name == JobAttribute.FetchStatusCode)
            .ToArray();
        if (fetchStatusAttributes.Length == 0)
            return;

        var fetchStatusAttribute = fetchStatusAttributes[0];

        if (fetchStatusAttribute.Value is IppStatusCode fetchStatusCode && fetchStatusCode == IppStatusCode.SuccessfulOk)
            throw new IppRequestException("fetch-status-code MUST NOT be successful-ok", request, IppStatusCode.ClientErrorBadRequest);

        if (fetchStatusAttribute.Value is int fetchStatusCodeInt && fetchStatusCodeInt == (int)IppStatusCode.SuccessfulOk)
            throw new IppRequestException("fetch-status-code MUST NOT be successful-ok", request, IppStatusCode.ClientErrorBadRequest);

        if (fetchStatusAttribute.Value is short fetchStatusCodeShort && fetchStatusCodeShort == (short)IppStatusCode.SuccessfulOk)
            throw new IppRequestException("fetch-status-code MUST NOT be successful-ok", request, IppStatusCode.ClientErrorBadRequest);
    }

    /// <summary>
    /// Checks whether an attribute with the specified name exists in the collection.
    /// </summary>
    private static bool HasNamedAttribute(IEnumerable<IppAttribute> attributes, string name)
    {
        return attributes.Any(x => x.Name == name);
    }

    /// <summary>
    /// Reads and casts an operation attribute's value to the specified type if present.
    /// </summary>
    private static T? ReadOperationAttributeAs<T>(IEnumerable<IppAttribute> operationAttributes, string name) where T : struct
    {
        var attribute = operationAttributes.FirstOrDefault(x => x.Name == name);
        if (attribute.Value is T typed)
            return typed;

        return null;
    }

    /// <summary>
    /// Validates that requested attribute group keywords for Document query operations are supported by the target.
    /// Spec: PWG 5100.5 (Document Object).
    /// </summary>
    public void ValidateDocumentRequestedAttributesGroupKeywords(IIppRequestMessage request)
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
            .Select(x => x.Value.ToString())
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

    /// <summary>
    /// Validates that notify-events keywords are supported by the target printer/system.
    /// Spec: RFC 3995 Section 5.3.3 (notify-events).
    /// </summary>
    public void ValidateNotifyEventsValues(IIppRequestMessage request)
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
            .Select(x => x.Value.ToString())
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

    /// <summary>
    /// Validates that a Document is in a state that permits cancellation.
    /// Spec: PWG 5100.5 Section 5 (Cancel-Document state rules).
    /// </summary>
    public void ValidateCancelDocumentStateRules(int documentNumber, IIppRequestMessage request)
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

    /// <summary>
    /// Validates that a Document is in a state that permits setting of attributes.
    /// Spec: PWG 5100.5 Section 5 (Set-Document-Attributes state rules).
    /// </summary>
    public void ValidateSetDocumentAttributesStateRules(int documentNumber, IIppRequestMessage request)
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

    /// <summary>
    /// Validates job overrides collections including formatting, page/document selectors, and membership rules.
    /// Spec: PWG 5100.6 Section 3 (Overrides).
    /// </summary>
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

    /// <summary>
    /// Validates that the member attributes inside an overrides collection are supported by the target printer/system.
    /// Spec: PWG 5100.6 Section 3 (Overrides).
    /// </summary>
    public void ValidateOverrideMembersAgainstSupportedValues(IReadOnlyCollection<IppAttribute> members, IIppRequestMessage request)
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

    /// <summary>
    /// Checks whether the ipp-attribute-fidelity attribute is set to true in the request.
    /// </summary>
    private static bool IsIppAttributeFidelityTrue(IIppRequestMessage request)
    {
        var fidelityAttribute = request.OperationAttributes.FirstOrDefault(x => x.Name == JobAttribute.IppAttributeFidelity);
        return fidelityAttribute.Value is bool value && value;
    }

    /// <summary>
    /// Helper method to enumerate only the top-level member names of an overrides collection.
    /// </summary>
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

    /// <summary>
    /// Helper method to enumerate collections of attributes matching a specific attribute name.
    /// </summary>
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

    /// <summary>
    /// Helper method to check if a member name is one of the overrides selector attributes.
    /// </summary>
    private static bool IsSelectorMember(string memberName)
    {
        return OverrideSelectorMemberNames.Contains(memberName);
    }

    /// <summary>
    /// Reads and parses overrides range selector attributes (e.g., pages, document-numbers).
    /// </summary>
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

    /// <summary>
    /// Validates that the range selectors in an overrides collection are specified in the correct sequence.
    /// Spec: PWG 5100.6 Section 3 (Overrides).
    /// </summary>
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

    /// <summary>
    /// Validates that a list of range attributes is sorted in ascending order and does not contain overlapping ranges.
    /// Spec: PWG 5100.6 Section 3 (Overrides), RFC 8011.
    /// </summary>
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

    /// <summary>
    /// Validates that document number ranges across multiple override collections are ascending and non-overlapping.
    /// Spec: PWG 5100.6 Section 3 (Overrides).
    /// </summary>
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

    private static readonly HashSet<string> DocumentMetadataKeywords = new()
    {
        // dc-elements
        "contributor", "coverage", "creator", "date", "description", "format", "identifier", "language", "publisher", "relation", "rights", "source", "subject", "title", "type",
        // dc-terms
        "abstract", "accessRights", "accrualMethod", "accrualPeriodicity", "accrualPolicy", "alternative", "audience", "available", "bibliographicCitation", "conformsTo", "created", "dateAccepted", "dateCopyrighted", "dateSubmitted", "educationLevel", "extent", "hasFormat", "hasPart", "hasVersion", "instructionalMethod", "isFormatOf", "isPartOf", "isReferencedBy", "isReplacedBy", "isRequiredBy", "issued", "isVersionOf", "license", "mediator", "medium", "modified", "provenance", "references", "replaces", "requires", "rightsHolder", "spatial", "tableOfContents", "temporal", "valid"
    };

    private static readonly System.Text.UTF8Encoding StrictUtf8 = new(false, true);

    /// <summary>
    /// Verifies that the bytes represent a valid UTF-8 string containing no ASCII control characters.
    /// Spec: PWG 5100.13 Section 6.1.1 (document-metadata).
    /// </summary>
    private static bool IsValidUtf8String(byte[] bytes)
    {
        try
        {
            var str = StrictUtf8.GetString(bytes);
            foreach (var c in str)
            {
                if (c < 0x20 || c == 0x7F)
                    return false;
            }
            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    /// <summary>
    /// Checks whether a metadata keyword conforms to Dublin Core or vendor-specific keyword patterns.
    /// Spec: PWG 5100.13 Section 6.1.1 (document-metadata).
    /// </summary>
    private static bool IsValidDocumentMetadataKeyword(string keyword)
    {
        if (DocumentMetadataKeywords.Contains(keyword))
            return true;

        if (keyword.StartsWith("x-", StringComparison.Ordinal))
        {
            var suffix = keyword.Substring(2);
            if (suffix.Length == 0)
                return false;
            foreach (var c in suffix)
            {
                if (!((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || c == '.' || c == '-' || c == '_'))
                    return false;
            }
            return true;
        }

        return false;
    }

    /// <summary>
    /// Extracts the raw byte array from an octet string or string representation.
    /// </summary>
    private static byte[]? GetOctetStringBytes(object? value)
    {
        if (value is OctetString octetString)
            return octetString.Value;
        if (value is byte[] bytes)
            return bytes;
        if (value is string s)
            return System.Text.Encoding.UTF8.GetBytes(s);
        return null;
    }

    /// <summary>
    /// Validates the structure and encoding of the document-metadata attribute values.
    /// Spec: PWG 5100.13 Section 6.1.1 (document-metadata).
    /// </summary>
    private void ValidateDocumentMetadata(IIppRequestMessage request)
    {
        var metadataAttributes = request.OperationAttributes
            .Concat(request.JobAttributes)
            .Concat(request.DocumentAttributes)
            .Where(x => x.Name == DocumentAttribute.DocumentMetadata);

        foreach (var attr in metadataAttributes)
        {
            if (attr.Tag.IsOutOfBand())
                continue;

            var bytes = GetOctetStringBytes(attr.Value);
            if (bytes == null)
                continue;

            if (!IsValidUtf8String(bytes))
                throw new IppRequestException("invalid document-metadata value encoding: must be valid UTF-8 and contain no control characters", request, IppStatusCode.ClientErrorBadRequest);

            var decoded = System.Text.Encoding.UTF8.GetString(bytes);
            var eqIndex = decoded.IndexOf('=');
            if (eqIndex <= 0)
                throw new IppRequestException("invalid document-metadata value: must be in keyword=value format", request, IppStatusCode.ClientErrorBadRequest);

            var keyword = decoded.Substring(0, eqIndex);
            if (!IsValidDocumentMetadataKeyword(keyword))
                throw new IppRequestException($"invalid document-metadata keyword '{keyword}'", request, IppStatusCode.ClientErrorBadRequest);
        }
    }

    /// <summary>
    /// Validates that attributes are not duplicated (non-consecutive instances of the same name) within an attribute group.
    /// Spec: RFC 8011 Section 4.1.3 (attribute group uniqueness).
    /// </summary>
    private void ValidateUniqueAttributes(List<IppAttribute> attributes, string groupName, IIppRequestMessage request)
    {
        var seenNames = new HashSet<string>(StringComparer.Ordinal);
        string? currentName = null;
        var level = 0;
        foreach (var attr in attributes)
        {
            if (attr.Tag == Tag.BegCollection)
            {
                level++;
                continue;
            }
            if (attr.Tag == Tag.EndCollection)
            {
                level--;
                continue;
            }
            if (level > 0)
                continue;

            if (string.IsNullOrEmpty(attr.Name))
                continue;

            if (attr.Name != currentName)
            {
                if (seenNames.Contains(attr.Name))
                {
                    throw new IppRequestException($"Duplicate attribute '{attr.Name}' in group '{groupName}'", request, IppStatusCode.ClientErrorBadRequest);
                }
                seenNames.Add(attr.Name);
                currentName = attr.Name;
            }
        }
    }

    /// <summary>
    /// Validates that any 'xxx-default' attribute values are within the corresponding 'xxx-supported' values.
    /// Spec: RFC 3380 Section 4.1.2.
    /// </summary>
    private void ValidateDefaultAgainstSupportedAttributes(IReadOnlyCollection<IppAttribute> attributes, IIppRequestMessage request)
    {
        var attributesByName = attributes.GroupBy(x => x.Name).ToDictionary(g => g.Key, g => g.ToArray());
        foreach (var kvp in attributesByName)
        {
            var name = kvp.Key;
            if (!name.EndsWith("-default") || name.Length <= "-default".Length)
                continue;

            var baseName = name.Substring(0, name.Length - "-default".Length);
            var supportedName = baseName + "-supported";

            if (!attributesByName.TryGetValue(supportedName, out var supportedAttrs))
                continue;

            foreach (var defaultAttr in kvp.Value)
            {
                var defaultValues = GetAttributeValues(defaultAttr).ToArray();
                foreach (var defaultValue in defaultValues)
                {
                    var isSupported = false;
                    foreach (var supportedAttr in supportedAttrs)
                    {
                        var supportedValues = GetAttributeValues(supportedAttr);
                        foreach (var supportedValue in supportedValues)
                        {
                            if (IsValueSupported(defaultValue, supportedValue))
                            {
                                isSupported = true;
                                break;
                            }
                        }
                        if (isSupported)
                            break;
                    }

                    if (!isSupported)
                    {
                        throw new IppRequestException(
                            $"invalid '{name}' value '{defaultValue}': not in '{supportedName}'",
                            request,
                            IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
                    }
                }
            }
        }
    }

    private static IEnumerable<object> GetAttributeValues(IppAttribute attribute)
    {
        if (attribute.Value == null)
            yield break;

        if (attribute.Value is System.Collections.IEnumerable enumerable && !(attribute.Value is string))
        {
            foreach (var item in enumerable)
            {
                if (item != null)
                    yield return item;
            }
        }
        else
        {
            yield return attribute.Value;
        }
    }

    private static bool IsValueSupported(object defaultValue, object supportedValue)
    {
        if (supportedValue is SharpIpp.Protocol.Models.Range range)
        {
            if (defaultValue is int intVal)
            {
                return intVal >= range.Lower && intVal <= range.Upper;
            }
            if (defaultValue is long longVal)
            {
                return longVal >= range.Lower && longVal <= range.Upper;
            }
            var defStr = defaultValue.ToString();
            if (defStr != null && int.TryParse(defStr, out var parsedInt))
            {
                return parsedInt >= range.Lower && parsedInt <= range.Upper;
            }
        }

        return string.Equals(defaultValue.ToString(), supportedValue.ToString(), StringComparison.Ordinal);
    }
}
