using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Tests.Protocol.Extensions;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppAttributeExtensionsTests
{
    [TestMethod]
    public void ToIppDictionary_ShouldGroupAttributes()
    {
        // Arrange
        var attributes = new List<IppAttribute>
        {
            new IppAttribute(Tag.Charset, "attributes-charset", "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, "attributes-natural-language", "en"),
            new IppAttribute(Tag.Uri, "printer-uri", "ipp://localhost/printer")
        };

        // Act
        var result = attributes.ToIppDictionary();

        // Assert
        result.Should().HaveCount(3);
        result["attributes-charset"].Should().HaveCount(1);
        result["attributes-charset"][0].Value.Should().Be("utf-8");
    }

    [TestMethod]
    public void ToIppDictionary_ShouldHandleCollections()
    {
        // Arrange
        var attributes = new List<IppAttribute>
        {
            new IppAttribute(Tag.BegCollection, "media-col", NoValue.Instance),
            new IppAttribute(Tag.MemberAttrName, "", "media-size"),
            new IppAttribute(Tag.BegCollection, "", NoValue.Instance),
            new IppAttribute(Tag.MemberAttrName, "", "x-dimension"),
            new IppAttribute(Tag.Integer, "", 21000),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
        };

        // Act
        var result = attributes.ToIppDictionary();

        // Assert
        result.Should().ContainKey("media-col");
        result["media-col"].Should().HaveCount(6);
        result.Should().ContainKey("");
        result[""].Should().HaveCount(1);
    }

    [TestMethod]
    public void ToBegCollection_ShouldWrapAttributes()
    {
        // Arrange
        var innerAttributes = new List<IppAttribute>
        {
            new IppAttribute(Tag.Integer, "x-dimension", 21000),
            new IppAttribute(Tag.Integer, "y-dimension", 29700)
        };

        // Act
        var result = innerAttributes.ToBegCollection("media-size").ToList();

        // Assert
        result.Should().HaveCount(6);
        result[0].Tag.Should().Be(Tag.BegCollection);
        result[0].Name.Should().Be("media-size");
        result[1].Tag.Should().Be(Tag.MemberAttrName);
        result[1].Value.Should().Be("x-dimension");
        result[5].Tag.Should().Be(Tag.EndCollection);
    }

    [TestMethod]
    public void FromBegCollection_ShouldUnwrapAttributes()
    {
        // Arrange
        var wrapped = new List<IppAttribute>
        {
            new IppAttribute(Tag.BegCollection, "media-size", NoValue.Instance),
            new IppAttribute(Tag.MemberAttrName, "", "x-dimension"),
            new IppAttribute(Tag.Integer, "", 21000),
            new IppAttribute(Tag.MemberAttrName, "", "y-dimension"),
            new IppAttribute(Tag.Integer, "", 29700),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
        };

        // Act
        var result = wrapped.FromBegCollection().ToList();

        // Assert
        result.Should().HaveCount(2);
        result[0].Name.Should().Be("x-dimension");
        result[0].Value.Should().Be(21000);
        result[1].Name.Should().Be("y-dimension");
        result[1].Value.Should().Be(29700);
    }

    [TestMethod]
    public void ToBegCollection_Nested_ShouldWrapCorrectly()
    {
        // Arrange
        var innerAttributes = new List<IppAttribute>
        {
            new IppAttribute(Tag.BegCollection, "inner-col", NoValue.Instance),
            new IppAttribute(Tag.Integer, "inner-val", 42),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
        };

        // Act
        var result = innerAttributes.ToBegCollection("outer-col").ToList();

        // Assert
        var resultDisplay = string.Join("\n", result.Select((a, i) => $"{i}: ({a.Tag}) {a.Name}: {a.Value}"));
        result.Should().HaveCount(6, resultDisplay);
    }

    [TestMethod]
    public void FromBegCollection_Nested_ShouldUnwrapCorrectly()
    {
        // Arrange
        var wrapped = new List<IppAttribute>
        {
            new IppAttribute(Tag.BegCollection, "outer-col", NoValue.Instance),
            new IppAttribute(Tag.MemberAttrName, "", "inner-col"),
            new IppAttribute(Tag.BegCollection, "", NoValue.Instance),
            new IppAttribute(Tag.MemberAttrName, "", "inner-val"),
            new IppAttribute(Tag.Integer, "", 42),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
        };

        // Act
        var result = wrapped.FromBegCollection().ToList();

        // Assert
        result.Should().HaveCount(4);
        result[0].Name.Should().Be("inner-col");
        result[0].Tag.Should().Be(Tag.BegCollection);
        result[1].Tag.Should().Be(Tag.MemberAttrName);
        result[1].Value.Should().Be("inner-val");
        result[2].Tag.Should().Be(Tag.Integer);
        result[3].Tag.Should().Be(Tag.EndCollection);
    }
}
