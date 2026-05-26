using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class DocumentMetadataProfileTests : MapperTestBase
{
    [TestMethod]
    public void Map_StringArray_To_DocumentMetadata_Should_Parse_Correctly()
    {
        // Arrange
        var source = new[]
        {
            "title=My Document",
            "creator=John Doe",
            "x-custom-key=custom-value",
            "invalid-entry",
            ""
        };

        // Act
        var result = _mapper.Map<string[], DocumentMetadata>(source);

        // Assert
        result.Should().NotBeNull();
        NoValue.IsNoValue(result).Should().BeFalse();
        result.Title.Should().Be("My Document");
        result.Creator.Should().Be("John Doe");
        result.ContainsKey("x-custom-key").Should().BeTrue();
        result["x-custom-key"].Should().Be("custom-value");
        result.ContainsKey("invalid-entry").Should().BeFalse();
    }

    [TestMethod]
    public void Map_ObjectArray_To_DocumentMetadata_Should_Map_Values()
    {
        // Arrange
        var source = new object[]
        {
            new OctetString("title=My Document"),
            new OctetString("x-custom-key=custom-value")
        };

        // Act
        var result = _mapper.Map<object[], DocumentMetadata>(source);

        // Assert
        result.Should().NotBeNull();
        NoValue.IsNoValue(result).Should().BeFalse();
        result.Title.Should().Be("My Document");
        result.ContainsKey("x-custom-key").Should().BeTrue();
        result["x-custom-key"].Should().Be("custom-value");
    }

    [TestMethod]
    public void Map_ObjectArray_Containing_Single_NoValue_To_DocumentMetadata_Should_Return_NoValue()
    {
        // Arrange
        var source = new object[]
        {
            NoValue.Instance
        };

        // Act
        var result = _mapper.Map<object[], DocumentMetadata>(source);

        // Assert
        result.Should().NotBeNull();
        NoValue.IsNoValue(result).Should().BeTrue();
    }

    [TestMethod]
    public void Map_NoValue_To_DocumentMetadata_Should_Return_NoValue()
    {
        // Arrange
        var source = NoValue.Instance;

        // Act
        var result = _mapper.Map<NoValue, DocumentMetadata>(source);

        // Assert
        result.Should().NotBeNull();
        NoValue.IsNoValue(result).Should().BeTrue();
    }
}
