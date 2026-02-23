using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Mapping.Profiles;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class TypesProfileTest
{
    private readonly IMapper _mapper;

    public TypesProfileTest()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(TypesProfile));
        mapper.FillFromAssembly(assembly!);
        _mapper = mapper;
    }

    [TestMethod]
    public void Map_IntToDateTime_MapsCorrectly()
    {
        // Act
        var result = _mapper.Map<int, DateTime>(10);

        // Assert
        result.Should().Be(new DateTime(1970, 1, 1, 0, 0, 10, DateTimeKind.Unspecified));
    }

    [TestMethod]
    public void Map_DateTimeToInt_MapsCorrectly()
    {
        // Arrange
        var dt = new DateTime(1970, 1, 1, 0, 0, 20, DateTimeKind.Unspecified);

        // Act
        var result = _mapper.Map<DateTime, int>(dt);

        // Assert
        result.Should().Be(20);
    }

    [TestMethod]
    public void Map_IntToIppStatusCode_MapsCorrectly()
    {
        // Act
        var result = _mapper.Map<int, IppStatusCode>(0x0000);

        // Assert
        result.Should().Be(IppStatusCode.SuccessfulOk);
    }

    [TestMethod]
    public void Map_IntToResolutionUnit_MapsCorrectly()
    {
        // Act
        var result = _mapper.Map<int, ResolutionUnit>(3);

        // Assert
        result.Should().Be(ResolutionUnit.DotsPerInch);
    }

    [TestMethod]
    public void Map_StringWithLanguageToString_MapsCorrectly()
    {
        // Arrange
        var strLang = new StringWithLanguage("en", "Test Value");

        // Act
        var result = _mapper.Map<StringWithLanguage, string>(strLang);

        // Assert
        result.Should().Be("Test Value");
    }

    [TestMethod]
    public void Map_StringToStringWithLanguageNullable_MapsCorrectly()
    {
        // Act
        var result = _mapper.MapNullable<string, StringWithLanguage?>("test");

        // Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void Map_StringToEnum_ValidString_MapsCorrectly()
    {
        // Act
        var result = _mapper.Map<string, JobHoldUntil>("no-hold");

        // Assert
        result.Should().Be(JobHoldUntil.NoHold);
    }

    [TestMethod]
    public void Map_StringToEnum_InvalidString_MapsToDefault()
    {
        // Act
        var result = _mapper.Map<string, JobHoldUntil>("invalid-value");

        // Assert
        result.Should().Be(JobHoldUntil.Unsupported);
    }

    [TestMethod]
    public void Map_NoValueToInt_MapsCorrectly()
    {
        // Act
        var result = _mapper.Map<NoValue, int>(NoValue.Instance);

        // Assert
        result.Should().Be(0);
    }

    [TestMethod]
    public void Map_NoValueToJobState_MapsCorrectly()
    {
        // Act
        var result = _mapper.Map<NoValue, JobState>(NoValue.Instance);

        // Assert
        result.Should().Be((JobState)0);
    }
}
