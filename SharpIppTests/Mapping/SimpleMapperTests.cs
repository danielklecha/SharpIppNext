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

    private interface IMyInterface { string? Value { get; } }
    private class MyImplementation : IMyInterface { public string? Value { get; set; } }
}
