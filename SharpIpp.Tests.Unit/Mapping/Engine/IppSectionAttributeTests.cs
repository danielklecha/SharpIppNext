using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Mapping.Engine;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppSectionAttributeTests
{
    [TestMethod]
    [DataRow(SectionTag.OperationAttributesTag)]
    [DataRow(SectionTag.JobAttributesTag)]
    [DataRow(SectionTag.PrinterAttributesTag)]
    [DataRow(SectionTag.SystemAttributesTag)]
    [DataRow(SectionTag.UnsupportedAttributesTag)]
    public void Constructor_SetsSectionTag(SectionTag sectionTag)
    {
        var attr = new IppSectionAttribute(sectionTag);

        attr.SectionTag.Should().Be(sectionTag);
    }

    [TestMethod]
    public void AttributeUsage_CanBeAppliedToClassAndProperty()
    {
        var classAttr = typeof(TestTargetClass).GetCustomAttribute<IppSectionAttribute>();
        classAttr.Should().NotBeNull();
        classAttr!.SectionTag.Should().Be(SectionTag.SystemAttributesTag);

        var propAttr = typeof(TestTargetClass).GetProperty(nameof(TestTargetClass.TestProperty))!
            .GetCustomAttribute<IppSectionAttribute>();
        propAttr.Should().NotBeNull();
        propAttr!.SectionTag.Should().Be(SectionTag.JobAttributesTag);
    }

    [IppSection(SectionTag.SystemAttributesTag)]
    private class TestTargetClass
    {
        [IppSection(SectionTag.JobAttributesTag)]
        public string? TestProperty { get; set; }
    }
}
