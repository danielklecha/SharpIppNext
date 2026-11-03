using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Models.Requests;

namespace SharpIpp.Tests.Unit.Mapping;

[TestClass]
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
        attributes[2].Name.Should().Be(IppAttributeNames.PrinterUri);
        attributes.Select(x => x.Name).Should().Contain([IppAttributeNames.RequestingUserName]);
    }

    [TestMethod]
    public void Mapper_OrdersJobTargetAttributesDirectlyAfterLanguage()
    {
        var mapper = SimpleMapper.Instance;

        var op = new GetJobAttributesOperationAttributes
        {
            PrinterUri = new Uri("ipp://localhost/printers/test"),
            RequestingUserName = "john",
            JobId = 42,
            RequestedAttributes = new[] { "job-id", "job-state" }
        };

        var attributes = mapper.Map<List<IppAttribute>>(op);

        // RFC 8011 §4.1.5: [0] charset, [1] language, [2] printer-uri, [3] job-id
        attributes[0].Name.Should().Be(IppAttributeNames.AttributesCharset);
        attributes[1].Name.Should().Be(IppAttributeNames.AttributesNaturalLanguage);
        attributes[2].Name.Should().Be(IppAttributeNames.PrinterUri);
        attributes[3].Name.Should().Be(IppAttributeNames.JobId);
        attributes[3].Value.Should().Be(42);

        var remaining = attributes.Skip(4).Select(x => x.Name).ToList();
        remaining.Should().Contain(IppAttributeNames.RequestingUserName);
        remaining.Should().Contain(IppAttributeNames.RequestedAttributes);
    }

    [TestMethod]
    public void Mapper_OrdersJobUriTargetAttributeAsThird()
    {
        var mapper = SimpleMapper.Instance;

        var op = new JobOperationAttributes
        {
            JobUri = new Uri("ipp://localhost/jobs/42"),
            RequestingUserName = "john"
        };

        var attributes = mapper.Map<List<IppAttribute>>(op);

        attributes[0].Name.Should().Be(IppAttributeNames.AttributesCharset);
        attributes[1].Name.Should().Be(IppAttributeNames.AttributesNaturalLanguage);
        attributes[2].Name.Should().Be(IppAttributeNames.JobUri);
        attributes.Skip(3).Select(x => x.Name).Should().Contain(IppAttributeNames.RequestingUserName);
    }

    [TestMethod]
    public void Mapper_OrdersDocumentNumberTargetAttributeAsFifth()
    {
        var mapper = SimpleMapper.Instance;

        var op = new GetDocumentAttributesOperationAttributes
        {
            PrinterUri = new Uri("ipp://localhost/printers/test"),
            JobId = 10,
            DocumentNumber = 1,
            RequestingUserName = "john"
        };

        var attributes = mapper.Map<List<IppAttribute>>(op);

        // PWG 5100.5 §5.1: [0] charset, [1] language, [2] printer-uri, [3] job-id, [4] document-number
        attributes[0].Name.Should().Be(IppAttributeNames.AttributesCharset);
        attributes[1].Name.Should().Be(IppAttributeNames.AttributesNaturalLanguage);
        attributes[2].Name.Should().Be(IppAttributeNames.PrinterUri);
        attributes[3].Name.Should().Be(IppAttributeNames.JobId);
        attributes[4].Name.Should().Be(IppAttributeNames.DocumentNumber);
        attributes.Skip(5).Select(x => x.Name).Should().Contain(IppAttributeNames.RequestingUserName);
    }
}
