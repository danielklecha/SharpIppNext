using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
public class IppResponseValidatorTests
{
    [TestMethod]
    public void Validate_WhenResponseIsValid_DoesNotThrow()
    {
        var validator = IppResponseValidator.Default;
        var response = new GetPrinterAttributesResponse
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            StatusCode = IppStatusCode.SuccessfulOk
        };

        Action act = () => validator.Validate(response);
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_WhenRequestIdIsInvalid_ThrowsValidationException()
    {
        var validator = IppResponseValidator.Default;
        var response = new GetPrinterAttributesResponse
        {
            Version = new IppVersion(2, 0),
            RequestId = 0, // Out of range [1, int.MaxValue]
            StatusCode = IppStatusCode.SuccessfulOk
        };

        Action act = () => validator.Validate(response);
        act.Should().Throw<ValidationException>().WithMessage("*RequestId*");
    }

    [TestMethod]
    public void Validate_WhenDocumentDataGetIntervalIsInvalid_ThrowsValidationException()
    {
        var validator = IppResponseValidator.Default;
        var response = new GetNextDocumentDataResponse
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            StatusCode = IppStatusCode.SuccessfulOk,
            OperationAttributes = new GetNextDocumentDataResponseOperationAttributes
            {
                DocumentDataGetInterval = -1 // Out of range [0, int.MaxValue]
            }
        };

        Action act = () => validator.Validate(response);
        act.Should().Throw<ValidationException>().WithMessage("*DocumentDataGetInterval*");
    }

    [TestMethod]
    public void Validate_WhenResponseIsNull_ThrowsArgumentNullException()
    {
        var validator = IppResponseValidator.Default;
        Action act = () => validator.Validate<GetPrinterAttributesResponse>(null!);
        act.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void Validate_WhenCircularReferenceExists_DoesNotRecurseInfinitely()
    {
        var validator = IppResponseValidator.Default;
        var response = new TestCircularResponseModel
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            StatusCode = IppStatusCode.SuccessfulOk,
            JobTemplateAttributes = new JobTemplateAttributes()
        };
        var overrideInstruction = new OverrideInstruction
        {
            JobTemplateAttributes = response.JobTemplateAttributes
        };
        response.JobTemplateAttributes.Overrides = new[] { overrideInstruction };

        Action act = () => validator.Validate(response);
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_WhenCollectionHasNullElements_SkipsNullElements()
    {
        var validator = IppResponseValidator.Default;
        var response = new TestNullCollectionItemResponseModel
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            StatusCode = IppStatusCode.SuccessfulOk,
            Materials = new Material[] { null! }
        };

        Action act = () => validator.Validate(response);
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_WhenModelHasIndexedProperty_SkipsIndexedProperty()
    {
        var validator = IppResponseValidator.Default;
        var response = new TestIndexedPropertyResponseModel
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            StatusCode = IppStatusCode.SuccessfulOk
        };

        Action act = () => validator.Validate(response);
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_WhenByteRangeAttributeCharsetIsInvalid_FallsBackToUtf8()
    {
        var validator = IppResponseValidator.Default;
        var response = new TestByteRangeResponseModel
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            StatusCode = IppStatusCode.SuccessfulOk,
            OperationAttributes = new OperationAttributes { AttributesCharset = "invalid-charset-name" },
            StringValue = "abcdef"
        };

        Action act = () => validator.Validate(response);
        act.Should().NotThrow();

        response.StringValue = "abcdefghijk"; // 11 bytes in UTF-8, exceeds 10 bytes limit
        act.Should().Throw<ValidationException>().WithMessage("*StringValue*");
    }

    private class TestCircularResponseModel : IIppResponse
    {
        public IppVersion Version { get; set; }
        public IppStatusCode StatusCode { get; set; }
        public int RequestId { get; set; }
        public OperationAttributes? OperationAttributes { get; set; }
        public JobTemplateAttributes? JobTemplateAttributes { get; set; }
    }

    private class TestNullCollectionItemResponseModel : IIppResponse
    {
        public IppVersion Version { get; set; }
        public IppStatusCode StatusCode { get; set; }
        public int RequestId { get; set; }
        public OperationAttributes? OperationAttributes { get; set; }
        public Material[]? Materials { get; set; }
    }

    private class TestIndexedPropertyResponseModel : IIppResponse
    {
        public IppVersion Version { get; set; }
        public IppStatusCode StatusCode { get; set; }
        public int RequestId { get; set; }
        public OperationAttributes? OperationAttributes { get; set; }
        public string this[int index]
        {
            get => "test";
            set { }
        }
    }

    private class TestByteRangeResponseModel : IIppResponse
    {
        public IppVersion Version { get; set; }
        public IppStatusCode StatusCode { get; set; }
        public int RequestId { get; set; }
        public OperationAttributes? OperationAttributes { get; set; }

        [ByteRange(1, 10)]
        public string? StringValue { get; set; }
    }
}
