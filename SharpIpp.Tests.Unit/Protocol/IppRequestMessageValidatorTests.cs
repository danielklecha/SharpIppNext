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
            new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"),
            new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "ipp://127.0.0.1:631/")
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
            new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"),
            new IppAttribute(Tag.Uri, IppAttributeNames.SystemUri, "ipp://127.0.0.1:8631/system")
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

        request.OperationAttributes.Add(new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"));
        request.OperationAttributes.Add(new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"));
        request.OperationAttributes.Add(new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "ipp://printer"));
        request.PrinterAttributes.AddRange(printerAttributes);
        return request;
    }

    private sealed class NullToStringObject
    {
        public override string? ToString() => null;
    }

    private static IppAttribute CreateIppAttributeWithNullValue(Tag tag, string name)
    {
        var attr = new IppAttribute(tag, name, NoValue.Instance);
        var field = typeof(IppAttribute).GetField("<Value>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
        object boxed = attr;
        field!.SetValue(boxed, null);
        return (IppAttribute)boxed;
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
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, IppAttributeNames.Finishings, (int)Finishings.Staple));
        request.JobAttributes.Add(new IppAttribute(Tag.BegCollection, IppAttributeNames.FinishingsCol, NoValue.Instance));

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
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, IppAttributeNames.Finishings, (int)Finishings.Staple));
        request.JobAttributes.Add(new IppAttribute(Tag.BegCollection, IppAttributeNames.FinishingsCol, NoValue.Instance));

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
            new(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
            new(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"),
            new(Tag.Uri, IppAttributeNames.PrinterUri, "ipp://printer"),

            new(Tag.BegCollection, IppAttributeNames.DestinationUris, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "destination-uri"),
            new(Tag.Uri, string.Empty, "https://example.test/a"),
            new(Tag.EndCollection, string.Empty, NoValue.Instance),

            new(Tag.BegCollection, IppAttributeNames.DestinationUris, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "destination-uri"),
            new(Tag.Uri, string.Empty, "https://example.test/b"),
            new(Tag.EndCollection, string.Empty, NoValue.Instance),

            new(Tag.BegCollection, IppAttributeNames.DestinationAccesses, NoValue.Instance),
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
            new(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
            new(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"),
            new(Tag.Uri, IppAttributeNames.PrinterUri, "ipp://printer"),

            new(Tag.BegCollection, IppAttributeNames.DestinationUris, NoValue.Instance),
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
            new(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
            new(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"),
            new(Tag.Uri, IppAttributeNames.PrinterUri, "ipp://printer"),

            new(Tag.BegCollection, IppAttributeNames.DestinationUris, NoValue.Instance),
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
            new(Tag.BegCollection, IppAttributeNames.DestinationUriReady, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, attrName),
            new IppAttribute(tag, string.Empty, val),
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
            new(Tag.BegCollection, IppAttributeNames.DestinationUriReady, NoValue.Instance),
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
            new(Tag.BegCollection, IppAttributeNames.DestinationUriReady, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "destination-attributes-supported"),
            new(Tag.Keyword, string.Empty, IppAttributeNames.JobPassword),
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
            new(Tag.BegCollection, IppAttributeNames.DestinationUriReady, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "destination-attributes"),
            new(Tag.BegCollection, string.Empty, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, IppAttributeNames.DocumentPassword),
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
            new IppAttribute(Tag.BegCollection, IppAttributeNames.MediaCol, NoValue.Instance),
            new IppAttribute(Tag.BegCollection, "media-size", NoValue.Instance),
            new IppAttribute(Tag.Integer, "x-dimension", 21000),
            new IppAttribute(Tag.Integer, "y-dimension", 29700),
            new IppAttribute(Tag.EndCollection, string.Empty, NoValue.Instance),
            new IppAttribute(Tag.EndCollection, string.Empty, NoValue.Instance)
        }.ToBegCollection(IppAttributeNames.Overrides));

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
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.IppAttributeFidelity, fidelityValue));
        request.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.Keyword, IppAttributeNames.Media, "iso_a4_210x297mm")
        }.ToBegCollection(IppAttributeNames.Overrides));

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
        request.OperationAttributes.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.RequestedAttributes, requestedKeyword));

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
        request.OperationAttributes[0] = new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, charset);
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
            request.OperationAttributes.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.NotifyPullMethod, notifyPullMethod));
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
            request.OperationAttributes.Add(new IppAttribute(Tag.Integer, IppAttributeNames.NotifySubscriptionId, 1));
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
            request.JobAttributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.JobPassword, "secret"));
        }
        if (hasEncryption)
        {
            request.JobAttributes.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.JobPasswordEncryption, "none"));
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
            request.JobAttributes.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.Media, "iso_a4"));
        }
        if (hasMediaCol)
        {
            request.JobAttributes.Add(new IppAttribute(Tag.BegCollection, IppAttributeNames.MediaCol, NoValue.Instance));
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
        request.JobAttributes.Add(new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.PageRanges, new SharpIpp.Protocol.Models.Range(start1, end1)));
        request.JobAttributes.Add(new IppAttribute(Tag.RangeOfInteger, IppAttributeNames.PageRanges, new SharpIpp.Protocol.Models.Range(start2, end2)));

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
            request.JobAttributes.Add(new IppAttribute(Tag.Integer, IppAttributeNames.Copies, copiesValue.Value));
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
    [DataRow(IppAttributeNames.Copies)]
    [DataRow(IppAttributeNames.JobPriority)]
    [DataRow(IppAttributeNames.NumberUp)]
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
        request.JobAttributes.Add(new IppAttribute(Tag.Integer, IppAttributeNames.JobPriority, jobPriority));

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
        request.JobAttributes.Add(new IppAttribute(Tag.Integer, IppAttributeNames.NumberUp, numberUp));

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
            request.OperationAttributes.Add(new IppAttribute(Tag.Integer, IppAttributeNames.ResourceId, 1));
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
            request.OperationAttributes.Add(new IppAttribute(Tag.Integer, IppAttributeNames.PrinterId, 1));
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
            request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.IppAttributeFidelity, true));
        }
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.Media, media));

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
            request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.IppAttributeFidelity, true));
        }
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, IppAttributeNames.Finishings, (int)requested));

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
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, IppAttributeNames.Finishings, (int)Finishings.Staple));

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
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, IppAttributeNames.Finishings, (int)Finishings.None));
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, IppAttributeNames.Finishings, (int)Finishings.Staple));

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
            request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.IppAttributeFidelity, true));
        }
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.Sides, requested));

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
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.Sides, Sides.TwoSidedLongEdge.Value));

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
            request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.IppAttributeFidelity, true));
        }
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, IppAttributeNames.PrintQuality, (int)requested));

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
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, IppAttributeNames.PrintQuality, (int)PrintQuality.Draft));

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
            request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.IppAttributeFidelity, true));
        }
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, IppAttributeNames.OrientationRequested, (int)requested));

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
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, IppAttributeNames.OrientationRequested, (int)Orientation.Landscape));

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
            request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.IppAttributeFidelity, true));
        }
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.PrintColorMode, requested));

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
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.PrintColorMode, PrintColorMode.Monochrome.Value));

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
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.NoValue, IppAttributeNames.PrintColorMode, NoValue.Instance));

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
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.PrintQuality, "draft"));

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
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.OrientationRequested, "landscape"));

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
        request.OperationAttributes.Add(new IppAttribute(Tag.Uri, IppAttributeNames.OutputDeviceUuid, "ipp://127.0.0.1/output-device"));
        if (hasJobIds)
        {
            request.OperationAttributes.Add(new IppAttribute(Tag.Integer, IppAttributeNames.JobIds, 1));
            if (mismatchedCardinality)
            {
                request.OperationAttributes.Add(new IppAttribute(Tag.Integer, IppAttributeNames.JobIds, 2));
            }
        }
        if (hasOutputDeviceJobStates)
        {
            request.OperationAttributes.Add(new IppAttribute(Tag.Enum, IppAttributeNames.OutputDeviceJobStates, (int)JobState.Processing));
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
        request.PrinterAttributes.Add(new IppAttribute(Tag.NoValue, IppAttributeNames.DestinationUriReady, NoValue.Instance));

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
            attr = new IppAttribute(Tag.Keyword, "destination-attributes-supported", new NullToStringObject());
        }
        else
        {
            attr = new IppAttribute(Tag.Keyword, "destination-attributes-supported", IppAttributeNames.DocumentPassword);
        }
        request.PrinterAttributes.AddRange(new[] { attr }.ToBegCollection(IppAttributeNames.DestinationUriReady));

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
        }.ToBegCollection(IppAttributeNames.DestinationUriReady));

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
            new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.DocumentPassword, "secret"),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
        }.ToBegCollection(IppAttributeNames.DestinationUriReady));

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
        }.ToBegCollection(IppAttributeNames.DestinationUriReady));

        validator.ValidatePrinterAttributes(request);
    }

    [TestMethod]
    public void ValidateCollectionMediaSelectionRules_OutOfBandCollection_ShouldSkip()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.CreateJob);
        validator.ValidateCollectionMediaSelectionRules(new[] { new IppAttribute(Tag.NoValue, IppAttributeNames.CoverBack, NoValue.Instance) }, request);
    }

    [TestMethod]
    public void ValidateCollectionMediaSelectionRules_InvalidEncoding_ShouldThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.CreateJob);
        var attrs = new[]
        {
            new IppAttribute(Tag.BegCollection, IppAttributeNames.CoverBack, NoValue.Instance),
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
        var attr = new IppAttribute(Tag.Keyword, IppAttributeNames.OutputBin, new NullToStringObject());
        validator.ValidateOutputBinAgainstSupportedValues(attr, request);
    }

    [TestMethod]
    public void ValidateOutputBinAgainstSupportedValues_WhitespaceValue_ShouldReturn()
    {
        var validator = new IppRequestMessageValidator();
        validator.Context.OutputBinSupported = new[] { new OutputBin("top", true) };
        var request = CreateBasicRequest(IppOperation.CreateJob);
        validator.ValidateOutputBinAgainstSupportedValues(new IppAttribute(Tag.Keyword, IppAttributeNames.OutputBin, "   "), request);
    }

    [TestMethod]
    public void ValidateOperationRules_SetDocumentAttributes_WithValidState_ShouldPass()
    {
        var validator = new IppRequestMessageValidator();
        validator.Context.DocumentStatesByNumber = new Dictionary<int, DocumentState> { { 1, DocumentState.Pending } };
        var request = CreateBasicRequest(IppOperation.SetDocumentAttributes);
        request.OperationAttributes.Add(new IppAttribute(Tag.Integer, IppAttributeNames.DocumentNumber, 1));
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
        request.OperationAttributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.DocumentPassword, "secret"));

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
        request.OperationAttributes.Add(new IppAttribute(Tag.NoValue, IppAttributeNames.DestinationUris, NoValue.Instance));
        validator.ValidateCreateJobDestinationRules(request.OperationAttributes, request);
    }

    [TestMethod]
    public void ValidateCreateJobDestinationRules_NullUriValue_ShouldSkip()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.CreateJob);
        var attr = new IppAttribute(Tag.Uri, "destination-uri", new NullToStringObject());
        request.OperationAttributes.AddRange(new[] { attr }.ToBegCollection(IppAttributeNames.DestinationUris));
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
        }.ToBegCollection(IppAttributeNames.DestinationUris));

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
        }.ToBegCollection(IppAttributeNames.DestinationUris));

        request.OperationAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.Keyword, "access-oauth-token", "token")
        }.ToBegCollection(IppAttributeNames.DestinationAccesses));
        request.OperationAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.Keyword, "access-oauth-token", "token2")
        }.ToBegCollection(IppAttributeNames.DestinationAccesses));

        Action act = () => validator.ValidateCreateJobDestinationRules(request.OperationAttributes, request);
        act.Should().Throw<IppRequestException>().WithMessage("destination-accesses cardinality MUST match destination-uris");
    }

    [TestMethod]
    public void ValidateJobRequestedAttributesGroupKeywords_NullValue_ShouldSkip()
    {
        var validator = new IppRequestMessageValidator();
        validator.Context.JobRequestedAttributeGroupKeywordsSupported = new[] { "all" };
        var request = CreateBasicRequest(IppOperation.GetJobAttributes);
        request.OperationAttributes.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.RequestedAttributes, new NullToStringObject()));
        validator.ValidateJobRequestedAttributesGroupKeywords(request);
    }

    [TestMethod]
    public void ValidateJobRequestedAttributesGroupKeywords_FidelityTrue_ShouldReturn()
    {
        var validator = new IppRequestMessageValidator { UseIppAttributeFidelityForCapabilityValidation = true };
        validator.Context.JobRequestedAttributeGroupKeywordsSupported = new[] { "all" };
        var request = CreateBasicRequest(IppOperation.GetJobAttributes);
        request.OperationAttributes.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.RequestedAttributes, "job-description"));
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.IppAttributeFidelity, true));

        Action act = () => validator.ValidateJobRequestedAttributesGroupKeywords(request);
        act.Should().Throw<IppRequestException>();

        request.OperationAttributes.RemoveAll(x => x.Name == IppAttributeNames.IppAttributeFidelity);
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.IppAttributeFidelity, false));
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
            attr = new IppAttribute(Tag.Enum, IppAttributeNames.FetchStatusCode, (int)rawValue);
        }
        else
        {
            attr = new IppAttribute(Tag.Enum, IppAttributeNames.FetchStatusCode, rawValue);
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
        request.OperationAttributes.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.RequestedAttributes, new NullToStringObject()));
        validator.ValidateDocumentRequestedAttributesGroupKeywords(request);
    }

    [TestMethod]
    public void ValidateDocumentRequestedAttributesGroupKeywords_FidelityFalse_ShouldReturn()
    {
        var validator = new IppRequestMessageValidator { UseIppAttributeFidelityForCapabilityValidation = true };
        validator.Context.DocumentRequestedAttributeGroupKeywordsSupported = new[] { "all" };
        var request = CreateBasicRequest(IppOperation.GetDocumentAttributes);
        request.OperationAttributes.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.RequestedAttributes, IppAttributeNames.DocumentDescription));
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.IppAttributeFidelity, false));
        validator.ValidateDocumentRequestedAttributesGroupKeywords(request);
    }

    [TestMethod]
    public void ValidateNotifyEventsValues_NullValue_ShouldSkip()
    {
        var validator = new IppRequestMessageValidator();
        validator.Context.NotifyEventsSupported = new[] { "job-completed" };
        var request = CreateBasicRequest(IppOperation.CreateJob);
        request.SubscriptionAttributes.Add(new IppAttribute(Tag.Keyword, "notify-events", new NullToStringObject()));
        validator.ValidateNotifyEventsValues(request);
    }

    [TestMethod]
    public void ValidateNotifyEventsValues_FidelityFalse_ShouldReturn()
    {
        var validator = new IppRequestMessageValidator { UseIppAttributeFidelityForCapabilityValidation = true };
        validator.Context.NotifyEventsSupported = new[] { "job-completed" };
        var request = CreateBasicRequest(IppOperation.CreateJob);
        request.SubscriptionAttributes.Add(new IppAttribute(Tag.Keyword, "notify-events", "job-created"));
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.IppAttributeFidelity, false));
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
            new IppAttribute(Tag.Integer, IppAttributeNames.Copies, 1),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
        }.ToBegCollection(IppAttributeNames.DestinationUriReady));

        validator.ValidatePrinterAttributes(request);
    }

    [TestMethod]
    public void ValidatePrinterAttributes_SupportedValidMembers_ShouldPass()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.GetPrinterAttributes);
        request.PrinterAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.Keyword, "destination-attributes-supported", IppAttributeNames.Copies)
        }.ToBegCollection(IppAttributeNames.DestinationUriReady));

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
            new IppAttribute(Tag.BegCollection, IppAttributeNames.Overrides, NoValue.Instance),
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
        request.OperationAttributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.DocumentPassword, "secret"));
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
            new IppAttribute(Tag.BegCollection, IppAttributeNames.CoverBack, NoValue.Instance),
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
        var attr = new IppAttribute(Tag.Keyword, IppAttributeNames.OutputBin, "top");
        validator.ValidateOutputBinAgainstSupportedValues(attr, request);
    }

    [TestMethod]
    public void ValidateUniqueAttributes_WithDuplicates_ShouldThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.OperationAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.Keyword, IppAttributeNames.RequestedAttributes, "all"),
            new IppAttribute(Tag.Integer, IppAttributeNames.Copies, 1),
            new IppAttribute(Tag.Keyword, IppAttributeNames.RequestedAttributes, "media")
        });

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("Duplicate attribute 'requested-attributes' in group 'operation-attributes'");
    }

    [TestMethod]
    public void ValidateUniqueAttributes_WithConsecutiveSameName_ShouldPass()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.OperationAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.Keyword, IppAttributeNames.RequestedAttributes, "all"),
            new IppAttribute(Tag.Keyword, IppAttributeNames.RequestedAttributes, "media")
        });

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateDocumentMetadata_WithInvalidFormat_ShouldThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.DocumentMetadata, new OctetString("invalid-format")));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid document-metadata value: must be in keyword=value format");
    }

    [TestMethod]
    public void ValidateDocumentMetadata_WithInvalidKeyword_ShouldThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.DocumentMetadata, new OctetString("invalidkeyword=val")));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid document-metadata keyword 'invalidkeyword'");
    }

    [TestMethod]
    public void ValidateDocumentMetadata_WithValidKeyword_ShouldPass()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.DocumentMetadata, new OctetString("title=val")));
        request.OperationAttributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.DocumentMetadata, new OctetString("x-custom_keyword-1=val")));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_RegisterOutputDevice_WithBothX509CertificateAndRequest_Throws()
    {
        var validator = IppRequestMessageValidator.Default;
        var request = CreateBasicSystemRequest(IppOperation.RegisterOutputDevice);
        request.OperationAttributes.Add(new IppAttribute(Tag.Uri, IppAttributeNames.OutputDeviceUuid, "urn:uuid:123e4567-e89b-12d3-a456-426614174000"));
        request.OperationAttributes.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.OutputDeviceX509Certificate, "cert-data"));
        request.OperationAttributes.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.OutputDeviceX509Request, "csr-data"));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorConflictingAttributes);
    }

    [TestMethod]
    public void Validate_RegisterOutputDevice_MissingOutputDeviceUuid_Throws()
    {
        var validator = IppRequestMessageValidator.Default;
        var request = CreateBasicSystemRequest(IppOperation.RegisterOutputDevice);

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_SetPrinterAttributes_DefaultValueNotInSupportedValues_Throws()
    {
        var validator = IppRequestMessageValidator.Default;
        var request = CreateMinimalSetPrinterAttributesRequest(new List<IppAttribute>
        {
            new IppAttribute(Tag.Integer, "copies-default", 15),
            new IppAttribute(Tag.RangeOfInteger, "copies-supported", new SharpIpp.Protocol.Models.Range(1, 10))
        });

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    [TestMethod]
    public void Validate_SetPrinterAttributes_DefaultValueInSupportedValues_Passes()
    {
        var validator = IppRequestMessageValidator.Default;
        var request = CreateMinimalSetPrinterAttributesRequest(new List<IppAttribute>
        {
            new IppAttribute(Tag.Integer, "copies-default", 5),
            new IppAttribute(Tag.RangeOfInteger, "copies-supported", new SharpIpp.Protocol.Models.Range(1, 10)),
            new IppAttribute(Tag.Keyword, "media-default", "iso_a4"),
            new IppAttribute(Tag.Keyword, "media-supported", "iso_a4"),
            new IppAttribute(Tag.Keyword, "media-supported", "na_letter")
        });

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateOverridesRules_MultipleCollections_NoOverlap_ShouldPass()
    {
        var validator = new IppRequestMessageValidator();
        validator.Context.OverridesSupportedOperations = new HashSet<IppOperation> { IppOperation.CreateJob };
        var request = CreateBasicRequest(IppOperation.CreateJob);
        request.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.BegCollection, IppAttributeNames.Overrides, NoValue.Instance),
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.RangeOfInteger, "document-numbers", new SharpIpp.Protocol.Models.Range(1, 2)),
            new IppAttribute(Tag.Keyword, "some-job-attr", "value"),
            new IppAttribute(Tag.EndCollection, string.Empty, NoValue.Instance),

            new IppAttribute(Tag.BegCollection, IppAttributeNames.Overrides, NoValue.Instance),
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(2, 2)),
            new IppAttribute(Tag.RangeOfInteger, "document-numbers", new SharpIpp.Protocol.Models.Range(3, 4)),
            new IppAttribute(Tag.Keyword, "some-job-attr", "value"),
            new IppAttribute(Tag.EndCollection, string.Empty, NoValue.Instance)
        });

        validator.Context.OverrideMemberScopesByName = new Dictionary<string, OverrideMemberScope>
        {
            { "some-job-attr", OverrideMemberScope.Page }
        };
        validator.ValidateJobAttributesGroup = true;

        Action act = () => validator.Validate(request);
        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateOverridesRules_MultipleCollections_OverlappingDocumentNumbers_Throws()
    {
        var validator = new IppRequestMessageValidator();
        validator.Context.OverridesSupportedOperations = new HashSet<IppOperation> { IppOperation.CreateJob };
        var request = CreateBasicRequest(IppOperation.CreateJob);
        request.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.BegCollection, IppAttributeNames.Overrides, NoValue.Instance),
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.RangeOfInteger, "document-numbers", new SharpIpp.Protocol.Models.Range(1, 3)),
            new IppAttribute(Tag.Keyword, "some-job-attr", "value"),
            new IppAttribute(Tag.EndCollection, string.Empty, NoValue.Instance),

            new IppAttribute(Tag.BegCollection, IppAttributeNames.Overrides, NoValue.Instance),
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(2, 2)),
            new IppAttribute(Tag.RangeOfInteger, "document-numbers", new SharpIpp.Protocol.Models.Range(2, 4)),
            new IppAttribute(Tag.Keyword, "some-job-attr", "value"),
            new IppAttribute(Tag.EndCollection, string.Empty, NoValue.Instance)
        });

        validator.Context.OverrideMemberScopesByName = new Dictionary<string, OverrideMemberScope>
        {
            { "some-job-attr", OverrideMemberScope.Page }
        };
        validator.ValidateJobAttributesGroup = true;

        Action act = () => validator.Validate(request);
        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid overrides: override collections must have ascending, non-overlapping document-numbers");
    }

    [TestMethod]
    public void ValidateDocumentMetadata_WithInvalidUtf8Bytes_ShouldThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.DocumentMetadata, new byte[] { 0xC3, 0x28 }));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid document-metadata value encoding: must be valid UTF-8 and contain no control characters");
    }

    [TestMethod]
    public void ValidateDocumentMetadata_WithControlCharacters_ShouldThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.DocumentMetadata, new byte[] { 0x01 }));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid document-metadata value encoding: must be valid UTF-8 and contain no control characters");
    }

    [TestMethod]
    public void ValidateDocumentMetadata_WithXPrefixOnly_ShouldThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.DocumentMetadata, new OctetString("x-=val")));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid document-metadata keyword 'x-'");
    }

    [TestMethod]
    public void ValidateDocumentMetadata_WithInvalidCharactersInXKeyword_ShouldThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.DocumentMetadata, new OctetString("x-abc#=val")));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid document-metadata keyword 'x-abc#'");
    }

    [TestMethod]
    public void ValidateDocumentMetadata_WithByteArrayValue_ShouldPass()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        var bytes = System.Text.Encoding.UTF8.GetBytes("title=val");
        request.OperationAttributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.DocumentMetadata, bytes));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateDocumentMetadata_WithStringValue_ShouldPass()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.DocumentMetadata, "title=val"));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateDocumentMetadata_WithIntValue_ShouldSkip()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.DocumentMetadata, 123));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateDocumentMetadata_WithOutOfBandTag_ShouldSkip()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.NoValue, IppAttributeNames.DocumentMetadata, NoValue.Instance));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateUniqueAttributes_WithEmptyAttributeNameAtRoot_ShouldSkip()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.Integer, "", 123));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_SetPrinterAttributes_DefaultWithoutSupported_ShouldPass()
    {
        var validator = IppRequestMessageValidator.Default;
        var request = CreateMinimalSetPrinterAttributesRequest(new List<IppAttribute>
        {
            new IppAttribute(Tag.Integer, "copies-default", 5)
        });

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void GetAttributeValues_WithNullValue_ShouldYieldBreak()
    {
        var method = typeof(IppRequestMessageValidator).GetMethod("GetAttributeValues", BindingFlags.NonPublic | BindingFlags.Static);
        method.Should().NotBeNull();
        var attr = default(IppAttribute);
        var result = (IEnumerable<object>)method!.Invoke(null, new object[] { attr })!;
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void Validate_SetPrinterAttributes_DefaultWithCollectionValueContainingNull_ShouldPass()
    {
        var validator = IppRequestMessageValidator.Default;
        var request = CreateMinimalSetPrinterAttributesRequest(new List<IppAttribute>
        {
            new IppAttribute(Tag.Integer, "copies-default", new object[] { 3, null!, 5 }),
            new IppAttribute(Tag.RangeOfInteger, "copies-supported", new SharpIpp.Protocol.Models.Range(1, 10))
        });

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_SetPrinterAttributes_DefaultValueLongInSupportedRange_Passes()
    {
        var validator = IppRequestMessageValidator.Default;
        var request = CreateMinimalSetPrinterAttributesRequest(new List<IppAttribute>
        {
            new IppAttribute(Tag.Integer, "copies-default", 5L),
            new IppAttribute(Tag.RangeOfInteger, "copies-supported", new SharpIpp.Protocol.Models.Range(1, 10))
        });

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_SetPrinterAttributes_DefaultValueStringInSupportedRange_Passes()
    {
        var validator = IppRequestMessageValidator.Default;
        var request = CreateMinimalSetPrinterAttributesRequest(new List<IppAttribute>
        {
            new IppAttribute(Tag.Integer, "copies-default", "5"),
            new IppAttribute(Tag.RangeOfInteger, "copies-supported", new SharpIpp.Protocol.Models.Range(1, 10))
        });

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_SetPrinterAttributes_DefaultValueIntBelowSupportedRange_Throws()
    {
        var validator = IppRequestMessageValidator.Default;
        var request = CreateMinimalSetPrinterAttributesRequest(new List<IppAttribute>
        {
            new IppAttribute(Tag.Integer, "copies-default", 0),
            new IppAttribute(Tag.RangeOfInteger, "copies-supported", new SharpIpp.Protocol.Models.Range(1, 10))
        });

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    [TestMethod]
    public void Validate_SetPrinterAttributes_DefaultValueLongBelowSupportedRange_Throws()
    {
        var validator = IppRequestMessageValidator.Default;
        var request = CreateMinimalSetPrinterAttributesRequest(new List<IppAttribute>
        {
            new IppAttribute(Tag.Integer, "copies-default", 0L),
            new IppAttribute(Tag.RangeOfInteger, "copies-supported", new SharpIpp.Protocol.Models.Range(1, 10))
        });

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    [TestMethod]
    public void Validate_SetPrinterAttributes_DefaultValueLongAboveSupportedRange_Throws()
    {
        var validator = IppRequestMessageValidator.Default;
        var request = CreateMinimalSetPrinterAttributesRequest(new List<IppAttribute>
        {
            new IppAttribute(Tag.Integer, "copies-default", 15L),
            new IppAttribute(Tag.RangeOfInteger, "copies-supported", new SharpIpp.Protocol.Models.Range(1, 10))
        });

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    [TestMethod]
    public void Validate_SetPrinterAttributes_DefaultValueStringBelowSupportedRange_Throws()
    {
        var validator = IppRequestMessageValidator.Default;
        var request = CreateMinimalSetPrinterAttributesRequest(new List<IppAttribute>
        {
            new IppAttribute(Tag.Integer, "copies-default", "0"),
            new IppAttribute(Tag.RangeOfInteger, "copies-supported", new SharpIpp.Protocol.Models.Range(1, 10))
        });

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    [TestMethod]
    public void Validate_SetPrinterAttributes_DefaultValueStringAboveSupportedRange_Throws()
    {
        var validator = IppRequestMessageValidator.Default;
        var request = CreateMinimalSetPrinterAttributesRequest(new List<IppAttribute>
        {
            new IppAttribute(Tag.Integer, "copies-default", "15"),
            new IppAttribute(Tag.RangeOfInteger, "copies-supported", new SharpIpp.Protocol.Models.Range(1, 10))
        });

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    [TestMethod]
    public void Validate_SetPrinterAttributes_DefaultValueNonNumericStringForRangeSupported_Throws()
    {
        var validator = IppRequestMessageValidator.Default;
        var request = CreateMinimalSetPrinterAttributesRequest(new List<IppAttribute>
        {
            new IppAttribute(Tag.Integer, "copies-default", "abc"),
            new IppAttribute(Tag.RangeOfInteger, "copies-supported", new SharpIpp.Protocol.Models.Range(1, 10))
        });

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    [TestMethod]
    public void Validate_SetPrinterAttributes_DefaultValueNullToStringForRangeSupported_Throws()
    {
        var validator = IppRequestMessageValidator.Default;
        var request = CreateMinimalSetPrinterAttributesRequest(new List<IppAttribute>
        {
            new IppAttribute(Tag.Integer, "copies-default", new NullToStringObject()),
            new IppAttribute(Tag.RangeOfInteger, "copies-supported", new SharpIpp.Protocol.Models.Range(1, 10))
        });

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    [TestMethod]
    public void Validate_SetPrinterAttributes_DefaultValueNullToStringForStringSupported_Throws()
    {
        var validator = IppRequestMessageValidator.Default;
        var request = CreateMinimalSetPrinterAttributesRequest(new List<IppAttribute>
        {
            new IppAttribute(Tag.Keyword, "media-default", new NullToStringObject()),
            new IppAttribute(Tag.Keyword, "media-supported", "iso_a4")
        });

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    [TestMethod]
    public void Validate_SetPrinterAttributes_SupportedValueNullToString_Throws()
    {
        var validator = IppRequestMessageValidator.Default;
        var request = CreateMinimalSetPrinterAttributesRequest(new List<IppAttribute>
        {
            new IppAttribute(Tag.Keyword, "media-default", "iso_a4"),
            new IppAttribute(Tag.Keyword, "media-supported", new NullToStringObject())
        });

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    [TestMethod]
    public void Validate_StringLengthLimits_WhenTextTooLong_Throws()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();

        // 1024 octets exceeds 1023 limit
        var longText = new string('a', 1024);
        request.JobAttributes.Add(new IppAttribute(Tag.TextWithoutLanguage, "some-text-attribute", longText));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("Attribute 'some-text-attribute' of tag 'TextWithoutLanguage' length (1024 octets) exceeds RFC 8011 limit of 1023 octets")
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_StringLengthLimits_WhenNameTooLong_Throws()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();

        // 256 octets exceeds 255 limit
        var longName = new string('a', 256);
        request.JobAttributes.Add(new IppAttribute(Tag.NameWithoutLanguage, "some-name-attribute", longName));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("Attribute 'some-name-attribute' of tag 'NameWithoutLanguage' length (256 octets) exceeds RFC 8011 limit of 255 octets")
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_StringLengthLimits_WhenKeywordTooLong_Throws()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();

        // 256 octets exceeds 255 limit
        var longKeyword = new string('a', 256);
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, "some-keyword-attribute", longKeyword));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("Attribute 'some-keyword-attribute' of tag 'Keyword' length (256 octets) exceeds RFC 8011 limit of 255 octets")
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_StringLengthLimits_WhenUriTooLong_Throws()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();

        // 1024 octets exceeds 1023 limit
        var longUri = "http://" + new string('a', 1017);
        request.JobAttributes.Add(new IppAttribute(Tag.Uri, "some-uri-attribute", longUri));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("Attribute 'some-uri-attribute' of tag 'Uri' length (1024 octets) exceeds RFC 8011 limit of 1023 octets")
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_StringLengthLimits_WhenDisabled_DoesNotThrow()
    {
        var validator = new IppRequestMessageValidator { ValidateStringLengthLimits = false };
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();

        var longName = new string('a', 256);
        request.JobAttributes.Add(new IppAttribute(Tag.NameWithoutLanguage, "some-name-attribute", longName));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_TrayAndSupply_WithValidValues_DoesNotThrow()
    {
        var validator = IppRequestMessageValidator.Default;
        var printerAttributes = new List<IppAttribute>
        {
            new(Tag.BegCollection, IppAttributeNames.PrinterInputTray, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "type"),
            new(Tag.Keyword, string.Empty, "sheetFeedAutoRemovableTray"),
            new(Tag.MemberAttrName, string.Empty, "media-info"),
            new(Tag.TextWithoutLanguage, string.Empty, "Standard Tray 1"),
            new(Tag.EndCollection, string.Empty, NoValue.Instance)
        };
        var request = CreateMinimalSetPrinterAttributesRequest(printerAttributes);

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    [DataRow(IppAttributeNames.PrinterInputTray, "media-info", "\x01")]
    [DataRow(IppAttributeNames.PrinterOutputTray, "type", "bin-\x1F")]
    [DataRow(IppAttributeNames.PrinterSupply, "color-name", "cyan\x7F")]
    public void Validate_TrayAndSupply_WithControlCharacter_Throws(string colName, string memberName, string valueWithControlChar)
    {
        var validator = IppRequestMessageValidator.Default;
        var printerAttributes = new List<IppAttribute>
        {
            new(Tag.BegCollection, colName, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, memberName),
            new(Tag.Keyword, string.Empty, valueWithControlChar),
            new(Tag.EndCollection, string.Empty, NoValue.Instance)
        };
        var request = CreateMinimalSetPrinterAttributesRequest(printerAttributes);

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage($"Attribute '{memberName}' in '{colName}' collection contains forbidden control character(s) (0x00-0x1F, 0x7F)")
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_TrayAndSupply_WithControlCharacterWhenGroupDisabled_DoesNotThrow()
    {
        var validator = new IppRequestMessageValidator { ValidatePrinterAttributesGroup = false };
        var printerAttributes = new List<IppAttribute>
        {
            new(Tag.BegCollection, IppAttributeNames.PrinterInputTray, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "media-info"),
            new(Tag.Keyword, string.Empty, "\x05"),
            new(Tag.EndCollection, string.Empty, NoValue.Instance)
        };
        var request = CreateMinimalSetPrinterAttributesRequest(printerAttributes);

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_InputAttributes_WithAutoExposureFalseAndOtherSettings_DoesNotThrow()
    {
        var validator = IppRequestMessageValidator.Default;
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.Boolean, IppAttributeNames.InputAutoExposure, false),
            new IppAttribute(Tag.Integer, IppAttributeNames.InputBrightness, 50)
        }.ToBegCollection(IppAttributeNames.InputAttributes));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_InputAttributes_WithAutoExposureTrueAndNoBrightnessContrastOrSharpness_DoesNotThrow()
    {
        var validator = IppRequestMessageValidator.Default;
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.Boolean, IppAttributeNames.InputAutoExposure, true),
            new IppAttribute(Tag.Keyword, IppAttributeNames.InputColorMode, "color")
        }.ToBegCollection(IppAttributeNames.InputAttributes));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    [DataRow(IppAttributeNames.InputBrightness, 10)]
    [DataRow(IppAttributeNames.InputContrast, -5)]
    [DataRow(IppAttributeNames.InputSharpness, 0)]
    public void Validate_InputAttributes_WithAutoExposureTrueAndForbiddenAttribute_Throws(string forbiddenAttrName, int val)
    {
        var validator = IppRequestMessageValidator.Default;
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.Boolean, IppAttributeNames.InputAutoExposure, true),
            new IppAttribute(Tag.Integer, forbiddenAttrName, val)
        }.ToBegCollection(IppAttributeNames.InputAttributes));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage($"invalid input-attributes: '{forbiddenAttrName}' MUST NOT be supplied when 'input-auto-exposure' is true")
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_InputAttributes_WithAutoExposureTrueAndForbiddenAttributeWhenRulesDisabled_DoesNotThrow()
    {
        var validator = new IppRequestMessageValidator { ValidateOperationSpecificRules = false };
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.Boolean, IppAttributeNames.InputAutoExposure, true),
            new IppAttribute(Tag.Integer, IppAttributeNames.InputBrightness, 10)
        }.ToBegCollection(IppAttributeNames.InputAttributes));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_StringLengthLimits_WhenValueIsNull_DoesNotThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();

        // default(IppAttribute) will have Value = null
        request.JobAttributes.Add(default(IppAttribute));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_StringLengthLimits_WhenValueIsNoValue_DoesNotThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();

        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, "some-keyword", NoValue.Instance));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_StringLengthLimits_TextWithLanguage_WithValidValues_DoesNotThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();

        request.JobAttributes.Add(new IppAttribute(Tag.TextWithLanguage, "some-text", new StringWithLanguage("en", "valid text")));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_StringLengthLimits_TextWithLanguage_WhenValueTooLong_Throws()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();

        // 1024 octets exceeds 1023 limit
        var longValue = new string('a', 1024);
        request.JobAttributes.Add(new IppAttribute(Tag.TextWithLanguage, "some-text", new StringWithLanguage("en", longValue)));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("Attribute 'some-text' of tag 'TextWithLanguage' value length (1024 octets) exceeds RFC 8011 limit of 1023 octets")
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_StringLengthLimits_TextWithLanguage_WhenLanguageTooLong_Throws()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();

        // 256 octets exceeds 255 limit
        var longLanguage = new string('e', 256);
        request.JobAttributes.Add(new IppAttribute(Tag.TextWithLanguage, "some-text", new StringWithLanguage(longLanguage, "valid text")));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("Attribute 'some-text' of tag 'TextWithLanguage' language length (256 octets) exceeds RFC 8011 limit of 255 octets")
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_StringLengthLimits_TextWithLanguage_WithNullStringWithLanguageProperties_DoesNotThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();

        request.JobAttributes.Add(new IppAttribute(Tag.TextWithLanguage, "some-text", default(StringWithLanguage)));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_StringLengthLimits_NameWithLanguage_WithValidValues_DoesNotThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();

        request.JobAttributes.Add(new IppAttribute(Tag.NameWithLanguage, "some-name", new StringWithLanguage("en", "valid name")));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_StringLengthLimits_NameWithLanguage_WhenValueTooLong_Throws()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();

        // 256 octets exceeds 255 limit
        var longValue = new string('a', 256);
        request.JobAttributes.Add(new IppAttribute(Tag.NameWithLanguage, "some-name", new StringWithLanguage("en", longValue)));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("Attribute 'some-name' of tag 'NameWithLanguage' value length (256 octets) exceeds RFC 8011 limit of 255 octets")
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_StringLengthLimits_NameWithLanguage_WhenLanguageTooLong_Throws()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();

        // 256 octets exceeds 255 limit
        var longLanguage = new string('e', 256);
        request.JobAttributes.Add(new IppAttribute(Tag.NameWithLanguage, "some-name", new StringWithLanguage(longLanguage, "valid name")));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("Attribute 'some-name' of tag 'NameWithLanguage' language length (256 octets) exceeds RFC 8011 limit of 255 octets")
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_StringLengthLimits_NameWithLanguage_WithNullStringWithLanguageProperties_DoesNotThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();

        request.JobAttributes.Add(new IppAttribute(Tag.NameWithLanguage, "some-name", default(StringWithLanguage)));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_StringLengthLimits_WithNonUtf8Charset_UsesDynamicEncoding()
    {
        var validator = new IppRequestMessageValidator { ValidateCoreRules = false };
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();

        // Overwrite the first operation attribute to be attributes-charset = "iso-8859-1"
        request.OperationAttributes[0] = new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "iso-8859-1");

        // The character 'é' takes 2 bytes in UTF-8, but 1 byte in iso-8859-1.
        // A string of 255 'é' characters has:
        // - UTF-8 length = 510 bytes (exceeds 255 keyword limit)
        // - iso-8859-1 length = 255 bytes (exactly matches 255 keyword limit)
        var keyword = new string('é', 255);
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, "some-keyword", keyword));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_StringLengthLimits_WithNonUtf8Charset_UsesDynamicEncoding_ThrowsWhenTooLong()
    {
        var validator = new IppRequestMessageValidator { ValidateCoreRules = false };
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();

        // Overwrite the first operation attribute to be attributes-charset = "iso-8859-1"
        request.OperationAttributes[0] = new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "iso-8859-1");

        // 256 'é' characters in iso-8859-1 = 256 bytes (exceeds 255 limit)
        var keyword = new string('é', 256);
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, "some-keyword", keyword));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("Attribute 'some-keyword' of tag 'Keyword' length (256 octets) exceeds RFC 8011 limit of 255 octets");
    }

    [TestMethod]
    public void Validate_StringLengthLimits_WithCollectionOfStrings_ValidatesIndividualElements()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();

        var validKeyword = "valid";
        var longKeyword = new string('a', 256);
        var array = new[] { validKeyword, longKeyword };

        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, "some-keyword-array", array));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("Attribute 'some-keyword-array' of tag 'Keyword' length (256 octets) exceeds RFC 8011 limit of 255 octets");
    }

    [TestMethod]
    public void Validate_StringLengthLimits_WithOctetStringOverLimit_Throws()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();

        var longBytes = new byte[1024];
        request.JobAttributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, "some-octet-string", longBytes));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("Attribute 'some-octet-string' of tag 'OctetStringWithAnUnspecifiedFormat' length (1024 octets) exceeds RFC 8011 limit of 1023 octets");
    }

    [TestMethod]
    public void Validate_StringLengthLimits_WithOctetStringWithinLimit_Passes()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();

        var validBytes = new byte[1023];
        request.JobAttributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, "some-octet-string", validBytes));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_StringLengthLimits_WithInvalidCharset_FallsBackToUtf8()
    {
        var validator = new IppRequestMessageValidator { ValidateCoreRules = false };
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();

        request.OperationAttributes[0] = new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "invalid-charset-name");
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, "some-keyword", "valid-keyword"));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_StringLengthLimits_WithNoValueAndNullInArray_DoesNotThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();

        var array = new object?[] { null, NoValue.Instance };
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, "some-keyword", array));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_StringLengthLimits_TextWithLanguage_PlainString_Validation()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();

        request.JobAttributes.Add(new IppAttribute(Tag.TextWithLanguage, "text-1", "valid text"));

        Action act = () => validator.Validate(request);
        act.Should().NotThrow();

        var longText = new string('a', 1024);
        var request2 = CreateBasicRequest(IppOperation.PrintJob);
        request2.Document = new MemoryStream();
        request2.JobAttributes.Add(new IppAttribute(Tag.TextWithLanguage, "text-2", longText));

        Action act2 = () => validator.Validate(request2);
        act2.Should().Throw<IppRequestException>()
            .WithMessage("Attribute 'text-2' of tag 'TextWithLanguage' length (1024 octets) exceeds RFC 8011 limit of 1023 octets");
    }

    [TestMethod]
    public void Validate_StringLengthLimits_NameWithLanguage_PlainString_Validation()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();

        request.JobAttributes.Add(new IppAttribute(Tag.NameWithLanguage, "name-1", "valid name"));

        Action act = () => validator.Validate(request);
        act.Should().NotThrow();

        var longName = new string('a', 256);
        var request2 = CreateBasicRequest(IppOperation.PrintJob);
        request2.Document = new MemoryStream();
        request2.JobAttributes.Add(new IppAttribute(Tag.NameWithLanguage, "name-2", longName));

        Action act2 = () => validator.Validate(request2);
        act2.Should().Throw<IppRequestException>()
            .WithMessage("Attribute 'name-2' of tag 'NameWithLanguage' length (256 octets) exceeds RFC 8011 limit of 255 octets");
    }

    [TestMethod]
    public void Validate_TrayAndSupply_WithOutOfBandTag_DoesNotThrow()
    {
        var validator = IppRequestMessageValidator.Default;
        var printerAttributes = new List<IppAttribute>
        {
            new(Tag.Unknown, IppAttributeNames.PrinterInputTray, NoValue.Instance)
        };
        var request = CreateMinimalSetPrinterAttributesRequest(printerAttributes);

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_TrayAndSupply_WithNoValueOrNullMember_DoesNotThrow()
    {
        var validator = IppRequestMessageValidator.Default;
        var printerAttributes = new List<IppAttribute>
        {
            new(Tag.BegCollection, IppAttributeNames.PrinterInputTray, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "media-info"),
            new(Tag.NoValue, string.Empty, NoValue.Instance),
            new(Tag.EndCollection, string.Empty, NoValue.Instance)
        };
        var request = CreateMinimalSetPrinterAttributesRequest(printerAttributes);

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_InputAttributes_WithOutOfBandTag_DoesNotThrow()
    {
        var validator = IppRequestMessageValidator.Default;
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.Unknown, IppAttributeNames.InputAttributes, NoValue.Instance));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    [DataRow(1, true)]
    [DataRow(0, false)]
    [DataRow("true", true)]
    [DataRow("false", false)]
    public void Validate_InputAttributes_WithAutoExposureNonBool_Validation(object autoExposureVal, bool shouldThrow)
    {
        var validator = IppRequestMessageValidator.Default;
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.Integer, IppAttributeNames.InputAutoExposure, autoExposureVal),
            new IppAttribute(Tag.Integer, IppAttributeNames.InputBrightness, 50)
        }.ToBegCollection(IppAttributeNames.InputAttributes));

        Action act = () => validator.Validate(request);

        if (shouldThrow)
        {
            act.Should().Throw<IppRequestException>()
                .WithMessage($"invalid input-attributes: '{IppAttributeNames.InputBrightness}' MUST NOT be supplied when 'input-auto-exposure' is true");
        }
        else
        {
            act.Should().NotThrow();
        }
    }

    [TestMethod]
    public void Validate_TrayAndSupply_StringWithLanguage_ControlCharacters_Validation()
    {
        var validator = IppRequestMessageValidator.Default;

        var printerAttributes1 = new List<IppAttribute>
        {
            new(Tag.BegCollection, IppAttributeNames.PrinterInputTray, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "media-info"),
            new(Tag.TextWithLanguage, string.Empty, new StringWithLanguage("en", "Tray\x01")),
            new(Tag.EndCollection, string.Empty, NoValue.Instance)
        };
        var request1 = CreateMinimalSetPrinterAttributesRequest(printerAttributes1);
        Action act1 = () => validator.Validate(request1);
        act1.Should().Throw<IppRequestException>()
            .WithMessage("Attribute 'media-info' in 'printer-input-tray' collection contains forbidden control character(s) (0x00-0x1F, 0x7F)");

        var printerAttributes2 = new List<IppAttribute>
        {
            new(Tag.BegCollection, IppAttributeNames.PrinterInputTray, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "media-info"),
            new(Tag.TextWithLanguage, string.Empty, new StringWithLanguage("e\x01n", "Tray")),
            new(Tag.EndCollection, string.Empty, NoValue.Instance)
        };
        var request2 = CreateMinimalSetPrinterAttributesRequest(printerAttributes2);
        Action act2 = () => validator.Validate(request2);
        act2.Should().Throw<IppRequestException>()
            .WithMessage("Attribute 'media-info' in 'printer-input-tray' collection contains forbidden control character(s) (0x00-0x1F, 0x7F)");

        var printerAttributes3 = new List<IppAttribute>
        {
            new(Tag.BegCollection, IppAttributeNames.PrinterInputTray, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "media-info"),
            new(Tag.TextWithLanguage, string.Empty, new StringWithLanguage("en", "Tray")),
            new(Tag.EndCollection, string.Empty, NoValue.Instance)
        };
        var request3 = CreateMinimalSetPrinterAttributesRequest(printerAttributes3);
        Action act3 = () => validator.Validate(request3);
        act3.Should().NotThrow();

        var printerAttributes4 = new List<IppAttribute>
        {
            new(Tag.BegCollection, IppAttributeNames.PrinterInputTray, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "media-info"),
            new(Tag.Integer, string.Empty, 123),
            new(Tag.EndCollection, string.Empty, NoValue.Instance)
        };
        var request4 = CreateMinimalSetPrinterAttributesRequest(printerAttributes4);
        Action act4 = () => validator.Validate(request4);
        act4.Should().NotThrow();

        var printerAttributes5 = new List<IppAttribute>
        {
            new(Tag.BegCollection, IppAttributeNames.PrinterInputTray, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "media-info"),
            new(Tag.TextWithLanguage, string.Empty, new StringWithLanguage("e\x01n", null!)),
            new(Tag.EndCollection, string.Empty, NoValue.Instance)
        };
        var request5 = CreateMinimalSetPrinterAttributesRequest(printerAttributes5);
        Action act5 = () => validator.Validate(request5);
        act5.Should().Throw<IppRequestException>()
            .WithMessage("Attribute 'media-info' in 'printer-input-tray' collection contains forbidden control character(s) (0x00-0x1F, 0x7F)");

        var printerAttributes6 = new List<IppAttribute>
        {
            new(Tag.BegCollection, IppAttributeNames.PrinterInputTray, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "media-info"),
            new(Tag.TextWithLanguage, string.Empty, new StringWithLanguage(null!, "Tray\x01")),
            new(Tag.EndCollection, string.Empty, NoValue.Instance)
        };
        var request6 = CreateMinimalSetPrinterAttributesRequest(printerAttributes6);
        Action act6 = () => validator.Validate(request6);
        act6.Should().Throw<IppRequestException>()
            .WithMessage("Attribute 'media-info' in 'printer-input-tray' collection contains forbidden control character(s) (0x00-0x1F, 0x7F)");
    }

    [TestMethod]
    public void Validate_StringLengthLimits_TextWithoutLanguage_NullToStringObject_DoesNotThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.TextWithoutLanguage, "my-text", new NullToStringObject()));
        Action act = () => validator.Validate(request);
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_StringLengthLimits_TextWithLanguage_DefaultStringWithLanguage_DoesNotThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.TextWithLanguage, "my-text-lg", new StringWithLanguage()));
        Action act = () => validator.Validate(request);
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_StringLengthLimits_TextWithLanguage_NullToStringObject_DoesNotThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.TextWithLanguage, "my-text-lg-null", new NullToStringObject()));
        Action act = () => validator.Validate(request);
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_StringLengthLimits_NameWithoutLanguage_NullToStringObject_DoesNotThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.NameWithoutLanguage, "my-name", new NullToStringObject()));
        Action act = () => validator.Validate(request);
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_StringLengthLimits_NameWithLanguage_DefaultStringWithLanguage_DoesNotThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.NameWithLanguage, "my-name-lg", new StringWithLanguage()));
        Action act = () => validator.Validate(request);
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_StringLengthLimits_NameWithLanguage_NullToStringObject_DoesNotThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.NameWithLanguage, "my-name-lg-null", new NullToStringObject()));
        Action act = () => validator.Validate(request);
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_StringLengthLimits_OctetStringWithAnUnspecifiedFormat_DefaultOctetString_DoesNotThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, "my-octet-default", new OctetString()));
        Action act = () => validator.Validate(request);
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_StringLengthLimits_OctetStringWithAnUnspecifiedFormat_NullToStringObject_DoesNotThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, "my-octet-null", new NullToStringObject()));
        Action act = () => validator.Validate(request);
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_StringLengthLimits_OctetStringWithAnUnspecifiedFormat_NonNullOctetString_DoesNotThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, "my-octet", new OctetString("valid value")));
        Action act = () => validator.Validate(request);
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_StringLengthLimits_NameWithLanguage_NonNullValueNullLanguage_DoesNotThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.NameWithLanguage, "some-name", new StringWithLanguage(null!, "valid name")));
        Action act = () => validator.Validate(request);
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_StringLengthLimits_NameWithLanguage_NullValueNonNullLanguage_DoesNotThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.NameWithLanguage, "some-name", new StringWithLanguage("en", null!)));
        Action act = () => validator.Validate(request);
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_StringLengthLimits_TextWithLanguage_NonNullValueNullLanguage_DoesNotThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.TextWithLanguage, "some-text", new StringWithLanguage(null!, "valid text")));
        Action act = () => validator.Validate(request);
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_StringLengthLimits_TextWithLanguage_NullValueNonNullLanguage_DoesNotThrow()
    {
        var validator = new IppRequestMessageValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.TextWithLanguage, "some-text", new StringWithLanguage("en", null!)));
        Action act = () => validator.Validate(request);
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_WithPassedContext_UsesPassedContextInsteadOfInstanceContext()
    {
        var validator = new IppRequestMessageValidator
        {
            UseIppAttributeFidelityForCapabilityValidation = true
        };
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.Media, "iso_a5_148x210mm"));

        // Case 1: Call without passing context (falls back to validator.Context which has null MediaSupported)
        Action actDefault = () => validator.Validate(request);
        actDefault.Should().NotThrow();

        // Case 2: Call passing context with limited MediaSupported
        var customContext = new IppRequestValidationContext
        {
            MediaSupported = [ (Media)"iso_a4_210x297mm" ]
        };
        Action actCustom = () => validator.Validate(request, customContext);
        actCustom.Should().Throw<IppRequestException>()
            .WithMessage("'media' value 'iso_a5_148x210mm' is not supported by target printer");
    }
    #endregion
}
