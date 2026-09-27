using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Mapping.Types;

/// <summary>
/// Comprehensive round-trip tests covering all ISmartEnum and IMarkedSmartEnum types.
/// </summary>
[TestClass]
[ExcludeFromCodeCoverage]
public class AllSmartEnumsRoundTripTests : MapperTestBase
{
    private static readonly Assembly SharpIppAssembly = typeof(ISharpIppClient).Assembly;

    private static List<Type> GetAllSmartEnumTypes() =>
        SharpIppAssembly.GetTypes()
            .Where(t => t.IsValueType && typeof(ISmartEnum).IsAssignableFrom(t))
            .ToList();

    [TestMethod]
    public void AllSmartEnums_ArbitraryString_RoundTripsSuccessfully()
    {
        var smartEnums = GetAllSmartEnumTypes();
        smartEnums.Should().NotBeEmpty();

        foreach (var type in smartEnums)
        {
            var testValue = $"custom-{type.Name.ToLowerInvariant()}-value";

            // string -> SmartEnum
            var enumInstance = _mapper.Map(testValue, typeof(string), type);
            enumInstance.Should().NotBeNull();

            // SmartEnum -> string
            var backToString = (string)_mapper.Map(enumInstance, type, typeof(string));
            backToString.Should().Be(testValue, $"round-tripping custom string for {type.Name} must match original value");
        }
    }

    [TestMethod]
    public void AllSmartEnums_NoValue_ProducesNonValueInstance()
    {
        var smartEnums = GetAllSmartEnumTypes();

        foreach (var type in smartEnums)
        {
            var ippValueType = typeof(IppValue<>).MakeGenericType(type);
            var ippValueInstance = (IIppValue)_mapper.Map(NoValue.Instance, typeof(NoValue), ippValueType);
            ippValueInstance.IsValue.Should().BeFalse($"NoValue for IppValue<{type.Name}> must have IsValue == false");
        }
    }

    [TestMethod]
    public void AllSmartEnums_ArrayConversions_RoundTrip()
    {
        var smartEnums = GetAllSmartEnumTypes();

        foreach (var type in smartEnums)
        {
            var testValues = new[] { "val-1", "val-2" };

            // string[] -> SmartEnum[]
            var arrayType = type.MakeArrayType();
            var enumArray = (Array)_mapper.Map(testValues, typeof(string[]), arrayType);
            enumArray.Length.Should().Be(2);

            // SmartEnum[] -> string[]
            var stringArray = (string[])_mapper.Map(enumArray, arrayType, typeof(string[]));
            stringArray.Should().Equal(testValues, $"array roundtrip for {type.Name} must preserve items");
        }
    }

    [TestMethod]
    public void MarkedSmartEnums_PreserveMarkedState()
    {
        var markedEnums = SharpIppAssembly.GetTypes()
            .Where(t => t.IsValueType && typeof(IMarkedSmartEnum).IsAssignableFrom(t))
            .ToList();

        markedEnums.Should().NotBeEmpty();

        foreach (var type in markedEnums)
        {
            // Keyword tag test: constructor (string, isKeyword)
            var keywordInst = (IMarkedSmartEnum)Activator.CreateInstance(type, "sample-kw", true)!;
            keywordInst.IsMarked.Should().BeTrue();
            keywordInst.ToIppTag().Should().Be(Tag.Keyword);

            // NameWithoutLanguage tag test: constructor (string, false)
            var nameInst = (IMarkedSmartEnum)Activator.CreateInstance(type, "sample-name", false)!;
            nameInst.IsMarked.Should().BeFalse();
            nameInst.ToIppTag().Should().Be(Tag.NameWithoutLanguage);
        }
    }
}
