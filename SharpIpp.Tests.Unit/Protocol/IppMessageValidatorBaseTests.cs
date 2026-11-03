using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Exceptions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Protocol;

[TestClass]
public class IppMessageValidatorBaseTests
{
    private class CustomResponseValidator : IppResponseMessageValidator
    {
        public bool CustomResponseHookExecuted { get; private set; }

        protected override void ValidateOperationRules(IIppResponseMessage response)
        {
            CustomResponseHookExecuted = true;
            base.ValidateOperationRules(response);
        }
    }
    private class TestMessageValidator : IppMessageValidatorBase
    {
        public void RunCoreHeader(int requestId, IppVersion version, Action<string, IppStatusCode> onError) =>
            ValidateCoreHeader(requestId, version, onError);

        public void RunOperationAttributesPrefix(IReadOnlyList<IppAttribute>? opAttrs, Action<string, IppStatusCode> onError) =>
            ValidateOperationAttributesPrefix(opAttrs, onError);

        public void RunUniqueAttributes(IEnumerable<IppAttribute>? attrs, string group, Action<string, IppStatusCode> onError) =>
            ValidateUniqueAttributesCore(attrs, group, onError);

        public void RunRequireAttribute(IEnumerable<IppAttribute> attrs, string name, Action<string, IppStatusCode> onError) =>
            RequireAttribute(attrs, name, onError);

        public void RunMutuallyExclusive(IReadOnlyCollection<IppAttribute> attrs, string name1, string name2, Action<string, IppStatusCode> onError) =>
            ValidateMutuallyExclusive(attrs, name1, name2, onError);

        public void RunCoDependent(IReadOnlyCollection<IppAttribute> attrs, string name1, string name2, Action<string, IppStatusCode> onError) =>
            ValidateCoDependent(attrs, name1, name2, onError);

        public void RunIntegerRange(
            IReadOnlyCollection<IppAttribute> attrs,
            string name,
            int min,
            int max,
            Action<string, IppStatusCode> onError,
            string? customMessage = null,
            IppStatusCode statusCode = IppStatusCode.ClientErrorBadRequest) =>
            ValidateIntegerRange(attrs, name, min, max, onError, customMessage, statusCode);

        public bool RunHasNamedAttribute(IEnumerable<IppAttribute>? attrs, string name) =>
            HasNamedAttribute(attrs, name);

        public IppAttribute? RunGetNamedAttribute(IEnumerable<IppAttribute>? attrs, string name) =>
            GetNamedAttribute(attrs, name);

        public int RunCountNamedAttributes(IEnumerable<IppAttribute>? attrs, string name) =>
            CountNamedAttributes(attrs, name);

        public void RunValidateStringLengthLimits(IEnumerable<IppAttribute>? attrs, IppAttribute? charsetAttr, Action<string> onError) =>
            ValidateStringLengthLimitsCore(attrs, charsetAttr, onError);

        public void RunValidateStringLengthLimits(IEnumerable<IppAttribute>? attrs, IppAttribute? charsetAttr, Action<string, IppStatusCode> onError, IppStatusCode statusCode = IppStatusCode.ClientErrorBadRequest) =>
            ValidateStringLengthLimitsCore(attrs, charsetAttr, onError, statusCode);
    }

    private class CustomRequestValidator : IppRequestMessageValidator
    {
        public bool CustomOperationHookExecuted { get; private set; }

        public override void ValidateOperationRules(IIppRequestMessage request, IppRequestValidationContext? context = null)
        {
            CustomOperationHookExecuted = true;
            base.ValidateOperationRules(request, context);
        }
    }

    [TestMethod]
    public void BaseProperties_DefaultToTrue()
    {
        var validator = new TestMessageValidator();
        validator.ValidateCoreRules.Should().BeTrue();
        validator.ValidateOperationAttributesGroup.Should().BeTrue();
        validator.ValidateJobAttributesGroup.Should().BeTrue();
        validator.ValidatePrinterAttributesGroup.Should().BeTrue();
        validator.ValidateUnsupportedAttributesGroup.Should().BeTrue();
        validator.ValidateSubscriptionAttributesGroup.Should().BeTrue();
        validator.ValidateEventNotificationAttributesGroup.Should().BeTrue();
        validator.ValidateResourceAttributesGroup.Should().BeTrue();
        validator.ValidateDocumentAttributesGroup.Should().BeTrue();
        validator.ValidateSystemAttributesGroup.Should().BeTrue();
        validator.ValidateStringLengthLimits.Should().BeTrue();
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    public void ValidateCoreHeader_InvalidRequestId_TriggersError(int requestId)
    {
        var validator = new TestMessageValidator();
        var errorCount = 0;
        validator.RunCoreHeader(requestId, new IppVersion(1, 1), (msg, status) =>
        {
            errorCount++;
            msg.Should().Be("Bad request-id value");
            status.Should().Be(IppStatusCode.ClientErrorBadRequest);
        });
        errorCount.Should().Be(1);
    }

    [TestMethod]
    public void ValidateCoreHeader_InvalidVersion_TriggersError()
    {
        var validator = new TestMessageValidator();
        var errorCount = 0;
        validator.RunCoreHeader(1, new IppVersion(0, 9), (msg, status) =>
        {
            errorCount++;
            msg.Should().Be("Unsupported IPP version");
            status.Should().Be(IppStatusCode.ServerErrorVersionNotSupported);
        });
        errorCount.Should().Be(1);
    }

    [TestMethod]
    public void ValidateOperationAttributesPrefix_Empty_TriggersError()
    {
        var validator = new TestMessageValidator();
        var errors = new List<string>();
        validator.RunOperationAttributesPrefix(new List<IppAttribute>(), (msg, _) => errors.Add(msg));
        errors.Should().Contain("No Operation Attributes");
    }

    [TestMethod]
    public void ValidateOperationAttributesPrefix_InvalidFirstAttribute_TriggersError()
    {
        var validator = new TestMessageValidator();
        var errors = new List<string>();
        var attrs = new List<IppAttribute>
        {
            new(Tag.NameWithoutLanguage, "printer-uri", "ipp://localhost/ipp/print")
        };
        validator.RunOperationAttributesPrefix(attrs, (msg, _) => errors.Add(msg));
        errors.Should().Contain("attributes-charset MUST be the first attribute");
    }

    [TestMethod]
    public void ValidateOperationAttributesPrefix_NonUtf8Charset_TriggersError()
    {
        var validator = new TestMessageValidator();
        var errors = new List<string>();
        var attrs = new List<IppAttribute>
        {
            new(Tag.Charset, "attributes-charset", "iso-8859-1"),
            new(Tag.NaturalLanguage, "attributes-natural-language", "en-us")
        };
        validator.RunOperationAttributesPrefix(attrs, (msg, _) => errors.Add(msg));
        errors.Should().Contain("attributes-charset MUST be 'utf-8'");
    }

    [TestMethod]
    public void ValidateOperationAttributesPrefix_InvalidSecondAttribute_TriggersError()
    {
        var validator = new TestMessageValidator();
        var errors = new List<string>();
        var attrs = new List<IppAttribute>
        {
            new(Tag.Charset, "attributes-charset", "utf-8"),
            new(Tag.Uri, "printer-uri", "ipp://localhost/ipp/print")
        };
        validator.RunOperationAttributesPrefix(attrs, (msg, _) => errors.Add(msg));
        errors.Should().Contain("attributes-natural-language MUST be the second attribute");
    }

    [TestMethod]
    public void ValidateUniqueAttributes_Duplicates_TriggersError()
    {
        var validator = new TestMessageValidator();
        var errors = new List<string>();
        var attrs = new List<IppAttribute>
        {
            new(Tag.NameWithoutLanguage, "job-name", "Job1"),
            new(Tag.Integer, "copies", 2),
            new(Tag.NameWithoutLanguage, "job-name", "Job2")
        };
        validator.RunUniqueAttributes(attrs, "job-attributes", (msg, _) => errors.Add(msg));
        errors.Should().Contain("Duplicate attribute 'job-name' in group 'job-attributes'");
    }

    [TestMethod]
    public void ValidateUniqueAttributes_NullAttributes_ReturnsImmediatelyWithoutError()
    {
        var validator = new TestMessageValidator();
        var errors = new List<string>();
        validator.RunUniqueAttributes(null, "job-attributes", (msg, _) => errors.Add(msg));
        errors.Should().BeEmpty();
    }

    [TestMethod]
    public void HasNamedAttribute_NullAttributes_ReturnsFalse()
    {
        var validator = new TestMessageValidator();
        validator.RunHasNamedAttribute(null, "job-id").Should().BeFalse();
    }

    [TestMethod]
    public void HasNamedAttribute_AttributeExists_ReturnsTrue()
    {
        var validator = new TestMessageValidator();
        var attrs = new List<IppAttribute>
        {
            new(Tag.Integer, "job-id", 123)
        };
        validator.RunHasNamedAttribute(attrs, "job-id").Should().BeTrue();
    }

    [TestMethod]
    public void HasNamedAttribute_AttributeDoesNotExist_ReturnsFalse()
    {
        var validator = new TestMessageValidator();
        var attrs = new List<IppAttribute>
        {
            new(Tag.Integer, "job-id", 123)
        };
        validator.RunHasNamedAttribute(attrs, "printer-id").Should().BeFalse();
    }

    [TestMethod]
    public void GetNamedAttribute_NullAttributes_ReturnsNull()
    {
        var validator = new TestMessageValidator();
        validator.RunGetNamedAttribute(null, "printer-uri").Should().BeNull();
    }

    [TestMethod]
    public void GetNamedAttribute_AttributeExists_ReturnsAttribute()
    {
        var validator = new TestMessageValidator();
        var expected = new IppAttribute(Tag.Uri, "printer-uri", "ipp://localhost");
        var attrs = new List<IppAttribute> { expected };
        validator.RunGetNamedAttribute(attrs, "printer-uri").Should().Be(expected);
    }

    [TestMethod]
    public void GetNamedAttribute_AttributeDoesNotExist_ReturnsNull()
    {
        var validator = new TestMessageValidator();
        var attrs = new List<IppAttribute> { new(Tag.Integer, "job-id", 123) };
        validator.RunGetNamedAttribute(attrs, "printer-uri").Should().BeNull();
    }

    [TestMethod]
    public void CountNamedAttributes_NullAttributes_ReturnsZero()
    {
        var validator = new TestMessageValidator();
        validator.RunCountNamedAttributes(null, "job-ids").Should().Be(0);
    }

    [TestMethod]
    public void CountNamedAttributes_AttributesExist_ReturnsCorrectCount()
    {
        var validator = new TestMessageValidator();
        var attrs = new List<IppAttribute>
        {
            new(Tag.Integer, "job-ids", 1),
            new(Tag.Integer, "job-ids", 2),
            new(Tag.NameWithoutLanguage, "job-name", "Test")
        };
        validator.RunCountNamedAttributes(attrs, "job-ids").Should().Be(2);
    }

    [TestMethod]
    public void CountNamedAttributes_AttributesDoNotExist_ReturnsZero()
    {
        var validator = new TestMessageValidator();
        var attrs = new List<IppAttribute>
        {
            new(Tag.NameWithoutLanguage, "job-name", "Test")
        };
        validator.RunCountNamedAttributes(attrs, "job-ids").Should().Be(0);
    }

    [TestMethod]
    public void ValidateStringLengthLimitsCore_NullAttributes_DoesNotTriggerError()
    {
        var validator = new TestMessageValidator();
        var errors = new List<string>();
        validator.RunValidateStringLengthLimits((IEnumerable<IppAttribute>?)null, null, (msg) => errors.Add(msg));
        errors.Should().BeEmpty();
    }

    [TestMethod]
    public void ValidateStringLengthLimitsCore_ValidAttributes_DoesNotTriggerError()
    {
        var validator = new TestMessageValidator();
        var errors = new List<string>();
        var attrs = new List<IppAttribute>
        {
            new(Tag.Keyword, "job-state-reasons", "job-incoming")
        };
        validator.RunValidateStringLengthLimits(attrs, null, (msg) => errors.Add(msg));
        errors.Should().BeEmpty();
    }

    [TestMethod]
    public void ValidateStringLengthLimitsCore_ExceedsLimit_TriggersErrorAction()
    {
        var validator = new TestMessageValidator();
        var errors = new List<string>();
        var attrs = new List<IppAttribute>
        {
            new(Tag.Keyword, "job-state-reasons", new string('a', 256))
        };
        validator.RunValidateStringLengthLimits(attrs, null, (msg) => errors.Add(msg));
        errors.Should().HaveCount(1);
        errors[0].Should().Contain("exceeds RFC 8011 limit");
    }

    [TestMethod]
    public void ValidateStringLengthLimitsCore_WithStatusCode_TriggersErrorWithStatusCode()
    {
        var validator = new TestMessageValidator();
        var errors = new List<(string Msg, IppStatusCode Code)>();
        var attrs = new List<IppAttribute>
        {
            new(Tag.MimeMediaType, "document-format", new string('b', 256))
        };
        validator.RunValidateStringLengthLimits(attrs, null, (msg, code) => errors.Add((msg, code)), IppStatusCode.ClientErrorBadRequest);
        errors.Should().HaveCount(1);
        errors[0].Code.Should().Be(IppStatusCode.ClientErrorBadRequest);
        errors[0].Msg.Should().Contain("exceeds RFC 8011 limit");
    }

    [TestMethod]
    public void RequireAttribute_MissingAttribute_TriggersError()
    {
        var validator = new TestMessageValidator();
        var errors = new List<string>();
        var attrs = new List<IppAttribute>
        {
            new(Tag.Integer, "job-id", 123)
        };
        validator.RunRequireAttribute(attrs, "printer-id", (msg, _) => errors.Add(msg));
        errors.Should().Contain("missing printer-id");
    }

    [TestMethod]
    public void ValidateMutuallyExclusive_BothPresent_TriggersError()
    {
        var validator = new TestMessageValidator();
        var errors = new List<string>();
        var attrs = new List<IppAttribute>
        {
            new(Tag.Keyword, "finishings", "none"),
            new(Tag.BegCollection, "finishings-col", string.Empty)
        };
        validator.RunMutuallyExclusive(attrs, "finishings", "finishings-col", (msg, _) => errors.Add(msg));
        errors.Should().HaveCount(1);
    }

    [TestMethod]
    public void ValidateCoDependent_OnlyOnePresent_TriggersError()
    {
        var validator = new TestMessageValidator();
        var errors = new List<string>();
        var attrs = new List<IppAttribute>
        {
            new(Tag.NameWithoutLanguage, "job-password", "secret")
        };
        validator.RunCoDependent(attrs, "job-password", "job-password-encryption", (msg, _) => errors.Add(msg));
        errors.Should().HaveCount(1);
    }

    [TestMethod]
    [DataRow(0, false)]
    [DataRow(1, true)]
    [DataRow(50, true)]
    [DataRow(100, true)]
    [DataRow(101, false)]
    public void ValidateIntegerRange_ChecksRange(int value, bool isValid)
    {
        var validator = new TestMessageValidator();
        var errors = new List<string>();
        var attrs = new List<IppAttribute>
        {
            new(Tag.Integer, "job-priority", value)
        };
        validator.RunIntegerRange(attrs, "job-priority", 1, 100, (msg, _) => errors.Add(msg));
        (errors.Count == 0).Should().Be(isValid);
    }

    [TestMethod]
    public void ValidateIntegerRange_AttributeNotPresent_DoesNotTriggerError()
    {
        var validator = new TestMessageValidator();
        var errors = new List<string>();
        var attrs = new List<IppAttribute>();
        validator.RunIntegerRange(attrs, "job-priority", 1, 100, (msg, _) => errors.Add(msg));
        errors.Should().BeEmpty();
    }

    [TestMethod]
    public void ValidateIntegerRange_AttributeWrongType_DoesNotTriggerError()
    {
        var validator = new TestMessageValidator();
        var errors = new List<string>();
        var attrs = new List<IppAttribute>
        {
            new(Tag.NameWithoutLanguage, "job-priority", "not-an-int")
        };
        validator.RunIntegerRange(attrs, "job-priority", 1, 100, (msg, _) => errors.Add(msg));
        errors.Should().BeEmpty();
    }

    [TestMethod]
    public void ValidateIntegerRange_OutOfRange_FormatsStandardMessageAndStatusCode()
    {
        var validator = new TestMessageValidator();
        var errors = new List<(string Msg, IppStatusCode Code)>();
        var attrs = new List<IppAttribute>
        {
            new(Tag.Integer, "job-priority", 0)
        };
        validator.RunIntegerRange(attrs, "job-priority", 1, 100, (msg, code) => errors.Add((msg, code)));
        errors.Should().HaveCount(1);
        errors[0].Msg.Should().Be("'job-priority' must be in the range 1-100");
        errors[0].Code.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void ValidateIntegerRange_MaxIsIntMaxValue_FormatsGreaterThanOrEqualToMessage()
    {
        var validator = new TestMessageValidator();
        var errors = new List<(string Msg, IppStatusCode Code)>();
        var attrs = new List<IppAttribute>
        {
            new(Tag.Integer, "copies", 0)
        };
        validator.RunIntegerRange(attrs, "copies", 1, int.MaxValue, (msg, code) => errors.Add((msg, code)));
        errors.Should().HaveCount(1);
        errors[0].Msg.Should().Be("'copies' must be >= 1");
        errors[0].Code.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void ValidateIntegerRange_CustomMessageAndStatusCode_UsesOverrides()
    {
        var validator = new TestMessageValidator();
        var errors = new List<(string Msg, IppStatusCode Code)>();
        var attrs = new List<IppAttribute>
        {
            new(Tag.Integer, "job-priority", 150)
        };
        validator.RunIntegerRange(
            attrs,
            "job-priority",
            1,
            100,
            (msg, code) => errors.Add((msg, code)),
            customMessage: "Priority out of range",
            statusCode: IppStatusCode.ClientErrorNotPossible);

        errors.Should().HaveCount(1);
        errors[0].Msg.Should().Be("Priority out of range");
        errors[0].Code.Should().Be(IppStatusCode.ClientErrorNotPossible);
    }

    [TestMethod]
    public void CustomSubclass_CanOverrideValidationMethods()
    {
        var custom = new CustomRequestValidator();
        var request = new IppRequestMessage
        {
            IppOperation = IppOperation.GetPrinterAttributes,
            RequestId = 42,
            Version = new IppVersion(2, 0),
        };
        request.OperationAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.Charset, "attributes-charset", "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, "attributes-natural-language", "en-us"),
            new IppAttribute(Tag.Uri, "printer-uri", "ipp://localhost/ipp/print"),
        });

        custom.Validate(request);
        custom.CustomOperationHookExecuted.Should().BeTrue();
    }

    [TestMethod]
    [DataRow(null, "utf-8")]
    [DataRow("", "utf-8")]
    [DataRow("   ", "utf-8")]
    [DataRow("utf-8", "utf-8")]
    [DataRow("utf-16", "utf-16")]
    [DataRow("invalid-unknown-charset-name", "utf-8")]
    public void IppEncodingHelper_ResolvesEncodingSafely(string? charset, string expectedWebName)
    {
        var encoding = IppEncodingHelper.GetEncoding(charset);
        encoding.WebName.ToLowerInvariant().Should().Be(expectedWebName);
    }

    [TestMethod]
    public void IppEncodingHelper_AttributeOverload_WorksCorrectly()
    {
        var attr = new IppAttribute(Tag.Charset, "attributes-charset", "utf-8");
        var encoding = IppEncodingHelper.GetEncoding(attr);
        encoding.WebName.ToLowerInvariant().Should().Be("utf-8");

        var nullAttrEncoding = IppEncodingHelper.GetEncoding((IppAttribute?)null);
        nullAttrEncoding.Should().Be(Encoding.UTF8);
    }

    [TestMethod]
    public void CustomResponseSubclass_CanOverrideOperationRules()
    {
        var custom = new CustomResponseValidator();
        var response = new IppResponseMessage
        {
            StatusCode = IppStatusCode.SuccessfulOk,
            RequestId = 42,
            Version = new IppVersion(2, 0),
        };
        response.OperationAttributes.Add(new List<IppAttribute>
        {
            new(Tag.Charset, "attributes-charset", "utf-8"),
            new(Tag.NaturalLanguage, "attributes-natural-language", "en-us"),
        });

        custom.Validate(response);
        custom.CustomResponseHookExecuted.Should().BeTrue();
    }

    [TestMethod]
    public void EnumerateNamedCollections_FindsCollectionsAndOutOfBand()
    {
        var attrs = new List<IppAttribute>
        {
            new(Tag.BegCollection, "media-col", NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "media-type"),
            new(Tag.Keyword, string.Empty, "stationery"),
            new(Tag.EndCollection, string.Empty, NoValue.Instance),
            new(Tag.NoValue, "media-col", NoValue.Instance),
        };

        var collections = attrs.EnumerateNamedCollections("media-col").ToList();
        collections.Should().HaveCount(2);
        collections[0].Should().HaveCount(4);
        collections[1].Should().HaveCount(1);
    }
}
