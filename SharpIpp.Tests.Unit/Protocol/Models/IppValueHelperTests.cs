using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Protocol.Models;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppValueHelperTests
{
    [TestMethod]
    public void FromString_WithValueType_ShouldMapAndReturnIppValue()
    {
        var mapperMock = new Mock<IMapperApplier>();
        mapperMock.Setup(m => m.Map<JobState>("3")).Returns(JobState.Pending);

        var result = IppValueHelper.FromString<JobState>("3", mapperMock.Object);

        result.IsValue.Should().BeTrue();
        result.Value.Should().Be(JobState.Pending);
    }

    [TestMethod]
    public void FromString_WithKeywordEnum_ShouldMapAndReturnIppValue()
    {
        var mapperMock = new Mock<IMapperApplier>();
        mapperMock.Setup(m => m.Map<WhichJobs>("completed")).Returns(WhichJobs.Completed);

        var result = IppValueHelper.FromString<WhichJobs>("completed", mapperMock.Object);

        result.IsValue.Should().BeTrue();
        result.Value.Should().Be(WhichJobs.Completed);
    }

    [TestMethod]
    public void FromString_WithString_ShouldMapAndReturnIppValue()
    {
        var mapperMock = new Mock<IMapperApplier>();
        mapperMock.Setup(m => m.Map<string>("test")).Returns("test");

        var result = IppValueHelper.FromString<string>("test", mapperMock.Object);

        result.IsValue.Should().BeTrue();
        result.Value.Should().Be("test");
    }

    [TestMethod]
    public void FromValue_ShouldWrapValueInIppValue()
    {
        var mapperMock = Mock.Of<IMapperApplier>();

        var intVal = IppValueHelper.FromValue(42, mapperMock);
        intVal.IsValue.Should().BeTrue();
        intVal.Value.Should().Be(42);

        var strVal = IppValueHelper.FromValue("hello", mapperMock);
        strVal.IsValue.Should().BeTrue();
        strVal.Value.Should().Be("hello");
    }

    [TestMethod]
    public void FromNoValue_ShouldReturnDefaultIppValue()
    {
        var mapperMock = Mock.Of<IMapperApplier>();

        var intVal = IppValueHelper.FromNoValue<int>(NoValue.Instance, mapperMock);
        intVal.IsValue.Should().BeFalse();

        var strVal = IppValueHelper.FromNoValue<string>(NoValue.Instance, mapperMock);
        strVal.IsValue.Should().BeFalse();
    }

    [TestMethod]
    public void ArrayEquals_ShouldCompareArraysCorrectly()
    {
        var arr1 = new[] { 1, 2, 3 };
        var arr2 = new[] { 1, 2, 3 };
        var diffLen = new[] { 1, 2 };
        var diffVal = new[] { 1, 2, 4 };

        IppValueHelper.ArrayEquals(arr1, arr2).Should().BeTrue();
        IppValueHelper.ArrayEquals(arr1, diffLen).Should().BeFalse();
        IppValueHelper.ArrayEquals(arr1, diffVal).Should().BeFalse();
    }

    [TestMethod]
    public void ArrayGetHashCode_ShouldComputeHashConsistently()
    {
        var arr1 = new[] { 1, 2, 3 };
        var arr2 = new[] { 1, 2, 3 };
        var arrWithNull = new object?[] { 1, null, "test" };

        IppValueHelper.ArrayGetHashCode(arr1).Should().Be(IppValueHelper.ArrayGetHashCode(arr2));
        IppValueHelper.ArrayGetHashCode(arrWithNull).Should().NotBe(0);
    }
}
