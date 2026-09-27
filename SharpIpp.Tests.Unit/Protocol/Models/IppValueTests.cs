using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Protocol.Models;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppValueTests
{
    [TestMethod]
    public void WhenConstructedWithValue_HasValueAndReturnsValue()
    {
        IppValue<int> val = new(42);

        val.IsValue.Should().BeTrue();
        val.HasValue.Should().BeTrue();
        val.Value.Should().Be(42);
        val.GetValueOrDefault().Should().Be(42);
        val.GetValueOrDefault(99).Should().Be(42);
    }

    [TestMethod]
    public void WhenDefault_RepresentsNoValue()
    {
        IppValue<int> val = default;

        val.IsValue.Should().BeFalse();
        val.HasValue.Should().BeFalse();
        val.Invoking(x => x.Value).Should().Throw<InvalidOperationException>();
        val.GetValueOrDefault().Should().Be(0);
        val.GetValueOrDefault(99).Should().Be(99);
    }

    [TestMethod]
    public void ValueAsObject_ReturnsExpectedValueOrNull()
    {
        IppValue<int> intVal = 42;
        intVal.ValueAsObject.Should().Be(42);
        ((IIppValue)intVal).ValueAsObject.Should().Be(42);

        IppValue<string> strVal = "hello";
        strVal.ValueAsObject.Should().Be("hello");
        ((IIppValue)strVal).ValueAsObject.Should().Be("hello");

        IppValue<string?> nullVal = new((string?)null);
        nullVal.ValueAsObject.Should().BeNull();
        ((IIppValue)nullVal).ValueAsObject.Should().BeNull();

        IppValue<int> noVal = NoValue.Instance;
        noVal.ValueAsObject.Should().BeNull();
        ((IIppValue)noVal).ValueAsObject.Should().BeNull();
    }

    [TestMethod]
    public void NoValue_Property_ReturnsDefault()
    {
        IppValue<int> property = IppValue<int>.NoValue;
        property.IsValue.Should().BeFalse();

        IppValue<int> ctor = new();
        ctor.IsValue.Should().BeFalse();
    }

    [TestMethod]
    public void WhenCreatedFromNoValueInstance_RepresentsNoValue()
    {
        IppValue<string> val = NoValue.Instance;

        val.IsValue.Should().BeFalse();
        val.HasValue.Should().BeFalse();
        (val == NoValue.Instance).Should().BeTrue();
        (val != NoValue.Instance).Should().BeFalse();
        (NoValue.Instance == val).Should().BeTrue();
        (NoValue.Instance != val).Should().BeFalse();
    }

    [TestMethod]
    public void NoValueComparison_WithValue_ReturnsExpected()
    {
        IppValue<int> val = 42;

        (val == NoValue.Instance).Should().BeFalse();
        (val != NoValue.Instance).Should().BeTrue();
        (NoValue.Instance == val).Should().BeFalse();
        (NoValue.Instance != val).Should().BeTrue();
    }

    [TestMethod]
    public void WhenValueIsImplicitlyConverted_ProducesIppValue()
    {
        IppValue<string> val = "hello";

        val.IsValue.Should().BeTrue();
        val.Value.Should().Be("hello");
        (val == NoValue.Instance).Should().BeFalse();
    }

    [TestMethod]
    public void WhenConvertedToUnderlyingType_ReturnsValueOrDefault()
    {
        IppValue<int> withVal = 10;
        int converted = withVal;
        converted.Should().Be(10);

        IppValue<int> noVal = NoValue.Instance;
        int defaultConverted = noVal;
        defaultConverted.Should().Be(0);

        IppValue<string> stringNoVal = NoValue.Instance;
        string? nullConverted = stringNoVal;
        nullConverted.Should().BeNull();
    }

    [TestMethod]
    public void TryGetValue_WhenValuePresent_ReturnsTrue()
    {
        IppValue<string> val = "test";
        var result = val.TryGetValue(out var str);

        result.Should().BeTrue();
        str.Should().Be("test");
    }

    [TestMethod]
    public void TryGetValue_WhenNoValue_ReturnsFalse()
    {
        IppValue<string> val = NoValue.Instance;
        var result = val.TryGetValue(out var str);

        result.Should().BeFalse();
        str.Should().BeNull();
    }

    [TestMethod]
    public void TryGetValue_WhenValueIsNull_ReturnsFalse()
    {
        IppValue<string?> val = new((string?)null);
        var result = val.TryGetValue(out var str);

        result.Should().BeFalse();
        str.Should().BeNull();
    }

    [TestMethod]
    public void Deconstruct_ReturnsIsValueAndValue()
    {
        IppValue<int> val = 7;
        var (isValue, value) = val;

        isValue.Should().BeTrue();
        value.Should().Be(7);

        IppValue<int> noVal = NoValue.Instance;
        var (noIsValue, noValue) = noVal;

        noIsValue.Should().BeFalse();
        noValue.Should().Be(0);
    }

    [TestMethod]
    public void Equality_ScalarComparison()
    {
        IppValue<int> a = 5;
        IppValue<int> b = 5;
        IppValue<int> c = 6;
        IppValue<int> d = NoValue.Instance;
        IppValue<int> e = NoValue.Instance;

        (a == b).Should().BeTrue();
        (a != b).Should().BeFalse();
        (a != c).Should().BeTrue();
        (a == c).Should().BeFalse();
        (a == d).Should().BeFalse();
        (a != d).Should().BeTrue();
        (d == e).Should().BeTrue();
        (d != e).Should().BeFalse();
    }

    [TestMethod]
    public void Equality_ObjectEquals()
    {
        IppValue<int> a = 5;
        IppValue<int> b = 5;
        IppValue<int> c = 6;
        IppValue<int> d = NoValue.Instance;

        // Equals(object) with IppValue<T>
        a.Equals((object)b).Should().BeTrue();
        a.Equals((object)c).Should().BeFalse();
        a.Equals((object)d).Should().BeFalse();
        d.Equals((object)IppValue<int>.NoValue).Should().BeTrue();

        // Equals(object) with NoValue
        a.Equals((object)NoValue.Instance).Should().BeFalse();
        d.Equals((object)NoValue.Instance).Should().BeTrue();

        // Equals(object) with raw T
        a.Equals((object)5).Should().BeTrue();
        a.Equals((object)6).Should().BeFalse();
        d.Equals((object)5).Should().BeFalse();

        // Equals(object) with unrelated type and null
        a.Equals("string").Should().BeFalse();
        a.Equals(null).Should().BeFalse();
        d.Equals(null).Should().BeFalse();
    }

    [TestMethod]
    public void Equality_StructuralArrayComparison()
    {
        IppValue<int[]> arr1 = new[] { 1, 2, 3 };
        IppValue<int[]> arr2 = new[] { 1, 2, 3 };
        IppValue<int[]> arr3 = new[] { 1, 2, 4 };
        IppValue<int[]> arr4 = new[] { 1, 2 };
        IppValue<int[]> noVal = NoValue.Instance;

        (arr1 == arr2).Should().BeTrue();
        arr1.Equals(arr2).Should().BeTrue();
        (arr1 == arr3).Should().BeFalse();
        arr1.Equals(arr3).Should().BeFalse();
        (arr1 == arr4).Should().BeFalse();
        arr1.Equals(arr4).Should().BeFalse();
        (arr1 == noVal).Should().BeFalse();
        arr1.Equals(noVal).Should().BeFalse();

        // Object Equals with array of T
        arr1.Equals((object)new[] { 1, 2, 3 }).Should().BeTrue();
        arr1.Equals((object)new[] { 1, 2 }).Should().BeFalse();
        arr1.Equals((object)new[] { 1, 2, 4 }).Should().BeFalse();
        noVal.Equals((object)new[] { 1, 2, 3 }).Should().BeFalse();
    }

    [TestMethod]
    public void GetHashCode_ComputesConsistently()
    {
        IppValue<int> val = 42;
        val.GetHashCode().Should().Be(42.GetHashCode());

        IppValue<int> noVal = NoValue.Instance;
        noVal.GetHashCode().Should().Be(0);

        IppValue<string?> nullVal = new((string?)null);
        nullVal.GetHashCode().Should().Be(0);

        IppValue<int[]> arr1 = new[] { 1, 2, 3 };
        IppValue<int[]> arr2 = new[] { 1, 2, 3 };
        arr1.GetHashCode().Should().Be(arr2.GetHashCode());

        IppValue<string?[]> arrWithNull = new[] { "a", null, "b" };
        arrWithNull.GetHashCode().Should().NotBe(0);
    }

    [TestMethod]
    public void ToString_FormatsAppropriately()
    {
        IppValue<int> val = 42;
        val.ToString().Should().Be("42");

        IppValue<int> noVal = NoValue.Instance;
        noVal.ToString().Should().Be("no value");

        IppValue<string?> nullVal = new((string?)null);
        nullVal.ToString().Should().Be(string.Empty);
    }
}