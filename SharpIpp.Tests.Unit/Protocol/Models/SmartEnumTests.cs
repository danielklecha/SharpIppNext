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
        var instance = Activator.CreateInstance(type, value, true);
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
    [DynamicData(nameof(SmartEnumData))]
    public void PropertyInitializer_SetsIsValue(Type type, string value)
    {
        // Arrange
        var instance = Activator.CreateInstance(type, value, true);
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
        var instance = Activator.CreateInstance(type, value, true);

        // Act
        var result = instance?.ToString();

        // Assert
        result.Should().Be(value);
    }
}
