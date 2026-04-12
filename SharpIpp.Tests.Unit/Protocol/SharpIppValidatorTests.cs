using System;
using System.Diagnostics.CodeAnalysis;
using SharpIpp.Exceptions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Protocol;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppRequestValidatorTests
{
    [TestMethod]
    public void Default_ShouldReturnNewValidatorEachTime()
    {
        var first = IppRequestValidator.Default;
        var second = IppRequestValidator.Default;

        first.Should().NotBeSameAs(second);
    }

    [TestMethod]
    public void Default_ShouldUseSafeNonFidelityAwareSettings()
    {
        var validator = IppRequestValidator.Default;

        validator.ValidateCoreRules.Should().BeTrue();
        validator.ValidateOperationSpecificRules.Should().BeTrue();
        validator.ValidateOperationAttributesGroup.Should().BeTrue();
        validator.ValidateJobAttributesGroup.Should().BeTrue();
        validator.ValidatePrinterAttributesGroup.Should().BeTrue();
        validator.ValidateUnsupportedAttributesGroup.Should().BeTrue();
        validator.ValidateSubscriptionAttributesGroup.Should().BeTrue();
        validator.ValidateEventNotificationAttributesGroup.Should().BeTrue();
        validator.ValidateResourceAttributesGroup.Should().BeTrue();
        validator.ValidateDocumentAttributesGroup.Should().BeTrue();
        validator.ValidateSystemAttributesGroup.Should().BeTrue();
        validator.UseIppAttributeFidelityForCapabilityValidation.Should().BeFalse();
    }

    [TestMethod]
    public void Validate_WhenOperationAttributesGroupDisabled_ShouldAllowMissingOperationAttributes()
    {
        var validator = new IppRequestValidator
        {
            ValidateOperationAttributesGroup = false,
            ValidateOperationSpecificRules = false,
            ValidateJobAttributesGroup = false,
        };

        var request = new IppRequestMessage
        {
            IppOperation = IppOperation.CreateJob,
            RequestId = 123,
        };

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_WhenJobAttributesGroupDisabled_ShouldAllowConflictingFinishingsAttributes()
    {
        var validator = new IppRequestValidator
        {
            ValidateJobAttributesGroup = false,
            ValidateOperationSpecificRules = false,
        };

        var request = CreateBasicRequest(IppOperation.CreateJob);
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.Finishings, (int)Finishings.Staple));
        request.JobAttributes.Add(new IppAttribute(Tag.BegCollection, JobAttribute.FinishingsCol, NoValue.Instance));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_WhenConflictingFinishingsAttributesPresent_ShouldThrowException()
    {
        var validator = new IppRequestValidator
        {
            ValidateOperationSpecificRules = false,
        };

        var request = CreateBasicRequest(IppOperation.CreateJob);
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.Finishings, (int)Finishings.Staple));
        request.JobAttributes.Add(new IppAttribute(Tag.BegCollection, JobAttribute.FinishingsCol, NoValue.Instance));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("'finishings' and 'finishings-col' are conflicting attributes and cannot be supplied together.")
            .Which.RequestMessage.Should().Be(request);
    }

    [TestMethod]
    public void Validate_WhenOperationSpecificRulesDisabled_ShouldAllowPrintJobWithoutDocument()
    {
        var validator = new IppRequestValidator
        {
            ValidateOperationSpecificRules = false,
        };

        var request = CreateBasicRequest(IppOperation.PrintJob);

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_WhenOperationSpecificRulesEnabled_ShouldRejectPrintJobWithoutDocument()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("document stream required")
            .Which.RequestMessage.Should().Be(request);
    }

    [TestMethod]
    public void Validate_WhenOverrideMemberIsUnsupportedByContextCapabilities_ShouldThrowException()
    {
        var validator = new IppRequestValidator();
        validator.Context.OverridesSupported =
        [
            OverrideSupported.Pages,
            OverrideSupported.Sides
        ];

        var request = CreateBasicRequest(IppOperation.ValidateJob);
        request.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.Keyword, JobAttribute.Media, "iso_a4_210x297mm")
        }.ToBegCollection(JobAttribute.Overrides));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid overrides: member(s) not supported by target printer: media")
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    [TestMethod]
    public void Validate_WhenNestedOverrideCollectionMemberIsSupported_ShouldIgnoreNestedAttributes()
    {
        var validator = new IppRequestValidator();
        validator.Context.OverridesSupported =
        [
            OverrideSupported.Pages,
            OverrideSupported.MediaCol
        ];

        var request = CreateBasicRequest(IppOperation.ValidateJob);
        request.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.BegCollection, JobAttribute.MediaCol, NoValue.Instance),
            new IppAttribute(Tag.BegCollection, "media-size", NoValue.Instance),
            new IppAttribute(Tag.Integer, "x-dimension", 21000),
            new IppAttribute(Tag.Integer, "y-dimension", 29700),
            new IppAttribute(Tag.EndCollection, string.Empty, NoValue.Instance),
            new IppAttribute(Tag.EndCollection, string.Empty, NoValue.Instance)
        }.ToBegCollection(JobAttribute.Overrides));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_WhenFidelityAwareModeEnabledAndIppAttributeFidelityFalse_ShouldNotHardFailOnUnsupportedOverrideMembers()
    {
        var validator = new IppRequestValidator
        {
            UseIppAttributeFidelityForCapabilityValidation = true
        };
        validator.Context.OverridesSupported =
        [
            OverrideSupported.Pages,
            OverrideSupported.Sides
        ];

        var request = CreateBasicRequest(IppOperation.ValidateJob);
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, false));
        request.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.Keyword, JobAttribute.Media, "iso_a4_210x297mm")
        }.ToBegCollection(JobAttribute.Overrides));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_WhenFidelityAwareModeEnabledAndIppAttributeFidelityTrue_ShouldHardFailOnUnsupportedOverrideMembers()
    {
        var validator = new IppRequestValidator
        {
            UseIppAttributeFidelityForCapabilityValidation = true
        };
        validator.Context.OverridesSupported =
        [
            OverrideSupported.Pages,
            OverrideSupported.Sides
        ];

        var request = CreateBasicRequest(IppOperation.ValidateJob);
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        request.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.Keyword, JobAttribute.Media, "iso_a4_210x297mm")
        }.ToBegCollection(JobAttribute.Overrides));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid overrides: member(s) not supported by target printer: media")
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    [TestMethod]
    public void Validate_WhenJobRequestedAttributesGroupKeywordUnsupported_ShouldThrowException()
    {
        var validator = new IppRequestValidator();
        validator.Context.JobRequestedAttributeGroupKeywordsSupported =
        [
            "all",
            "job-description",
            "job-template"
        ];

        var request = CreateBasicRequest(IppOperation.GetJobAttributes);
        request.OperationAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.RequestedAttributes, "job-actual"));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("requested-attributes group value(s) not supported: job-actual")
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    [TestMethod]
    public void Validate_WhenJobRequestedAttributesGroupKeywordSupported_ShouldNotThrowException()
    {
        var validator = new IppRequestValidator();
        validator.Context.JobRequestedAttributeGroupKeywordsSupported =
        [
            "all",
            "job-description",
            "job-template",
            "job-actual"
        ];

        var request = CreateBasicRequest(IppOperation.GetJobs);
        request.OperationAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.RequestedAttributes, "job-actual"));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

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
}
