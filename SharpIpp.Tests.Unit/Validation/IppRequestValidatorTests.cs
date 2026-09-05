using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using SharpIpp.Validation;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace SharpIpp.Tests.Unit.Validation;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppRequestValidatorTests
{
    [TestMethod]
    public void Validate_WhenRequestIsNull_ThrowsArgumentNullException()
    {
        var validator = IppRequestValidator.Default;
        Action act = () => validator.Validate<PrintJobRequest>(null!);
        act.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void Validate_WhenAttributesAreValid_DoesNotThrow()
    {
        var validator = IppRequestValidator.Default;
        var request = new PrintJobRequest
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new PrintJobOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/")
            },
            JobTemplateAttributes = new JobTemplateAttributes
            {
                JobPriority = 50,
                Copies = 5
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_WhenJobPriorityOutOfRange_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new PrintJobRequest
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new PrintJobOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/")
            },
            JobTemplateAttributes = new JobTemplateAttributes
            {
                JobPriority = 150 // Out of range [1, 100]
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*JobPriority*");
    }

    [TestMethod]
    public void Validate_WhenCopiesOutOfRange_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new PrintJobRequest
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new PrintJobOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/")
            },
            JobTemplateAttributes = new JobTemplateAttributes
            {
                Copies = 0 // Out of range [1, int.MaxValue]
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*Copies*");
    }

    [TestMethod]
    public void Validate_WhenChamberHumidityOutOfRange_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new PrintJobRequest
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new PrintJobOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/")
            },
            JobTemplateAttributes = new JobTemplateAttributes
            {
                ChamberHumidity = -5 // Out of range [0, 100]
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*ChamberHumidity*");
    }

    [TestMethod]
    public void Validate_WhenCUPSGetPrintersLimitOutOfRange_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new CUPSGetPrintersRequest
        {
            OperationAttributes = new CUPSGetPrintersOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/"),
                Limit = 0 // Out of range [1, int.MaxValue]
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*Limit*");
    }

    [TestMethod]
    public void Validate_WhenCUPSGetPrintersPrinterIdOutOfRange_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new CUPSGetPrintersRequest
        {
            OperationAttributes = new CUPSGetPrintersOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/"),
                PrinterId = 70000 // Out of range [1, 65535]
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*PrinterId*");
    }

    [TestMethod]
    public void Validate_WhenGetPrinterAttributesFirstIndexOutOfRange_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new GetPrinterAttributesRequest
        {
            OperationAttributes = new GetPrinterAttributesOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/"),
                FirstIndex = 0 // Out of range [1, int.MaxValue]
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*FirstIndex*");
    }

    [TestMethod]
    public void Validate_WhenMaterialFillDensityOutOfRange_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new CreateJobRequest
        {
            OperationAttributes = new CreateJobOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/")
            },
            JobTemplateAttributes = new JobTemplateAttributes
            {
                MaterialsCol = new[]
                {
                    new Material
                    {
                        MaterialFillDensity = 150 // Out of range [0, 100]
                    }
                }
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*MaterialFillDensity*");
    }

    [TestMethod]
    public void Validate_WhenMaterialAmountOutOfRange_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new CreateJobRequest
        {
            OperationAttributes = new CreateJobOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/")
            },
            JobTemplateAttributes = new JobTemplateAttributes
            {
                MaterialsCol = new[]
                {
                    new Material
                    {
                        MaterialAmount = -1 // Out of range [0, int.MaxValue]
                    }
                }
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*MaterialAmount*");
    }

    [TestMethod]
    public void Validate_WhenMaterialDiameterOutOfRange_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new CreateJobRequest
        {
            OperationAttributes = new CreateJobOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/")
            },
            JobTemplateAttributes = new JobTemplateAttributes
            {
                MaterialsCol = new[]
                {
                    new Material
                    {
                        MaterialDiameter = -5 // Out of range [0, int.MaxValue]
                    }
                }
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*MaterialDiameter*");
    }

    [TestMethod]
    public void Validate_WhenMaterialRateOutOfRange_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new CreateJobRequest
        {
            OperationAttributes = new CreateJobOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/")
            },
            JobTemplateAttributes = new JobTemplateAttributes
            {
                MaterialsCol = new[]
                {
                    new Material
                    {
                        MaterialRate = 0 // Out of range [1, int.MaxValue]
                    }
                }
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*MaterialRate*");
    }

    [TestMethod]
    public void Validate_WhenMaterialShellThicknessOutOfRange_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new CreateJobRequest
        {
            OperationAttributes = new CreateJobOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/")
            },
            JobTemplateAttributes = new JobTemplateAttributes
            {
                MaterialsCol = new[]
                {
                    new Material
                    {
                        MaterialShellThickness = -10 // Out of range [0, int.MaxValue]
                    }
                }
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*MaterialShellThickness*");
    }

    [TestMethod]
    public void Validate_WhenMaterialTemperatureOutOfRange_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new CreateJobRequest
        {
            OperationAttributes = new CreateJobOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/")
            },
            JobTemplateAttributes = new JobTemplateAttributes
            {
                MaterialsCol = new[]
                {
                    new Material
                    {
                        MaterialTemperature = -300 // Out of range [-273, int.MaxValue]
                    }
                }
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*MaterialTemperature*");
    }

    [TestMethod]
    public void Validate_WhenOutputAttributesNoiseRemovalOutOfRange_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new CreateJobRequest
        {
            OperationAttributes = new CreateJobOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/"),
                OutputAttributes = new OutputAttributes
                {
                    NoiseRemoval = 101 // Out of range [0, 100]
                }
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*NoiseRemoval*");
    }

    [TestMethod]
    public void Validate_WhenRangeAttributeHasMultipleRanges_WithValidValues_DoesNotThrow()
    {
        var validator = IppRequestValidator.Default;
        var request = new TestMultiRangeModel { Value = null };
        Action act = () => validator.Validate(request);
        act.Should().NotThrow();

        request.Value = 0;
        act.Should().NotThrow();

        request.Value = 100;
        act.Should().NotThrow();

        request.Value = 200;
        act.Should().NotThrow();

        request.Value = 255;
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_WhenRangeAttributeHasMultipleRanges_WithInvalidValues_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new TestMultiRangeModel { Value = 50 };
        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*Value*");

        request.Value = 256;
        act.Should().Throw<ValidationException>().WithMessage("*Value*");

        request.Value = -1;
        act.Should().Throw<ValidationException>().WithMessage("*Value*");
    }

    [TestMethod]
    public void Validate_WhenJobPasswordSupportedIsValid_DoesNotThrow()
    {
        var validator = IppRequestValidator.Default;
        var request = new SetPrinterAttributesRequest
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new SetPrinterAttributesOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/")
            },
            PrinterAttributes = new PrinterDescriptionAttributes
            {
                JobPasswordSupported = 128
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_WhenJobPasswordSupportedIsInvalid_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new SetPrinterAttributesRequest
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new SetPrinterAttributesOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/")
            },
            PrinterAttributes = new PrinterDescriptionAttributes
            {
                JobPasswordSupported = 256
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*JobPasswordSupported*");
    }

    [TestMethod]
    public void Validate_WhenDocumentPasswordSupportedIsValid_DoesNotThrow()
    {
        var validator = IppRequestValidator.Default;
        var request = new SetPrinterAttributesRequest
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new SetPrinterAttributesOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/")
            },
            PrinterAttributes = new PrinterDescriptionAttributes
            {
                DocumentPasswordSupported = 512
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().NotThrow();

        request.PrinterAttributes.DocumentPasswordSupported = 0;
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_WhenDocumentPasswordSupportedIsInvalid_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new SetPrinterAttributesRequest
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new SetPrinterAttributesOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/")
            },
            PrinterAttributes = new PrinterDescriptionAttributes
            {
                DocumentPasswordSupported = 100
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*DocumentPasswordSupported*");
    }

    private class TestMultiRangeModel : IIppRequest
    {
        [Range(0, 0, 100, 255)]
        public int? Value { get; set; }
        public IppVersion Version { get; set; }
        public int RequestId { get; set; }
        public OperationAttributes? OperationAttributes { get; }
    }

    [TestMethod]
    public void Validate_WhenCircularReferenceExists_DoesNotRecurseInfinitely()
    {
        var validator = IppRequestValidator.Default;
        var request = new PrintJobRequest
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new PrintJobOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/")
            },
            JobTemplateAttributes = new JobTemplateAttributes()
        };
        var overrideInstruction = new OverrideInstruction
        {
            JobTemplateAttributes = request.JobTemplateAttributes
        };
        request.JobTemplateAttributes.Overrides = new[] { overrideInstruction };

        Action act = () => validator.Validate(request);
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_WhenCollectionHasNullElements_SkipsNullElements()
    {
        var validator = IppRequestValidator.Default;
        var request = new PrintJobRequest
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new PrintJobOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/")
            },
            JobTemplateAttributes = new JobTemplateAttributes
            {
                MaterialsCol = new Material[] { null! }
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_WhenModelHasIndexedProperty_SkipsIndexedProperty()
    {
        var validator = IppRequestValidator.Default;
        var request = new TestIndexedPropertyModel
        {
            Version = new IppVersion(2, 0),
            RequestId = 123
        };

        Action act = () => validator.Validate(request);
        act.Should().NotThrow();
    }

    private class TestIndexedPropertyModel : IIppRequest
    {
        public IppVersion Version { get; set; }
        public int RequestId { get; set; }
        public OperationAttributes? OperationAttributes { get; }
        public string this[int index]
        {
            get => "test";
            set { }
        }
    }

    [TestMethod]
    public void Validate_WhenByteRangeAttributeIsValid_DoesNotThrow()
    {
        var validator = IppRequestValidator.Default;
        var request = new TestByteRangeModel
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new OperationAttributes { AttributesCharset = (SharpIpp.Protocol.Models.Charset)"utf-8" },
            StringValue = "hello",
            StringWithLanguageValue = new StringWithLanguage("en", "test"),
            OctetStringValue = new OctetString("hello"),
            BytesValue = new byte[] { 1, 2, 3 },
            StringArrayValue = new[] { "abc", "def" }
        };

        Action act = () => validator.Validate(request);
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_WhenByteRangeAttributeIsInvalid_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new TestByteRangeModel
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new OperationAttributes { AttributesCharset = (SharpIpp.Protocol.Models.Charset)"utf-8" },
            StringValue = "abcdefghijk", // 11 bytes, exceeds max 10
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*StringValue*");

        request.StringValue = "hello";
        request.StringWithLanguageValue = new StringWithLanguage("en", "longertextstring"); // 2 + 16 + 4 = 22 bytes, exceeds max 15
        act.Should().Throw<ValidationException>().WithMessage("*StringWithLanguageValue*");

        request.StringWithLanguageValue = null;
        request.OctetStringValue = new OctetString("abcdefghijk"); // 11 bytes, exceeds max 10
        act.Should().Throw<ValidationException>().WithMessage("*OctetStringValue*");

        request.OctetStringValue = null;
        request.BytesValue = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }; // 11 bytes, exceeds max 10
        act.Should().Throw<ValidationException>().WithMessage("*BytesValue*");

        request.BytesValue = null;
        request.StringArrayValue = new[] { "abc", "abcdefghijk" }; // elements exceeds max 10
        act.Should().Throw<ValidationException>().WithMessage("*StringArrayValue*");
    }

    [TestMethod]
    public void Validate_WhenByteRangeAttributeRespectsCharset_ThrowsOrDoesNotThrow()
    {
        var validator = IppRequestValidator.Default;
        
        // "abcdef" is 6 bytes in UTF-8, but 12 bytes in UTF-16.
        // StringValue has [ByteRange(1, 10)].
        var request = new TestByteRangeModel
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new OperationAttributes { AttributesCharset = (SharpIpp.Protocol.Models.Charset)"utf-16" },
            StringValue = "abcdef" // 12 bytes in UTF-16, exceeds 10 bytes limit
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*StringValue*");

        request.OperationAttributes.AttributesCharset = (SharpIpp.Protocol.Models.Charset)"utf-8"; // 6 bytes in UTF-8, fits in 10 bytes limit
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_WhenByteRangeAttributeCharsetIsInvalid_FallsBackToUtf8()
    {
        var validator = IppRequestValidator.Default;
        var request = new TestByteRangeModel
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new OperationAttributes { AttributesCharset = (SharpIpp.Protocol.Models.Charset)"invalid-charset-name" },
            StringValue = "abcdef"
        };

        Action act = () => validator.Validate(request);
        act.Should().NotThrow();

        request.StringValue = "abcdefghijk"; // 11 bytes in UTF-8, exceeds 10 bytes limit
        act.Should().Throw<ValidationException>().WithMessage("*StringValue*");
    }

    [TestMethod]
    public void Validate_WhenByteRangeAttributeWithMultipleRangesIsValid_DoesNotThrow()
    {
        var validator = IppRequestValidator.Default;
        var request = new TestByteRangeModel
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new OperationAttributes { AttributesCharset = (SharpIpp.Protocol.Models.Charset)"utf-8" },
            MultiRangeStringValue = "ab" // 2 bytes, falls in [1, 3]
        };

        Action act = () => validator.Validate(request);
        act.Should().NotThrow();

        request.MultiRangeStringValue = "abcdefgh"; // 8 bytes, falls in [7, 10]
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_WhenByteRangeAttributeWithMultipleRangesIsInvalid_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new TestByteRangeModel
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new OperationAttributes { AttributesCharset = (SharpIpp.Protocol.Models.Charset)"utf-8" },
            MultiRangeStringValue = "abcde" // 5 bytes, outside [1, 3] and [7, 10]
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*MultiRangeStringValue*");
    }

    [TestMethod]
    public void ByteRangeAttribute_Constructor_SetsMinimumAndMaximum()
    {
        var attribute = new ByteRangeAttribute(1, 10);
        attribute.Minimum.Should().Be(1);
        attribute.Maximum.Should().Be(10);
    }

    [TestMethod]
    public void ByteRangeAttribute_Constructor_WhenRangesIsNull_ThrowsArgumentNullException()
    {
        Action act = () => new ByteRangeAttribute(null!);
        act.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void ByteRangeAttribute_Constructor_WhenRangesIsEmpty_ThrowsArgumentException()
    {
        Action act = () => new ByteRangeAttribute(Array.Empty<int>());
        act.Should().Throw<ArgumentException>().WithMessage("*even number*");
    }

    [TestMethod]
    public void ByteRangeAttribute_Constructor_WhenRangesHasOddLength_ThrowsArgumentException()
    {
        Action act = () => new ByteRangeAttribute(1, 2, 3);
        act.Should().Throw<ArgumentException>().WithMessage("*even number*");
    }

    [TestMethod]
    public void ByteRangeAttribute_IsValid_WhenValueIsNull_ReturnsSuccess()
    {
        var attribute = new ByteRangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var result = attribute.IsValid(null, context);
        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void ByteRangeAttribute_IsValid_WhenSingleRangeValid_ReturnsSuccess()
    {
        var attribute = new ByteRangeAttribute(1, 5);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var result = attribute.IsValid("abc", context);
        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void ByteRangeAttribute_IsValid_WhenOutOfSingleRange_ReturnsErrorMessage()
    {
        var attribute = new ByteRangeAttribute(1, 5);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var result = attribute.IsValid("abcdef", context);
        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField must be between 1 and 5 bytes.");
    }

    [TestMethod]
    public void ByteRangeAttribute_IsValid_WhenCustomErrorMessageSet_UsesCustomErrorMessage()
    {
        var attribute = new ByteRangeAttribute(1, 5) { ErrorMessage = "Custom error" };
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var result = attribute.IsValid("abcdef", context);
        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("Custom error");
    }

    [TestMethod]
    public void ByteRangeAttribute_IsValid_WhenEnumerableContainsNullItem_SkipsNullAndReturnsSuccess()
    {
        var attribute = new ByteRangeAttribute(1, 5);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var collection = new string?[] { "abc", null, "de" };
        var result = attribute.IsValid(collection, context);
        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void ByteRangeAttribute_IsValid_WhenEnumerableContainsInvalidItem_ReturnsValidationError()
    {
        var attribute = new ByteRangeAttribute(1, 5);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var collection = new string?[] { "abc", "abcdef" };
        var result = attribute.IsValid(collection, context);
        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField must be between 1 and 5 bytes.");
    }

    [TestMethod]
    public void ByteRangeAttribute_IsValid_WhenValueIsUnsupportedType_ReturnsValidationError()
    {
        var attribute = new ByteRangeAttribute(1, 5);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var result = attribute.IsValid(123, context);
        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("Unsupported type for byte length validation: System.Int32");
    }

    [TestMethod]
    public void ByteRangeAttribute_WhenMultiRange_ValidatesCorrectly()
    {
        var attribute = new ByteRangeAttribute(1, 3, 7, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");

        attribute.IsValid("ab", context).Should().Be(ValidationResult.Success);
        attribute.IsValid("abcdefgh", context).Should().Be(ValidationResult.Success);

        var result = attribute.IsValid("abcde", context);
        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField must be within one of the following ranges: 1-3, 7-10 bytes.");
    }

    [TestMethod]
    public void ByteRangeAttribute_GetByteLengths_WhenValueIsNull_YieldsBreak()
    {
        var lengths = TestByteRangeAttributeExposer.CallGetByteLengths(null, Encoding.UTF8).ToList();
        lengths.Should().BeEmpty();
    }

    [TestMethod]
    public void ByteRangeAttribute_GetByteLengths_WhenValueIsStringOrByteArray_YieldsCorrectLength()
    {
        var strLengths = TestByteRangeAttributeExposer.CallGetByteLengths("test", Encoding.UTF8).ToList();
        strLengths.Should().ContainSingle().Which.Should().Be(4);

        var byteLengths = TestByteRangeAttributeExposer.CallGetByteLengths(new byte[] { 1, 2, 3 }, Encoding.UTF8).ToList();
        byteLengths.Should().ContainSingle().Which.Should().Be(3);
    }

    [TestMethod]
    public void ByteRangeAttribute_GetByteLengths_WhenStringWithLanguageLanguageIsNull_CoalescesToEmptyString()
    {
        var swl = new StringWithLanguage(null!, "test");
        var lengths = TestByteRangeAttributeExposer.CallGetByteLengths(swl, Encoding.UTF8).ToList();
        lengths.Should().ContainSingle().Which.Should().Be(0 + 4 + 4);
    }

    [TestMethod]
    public void ByteRangeAttribute_GetByteLengths_WhenStringWithLanguageValueIsNull_CoalescesToEmptyString()
    {
        var swl = new StringWithLanguage("en", null!);
        var lengths = TestByteRangeAttributeExposer.CallGetByteLengths(swl, Encoding.UTF8).ToList();
        lengths.Should().ContainSingle().Which.Should().Be(2 + 0 + 4);
    }

    [TestMethod]
    public void ByteRangeAttribute_GetByteLengths_WhenOctetStringValueIsNull_CoalescesToZero()
    {
        var os = new OctetString((byte[])null!);
        var lengths = TestByteRangeAttributeExposer.CallGetByteLengths(os, Encoding.UTF8).ToList();
        lengths.Should().ContainSingle().Which.Should().Be(0);
    }

    [TestMethod]
    public void ByteRangeAttribute_GetByteLengths_WhenValueIsEnumerable_YieldsLengthsForNonNullItems()
    {
        var items = new object?[] { "hello", null, new object?[] { "world", null } };
        var lengths = TestByteRangeAttributeExposer.CallGetByteLengths(items, Encoding.UTF8).ToList();
        lengths.Should().Equal(5, 5);
    }

    [TestMethod]
    public void ByteRangeAttribute_GetByteLengths_WhenValueIsUnsupportedType_ThrowsArgumentException()
    {
        Action act = () => TestByteRangeAttributeExposer.CallGetByteLengths(123, Encoding.UTF8).ToList();
        act.Should().Throw<ArgumentException>().WithMessage("*Unsupported type for byte length validation*");
    }

    private class TestByteRangeAttributeExposer : ByteRangeAttribute
    {
        public TestByteRangeAttributeExposer() : base(0, 10) { }

        public static IEnumerable<int> CallGetByteLengths(object? value, Encoding encoding)
        {
            return GetByteLengths(value, encoding);
        }
    }

    [TestMethod]
    public void Validate_WhenByteRangeAttributeOnUnsupportedType_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new TestByteRangeModel
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new OperationAttributes { AttributesCharset = (SharpIpp.Protocol.Models.Charset)"utf-8" },
            UnsupportedValue = 123
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*Unsupported type*");
    }

    [TestMethod]
    public void Validate_PrintJobOperationAttributes_WhenDocumentPasswordIsTooLong_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new PrintJobRequest
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new PrintJobOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/"),
                DocumentPassword = new byte[1024] // Exceeds 1023 octets
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*DocumentPassword*");
    }

    [TestMethod]
    public void Validate_PrintJobOperationAttributes_WhenDocumentPasswordIsValid_DoesNotThrow()
    {
        var validator = IppRequestValidator.Default;
        var request = new PrintJobRequest
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new PrintJobOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/"),
                DocumentPassword = new byte[1023] // Exactly 1023 octets
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_SendDocumentOperationAttributes_WhenDocumentPasswordIsTooLong_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new SendDocumentRequest
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new SendDocumentOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/"),
                DocumentPassword = new byte[1024] // Exceeds 1023 octets
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*DocumentPassword*");
    }

    [TestMethod]
    public void Validate_DocumentTemplateAttributes_WhenDocumentPasswordIsTooLong_ThrowsValidationException()
    {
        var validator = IppRequestValidator.Default;
        var request = new SendDocumentRequest
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new SendDocumentOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/")
            },
            DocumentTemplateAttributes = new DocumentTemplateAttributes
            {
                DocumentPassword = new byte[1024] // Exceeds 1023 octets
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*DocumentPassword*");
    }

    private class TestByteRangeModel : IIppRequest
    {
        public IppVersion Version { get; set; }
        public int RequestId { get; set; }
        public OperationAttributes? OperationAttributes { get; set; }

        [ByteRange(1, 10)]
        public string? StringValue { get; set; }

        [ByteRange(1, 3, 7, 10)]
        public string? MultiRangeStringValue { get; set; }

        [ByteRange(1, 15)]
        public StringWithLanguage? StringWithLanguageValue { get; set; }

        [ByteRange(1, 10)]
        public OctetString? OctetStringValue { get; set; }

        [ByteRange(1, 10)]
        public byte[]? BytesValue { get; set; }

        [ByteRange(1, 10)]
        public string[]? StringArrayValue { get; set; }

        [ByteRange(1, 10)]
        public int? UnsupportedValue { get; set; }
    }
}
