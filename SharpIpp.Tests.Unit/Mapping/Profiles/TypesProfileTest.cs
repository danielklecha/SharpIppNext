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
            yield return ["no-hold", typeof(string), typeof(JobHoldUntil), JobHoldUntil.NoHold, "String -> JobHoldUntil (valid)"];
            yield return ["invalid-value", typeof(string), typeof(JobHoldUntil), (JobHoldUntil)int.MinValue, "String -> JobHoldUntil (invalid)"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(int), int.MinValue, "NoValue -> Int"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(JobState), (JobState)int.MinValue, "NoValue -> JobState"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(PrinterState), (PrinterState)int.MinValue, "NoValue -> PrinterState"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(Finishings), (Finishings)int.MinValue, "NoValue -> Finishings"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(IppStatusCode), (IppStatusCode)short.MinValue, "NoValue -> IppStatusCode"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(Orientation), (Orientation)int.MinValue, "NoValue -> Orientation"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(PrintQuality), (PrintQuality)int.MinValue, "NoValue -> PrintQuality"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(ResolutionUnit), (ResolutionUnit)int.MinValue, "NoValue -> ResolutionUnit"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(PrinterType), (PrinterType)int.MinValue, "NoValue -> PrinterType"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(IppOperation), (IppOperation)short.MinValue, "NoValue -> IppOperation"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(DateTime), NoValue.GetNoValue<DateTime>(), "NoValue -> DateTime"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(DateTime?), NoValue.GetNoValue<DateTime?>(), "NoValue -> DateTime?"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(DateTimeOffset), NoValue.GetNoValue<DateTimeOffset>(), "NoValue -> DateTimeOffset"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(SharpIpp.Protocol.Models.Range), NoValue.GetNoValue<SharpIpp.Protocol.Models.Range>(), "NoValue -> Range"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(StringWithLanguage), NoValue.GetNoValue<StringWithLanguage>(), "NoValue -> StringWithLanguage"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(string), NoValue.GetNoValue<string>(), "NoValue -> String"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(string), NoValue.GetNoValue<string?>(), "NoValue -> String?"];
            yield return ["separate-documents-uncollated-copies", typeof(string), typeof(MultipleDocumentHandling), MultipleDocumentHandling.SeparateDocumentsUncollatedCopies, "String -> MultipleDocumentHandling (valid)"];
            yield return ["invalid-value", typeof(string), typeof(MultipleDocumentHandling), (MultipleDocumentHandling)int.MinValue, "String -> MultipleDocumentHandling (invalid)"];
            yield return [MultipleDocumentHandling.SeparateDocumentsUncollatedCopies, typeof(MultipleDocumentHandling), typeof(string), "separate-documents-uncollated-copies", "MultipleDocumentHandling -> String"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(MultipleDocumentHandling), (MultipleDocumentHandling)int.MinValue, "NoValue -> MultipleDocumentHandling"];
            yield return [2, typeof(int), typeof(PrinterType), (PrinterType)2, "Int -> PrinterType"];
        }
    }

    [TestMethod]
    [DynamicData(nameof(SerializationData))]
    public void Map_Values_MapsCorrectly(object source, Type sourceType, Type destType, object expected, string description)
    {
        // Act
        var result = _mapper.Map(source, sourceType, destType);

        // Assert
        result.Should().Be(expected, description);
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
    public void CreateMaps_ShouldRegisterOverwrittenNullableMappings()
    {
        // Act
        var mockMapper = new Mock<IMapperConstructor>();
        var capturedStringFuncs = new System.Collections.Generic.List<Func<NoValue, IMapperApplier, string>>();
        var capturedStringWithLanguageFuncs = new System.Collections.Generic.List<Func<NoValue, IMapperApplier, StringWithLanguage?>>();

        mockMapper.Setup(x => x.CreateMap(It.IsAny<Func<NoValue, IMapperApplier, string>>()))
            .Callback<Func<NoValue, IMapperApplier, string>>(f => capturedStringFuncs.Add(f));

        mockMapper.Setup(x => x.CreateMap(It.IsAny<Func<NoValue, IMapperApplier, StringWithLanguage?>>()))
            .Callback<Func<NoValue, IMapperApplier, StringWithLanguage?>>(f => capturedStringWithLanguageFuncs.Add(f));

        var profileType = typeof(SimpleMapper).Assembly.GetType("SharpIpp.Mapping.Profiles.TypesProfile");
        var profile = Activator.CreateInstance(profileType!);
        var createMapsMethod = profileType!.GetMethod("CreateMaps");
        createMapsMethod!.Invoke(profile, new object[] { mockMapper.Object });

        // Assert
        capturedStringWithLanguageFuncs.Should().HaveCount(1);
        var lambdaLanguage = capturedStringWithLanguageFuncs.Single();
        var resultLanguage = lambdaLanguage(NoValue.Instance, Mock.Of<IMapperApplier>());
        resultLanguage.Should().Be(NoValue.GetNoValue<StringWithLanguage?>());

        capturedStringFuncs.Should().HaveCount(2);
        foreach (var func in capturedStringFuncs)
        {
            var resultString = func(NoValue.Instance, Mock.Of<IMapperApplier>());
            resultString.Should().Be(NoValue.NoValueString);
        }
    }

}
