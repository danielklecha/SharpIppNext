using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SharpIpp.Tests.Unit.Mapping.Extensions;

[TestClass]
[ExcludeFromCodeCoverage]
public class MapperConstructorExtensionsTests
{
    [TestMethod]
    public void CreateIppMap_Should_Register_Identity_Mapping()
    {
        // Arrange
        var mapperMock = new Mock<IMapperConstructor>();
        Func<int, IMapperApplier, int>? registeredMapFunc = null;

        mapperMock.Setup(x => x.CreateMap(It.IsAny<Func<int, IMapperApplier, int>>()))
            .Callback<Func<int, IMapperApplier, int>>(func => registeredMapFunc = func);

        // Act
        mapperMock.Object.CreateIppMap<int>();

        // Assert
        Assert.IsNotNull(registeredMapFunc);
        var result = registeredMapFunc!(42, Mock.Of<IMapperApplier>());
        Assert.AreEqual(42, result);
    }

    [TestMethod]
    public void CreateIppMap_Should_Register_Mapping()
    {
        // Arrange
        var mapperMock = new Mock<IMapperConstructor>();
        Func<string, IMapperApplier, int>? capturedFunc = null;

        mapperMock.Setup(x => x.CreateMap(It.IsAny<Func<string, IMapperApplier, int>>()))
            .Callback<Func<string, IMapperApplier, int>>(func => capturedFunc = func);

        // Act
        mapperMock.Object.CreateIppMap<string, int>((src, map) => int.Parse(src));

        // Assert
        Assert.IsNotNull(capturedFunc);
        var result = capturedFunc("123", Mock.Of<IMapperApplier>());
        Assert.AreEqual(123, result);
    }

    [TestMethod]
    public void SimpleMapper_Should_Promote_Scalar_To_Array()
    {
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, int>((src, _) => int.Parse(src));

        // Same type promotion: int -> int[]
        var directResult = mapper.Map<int[]>(42);
        Assert.IsNotNull(directResult);
        Assert.AreEqual(1, directResult.Length);
        Assert.AreEqual(42, directResult[0]);

        // Mapped scalar promotion: string -> int[]
        var mappedResult = mapper.Map<int[]>("123");
        Assert.IsNotNull(mappedResult);
        Assert.AreEqual(1, mappedResult.Length);
        Assert.AreEqual(123, mappedResult[0]);
    }

    [TestMethod]
    public void SimpleMapper_Should_Map_Array_To_Array()
    {
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, int>((src, _) => int.Parse(src));

        var result = mapper.Map<int[]>(new[] { "1", "2", "3" });
        Assert.IsNotNull(result);
        Assert.AreEqual(3, result.Length);
        CollectionAssert.AreEqual(new[] { 1, 2, 3 }, result);
    }

    [TestMethod]
    public void SimpleMapper_Should_Map_ObjectArray_To_Array()
    {
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, int>((src, _) => int.Parse(src));

        var result = mapper.Map<int[]>(new object[] { "1", "2" });
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Length);
        CollectionAssert.AreEqual(new[] { 1, 2 }, result);
    }

    [TestMethod]
    public void SimpleMapper_Should_Map_NoValue_To_Arrays_As_Null()
    {
        var mapper = new SimpleMapper();

        var intArray = mapper.MapNullable<int[]>(NoValue.Instance);
        Assert.IsNull(intArray);

        var stringArray = mapper.MapNullable<string[]>(NoValue.Instance);
        Assert.IsNull(stringArray);

        var rangeArray = mapper.MapNullable<SharpIpp.Protocol.Models.Range[]>(NoValue.Instance);
        Assert.IsNull(rangeArray);

        var enumerable = mapper.MapNullable<System.Collections.Generic.IEnumerable<int>>(NoValue.Instance);
        Assert.IsNull(enumerable);
    }

    [TestMethod]
    public void RegisterGeneratedProfiles_Should_Register_All_Mappings()
    {
        var mapper = new SimpleMapper();
        mapper.RegisterGeneratedProfiles();

        var uri = mapper.Map<Uri>("http://localhost:631");
        Assert.IsNotNull(uri);
        Assert.AreEqual("http://localhost:631/", uri.ToString());

        var requestMessage = mapper.Map<IppRequestMessage>(new SharpIpp.Models.Requests.GetPrinterAttributesRequest
        {
            Version = new IppVersion(1, 1),
            RequestId = 42
        });
        Assert.IsNotNull(requestMessage);
        Assert.AreEqual(42, requestMessage.RequestId);

        var response = mapper.Map<SharpIpp.Models.Responses.GetPrinterAttributesResponse>(new IppResponseMessage
        {
            Version = new IppVersion(1, 1),
            RequestId = 42,
            StatusCode = IppStatusCode.SuccessfulOk
        });
        Assert.IsNotNull(response);
        Assert.AreEqual(42, response.RequestId);
        Assert.AreEqual(IppStatusCode.SuccessfulOk, response.StatusCode);
    }
}
