using SharpIpp.Mapping;
using System.Diagnostics.CodeAnalysis;

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
}
