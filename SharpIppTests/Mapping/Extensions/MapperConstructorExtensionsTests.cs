using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SharpIppTests.Mapping.Extensions;

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
    public void CreateIppMap_Should_Register_Object_To_Destination_Mapping()
    {
        // Arrange
        var mapperMock = new Mock<IMapperConstructor>();
        Func<object, IMapperApplier, int>? capturedFunc = null;

        // The method registers: mapper.CreateMap<object, TDestination>((src, map) => ...);
        // We need to match this call.
        // It's likely resolving to CreateMap<TSource, TDest>(Func<TSource, IMapperApplier, TDest>) where TSource=object, TDest=int
        mapperMock.Setup(x => x.CreateMap(It.IsAny<Func<object, IMapperApplier, int>>()))
            .Callback<Func<object, IMapperApplier, int>>(func => capturedFunc = func);

        // Act
        mapperMock.Object.CreateIppMap<string, int>((src, map) => int.Parse(src));

        // Assert
        Assert.IsNotNull(capturedFunc);

        // Test Valid Case
        var mapperApplierMock = new Mock<IMapperApplier>();
        mapperApplierMock.Setup(x => x.Map<int>("123")).Returns(123);
        var result = capturedFunc!("123", mapperApplierMock.Object);
        Assert.AreEqual(123, result);

        // Test Null Case
        var exNull = Assert.ThrowsException<ArgumentException>(() => capturedFunc!(null!, mapperApplierMock.Object));
        Assert.IsTrue(exNull.Message.Contains("Mapping null to non nullable type"));

        // Test Wrong Type
        var exType = Assert.ThrowsException<ArgumentException>(() => capturedFunc!(123, mapperApplierMock.Object)); // 123 is int, expecting string
        Assert.IsTrue(exType.Message.Contains("Mapping not supported"));
    }

    [TestMethod]
    public void CreateIppMap_Should_Register_NoValue_To_Array_Mapping()
    {
        // Arrange
        var mapperMock = new Mock<IMapperConstructor>();
        Func<NoValue, IMapperApplier, int[]?>? capturedFunc = null;
        mapperMock.Setup(x => x.CreateMap(It.IsAny<Func<NoValue, IMapperApplier, int[]?>>()))
            .Callback<Func<NoValue, IMapperApplier, int[]?>>(func => capturedFunc = func);

        // Act
        mapperMock.Object.CreateIppMap<string, int>((src, map) => int.Parse(src));

        // Assert
        Assert.IsNotNull(capturedFunc);
        var result = capturedFunc!(NoValue.Instance, Mock.Of<IMapperApplier>());
        Assert.IsNull(result);
    }

    [TestMethod]
    public void CreateIppMap_Should_Register_Source_To_Array_Mappings()
    {
        // Arrange
        var mapperMock = new Mock<IMapperConstructor>();
        var capturedFuncs = new System.Collections.Generic.List<Func<string, IMapperApplier, int[]>>();
        
        mapperMock.Setup(x => x.CreateMap(It.IsAny<Func<string, IMapperApplier, int[]>>()))
            .Callback<Func<string, IMapperApplier, int[]>>(func => capturedFuncs.Add(func));

        // Act
        mapperMock.Object.CreateIppMap<string, int>((src, map) => int.Parse(src));

        // Assert
        // We expect 2 mappings:
        // 1. TSource -> TDestination[] (Strict)
        // 2. TSource -> TDestination[]? (Nullable)
        Assert.AreEqual(2, capturedFuncs.Count);
        
        var strictFunc = capturedFuncs[0];
        var nullableFunc = capturedFuncs[1];
        
        var mapperApplierMock = new Mock<IMapperApplier>();
        mapperApplierMock.Setup(x => x.Map<int[]>(It.IsAny<string[]>()))
            .Returns((string[] s) => s.Select(int.Parse).ToArray());
        mapperApplierMock.Setup(x => x.Map<int[]?>(It.IsAny<string[]>()))
             .Returns((string[] s) => s.Select(int.Parse).ToArray());

        // 1. Strict
        // Valid
        var resultStrict = strictFunc("123", mapperApplierMock.Object);
        Assert.AreEqual(1, resultStrict.Length);
        Assert.AreEqual(123, resultStrict[0]);
        // Null -> Throws
        var ex = Assert.ThrowsException<ArgumentException>(() => strictFunc(null!, mapperApplierMock.Object));
        Assert.IsTrue(ex.Message.Contains("Mapping null to non nullable type"));

        // 2. Nullable
        // Valid
        var resultNullable = nullableFunc("123", mapperApplierMock.Object);
        Assert.IsNotNull(resultNullable);
        Assert.AreEqual(123, resultNullable[0]);
        // Null -> Returns Null
        var resultNull = nullableFunc(null!, mapperApplierMock.Object);
        Assert.IsNull(resultNull);
    }

    [TestMethod]
    public void CreateIppMap_Should_Register_SourceArray_To_DestinationArray_Mappings()
    {
        // Arrange
        var mapperMock = new Mock<IMapperConstructor>();
        var capturedFuncs = new System.Collections.Generic.List<Func<string[], IMapperApplier, int[]>>();
        
        // This setup captures:
        // TSource[] -> TDestination[]
        // TSource[] -> TDestination[]?
        // TSource[]? -> TDestination[]?
        mapperMock.Setup(x => x.CreateMap(It.IsAny<Func<string[], IMapperApplier, int[]>>()))
            .Callback<Func<string[], IMapperApplier, int[]>>(func => capturedFuncs.Add(func));

        // Act
        mapperMock.Object.CreateIppMap<string, int>((src, map) => int.Parse(src));

        // Assert
        Assert.AreEqual(3, capturedFuncs.Count);
        
        var func1 = capturedFuncs[0]; // TSource[] -> TDestination[]
        var func2 = capturedFuncs[1]; // TSource[] -> TDestination[]?
        var func3 = capturedFuncs[2]; // TSource[]? -> TDestination[]?
        
        var mapperApplierMock = new Mock<IMapperApplier>();
        mapperApplierMock.Setup(x => x.Map<int>("1")).Returns(1);
        
        // Func 1: TSource[] -> TDestination[]
        var res1 = func1(new[] { "1" }, mapperApplierMock.Object);
        Assert.AreEqual(1, res1.Length);
        Assert.AreEqual(1, res1[0]);
        // If src is null? The lambda is src.Select(...) -> Throws NullReferenceException or similar if not checked?
        // Code: src.Select(...).ToArray(). Enumerable.Select throws on null source.
        Assert.ThrowsException<ArgumentNullException>(() => func1(null!, mapperApplierMock.Object));

        // Func 2: TSource[] -> TDestination[]?
        var res2 = func2(new[] { "1" }, mapperApplierMock.Object);
        Assert.AreEqual(1, res2.Length);
        // Src null? Code: src.Select(...) -> Throws
        Assert.ThrowsException<ArgumentNullException>(() => func2(null!, mapperApplierMock.Object));

        // Func 3: TSource[]? -> TDestination[]?
        var res3 = func3(new[] { "1" }, mapperApplierMock.Object);
        Assert.AreEqual(1, res3.Length);
        // Src null? Code: src?.Select(...) -> Returns null?
        // Wait, line 81: src?.Select(...).ToArray()
        // If src is null, returns null.
        var res3Null = func3(null!, mapperApplierMock.Object);
        Assert.IsNull(res3Null);
    }

    [TestMethod]
    public void CreateIppMap_Should_Register_Nullable_Destination_Logic()
    {
        // Arrange
        var mapperMock = new Mock<IMapperConstructor>();

        // We are looking for CreateMap(Type, Type, Func<object, IMapperApplier, object>)
        Func<object, IMapperApplier, object>? capturedFunc = null;
        Type? capturedSrcType = null;
        Type? capturedDestType = null;

        mapperMock.Setup(x => x.CreateMap(It.IsAny<Type>(), It.IsAny<Type>(), It.IsAny<Func<object, IMapperApplier, object>>()))
            .Callback<Type, Type, Func<object, IMapperApplier, object>>((src, dest, func) =>
            {
                // We are interested in the explicit Type registration: mapper.CreateMap(srcType, destNullable, ...)
                if (src == typeof(int) && dest == typeof(double?))
                {
                    capturedSrcType = src;
                    capturedDestType = dest;
                    capturedFunc = func;
                }
            });

        // Act
        mapperMock.Object.CreateIppMap<int, double>((src, map) => (double)src);

        // Assert
        Assert.IsNotNull(capturedFunc, "Should register TSource -> TDest?");
        Assert.AreEqual(typeof(int), capturedSrcType);
        Assert.AreEqual(typeof(double?), capturedDestType);

        // Test Logic
        var mapperApplierMock = new Mock<IMapperApplier>();
        mapperApplierMock.Setup(x => x.Map<double>(123)).Returns(123.0);

        // Valid
        // The lambda is: (src, map) => src == null ? destNull : map.Map<TDestination>(src)
        var result = capturedFunc!(123, mapperApplierMock.Object);
        Assert.AreEqual(123.0, result);

        // Null
        var resultNull = capturedFunc!(null!, mapperApplierMock.Object);
        Assert.IsNull(resultNull);
    }
    [TestMethod]
    public void CreateIppMap_Should_Register_ObjectArray_To_DestinationArray_Mappings()
    {
        // Arrange
        var mapperMock = new Mock<IMapperConstructor>();

        // At runtime, int[] and int[]? are the same type.
        // So CreateMap<object[], int[]> and CreateMap<object[]?, int[]?> map to the same generic method instantiation.
        // We capture all calls.
        var capturedFuncs = new System.Collections.Generic.List<Func<object[], IMapperApplier, int[]?>>();

        mapperMock.Setup(x => x.CreateMap(It.IsAny<Func<object[], IMapperApplier, int[]?>>()))
            .Callback<Func<object[], IMapperApplier, int[]?>>(f => capturedFuncs.Add(f));

        // Act
        mapperMock.Object.CreateIppMap<string, int>((src, map) => int.Parse(src));

        // Assert
        // Assert
        // We expect 3 calls (lines 83, 84, 85):
        // 1. object[] -> int[]
        // 2. object[] -> int[]?
        // 3. object[]? -> int[]?
        Assert.AreEqual(3, capturedFuncs.Count);
        
        var func1 = capturedFuncs[0];
        var func2 = capturedFuncs[1];
        var func3 = capturedFuncs[2];

        var mapperApplierMock = new Mock<IMapperApplier>();
        mapperApplierMock.Setup(x => x.Map<int>(It.IsAny<object>())).Returns(1);

        // Func 1: object[] -> TDestination[]
        var res1 = func1(new object[] { "1" }, mapperApplierMock.Object);
        Assert.AreEqual(1, res1.Length);
        Assert.AreEqual(1, res1[0]);

        // Func 2: object[] -> TDestination[]?
        var res2 = func2(new object[] { "1" }, mapperApplierMock.Object);
        Assert.IsNotNull(res2);
        Assert.AreEqual(1, res2.Length);

        // Func 3: object[]? -> TDestination[]?
        // Note: func3 expects source to be treated as object[]? but at runtime it is object[]
        // func3 takes object[], so passing null might throw if not careful, but the lambda handles nulls
        // The lambda signature inferred by compiler is Func<object[]?, ...> which is compatible with Func<object[], ...> at runtime
        var res3 = func3(new object[] { "1" }, mapperApplierMock.Object);
        Assert.IsNotNull(res3);
        Assert.AreEqual(1, res3.Length);

        var res3Null = func3(null, mapperApplierMock.Object);
        Assert.IsNull(res3Null);
    }
}
