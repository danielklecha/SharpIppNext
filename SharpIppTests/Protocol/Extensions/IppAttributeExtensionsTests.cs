using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System.Diagnostics.CodeAnalysis;

namespace SharpIppTests.Protocol.Extensions;

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
            IppAttribute.CreateInt("", 21000),
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
            IppAttribute.CreateInt("x-dimension", 21000),
            IppAttribute.CreateInt("y-dimension", 29700)
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
            IppAttribute.CreateInt("", 21000),
            new IppAttribute(Tag.MemberAttrName, "", "y-dimension"),
            IppAttribute.CreateInt("", 29700),
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
            IppAttribute.CreateInt("inner-val", 42)
        };
        var nested = innerAttributes.ToBegCollection("inner-col");

        // Act
        var result = nested.ToBegCollection("outer-col").ToList();

        // Assert
        result.Should().HaveCount(7);
        result[0].Tag.Should().Be(Tag.BegCollection);
        result[0].Name.Should().Be("outer-col");
        result[1].Tag.Should().Be(Tag.MemberAttrName);
        result[1].Value.Should().Be("inner-col");
        result[2].Tag.Should().Be(Tag.BegCollection);
        result[3].Tag.Should().Be(Tag.MemberAttrName);
        result[3].Value.Should().Be("inner-val");
        result[4].Tag.Should().Be(Tag.Integer);
        result[5].Tag.Should().Be(Tag.EndCollection);
        result[6].Tag.Should().Be(Tag.EndCollection);
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
            IppAttribute.CreateInt("", 42),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
        };

        // Act
        var result = wrapped.FromBegCollection().ToList();

        // Assert
        result.Should().HaveCount(3);
        result[0].Name.Should().Be("inner-col");
        result[0].Tag.Should().Be(Tag.BegCollection);
        result[1].Name.Should().Be("inner-val");
        result[1].Tag.Should().Be(Tag.Integer);
        result[2].Name.Should().Be("inner-col");
        result[2].Tag.Should().Be(Tag.EndCollection);
    }

    [TestMethod]
    public void From_DoubleNested_ShouldUnwrapCorrectly()
    {
        // Arrange
        var wrapped = new List<IppAttribute>
        {
            new IppAttribute(Tag.BegCollection, "root", NoValue.Instance),
            new IppAttribute(Tag.MemberAttrName, "", "L1"),
            new IppAttribute(Tag.BegCollection, "", NoValue.Instance),
            new IppAttribute(Tag.MemberAttrName, "", "L2"),
            new IppAttribute(Tag.BegCollection, "", NoValue.Instance),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
        };

        // Act
        var result = wrapped.FromBegCollection().ToList();

        // Assert
        result.Should().HaveCount(4);
        result[0].Name.Should().Be("L1");
        result[0].Tag.Should().Be(Tag.BegCollection);
        result[1].Name.Should().Be("L2");
        result[1].Tag.Should().Be(Tag.BegCollection);
        result[2].Name.Should().Be("L2");
        result[2].Tag.Should().Be(Tag.EndCollection);
        result[3].Name.Should().Be("L1");
        result[3].Tag.Should().Be(Tag.EndCollection);
    }

    [TestMethod]
    public void FromBegCollection_MissingMemberAttrName_ShouldThrow()
    {
        // Arrange
        var wrapped = new List<IppAttribute>
        {
            new IppAttribute(Tag.BegCollection, "root", NoValue.Instance),
            new IppAttribute(Tag.BegCollection, "", NoValue.Instance),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
        };

        // Act
        Action action = () => wrapped.FromBegCollection().ToList();

        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("*preceded by MemberAttrName*");
    }

    [TestMethod]
    public void FromBegCollection_MultiValuedMember_ShouldSupportPersistence()
    {
        // Arrange
        var wrapped = new List<IppAttribute>
        {
            new IppAttribute(Tag.BegCollection, "root", NoValue.Instance),
            new IppAttribute(Tag.MemberAttrName, "", "list"),
            IppAttribute.CreateInt("", 1),
            new IppAttribute(Tag.MemberAttrName, "", ""),
            IppAttribute.CreateInt("", 2),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
        };

        // Act
        var result = wrapped.FromBegCollection().ToList();

        // Assert
        result.Should().HaveCount(2, string.Join("\n", result.Select(x => $"{x.Name}: {x.Value}")));
        result[0].Name.Should().Be("list");
        result[1].Name.Should().Be("list");
    }

    [TestMethod]
    public void ToBegCollection_MultiValuedMember_ShouldIncludeMemberAttrName()
    {
        // Arrange
        var inner = new List<IppAttribute>
        {
            IppAttribute.CreateInt("list", 1),
            IppAttribute.CreateInt("list", 2)
        };

        // Act
        var result = inner.ToBegCollection("root").ToList();

        // Assert
        result.Should().HaveCount(6); // BegCol, MemberAttrName, Val1, MemberAttrName(empty), Val2, EndCol
        result[0].Tag.Should().Be(Tag.BegCollection);
        result[1].Tag.Should().Be(Tag.MemberAttrName);
        result[1].Value.Should().Be("list");
        result[2].Tag.Should().Be(Tag.Integer);
        result[3].Tag.Should().Be(Tag.MemberAttrName);
        result[3].Value.Should().Be("");
        result[4].Tag.Should().Be(Tag.Integer);
        result[5].Tag.Should().Be(Tag.EndCollection);
    }

    [TestMethod]
    public void FromBegCollection_TripleNested_ShouldUnwrapCorrectly()
    {
        // Arrange
        var wrapped = new List<IppAttribute>
        {
            new IppAttribute(Tag.BegCollection, "L0", NoValue.Instance),
            new IppAttribute(Tag.MemberAttrName, "", "L1"),
            new IppAttribute(Tag.BegCollection, "", NoValue.Instance),
            new IppAttribute(Tag.MemberAttrName, "", "L2"),
            new IppAttribute(Tag.BegCollection, "", NoValue.Instance),
            new IppAttribute(Tag.MemberAttrName, "", "L3"),
            IppAttribute.CreateInt("", 42),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
        };

        // Act
        var result = wrapped.FromBegCollection().ToList();

        // Assert
        result.Should().HaveCount(5);
        result[0].Name.Should().Be("L1");
        result[0].Tag.Should().Be(Tag.BegCollection);
        result[1].Name.Should().Be("L2");
        result[1].Tag.Should().Be(Tag.BegCollection);
        result[2].Name.Should().Be("L3");
        result[3].Name.Should().Be("L2");
        result[4].Name.Should().Be("L1");
    }

    [TestMethod]
    public void ToBegCollection_DeeplyNested_ShouldWrapCorrectly()
    {
        // Arrange
        var innerMost = new List<IppAttribute> { IppAttribute.CreateInt("L2", 42) };
        var innerAttributes = innerMost.ToBegCollection("L1");

        // Act
        var result = innerAttributes.ToBegCollection("L0").ToList();

        // Assert
        result.Should().HaveCount(7);
        result[3].Tag.Should().Be(Tag.MemberAttrName);
        result[3].Value.Should().Be("L2");
    }

    [TestMethod]
    public void ToBegCollection_ShouldSupport_WhenNestedAttributeHasName()
    {
        // Arrange
        var innerAttributes = new List<IppAttribute>
        {
            new IppAttribute(Tag.BegCollection, "L1", NoValue.Instance),
            IppAttribute.CreateInt("nested-named", 42),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
        };

        // Act
        var result = innerAttributes.ToBegCollection("L0").ToList();

        // Assert
        result.Should().HaveCount(7);
        // L0
        result[0].Tag.Should().Be(Tag.BegCollection);
        result[0].Name.Should().Be("L0");
        
        // MemberAttrName "L1"
        result[1].Tag.Should().Be(Tag.MemberAttrName);
        result[1].Value.Should().Be("L1");
        
        // BegCollection "" (was L1)
        result[2].Tag.Should().Be(Tag.BegCollection);
        result[2].Value.Should().Be(NoValue.Instance);
        
        // MemberAttrName "nested-named" -> content of L1
        result[3].Tag.Should().Be(Tag.MemberAttrName);
        result[3].Value.Should().Be("nested-named");
        
        // Integer 42
        result[4].Tag.Should().Be(Tag.Integer);
        result[4].Value.Should().Be(42);
        
        // EndCollection
        result[5].Tag.Should().Be(Tag.EndCollection);
        
        // EndCollection (L0)
        result[6].Tag.Should().Be(Tag.EndCollection);
    }

    [TestMethod]
    public void FromBegCollection_ShouldSupport_WhenDeepNestedAttributeHasName()
    {
        // Arrange
        var wrapped = new List<IppAttribute>
        {
            new IppAttribute(Tag.BegCollection, "L0", NoValue.Instance),
            new IppAttribute(Tag.MemberAttrName, "", "L1"),
            new IppAttribute(Tag.BegCollection, "", NoValue.Instance),
            IppAttribute.CreateInt("invalid-name", 42),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
        };

        // Act
        var result = wrapped.FromBegCollection().ToList();

        // Assert
        result.Should().HaveCount(3);
        
        // 1. BegCollection "L1"
        result[0].Name.Should().Be("L1");
        result[0].Tag.Should().Be(Tag.BegCollection);
        
        // 2. Integer "invalid-name" (became member name)
        result[1].Name.Should().Be("invalid-name");
        result[1].Value.Should().Be(42);
        
        // 3. EndCollection "L1"
        result[2].Name.Should().Be("L1");
        result[2].Tag.Should().Be(Tag.EndCollection);
    }

    [TestMethod]
    public void FromBegCollection_ShouldThrow_WhenValueNotPrecededByMemberAttrName()
    {
        // Arrange
        var wrapped = new List<IppAttribute>
        {
            new IppAttribute(Tag.BegCollection, "L0", NoValue.Instance),
            new IppAttribute(Tag.BegCollection, "", NoValue.Instance), // No MemberAttrName before this!
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
        };
        
        // Act
        Action action = () => wrapped.FromBegCollection().ToList();
        
        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("*preceded by MemberAttrName*");
    }

    [TestMethod]
    public void FromBegCollection_ShouldThrow_WhenFirstTagNotBegCollection()
    {
        var wrapped = new List<IppAttribute> { IppAttribute.CreateInt("", 1) };
        Action action = () => wrapped.FromBegCollection().ToList();
        action.Should().Throw<ArgumentException>().WithMessage("*First attribute must be BegCollection*");
    }

    [TestMethod]
    public void ToBegCollection_ShouldThrow_WhenEndCollectionAtRootLevel()
    {
        // Arrange
        var inner = new List<IppAttribute>
        {
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
        };

        // Act
        Action action = () => inner.ToBegCollection("root").ToList();

        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("*root level*");
    }

    [TestMethod]
    public void FromBegCollection_ShouldThrow_WhenSecondValueMissingMemberAttrName()
    {
        // Arrange
        var wrapped = new List<IppAttribute>
        {
            new IppAttribute(Tag.BegCollection, "root", NoValue.Instance),
            new IppAttribute(Tag.MemberAttrName, "", "list"),
            IppAttribute.CreateInt("", 1),
            IppAttribute.CreateInt("", 2), // Missing MemberAttrName("")!
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
        };

        // Act
        Action action = () => wrapped.FromBegCollection().ToList();

        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("*preceded by MemberAttrName*");
    }
    [TestMethod]
    public void FromBegCollection_MemberAttrNameFollowedByMemberAttrName_ShouldThrow()
    {
        // Arrange
        var wrapped = new List<IppAttribute>
        {
            new IppAttribute(Tag.BegCollection, "root", NoValue.Instance),
            new IppAttribute(Tag.MemberAttrName, "", "attr1"),
            new IppAttribute(Tag.MemberAttrName, "", "attr2"), // dangling attr1
            IppAttribute.CreateInt("", 1),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
        };

        // Act
        Action action = () => wrapped.FromBegCollection().ToList();

        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("*MemberAttrName cannot be followed by another MemberAttrName*");
    }

    [TestMethod]
    public void FromBegCollection_MemberAttrNameFollowedByEndCollection_ShouldThrow()
    {
        // Arrange
        var wrapped = new List<IppAttribute>
        {
            new IppAttribute(Tag.BegCollection, "root", NoValue.Instance),
            new IppAttribute(Tag.MemberAttrName, "", "attr1"),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance) // dangling attr1
        };

        // Act
        Action action = () => wrapped.FromBegCollection().ToList();

        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("*MemberAttrName cannot be followed by EndCollection*");
    }

    [TestMethod]
    public void ToBegCollection_ShouldThrow_WhenAttributeHasNoName()
    {
        // Arrange
        var attributes = new List<IppAttribute>
        {
            IppAttribute.CreateInt("", 42) // No name, and not preceded by MemberAttrName context
        };

        // Act
        Action action = () => attributes.ToBegCollection("root").ToList();

        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("*Attribute without name found*");
    }

    [TestMethod]
    public void FromBegCollection_MemberAttrNameEmptyAtStart_ShouldThrow()
    {
         // Arrange
        var wrapped = new List<IppAttribute>
        {
            new IppAttribute(Tag.BegCollection, "root", NoValue.Instance),
            new IppAttribute(Tag.MemberAttrName, "", ""), // Empty name!
            IppAttribute.CreateInt("", 1),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
        };

        // Act
        Action action = () => wrapped.FromBegCollection().ToList();

        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("*MemberAttrName with empty value must follow a named MemberAttrName*");
    }

    [TestMethod]
    public void FromBegCollection_MultiValuedCollection_ShouldUnwrapCorrectly()
    {
        // Arrange
        // list: [ { inner: 1 }, { inner: 2 } ]
        var wrapped = new List<IppAttribute>
        {
            new IppAttribute(Tag.BegCollection, "root", NoValue.Instance),
            
            // First item
            new IppAttribute(Tag.MemberAttrName, "", "list"),
            new IppAttribute(Tag.BegCollection, "", NoValue.Instance),
                new IppAttribute(Tag.MemberAttrName, "", "inner"),
                IppAttribute.CreateInt("", 1),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance),

            // Second item
            new IppAttribute(Tag.MemberAttrName, "", ""), // Empty name, refers to "list"
            new IppAttribute(Tag.BegCollection, "", NoValue.Instance),
                new IppAttribute(Tag.MemberAttrName, "", "inner"),
                IppAttribute.CreateInt("", 2),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance),

            new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
        };

        // Act
        var result = wrapped.FromBegCollection().ToList();

        // Assert
        result.Should().HaveCount(6);
        // list
        result[0].Name.Should().Be("list");
        result[0].Tag.Should().Be(Tag.BegCollection);
        // inner 1
        result[1].Name.Should().Be("inner");
        result[1].Value.Should().Be(1);
        // list end
        result[2].Name.Should().Be("list");
        result[2].Tag.Should().Be(Tag.EndCollection);
        
        // list 2
        result[3].Name.Should().Be("list");
        result[3].Tag.Should().Be(Tag.BegCollection);
        // inner 2
        result[4].Name.Should().Be("inner");
        result[4].Value.Should().Be(2);
        // list end
        result[5].Name.Should().Be("list");
        result[5].Tag.Should().Be(Tag.EndCollection);
    }

    [TestMethod]
    public void FromBegCollection_NamedBegCollection_ShouldUnwrapCorrectly()
    {
        // Arrange
        var wrapped = new List<IppAttribute>
        {
            new IppAttribute(Tag.BegCollection, "root", NoValue.Instance),
            new IppAttribute(Tag.BegCollection, "nested-col", NoValue.Instance), // Named inner collection!
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
        };

        // Act
        var result = wrapped.FromBegCollection().ToList();

        // Assert
        result.Should().HaveCount(2);
        result[0].Name.Should().Be("nested-col");
        result[0].Tag.Should().Be(Tag.BegCollection);
        result[1].Name.Should().Be("nested-col");
        result[1].Tag.Should().Be(Tag.EndCollection);
    }

    [TestMethod]
    public void FromBegCollection_NamedInteger_ShouldUnwrapCorrectly()
    {
        // Arrange
        var wrapped = new List<IppAttribute>
        {
            new IppAttribute(Tag.BegCollection, "root", NoValue.Instance),
            IppAttribute.CreateInt("named-int", 1), // Named integer!
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
        };

        // Act
        var result = wrapped.FromBegCollection().ToList();

        // Assert
        result.Should().HaveCount(1);
        result[0].Name.Should().Be("named-int");
        result[0].Tag.Should().Be(Tag.Integer);
        result[0].Value.Should().Be(1);
    }

    [TestMethod]
    public void FromBegCollection_MemberAttrNameCurrent_ShouldIgnoreInnerName()
    {
        // Arrange
        var wrapped = new List<IppAttribute>
        {
            new IppAttribute(Tag.BegCollection, "root", NoValue.Instance),
            new IppAttribute(Tag.MemberAttrName, "", "explicit-name"),
            new IppAttribute(Tag.BegCollection, "ignored-name", NoValue.Instance), // Name should be ignored
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance),
            new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
        };

        // Act
        var result = wrapped.FromBegCollection().ToList();

        // Assert
        result.Should().HaveCount(2);
        result[0].Name.Should().Be("explicit-name");
        result[0].Tag.Should().Be(Tag.BegCollection);
        result[1].Name.Should().Be("explicit-name");
        result[1].Tag.Should().Be(Tag.EndCollection);
    }
}

