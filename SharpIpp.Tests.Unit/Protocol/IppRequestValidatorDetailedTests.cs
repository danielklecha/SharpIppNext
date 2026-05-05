using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SharpIpp.Exceptions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Protocol;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppRequestValidatorDetailedTests
{
    private static IppRequestMessage CreateBasicRequest(IppOperation operation)
    {
        var request = new IppRequestMessage
        {
            IppOperation = operation,
            RequestId = 123,
        };

        request.OperationAttributes.AddRange(
        [
            new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en"),
            new IppAttribute(Tag.Uri, JobAttribute.PrinterUri, "ipp://127.0.0.1:631/")
        ]);

        return request;
    }

    private static IppAttribute CreateIppAttributeWithValue(Tag tag, string name, object value)
    {
        var ctor = typeof(IppAttribute).GetConstructor(
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance,
            null,
            new[] { typeof(Tag), typeof(string), typeof(object) },
            null)!;
        return (IppAttribute)ctor.Invoke(new object[] { tag, name, value });
    }

    // A helper object whose ToString() returns null — used to exercise x.Value?.ToString() null branches
    private sealed class NullToStringObject
    {
        public override string? ToString() => null;
    }

    [TestMethod]
    public void ValidatePrinterAttributes_OutOfBandReadyCollection_ShouldSkip()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.GetPrinterAttributes);
        request.PrinterAttributes.Add(new IppAttribute(Tag.NoValue, PrinterAttribute.DestinationUriReady, NoValue.Instance));
        
        // Line 227: destinationUriReadyCollection.Count == 1 && destinationUriReadyCollection[0].Tag.IsOutOfBand()
        validator.ValidatePrinterAttributes(request);
    }

    [TestMethod]
    public void ValidatePrinterAttributes_SupportedForbiddenMembers_WithNullValue_ShouldSkip()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.GetPrinterAttributes);
        // Use reflection to inject an attribute whose Value.ToString() returns null (line 242: x.Value?.ToString() is null)
        var attr = CreateIppAttributeWithValue(Tag.Keyword, "destination-attributes-supported", new NullToStringObject());
        request.PrinterAttributes.AddRange(new[] { attr }.ToBegCollection(PrinterAttribute.DestinationUriReady));

        validator.ValidatePrinterAttributes(request);
    }

    [TestMethod]
    public void ValidatePrinterAttributes_SupportedForbiddenMembers_WithForbiddenValue_ShouldThrow()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.GetPrinterAttributes);
        request.PrinterAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.Keyword, "destination-attributes-supported", JobAttribute.DocumentPassword)
        }.ToBegCollection(PrinterAttribute.DestinationUriReady));
        
        // Line 244
        Action act = () => validator.ValidatePrinterAttributes(request);
        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid destination-uri-ready: destination-attributes-supported MUST NOT include password attributes");
    }

    [TestMethod]
    public void ValidatePrinterAttributes_DestinationAttributesOutOfBand_ShouldSkip()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.GetPrinterAttributes);
        request.PrinterAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.NoValue, "destination-attributes", NoValue.Instance),
            new IppAttribute(Tag.Keyword, "destination-attributes-supported", "copies")
        }.ToBegCollection(PrinterAttribute.DestinationUriReady));
        
        // Line 254: destinationAttributesCollection[0].Tag.IsOutOfBand()
        validator.ValidatePrinterAttributes(request);
    }

    [TestMethod]
    public void ValidatePrinterAttributes_DestinationAttributesWithForbiddenMember_ShouldThrow()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.GetPrinterAttributes);
        request.PrinterAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.BegCollection, "destination-attributes", NoValue.Instance),
            new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, JobAttribute.DocumentPassword, "secret"),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
        }.ToBegCollection(PrinterAttribute.DestinationUriReady));
        
        // Line 261
        Action act = () => validator.ValidatePrinterAttributes(request);
        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid destination-uri-ready: destination-attributes MUST NOT include password attributes");
    }

    [TestMethod]
    public void ValidatePrinterAttributes_DestinationAttributesWithWhitespaceName_ShouldPass()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.GetPrinterAttributes);
        var attrWithWhitespaceName = CreateIppAttributeWithValue(Tag.Keyword, " ", "value");
        request.PrinterAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.BegCollection, "destination-attributes", NoValue.Instance),
            attrWithWhitespaceName,
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
        }.ToBegCollection(PrinterAttribute.DestinationUriReady));
        
        // Should hit line 261, but !string.IsNullOrWhiteSpace(x.Name) prevents exception
        validator.ValidatePrinterAttributes(request);
    }

    [TestMethod]
    public void ValidateCollectionMediaSelectionRules_OutOfBandCollection_ShouldSkip()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.CreateJob);
        // Line 283
        validator.ValidateCollectionMediaSelectionRules(new[] { new IppAttribute(Tag.NoValue, JobAttribute.CoverBack, NoValue.Instance) }, request);
    }

    [TestMethod]
    public void ValidateCollectionMediaSelectionRules_InvalidEncoding_ShouldThrow()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.CreateJob);
        // An attribute with empty name and no preceding MemberAttrName causes ArgumentException in FromBegCollection.
        // Must include EndCollection so EnumerateNamedCollections yields the collection for processing.
        var attrs = new[]
        {
            new IppAttribute(Tag.BegCollection, JobAttribute.CoverBack, NoValue.Instance),
            new IppAttribute(Tag.Keyword, string.Empty, "value-without-member-name"),
            new IppAttribute(Tag.EndCollection, string.Empty, NoValue.Instance),
        };

        // Line 294-299
        Action act = () => validator.ValidateCollectionMediaSelectionRules(attrs, request);
        act.Should().Throw<IppRequestException>().WithMessage("invalid cover-back collection encoding");
    }

    [TestMethod]
    public void ValidateOutputBinAgainstSupportedValues_NullValue_ShouldReturn()
    {
        var validator = new IppRequestValidator();
        validator.Context.OutputBinSupported = new[] { new OutputBin("top", true) };
        var request = CreateBasicRequest(IppOperation.CreateJob);
        // Line 345: outputBin.Value?.ToString() returns null — use NullToStringObject
        var attr = CreateIppAttributeWithValue(Tag.Keyword, JobAttribute.OutputBin, new NullToStringObject());
        validator.ValidateOutputBinAgainstSupportedValues(attr, request);
    }

    [TestMethod]
    public void ValidateOutputBinAgainstSupportedValues_WhitespaceValue_ShouldReturn()
    {
        var validator = new IppRequestValidator();
        validator.Context.OutputBinSupported = new[] { new OutputBin("top", true) };
        var request = CreateBasicRequest(IppOperation.CreateJob);
        // Line 347
        validator.ValidateOutputBinAgainstSupportedValues(new IppAttribute(Tag.Keyword, JobAttribute.OutputBin, "   "), request);
    }

    [TestMethod]
    public void ValidateOperationRules_SetDocumentAttributes_WithValidState_ShouldPass()
    {
        var validator = new IppRequestValidator();
        validator.Context.DocumentStatesByNumber = new Dictionary<int, DocumentState> { { 1, DocumentState.Pending } };
        var request = CreateBasicRequest(IppOperation.SetDocumentAttributes);
        request.OperationAttributes.Add(new IppAttribute(Tag.Integer, DocumentAttribute.DocumentNumber, 1));
        request.DocumentAttributes.Add(new IppAttribute(Tag.NameWithoutLanguage, "document-name", "doc"));
        // Line 391
        validator.ValidateOperationRules(request);
    }

    [TestMethod]
    public void ValidateOperationRules_ValidateJob_DocumentPassword_ShouldThrow()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.ValidateJob);
        request.OperationAttributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, JobAttribute.DocumentPassword, "secret"));
        // Line 447
        Action act = () => validator.ValidateOperationRules(request);
        act.Should().Throw<IppRequestException>().WithMessage("document-password is not allowed for validate operations");
    }

    [TestMethod]
    public void ValidateCreateJobDestinationRules_GroupDisabled_ShouldReturn()
    {
        var validator = new IppRequestValidator { ValidateOperationAttributesGroup = false };
        var request = CreateBasicRequest(IppOperation.CreateJob);
        // Line 464
        validator.ValidateCreateJobDestinationRules(request.OperationAttributes, request);
    }

    [TestMethod]
    public void ValidateCreateJobDestinationRules_OutOfBandUriCollection_ShouldSkip()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.CreateJob);
        request.OperationAttributes.Add(new IppAttribute(Tag.NoValue, JobAttribute.DestinationUris, NoValue.Instance));
        // Line 473
        validator.ValidateCreateJobDestinationRules(request.OperationAttributes, request);
    }

    [TestMethod]
    public void ValidateCreateJobDestinationRules_NullUriValue_ShouldSkip()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.CreateJob);
        // Line 487: destinationUri is null — inject via reflection
        var attr = CreateIppAttributeWithValue(Tag.Uri, "destination-uri", new NullToStringObject());
        request.OperationAttributes.AddRange(new[] { attr }.ToBegCollection(JobAttribute.DestinationUris));
        validator.ValidateCreateJobDestinationRules(request.OperationAttributes, request);
    }

    [TestMethod]
    public void ValidateCreateJobDestinationRules_ValidAbsoluteUri_ShouldExtractScheme()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.CreateJob);
        request.OperationAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.Uri, "destination-uri", "mailto:test@example.com")
        }.ToBegCollection(JobAttribute.DestinationUris));
        // Line 493, 503, 505
        validator.ValidateCreateJobDestinationRules(request.OperationAttributes, request);
    }

    [TestMethod]
    public void ValidateCreateJobDestinationRules_ValidForbiddenScheme_ShouldThrow()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.CreateJob);
        request.OperationAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.Uri, "destination-uri", "fax:+123456")
        }.ToBegCollection(JobAttribute.DestinationUris));
        
        Action act = () => validator.ValidateCreateJobDestinationRules(request.OperationAttributes, request);
        act.Should().Throw<IppRequestException>().WithMessage("invalid destination-uri scheme for Scan");
    }

    [TestMethod]
    public void ValidateCreateJobDestinationRules_InvalidUriWithColon_ShouldCoverElseBranch()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.CreateJob);
        request.OperationAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.Uri, "destination-uri", "123:abc")
        }.ToBegCollection(JobAttribute.DestinationUris));
        
        validator.ValidateCreateJobDestinationRules(request.OperationAttributes, request);
    }

    [TestMethod]
    public void ValidateCreateJobDestinationRules_NoScheme_ShouldPass()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.CreateJob);
        request.OperationAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.Uri, "destination-uri", "noscheme")
        }.ToBegCollection(JobAttribute.DestinationUris));
        
        validator.ValidateCreateJobDestinationRules(request.OperationAttributes, request);
    }

    [TestMethod]
    public void ValidateCreateJobDestinationRules_CaseInsensitiveScheme_ShouldThrow()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.CreateJob);
        request.OperationAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.Uri, "destination-uri", "FAX:+123456")
        }.ToBegCollection(JobAttribute.DestinationUris));
        
        Action act = () => validator.ValidateCreateJobDestinationRules(request.OperationAttributes, request);
        act.Should().Throw<IppRequestException>().WithMessage("invalid destination-uri scheme for Scan");
    }

    [TestMethod]
    public void ValidateCreateJobDestinationRules_EmptyStringUri_ShouldSkip()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.CreateJob);
        request.OperationAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.Uri, "destination-uri", string.Empty)
        }.ToBegCollection(JobAttribute.DestinationUris));
        
        validator.ValidateCreateJobDestinationRules(request.OperationAttributes, request);
    }

    [TestMethod]
    public void ValidateCreateJobDestinationRules_MismatchedAccesses_ShouldThrow()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.CreateJob);
        request.OperationAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.Uri, "destination-uri", "mailto:test@example.com")
        }.ToBegCollection(JobAttribute.DestinationUris));
        
        request.OperationAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.Keyword, "access-oauth-token", "token")
        }.ToBegCollection(JobAttribute.DestinationAccesses));
        request.OperationAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.Keyword, "access-oauth-token", "token2")
        }.ToBegCollection(JobAttribute.DestinationAccesses));

        // Line 511
        Action act = () => validator.ValidateCreateJobDestinationRules(request.OperationAttributes, request);
        act.Should().Throw<IppRequestException>().WithMessage("destination-accesses cardinality MUST match destination-uris");
    }

    [TestMethod]
    public void ValidateJobRequestedAttributesGroupKeywords_NullValue_ShouldSkip()
    {
        var validator = new IppRequestValidator();
        validator.Context.JobRequestedAttributeGroupKeywordsSupported = new[] { "all" };
        var request = CreateBasicRequest(IppOperation.GetJobAttributes);
        // Line 529: x.Value?.ToString() returns null — inject via reflection
        request.OperationAttributes.Add(CreateIppAttributeWithValue(Tag.Keyword, JobAttribute.RequestedAttributes, new NullToStringObject()));
        // Line 535: requestedAttributes.Length == 0 because the null-ToString value is filtered by IsNullOrWhiteSpace
        validator.ValidateJobRequestedAttributesGroupKeywords(request);
    }

    [TestMethod]
    public void ValidateJobRequestedAttributesGroupKeywords_FidelityTrue_ShouldReturn()
    {
        var validator = new IppRequestValidator { UseIppAttributeFidelityForCapabilityValidation = true };
        validator.Context.JobRequestedAttributeGroupKeywordsSupported = new[] { "all" };
        var request = CreateBasicRequest(IppOperation.GetJobAttributes);
        request.OperationAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.RequestedAttributes, "job-description"));
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        // Line 548
        Action act = () => validator.ValidateJobRequestedAttributesGroupKeywords(request);
        act.Should().Throw<IppRequestException>();
        
        request.OperationAttributes.RemoveAll(x => x.Name == JobAttribute.IppAttributeFidelity);
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, false));
        validator.ValidateJobRequestedAttributesGroupKeywords(request); // shouldn't throw when fidelity is false
    }

    [TestMethod]
    public void ValidateRequiredLastDocument_GroupDisabled_ShouldReturn()
    {
        var validator = new IppRequestValidator { ValidateOperationAttributesGroup = false };
        // Line 570
        validator.ValidateRequiredLastDocument(false, null, CreateBasicRequest(IppOperation.SendDocument));
    }

    [TestMethod]
    public void ValidateRequiredOutputDeviceUuid_GroupDisabled_ShouldReturn()
    {
        var validator = new IppRequestValidator { ValidateOperationAttributesGroup = false };
        // Line 581
        validator.ValidateRequiredOutputDeviceUuid(false, CreateBasicRequest(IppOperation.UpdateJobStatus));
    }


    [TestMethod]
    public void ValidateFetchStatusCodeNotSuccessful_SuccessfulEnum_ShouldThrow()
    {
        var request = CreateBasicRequest(IppOperation.AcknowledgeJob);
        // Line 598 — value is IppStatusCode enum type directly (requires internal ctor via reflection)
        var attr = CreateIppAttributeWithValue(Tag.Enum, JobAttribute.FetchStatusCode, IppStatusCode.SuccessfulOk);
        Action act = () => IppRequestValidator.ValidateFetchStatusCodeNotSuccessful(new[] { attr }, request);
        act.Should().Throw<IppRequestException>().WithMessage("fetch-status-code MUST NOT be successful-ok");
    }

    [TestMethod]
    public void ValidateFetchStatusCodeNotSuccessful_SuccessfulInt_ShouldThrow()
    {
        var request = CreateBasicRequest(IppOperation.AcknowledgeJob);
        // Line 600
        Action act = () => IppRequestValidator.ValidateFetchStatusCodeNotSuccessful(
            new[] { new IppAttribute(Tag.Enum, JobAttribute.FetchStatusCode, (int)IppStatusCode.SuccessfulOk) }, request);
        act.Should().Throw<IppRequestException>().WithMessage("fetch-status-code MUST NOT be successful-ok");
    }

    [TestMethod]
    public void ValidateFetchStatusCodeNotSuccessful_SuccessfulShort_ShouldThrow()
    {
        var request = CreateBasicRequest(IppOperation.AcknowledgeJob);
        // Line 604 — value is short, requires internal ctor via reflection
        var attr = CreateIppAttributeWithValue(Tag.Enum, JobAttribute.FetchStatusCode, (short)IppStatusCode.SuccessfulOk);
        Action act = () => IppRequestValidator.ValidateFetchStatusCodeNotSuccessful(new[] { attr }, request);
        act.Should().Throw<IppRequestException>().WithMessage("fetch-status-code MUST NOT be successful-ok");
    }

    [TestMethod]
    public void ValidateDocumentRequestedAttributesGroupKeywords_NullValue_ShouldSkip()
    {
        var validator = new IppRequestValidator();
        validator.Context.DocumentRequestedAttributeGroupKeywordsSupported = new[] { "all" };
        var request = CreateBasicRequest(IppOperation.GetDocumentAttributes);
        // Line 635: x.Value?.ToString() returns null; line 641: filtered out -> length==0
        request.OperationAttributes.Add(CreateIppAttributeWithValue(Tag.Keyword, JobAttribute.RequestedAttributes, new NullToStringObject()));
        validator.ValidateDocumentRequestedAttributesGroupKeywords(request);
    }

    [TestMethod]
    public void ValidateDocumentRequestedAttributesGroupKeywords_FidelityFalse_ShouldReturn()
    {
        var validator = new IppRequestValidator { UseIppAttributeFidelityForCapabilityValidation = true };
        validator.Context.DocumentRequestedAttributeGroupKeywordsSupported = new[] { "all" };
        var request = CreateBasicRequest(IppOperation.GetDocumentAttributes);
        request.OperationAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.RequestedAttributes, DocumentAttribute.DocumentDescription));
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, false));
        // Line 654
        validator.ValidateDocumentRequestedAttributesGroupKeywords(request);
    }

    [TestMethod]
    public void ValidateNotifyEventsValues_NullValue_ShouldSkip()
    {
        var validator = new IppRequestValidator();
        validator.Context.NotifyEventsSupported = new[] { "job-completed" };
        var request = CreateBasicRequest(IppOperation.CreateJob);
        // Line 675: x.Value?.ToString() returns null; filtered by IsNullOrWhiteSpace so length==0 -> returns early
        request.SubscriptionAttributes.Add(CreateIppAttributeWithValue(Tag.Keyword, "notify-events", new NullToStringObject()));
        validator.ValidateNotifyEventsValues(request);
    }

    [TestMethod]
    public void ValidateNotifyEventsValues_FidelityFalse_ShouldReturn()
    {
        var validator = new IppRequestValidator { UseIppAttributeFidelityForCapabilityValidation = true };
        validator.Context.NotifyEventsSupported = new[] { "job-completed" };
        var request = CreateBasicRequest(IppOperation.CreateJob);
        request.SubscriptionAttributes.Add(new IppAttribute(Tag.Keyword, "notify-events", "job-created"));
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, false));
        // Line 692
        validator.ValidateNotifyEventsValues(request);
    }

    [TestMethod]
    public void ValidateCancelDocumentStateRules_StateNotPresent_ShouldReturn()
    {
        var validator = new IppRequestValidator();
        validator.Context.DocumentStatesByNumber = new Dictionary<int, DocumentState> { { 2, DocumentState.Pending } };
        // Line 706
        validator.ValidateCancelDocumentStateRules(1, CreateBasicRequest(IppOperation.CancelDocument));
    }

    [TestMethod]
    public void ValidateCancelDocumentStateRules_StateNotProcessing_ShouldReturn()
    {
        var validator = new IppRequestValidator();
        validator.Context.DocumentStatesByNumber = new Dictionary<int, DocumentState> { { 1, DocumentState.Pending } };
        // Line 712
        validator.ValidateCancelDocumentStateRules(1, CreateBasicRequest(IppOperation.CancelDocument));
    }

    [TestMethod]
    public void ValidateCancelDocumentStateRules_NoStateReasonsDictionary_ShouldReturn()
    {
        var validator = new IppRequestValidator();
        validator.Context.DocumentStatesByNumber = new Dictionary<int, DocumentState> { { 1, DocumentState.Processing } };
        validator.Context.DocumentStateReasonsByNumber = null;
        // Line 715
        validator.ValidateCancelDocumentStateRules(1, CreateBasicRequest(IppOperation.CancelDocument));
    }

    [TestMethod]
    public void ValidateCancelDocumentStateRules_NoStateReasonsForNumber_ShouldReturn()
    {
        var validator = new IppRequestValidator();
        validator.Context.DocumentStatesByNumber = new Dictionary<int, DocumentState> { { 1, DocumentState.Processing } };
        validator.Context.DocumentStateReasonsByNumber = new Dictionary<int, IReadOnlyCollection<DocumentStateReason>>();
        // Line 718
        validator.ValidateCancelDocumentStateRules(1, CreateBasicRequest(IppOperation.CancelDocument));
    }

    [TestMethod]
    public void ValidateSetDocumentAttributesStateRules_StateNotPresent_ShouldReturn()
    {
        var validator = new IppRequestValidator();
        validator.Context.DocumentStatesByNumber = new Dictionary<int, DocumentState> { { 2, DocumentState.Pending } };
        // Line 730
        validator.ValidateSetDocumentAttributesStateRules(1, CreateBasicRequest(IppOperation.SetDocumentAttributes));
    }

    [TestMethod]
    public void ValidateSetDocumentAttributesStateRules_StateCompleted_ShouldThrow()
    {
        var validator = new IppRequestValidator();
        validator.Context.DocumentStatesByNumber = new Dictionary<int, DocumentState> { { 1, DocumentState.Completed } };
        // Line 733
        Action act = () => validator.ValidateSetDocumentAttributesStateRules(1, CreateBasicRequest(IppOperation.SetDocumentAttributes));
        act.Should().Throw<IppRequestException>().WithMessage("invalid document-state for Set-Document-Attributes");
    }

    [TestMethod]
    public void ValidateSetDocumentAttributesStateRules_ProcessingDisallowed_ShouldThrow()
    {
        var validator = new IppRequestValidator();
        validator.Context.DocumentStatesByNumber = new Dictionary<int, DocumentState> { { 1, DocumentState.Processing } };
        validator.Context.AllowSetDocumentAttributesWhenProcessing = false;
        // Line 735
        Action act = () => validator.ValidateSetDocumentAttributesStateRules(1, CreateBasicRequest(IppOperation.SetDocumentAttributes));
        act.Should().Throw<IppRequestException>().WithMessage("invalid document-state for Set-Document-Attributes");
    }

    [TestMethod]
    public void ValidateOverrideMembersAgainstSupportedValues_WithScopes_ShouldFilterCorrectly()
    {
        var validator = new IppRequestValidator();
        validator.Context.OverrideMemberScopesByName = new Dictionary<string, OverrideMemberScope>
        {
            { "supported-job-attr", OverrideMemberScope.Job },
            { "unsupported-system-attr", OverrideMemberScope.Page }
        };
        
        var request = CreateBasicRequest(IppOperation.CreateJob);
        var members = new[]
        {
            new IppAttribute(Tag.Keyword, "supported-job-attr", "value")
        };
        // Lines 818-819
        Action act = () => validator.ValidateOverrideMembersAgainstSupportedValues(members, request);
        act.Should().Throw<IppRequestException>().WithMessage("invalid overrides: member(s) not supported by target printer: supported-job-attr");
    }

    [TestMethod]
    public void ValidateOverrideMembersAgainstSupportedValues_WithDocumentScope_ShouldThrow()
    {
        var validator = new IppRequestValidator();
        validator.Context.OverrideMemberScopesByName = new Dictionary<string, OverrideMemberScope>
        {
            { "supported-doc-attr", OverrideMemberScope.Document }
        };
        
        var request = CreateBasicRequest(IppOperation.CreateJob);
        var members = new[]
        {
            new IppAttribute(Tag.Keyword, "supported-doc-attr", "value")
        };
        
        Action act = () => validator.ValidateOverrideMembersAgainstSupportedValues(members, request);
        act.Should().Throw<IppRequestException>().WithMessage("invalid overrides: member(s) not supported by target printer: supported-doc-attr");
    }

    [TestMethod]
    public void ValidateOverrideMembersAgainstSupportedValues_WithPageScope_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        validator.Context.OverrideMemberScopesByName = new Dictionary<string, OverrideMemberScope>
        {
            { "supported-page-attr", OverrideMemberScope.Page }
        };
        
        var request = CreateBasicRequest(IppOperation.CreateJob);
        var members = new[]
        {
            new IppAttribute(Tag.Keyword, "supported-page-attr", "value")
        };
        
        validator.ValidateOverrideMembersAgainstSupportedValues(members, request);
    }

    [TestMethod]
    public void ValidateOverrideMembersAgainstSupportedValues_WithUnknownScope_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        validator.Context.OverrideMemberScopesByName = new Dictionary<string, OverrideMemberScope>
        {
            { "other-attr", OverrideMemberScope.Page }
        };
        
        var request = CreateBasicRequest(IppOperation.CreateJob);
        var members = new[]
        {
            new IppAttribute(Tag.Keyword, "unknown-attr", "value")
        };
        
        validator.ValidateOverrideMembersAgainstSupportedValues(members, request);
    }

    [TestMethod]
    public void ValidatePrinterAttributes_DestinationAttributesWithValidMember_ShouldPass()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.GetPrinterAttributes);
        request.PrinterAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.BegCollection, "destination-attributes", NoValue.Instance),
            new IppAttribute(Tag.Integer, JobAttribute.Copies, 1),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
        }.ToBegCollection(PrinterAttribute.DestinationUriReady));
        
        validator.ValidatePrinterAttributes(request);
    }

    [TestMethod]
    public void ValidatePrinterAttributes_SupportedValidMembers_ShouldPass()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.GetPrinterAttributes);
        request.PrinterAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.Keyword, "destination-attributes-supported", JobAttribute.Copies)
        }.ToBegCollection(PrinterAttribute.DestinationUriReady));
        
        validator.ValidatePrinterAttributes(request);
    }

    [TestMethod]
    public void ValidateOverridesRules_OperationSupported_ShouldPass()
    {
        var validator = new IppRequestValidator();
        validator.Context.OverridesSupportedOperations = new HashSet<IppOperation> { IppOperation.CreateJob };
        var request = CreateBasicRequest(IppOperation.CreateJob);
        request.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.BegCollection, JobAttribute.Overrides, NoValue.Instance),
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.Keyword, "some-job-attr", "value"),
            new IppAttribute(Tag.EndCollection, string.Empty, NoValue.Instance)
        });
        
        validator.Context.OverrideMemberScopesByName = new Dictionary<string, OverrideMemberScope>
        {
            { "some-job-attr", OverrideMemberScope.Page }
        };
        validator.ValidateJobAttributesGroup = true;
        validator.Validate(request);
    }

    [TestMethod]
    public void ValidateOperationRules_ValidateJob_NoDocumentPassword_ShouldPass()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.ValidateJob);
        validator.ValidateOperationRules(request);
    }

    [TestMethod]
    public void ValidateOperationRules_SetDocumentAttributes_GroupDisabled_ShouldPass()
    {
        var validator = new IppRequestValidator { ValidateOperationAttributesGroup = false };
        var request = CreateBasicRequest(IppOperation.SetDocumentAttributes);
        request.DocumentAttributes.Add(new IppAttribute(Tag.NameWithoutLanguage, "document-name", "doc"));
        validator.ValidateOperationRules(request);
    }

    [TestMethod]
    public void ValidateOperationRules_ValidateJob_GroupDisabled_ShouldPass()
    {
        var validator = new IppRequestValidator { ValidateOperationAttributesGroup = false };
        var request = CreateBasicRequest(IppOperation.ValidateJob);
        request.OperationAttributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, JobAttribute.DocumentPassword, "secret"));
        validator.ValidateOperationRules(request);
    }

    [TestMethod]
    public void ValidateOperationRules_ValidateDocument_DocumentPassword_ShouldThrow()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.ValidateDocument);
        request.OperationAttributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, JobAttribute.DocumentPassword, "secret"));
        Action act = () => validator.ValidateOperationRules(request);
        act.Should().Throw<IppRequestException>().WithMessage("document-password is not allowed for validate operations");
    }

    [TestMethod]
    public void ValidateOperationRules_ValidateDocument_NoDocumentPassword_ShouldPass()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.ValidateDocument);
        validator.ValidateOperationRules(request);
    }

    [TestMethod]
    public void ValidateCollectionMediaSelectionRules_EmptyCollection_ShouldSkip()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.CreateJob);
        var attrs = new[]
        {
            new IppAttribute(Tag.BegCollection, JobAttribute.CoverBack, NoValue.Instance),
            new IppAttribute(Tag.EndCollection, string.Empty, NoValue.Instance),
        };
        validator.ValidateCollectionMediaSelectionRules(attrs, request);
    }

    [TestMethod]
    public void ValidateOutputBinAgainstSupportedValues_ValidValue_ShouldPass()
    {
        var validator = new IppRequestValidator();
        validator.Context.OutputBinSupported = new[] { new OutputBin("top", true) };
        var request = CreateBasicRequest(IppOperation.CreateJob);
        var attr = new IppAttribute(Tag.Keyword, JobAttribute.OutputBin, "top");
        validator.ValidateOutputBinAgainstSupportedValues(attr, request);
    }
}
