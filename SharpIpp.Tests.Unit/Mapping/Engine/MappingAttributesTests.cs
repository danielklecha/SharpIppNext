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
    }

    [TestMethod]
    public void MapperConfigurationAttribute_CustomValues()
    {
        var attr = new MapperConfigurationAttribute(5);
        attr.Order.Should().Be(5);
    }

    [TestMethod]
    public void IppSectionAttribute_SetsSectionTag()
    {
        var attr = new IppSectionAttribute(SectionTag.JobAttributesTag);
        attr.SectionTag.Should().Be(SectionTag.JobAttributesTag);
    }
}

