using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Tests.Unit.Mapping;

[TestClass]
[ExcludeFromCodeCoverage]
public class MappingAttributesTests
{
    [TestMethod]
    public void IppRequestAttribute_SetsOperation()
    {
        var attr = new IppRequestAttribute(IppOperation.PrintJob);
        attr.Operation.Should().Be(IppOperation.PrintJob);
    }

    [TestMethod]
    public void IppResponseAttribute_CanBeInstantiated()
    {
        var attr = new IppResponseAttribute();
        attr.Should().NotBeNull();
    }

    [TestMethod]
    public void MapperConfigurationAttribute_DefaultValues()
    {
        var attr = new MapperConfigurationAttribute();
        attr.Order.Should().Be(0);
        attr.HandledTypes.Should().BeEmpty();
        attr.HandledType.Should().BeNull();
    }

    [TestMethod]
    public void MapperConfigurationAttribute_CustomValues()
    {
        var attr = new MapperConfigurationAttribute(5);
        attr.Order.Should().Be(5);
        attr.HandledTypes.Should().BeEmpty();
        attr.HandledType.Should().BeNull();
    }

    [TestMethod]
    public void MapperConfigurationAttribute_WithOrderAndHandledTypes()
    {
        var attr = new MapperConfigurationAttribute(2, typeof(OverrideInstruction));
        attr.Order.Should().Be(2);
        attr.HandledTypes.Should().ContainSingle().Which.Should().Be(typeof(OverrideInstruction));
        attr.HandledType.Should().Be(typeof(OverrideInstruction));

        var attrNullType = new MapperConfigurationAttribute(2, (Type)null!);
        attrNullType.Order.Should().Be(2);
        attrNullType.HandledTypes.Should().BeEmpty();
        attrNullType.HandledType.Should().BeNull();

        var attrNullArray = new MapperConfigurationAttribute(2, (Type[])null!);
        attrNullArray.Order.Should().Be(2);
        attrNullArray.HandledTypes.Should().BeEmpty();
        attrNullArray.HandledType.Should().BeNull();

        var attrMultiple = new MapperConfigurationAttribute(2, typeof(OverrideInstruction), typeof(string));
        attrMultiple.Order.Should().Be(2);
        attrMultiple.HandledTypes.Should().Equal(typeof(OverrideInstruction), typeof(string));
        attrMultiple.HandledType.Should().Be(typeof(OverrideInstruction));
    }

    [TestMethod]
    public void MapperConfigurationAttribute_WithHandledTypesOnly()
    {
        var attr = new MapperConfigurationAttribute(typeof(OverrideInstruction));
        attr.Order.Should().Be(0);
        attr.HandledTypes.Should().ContainSingle().Which.Should().Be(typeof(OverrideInstruction));
        attr.HandledType.Should().Be(typeof(OverrideInstruction));

        var attrNullType = new MapperConfigurationAttribute((Type)null!);
        attrNullType.Order.Should().Be(0);
        attrNullType.HandledTypes.Should().BeEmpty();
        attrNullType.HandledType.Should().BeNull();

        var attrNullArray = new MapperConfigurationAttribute((Type[])null!);
        attrNullArray.Order.Should().Be(0);
        attrNullArray.HandledTypes.Should().BeEmpty();
        attrNullArray.HandledType.Should().BeNull();

        var attrMultiple = new MapperConfigurationAttribute(typeof(OverrideInstruction), typeof(string));
        attrMultiple.Order.Should().Be(0);
        attrMultiple.HandledTypes.Should().Equal(typeof(OverrideInstruction), typeof(string));
        attrMultiple.HandledType.Should().Be(typeof(OverrideInstruction));
    }

    [TestMethod]
    public void MapperConfigurationAttribute_HandledTypeProperty()
    {
        var attr = new MapperConfigurationAttribute();
        attr.HandledType.Should().BeNull();

        attr.HandledType = typeof(OverrideInstruction);
        attr.HandledType.Should().Be(typeof(OverrideInstruction));
        attr.HandledTypes.Should().ContainSingle().Which.Should().Be(typeof(OverrideInstruction));

        attr.HandledType = null;
        attr.HandledType.Should().BeNull();
        attr.HandledTypes.Should().BeEmpty();

        attr.HandledTypes = new[] { typeof(int), typeof(string) };
        attr.HandledType.Should().Be(typeof(int));

        attr.Order = 42;
        attr.Order.Should().Be(42);
    }

    [TestMethod]
    public void IppSectionAttribute_SetsSectionTag()
    {
        var attr = new IppSectionAttribute(SectionTag.JobAttributesTag);
        attr.SectionTag.Should().Be(SectionTag.JobAttributesTag);
    }
}

