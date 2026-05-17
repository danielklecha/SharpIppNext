using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Exceptions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Protocol;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppRequestMessageValidatorTests
{
    #region Helpers and Shared Types

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

    private static IppRequestMessage CreateBasicSystemRequest(IppOperation operation)
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
            new IppAttribute(Tag.Uri, SystemAttribute.SystemUri, "ipp://127.0.0.1:8631/system")
        ]);

        return request;
    }

    private static IppRequestMessage CreateMinimalCreateJobRequest(List<IppAttribute> operationAttributes)
    {
        var request = new IppRequestMessage
        {
            RequestId = 1,
            Version = new IppVersion(2, 0),
            IppOperation = IppOperation.CreateJob
        };

        request.OperationAttributes.AddRange(operationAttributes);
        return request;
    }

    private static IppRequestMessage CreateMinimalSetPrinterAttributesRequest(List<IppAttribute> printerAttributes)
    {
        var request = new IppRequestMessage
        {
            RequestId = 2,
            Version = new IppVersion(2, 0),
            IppOperation = IppOperation.SetPrinterAttributes
        };

        request.OperationAttributes.Add(new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"));
        request.OperationAttributes.Add(new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en"));
        request.OperationAttributes.Add(new IppAttribute(Tag.Uri, JobAttribute.PrinterUri, "ipp://printer"));
        request.PrinterAttributes.AddRange(printerAttributes);
        return request;
    }

    private sealed class NullToStringObject
    {
        public override string? ToString() => null;
    }

    #endregion

    #region Base Validation Tests

    [TestMethod]
    public void Default_ShouldReturnNewValidatorEachTime()
    {
        var first = IppRequestMessageValidator.Default;
        var second = IppRequestMessageValidator.Default;

        first.Should().NotBeSameAs(second);
    }

    [TestMethod]
    public void Default_ShouldUseSafeNonFidelityAwareSettings()
    {
        var validator = IppRequestMessageValidator.Default;

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
        var validator = new IppRequestMessageValidator
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
        var validator = new IppRequestMessageValidator
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
        var validator = new IppRequestMessageValidator
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
        var validator = new IppRequestMessageValidator
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
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("document stream required")
            .Which.RequestMessage.Should().Be(request);
    }

    [TestMethod]
    public void Validate_CreateJob_WithDestinationAccessesCardinalityMismatch_Throws()
    {
        var attributes = new List<IppAttribute>
        {
            new(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"),
            new(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en"),
            new(Tag.Uri, JobAttribute.PrinterUri, "ipp://printer"),

            new(Tag.BegCollection, JobAttribute.DestinationUris, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "destination-uri"),
            new(Tag.Uri, string.Empty, "https://example.test/a"),
            new(Tag.EndCollection, string.Empty, NoValue.Instance),

            new(Tag.BegCollection, JobAttribute.DestinationUris, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "destination-uri"),
            new(Tag.Uri, string.Empty, "https://example.test/b"),
            new(Tag.EndCollection, string.Empty, NoValue.Instance),

            new(Tag.BegCollection, JobAttribute.DestinationAccesses, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "access-user-name"),
            new(Tag.NameWithoutLanguage, string.Empty, "scan-user"),
            new(Tag.EndCollection, string.Empty, NoValue.Instance)
        };

        var request = CreateMinimalCreateJobRequest(attributes);
        var validator = IppRequestMessageValidator.Default;

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("*destination-accesses cardinality MUST match destination-uris*");
    }

    [TestMethod]
    public void Validate_CreateJob_WithForbiddenDestinationUriScheme_Throws()
    {
        var attributes = new List<IppAttribute>
        {
            new(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"),
            new(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en"),
            new(Tag.Uri, JobAttribute.PrinterUri, "ipp://printer"),

            new(Tag.BegCollection, JobAttribute.DestinationUris, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "destination-uri"),
            new(Tag.Uri, string.Empty, "tel:+12025550123"),
            new(Tag.EndCollection, string.Empty, NoValue.Instance)
        };

        var request = CreateMinimalCreateJobRequest(attributes);
        var validator = IppRequestMessageValidator.Default;

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("*invalid destination-uri scheme for Scan*");
    }

    [TestMethod]
    public void Validate_CreateJob_WithForbiddenDestinationUriMembers_Throws()
    {
        var attributes = new List<IppAttribute>
        {
            new(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"),
            new(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en"),
            new(Tag.Uri, JobAttribute.PrinterUri, "ipp://printer"),

            new(Tag.BegCollection, JobAttribute.DestinationUris, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "destination-uri"),
            new(Tag.Uri, string.Empty, "https://example.test/upload"),
            new(Tag.MemberAttrName, string.Empty, "post-dial-string"),
            new(Tag.TextWithoutLanguage, string.Empty, "123"),
            new(Tag.EndCollection, string.Empty, NoValue.Instance)
        };

        var request = CreateMinimalCreateJobRequest(attributes);
        var validator = IppRequestMessageValidator.Default;

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("*reserved fax member attributes are not allowed for Scan*");
    }

    [TestMethod]
    [DataRow("destination-oauth-scope", Tag.OctetStringWithAnUnspecifiedFormat, "scope1 scope2", "destination-oauth-scope requires destination-oauth-token")]
    [DataRow("destination-oauth-uri", Tag.Uri, "https://issuer.example/token", "destination-oauth-uri requires destination-oauth-token")]
    public void Validate_SetPrinterAttributes_WithDestinationOAuthAttributeWithoutToken_Throws(string attrName, Tag tag, object val, string expectedMsg)
    {
        var printerAttributes = new List<IppAttribute>
        {
            new(Tag.BegCollection, PrinterAttribute.DestinationUriReady, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, attrName),
            IppAttribute.Create(tag, string.Empty, val),
            new(Tag.EndCollection, string.Empty, NoValue.Instance)
        };

        var request = CreateMinimalSetPrinterAttributesRequest(printerAttributes);
        var validator = IppRequestMessageValidator.Default;

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage($"*{expectedMsg}*");
    }

    [TestMethod]
    public void Validate_SetPrinterAttributes_WithDestinationOAuthTokenDependenciesSatisfied_DoesNotThrow()
    {
        var printerAttributes = new List<IppAttribute>
        {
            new(Tag.BegCollection, PrinterAttribute.DestinationUriReady, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "destination-oauth-token"),
            new(Tag.OctetStringWithAnUnspecifiedFormat, string.Empty, "token-part-1"),
            new(Tag.MemberAttrName, string.Empty, "destination-oauth-scope"),
            new(Tag.OctetStringWithAnUnspecifiedFormat, string.Empty, "scope1 scope2"),
            new(Tag.MemberAttrName, string.Empty, "destination-oauth-uri"),
            new(Tag.Uri, string.Empty, "https://issuer.example/token"),
            new(Tag.EndCollection, string.Empty, NoValue.Instance)
        };

        var request = CreateMinimalSetPrinterAttributesRequest(printerAttributes);
        var validator = IppRequestMessageValidator.Default;

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_SetPrinterAttributes_WithForbiddenDestinationAttributesSupportedMember_Throws()
    {
        var printerAttributes = new List<IppAttribute>
        {
            new(Tag.BegCollection, PrinterAttribute.DestinationUriReady, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "destination-attributes-supported"),
            new(Tag.Keyword, string.Empty, JobAttribute.JobPassword),
            new(Tag.EndCollection, string.Empty, NoValue.Instance)
        };

        var request = CreateMinimalSetPrinterAttributesRequest(printerAttributes);
        var validator = IppRequestMessageValidator.Default;

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("*destination-attributes-supported MUST NOT include password attributes*");
    }

    [TestMethod]
    public void Validate_SetPrinterAttributes_WithForbiddenDestinationAttributesMember_Throws()
    {
        var printerAttributes = new List<IppAttribute>
        {
            new(Tag.BegCollection, PrinterAttribute.DestinationUriReady, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "destination-attributes"),
            new(Tag.BegCollection, string.Empty, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, JobAttribute.DocumentPassword),
            new(Tag.OctetStringWithAnUnspecifiedFormat, string.Empty, "secret"),
            new(Tag.EndCollection, string.Empty, NoValue.Instance),
            new(Tag.EndCollection, string.Empty, NoValue.Instance)
        };

        var request = CreateMinimalSetPrinterAttributesRequest(printerAttributes);
        var validator = IppRequestMessageValidator.Default;

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("*destination-attributes MUST NOT include password attributes*");
    }

    [TestMethod]
    public void Validate_WhenNestedOverrideCollectionMemberIsSupported_ShouldIgnoreNestedAttributes()
    {
        var validator = new IppRequestMessageValidator();
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
    [DataRow(false, false)]
    [DataRow(true, true)]
    public void Validate_WhenFidelityAwareModeEnabledAndIppAttributeFidelity_OverrideMembers_Validation(bool fidelityValue, bool shouldThrow)
    {
        var validator = new IppRequestMessageValidator
        {
            UseIppAttributeFidelityForCapabilityValidation = true
        };
        validator.Context.OverridesSupported =
        [
            OverrideSupported.Pages,
            OverrideSupported.Sides
        ];

        var request = CreateBasicRequest(IppOperation.ValidateJob);
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, fidelityValue));
        request.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.Keyword, JobAttribute.Media, "iso_a4_210x297mm")
        }.ToBegCollection(JobAttribute.Overrides));

        Action act = () => validator.Validate(request);

        if (shouldThrow)
        {
            act.Should().Throw<IppRequestException>()
                .WithMessage("invalid overrides: member(s) not supported by target printer: media")
                .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
        }
        else
        {
            act.Should().NotThrow();
        }
    }

    [TestMethod]
    [DataRow("job-actual", true)]
    [DataRow("job-template", false)]
    public void Validate_JobRequestedAttributesGroupKeyword_Validation(string requestedKeyword, bool shouldThrow)
    {
        var validator = new IppRequestMessageValidator();
        validator.Context.JobRequestedAttributeGroupKeywordsSupported =
        [
            "all",
            "job-description",
            "job-template"
        ];

        var request = CreateBasicRequest(IppOperation.GetJobAttributes);
        request.OperationAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.RequestedAttributes, requestedKeyword));

        Action act = () => validator.Validate(request);

        if (shouldThrow)
        {
            act.Should().Throw<IppRequestException>()
                .WithMessage($"requested-attributes group value(s) not supported: {requestedKeyword}")
                .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
        }
        else
        {
            act.Should().NotThrow();
        }
    }

    #endregion

    #region Specific and New Rules Validation (Consolidated with DataRow)

    [TestMethod]
    [DataRow("iso-8859-1", true)]
    [DataRow("UTF-8", false)]
    [DataRow("utf-8", false)]
    public void Validate_AttributesCharset_Validation(string charset, bool shouldThrow)
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.OperationAttributes[0] = new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, charset);
        request.Document = new MemoryStream();

        Action act = () => validator.Validate(request);

        if (shouldThrow)
        {
            act.Should().Throw<IppRequestException>()
                .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
        }
        else
        {
            act.Should().NotThrow();
        }
    }

    [TestMethod]
    [DataRow(null, true)]
    [DataRow("other", true)]
    [DataRow("ippget", false)]
    public void Validate_GetNotifications_NotifyPullMethod_Validation(string? notifyPullMethod, bool shouldThrow)
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicSystemRequest(IppOperation.GetNotifications);
        if (notifyPullMethod != null)
        {
            request.OperationAttributes.Add(new IppAttribute(Tag.Keyword, SystemAttribute.NotifyPullMethod, notifyPullMethod));
        }

        Action act = () => validator.Validate(request);

        if (shouldThrow)
        {
            act.Should().Throw<IppRequestException>()
                .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
        }
        else
        {
            act.Should().NotThrow();
        }
    }

    [TestMethod]
    [DataRow(IppOperation.CancelSubscription, false, true)]
    [DataRow(IppOperation.GetSubscriptionAttributes, false, true)]
    [DataRow(IppOperation.RenewSubscription, false, true)]
    [DataRow(IppOperation.CancelSubscription, true, false)]
    [DataRow(IppOperation.GetSubscriptionAttributes, true, false)]
    [DataRow(IppOperation.RenewSubscription, true, false)]
    public void Validate_SubscriptionOperations_NotifySubscriptionId_Validation(IppOperation operation, bool withId, bool shouldThrow)
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicSystemRequest(operation);
        if (withId)
        {
            request.OperationAttributes.Add(new IppAttribute(Tag.Integer, SystemAttribute.NotifySubscriptionId, 1));
        }

        Action act = () => validator.Validate(request);

        if (shouldThrow)
        {
            act.Should().Throw<IppRequestException>()
                .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
        }
        else
        {
            act.Should().NotThrow();
        }
    }

    [TestMethod]
    [DataRow(true, false, true)]
    [DataRow(false, true, true)]
    [DataRow(true, true, false)]
    [DataRow(false, false, false)]
    public void Validate_PrintJob_JobPasswordAndEncryption_Validation(bool hasPassword, bool hasEncryption, bool shouldThrow)
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        if (hasPassword)
        {
            request.JobAttributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, JobAttribute.JobPassword, "secret"));
        }
        if (hasEncryption)
        {
            request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.JobPasswordEncryption, "none"));
        }

        Action act = () => validator.Validate(request);

        if (shouldThrow)
        {
            act.Should().Throw<IppRequestException>()
                .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
        }
        else
        {
            act.Should().NotThrow();
        }
    }

    [TestMethod]
    [DataRow(true, true, true)]
    [DataRow(true, false, false)]
    [DataRow(false, true, false)]
    [DataRow(false, false, false)]
    public void Validate_CreateJob_MediaAndMediaCol_Validation(bool hasMedia, bool hasMediaCol, bool shouldThrow)
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.CreateJob);
        if (hasMedia)
        {
            request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.Media, "iso_a4"));
        }
        if (hasMediaCol)
        {
            request.JobAttributes.Add(new IppAttribute(Tag.BegCollection, JobAttribute.MediaCol, NoValue.Instance));
            request.JobAttributes.Add(new IppAttribute(Tag.EndCollection, string.Empty, NoValue.Instance));
        }

        Action act = () => validator.Validate(request);

        if (shouldThrow)
        {
            act.Should().Throw<IppRequestException>()
                .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
        }
        else
        {
            act.Should().NotThrow();
        }
    }

    [TestMethod]
    [DataRow(1, 5, 3, 8, true)]
    [DataRow(5, 10, 1, 4, true)]
    [DataRow(1, 5, 7, 10, false)]
    public void Validate_PageRanges_Validation(int start1, int end1, int start2, int end2, bool shouldThrow)
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.RangeOfInteger, JobAttribute.PageRanges, new SharpIpp.Protocol.Models.Range(start1, end1)));
        request.JobAttributes.Add(new IppAttribute(Tag.RangeOfInteger, JobAttribute.PageRanges, new SharpIpp.Protocol.Models.Range(start2, end2)));

        Action act = () => validator.Validate(request);

        if (shouldThrow)
        {
            act.Should().Throw<IppRequestException>()
                .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
        }
        else
        {
            act.Should().NotThrow();
        }
    }

    [TestMethod]
    [DataRow(0, true)]
    [DataRow(-1, true)]
    [DataRow(1, false)]
    [DataRow(null, false)]
    public void Validate_Copies_Validation(int? copiesValue, bool shouldThrow)
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        if (copiesValue.HasValue)
        {
            request.JobAttributes.Add(new IppAttribute(Tag.Integer, JobAttribute.Copies, copiesValue.Value));
        }

        Action act = () => validator.Validate(request);

        if (shouldThrow)
        {
            act.Should().Throw<IppRequestException>()
                .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
        }
        else
        {
            act.Should().NotThrow();
        }
    }

    [TestMethod]
    [DataRow(JobAttribute.Copies)]
    [DataRow(JobAttribute.JobPriority)]
    [DataRow(JobAttribute.NumberUp)]
    public void Validate_IntegerJobAttribute_NonIntegerValue_ShouldNotThrow(string attributeName)
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.NoValue, attributeName, NoValue.Instance));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    [DataRow(0, true)]
    [DataRow(101, true)]
    [DataRow(50, false)]
    public void Validate_JobPriority_Validation(int jobPriority, bool shouldThrow)
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.Integer, JobAttribute.JobPriority, jobPriority));

        Action act = () => validator.Validate(request);

        if (shouldThrow)
        {
            act.Should().Throw<IppRequestException>()
                .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
        }
        else
        {
            act.Should().NotThrow();
        }
    }

    [TestMethod]
    [DataRow(0, true)]
    [DataRow(2, false)]
    public void Validate_NumberUp_Validation(int numberUp, bool shouldThrow)
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.Integer, JobAttribute.NumberUp, numberUp));

        Action act = () => validator.Validate(request);

        if (shouldThrow)
        {
            act.Should().Throw<IppRequestException>()
                .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
        }
        else
        {
            act.Should().NotThrow();
        }
    }

    [TestMethod]
    [DataRow(IppOperation.CancelResource, false, true)]
    [DataRow(IppOperation.GetResourceAttributes, false, true)]
    [DataRow(IppOperation.InstallResource, false, true)]
    [DataRow(IppOperation.SendResourceData, false, true)]
    [DataRow(IppOperation.SetResourceAttributes, false, true)]
    [DataRow(IppOperation.CancelResource, true, false)]
    [DataRow(IppOperation.GetResourceAttributes, true, false)]
    [DataRow(IppOperation.InstallResource, true, false)]
    [DataRow(IppOperation.SendResourceData, true, false)]
    [DataRow(IppOperation.SetResourceAttributes, true, false)]
    public void Validate_ResourceOperations_ResourceId_Validation(IppOperation operation, bool withId, bool shouldThrow)
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicSystemRequest(operation);
        if (withId)
        {
            request.OperationAttributes.Add(new IppAttribute(Tag.Integer, SystemAttribute.ResourceId, 1));
        }

        Action act = () => validator.Validate(request);

        if (shouldThrow)
        {
            act.Should().Throw<IppRequestException>()
                .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
        }
        else
        {
            act.Should().NotThrow();
        }
    }

    [TestMethod]
    [DataRow(IppOperation.AllocatePrinterResources, false, true)]
    [DataRow(IppOperation.DeallocatePrinterResources, false, true)]
    [DataRow(IppOperation.DeletePrinter, false, true)]
    [DataRow(IppOperation.GetPrinterResources, false, true)]
    [DataRow(IppOperation.ShutdownOnePrinter, false, true)]
    [DataRow(IppOperation.StartupOnePrinter, false, true)]
    [DataRow(IppOperation.RestartOnePrinter, false, true)]
    [DataRow(IppOperation.AllocatePrinterResources, true, false)]
    [DataRow(IppOperation.DeallocatePrinterResources, true, false)]
    [DataRow(IppOperation.DeletePrinter, true, false)]
    [DataRow(IppOperation.GetPrinterResources, true, false)]
    [DataRow(IppOperation.ShutdownOnePrinter, true, false)]
    [DataRow(IppOperation.StartupOnePrinter, true, false)]
    [DataRow(IppOperation.RestartOnePrinter, true, false)]
    public void Validate_PrinterOperations_PrinterId_Validation(IppOperation operation, bool withId, bool shouldThrow)
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicSystemRequest(operation);
        if (withId)
        {
            request.OperationAttributes.Add(new IppAttribute(Tag.Integer, JobAttribute.PrinterId, 1));
        }

        Action act = () => validator.Validate(request);

        if (shouldThrow)
        {
            act.Should().Throw<IppRequestException>()
                .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
        }
        else
        {
            act.Should().NotThrow();
        }
    }

    [TestMethod]
    [DataRow(true, "na_letter", true)]
    [DataRow(true, "iso_a4", false)]
    [DataRow(false, "na_letter", false)]
    public void Validate_Fidelity_Media_Validation(bool fidelity, string media, bool shouldThrow)
    {
        var validator = new IppRequestMessageValidator();
        validator.Context.MediaSupported = [new Media("iso_a4", true)];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        if (fidelity)
        {
            request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        }
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.Media, media));

        Action act = () => validator.Validate(request);

        if (shouldThrow)
        {
            act.Should().Throw<IppRequestException>()
                .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
        }
        else
        {
            act.Should().NotThrow();
        }
    }

    [TestMethod]
    [DataRow(true, Finishings.Staple, true)]
    [DataRow(true, Finishings.None, false)]
    [DataRow(false, Finishings.Staple, false)]
    public void Validate_FidelityFinishings_Validation(bool fidelity, Finishings requested, bool shouldThrow)
    {
        var validator = new IppRequestMessageValidator();
        validator.Context.FinishingsSupported = [Finishings.None];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        if (fidelity)
        {
            request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        }
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.Finishings, (int)requested));

        Action act = () => validator.Validate(request);

        if (shouldThrow)
        {
            act.Should().Throw<IppRequestException>()
                .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
        }
        else
        {
            act.Should().NotThrow();
        }
    }

    [TestMethod]
    public void Validate_FidelityFinishings_NullSupportedList_ShouldNotThrow()
    {
        var validator = new IppRequestMessageValidator();

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.Finishings, (int)Finishings.Staple));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_FidelityFinishings_MultipleValuesSecondUnsupported_ShouldThrowClientErrorAttributesOrValuesNotSupported()
    {
        var validator = new IppRequestMessageValidator();
        validator.Context.FinishingsSupported = [Finishings.None];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.Finishings, (int)Finishings.None));
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.Finishings, (int)Finishings.Staple));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    [TestMethod]
    [DataRow(true, "two-sided-long-edge", true)]
    [DataRow(true, "one-sided", false)]
    [DataRow(false, "two-sided-long-edge", false)]
    public void Validate_FidelitySides_Validation(bool fidelity, string requested, bool shouldThrow)
    {
        var validator = new IppRequestMessageValidator();
        validator.Context.SidesSupported = [Sides.OneSided];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        if (fidelity)
        {
            request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        }
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.Sides, requested));

        Action act = () => validator.Validate(request);

        if (shouldThrow)
        {
            act.Should().Throw<IppRequestException>()
                .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
        }
        else
        {
            act.Should().NotThrow();
        }
    }

    [TestMethod]
    public void Validate_FidelitySides_NullSupportedList_ShouldNotThrow()
    {
        var validator = new IppRequestMessageValidator();

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.Sides, Sides.TwoSidedLongEdge.Value));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    [DataRow(true, PrintQuality.Draft, true)]
    [DataRow(true, PrintQuality.Normal, false)]
    [DataRow(false, PrintQuality.Draft, false)]
    public void Validate_FidelityPrintQuality_Validation(bool fidelity, PrintQuality requested, bool shouldThrow)
    {
        var validator = new IppRequestMessageValidator();
        validator.Context.PrintQualitySupported = [PrintQuality.Normal];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        if (fidelity)
        {
            request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        }
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.PrintQuality, (int)requested));

        Action act = () => validator.Validate(request);

        if (shouldThrow)
        {
            act.Should().Throw<IppRequestException>()
                .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
        }
        else
        {
            act.Should().NotThrow();
        }
    }

    [TestMethod]
    public void Validate_FidelityPrintQuality_NullSupportedList_ShouldNotThrow()
    {
        var validator = new IppRequestMessageValidator();

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.PrintQuality, (int)PrintQuality.Draft));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    [DataRow(true, Orientation.Landscape, true)]
    [DataRow(true, Orientation.Portrait, false)]
    [DataRow(false, Orientation.Landscape, false)]
    public void Validate_FidelityOrientationRequested_Validation(bool fidelity, Orientation requested, bool shouldThrow)
    {
        var validator = new IppRequestMessageValidator();
        validator.Context.OrientationRequestedSupported = [Orientation.Portrait];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        if (fidelity)
        {
            request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        }
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.OrientationRequested, (int)requested));

        Action act = () => validator.Validate(request);

        if (shouldThrow)
        {
            act.Should().Throw<IppRequestException>()
                .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
        }
        else
        {
            act.Should().NotThrow();
        }
    }

    [TestMethod]
    public void Validate_FidelityOrientationRequested_NullSupportedList_ShouldNotThrow()
    {
        var validator = new IppRequestMessageValidator();

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.OrientationRequested, (int)Orientation.Landscape));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    [DataRow(true, "monochrome", true)]
    [DataRow(true, "color", false)]
    [DataRow(false, "monochrome", false)]
    public void Validate_FidelityPrintColorMode_Validation(bool fidelity, string requested, bool shouldThrow)
    {
        var validator = new IppRequestMessageValidator();
        validator.Context.PrintColorModeSupported = [PrintColorMode.Color];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        if (fidelity)
        {
            request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        }
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.PrintColorMode, requested));

        Action act = () => validator.Validate(request);

        if (shouldThrow)
        {
            act.Should().Throw<IppRequestException>()
                .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
        }
        else
        {
            act.Should().NotThrow();
        }
    }

    [TestMethod]
    [DataRow("null-list")]
    [DataRow("empty-list")]
    public void Validate_FidelityPrintColorMode_NoEffectiveSupportedList_ShouldNotThrow(string scenario)
    {
        var validator = new IppRequestMessageValidator();
        if (scenario == "empty-list")
            validator.Context.PrintColorModeSupported = [];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.PrintColorMode, PrintColorMode.Monochrome.Value));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_FidelityPrintColorMode_NoValue_ShouldNotThrow()
    {
        var validator = new IppRequestMessageValidator();
        validator.Context.PrintColorModeSupported = [PrintColorMode.Monochrome];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.NoValue, JobAttribute.PrintColorMode, NoValue.Instance));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    [DataRow(IppOperation.CreatePrinterSubscriptions, false, true)]
    [DataRow(IppOperation.CreateSystemSubscriptions, false, true)]
    [DataRow(IppOperation.CreatePrinterSubscriptions, true, false)]
    public void Validate_CreateSubscriptions_SubscriptionAttributes_Validation(IppOperation operation, bool withSubscriptionAttributes, bool shouldThrow)
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicSystemRequest(operation);
        if (withSubscriptionAttributes)
        {
            request.SubscriptionAttributes.Add(new IppAttribute(Tag.Keyword, "notify-events", "job-completed"));
        }

        Action act = () => validator.Validate(request);

        if (shouldThrow)
        {
            act.Should().Throw<IppRequestException>()
                .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
        }
        else
        {
            act.Should().NotThrow();
        }
    }

    [TestMethod]
    public void ValidateFidelityBasedJobAttributes_PrintQuality_NonIntValue_ShouldNotThrow()
    {
        var validator = new IppRequestMessageValidator();
        validator.Context.PrintQualitySupported = [PrintQuality.Normal];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.PrintQuality, "draft"));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateFidelityBasedJobAttributes_OrientationRequested_NonIntValue_ShouldNotThrow()
    {
        var validator = new IppRequestMessageValidator();
        validator.Context.OrientationRequestedSupported = [Orientation.Portrait];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.OrientationRequested, "landscape"));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    [DataRow(IppOperation.CancelSubscription)]
    [DataRow(IppOperation.GetSubscriptionAttributes)]
    [DataRow(IppOperation.RenewSubscription)]
    [DataRow(IppOperation.CancelResource)]
    [DataRow(IppOperation.GetResourceAttributes)]
    [DataRow(IppOperation.AllocatePrinterResources)]
    [DataRow(IppOperation.DeletePrinter)]
    public void ValidateOperationRules_GroupDisabled_AbsentIds_ShouldNotThrow(IppOperation operation)
    {
        var validator = new IppRequestMessageValidator { ValidateOperationAttributesGroup = false };
        var request = CreateBasicSystemRequest(operation);

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    [DataRow(true, false, true, false)]
    [DataRow(false, true, true, false)]
    [DataRow(true, true, false, false)]
    [DataRow(true, true, true, true)]
    public void Validate_UpdateActiveJobs_Validation(bool hasJobIds, bool hasOutputDeviceJobStates, bool shouldThrow, bool mismatchedCardinality)
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.UpdateActiveJobs);
        request.OperationAttributes.Add(new IppAttribute(Tag.Uri, JobAttribute.OutputDeviceUuid, "ipp://127.0.0.1/output-device"));
        if (hasJobIds)
        {
            request.OperationAttributes.Add(new IppAttribute(Tag.Integer, JobAttribute.JobIds, 1));
            if (mismatchedCardinality)
            {
                request.OperationAttributes.Add(new IppAttribute(Tag.Integer, JobAttribute.JobIds, 2));
            }
        }
        if (hasOutputDeviceJobStates)
        {
            request.OperationAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.OutputDeviceJobStates, (int)JobState.Processing));
        }

        Action act = () => validator.Validate(request);

        if (shouldThrow)
        {
            act.Should().Throw<IppRequestException>()
                .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
        }
        else
        {
            act.Should().NotThrow();
        }
    }

    #endregion

    #region Detailed Validator Tests (Consolidated)

    [TestMethod]
    public void ValidatePrinterAttributes_OutOfBandReadyCollection_ShouldSkip()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.GetPrinterAttributes);
        request.PrinterAttributes.Add(new IppAttribute(Tag.NoValue, PrinterAttribute.DestinationUriReady, NoValue.Instance));

        validator.ValidatePrinterAttributes(request);
    }

    [TestMethod]
    [DataRow(true, false)]
    [DataRow(false, true)]
    public void ValidatePrinterAttributes_SupportedForbiddenMembers_Validation(bool isNullValue, bool shouldThrow)
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.GetPrinterAttributes);
        IppAttribute attr;
        if (isNullValue)
        {
            attr = IppAttribute.Create(Tag.Keyword, "destination-attributes-supported", new NullToStringObject());
        }
        else
        {
            attr = new IppAttribute(Tag.Keyword, "destination-attributes-supported", JobAttribute.DocumentPassword);
        }
        request.PrinterAttributes.AddRange(new[] { attr }.ToBegCollection(PrinterAttribute.DestinationUriReady));

        Action act = () => validator.ValidatePrinterAttributes(request);

        if (shouldThrow)
        {
            act.Should().Throw<IppRequestException>()
                .WithMessage("invalid destination-uri-ready: destination-attributes-supported MUST NOT include password attributes");
        }
        else
        {
            act.Should().NotThrow();
        }
    }

    [TestMethod]
    public void ValidatePrinterAttributes_DestinationAttributesOutOfBand_ShouldSkip()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.GetPrinterAttributes);
        request.PrinterAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.NoValue, "destination-attributes", NoValue.Instance),
            new IppAttribute(Tag.Keyword, "destination-attributes-supported", "copies")
        }.ToBegCollection(PrinterAttribute.DestinationUriReady));

        validator.ValidatePrinterAttributes(request);
    }

    [TestMethod]
    public void ValidatePrinterAttributes_DestinationAttributesWithForbiddenMember_ShouldThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.GetPrinterAttributes);
        request.PrinterAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.BegCollection, "destination-attributes", NoValue.Instance),
            new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, JobAttribute.DocumentPassword, "secret"),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
        }.ToBegCollection(PrinterAttribute.DestinationUriReady));

        Action act = () => validator.ValidatePrinterAttributes(request);
        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid destination-uri-ready: destination-attributes MUST NOT include password attributes");
    }

    [TestMethod]
    public void ValidatePrinterAttributes_DestinationAttributesWithWhitespaceName_ShouldPass()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.GetPrinterAttributes);
        var attrWithWhitespaceName = new IppAttribute(Tag.Keyword, " ", "value");
        request.PrinterAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.BegCollection, "destination-attributes", NoValue.Instance),
            attrWithWhitespaceName,
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
        }.ToBegCollection(PrinterAttribute.DestinationUriReady));

        validator.ValidatePrinterAttributes(request);
    }

    [TestMethod]
    public void ValidateCollectionMediaSelectionRules_OutOfBandCollection_ShouldSkip()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.CreateJob);
        validator.ValidateCollectionMediaSelectionRules(new[] { new IppAttribute(Tag.NoValue, JobAttribute.CoverBack, NoValue.Instance) }, request);
    }

    [TestMethod]
    public void ValidateCollectionMediaSelectionRules_InvalidEncoding_ShouldThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.CreateJob);
        var attrs = new[]
        {
            new IppAttribute(Tag.BegCollection, JobAttribute.CoverBack, NoValue.Instance),
            new IppAttribute(Tag.Keyword, string.Empty, "value-without-member-name"),
            new IppAttribute(Tag.EndCollection, string.Empty, NoValue.Instance),
        };

        Action act = () => validator.ValidateCollectionMediaSelectionRules(attrs, request);
        act.Should().Throw<IppRequestException>().WithMessage("invalid cover-back collection encoding");
    }

    [TestMethod]
    public void ValidateOutputBinAgainstSupportedValues_NullValue_ShouldReturn()
    {
        var validator = new IppRequestMessageValidator();
        validator.Context.OutputBinSupported = new[] { new OutputBin("top", true) };
        var request = CreateBasicRequest(IppOperation.CreateJob);
        var attr = IppAttribute.Create(Tag.Keyword, JobAttribute.OutputBin, new NullToStringObject());
        validator.ValidateOutputBinAgainstSupportedValues(attr, request);
    }

    [TestMethod]
    public void ValidateOutputBinAgainstSupportedValues_WhitespaceValue_ShouldReturn()
    {
        var validator = new IppRequestMessageValidator();
        validator.Context.OutputBinSupported = new[] { new OutputBin("top", true) };
        var request = CreateBasicRequest(IppOperation.CreateJob);
        validator.ValidateOutputBinAgainstSupportedValues(new IppAttribute(Tag.Keyword, JobAttribute.OutputBin, "   "), request);
    }

    [TestMethod]
    public void ValidateOperationRules_SetDocumentAttributes_WithValidState_ShouldPass()
    {
        var validator = new IppRequestMessageValidator();
        validator.Context.DocumentStatesByNumber = new Dictionary<int, DocumentState> { { 1, DocumentState.Pending } };
        var request = CreateBasicRequest(IppOperation.SetDocumentAttributes);
        request.OperationAttributes.Add(new IppAttribute(Tag.Integer, DocumentAttribute.DocumentNumber, 1));
        request.DocumentAttributes.Add(new IppAttribute(Tag.NameWithoutLanguage, "document-name", "doc"));
        validator.ValidateOperationRules(request);
    }

    [TestMethod]
    [DataRow(IppOperation.ValidateJob)]
    [DataRow(IppOperation.ValidateDocument)]
    public void Validate_WhenValidateOperationContainsDocumentPassword_ShouldThrowBadRequest(IppOperation operation)
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(operation);
        request.OperationAttributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, JobAttribute.DocumentPassword, "secret"));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("document-password is not allowed for validate operations")
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void ValidateCreateJobDestinationRules_GroupDisabled_ShouldReturn()
    {
        var validator = new IppRequestMessageValidator { ValidateOperationAttributesGroup = false };
        var request = CreateBasicRequest(IppOperation.CreateJob);
        validator.ValidateCreateJobDestinationRules(request.OperationAttributes, request);
    }

    [TestMethod]
    public void ValidateCreateJobDestinationRules_OutOfBandUriCollection_ShouldSkip()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.CreateJob);
        request.OperationAttributes.Add(new IppAttribute(Tag.NoValue, JobAttribute.DestinationUris, NoValue.Instance));
        validator.ValidateCreateJobDestinationRules(request.OperationAttributes, request);
    }

    [TestMethod]
    public void ValidateCreateJobDestinationRules_NullUriValue_ShouldSkip()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.CreateJob);
        var attr = IppAttribute.Create(Tag.Uri, "destination-uri", new NullToStringObject());
        request.OperationAttributes.AddRange(new[] { attr }.ToBegCollection(JobAttribute.DestinationUris));
        validator.ValidateCreateJobDestinationRules(request.OperationAttributes, request);
    }

    [TestMethod]
    [DataRow("mailto:test@example.com", false, null)]
    [DataRow("fax:+123456", true, "invalid destination-uri scheme for Scan")]
    [DataRow("FAX:+123456", true, "invalid destination-uri scheme for Scan")]
    [DataRow("123:abc", false, null)]
    [DataRow("noscheme", false, null)]
    [DataRow("", false, null)]
    public void ValidateCreateJobDestinationRules_UriValues_Validation(string uriValue, bool shouldThrow, string? expectedMessage)
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.CreateJob);
        request.OperationAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.Uri, "destination-uri", uriValue)
        }.ToBegCollection(JobAttribute.DestinationUris));

        Action act = () => validator.ValidateCreateJobDestinationRules(request.OperationAttributes, request);

        if (shouldThrow)
        {
            act.Should().Throw<IppRequestException>().WithMessage(expectedMessage);
        }
        else
        {
            act.Should().NotThrow();
        }
    }

    [TestMethod]
    public void ValidateCreateJobDestinationRules_MismatchedAccesses_ShouldThrow()
    {
        var validator = new IppRequestMessageValidator();
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

        Action act = () => validator.ValidateCreateJobDestinationRules(request.OperationAttributes, request);
        act.Should().Throw<IppRequestException>().WithMessage("destination-accesses cardinality MUST match destination-uris");
    }

    [TestMethod]
    public void ValidateJobRequestedAttributesGroupKeywords_NullValue_ShouldSkip()
    {
        var validator = new IppRequestMessageValidator();
        validator.Context.JobRequestedAttributeGroupKeywordsSupported = new[] { "all" };
        var request = CreateBasicRequest(IppOperation.GetJobAttributes);
        request.OperationAttributes.Add(IppAttribute.Create(Tag.Keyword, JobAttribute.RequestedAttributes, new NullToStringObject()));
        validator.ValidateJobRequestedAttributesGroupKeywords(request);
    }

    [TestMethod]
    public void ValidateJobRequestedAttributesGroupKeywords_FidelityTrue_ShouldReturn()
    {
        var validator = new IppRequestMessageValidator { UseIppAttributeFidelityForCapabilityValidation = true };
        validator.Context.JobRequestedAttributeGroupKeywordsSupported = new[] { "all" };
        var request = CreateBasicRequest(IppOperation.GetJobAttributes);
        request.OperationAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.RequestedAttributes, "job-description"));
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));

        Action act = () => validator.ValidateJobRequestedAttributesGroupKeywords(request);
        act.Should().Throw<IppRequestException>();

        request.OperationAttributes.RemoveAll(x => x.Name == JobAttribute.IppAttributeFidelity);
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, false));
        validator.ValidateJobRequestedAttributesGroupKeywords(request);
    }

    [TestMethod]
    public void ValidateRequiredLastDocument_GroupDisabled_ShouldReturn()
    {
        var validator = new IppRequestMessageValidator { ValidateOperationAttributesGroup = false };
        validator.ValidateRequiredLastDocument(false, null, CreateBasicRequest(IppOperation.SendDocument));
    }

    [TestMethod]
    public void ValidateRequiredOutputDeviceUuid_GroupDisabled_ShouldReturn()
    {
        var validator = new IppRequestMessageValidator { ValidateOperationAttributesGroup = false };
        validator.ValidateRequiredOutputDeviceUuid(false, CreateBasicRequest(IppOperation.UpdateJobStatus));
    }

    [TestMethod]
    [DataRow(typeof(IppStatusCode), IppStatusCode.SuccessfulOk)]
    [DataRow(typeof(int), (int)IppStatusCode.SuccessfulOk)]
    [DataRow(typeof(short), (short)IppStatusCode.SuccessfulOk)]
    public void ValidateFetchStatusCodeNotSuccessful_SuccessfulValues_ShouldThrow(Type valueType, object rawValue)
    {
        var request = CreateBasicRequest(IppOperation.AcknowledgeJob);
        IppAttribute attr;
        if (valueType == typeof(int))
        {
            attr = new IppAttribute(Tag.Enum, JobAttribute.FetchStatusCode, (int)rawValue);
        }
        else
        {
            attr = IppAttribute.Create(Tag.Enum, JobAttribute.FetchStatusCode, rawValue);
        }

        Action act = () => IppRequestMessageValidator.ValidateFetchStatusCodeNotSuccessful(new[] { attr }, request);
        act.Should().Throw<IppRequestException>().WithMessage("fetch-status-code MUST NOT be successful-ok");
    }

    [TestMethod]
    public void ValidateDocumentRequestedAttributesGroupKeywords_NullValue_ShouldSkip()
    {
        var validator = new IppRequestMessageValidator();
        validator.Context.DocumentRequestedAttributeGroupKeywordsSupported = new[] { "all" };
        var request = CreateBasicRequest(IppOperation.GetDocumentAttributes);
        request.OperationAttributes.Add(IppAttribute.Create(Tag.Keyword, JobAttribute.RequestedAttributes, new NullToStringObject()));
        validator.ValidateDocumentRequestedAttributesGroupKeywords(request);
    }

    [TestMethod]
    public void ValidateDocumentRequestedAttributesGroupKeywords_FidelityFalse_ShouldReturn()
    {
        var validator = new IppRequestMessageValidator { UseIppAttributeFidelityForCapabilityValidation = true };
        validator.Context.DocumentRequestedAttributeGroupKeywordsSupported = new[] { "all" };
        var request = CreateBasicRequest(IppOperation.GetDocumentAttributes);
        request.OperationAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.RequestedAttributes, DocumentAttribute.DocumentDescription));
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, false));
        validator.ValidateDocumentRequestedAttributesGroupKeywords(request);
    }

    [TestMethod]
    public void ValidateNotifyEventsValues_NullValue_ShouldSkip()
    {
        var validator = new IppRequestMessageValidator();
        validator.Context.NotifyEventsSupported = new[] { "job-completed" };
        var request = CreateBasicRequest(IppOperation.CreateJob);
        request.SubscriptionAttributes.Add(IppAttribute.Create(Tag.Keyword, "notify-events", new NullToStringObject()));
        validator.ValidateNotifyEventsValues(request);
    }

    [TestMethod]
    public void ValidateNotifyEventsValues_FidelityFalse_ShouldReturn()
    {
        var validator = new IppRequestMessageValidator { UseIppAttributeFidelityForCapabilityValidation = true };
        validator.Context.NotifyEventsSupported = new[] { "job-completed" };
        var request = CreateBasicRequest(IppOperation.CreateJob);
        request.SubscriptionAttributes.Add(new IppAttribute(Tag.Keyword, "notify-events", "job-created"));
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, false));
        validator.ValidateNotifyEventsValues(request);
    }

    [TestMethod]
    [DataRow(true, false, false, false)]
    [DataRow(false, true, false, false)]
    [DataRow(false, false, true, false)]
    [DataRow(false, false, false, true)]
    public void ValidateCancelDocumentStateRules_SkippingScenarios_ShouldReturn(bool stateNotPresent, bool stateNotProcessing, bool noStateReasonsDict, bool noStateReasonsForNumber)
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.CancelDocument);

        if (stateNotPresent)
        {
            validator.Context.DocumentStatesByNumber = new Dictionary<int, DocumentState> { { 2, DocumentState.Pending } };
        }
        else if (stateNotProcessing)
        {
            validator.Context.DocumentStatesByNumber = new Dictionary<int, DocumentState> { { 1, DocumentState.Pending } };
        }
        else if (noStateReasonsDict)
        {
            validator.Context.DocumentStatesByNumber = new Dictionary<int, DocumentState> { { 1, DocumentState.Processing } };
            validator.Context.DocumentStateReasonsByNumber = null;
        }
        else if (noStateReasonsForNumber)
        {
            validator.Context.DocumentStatesByNumber = new Dictionary<int, DocumentState> { { 1, DocumentState.Processing } };
            validator.Context.DocumentStateReasonsByNumber = new Dictionary<int, IReadOnlyCollection<DocumentStateReason>>();
        }

        validator.ValidateCancelDocumentStateRules(1, request);
    }

    [TestMethod]
    [DataRow(2, DocumentState.Pending, true, false, null)]
    [DataRow(1, DocumentState.Completed, true, true, "invalid document-state for Set-Document-Attributes")]
    [DataRow(1, DocumentState.Processing, false, true, "invalid document-state for Set-Document-Attributes")]
    public void ValidateSetDocumentAttributesStateRules_Validation(int number, DocumentState state, bool allowSet, bool shouldThrow, string? expectedMessage)
    {
        var validator = new IppRequestMessageValidator();
        validator.Context.DocumentStatesByNumber = new Dictionary<int, DocumentState> { { number, state } };
        validator.Context.AllowSetDocumentAttributesWhenProcessing = allowSet;
        var request = CreateBasicRequest(IppOperation.SetDocumentAttributes);

        Action act = () => validator.ValidateSetDocumentAttributesStateRules(1, request);

        if (shouldThrow)
        {
            act.Should().Throw<IppRequestException>().WithMessage(expectedMessage);
        }
        else
        {
            act.Should().NotThrow();
        }
    }

    [TestMethod]
    [DataRow("attr", OverrideMemberScope.Job, "attr", true)]
    [DataRow("attr", OverrideMemberScope.Document, "attr", true)]
    [DataRow("attr", OverrideMemberScope.Page, "attr", false)]
    [DataRow("other-attr", OverrideMemberScope.Page, "unknown-attr", false)]
    public void ValidateOverrideMembersAgainstSupportedValues_Scopes_Validation(string scopeAttrName, OverrideMemberScope scope, string memberAttrName, bool shouldThrow)
    {
        var validator = new IppRequestMessageValidator();
        validator.Context.OverrideMemberScopesByName = new Dictionary<string, OverrideMemberScope>
        {
            { scopeAttrName, scope }
        };

        var request = CreateBasicRequest(IppOperation.CreateJob);
        var members = new[]
        {
            new IppAttribute(Tag.Keyword, memberAttrName, "value")
        };

        Action act = () => validator.ValidateOverrideMembersAgainstSupportedValues(members, request);

        if (shouldThrow)
        {
            act.Should().Throw<IppRequestException>()
                .WithMessage($"invalid overrides: member(s) not supported by target printer: {memberAttrName}");
        }
        else
        {
            act.Should().NotThrow();
        }
    }

    [TestMethod]
    public void ValidatePrinterAttributes_DestinationAttributesWithValidMember_ShouldPass()
    {
        var validator = new IppRequestMessageValidator();
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
        var validator = new IppRequestMessageValidator();
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
        var validator = new IppRequestMessageValidator();
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
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.ValidateJob);
        validator.ValidateOperationRules(request);
    }

    [TestMethod]
    public void ValidateOperationRules_SetDocumentAttributes_GroupDisabled_ShouldPass()
    {
        var validator = new IppRequestMessageValidator { ValidateOperationAttributesGroup = false };
        var request = CreateBasicRequest(IppOperation.SetDocumentAttributes);
        request.DocumentAttributes.Add(new IppAttribute(Tag.NameWithoutLanguage, "document-name", "doc"));
        validator.ValidateOperationRules(request);
    }

    [TestMethod]
    public void ValidateOperationRules_ValidateJob_GroupDisabled_ShouldPass()
    {
        var validator = new IppRequestMessageValidator { ValidateOperationAttributesGroup = false };
        var request = CreateBasicRequest(IppOperation.ValidateJob);
        request.OperationAttributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, JobAttribute.DocumentPassword, "secret"));
        validator.ValidateOperationRules(request);
    }

    [TestMethod]
    public void ValidateOperationRules_ValidateDocument_NoDocumentPassword_ShouldPass()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.ValidateDocument);
        validator.ValidateOperationRules(request);
    }

    [TestMethod]
    public void ValidateCollectionMediaSelectionRules_EmptyCollection_ShouldSkip()
    {
        var validator = new IppRequestMessageValidator();
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
        var validator = new IppRequestMessageValidator();
        validator.Context.OutputBinSupported = new[] { new OutputBin("top", true) };
        var request = CreateBasicRequest(IppOperation.CreateJob);
        var attr = new IppAttribute(Tag.Keyword, JobAttribute.OutputBin, "top");
        validator.ValidateOutputBinAgainstSupportedValues(attr, request);
    }

    #endregion
}
