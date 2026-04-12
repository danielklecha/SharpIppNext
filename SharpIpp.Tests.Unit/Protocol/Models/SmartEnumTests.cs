using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace SharpIpp.Tests.Unit.Protocol.Models;

[TestClass]
[ExcludeFromCodeCoverage]
public class SmartEnumTests
{
    [TestMethod]
    [DynamicData(nameof(SmartEnumData))]
    public void StaticFields_AreNotNull(Type type, string _)
    {
        // Arrange
        var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.FieldType == type);

        // Act & Assert
        foreach (var field in fields)
        {
            var value = field.GetValue(null);
            value.Should().NotBeNull($"Field {field.Name} in {type.Name} should not be null");
            value?.ToString().Should().NotBeNullOrEmpty($"Field {field.Name} in {type.Name} should have a valid string value");
        }
    }

    public static IEnumerable<object[]> SmartEnumData =>
        typeof(ISmartEnum).Assembly
            .GetTypes()
            .Where(type => typeof(ISmartEnum).IsAssignableFrom(type) && type is { IsValueType: true, IsAbstract: false, IsInterface: false })
            .OrderBy(type => type.FullName)
            .Select(type => new object[] { type, GetSampleValue(type) });

    public static IEnumerable<object[]> KeywordSmartEnumData =>
        typeof(IKeywordSmartEnum).Assembly
            .GetTypes()
            .Where(type => typeof(IKeywordSmartEnum).IsAssignableFrom(type) && type is { IsValueType: true, IsAbstract: false, IsInterface: false })
            .OrderBy(type => type.FullName)
            .Select(type => new object[] { type, GetSampleValue(type) });

    private static string GetSampleValue(Type type)
    {
        var knownValue = type.GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(field => field.FieldType == type)
            .Select(field => field.GetValue(null)?.ToString())
            .FirstOrDefault(value => !string.IsNullOrEmpty(value));

        return knownValue ?? "sample-value";
    }

    [TestMethod]
    [DynamicData(nameof(SmartEnumData))]
    public void ImplicitOperator_ToString_ReturnsValue(Type type, string value)
    {
        // Arrange
        var instance = CreatePopulatedSmartEnum(type, value);
        var implicitOperator = type.GetMethod("op_Implicit", new[] { type });

        // Act
        var result = implicitOperator?.Invoke(null, new[] { instance });

        // Assert
        result.Should().Be(value);
    }

    [TestMethod]
    [DynamicData(nameof(SmartEnumData))]
    public void PropertyInitializer_SetsValue(Type type, string value)
    {
        // Arrange
        var instance = Activator.CreateInstance(type);
        var property = type.GetProperty("Value");

        // Act
        property?.SetValue(instance, value);

        // Assert
        instance?.ToString().Should().Be(value);
    }

    [TestMethod]
    [DynamicData(nameof(KeywordSmartEnumData))]
    public void PropertyInitializer_SetsIsKeyword(Type type, string value)
    {
        // Arrange
        var instance = Activator.CreateInstance(type);
        var valueProperty = type.GetProperty("Value");
        var keywordProperty = type.GetProperty("IsKeyword");

        // Act
        valueProperty?.SetValue(instance, value);
        keywordProperty?.SetValue(instance, true);

        // Assert
        keywordProperty.Should().NotBeNull($"{type.Name} should expose IsKeyword property");
        keywordProperty?.GetValue(instance).Should().Be(true);
        instance.Should().BeAssignableTo<IKeywordSmartEnum>();
        ((IKeywordSmartEnum)instance!).Value.Should().Be(value);
    }

    [TestMethod]
    [DynamicData(nameof(SmartEnumData))]
    public void PropertyInitializer_SetsIsValue(Type type, string value)
    {
        // Arrange
        var instance = CreatePopulatedSmartEnum(type, value);
        var property = type.GetProperty("IsValue");

        // Act
        property?.SetValue(instance, false);

        // Assert
        property.Should().NotBeNull($"{type.Name} should expose IsValue property");
        property?.GetValue(instance).Should().Be(false);
    }

    [TestMethod]
    [DynamicData(nameof(SmartEnumData))]
    public void ExplicitOperator_FromString_ReturnsInstance(Type type, string value)
    {
        // Arrange
        var explicitOperator = type.GetMethods(BindingFlags.Public | BindingFlags.Static)
            .FirstOrDefault(m => m.Name == "op_Explicit" && m.ReturnType == type);

        // Act
        var result = explicitOperator?.Invoke(null, new object[] { value });

        // Assert
        result.Should().BeOfType(type);
        result?.ToString().Should().Be(value);
    }

    [TestMethod]
    [DynamicData(nameof(SmartEnumData))]
    public void ToString_ReturnsValue(Type type, string value)
    {
        // Arrange
        var instance = CreatePopulatedSmartEnum(type, value);

        // Act
        var result = instance?.ToString();

        // Assert
        result.Should().Be(value);
    }

    internal static object CreatePopulatedSmartEnum(Type type, string value)
    {
        var threeArgumentConstructor = type.GetConstructor([typeof(string), typeof(bool), typeof(bool)]);
        if (threeArgumentConstructor != null)
            return threeArgumentConstructor.Invoke([value, true, true]);

        var twoArgumentConstructor = type.GetConstructor([typeof(string), typeof(bool)]);
        if (twoArgumentConstructor != null)
            return twoArgumentConstructor.Invoke([value, true]);

        throw new MissingMethodException($"No supported constructor found for smart enum type '{type.FullName}'.");
    }
}
