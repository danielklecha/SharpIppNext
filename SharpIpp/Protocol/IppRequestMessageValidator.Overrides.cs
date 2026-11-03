using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Exceptions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol;

public partial class IppRequestMessageValidator
{
    private const string OverridePagesMemberName = "pages";
    private const string OverrideDocumentNumbersMemberName = "document-numbers";
    private const string OverrideDocumentCopiesMemberName = "document-copies";

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
        IppAttributeNames.Copies,
        IppAttributeNames.CoverBack,
        IppAttributeNames.CoverFront,
        IppAttributeNames.InsertSheet,
        IppAttributeNames.JobAccountId,
        IppAttributeNames.JobAccountingSheets,
        IppAttributeNames.JobAccountingUserId,
        IppAttributeNames.JobErrorSheet,
        IppAttributeNames.JobHoldUntil,
        IppAttributeNames.JobMessageToOperator,
        IppAttributeNames.JobPriority,
        IppAttributeNames.JobSheets,
        IppAttributeNames.JobSheetsCol,
        IppAttributeNames.MediaInputTrayCheck,
        IppAttributeNames.MultipleDocumentHandling,
        IppAttributeNames.OutputBin,
        IppAttributeNames.PageDelivery,
        IppAttributeNames.PageRanges,
        IppAttributeNames.SeparatorSheets,
        "sheet-collate",
    ];

    /// <summary>
    /// Validates job overrides collections including formatting, page/document selectors, and membership rules.
    /// Spec: PWG 5100.6 Section 3 (Overrides).
    /// </summary>
    private void ValidateOverridesRules(IIppRequestMessage request, IppRequestValidationContext context)
    {
        var overrideCollections = request.JobAttributes.EnumerateNamedCollections(IppAttributeNames.Overrides).ToArray();
        if (!overrideCollections.Any())
            return;

        var supportedOperations = context.OverridesSupportedOperations;
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

                ValidateOverrideMembersAgainstSupportedValues(members, request, context);

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
    public virtual void ValidateOverrideMembersAgainstSupportedValues(IReadOnlyCollection<IppAttribute> members, IIppRequestMessage request, IppRequestValidationContext? context = null)
    {
        var memberNames = EnumerateTopLevelOverrideMemberNames(members)
            .Distinct(StringComparer.Ordinal)
            .ToArray();

        var unsupportedMembers = new List<string>();

        var ctx = context ?? Context;
        var overrideMemberScopesByName = ctx.OverrideMemberScopesByName;
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

        var overridesSupported = ctx.OverridesSupported;
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
    /// <summary>
    /// Reads and validates selector ranges (pages, document-numbers, document-copies) from overrides collection members.
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
    /// Helper method to check if a member name is one of the overrides selector attributes.
    /// </summary>
    private static bool IsSelectorMember(string memberName)
    {
        return OverrideSelectorMemberNames.Contains(memberName);
    }

    /// <summary>
    /// Validates selector ordering inside an overrides collection:
    /// pages MUST be first, followed optionally by document-numbers, followed optionally by document-copies.
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
    /// Validates that a list of integer ranges is strictly ascending and non-overlapping:
    /// range.Lower &lt;= range.Upper, and each subsequent range's Lower &gt; previous range's Upper.
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
    /// Validates that document-numbers ranges across multiple overrides collections do not overlap.
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
}

