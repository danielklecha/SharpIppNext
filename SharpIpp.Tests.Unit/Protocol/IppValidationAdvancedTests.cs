using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Exceptions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Protocol;

[TestClass]
public class IppValidationAdvancedTests
{
    private static IppRequestMessage CreateValidRequest()
    {
        var request = new IppRequestMessage
        {
            IppOperation = IppOperation.GetPrinterAttributes,
            RequestId = 42,
            Version = new IppVersion(1, 1),
        };

        request.OperationAttributes.AddRange(
        [
            new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en-us"),
            new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "ipp://localhost:631/printers/printer1"),
        ]);

        return request;
    }

    private static IppResponseMessage CreateValidResponse()
    {
        var response = new IppResponseMessage
        {
            RequestId = 42,
            Version = new IppVersion(1, 1),
            StatusCode = IppStatusCode.SuccessfulOk,
        };

        response.OperationAttributes.Add(
        [
            new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en-us"),
        ]);

        return response;
    }

    #region Symmetric Response Operation Rules Tests

    [TestMethod]
    public void Response_Validate_WhenUnsupportedStatusAndNoUnsupportedGroup_ThrowsException()
    {
        var validator = IppResponseMessageValidator.Default;
        var response = CreateValidResponse();
        response.StatusCode = IppStatusCode.ClientErrorAttributesOrValuesNotSupported;

        Action act = () => validator.Validate(response);

        act.Should().Throw<IppResponseException>()
            .WithMessage("*unsupported-attributes*");
    }

    [TestMethod]
    public void Response_Validate_WhenUnsupportedStatusAndHasUnsupportedGroup_Succeeds()
    {
        var validator = IppResponseMessageValidator.Default;
        var response = CreateValidResponse();
        response.StatusCode = IppStatusCode.ClientErrorAttributesOrValuesNotSupported;
        response.UnsupportedAttributes.Add([new IppAttribute(Tag.Keyword, "media", "custom_super_size")]);

        Action act = () => validator.Validate(response);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Response_Validate_WithPrintJob_WhenMissingJobIdAndUri_ThrowsException()
    {
        var validator = IppResponseMessageValidator.Default;
        var response = CreateValidResponse();
        response.StatusCode = IppStatusCode.SuccessfulOk;

        Action act = () => validator.Validate(response, IppOperation.PrintJob);

        act.Should().Throw<IppResponseException>()
            .WithMessage("*job-id*job-uri*");
    }

    [TestMethod]
    public void Response_Validate_WithPrintJob_WhenMissingJobState_ThrowsException()
    {
        var validator = IppResponseMessageValidator.Default;
        var response = CreateValidResponse();
        response.StatusCode = IppStatusCode.SuccessfulOk;
        response.JobAttributes.Add([new IppAttribute(Tag.Integer, IppAttributeNames.JobId, 123)]);

        Action act = () => validator.Validate(response, IppOperation.PrintJob);

        act.Should().Throw<IppResponseException>()
            .WithMessage("*job-state*");
    }

    [TestMethod]
    public void Response_Validate_WithPrintJob_WhenHasJobIdAndState_Succeeds()
    {
        var validator = IppResponseMessageValidator.Default;
        var response = CreateValidResponse();
        response.StatusCode = IppStatusCode.SuccessfulOk;
        response.JobAttributes.Add(
        [
            new IppAttribute(Tag.Integer, IppAttributeNames.JobId, 123),
            new IppAttribute(Tag.Enum, IppAttributeNames.JobState, 3), // Pending
        ]);

        Action act = () => validator.Validate(response, IppOperation.PrintJob);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Response_Validate_WithGetPrinterAttributes_WhenMissingPrinterGroup_ThrowsException()
    {
        var validator = IppResponseMessageValidator.Default;
        var response = CreateValidResponse();
        response.StatusCode = IppStatusCode.SuccessfulOk;

        Action act = () => validator.Validate(response, IppOperation.GetPrinterAttributes);

        act.Should().Throw<IppResponseException>()
            .WithMessage("*printer-attributes*");
    }

    [TestMethod]
    public void Response_Validate_WithGetPrinterAttributes_WhenHasPrinterGroup_Succeeds()
    {
        var validator = IppResponseMessageValidator.Default;
        var response = CreateValidResponse();
        response.StatusCode = IppStatusCode.SuccessfulOk;
        response.PrinterAttributes.Add(
        [
            new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.PrinterName, "My Printer"),
        ]);

        Action act = () => validator.Validate(response, IppOperation.GetPrinterAttributes);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Response_Validate_WithGetJobAttributes_WhenMissingJobGroup_ThrowsException()
    {
        var validator = IppResponseMessageValidator.Default;
        var response = CreateValidResponse();
        response.StatusCode = IppStatusCode.SuccessfulOk;

        Action act = () => validator.Validate(response, IppOperation.GetJobAttributes);

        act.Should().Throw<IppResponseException>()
            .WithMessage("*job-attributes*");
    }

    [TestMethod]
    public void Response_Validate_WithGetJobAttributes_WhenHasJobGroup_Succeeds()
    {
        var validator = IppResponseMessageValidator.Default;
        var response = CreateValidResponse();
        response.StatusCode = IppStatusCode.SuccessfulOk;
        response.JobAttributes.Add(
        [
            new IppAttribute(Tag.Integer, IppAttributeNames.JobId, 123),
        ]);

        Action act = () => validator.Validate(response, IppOperation.GetJobAttributes);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Response_Validate_WithGetJobAttributes_WhenGroupValidationDisabled_DoesNotThrow()
    {
        var validator = new IppResponseMessageValidator { ValidateJobAttributesGroup = false };
        var response = CreateValidResponse();
        response.StatusCode = IppStatusCode.SuccessfulOk;

        Action act = () => validator.Validate(response, IppOperation.GetJobAttributes);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Response_Validate_WithGetSubscriptionAttributes_WhenMissingSubscriptionGroup_ThrowsException()
    {
        var validator = IppResponseMessageValidator.Default;
        var response = CreateValidResponse();
        response.StatusCode = IppStatusCode.SuccessfulOk;

        Action act = () => validator.Validate(response, IppOperation.GetSubscriptionAttributes);

        act.Should().Throw<IppResponseException>()
            .WithMessage("*subscription-attributes*");
    }

    [TestMethod]
    public void Response_Validate_WithGetSubscriptionAttributes_WhenHasSubscriptionGroup_Succeeds()
    {
        var validator = IppResponseMessageValidator.Default;
        var response = CreateValidResponse();
        response.StatusCode = IppStatusCode.SuccessfulOk;
        response.SubscriptionAttributes.Add(
        [
            new IppAttribute(Tag.Integer, IppAttributeNames.NotifySubscriptionId, 456),
        ]);

        Action act = () => validator.Validate(response, IppOperation.GetSubscriptionAttributes);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Response_Validate_WithGetSubscriptionAttributes_WhenGroupValidationDisabled_DoesNotThrow()
    {
        var validator = new IppResponseMessageValidator { ValidateSubscriptionAttributesGroup = false };
        var response = CreateValidResponse();
        response.StatusCode = IppStatusCode.SuccessfulOk;

        Action act = () => validator.Validate(response, IppOperation.GetSubscriptionAttributes);

        act.Should().NotThrow();
    }

    #endregion

    [TestMethod]
    public void Request_Validate_WhenJobAttributesDisabled_DuplicateJobAttributes_DoesNotThrow()
    {
        var validator = new IppRequestMessageValidator { ValidateJobAttributesGroup = false };
        var request = CreateValidRequest();
        request.JobAttributes.AddRange(
        [
            new IppAttribute(Tag.Integer, "job-id", 1),
            new IppAttribute(Tag.Keyword, "other", "val"),
            new IppAttribute(Tag.Integer, "job-id", 2),
        ]);

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Request_Validate_WhenOperationAttributesDisabled_DuplicateOperationAttributes_DoesNotThrow()
    {
        var validator = new IppRequestMessageValidator { ValidateOperationAttributesGroup = false };
        var request = CreateValidRequest();
        request.OperationAttributes.AddRange(
        [
            new IppAttribute(Tag.Keyword, "my-attr", "1"),
            new IppAttribute(Tag.Keyword, "other", "val"),
            new IppAttribute(Tag.Keyword, "my-attr", "2"),
        ]);

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Response_Validate_WhenOperationAttributesDisabled_DuplicateOperationAttributes_DoesNotThrow()
    {
        var validator = new IppResponseMessageValidator { ValidateOperationAttributesGroup = false };
        var response = CreateValidResponse();
        response.OperationAttributes[0].AddRange(
        [
            new IppAttribute(Tag.Keyword, "my-attr", "1"),
            new IppAttribute(Tag.Keyword, "other", "val"),
            new IppAttribute(Tag.Keyword, "my-attr", "2"),
        ]);

        Action act = () => validator.Validate(response);

        act.Should().NotThrow();
    }

    #region Fast-Path Encoding Helper Tests

    [TestMethod]
    [DataRow("utf-8")]
    [DataRow("UTF-8")]
    [DataRow("utf8")]
    [DataRow("UTF8")]
    [DataRow("  utf-8  ")]
    public void IppEncodingHelper_Utf8FastPath_ReturnsUtf8Encoding(string charset)
    {
        var encoding = IppEncodingHelper.GetEncoding(charset);
        encoding.Should().Be(Encoding.UTF8);
    }

    [TestMethod]
    [DataRow("us-ascii")]
    [DataRow("US-ASCII")]
    [DataRow("ascii")]
    [DataRow("ASCII")]
    public void IppEncodingHelper_AsciiFastPath_ReturnsAsciiEncoding(string charset)
    {
        var encoding = IppEncodingHelper.GetEncoding(charset);
        encoding.Should().Be(Encoding.ASCII);
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void IppEncodingHelper_NullOrEmpty_ReturnsUtf8Fallback(string? charset)
    {
        var encoding = IppEncodingHelper.GetEncoding(charset);
        encoding.Should().Be(Encoding.UTF8);
    }

    [TestMethod]
    public void IppEncodingHelper_InvalidCharset_ReturnsUtf8FallbackWithoutThrowing()
    {
        var encoding = IppEncodingHelper.GetEncoding("this-is-not-a-valid-charset-123456");
        encoding.Should().Be(Encoding.UTF8);
    }

    #endregion
}

