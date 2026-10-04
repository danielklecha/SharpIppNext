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
    public void RangeAttribute_WhenMultipleRanges_ValidatesCorrectly()
    {
        var attribute = new RangeAttribute(0, 0, 100, 255);
        var context = new ValidationContext(Encoding.UTF8, "Value");

        attribute.IsValid(null, context).Should().Be(ValidationResult.Success);
        attribute.IsValid(0, context).Should().Be(ValidationResult.Success);
        attribute.IsValid(100, context).Should().Be(ValidationResult.Success);
        attribute.IsValid(200, context).Should().Be(ValidationResult.Success);
        attribute.IsValid(255, context).Should().Be(ValidationResult.Success);

        attribute.IsValid(50, context)!.ErrorMessage.Should().Contain("Value");
        attribute.IsValid(256, context)!.ErrorMessage.Should().Contain("Value");
        attribute.IsValid(-1, context)!.ErrorMessage.Should().Contain("Value");
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
    public void ByteRangeAttribute_WhenValid_ReturnsSuccess()
    {
        var attr = new ByteRangeAttribute(1, 10);
        var ctx = new ValidationContext(Encoding.UTF8, "TestProp");

        attr.IsValid("hello", ctx).Should().Be(ValidationResult.Success);
        attr.IsValid(new StringWithLanguage("en", "test"), ctx).Should().Be(ValidationResult.Success);
        attr.IsValid(new OctetString("hello"), ctx).Should().Be(ValidationResult.Success);
        attr.IsValid(new byte[] { 1, 2, 3 }, ctx).Should().Be(ValidationResult.Success);
        attr.IsValid(new[] { "abc", "def" }, ctx).Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void ByteRangeAttribute_WhenInvalid_ReturnsError()
    {
        var attr = new ByteRangeAttribute(1, 10);
        var ctx = new ValidationContext(Encoding.UTF8, "TestProp");

        attr.IsValid("abcdefghijk", ctx)!.ErrorMessage.Should().Contain("TestProp");
        attr.IsValid(new StringWithLanguage("en", "longertextstring"), ctx)!.ErrorMessage.Should().Contain("TestProp");
        attr.IsValid(new OctetString("abcdefghijk"), ctx)!.ErrorMessage.Should().Contain("TestProp");
        attr.IsValid(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, ctx)!.ErrorMessage.Should().Contain("TestProp");
        attr.IsValid(new[] { "abcdefghijk" }, ctx)!.ErrorMessage.Should().Contain("TestProp");
    }

    [TestMethod]
    public void ByteRangeAttribute_RespectsCharset()
    {
        var attr = new ByteRangeAttribute(1, 10);
        var utf16Ctx = new ValidationContext(Encoding.Unicode, "StringValue");
        var utf8Ctx = new ValidationContext(Encoding.UTF8, "StringValue");

        // "abcdef" is 6 bytes in UTF-8, but 12 bytes in UTF-16
        attr.IsValid("abcdef", utf16Ctx)!.ErrorMessage.Should().Contain("StringValue");
        attr.IsValid("abcdef", utf8Ctx).Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void Validate_WhenAttributesCharsetIsInvalid_FallsBackToUtf8()
    {
        var validator = IppRequestValidator.Default;
        var request = new PrintJobRequest
        {
            Version = new IppVersion(2, 0),
            RequestId = 123,
            OperationAttributes = new PrintJobOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/"),
                AttributesCharset = (Charset)"invalid-charset-name"
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().NotThrow();
    }

    [TestMethod]
    public void ByteRangeAttribute_WhenCharsetIsInvalid_FallsBackToUtf8()
    {
        var attr = new ByteRangeAttribute(1, 10);
        Encoding encoding;
        try
        {
            encoding = Encoding.GetEncoding("invalid-charset-name");
        }
        catch
        {
            encoding = Encoding.UTF8;
        }
        var ctx = new ValidationContext(encoding, "StringValue");

        attr.IsValid("abcdef", ctx).Should().Be(ValidationResult.Success);
        attr.IsValid("abcdefghijk", ctx)!.ErrorMessage.Should().Contain("StringValue");
    }

    [TestMethod]
    public void ByteRangeAttribute_WithMultipleRanges_ValidatesCorrectly()
    {
        var attr = new ByteRangeAttribute(1, 3, 7, 10);
        var ctx = new ValidationContext(Encoding.UTF8, "MultiRange");

        attr.IsValid("ab", ctx).Should().Be(ValidationResult.Success); // 2 bytes in [1, 3]
        attr.IsValid("abcdefgh", ctx).Should().Be(ValidationResult.Success); // 8 bytes in [7, 10]
        attr.IsValid("abcde", ctx)!.ErrorMessage.Should().Contain("MultiRange"); // 5 bytes outside
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
    public void ByteRangeAttribute_IsValid_WhenValueIsIppValueNoValue_ReturnsSuccess()
    {
        var attribute = new ByteRangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        IppValue<string> value = IppValue<string>.NoValue;

        var result = attribute.IsValid(value, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void ByteRangeAttribute_IsValid_WhenValueIsOctetStringNoValue_ReturnsSuccess()
    {
        var attribute = new ByteRangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        OctetString value = default;

        var result = attribute.IsValid(value, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void ByteRangeAttribute_IsValid_WhenValueIsStringWithLanguageNoValue_ReturnsSuccess()
    {
        var attribute = new ByteRangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        StringWithLanguage value = default;

        var result = attribute.IsValid(value, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void ByteRangeAttribute_IsValid_WhenValueIsNoValueInstance_ReturnsSuccess()
    {
        var attribute = new ByteRangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");

        var result = attribute.IsValid(NoValue.Instance, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void ByteRangeAttribute_IsValid_WhenValueIsIppValueWithValidValue_ReturnsSuccess()
    {
        var attribute = new ByteRangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        IppValue<string> value = new("hello");

        var result = attribute.IsValid(value, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void ByteRangeAttribute_IsValid_WhenValueIsIppValueWithInvalidValue_ReturnsValidationError()
    {
        var attribute = new ByteRangeAttribute(1, 3);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        IppValue<string> value = new("hello");

        var result = attribute.IsValid(value, context);

        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField must be between 1 and 3 bytes.");
    }

    [TestMethod]
    public void ByteRangeAttribute_IsValid_WhenValueIsIppValueWithNull_ReturnsSuccess()
    {
        var attribute = new ByteRangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        IppValue<string?> value = new((string?)null);

        var result = attribute.IsValid(value, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void ByteRangeAttribute_IsValid_WhenCollectionContainsIppValueNoValue_ReturnsSuccess()
    {
        var attribute = new ByteRangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var collection = new IIppValue[]
        {
            IppValue<string>.NoValue,
            new IppValue<string>("abc")
        };

        var result = attribute.IsValid(collection, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void ByteRangeAttribute_IsValid_WhenCollectionContainsOctetStringNoValue_ReturnsSuccess()
    {
        var attribute = new ByteRangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var collection = new object[]
        {
            default(OctetString),
            new OctetString(new byte[] { 1, 2, 3 })
        };

        var result = attribute.IsValid(collection, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void ByteRangeAttribute_IsValid_WhenCollectionContainsIppValueWithNull_ReturnsSuccess()
    {
        var attribute = new ByteRangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var collection = new IIppValue[]
        {
            new IppValue<string?>((string?)null),
            new IppValue<string>("abc")
        };

        var result = attribute.IsValid(collection, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void ByteRangeAttribute_IsValid_WhenCollectionContainsIppValueWithValidValue_ReturnsSuccess()
    {
        var attribute = new ByteRangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var collection = new IppValue<string>[]
        {
            new("abc"),
            new("def")
        };

        var result = attribute.IsValid(collection, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void ByteRangeAttribute_IsValid_WhenCollectionContainsIppValueWithOutOfRangeValue_ReturnsValidationError()
    {
        var attribute = new ByteRangeAttribute(1, 5);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        var collection = new IppValue<string>[]
        {
            new("abc"),
            new("abcdef")
        };

        var result = attribute.IsValid(collection, context);

        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField must be between 1 and 5 bytes.");
    }

    [TestMethod]
    public void ByteRangeAttribute_IsValid_WhenValueIsIppValueWithCollection_ReturnsSuccess()
    {
        var attribute = new ByteRangeAttribute(1, 10);
        var context = new ValidationContext(Encoding.UTF8, "TestField");
        IppValue<string[]> value = new(new[] { "abc", "def" });

        var result = attribute.IsValid(value, context);

        result.Should().Be(ValidationResult.Success);
    }

    [TestMethod]
    public void ByteRangeAttribute_IsValid_WhenValueIsIppValueOctetString_ValidatesCorrectly()
    {
        var attribute = new ByteRangeAttribute(1, 5);
        var context = new ValidationContext(Encoding.UTF8, "TestField");

        IppValue<OctetString> valid = new(new OctetString(new byte[] { 1, 2 }));
        attribute.IsValid(valid, context).Should().Be(ValidationResult.Success);

        IppValue<OctetString> invalid = new(new OctetString(new byte[10]));
        var result = attribute.IsValid(invalid, context);
        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("The field TestField must be between 1 and 5 bytes.");
    }

    [TestMethod]
    public void ByteRangeAttribute_IsValid_WhenValueIsIppValueStringWithLanguage_ValidatesCorrectly()
    {
        var attribute = new ByteRangeAttribute(1, 15);
        var context = new ValidationContext(Encoding.UTF8, "TestField");

        IppValue<StringWithLanguage> valid = new(new StringWithLanguage("en", "hi"));
        attribute.IsValid(valid, context).Should().Be(ValidationResult.Success);

        IppValue<StringWithLanguage> invalid = new(new StringWithLanguage("en", "this is way too long for fifteen bytes"));
        var result = attribute.IsValid(invalid, context);
        result.Should().NotBeNull();
        result!.IsSuccess.Should().BeFalse();
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
    public void ByteRangeAttribute_OnUnsupportedType_ThrowsValidationException()
    {
        var attr = new ByteRangeAttribute(1, 10);
        var ctx = new ValidationContext(Encoding.UTF8, "UnsupportedValue");

        var result = attr.IsValid(123, ctx);
        result.Should().NotBeNull();
        result!.ErrorMessage.Should().Contain("Unsupported type");
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
                DocumentPassword = new OctetString(new byte[1024]) // Exceeds 1023 octets
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
                DocumentPassword = new OctetString(new byte[1023]) // Exactly 1023 octets
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
                DocumentPassword = new OctetString(new byte[1024]) // Exceeds 1023 octets
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
                DocumentPassword = new OctetString(new byte[1024]) // Exceeds 1023 octets
            }
        };

        Action act = () => validator.Validate(request);
        act.Should().Throw<ValidationException>().WithMessage("*DocumentPassword*");
    }


}
