using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Mapping;

[TestClass]
[ExcludeFromCodeCoverage]
public class SimpleMapperTests
{
    [TestMethod]
    public void Map_SimpleTypes_ShouldWork()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, int>((src, m) => int.Parse(src));

        // Act
        var result = mapper.Map<int>("123");

        // Assert
        result.Should().Be(123);
    }

    [TestMethod]
    public void Map_Interface_ShouldWork()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<IMyInterface, string?>((src, m) => src.Value);
        var source = new MyImplementation { Value = "test" };

        // Act
        var result = mapper.Map<IMyInterface, string?>(source);

        // Assert
        result.Should().Be("test");
    }

    [TestMethod]
    public void Map_NoMappingFound_ShouldThrowArgumentException()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        Action act = () => mapper.Map<int>("test");

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*No mapping found*");
    }

    [TestMethod]
    public void Map_NullSource_ShouldReturnDefault()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        var result = mapper.Map<int>((object?)null);

        // Assert
        result.Should().Be(default(int));
    }

    [TestMethod]
    public void Map_NullSource_WithGenericOverload_ShouldReturnDestination()
    {
        // Arrange
        var mapper = new SimpleMapper();
        var dest = 123;

        // Act
        var result = mapper.Map<string, int>(null, dest);

        // Assert
        result.Should().Be(dest);
    }

    [TestMethod]
    public void Map_Object_NullSource_ThrowsArgumentException()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        Action act = () => mapper.Map<string>((object?)null);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Cannot map null source to non-nullable destination without a default destination.");
    }

    [TestMethod]
    public void Map_Object_NullSourceAndNullDest_ThrowsArgumentException()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        Action act = () => mapper.Map<string>((object?)null, (string?)null);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Cannot map null source to non-nullable destination without a default destination.");
    }

    [TestMethod]
    public void Map_Generic_NullSource_ThrowsArgumentException()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        Action act = () => mapper.Map<string, string>((string?)null);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Cannot map null source to non-nullable destination without a default destination.");
    }

    [TestMethod]
    public void Map_Generic_NullSourceAndNullDest_ThrowsArgumentException()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        Action act = () => mapper.Map<string, string>((string?)null, (string?)null);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Cannot map null source to non-nullable destination without a default destination.");
    }

    [TestMethod]
    public void Map_Object_ValidSource_WithDest_ShouldReturnMappedValue()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, int>((src, m) => int.Parse(src));
        var dest = 0;

        // Act
        var result = mapper.Map<int>((object)"123", dest);

        // Assert
        result.Should().Be(123);
    }

    [TestMethod]
    public void Map_Object_NullSource_WithDest_ShouldReturnDest()
    {
        // Arrange
        var mapper = new SimpleMapper();
        var dest = "default_value";

        // Act
        var result = mapper.Map<string>((object?)null, dest);

        // Assert
        result.Should().Be(dest);
    }

    private interface IMyInterface { string? Value { get; } }
    private class MyImplementation : IMyInterface { public string? Value { get; set; } }
    private class TestBaseClass { public string? Value { get; set; } }
    private class TestDerivedClass : TestBaseClass { }

    [TestMethod]
    public void Map_Object_ValidSource_WithNullDest_ShouldReturnMappedValue()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<int, string>((src, m) => src.ToString());

        // Act
        var result = mapper.Map<string>((object)123, null);

        // Assert
        result.Should().Be("123");
    }

    [TestMethod]
    public void Map_Generic_ValidSource_WithNullDest_ShouldReturnMappedValue()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<int, string>((src, m) => src.ToString());

        // Act
        var result = mapper.Map<int, string>(123, null);

        // Assert
        result.Should().Be("123");
    }

    [TestMethod]
    public void CreateMap_ThreeArgFunc_ReceivesNullDest()
    {
        // Arrange
        var mapper = new SimpleMapper();
        string? receivedDest = "not_null";
        bool wasCalled = false;
        mapper.CreateMap<int, string>((src, dst, m) =>
        {
            receivedDest = dst;
            wasCalled = true;
            return src.ToString();
        });

        // Act
        var result = mapper.MapNullable<string>((object)123, null);

        // Assert
        wasCalled.Should().BeTrue();
        receivedDest.Should().BeNull();
        result.Should().Be("123");
    }

    [TestMethod]
    public void CreateMap_ThreeArgFunc_ReceivesProvidedDest()
    {
        // Arrange
        var mapper = new SimpleMapper();
        string? receivedDest = null;
        mapper.CreateMap<int, string>((src, dst, m) =>
        {
            receivedDest = dst;
            return src + "_" + (dst ?? "none");
        });

        // Act
        var result = mapper.Map<string>((object)42, "existing");

        // Assert
        receivedDest.Should().Be("existing");
        result.Should().Be("42_existing");
    }


    [TestMethod]
    public void MapNullable_WithSourceType_NonNullSource_ShouldReturnMappedValue()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<int, string>((src, m) => src.ToString());

        // Act
        var result = mapper.MapNullable<string>((object)123, typeof(int), "default");

        // Assert
        result.Should().Be("123");
    }

    [TestMethod]
    public void MapNullable_Generic_ShouldReturnMappedValue()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<int, string>((src, m) => src.ToString());

        // Act
        var result = mapper.MapNullable<int, string>(123);

        // Assert
        result.Should().Be("123");
    }

    [TestMethod]
    public void MapNullable_Generic_WithDest_ShouldReturnMappedValue()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<int, string>((src, m) => src.ToString());

        // Act
        var result = mapper.MapNullable<int, string>(123, "default");

        // Assert
        result.Should().Be("123");
    }

    [TestMethod]
    public void Map_SourceImplementsInterface_ToNullableValueType_WhenInterfaceMappedToUnderlying_ShouldWork()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<IMyInterface, int>((src, m) => int.Parse(src.Value!));
        var source = new MyImplementation { Value = "42" };

        // Act
        var result = mapper.Map<IMyInterface, int?>(source);

        // Assert
        result.Should().Be(42);
    }

    [TestMethod]
    public void Map_SourceInheritsBaseClass_ToDestination_WhenBaseClassMapped_ShouldWork()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<TestBaseClass, string>((src, m) => src.Value!);
        var source = new TestDerivedClass { Value = "base_test" };

        // Act
        var result = mapper.Map<TestBaseClass, string>(source);

        // Assert
        result.Should().Be("base_test");
    }

    [TestMethod]
    public void Map_SourceInheritsBaseClass_ToNullableValueType_WhenBaseClassMappedToUnderlying_ShouldWork()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<TestBaseClass, int>((src, m) => int.Parse(src.Value!));
        var source = new TestDerivedClass { Value = "84" };

        // Act
        var result = mapper.Map<TestBaseClass, int?>(source);

        // Assert
        result.Should().Be(84);
    }

    [TestMethod]
    public void Map_ToDestinationInheritingBaseClass_WhenSourceMappedToBaseClass_ShouldWork()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, TestDerivedClass>((src, m) => new TestDerivedClass { Value = src });

        // Act
        var result = mapper.Map<TestDerivedClass>("derived_dest");

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().Be("derived_dest");
    }

    [TestMethod]
    public void Map_ToDestinationImplementingInterface_WhenSourceMappedToInterface_ShouldWork()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, MyImplementation>((src, m) => new MyImplementation { Value = src });

        // Act
        var result = mapper.Map<MyImplementation>("interface_dest");

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().Be("interface_dest");
    }
    [TestMethod]
    public void MapNullable_WithSourceType_NullSource_ReturnsDest()
    {
        // Arrange
        var mapper = new SimpleMapper();
        var dest = "default";

        // Act
        var result = mapper.MapNullable<string>(null, typeof(int), dest);

        // Assert
        result.Should().Be(dest);
    }

    [TestMethod]
    public void Map_NonGeneric_ShouldWork()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, int>((src, m) => int.Parse(src));

        // Act
        var result = mapper.Map("123", destType: typeof(int));

        // Assert
        result.Should().Be(123);
    }

    [TestMethod]
    public void Map_NonGeneric_NullSource_ShouldThrow()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        Action act = () => mapper.Map(null, destType: typeof(int));

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Cannot map null source to non-nullable destination without a default destination.");
    }

    [TestMethod]
    public void Map_NonGeneric_WithSourceAndDest_ShouldWork()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, int>((src, dst, m) => int.Parse(src) + (int?)dst ?? 0);

        // Act
        var result = mapper.Map("100", sourceType: typeof(string), destType: typeof(int), dest: 23);

        // Assert
        result.Should().Be(123);
    }

    [TestMethod]
    public void Map_NonGeneric_WithSourceAndDest_NullSourceAndDest_ShouldThrow()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        Action act = () => mapper.Map(null, sourceType: typeof(string), destType: typeof(int), dest: null);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Cannot map null source to non-nullable destination without a default destination.");
    }

    [TestMethod]
    public void Map_NonGeneric_WithSourceTypeAndDestType_ValidSource_ShouldReturnMappedValue()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, int>((src, m) => int.Parse(src));

        // Act
        var result = mapper.Map("123", sourceType: typeof(string), destType: typeof(int));

        // Assert
        result.Should().Be(123);
    }

    [TestMethod]
    public void Map_NonGeneric_WithSourceTypeAndDestType_NullSource_ShouldThrow()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        Action act = () => mapper.Map(null, sourceType: typeof(string), destType: typeof(int));

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Cannot map null source to non-nullable destination without a default destination.");
    }

    [TestMethod]
    public void Map_NonGeneric_WithSourceTypeAndDestType_MappedToNull_ShouldThrow()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap(typeof(string), typeof(int), (src, m) => null!);

        // Act
        Action act = () => mapper.Map("hello", sourceType: typeof(string), destType: typeof(int));

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Cannot map null source to non-nullable destination without a default destination.");
    }

    [TestMethod]
    public void MapNullable_NonGeneric_ShouldWork()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, int>((src, m) => int.Parse(src));

        // Act
        var result = mapper.MapNullable("123", destType: typeof(int));

        // Assert
        result.Should().Be(123);
    }

    [TestMethod]
    public void MapNullable_NonGeneric_NullSource_ShouldReturnNull()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        var result = mapper.MapNullable(null, destType: typeof(int));

        // Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void MapNullable_NonGeneric_WithSourceAndDest_NullSource_ShouldReturnDest()
    {
        // Arrange
        var mapper = new SimpleMapper();
        var dest = 456;

        // Act
        var result = mapper.MapNullable(null, sourceType: typeof(string), destType: typeof(int), dest: dest);

        // Assert
        result.Should().Be(dest);
    }

    [TestMethod]
    public void MapNullable_NonGeneric_WithSourceType_ShouldWork()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, int>((src, m) => int.Parse(src));

        // Act
        var result = mapper.MapNullable("123", sourceType: typeof(string), destType: typeof(int));

        // Assert
        result.Should().Be(123);
    }

    [TestMethod]
    public void Map_NullableDestination_ShouldUseUnderlyingDestinationMap_WhenDirectNullableMapMissing()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, int>((src, m) => int.Parse(src));

        // Act
        var result = mapper.Map<int?>("123");

        // Assert
        result.Should().Be(123);
    }

    [TestMethod]
    public void Map_EnumerableSource_ToArrayDestination_ShouldWork()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, int>((src, m) => int.Parse(src));
        mapper.CreateCollectionMap<int>();

        // Act
        var result = mapper.Map<int[]>(new object[] { "1", "2" });

        // Assert
        result.Should().Equal(1, 2);
    }

    [TestMethod]
    public void Map_EnumerableSource_ToListDestination_ShouldWork()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, int>((src, m) => int.Parse(src));
        mapper.CreateCollectionMap<int>();

        // Act
        var result = mapper.Map<List<int>>(new object[] { "3", "4" });

        // Assert
        result.Should().Equal(3, 4);
    }

    [TestMethod]
    public void Map_EnumerableSource_ToIEnumerableDestination_ShouldWork()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, int>((src, m) => int.Parse(src));
        mapper.CreateCollectionMap<int>();

        // Act
        var result = mapper.Map<IEnumerable<int>>(new object[] { "5", "6" });

        // Assert
        result.Should().Equal(5, 6);
        result.Should().BeAssignableTo<List<int>>();
    }

    [TestMethod]
    public void Map_EnumerableSource_ToArray_WithNullValue_ForValueType_ShouldMapToDefault()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, int>((src, m) => int.Parse(src));
        mapper.CreateCollectionMap<int>();

        // Act
        var result = mapper.Map<int[]>(new object?[] { "7", null });

        // Assert
        result.Should().Equal(7, 0);
    }

    [TestMethod]
    public void Map_EnumerableSource_ToList_WithNullValue_ForValueType_ShouldMapToDefault()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, int>((src, m) => int.Parse(src));
        mapper.CreateCollectionMap<int>();

        // Act
        var result = mapper.Map<List<int>>(new object?[] { "8", null });

        // Assert
        result.Should().Equal(8, 0);
    }

    [TestMethod]
    public void Map_EnumerableSource_ToConcreteCollectionWithEnumerableCtor_ShouldWork()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, int>((src, m) => int.Parse(src));

        // Act
        Action act = () => mapper.Map<HashSet<int>>(new object[] { "9", "10", "10" });

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*No mapping found*");
    }

    [TestMethod]
    public void Map_EnumerableSource_ToConcreteIListWithDefaultCtor_ShouldWork()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, int>((src, m) => int.Parse(src));

        // Act
        Action act = () => mapper.Map<Collection<int>>(new object[] { "11", "12" });

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*No mapping found*");
    }

    [TestMethod]
    public void Map_StringSource_ToCollectionDestination_ShouldThrow()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        Action act = () => mapper.Map<List<int>>("not_a_collection");

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*No mapping found*");
    }

    [TestMethod]
    public void Map_NonEnumerableSource_ToCollectionDestination_ShouldThrow()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        Action act = () => mapper.Map<List<int>>(123);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*No mapping found*");
    }

    [TestMethod]
    public void Map_EnumerableSource_ToUnsupportedGenericCollectionDestination_ShouldThrow()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        Action act = () => mapper.Map<Dictionary<int, int>>(new[] { 1, 2 });

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*No mapping found*");
    }

    [TestMethod]
    public void Map_EnumerableSource_ToCollectionInterfaceWithoutConcreteConversion_ShouldThrow()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, int>((src, m) => int.Parse(src));

        // Act
        Action act = () => mapper.Map<ISet<int>>(new object[] { "13", "14" });

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*No mapping found*");
    }

    [TestMethod]
    public void Map_EnumerableSource_ToCustomNonGenericTypeImplementingIEnumerableOfT_ShouldWork()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, int>((src, m) => int.Parse(src));

        // Act
        Action act = () => mapper.Map<CustomIntEnumerable>(new object[] { "15", "16" });

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*No mapping found*");
    }

    [TestMethod]
    public void Map_EnumerableSource_ToGenericIListConcreteTypeWithoutEnumerableCtor_ShouldWork()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, int>((src, m) => int.Parse(src));

        // Act
        Action act = () => mapper.Map<CustomDefaultCtorOnlyList<int>>(new object[] { "17", "18" });

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*No mapping found*");
    }

    [TestMethod]
    public void Map_EnumerableSource_ToGenericIListConcreteTypeWithoutDefaultCtor_ShouldThrow()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, int>((src, m) => int.Parse(src));

        // Act
        Action act = () => mapper.Map<CustomNoDefaultCtorList<int>>(new object[] { "19", "20" });

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*No mapping found*");
    }

    [TestMethod]
    public void Map_EnumerableSource_ToGenericCollectionTypeNotIListWithoutEnumerableCtor_ShouldThrow()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, int>((src, m) => int.Parse(src));

        // Act
        Action act = () => mapper.Map<CustomCollectionNoEnumerableCtor<int>>(new object[] { "21", "22" });

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*No mapping found*");
    }

    [TestMethod]
    public void Map_EnumerableSource_ToNonGenericCollectionType_ShouldThrow()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        Action act = () => mapper.Map<System.Collections.ArrayList>(new[] { 1, 2 });

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*No mapping found*");
    }

    [TestMethod]
    public void Map_ValueTypeToNullableValueType_ShouldCastDirectly()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        var result = mapper.Map<int, int?>(42);

        // Assert
        result.Should().Be(42);
    }

    [TestMethod]
    public void Map_ObjectValueTypeToNullableValueType_ShouldCastDirectly()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        var result = mapper.Map<int?>((object)42);

        // Assert
        result.Should().Be(42);
    }

    [TestMethod]
    public void Map_TypeOverload_ValueTypeToNullableValueType_ShouldCastDirectly()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        var result = mapper.Map(42, typeof(int), typeof(int?));

        // Assert
        result.Should().Be(42);
    }

    [TestMethod]
    public void Map_ValueTypeToNullableValueType_SubsequentCallsUseCache()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        var first = mapper.Map<int, int?>(42);
        var second = mapper.Map<int, int?>(84);

        // Assert
        first.Should().Be(42);
        second.Should().Be(84);
    }

    [TestMethod]
    public void Map_EnumerableValueTypeToNullableValueType_ShouldCastElements()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<int, int?>((src, _) => src);
        mapper.CreateCollectionMap<int?>();
        mapper.CreateMap<int[], List<int?>>((src, map) => src.Select(x => map.Map<int?>(x)).ToList());

        // Act
        var result = mapper.Map<List<int?>>(new[] { 1, 2, 3 });

        // Assert
        result.Should().Equal(1, 2, 3);
    }

    [TestMethod]
    public void Map_SingleItem_ToArrayDestination_WhenSameType_ShouldWrapInArray()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateCollectionMap<int>();
        mapper.CreateCollectionMap<string>();

        // Act
        var intResult = mapper.Map<int[]>(42);
        var strResult = mapper.Map<string[]>("test");

        // Assert
        intResult.Should().Equal(42);
        strResult.Should().Equal("test");
    }

    [TestMethod]
    public void Map_SingleItem_ToArrayDestination_WhenNoMappingExists_ShouldThrow()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        Action act = () => mapper.Map<int[]>("cannot_map");

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*No mapping found*");
    }

    [TestMethod]
    public void Map_StringSource_WithEnumerableSourceType_ToCollectionDestination_ShouldThrow()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        Action act = () => mapper.Map("not_a_collection", typeof(System.Collections.IEnumerable), typeof(int[]));

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*No mapping found*");
    }

    [TestMethod]
    public void MapNullable_StringSource_WithEnumerableSourceType_ToCollectionDestination_ShouldThrow()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        Action act = () => mapper.MapNullable("not_a_collection", typeof(System.Collections.IEnumerable), typeof(int[]));

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*No mapping found*");
    }

    [TestMethod]
    public void Map_NonEnumerableSource_WithEnumerableSourceType_ToCollectionDestination_ShouldThrow()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        Action act = () => mapper.Map(123, typeof(System.Collections.IEnumerable), typeof(int[]));

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*No mapping found*");
    }

    [TestMethod]
    public void MapNullable_NonEnumerableSource_WithEnumerableSourceType_ToCollectionDestination_ShouldThrow()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        Action act = () => mapper.MapNullable(123, typeof(System.Collections.IEnumerable), typeof(int[]));

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*No mapping found*");
    }

    [TestMethod]
    public void Map_NoValue_WhenDirectMappingRegistered_ShouldUseDirectMap()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<NoValue, string>((src, m) => "custom_no_value");

        // Act
        var result = mapper.Map<string>(NoValue.Instance);

        // Assert
        result.Should().Be("custom_no_value");
    }

    [TestMethod]
    public void Map_NoValue_WhenUnderlyingDestinationMappingRegistered_ShouldUseUnderlyingMap()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<NoValue, int>((src, dst, m) => -1);

        // Act
        var result = mapper.MapNullable<int?>(NoValue.Instance);

        // Assert
        result.Should().Be(-1);
    }

    [TestMethod]
    public void Map_NoValue_ToReferenceTypeDestination_ShouldReturnNull()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        var result = mapper.MapNullable<string>(NoValue.Instance);
        var resultObject = mapper.MapNullable<object>(NoValue.Instance);

        // Assert
        result.Should().BeNull();
        resultObject.Should().BeNull();
    }

    [TestMethod]
    public void Map_NoValue_ToNullableValueTypeDestination_ShouldReturnNull()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        var resultInt = mapper.MapNullable<int?>(NoValue.Instance);
        var resultBool = mapper.MapNullable<bool?>(NoValue.Instance);

        // Assert
        resultInt.Should().BeNull();
        resultBool.Should().BeNull();
    }

    [TestMethod]
    public void Map_NoValue_ToNonNullableValueTypeDestination_WhenNoMapping_ShouldThrow()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        Action act = () => mapper.MapNullable<int>(NoValue.Instance);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*No mapping found*");
    }

    [TestMethod]
    public void Map_IppValue_WhenDirectMappingRegistered_ShouldUseDirectMap()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<IppValue<int>, string>((src, m) => $"val_{src.GetValueOrDefault()}");

        // Act
        var resultWithValue = mapper.Map<string>(new IppValue<int>(42));
        var resultNoValue = mapper.Map<string>(IppValue<int>.NoValue);

        // Assert
        resultWithValue.Should().Be("val_42");
        resultNoValue.Should().Be("val_0");
    }

    [TestMethod]
    public void Map_IppValue_WhenUnderlyingDestinationMappingRegistered_ShouldUseUnderlyingMap()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<IppValue<int>, int>((src, dst, m) => src.IsValue ? src.Value * 10 : -1);

        // Act
        var resultWithValue = mapper.MapNullable<int?>(new IppValue<int>(5));
        var resultNoValue = mapper.MapNullable<int?>(IppValue<int>.NoValue);

        // Assert
        resultWithValue.Should().Be(50);
        resultNoValue.Should().Be(-1);
    }

    [TestMethod]
    public void Map_IppValue_NoValue_ToNoValueDestination_ShouldReturnNoValueInstance()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        var result = mapper.Map<NoValue>(IppValue<int>.NoValue);
        var resultNullable = mapper.MapNullable<NoValue>(IppValue<string>.NoValue);

        // Assert
        result.Should().Be(NoValue.Instance);
        resultNullable.Should().Be(NoValue.Instance);
    }

    [TestMethod]
    public void Map_IppValue_NoValue_ToReferenceTypeDestination_ShouldReturnNull()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        var result = mapper.MapNullable<string>(IppValue<string>.NoValue);
        var resultIntArray = mapper.MapNullable<int[]>(IppValue<int[]>.NoValue);

        // Assert
        result.Should().BeNull();
        resultIntArray.Should().BeNull();
    }

    [TestMethod]
    public void Map_IppValue_NoValue_ToNullableValueTypeDestination_ShouldReturnNull()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        var resultInt = mapper.MapNullable<int?>(IppValue<int>.NoValue);
        var resultBool = mapper.MapNullable<bool?>(IppValue<bool>.NoValue);

        // Assert
        resultInt.Should().BeNull();
        resultBool.Should().BeNull();
    }

    [TestMethod]
    public void Map_IppValue_NoValue_ToNonNullableValueTypeDestination_WhenNoMapping_ShouldThrow()
    {
        // Arrange
        var mapper = new SimpleMapper();

        // Act
        Action act = () => mapper.MapNullable<int>(IppValue<int>.NoValue);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("*No mapping found*");
    }

    [TestMethod]
    public void Map_IppValue_WithValue_WhenUnderlyingValueIsNull_ShouldReturnNull()
    {
        // Arrange
        var mapper = new SimpleMapper();
        var ippVal = new IppValue<string>((string?)null);

        // Act
        var result = mapper.MapNullable<string>(ippVal);
        var resultNullableInt = mapper.MapNullable<int?>(new IppValue<int?>((int?)null));

        // Assert
        result.Should().BeNull();
        resultNullableInt.Should().BeNull();
    }

    [TestMethod]
    public void Map_IppValue_WithValue_WhenDestinationIsAssignableFromUnderlying_ShouldReturnDirectly()
    {
        // Arrange
        var mapper = new SimpleMapper();
        var myImpl = new MyImplementation { Value = "test" };

        // Act
        var resultInt = mapper.Map<int>(new IppValue<int>(42));
        var resultString = mapper.Map<string>(new IppValue<string>("hello"));
        var resultInterface = mapper.Map<IMyInterface>(new IppValue<MyImplementation>(myImpl));
        var resultObject = mapper.Map<object>(new IppValue<int>(100));

        // Assert
        resultInt.Should().Be(42);
        resultString.Should().Be("hello");
        resultInterface.Should().BeSameAs(myImpl);
        resultObject.Should().Be(100);
    }

    [TestMethod]
    public void Map_IppValue_WithValue_WhenDestinationIsNotAssignableFromUnderlying_ShouldMapInnerValue()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<int, string>((src, dst, m) => dst != null ? $"{dst}:{src}" : $"num_{src}");

        // Act - mapped to nullable value type
        var resultNullableInt = mapper.MapNullable<int?>(new IppValue<int>(42));
        // Act - mapped through registered type mapping
        var resultString = mapper.Map<string>(new IppValue<int>(99));
        // Act - mapped with existing destination passed
        var resultWithDest = mapper.MapNullable<string>(new IppValue<int>(123), typeof(IppValue<int>), "prefix");

        // Assert
        resultNullableInt.Should().Be(42);
        resultString.Should().Be("num_99");
        resultWithDest.Should().Be("prefix:123");
    }

    [TestMethod]
    public void Map_IppValue_WithValue_CollectionMapping_ShouldMapInnerCollection()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, int>((src, m) => int.Parse(src));
        mapper.CreateMap<string[], int[]>((src, map) => src.Select(x => map.Map<int>(x)).ToArray());

        // Act
        var result = mapper.Map<int[]>(new IppValue<string[]>(new[] { "1", "2", "3" }));

        // Assert
        result.Should().Equal(1, 2, 3);
    }

    private sealed class CustomIppValue : IIppValue
    {
        public bool IsValue { get; set; }
        public object? ValueAsObject { get; set; }
    }

    [TestMethod]
    public void Map_CustomIIppValue_ShouldBeHandledCorrectly()
    {
        // Arrange
        var mapper = new SimpleMapper();
        var customNoValue = new CustomIppValue { IsValue = false };
        var customNullValue = new CustomIppValue { IsValue = true, ValueAsObject = null };
        var customWithValue = new CustomIppValue { IsValue = true, ValueAsObject = "hello" };

        // Act & Assert
        mapper.MapNullable<string>(customNoValue).Should().BeNull();
        mapper.Map<NoValue>(customNoValue).Should().Be(NoValue.Instance);
        mapper.MapNullable<string>(customNullValue).Should().BeNull();
        mapper.Map<string>(customWithValue).Should().Be("hello");
    }

    private sealed class CustomIntEnumerable : IEnumerable<int>
    {
        public List<int> Items { get; } = new();

        public CustomIntEnumerable()
        {
        }

        public CustomIntEnumerable(IEnumerable<int> items)
        {
            Items.AddRange(items);
        }

        public IEnumerator<int> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    private sealed class CustomDefaultCtorOnlyList<T> : List<T>
    {
        public CustomDefaultCtorOnlyList()
        {
        }
    }

    private sealed class CustomNoDefaultCtorList<T> : List<T>
    {
        public CustomNoDefaultCtorList(int capacity) : base(capacity)
        {
        }
    }

    private sealed class CustomCollectionNoEnumerableCtor<T> : ICollection<T>
    {
        private readonly List<T> _items = new();

        public int Count => _items.Count;
        public bool IsReadOnly => false;

        public void Add(T item)
        {
            _items.Add(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return _items.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


    [TestMethod]
    public void Instance_ShouldReturnSingletonWithRegisteredProfiles()
    {
        var instance1 = SimpleMapper.Instance;
        var instance2 = SimpleMapper.Instance;

        instance1.Should().NotBeNull();
        instance1.Should().BeSameAs(instance2);

        var uri = instance1.Map<Uri>("http://localhost:631");
        uri.Should().NotBeNull();
        uri.ToString().Should().Be("http://localhost:631/");
    }
}
