using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            yield return ["invalid-value", typeof(string), typeof(JobHoldUntil), JobHoldUntil.Unsupported, "String -> JobHoldUntil (invalid)"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(int), 0, "NoValue -> Int"];
            yield return [NoValue.Instance, typeof(NoValue), typeof(JobState), (JobState)0, "NoValue -> JobState"];
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
        result.Should().BeNull();
    }

}
