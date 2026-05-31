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

    [TestMethod]
    public void Validate_WhenJobImpressionsIsNegative_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new CreateJobRequest
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new CreateJobOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/"),
                JobImpressions = -1
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*JobImpressions*");
    }

    [TestMethod]
    public void Validate_WhenJobMediaSheetsIsZero_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new CreateJobRequest
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new CreateJobOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/"),
                JobMediaSheets = 0
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*JobMediaSheets*");
    }

    [TestMethod]
    public void Validate_WhenJobKOctetsIsNegative_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new CreateJobRequest
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new CreateJobOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/"),
                JobKOctets = -1
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*JobKOctets*");
    }

    [TestMethod]
    public void Validate_WhenJobImpressionsEstimatedIsZero_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new CreateJobRequest
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new CreateJobOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/"),
                JobImpressionsEstimated = 0
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*JobImpressionsEstimated*");
    }

    [TestMethod]
    public void Validate_WhenProofCopiesIsZero_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new CreateJobRequest
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new CreateJobOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/"),
                ProofCopies = 0
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*ProofCopies*");
    }

    [TestMethod]
    public void Validate_WhenResourceKOctetsIsNegative_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new SendResourceDataRequest
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new SendResourceDataOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/"),
                ResourceKOctets = -1
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*ResourceKOctets*");
    }

    [TestMethod]
    public void Validate_WhenNotifyResourceIdIsZero_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new CancelSubscriptionRequest
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new SystemOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/"),
                NotifyResourceId = 0
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*NotifyResourceId*");
    }

    [TestMethod]
    public void Validate_WhenRestartGetIntervalIsNegative_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new CancelSubscriptionRequest
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new SystemOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/"),
                RestartGetInterval = -1
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*RestartGetInterval*");
    }

    [TestMethod]
    public void Validate_WhenNotifySystemUpTimeIsNegative_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new CancelSubscriptionRequest
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new SystemOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/"),
                NotifySystemUpTime = -1
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*NotifySystemUpTime*");
    }

    [TestMethod]
    public void Validate_WhenNotifySubscriptionIdIsZero_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new CancelSubscriptionRequest
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new SystemOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/"),
                NotifySubscriptionId = 0
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*NotifySubscriptionId*");
    }

    [TestMethod]
    public void Validate_WhenAddDocumentImagesJobIdIsZero_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new AddDocumentImagesRequest
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new AddDocumentImagesOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/"),
                JobId = 0
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*JobId*");
    }
}
