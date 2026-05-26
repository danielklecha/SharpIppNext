using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Exceptions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;
using Moq;

namespace SharpIpp.Tests.Unit.Protocol;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppResponseMessageValidatorTests
{
    private static IppResponseMessage CreateBasicResponse()
    {
        var response = new IppResponseMessage
        {
            RequestId = 123,
            Version = new IppVersion(1, 1),
            StatusCode = IppStatusCode.SuccessfulOk
        };

        response.OperationAttributes.Add(
        [
            new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en")
        ]);

        return response;
    }

    [TestMethod]
    public void Default_ShouldReturnNewValidatorEachTime()
    {
        var first = IppResponseMessageValidator.Default;
        var second = IppResponseMessageValidator.Default;

        first.Should().NotBeSameAs(second);
    }

    [TestMethod]
    public void Default_ShouldUseSafeSettings()
    {
        var validator = IppResponseMessageValidator.Default;

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
    }

    [TestMethod]
    public void Validate_WithValidResponse_ShouldNotThrow()
    {
        var validator = IppResponseMessageValidator.Default;
        var response = CreateBasicResponse();

        Action act = () => validator.Validate(response);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_WithNullResponse_ShouldThrowArgumentNullException()
    {
        var validator = IppResponseMessageValidator.Default;

        Action act = () => validator.Validate(null);

        act.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    public void Validate_WithInvalidRequestId_ShouldThrow(int requestId)
    {
        var validator = IppResponseMessageValidator.Default;
        var response = CreateBasicResponse();
        response.RequestId = requestId;

        Action act = () => validator.Validate(response);

        act.Should().Throw<IppResponseException>()
            .WithMessage("Bad request-id value");
    }

    [TestMethod]
    public void Validate_WithInvalidVersion_ShouldThrow()
    {
        var validator = IppResponseMessageValidator.Default;
        var response = CreateBasicResponse();
        response.Version = new IppVersion(0, 9);

        Action act = () => validator.Validate(response);

        act.Should().Throw<IppResponseException>()
            .WithMessage("Unsupported IPP version");
    }

    [TestMethod]
    public void Validate_WithDuplicateAttributesInSameGroup_ShouldThrow()
    {
        var validator = IppResponseMessageValidator.Default;
        var response = CreateBasicResponse();
        response.JobAttributes.Add(
        [
            new IppAttribute(Tag.Integer, "job-id", 1),
            new IppAttribute(Tag.Keyword, "some-other-attribute", "val"),
            new IppAttribute(Tag.Integer, "job-id", 2)
        ]);

        Action act = () => validator.Validate(response);

        act.Should().Throw<IppResponseException>()
            .WithMessage("Duplicate attribute 'job-id' in group 'job-attributes'");
    }

    [TestMethod]
    public void Validate_WhenDuplicateValidationDisabled_ShouldNotThrow()
    {
        var validator = IppResponseMessageValidator.Default;
        validator.ValidateJobAttributesGroup = false;
        var response = CreateBasicResponse();
        response.JobAttributes.Add(
        [
            new IppAttribute(Tag.Integer, "job-id", 1),
            new IppAttribute(Tag.Keyword, "some-other-attribute", "val"),
            new IppAttribute(Tag.Integer, "job-id", 2)
        ]);

        Action act = () => validator.Validate(response);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_WithEmptyOperationAttributes_ShouldThrow()
    {
        var validator = IppResponseMessageValidator.Default;
        var response = CreateBasicResponse();
        response.OperationAttributes.Clear();

        Action act = () => validator.Validate(response);

        act.Should().Throw<IppResponseException>()
            .WithMessage("No Operation Attributes");
    }

    [TestMethod]
    public void Validate_WithOperationAttributesFirstNotCharset_ShouldThrow()
    {
        var validator = IppResponseMessageValidator.Default;
        var response = CreateBasicResponse();
        response.OperationAttributes.Clear();
        response.OperationAttributes.Add(
        [
            new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en")
        ]);

        Action act = () => validator.Validate(response);

        act.Should().Throw<IppResponseException>()
            .WithMessage("attributes-charset MUST be the first attribute");
    }

    [TestMethod]
    [DataRow("iso-8859-1")]
    [DataRow("ascii")]
    public void Validate_WithOperationAttributesCharsetNotUtf8_ShouldThrow(string charset)
    {
        var validator = IppResponseMessageValidator.Default;
        var response = CreateBasicResponse();
        response.OperationAttributes.Clear();
        response.OperationAttributes.Add(
        [
            new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, charset),
            new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en")
        ]);

        Action act = () => validator.Validate(response);

        act.Should().Throw<IppResponseException>()
            .WithMessage("attributes-charset MUST be 'utf-8'");
    }

    [TestMethod]
    public void Validate_WithOperationAttributesSecondNotNaturalLanguage_ShouldThrow()
    {
        var validator = IppResponseMessageValidator.Default;
        var response = CreateBasicResponse();
        response.OperationAttributes.Clear();
        response.OperationAttributes.Add(
        [
            new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
            new IppAttribute(Tag.Keyword, "requested-attributes", "all")
        ]);

        Action act = () => validator.Validate(response);

        act.Should().Throw<IppResponseException>()
            .WithMessage("attributes-natural-language MUST be the second attribute");
    }

    [TestMethod]
    public void Validate_WhenOperationAttributesDisabled_ShouldNotValidateOperationAttributes()
    {
        var validator = IppResponseMessageValidator.Default;
        validator.ValidateOperationAttributesGroup = false;
        var response = CreateBasicResponse();
        response.OperationAttributes.Clear();

        Action act = () => validator.Validate(response);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_WithCharsetValueNull_ShouldThrow()
    {
        var validator = IppResponseMessageValidator.Default;
        var response = CreateBasicResponse();
        response.OperationAttributes.Clear();
        response.OperationAttributes.Add(
        [
            CreateIppAttributeWithNullValue(Tag.Charset, IppAttributeNames.AttributesCharset),
            new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en")
        ]);

        Action act = () => validator.Validate(response);

        act.Should().Throw<IppResponseException>()
            .WithMessage("attributes-charset MUST be 'utf-8'");
    }

    [TestMethod]
    public void Validate_WithOnlyCharsetAttribute_ShouldThrow()
    {
        var validator = IppResponseMessageValidator.Default;
        var response = CreateBasicResponse();
        response.OperationAttributes.Clear();
        response.OperationAttributes.Add(
        [
            new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8")
        ]);

        Action act = () => validator.Validate(response);

        act.Should().Throw<IppResponseException>()
            .WithMessage("attributes-natural-language MUST be the second attribute");
    }

    [TestMethod]
    public void Validate_WithNullAttributeGroupList_ShouldNotThrow()
    {
        var validator = IppResponseMessageValidator.Default;
        var mockResponse = new Mock<IIppResponseMessage>();
        mockResponse.Setup(x => x.RequestId).Returns(1);
        mockResponse.Setup(x => x.Version).Returns(new IppVersion(1, 1));
        var opAttrs = new List<List<IppAttribute>>
        {
            new List<IppAttribute>
            {
                new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
                new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en")
            }
        };
        mockResponse.Setup(x => x.OperationAttributes).Returns(opAttrs);

        Action act = () => validator.Validate(mockResponse.Object);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_WithEmptyOrNullAttributeName_ShouldIgnoreItAndNotThrow()
    {
        var validator = IppResponseMessageValidator.Default;
        var response = CreateBasicResponse();
        response.JobAttributes.Add(
        [
            new IppAttribute(Tag.Integer, "", 1),
            CreateIppAttributeWithNullName(Tag.Integer, 2),
            new IppAttribute(Tag.Integer, "job-id", 3)
        ]);

        Action act = () => validator.Validate(response);

        act.Should().NotThrow();
    }

    private IppAttribute CreateIppAttributeWithNullValue(Tag tag, string name)
    {
        object attr = new IppAttribute(tag, name, "");
        var field = typeof(IppAttribute).GetField("<Value>k__BackingField", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        field!.SetValue(attr, null);
        return (IppAttribute)attr;
    }

    private IppAttribute CreateIppAttributeWithNullName(Tag tag, object value)
    {
        object attr = new IppAttribute(tag, "dummy", "dummy");
        var nameField = typeof(IppAttribute).GetField("<Name>k__BackingField", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        nameField!.SetValue(attr, null);
        var valueField = typeof(IppAttribute).GetField("<Value>k__BackingField", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        valueField!.SetValue(attr, value);
        return (IppAttribute)attr;
    }
}
