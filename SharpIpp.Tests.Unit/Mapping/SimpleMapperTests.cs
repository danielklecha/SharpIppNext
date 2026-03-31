using SharpIpp.Mapping;
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
        var result = mapper.Map<string?>(source);

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
        var result = mapper.Map<HashSet<int>>(new object[] { "9", "10", "10" });

        // Assert
        result.Should().BeEquivalentTo(new[] { 9, 10 });
    }

    [TestMethod]
    public void Map_EnumerableSource_ToConcreteIListWithDefaultCtor_ShouldWork()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, int>((src, m) => int.Parse(src));

        // Act
        var result = mapper.Map<Collection<int>>(new object[] { "11", "12" });

        // Assert
        result.Should().Equal(11, 12);
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
        var result = mapper.Map<CustomIntEnumerable>(new object[] { "15", "16" });

        // Assert
        result.Should().NotBeNull();
        result.Items.Should().Equal(15, 16);
    }

    [TestMethod]
    public void Map_EnumerableSource_ToGenericIListConcreteTypeWithoutEnumerableCtor_ShouldWork()
    {
        // Arrange
        var mapper = new SimpleMapper();
        mapper.CreateMap<string, int>((src, m) => int.Parse(src));

        // Act
        var result = mapper.Map<CustomDefaultCtorOnlyList<int>>(new object[] { "17", "18" });

        // Assert
        result.Should().Equal(17, 18);
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

}
