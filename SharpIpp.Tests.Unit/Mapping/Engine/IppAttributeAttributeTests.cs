using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SharpIpp.Models.Requests;

namespace SharpIpp.Tests.Unit.Mapping;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppAttributeAttributeTests
{
    [TestMethod]
    public void DefaultConstructor_InitializesWithDefaultValues()
    {
        var attr = new IppAttributeAttribute();

        attr.Name.Should().BeNull();
        attr.Tag.Should().Be(Tag.Unsupported);
        attr.Order.Should().Be(-1);
        attr.ExplicitOrder.Should().BeNull();
        attr.DefaultValue.Should().BeNull();
    }

    [TestMethod]
    public void PropertySetters_UpdateValuesCorrectly()
    {
        var attr = new IppAttributeAttribute
        {
            Name = "custom-name",
            Tag = Tag.Keyword,
            Order = 3,
            DefaultValue = "default-val"
        };

        attr.Name.Should().Be("custom-name");
        attr.Tag.Should().Be(Tag.Keyword);
        attr.Order.Should().Be(3);
        attr.ExplicitOrder.Should().Be(3);
        attr.DefaultValue.Should().Be("default-val");
    }

    [TestMethod]
    public void ExplicitOrder_ReturnsExpectedNullableInt()
    {
        var attr = new IppAttributeAttribute();
        attr.ExplicitOrder.Should().BeNull();

        attr.Order = -1;
        attr.ExplicitOrder.Should().BeNull();

        attr.Order = -10;
        attr.ExplicitOrder.Should().BeNull();

        attr.Order = 0;
        attr.ExplicitOrder.Should().Be(0);

        attr.Order = 42;
        attr.ExplicitOrder.Should().Be(42);
    }

    [TestMethod]
    public void Constructor_WithName_SetsNameAndDefaults()
    {
        var attr = new IppAttributeAttribute("test-attr");

        attr.Name.Should().Be("test-attr");
        attr.Tag.Should().Be(Tag.Unsupported);
        attr.Order.Should().Be(-1);
        attr.ExplicitOrder.Should().BeNull();
        attr.DefaultValue.Should().BeNull();
    }

    [TestMethod]
    public void Constructor_WithNameAndTag_SetsNameAndTag()
    {
        var attr = new IppAttributeAttribute("test-attr", Tag.NameWithoutLanguage);

        attr.Name.Should().Be("test-attr");
        attr.Tag.Should().Be(Tag.NameWithoutLanguage);
        attr.Order.Should().Be(-1);
        attr.ExplicitOrder.Should().BeNull();
        attr.DefaultValue.Should().BeNull();
    }

    [TestMethod]
    public void Constructor_WithNameAndOrder_SetsNameAndOrder()
    {
        var attr = new IppAttributeAttribute("test-attr", 5);

        attr.Name.Should().Be("test-attr");
        attr.Tag.Should().Be(Tag.Unsupported);
        attr.Order.Should().Be(5);
        attr.ExplicitOrder.Should().Be(5);
        attr.DefaultValue.Should().BeNull();
    }

    [TestMethod]
    public void Constructor_WithNameTagAndOrder_SetsAllThree()
    {
        var attr = new IppAttributeAttribute("test-attr", Tag.Charset, 0);

        attr.Name.Should().Be("test-attr");
        attr.Tag.Should().Be(Tag.Charset);
        attr.Order.Should().Be(0);
        attr.ExplicitOrder.Should().Be(0);
        attr.DefaultValue.Should().BeNull();
    }

    [TestMethod]
    public void Mapper_OrdersAttributesByExplicitOrderFirst()
    {
        var mapper = SimpleMapper.Instance;

        var op = new OperationAttributes
        {
            PrinterUri = new Uri("ipp://localhost/printers/test"),
            RequestingUserName = "john"
        };

        var attributes = mapper.Map<List<IppAttribute>>(op);

        // AttributesCharset has Order = 0, AttributesNaturalLanguage has Order = 1
        attributes[0].Name.Should().Be(IppAttributeNames.AttributesCharset);
        attributes[1].Name.Should().Be(IppAttributeNames.AttributesNaturalLanguage);
        attributes.Select(x => x.Name).Should().Contain([IppAttributeNames.PrinterUri, IppAttributeNames.RequestingUserName]);
    }
}
