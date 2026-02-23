using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SharpIppTests.Mapping.Extensions;

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
        mapperMock.Setup(m => m.MapNullable<int[]?>(NoValue.Instance)).Returns((int[]?)null);
        var src = new Dictionary<string, IppAttribute[]>();

        var result = mapperMock.Object.MapFromDicSetNullable<int[]?>(src, "testKey");
        Assert.IsNull(result);
    }

    [TestMethod]
    public void MapFromDicSetNullable_Should_Return_NoValue_For_Empty_Array()
    {
        var mapperMock = new Mock<IMapperApplier>();
        mapperMock.Setup(m => m.MapNullable<int[]?>(NoValue.Instance)).Returns((int[]?)null);
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
}
