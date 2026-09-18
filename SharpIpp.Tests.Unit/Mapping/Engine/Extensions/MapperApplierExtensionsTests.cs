using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Tests.Unit.Mapping.Extensions;

[TestClass]
[ExcludeFromCodeCoverage]
public class MapperApplierExtensionsTests
{
    [TestMethod]
    public void MapFromDicSet_ShouldReturnMappedValues()
    {
        // Arrange
        var mapperMock = new Mock<IMapperApplier>();
        var src = new Dictionary<string, IppAttribute[]>
        {
            { "testKey", new[] { new IppAttribute(Tag.Integer, "testKey", 1), new IppAttribute(Tag.Integer, "testKey", 2) } }
        };

        mapperMock.Setup(m => m.Map<int[]>(It.Is<object[]>(v => v.Length == 2 && (int)v[0] == 1 && (int)v[1] == 2)))
            .Returns(new[] { 1, 2 });

        // Act
        var result = mapperMock.Object.MapFromDicSet<int[]>(src, "testKey");

        // Assert
        CollectionAssert.AreEqual(new[] { 1, 2 }, result);
    }

    [TestMethod]
    public void MapFromDicSet_ShouldReturnNoValueForMissingKey()
    {
        var mapperMock = new Mock<IMapperApplier>();
        mapperMock.Setup(m => m.Map<int[]>(NoValue.Instance)).Returns(new int[0]);
        var src = new Dictionary<string, IppAttribute[]>();

        var result = mapperMock.Object.MapFromDicSet<int[]>(src, "testKey");
        CollectionAssert.AreEqual(new int[0], result);
    }

    [TestMethod]
    public void MapFromDicSet_Should_Return_NoValue_For_Empty_Array()
    {
        var mapperMock = new Mock<IMapperApplier>();
        mapperMock.Setup(m => m.Map<int[]>(NoValue.Instance)).Returns(new int[0]);
        var src = new Dictionary<string, IppAttribute[]> { { "testKey", new IppAttribute[0] } };

        var result = mapperMock.Object.MapFromDicSet<int[]>(src, "testKey");
        CollectionAssert.AreEqual(new int[0], result);
    }


    [TestMethod]
    public void MapFromDicSetNullable_Should_Return_Mapped_Values()
    {
        // Arrange
        var mapperMock = new Mock<IMapperApplier>();
        var src = new Dictionary<string, IppAttribute[]>
        {
            { "testKey", new[] { new IppAttribute(Tag.Integer, "testKey", 3) } }
        };

        mapperMock.Setup(m => m.MapNullable<int[]?>(It.Is<object[]>(v => v.Length == 1 && (int)v[0] == 3)))
            .Returns(new[] { 3 });

        // Act
        var result = mapperMock.Object.MapFromDicSetNullable<int[]?>(src, "testKey");

        // Assert
        CollectionAssert.AreEqual(new[] { 3 }, result);
    }

    [TestMethod]
    public void MapFromDicSetNullable_Should_Return_NoValue_For_Missing_Key()
    {
        var mapperMock = new Mock<IMapperApplier>();
        mapperMock.Setup(m => m.MapNullable<int[]?>(null)).Returns((int[]?)null);
        var src = new Dictionary<string, IppAttribute[]>();

        var result = mapperMock.Object.MapFromDicSetNullable<int[]?>(src, "testKey");
        Assert.IsNull(result);
    }

    [TestMethod]
    public void MapFromDicSetNullable_Should_Return_NoValue_For_Empty_Array()
    {
        var mapperMock = new Mock<IMapperApplier>();
        mapperMock.Setup(m => m.MapNullable<int[]?>(null)).Returns((int[]?)null);
        var src = new Dictionary<string, IppAttribute[]> { { "testKey", new IppAttribute[0] } };

        var result = mapperMock.Object.MapFromDicSetNullable<int[]?>(src, "testKey");
        Assert.IsNull(result);
    }

    [TestMethod]
    public void MapFromDic_Should_Return_Mapped_Value()
    {
        // Arrange
        var mapperMock = new Mock<IMapperApplier>();
        var src = new Dictionary<string, IppAttribute[]>
        {
            { "testKey", new[] { new IppAttribute(Tag.Integer, "testKey", 5) } }
        };

        mapperMock.Setup(m => m.Map<int>(5)).Returns(5);

        // Act
        var result = mapperMock.Object.MapFromDic<int>(src, "testKey");

        // Assert
        Assert.AreEqual(5, result);
    }

    [TestMethod]
    public void MapFromDic_Should_Return_NoValue_For_Missing_Key()
    {
        var mapperMock = new Mock<IMapperApplier>();
        mapperMock.Setup(m => m.Map<int>(NoValue.Instance)).Returns(0);
        var src = new Dictionary<string, IppAttribute[]>();

        var result = mapperMock.Object.MapFromDic<int>(src, "testKey");
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void MapFromDic_Should_Return_NoValue_For_Empty_Array()
    {
        var mapperMock = new Mock<IMapperApplier>();
        mapperMock.Setup(m => m.Map<int>(NoValue.Instance)).Returns(0);
        var src = new Dictionary<string, IppAttribute[]> { { "testKey", new IppAttribute[0] } };

        var result = mapperMock.Object.MapFromDic<int>(src, "testKey");
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void MapFromDicNullable_Should_Return_Mapped_Value()
    {
        // Arrange
        var mapperMock = new Mock<IMapperApplier>();
        var src = new Dictionary<string, IppAttribute[]>
        {
            { "testKey", new[] { new IppAttribute(Tag.Integer, "testKey", 5) } }
        };

        mapperMock.Setup(m => m.MapNullable<int?>(5)).Returns(5);

        // Act
        var result = mapperMock.Object.MapFromDicNullable<int?>(src, "testKey");

        // Assert
        Assert.AreEqual(5, result);
    }

    [TestMethod]
    public void MapFromDicNullable_Should_Return_NoValue_For_Missing_Key()
    {
        var mapperMock = new Mock<IMapperApplier>();
        mapperMock.Setup(m => m.MapNullable<int?>(NoValue.Instance)).Returns((int?)null);
        var src = new Dictionary<string, IppAttribute[]>();

        var result = mapperMock.Object.MapFromDicNullable<int?>(src, "testKey");
        Assert.IsNull(result);
    }

    [TestMethod]
    public void MapFromDicNullable_Should_Return_NoValue_For_Empty_Array()
    {
        var mapperMock = new Mock<IMapperApplier>();
        mapperMock.Setup(m => m.MapNullable<int?>(NoValue.Instance)).Returns((int?)null);
        var src = new Dictionary<string, IppAttribute[]> { { "testKey", new IppAttribute[0] } };

        var result = mapperMock.Object.MapFromDicNullable<int?>(src, "testKey");
        Assert.IsNull(result);
    }

    [TestMethod]
    public void MapFromDicNullable_WithFactory_Should_Return_Factory_Result()
    {
        var mapperMock = new Mock<IMapperApplier>();
        var src = new Dictionary<string, IppAttribute[]>
        {
            { "testKey", new[] { new IppAttribute(Tag.NameWithoutLanguage, "testKey", "value") } }
        };

        mapperMock.Setup(m => m.MapNullable<string>("value")).Returns("value");

        var result = mapperMock.Object.MapFromDicNullable<string, string?>(
            src,
            "testKey",
            (attribute, value) => $"{attribute.Name}-{value}");

        Assert.AreEqual("testKey-value", result);
    }

    [TestMethod]
    public void MapFromDicNullable_WithFactory_Should_Return_Null_When_Partial_Is_Null()
    {
        var mapperMock = new Mock<IMapperApplier>();
        var src = new Dictionary<string, IppAttribute[]>
        {
            { "testKey", new[] { new IppAttribute(Tag.Integer, "testKey", 10) } }
        };

        mapperMock.Setup(m => m.MapNullable<int?>(10)).Returns((int?)null);
        var factoryCalled = false;

        var result = mapperMock.Object.MapFromDicNullable<int?, string?>(
            src,
            "testKey",
            (_, value) =>
            {
                factoryCalled = true;
                return value?.ToString();
            });

        Assert.IsNull(result);
        Assert.IsFalse(factoryCalled);
    }

    [TestMethod]
    public void MapFromDicNullable_WithFactory_Should_Return_Null_For_Empty_Array()
    {
        var mapperMock = new Mock<IMapperApplier>();
        var src = new Dictionary<string, IppAttribute[]>
        {
            { "testKey", new IppAttribute[0] }
        };

        mapperMock.Setup(m => m.MapNullable<string?>(null)).Returns((string?)null);
        var factoryCalled = false;

        var result = mapperMock.Object.MapFromDicNullable<int?, string?>(
            src,
            "testKey",
            (_, value) =>
            {
                factoryCalled = true;
                return value?.ToString();
            });

        Assert.IsNull(result);
        Assert.IsFalse(factoryCalled);
    }

    [TestMethod]
    public void MapFromDicSetNullable_WithFactory_Should_Return_Null_When_Any_Partial_Is_Null()
    {
        var mapperMock = new Mock<IMapperApplier>();
        var src = new Dictionary<string, IppAttribute[]>
        {
            { "testKey", new[] { new IppAttribute(Tag.Integer, "testKey", 1), new IppAttribute(Tag.Integer, "testKey", 2) } }
        };

        mapperMock.Setup(m => m.MapNullable<int?>(1)).Returns(1);
        mapperMock.Setup(m => m.MapNullable<int?>(2)).Returns((int?)null);
        mapperMock.Setup(m => m.MapNullable<string[]?>(null)).Returns((string[]?)null);

        var result = mapperMock.Object.MapFromDicSetNullable<int?, string>(
            src,
            "testKey",
            (_, value) => value!.Value.ToString());

        Assert.IsNull(result);
    }

    [TestMethod]
    public void MapFromDicSetNullable_WithFactory_Should_Return_Null_When_Factory_Returns_Null()
    {
        var mapperMock = new Mock<IMapperApplier>();
        var src = new Dictionary<string, IppAttribute[]>
        {
            { "testKey", new[] { new IppAttribute(Tag.Integer, "testKey", 1) } }
        };

        mapperMock.Setup(m => m.MapNullable<int?>(1)).Returns(1);
        mapperMock.Setup(m => m.MapNullable<string[]?>(null)).Returns((string[]?)null);

        var result = mapperMock.Object.MapFromDicSetNullable<int?, string?>(
            src,
            "testKey",
            (_, _) => null);

        Assert.IsNull(result);
    }

    [TestMethod]
    public void MapFromDicSetNullable_WithFactory_Should_Return_Null_For_Empty_Array()
    {
        var mapperMock = new Mock<IMapperApplier>();
        var src = new Dictionary<string, IppAttribute[]>
        {
            { "testKey", new IppAttribute[0] }
        };

        mapperMock.Setup(m => m.MapNullable<string[]?>(null)).Returns((string[]?)null);
        var factoryCalled = false;

        var result = mapperMock.Object.MapFromDicSetNullable<int?, string?>(
            src,
            "testKey",
            (_, _) =>
            {
                factoryCalled = true;
                return "never";
            });

        Assert.IsNull(result);
        Assert.IsFalse(factoryCalled);
    }

    [TestMethod]
    public void MapFromDicSetNullable_WithFactory_Should_Return_Result_Array()
    {
        var mapperMock = new Mock<IMapperApplier>();
        var src = new Dictionary<string, IppAttribute[]>
        {
            { "testKey", new[] { new IppAttribute(Tag.Integer, "testKey", 3), new IppAttribute(Tag.Integer, "testKey", 4) } }
        };

        mapperMock.Setup(m => m.MapNullable<int?>(3)).Returns(3);
        mapperMock.Setup(m => m.MapNullable<int?>(4)).Returns(4);

        var result = mapperMock.Object.MapFromDicSetNullable<int?, string>(
            src,
            "testKey",
            (_, value) => $"mapped-{value!.Value}");

        CollectionAssert.AreEqual(new[] { "mapped-3", "mapped-4" }, result!);
    }
}
