using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;
using SharpIpp.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Tests.Unit.Validation;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppModelValidationTests
{
    [TestMethod]
    public void Validate_WhenResourceIdIsZero_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new CancelResourceRequest
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new CancelResourceOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/"),
                ResourceId = 0 // Out of range [1, 2147483647]
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*ResourceId*");
    }

    [TestMethod]
    public void Validate_WhenResourceIdIsNegative_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new GetResourceAttributesRequest
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new GetResourceAttributesOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/"),
                ResourceId = -5 // Out of range [1, 2147483647]
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*ResourceId*");
    }

    [TestMethod]
    public void Validate_WhenResourceIdIsValid_DoesNotThrow()
    {
        var validator = IppRequestValidator.Default;
        var request = new CancelResourceRequest
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new CancelResourceOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/"),
                ResourceId = 12345
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_WhenNotifyLeaseDurationDefaultIsNegative_ThrowsValidationException()
    {
        var validator = IppResponseValidator.Default;
        var response = new GetSystemAttributesResponse
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            StatusCode = IppStatusCode.SuccessfulOk,
            SystemDescriptionAttributes = new SystemDescriptionAttributes
            {
                NotifyLeaseDurationDefault = -1 // Out of range [0, 67108863]
            }
        };

        Action act = () => validator.Validate(response);
        act.Should().Throw<ValidationException>().WithMessage("*NotifyLeaseDurationDefault*");
    }

    [TestMethod]
    public void Validate_WhenNotifyLeaseDurationDefaultExceedsMax_ThrowsValidationException()
    {
        var validator = IppResponseValidator.Default;
        var response = new GetSystemAttributesResponse
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            StatusCode = IppStatusCode.SuccessfulOk,
            SystemDescriptionAttributes = new SystemDescriptionAttributes
            {
                NotifyLeaseDurationDefault = 67108864 // Out of range [0, 67108863]
            }
        };

        Action act = () => validator.Validate(response);
        act.Should().Throw<ValidationException>().WithMessage("*NotifyLeaseDurationDefault*");
    }

    [TestMethod]
    public void Validate_WhenNotifyLeaseDurationDefaultIsValid_DoesNotThrow()
    {
        var validator = IppResponseValidator.Default;
        var response = new GetSystemAttributesResponse
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            StatusCode = IppStatusCode.SuccessfulOk,
            SystemDescriptionAttributes = new SystemDescriptionAttributes
            {
                NotifyLeaseDurationDefault = 3600 // Valid
            }
        };

        Action act = () => validator.Validate(response);
        act.Should().NotThrow();
    }
}
