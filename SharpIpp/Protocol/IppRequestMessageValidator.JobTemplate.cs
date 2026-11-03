using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Exceptions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol;

public partial class IppRequestMessageValidator
{
    private static readonly string[] Pwg51003MediaExclusiveCollections =
    [
        IppAttributeNames.CoverBack,
        IppAttributeNames.CoverFront,
        IppAttributeNames.InsertSheet,
        IppAttributeNames.JobAccountingSheets,
        IppAttributeNames.JobErrorSheet,
        IppAttributeNames.SeparatorSheets,
    ];

    private static readonly HashSet<string> Pwg51017ForbiddenDestinationAttributesMembers =
    [
        IppAttributeNames.DocumentPassword,
        IppAttributeNames.JobPassword,
        IppAttributeNames.JobPasswordEncryption,
    ];

    /// <summary>
    /// Validates job-level attributes (mutual exclusivity of finishings and media collection, valid copies/priority/number-up ranges).
    /// Spec: RFC 8011 Section 4.2 (Job Template Attributes), PWG 5100.3 (Production Printing).
    /// </summary>
    private void ValidateJobAttributes(IIppRequestMessage request, IppRequestValidationContext context)
    {
        ValidateFinishingsMutualExclusivity(request.JobAttributes, request);

        ValidateOutputBinAttributes(request.JobAttributes, request, context);
        ValidateCollectionMediaSelectionRules(request.JobAttributes, request);

        ValidateOverridesRules(request, context);

        if (request.IppOperation is IppOperation.PrintJob or IppOperation.CreateJob or IppOperation.ValidateJob)
        {
            ValidateCoDependent(
                request.JobAttributes,
                IppAttributeNames.JobPassword,
                IppAttributeNames.JobPasswordEncryption,
                request,
                "'job-password' and 'job-password-encryption' must be either both present or both absent");
        }

        if (EnforceMediaMutualExclusivity)
        {
            var depth = 0;
            var hasTopLevelMedia = false;
            var hasTopLevelMediaCol = false;
            foreach (var attr in request.JobAttributes)
            {
                if (attr.Tag == Tag.BegCollection)
                {
                    if (depth == 0 && attr.Name == IppAttributeNames.MediaCol)
                        hasTopLevelMediaCol = true;
                    depth++;
                }
                else if (attr.Tag == Tag.EndCollection)
                {
                    if (depth > 0) depth--;
                }
                else if (depth == 0 && attr.Name == IppAttributeNames.Media)
                {
                    hasTopLevelMedia = true;
                }
            }
            if (hasTopLevelMedia && hasTopLevelMediaCol)
                throw new IppRequestException("'media' and 'media-col' are mutually exclusive at the job level", request, IppStatusCode.ClientErrorConflictingAttributes);
        }

        var pageRangeAttributes = request.JobAttributes
            .Where(x => x.Name == IppAttributeNames.PageRanges && x.Tag == Tag.RangeOfInteger && x.Value is SharpIpp.Protocol.Models.Range)
            .Select(x => (SharpIpp.Protocol.Models.Range)x.Value)
            .ToArray();
        if (pageRangeAttributes.Length > 0)
            ValidateRangesAscendingNonOverlapping(pageRangeAttributes, IppAttributeNames.PageRanges, request);

        ValidateIntegerRange(request.JobAttributes, IppAttributeNames.Copies, 1, int.MaxValue, request);
        ValidateIntegerRange(request.JobAttributes, IppAttributeNames.JobPriority, 1, 100, request);
        ValidateIntegerRange(request.JobAttributes, IppAttributeNames.NumberUp, 1, int.MaxValue, request);

        ValidateFidelityBasedJobAttributes(request, context);
    }

    /// <summary>
    /// Enforces capability matching for job template attributes (media, finishings, sides, etc.) against printer capabilities when ipp-attribute-fidelity is enabled.
    /// Spec: RFC 8011 Section 3.2.1.1 (Fidelity validation).
    /// </summary>
    private void ValidateFidelityBasedJobAttributes(IIppRequestMessage request, IppRequestValidationContext context)
    {
        if (!UseIppAttributeFidelityForCapabilityValidation && !IsIppAttributeFidelityTrue(request))
            return;

        var mediaSupported = context.MediaSupported;
        if (mediaSupported is { Count: > 0 }
            && request.JobAttributes.TryGetIppValue<string>(IppAttributeNames.Media, out var media)
            && !mediaSupported.Contains((Media)media))
        {
            throw new IppRequestException($"'media' value '{media}' is not supported by target printer", request, IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
        }

        var finishingsSupported = context.FinishingsSupported;
        if (finishingsSupported is { Count: > 0 })
        {
            foreach (var finishingsInt in request.JobAttributes.Where(x => x.Name == IppAttributeNames.Finishings).Select(x => x.Value).OfType<int>())
            {
                if (!finishingsSupported.Contains((Finishings)finishingsInt))
                    throw new IppRequestException($"'finishings' value '{finishingsInt}' is not supported by target printer", request, IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
            }
        }

        var sidesSupported = context.SidesSupported;
        if (sidesSupported is { Count: > 0 } && request.JobAttributes.TryGetIppValue<string>(IppAttributeNames.Sides, out var sidesStr))
        {
            var sides = (Sides)sidesStr;
            if (!sidesSupported.Contains(sides))
                throw new IppRequestException($"'sides' value '{sides}' is not supported by target printer", request, IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
        }

        var printQualitySupported = context.PrintQualitySupported;
        if (printQualitySupported is { Count: > 0 })
        {
            if (request.JobAttributes.TryGetIppValue<int>(IppAttributeNames.PrintQuality, out var pqInt) && !printQualitySupported.Contains((PrintQuality)pqInt))
                throw new IppRequestException($"'print-quality' value '{pqInt}' is not supported by target printer", request, IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
        }

        var orientationSupported = context.OrientationRequestedSupported;
        if (orientationSupported is { Count: > 0 })
        {
            if (request.JobAttributes.TryGetIppValue<int>(IppAttributeNames.OrientationRequested, out var orientInt) && !orientationSupported.Contains((Orientation)orientInt))
                throw new IppRequestException($"'orientation-requested' value '{orientInt}' is not supported by target printer", request, IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
        }

        var printColorModeSupported = context.PrintColorModeSupported;
        if (printColorModeSupported is { Count: > 0 } && request.JobAttributes.TryGetIppValue<string>(IppAttributeNames.PrintColorMode, out var pcmStr))
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
    private void ValidateDocumentAttributes(IIppRequestMessage request, IppRequestValidationContext context)
    {
        ValidateFinishingsMutualExclusivity(request.DocumentAttributes, request);

        ValidateOutputBinAttributes(request.DocumentAttributes, request, context);
        ValidateCollectionMediaSelectionRules(request.DocumentAttributes, request);
    }

    /// <summary>
    /// Validates printer-level attributes, ensuring destination-uri-ready OAuth dependency rules and forbidden password properties.
    /// Spec: PWG 5101.7 / PWG 5100.17 (Scan Service).
    /// </summary>
    public virtual void ValidatePrinterAttributes(IIppRequestMessage request, IppRequestValidationContext? context = null)
    {
        ValidateDefaultAgainstSupportedAttributes(request.PrinterAttributes, request);

        ValidateTrayAndSupplyControlCharacters(request);

        var destinationUriReadyCollections = EnumerateNamedCollections(request.PrinterAttributes, IppAttributeNames.DestinationUriReady).ToArray();
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
        var hasFinishings = HasNamedAttribute(attributes, IppAttributeNames.Finishings);
        var hasFinishingsCol = HasNamedAttribute(attributes, IppAttributeNames.FinishingsCol);
        if (hasFinishings && hasFinishingsCol)
            throw new IppRequestException("'finishings' and 'finishings-col' are conflicting attributes and cannot be supplied together.", request, IppStatusCode.ClientErrorBadRequest);
    }

    /// <summary>
    /// Validates media collection attributes such as cover-back, insert-sheet, separator-sheets for mutual exclusivity rules.
    /// Spec: PWG 5100.3 (Production Printing).
    /// </summary>
    public virtual void ValidateCollectionMediaSelectionRules(IReadOnlyCollection<IppAttribute> attributes, IIppRequestMessage request)
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
        var hasMedia = members.Any(x => x.Name == IppAttributeNames.Media);
        var hasMediaCol = members.Any(x => x.Name == IppAttributeNames.MediaCol);

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
    private void ValidateOutputBinAttributes(IReadOnlyCollection<IppAttribute> attributes, IIppRequestMessage request, IppRequestValidationContext context)
    {
        var outputBins = attributes.Where(x => x.Name == IppAttributeNames.OutputBin).ToArray();

        if (outputBins.Length <= 0)
            return;

        if (outputBins.Length > 1)
            throw new IppRequestException("'output-bin' MUST be single-valued.", request, IppStatusCode.ClientErrorBadRequest);

        var outputBinTag = outputBins[0].Tag;
        if (outputBinTag != Tag.Keyword && outputBinTag != Tag.NameWithoutLanguage)
            throw new IppRequestException("'output-bin' MUST use 'keyword' or 'nameWithoutLanguage' syntax.", request, IppStatusCode.ClientErrorBadRequest);

        ValidateOutputBinAgainstSupportedValues(outputBins[0], request, context);
    }

    /// <summary>
    /// Validates that the value of the 'output-bin' attribute matches one of the printer-supported output bins.
    /// Spec: PWG 5100.2 Section 4.1 (output-bin-supported).
    /// </summary>
    public virtual void ValidateOutputBinAgainstSupportedValues(IppAttribute outputBin, IIppRequestMessage request, IppRequestValidationContext? context = null)
    {
        var ctx = context ?? Context;
        var outputBinSupported = ctx.OutputBinSupported;
        if (outputBinSupported is null || outputBinSupported.Count == 0)
            return;

        if (UseIppAttributeFidelityForCapabilityValidation && !IsIppAttributeFidelityTrue(request))
            return;

        var value = outputBin.Value.ToString();
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
}
