using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Mapping.Profiles;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class TypesProfileTest
{
    private readonly IMapper _mapper;

    public TypesProfileTest()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);
        _mapper = mapper;
    }

    public static IEnumerable<object[]> SerializationData
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
            yield return [NoValue.Instance, typeof(NoValue), typeof(int), int.MinValue, "NoValue -> Int"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(JobState), (JobState)int.MinValue, "NoValue -> JobState"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(DocumentState), (DocumentState)int.MinValue, "NoValue -> DocumentState"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(PrinterState), (PrinterState)int.MinValue, "NoValue -> PrinterState"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(Finishings), (Finishings)int.MinValue, "NoValue -> Finishings"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(IppStatusCode), (IppStatusCode)short.MinValue, "NoValue -> IppStatusCode"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(Orientation), (Orientation)int.MinValue, "NoValue -> Orientation"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(PrintQuality), (PrintQuality)int.MinValue, "NoValue -> PrintQuality"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(ResolutionUnit), (ResolutionUnit)int.MinValue, "NoValue -> ResolutionUnit"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(PrinterType), (PrinterType)int.MinValue, "NoValue -> PrinterType"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(IppOperation), (IppOperation)short.MinValue, "NoValue -> IppOperation"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(bool), false, "NoValue -> Bool"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(DateTime), NoValue.GetNoValue<DateTime>(), "NoValue -> DateTime"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(DateTime?), NoValue.GetNoValue<DateTime?>(), "NoValue -> DateTime?"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(DateTimeOffset), NoValue.GetNoValue<DateTimeOffset>(), "NoValue -> DateTimeOffset"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(SharpIpp.Protocol.Models.Range), NoValue.GetNoValue<SharpIpp.Protocol.Models.Range>(), "NoValue -> Range"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(StringWithLanguage), NoValue.GetNoValue<StringWithLanguage>(), "NoValue -> StringWithLanguage"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(string), NoValue.GetNoValue<string>(), "NoValue -> String"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(string), NoValue.GetNoValue<string?>(), "NoValue -> String?"];
            yield return ["separate-documents-uncollated-copies", typeof(string), typeof(MultipleDocumentHandling), MultipleDocumentHandling.SeparateDocumentsUncollatedCopies, "String -> MultipleDocumentHandling (valid)"];
            yield return ["invalid-value", typeof(string), typeof(MultipleDocumentHandling), new MultipleDocumentHandling("invalid-value"), "String -> MultipleDocumentHandling (invalid)"];
            yield return [MultipleDocumentHandling.SeparateDocumentsUncollatedCopies, typeof(MultipleDocumentHandling), typeof(string), "separate-documents-uncollated-copies", "MultipleDocumentHandling -> String"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(MultipleDocumentHandling), NoValue.GetNoValue<MultipleDocumentHandling>(), "NoValue -> MultipleDocumentHandling"];
            yield return ["auto", typeof(string), typeof(PrintScaling), PrintScaling.Auto, "String -> PrintScaling (valid)"];
            yield return ["invalid", typeof(string), typeof(PrintScaling), new PrintScaling("invalid"), "String -> PrintScaling (invalid)"];
            yield return [PrintScaling.Auto, typeof(PrintScaling), typeof(string), "auto", "PrintScaling -> String"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(PrintScaling), NoValue.GetNoValue<PrintScaling>(), "NoValue -> PrintScaling"];
            yield return ["completed", typeof(string), typeof(WhichJobs), WhichJobs.Completed, "String -> WhichJobs (valid)"];
            yield return ["invalid", typeof(string), typeof(WhichJobs), new WhichJobs("invalid"), "String -> WhichJobs (invalid)"];
            yield return [WhichJobs.Completed, typeof(WhichJobs), typeof(string), "completed", "WhichJobs -> String"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(WhichJobs), NoValue.GetNoValue<WhichJobs>(), "NoValue -> WhichJobs"];
            yield return ["adobe-1.7", typeof(string), typeof(PdfVersion), PdfVersion.Adobe17, "String -> PdfVersion (valid)"];
            yield return ["invalid", typeof(string), typeof(PdfVersion), new PdfVersion("invalid"), "String -> PdfVersion (invalid)"];
            yield return [PdfVersion.Adobe17, typeof(PdfVersion), typeof(string), "adobe-1.7", "PdfVersion -> String"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(PdfVersion), NoValue.GetNoValue<PdfVersion>(), "NoValue -> PdfVersion"];
            yield return ["job-incoming", typeof(string), typeof(JobStateReason), JobStateReason.JobIncoming, "String -> JobStateReason (valid)"];
            yield return ["invalid", typeof(string), typeof(JobStateReason), new JobStateReason("invalid"), "String -> JobStateReason (invalid)"];
            yield return [JobStateReason.JobIncoming, typeof(JobStateReason), typeof(string), "job-incoming", "JobStateReason -> String"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(JobStateReason), NoValue.GetNoValue<JobStateReason>(), "NoValue -> JobStateReason"];
            yield return ["ipp", typeof(string), typeof(UriScheme), UriScheme.Ipp, "String -> UriScheme (valid)"];
            yield return ["invalid", typeof(string), typeof(UriScheme), new UriScheme("invalid"), "String -> UriScheme (invalid)"];
            yield return [UriScheme.Ipp, typeof(UriScheme), typeof(string), "ipp", "UriScheme -> String"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(UriScheme), NoValue.GetNoValue<UriScheme>(), "NoValue -> UriScheme"];
            yield return [2, typeof(int), typeof(PrinterType), (PrinterType)2, "Int -> PrinterType"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(PowerState), NoValue.GetNoValue<PowerState>(), "NoValue -> PowerState"];
            yield return [5, typeof(int), typeof(PowerState), PowerState.Suspend, "Int -> PowerState"];
            yield return [PowerState.Suspend, typeof(PowerState), typeof(int), (int)PowerState.Suspend, "PowerState -> Int"];
            yield return [PowerState.Suspend, typeof(PowerState), typeof(NoValue), NoValue.Instance, "PowerState -> NoValue"];
            yield return [5, typeof(int), typeof(SharpIpp.Protocol.Models.Range), new SharpIpp.Protocol.Models.Range(5, 5), "Int -> Range"];
            yield return [new[] { "auto", "auto-fit" }, typeof(string[]), typeof(PrintScaling[]), new[] { PrintScaling.Auto, PrintScaling.AutoFit }, "string[] -> PrintScaling[]"];
            yield return [new object[] { "auto", "auto-fit" }, typeof(object[]), typeof(PrintScaling[]), new[] { PrintScaling.Auto, PrintScaling.AutoFit }, "object[] -> PrintScaling[]"];
        }
    }

    [TestMethod]
    [DynamicData(nameof(SerializationData))]
    public void Map_Values_MapsCorrectly(object source, Type sourceType, Type destType, object expected, string description)
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
    public void Map_NoValueToString_MapsToNoValueString()
    {
        var mockMapper = new Mock<IMapperConstructor>();
        mockMapper.Setup(x => x.CreateMap(
                It.IsAny<Type>(),
                It.IsAny<Type>(),
                It.IsAny<Func<object, object?, IMapperApplier, object?>>()))
            .Verifiable();

        mockMapper.Setup(x => x.CreateMap(
            It.IsAny<Type>(),
            It.IsAny<Type>(),
            It.IsAny<Func<object, IMapperApplier, object?>>()))
            .Verifiable();

        var profileType = typeof(SimpleMapper).Assembly.GetType("SharpIpp.Mapping.Profiles.TypesProfile");
        var profile = Activator.CreateInstance(profileType!);
        var createMapsMethod = profileType!.GetMethod("CreateMaps");
        createMapsMethod!.Invoke(profile, new object[] { mockMapper.Object });

        mockMapper.Verify(x => x.CreateMap(
            typeof(NoValue),
            typeof(string),
            It.IsAny<Func<object, object?, IMapperApplier, object?>>()), Times.AtLeastOnce);

        // Act
        var result = _mapper.Map<NoValue, string>(NoValue.Instance);

        // Assert
        result.Should().Be(NoValue.NoValueString);
    }

    [TestMethod]
    public void CreateMaps_ShouldRegisterOverwrittenNullableMappings()
    {
        // Act
        var mockMapper = new Mock<IMapperConstructor>();
        var capturedTypeMaps = new System.Collections.Generic.List<(Type src, Type dst, Func<object, object?, IMapperApplier, object?> map)>();
        var capturedNoValueToStringMaps = new System.Collections.Generic.List<Func<NoValue, IMapperApplier, string?>>();

        mockMapper.Setup(x => x.CreateMap(It.IsAny<Func<NoValue, IMapperApplier, string?>>()))
            .Callback<Func<NoValue, IMapperApplier, string?>>(map => capturedNoValueToStringMaps.Add(map));

        mockMapper.Setup(x => x.CreateMap(
                It.IsAny<Type>(),
                It.IsAny<Type>(),
                It.IsAny<Func<object, object?, IMapperApplier, object?>>()))
            .Callback<Type, Type, Func<object, object?, IMapperApplier, object?>>((src, dst, map) => capturedTypeMaps.Add((src, dst, map)));

        mockMapper.Setup(x => x.CreateMap(
            It.IsAny<Type>(),
            It.IsAny<Type>(),
            It.IsAny<Func<object, IMapperApplier, object?>>()))
            .Callback<Type, Type, Func<object, IMapperApplier, object?>>((src, dst, map) =>
            capturedTypeMaps.Add((src, dst, (source, _, applier) => map(source, applier))));

        var profileType = typeof(SimpleMapper).Assembly.GetType("SharpIpp.Mapping.Profiles.TypesProfile");
        var profile = Activator.CreateInstance(profileType!);
        var createMapsMethod = profileType!.GetMethod("CreateMaps");
        createMapsMethod!.Invoke(profile, new object[] { mockMapper.Object });

        mockMapper.Verify(x => x.CreateMap(
            typeof(NoValue),
            typeof(string),
            It.IsAny<Func<object, object?, IMapperApplier, object?>>()), Times.AtLeastOnce);

        // Assert
        var noValueToStringWithLanguageMap = capturedTypeMaps
            .Single(x => x.src == typeof(NoValue) && x.dst == typeof(StringWithLanguage?));
        var resultLanguage = noValueToStringWithLanguageMap.map(NoValue.Instance, null, Mock.Of<IMapperApplier>());
        resultLanguage.Should().Be(NoValue.GetNoValue<StringWithLanguage?>());

        var noValueToStringMaps = capturedTypeMaps
            .Where(x => x.src == typeof(NoValue) && x.dst == typeof(string))
            .Select(x => x.map)
            .ToArray();
        noValueToStringMaps.Should().NotBeEmpty();
        foreach (var map in noValueToStringMaps)
        {
            var resultString = map(NoValue.Instance, null, Mock.Of<IMapperApplier>());
            resultString.Should().Be(NoValue.NoValueString);
        }

        // Execute the typed CreateMap<NoValue, string> delegate to hit the lambda body in TypesProfile.
        capturedNoValueToStringMaps.Should().HaveCount(1);
        var typedResultString = capturedNoValueToStringMaps.Single()(NoValue.Instance, Mock.Of<IMapperApplier>());
        typedResultString.Should().Be(NoValue.NoValueString);
    }

}
