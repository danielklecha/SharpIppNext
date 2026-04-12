using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Exceptions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol;

public class IppRequestValidator : IIppRequestValidator
{
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

        if (!request.OperationAttributes.Any())
            throw new IppRequestException("No Operation Attributes", request, IppStatusCode.ClientErrorBadRequest);
        if (request.OperationAttributes.First().Name != JobAttribute.AttributesCharset)
            throw new IppRequestException("attributes-charset MUST be the first attribute", request, IppStatusCode.ClientErrorBadRequest);
        if (request.OperationAttributes.Skip(1).FirstOrDefault().Name != JobAttribute.AttributesNaturalLanguage)
            throw new IppRequestException("attributes-natural-language MUST be the second attribute", request, IppStatusCode.ClientErrorBadRequest);

        var hasPrinterUri = request.OperationAttributes.Any(x => x.Name == JobAttribute.PrinterUri);
        var hasSystemUri = request.OperationAttributes.Any(x => x.Name == SystemAttribute.SystemUri);
        var hasJobUri = request.OperationAttributes.Any(x => x.Name == JobAttribute.JobUri);
        if (!hasPrinterUri && !hasSystemUri && !(hasJobUri && request.IppOperation.IsPwg51005DocumentTargetOperation()))
            throw new IppRequestException("No printer-uri or system-uri operation attribute", request, IppStatusCode.ClientErrorBadRequest);

        if (request.IppOperation.IsSystemServiceOperation() && !hasSystemUri)
            throw new IppRequestException("No system-uri operation attribute", request, IppStatusCode.ClientErrorBadRequest);
    }

    private void ValidateJobAttributes(IIppRequestMessage request)
    {
        var hasFinishings = request.JobAttributes.Any(x => x.Name == JobAttribute.Finishings);
        var hasFinishingsCol = request.JobAttributes.Any(x => x.Name == JobAttribute.FinishingsCol);
        if (hasFinishings && hasFinishingsCol)
            throw new IppRequestException("'finishings' and 'finishings-col' are conflicting attributes and cannot be supplied together.", request, IppStatusCode.ClientErrorBadRequest);

        ValidateOverridesRules(request);
    }

    private static void ValidateDocumentAttributes(IIppRequestMessage request)
    {
        var hasFinishings = request.DocumentAttributes.Any(x => x.Name == JobAttribute.Finishings);
        var hasFinishingsCol = request.DocumentAttributes.Any(x => x.Name == JobAttribute.FinishingsCol);
        if (hasFinishings && hasFinishingsCol)
            throw new IppRequestException("'finishings' and 'finishings-col' are conflicting attributes and cannot be supplied together.", request, IppStatusCode.ClientErrorBadRequest);
    }

    private void ValidateOperationRules(IIppRequestMessage request)
    {
        var operationAttributes = request.OperationAttributes;
        var hasLastDocument = ValidateOperationAttributesGroup && operationAttributes.Any(x => x.Name == JobAttribute.LastDocument);
        var hasDocumentNumber = ValidateOperationAttributesGroup && operationAttributes.Any(x => x.Name == DocumentAttribute.DocumentNumber);
        var hasDocumentUri = ValidateOperationAttributesGroup && operationAttributes.Any(x => x.Name == JobAttribute.DocumentUri);
        int? documentNumber = null;
        bool? lastDocument = null;

        if (hasDocumentNumber)
        {
            var documentNumberValue = operationAttributes.First(x => x.Name == DocumentAttribute.DocumentNumber).Value;
            if (documentNumberValue is int value)
                documentNumber = value;
        }

        if (hasLastDocument)
        {
            var lastDocumentValue = operationAttributes.First(x => x.Name == JobAttribute.LastDocument).Value;
            if (lastDocumentValue is bool value)
                lastDocument = value;
        }

        switch (request.IppOperation)
        {
            case IppOperation.CancelDocument:
            case IppOperation.GetDocumentAttributes:
            case IppOperation.SetDocumentAttributes:
                if (ValidateOperationAttributesGroup)
                {
                    if (!hasDocumentNumber)
                        throw new IppRequestException("missing document-number", request, IppStatusCode.ClientErrorBadRequest);
                    if (documentNumber is null || documentNumber <= 0)
                        throw new IppRequestException("invalid document-number", request, IppStatusCode.ClientErrorBadRequest);
                }
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
                if (ValidateOperationAttributesGroup)
                {
                    if (!hasLastDocument)
                        throw new IppRequestException("missing last-document", request, IppStatusCode.ClientErrorBadRequest);
                    if (lastDocument is null)
                        throw new IppRequestException("invalid last-document", request, IppStatusCode.ClientErrorBadRequest);
                    if (lastDocument == false && request.Document == null)
                        throw new IppRequestException("document stream required when last-document=false", request, IppStatusCode.ClientErrorBadRequest);
                }
                break;

            case IppOperation.SendUri:
                if (ValidateOperationAttributesGroup)
                {
                    if (!hasLastDocument)
                        throw new IppRequestException("missing last-document", request, IppStatusCode.ClientErrorBadRequest);
                    if (lastDocument is null)
                        throw new IppRequestException("invalid last-document", request, IppStatusCode.ClientErrorBadRequest);
                    if (lastDocument == false && !hasDocumentUri)
                        throw new IppRequestException("missing document-uri", request, IppStatusCode.ClientErrorBadRequest);
                }
                break;
        }
    }

    private void ValidateOverridesRules(IIppRequestMessage request)
    {
        var overrideCollections = EnumerateNamedCollections(request.JobAttributes, JobAttribute.Overrides).ToArray();
        if (!overrideCollections.Any())
            return;

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

                var pageRanges = ReadSelectorRanges(members, "pages", required: true, request);
                var documentNumberRanges = ReadSelectorRanges(members, "document-numbers", required: false, request);
                var documentCopyRanges = ReadSelectorRanges(members, "document-copies", required: false, request);

                ValidateSelectorOrder(members, hasDocumentNumbers: documentNumberRanges != null, hasDocumentCopies: documentCopyRanges != null, request);
                ValidateRangesAscendingNonOverlapping(pageRanges!, "pages", request);

                if (documentNumberRanges != null)
                    ValidateRangesAscendingNonOverlapping(documentNumberRanges, "document-numbers", request);

                if (documentCopyRanges != null)
                    ValidateRangesAscendingNonOverlapping(documentCopyRanges, "document-copies", request);

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
        var overridesSupported = Context.OverridesSupported;
        if (overridesSupported is null || overridesSupported.Count == 0)
            return;

        var supportedMemberNames = new HashSet<string>(overridesSupported.Select(x => x.Value), StringComparer.Ordinal);
        if (supportedMemberNames.Count == 0)
            return;

        var unsupportedMembers = EnumerateTopLevelOverrideMemberNames(members)
            .Where(x => !supportedMemberNames.Contains(x))
            .Distinct(StringComparer.Ordinal)
            .ToArray();

        if (unsupportedMembers.Length == 0)
            return;

        if (UseIppAttributeFidelityForCapabilityValidation && !IsIppAttributeFidelityTrue(request))
            return;

        throw new IppRequestException(
            $"invalid overrides: member(s) not supported by target printer: {string.Join(", ", unsupportedMembers)}",
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
        return memberName == "pages" || memberName == "document-numbers" || memberName == "document-copies";
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

        if (selectorOrder.Length == 0 || selectorOrder[0] != "pages")
            throw new IppRequestException("invalid overrides: 'pages' must be the first member attribute", request, IppStatusCode.ClientErrorBadRequest);

        if (hasDocumentNumbers)
        {
            if (Array.IndexOf(selectorOrder, "document-numbers") != 1)
                throw new IppRequestException("invalid overrides: 'document-numbers' must be second when present", request, IppStatusCode.ClientErrorBadRequest);
        }

        if (hasDocumentCopies)
        {
            var expectedIndex = hasDocumentNumbers ? 2 : 1;
            if (Array.IndexOf(selectorOrder, "document-copies") != expectedIndex)
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
