using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using SharpIpp.Tests.Unit.Mapping;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class TypesProfileTest : MapperTestBase
{

    public static IEnumerable<object?[]> SerializationData
    {
        get
        {
            yield return [10, typeof(int), typeof(DateTime), new DateTime(1970, 1, 1, 0, 0, 10, DateTimeKind.Unspecified), "Int -> DateTime"];
            yield return [new DateTime(1970, 1, 1, 0, 0, 20, DateTimeKind.Unspecified), typeof(DateTime), typeof(int), 20, "DateTime -> Int"];
            yield return [0x0000, typeof(int), typeof(IppStatusCode), IppStatusCode.SuccessfulOk, "Int -> IppStatusCode"];
            yield return [3, typeof(int), typeof(ResolutionUnit), ResolutionUnit.DotsPerInch, "Int -> ResolutionUnit"];
            yield return [new StringWithLanguage("en", "Test Value"), typeof(StringWithLanguage), typeof(string), "Test Value", "StringWithLanguage -> String"];
            yield return [System.Text.Encoding.UTF8.GetBytes("hello"), typeof(byte[]), typeof(string), "hello", "Byte[] -> String"];
            yield return ["hello", typeof(string), typeof(byte[]), System.Text.Encoding.UTF8.GetBytes("hello"), "String -> Byte[]"];
            yield return ["no-hold", typeof(string), typeof(JobHoldUntil), JobHoldUntil.NoHold, "String -> JobHoldUntil (valid)"];
            yield return ["invalid-value", typeof(string), typeof(JobHoldUntil), new JobHoldUntil("invalid-value"), "String -> JobHoldUntil (invalid)"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(bool), false, "NoValue -> Bool"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(IppValue<Uri>), default(IppValue<Uri>), "NoValue -> IppValue<Uri>"];
            yield return ["separate-documents-uncollated-copies", typeof(string), typeof(MultipleDocumentHandling), MultipleDocumentHandling.SeparateDocumentsUncollatedCopies, "String -> MultipleDocumentHandling (valid)"];
            yield return ["invalid-value", typeof(string), typeof(MultipleDocumentHandling), new MultipleDocumentHandling("invalid-value"), "String -> MultipleDocumentHandling (invalid)"];
            yield return [MultipleDocumentHandling.SeparateDocumentsUncollatedCopies, typeof(MultipleDocumentHandling), typeof(string), "separate-documents-uncollated-copies", "MultipleDocumentHandling -> String"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(IppValue<MultipleDocumentHandling>), default(IppValue<MultipleDocumentHandling>), "NoValue -> IppValue<MultipleDocumentHandling>"];
            yield return ["auto", typeof(string), typeof(PrintScaling), PrintScaling.Auto, "String -> PrintScaling (valid)"];
            yield return ["invalid", typeof(string), typeof(PrintScaling), new PrintScaling("invalid"), "String -> PrintScaling (invalid)"];
            yield return [PrintScaling.Auto, typeof(PrintScaling), typeof(string), "auto", "PrintScaling -> String"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(IppValue<PrintScaling>), default(IppValue<PrintScaling>), "NoValue -> IppValue<PrintScaling>"];
            yield return ["completed", typeof(string), typeof(WhichJobs), WhichJobs.Completed, "String -> WhichJobs (valid)"];
            yield return ["invalid", typeof(string), typeof(WhichJobs), new WhichJobs("invalid"), "String -> WhichJobs (invalid)"];
            yield return [WhichJobs.Completed, typeof(WhichJobs), typeof(string), "completed", "WhichJobs -> String"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(IppValue<WhichJobs>), default(IppValue<WhichJobs>), "NoValue -> IppValue<WhichJobs>"];
            yield return ["adobe-1.7", typeof(string), typeof(PdfVersion), PdfVersion.Adobe17, "String -> PdfVersion (valid)"];
            yield return ["invalid", typeof(string), typeof(PdfVersion), new PdfVersion("invalid"), "String -> PdfVersion (invalid)"];
            yield return [PdfVersion.Adobe17, typeof(PdfVersion), typeof(string), "adobe-1.7", "PdfVersion -> String"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(IppValue<PdfVersion>), default(IppValue<PdfVersion>), "NoValue -> IppValue<PdfVersion>"];
            yield return ["job-incoming", typeof(string), typeof(JobStateReason), JobStateReason.JobIncoming, "String -> JobStateReason (valid)"];
            yield return ["invalid", typeof(string), typeof(JobStateReason), new JobStateReason("invalid"), "String -> JobStateReason (invalid)"];
            yield return [JobStateReason.JobIncoming, typeof(JobStateReason), typeof(string), "job-incoming", "JobStateReason -> String"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(IppValue<JobStateReason>), default(IppValue<JobStateReason>), "NoValue -> IppValue<JobStateReason>"];
            yield return ["ipp", typeof(string), typeof(UriScheme), UriScheme.Ipp, "String -> UriScheme (valid)"];
            yield return ["invalid", typeof(string), typeof(UriScheme), new UriScheme("invalid"), "String -> UriScheme (invalid)"];
            yield return [UriScheme.Ipp, typeof(UriScheme), typeof(string), "ipp", "UriScheme -> String"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(IppValue<UriScheme>), default(IppValue<UriScheme>), "NoValue -> IppValue<UriScheme>"];
            yield return [2, typeof(int), typeof(PrinterType), (PrinterType)2, "Int -> PrinterType"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(IppValue<PowerState>), default(IppValue<PowerState>), "NoValue -> IppValue<PowerState>"];
            yield return ["suspend", typeof(string), typeof(PowerState), PowerState.Suspend, "String -> PowerState"];
            yield return [PowerState.Suspend, typeof(PowerState), typeof(string), "suspend", "PowerState -> String"];
            yield return [5, typeof(int), typeof(SharpIpp.Protocol.Models.Range), new SharpIpp.Protocol.Models.Range(5, 5), "Int -> Range"];
            yield return ["1.1", typeof(string), typeof(IppVersion), new IppVersion(1, 1), "String -> IppVersion"];
            yield return [new IppVersion(1, 1), typeof(IppVersion), typeof(string), "1.1", "IppVersion -> String"];
            yield return [new[] { "auto", "auto-fit" }, typeof(string[]), typeof(PrintScaling[]), new[] { PrintScaling.Auto, PrintScaling.AutoFit }, "string[] -> PrintScaling[]"];
            yield return [new object[] { "auto", "auto-fit" }, typeof(object[]), typeof(PrintScaling[]), new[] { PrintScaling.Auto, PrintScaling.AutoFit }, "object[] -> PrintScaling[]"];
        }
    }

    [TestMethod]
    [DynamicData(nameof(SerializationData))]
    public void Map_Values_MapsCorrectly(object? source, Type sourceType, Type destType, object? expected, string description)
    {
        // Act
        var result = _mapper.Map(source, sourceType, destType);

        // Assert
        result.Should().BeEquivalentTo(expected, description);
    }

    [TestMethod]
    public void Map_StringToStringWithLanguageNullable_MapsCorrectly()
    {
        // Act
        var result = _mapper.MapNullable<string, StringWithLanguage?>("test");

        // Assert
        result.Should().BeEquivalentTo(new StringWithLanguage("en", "test"));
    }

    [TestMethod]
    public void Map_CoreTypeMappers_RegistersExpectedMappings()
    {
        var mockMapper = new Mock<IMapperConstructor>();
        mockMapper.Setup(x => x.CreateMap(It.IsAny<Func<int, IMapperApplier, DateTime>>())).Verifiable();
        mockMapper.Setup(x => x.CreateMap(It.IsAny<Func<DateTime, IMapperApplier, int>>())).Verifiable();
        mockMapper.Setup(x => x.CreateMap(It.IsAny<Func<byte[], IMapperApplier, string>>())).Verifiable();
        mockMapper.Setup(x => x.CreateMap(It.IsAny<Func<string, IMapperApplier, byte[]>>())).Verifiable();

        var mapperType = typeof(SimpleMapper).Assembly.GetType("SharpIpp.Mapping.CoreTypeMappers");
        var configureMethod = mapperType!.GetMethod("Configure", BindingFlags.Public | BindingFlags.Static);
        configureMethod!.Invoke(null, new object[] { mockMapper.Object });

        mockMapper.Verify();
    }

    [TestMethod]
    public void CreateMaps_ShouldRegisterDateTimeAndByteArrayMappings()
    {
        var mockMapper = new Mock<IMapperConstructor>();
        Func<int, IMapperApplier, DateTime>? intToDateTime = null;
        Func<DateTime, IMapperApplier, int>? dateTimeToInt = null;
        Func<byte[], IMapperApplier, string>? bytesToString = null;
        Func<string, IMapperApplier, byte[]>? stringToBytes = null;

        mockMapper.Setup(x => x.CreateMap(It.IsAny<Func<int, IMapperApplier, DateTime>>()))
            .Callback<Func<int, IMapperApplier, DateTime>>(map => intToDateTime = map);
        mockMapper.Setup(x => x.CreateMap(It.IsAny<Func<DateTime, IMapperApplier, int>>()))
            .Callback<Func<DateTime, IMapperApplier, int>>(map => dateTimeToInt = map);
        mockMapper.Setup(x => x.CreateMap(It.IsAny<Func<byte[], IMapperApplier, string>>()))
            .Callback<Func<byte[], IMapperApplier, string>>(map => bytesToString = map);
        mockMapper.Setup(x => x.CreateMap(It.IsAny<Func<string, IMapperApplier, byte[]>>()))
            .Callback<Func<string, IMapperApplier, byte[]>>(map => stringToBytes = map);

        var mapperType = typeof(SimpleMapper).Assembly.GetType("SharpIpp.Mapping.CoreTypeMappers");
        var configureMethod = mapperType!.GetMethod("Configure", BindingFlags.Public | BindingFlags.Static);
        configureMethod!.Invoke(null, new object[] { mockMapper.Object });

        intToDateTime.Should().NotBeNull();
        intToDateTime!(10, Mock.Of<IMapperApplier>()).Should().Be(new DateTime(1970, 1, 1, 0, 0, 10, DateTimeKind.Unspecified));

        dateTimeToInt.Should().NotBeNull();
        dateTimeToInt!(new DateTime(1970, 1, 1, 0, 0, 10, DateTimeKind.Unspecified), Mock.Of<IMapperApplier>()).Should().Be(10);

        bytesToString.Should().NotBeNull();
        bytesToString!(System.Text.Encoding.UTF8.GetBytes("hello"), Mock.Of<IMapperApplier>()).Should().Be("hello");

        stringToBytes.Should().NotBeNull();
        stringToBytes!("hello", Mock.Of<IMapperApplier>()).Should().Equal(System.Text.Encoding.UTF8.GetBytes("hello"));
    }
}