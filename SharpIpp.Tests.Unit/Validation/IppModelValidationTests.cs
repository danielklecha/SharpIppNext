using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;
using SharpIpp.Validation;
using System;
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

    [TestMethod]
    public void GeneratedModelValidator_TryValidate_WhenValidModel_ReturnsTrueAndNoErrors()
    {
        var request = new PrintJobRequest
        {
            RequestId = 1,
            OperationAttributes = new PrintJobOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/"),
                JobName = "Test"
            },
            JobTemplateAttributes = new JobTemplateAttributes
            {
                Copies = 2
            }
        };

        var results = new System.Collections.Generic.List<string>();
        var visited = new System.Collections.Generic.HashSet<object>();
        var handled = GeneratedModelValidator.TryValidate(request, System.Text.Encoding.UTF8, results, visited);

        handled.Should().BeTrue();
        results.Should().BeEmpty();
    }

    [TestMethod]
    public void GeneratedModelValidator_TryValidate_WhenInvalidNestedModel_ReturnsTrueAndCollectsErrors()
    {
        var request = new PrintJobRequest
        {
            RequestId = 0, // Invalid [1, int.MaxValue]
            OperationAttributes = new PrintJobOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/"),
                JobImpressions = -1 // Invalid [0, int.MaxValue]
            },
            JobTemplateAttributes = new JobTemplateAttributes
            {
                Copies = 0 // Invalid [1, int.MaxValue]
            }
        };

        var results = new System.Collections.Generic.List<string>();
        var visited = new System.Collections.Generic.HashSet<object>();
        var handled = GeneratedModelValidator.TryValidate(request, System.Text.Encoding.UTF8, results, visited);

        handled.Should().BeTrue();
        results.Should().HaveCount(3);
        results.Should().Contain(r => r.Contains("RequestId"));
        results.Should().Contain(r => r.Contains("JobImpressions"));
        results.Should().Contain(r => r.Contains("Copies"));
    }

    [TestMethod]
    public void GeneratedModelValidator_TryValidate_WhenUnknownType_ReturnsFalse()
    {
        var unknownObj = new { Name = "Unknown" };
        var results = new System.Collections.Generic.List<string>();
        var visited = new System.Collections.Generic.HashSet<object>();

        var handled = GeneratedModelValidator.TryValidate(unknownObj, System.Text.Encoding.UTF8, results, visited);
        handled.Should().BeFalse();
    }

    [TestMethod]
    public void IppModelValidator_Validate_WhenNull_ReturnsImmediately()
    {
        Action act = () => IppModelValidator.Validate(null!, System.Text.Encoding.UTF8);
        act.Should().NotThrow();
    }

    [TestMethod]
    public void IppModelValidator_ValidateRecursiveFallback_WhenAlreadyVisited_ReturnsImmediately()
    {
        var obj = new object();
        var visited = new System.Collections.Generic.HashSet<object> { obj };
        var results = new System.Collections.Generic.List<string>();

        IppModelValidator.ValidateRecursiveFallback(obj, System.Text.Encoding.UTF8, results, visited);

        results.Should().BeEmpty();
    }

    private class FallbackEnumerableContainer
    {
        public System.Collections.Generic.List<object?> Items { get; set; } = new();
    }

    [TestMethod]
    public void IppModelValidator_Validate_WhenFallbackObjectContainsEnumerableWithSharpIppType_RecurseFallback()
    {
        var container = new FallbackEnumerableContainer
        {
            Items = new System.Collections.Generic.List<object?>
            {
                null,
                new object(),
                new ExtendedValue(1, Array.Empty<byte>())
            }
        };

        Action act = () => IppModelValidator.Validate(container, System.Text.Encoding.UTF8);
        act.Should().NotThrow();
    }

    private class FallbackObjectWithValidation
    {
        [ByteRange(1, 10)]
        public string? Value { get; set; }
    }

    [TestMethod]
    public void IppModelValidator_Validate_WhenFallbackObjectFailsValidation_ThrowsValidationException()
    {
        var obj = new FallbackObjectWithValidation { Value = "Toolongstringvalue" };
        Action act = () => IppModelValidator.Validate(obj, System.Text.Encoding.UTF8);
        act.Should().Throw<ValidationException>().WithMessage("*Value*");
    }
}
