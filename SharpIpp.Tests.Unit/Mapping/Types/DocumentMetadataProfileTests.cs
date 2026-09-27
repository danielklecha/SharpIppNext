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
        result.Title.Should().Be("My Document");
        result.ContainsKey("x-custom-key").Should().BeTrue();
        result["x-custom-key"].Should().Be("custom-value");
    }

    [TestMethod]
    public void Map_NoValue_To_IppValue_DocumentMetadata_Should_Return_NoValue()
    {
        // Arrange
        var source = NoValue.Instance;

        // Act
        var result = _mapper.Map<NoValue, IppValue<DocumentMetadata>>(source);

        // Assert
        result.IsValue.Should().BeFalse();
    }

    [TestMethod]
    public void Map_DocumentMetadata_ToString_MapsCorrectly()
    {
        // Arrange
        var metadata = new DocumentMetadata { Title = "My Document", Creator = "Jane Doe" };

        // Act
        var result = _mapper.Map<DocumentMetadata, string>(metadata);

        // Assert
        result.Should().NotBeNull();
        result.Should().Contain("title=My Document");
        result.Should().Contain("creator=Jane Doe");
    }

    [TestMethod]
    public void Map_String_ToDocumentMetadata_MapsCorrectly()
    {
        // Arrange
        var source = "title=My Document;creator=Jane Doe";

        // Act
        var result = _mapper.Map<string, DocumentMetadata>(source);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be("My Document");
        result.Creator.Should().Be("Jane Doe");
    }
}

