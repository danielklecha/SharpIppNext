using SharpIpp.Mapping;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Tests.Mapping;

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
        Action act = () => mapper.Map<string>((object?)null, null!);

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
        Action act = () => mapper.Map<string, string>((string?)null, null!);

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
}
