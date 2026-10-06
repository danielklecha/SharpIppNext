using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections;
using System.Collections.Generic;
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

    [TestMethod]
    public void CreateCollectionMap_Should_Map_Array_To_List()
    {
        var mapper = new SimpleMapper();
        mapper.CreateCollectionMap<int>();

        var result = mapper.Map<List<int>>(new[] { 1, 2, 3 });
        Assert.IsNotNull(result);
        CollectionAssert.AreEqual(new[] { 1, 2, 3 }, result);
    }

    [TestMethod]
    public void CreateCollectionMap_Should_Map_List_To_Array()
    {
        var mapper = new SimpleMapper();
        mapper.CreateCollectionMap<int>();

        var result = mapper.Map<int[]>(new List<int> { 1, 2, 3 });
        Assert.IsNotNull(result);
        CollectionAssert.AreEqual(new[] { 1, 2, 3 }, result);
    }

    [TestMethod]
    public void CreateCollectionMap_Should_Map_Scalar_To_Array()
    {
        var mapper = new SimpleMapper();
        mapper.CreateCollectionMap<int>();

        var result = mapper.Map<int[]>(42);
        Assert.IsNotNull(result);
        CollectionAssert.AreEqual(new[] { 42 }, result);
    }

    [TestMethod]
    public void CreateCollectionMap_Should_Map_IEnumerable_To_List()
    {
        var mapper = new SimpleMapper();
        mapper.CreateCollectionMap<int>();

        var result = mapper.Map<IEnumerable<int>, List<int>>(new TestEnumerable<int>(1, 2));
        Assert.IsNotNull(result);
        CollectionAssert.AreEqual(new[] { 1, 2 }, result);
    }

    [TestMethod]
    public void CreateCollectionMap_Should_Map_IEnumerable_To_Array()
    {
        var mapper = new SimpleMapper();
        mapper.CreateCollectionMap<int>();

        var result = mapper.Map<IEnumerable<int>, int[]>(new TestEnumerable<int>(1, 2));
        Assert.IsNotNull(result);
        CollectionAssert.AreEqual(new[] { 1, 2 }, result);
    }

    [TestMethod]
    public void CreateCollectionMap_Should_Map_Array_To_IEnumerable()
    {
        var mapper = new SimpleMapper();
        mapper.CreateCollectionMap<int>();

        var array = new[] { 1, 2, 3 };
        var result = mapper.Map<IEnumerable<int>>(array);
        Assert.AreSame(array, result);
    }

    [TestMethod]
    public void CreateCollectionMap_Should_Map_List_To_IEnumerable()
    {
        var mapper = new SimpleMapper();
        mapper.CreateCollectionMap<int>();

        var list = new List<int> { 1, 2, 3 };
        var result = mapper.Map<IEnumerable<int>>(list);
        Assert.AreSame(list, result);
    }

    [TestMethod]
    public void CreateCollectionMap_Should_Map_Array_To_IReadOnlyCollection()
    {
        var mapper = new SimpleMapper();
        mapper.CreateCollectionMap<int>();

        var array = new[] { 1, 2, 3 };
        var result = mapper.Map<IReadOnlyCollection<int>>(array);
        Assert.AreSame(array, result);
    }

    [TestMethod]
    public void CreateCollectionMap_Should_Map_Array_To_IReadOnlyList()
    {
        var mapper = new SimpleMapper();
        mapper.CreateCollectionMap<int>();

        var array = new[] { 1, 2, 3 };
        var result = mapper.Map<IReadOnlyList<int>>(array);
        Assert.AreSame(array, result);
    }

    [TestMethod]
    public void CreateCollectionMap_Should_Map_Array_To_IList()
    {
        var mapper = new SimpleMapper();
        mapper.CreateCollectionMap<int>();

        var array = new[] { 1, 2, 3 };
        var result = mapper.Map<IList<int>>(array);
        Assert.AreSame(array, result);
    }

    [TestMethod]
    public void CreateCollectionMap_Should_Map_List_To_IReadOnlyCollection()
    {
        var mapper = new SimpleMapper();
        mapper.CreateCollectionMap<int>();

        var list = new List<int> { 1, 2, 3 };
        var result = mapper.Map<IReadOnlyCollection<int>>(list);
        Assert.AreSame(list, result);
    }

    [TestMethod]
    public void CreateCollectionMap_Should_Map_List_To_IReadOnlyList()
    {
        var mapper = new SimpleMapper();
        mapper.CreateCollectionMap<int>();

        var list = new List<int> { 1, 2, 3 };
        var result = mapper.Map<IReadOnlyList<int>>(list);
        Assert.AreSame(list, result);
    }

    [TestMethod]
    public void CreateCollectionMap_Should_Map_List_To_IList()
    {
        var mapper = new SimpleMapper();
        mapper.CreateCollectionMap<int>();

        var list = new List<int> { 1, 2, 3 };
        var result = mapper.Map<IList<int>>(list);
        Assert.AreSame(list, result);
    }

    [TestMethod]
    public void CreateCollectionMap_Should_Map_ObjectArray_To_Array_With_Null_Item()
    {
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, int>((src, _) => int.Parse(src));
        mapper.CreateCollectionMap<int>();

        var result = mapper.Map<int[]>(new object?[] { "10", null, "20" });
        Assert.IsNotNull(result);
        Assert.AreEqual(3, result.Length);
        CollectionAssert.AreEqual(new[] { 10, 0, 20 }, result);

        var mapperRef = new SimpleMapper();
        mapperRef.CreateMap<int, string>((src, _) => src.ToString());
        mapperRef.CreateCollectionMap<string>();

        var resultRef = mapperRef.Map<string[]>(new object?[] { 10, null, 20 });
        Assert.IsNotNull(resultRef);
        Assert.AreEqual(3, resultRef.Length);
        CollectionAssert.AreEqual(new string?[] { "10", null, "20" }, resultRef);
    }

    [TestMethod]
    public void CreateCollectionMap_Should_Map_ObjectArray_To_List_With_Null_Item()
    {
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, int>((src, _) => int.Parse(src));
        mapper.CreateCollectionMap<int>();

        var result = mapper.Map<List<int>>(new object?[] { "10", null, "20" });
        Assert.IsNotNull(result);
        Assert.AreEqual(3, result.Count);
        CollectionAssert.AreEqual(new[] { 10, 0, 20 }, result);

        var mapperRef = new SimpleMapper();
        mapperRef.CreateMap<int, string>((src, _) => src.ToString());
        mapperRef.CreateCollectionMap<string>();

        var resultRef = mapperRef.Map<List<string>>(new object?[] { 10, null, 20 });
        Assert.IsNotNull(resultRef);
        Assert.AreEqual(3, resultRef.Count);
        CollectionAssert.AreEqual(new string?[] { "10", null, "20" }, resultRef);
    }

    [TestMethod]
    public void CreateCollectionMap_Should_Map_ObjectArray_To_Collection_Interfaces()
    {
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, int>((src, _) => int.Parse(src));
        mapper.CreateCollectionMap<int>();

        var src = new object[] { "1", "2" };

        var ienumerable = mapper.Map<IEnumerable<int>>(src);
        Assert.IsNotNull(ienumerable);
        CollectionAssert.AreEqual(new[] { 1, 2 }, ienumerable.ToList());

        var readOnlyColl = mapper.Map<IReadOnlyCollection<int>>(src);
        Assert.IsNotNull(readOnlyColl);
        CollectionAssert.AreEqual(new[] { 1, 2 }, readOnlyColl.ToList());

        var readOnlyList = mapper.Map<IReadOnlyList<int>>(src);
        Assert.IsNotNull(readOnlyList);
        CollectionAssert.AreEqual(new[] { 1, 2 }, readOnlyList.ToList());

        var ilist = mapper.Map<IList<int>>(src);
        Assert.IsNotNull(ilist);
        CollectionAssert.AreEqual(new[] { 1, 2 }, ilist.ToList());
    }

    private sealed class TestEnumerable<T>(params T[] items) : IEnumerable<T>
    {
        public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)items).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
