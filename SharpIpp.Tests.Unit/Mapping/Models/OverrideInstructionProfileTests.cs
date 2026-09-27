using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using Range = SharpIpp.Protocol.Models.Range;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class OverrideInstructionProfileTests : MapperTestBase
{
    [TestMethod]
    public void Map_ToAttributes_WithNoValuePageRanges_EmitsNoValueTag()
    {
        var src = new OverrideInstruction
        {
            PageRanges = IppValue<Range[]>.NoValue
        };

        var attributes = _mapper.Map<IEnumerable<IppAttribute>>(src).ToList();

        var pageAttr = attributes.Single(x => x.Name == "pages");
        pageAttr.Tag.Should().Be(Tag.NoValue);
        pageAttr.Value.Should().Be(NoValue.Instance);
    }

    [TestMethod]
    public void Map_ToAttributes_WithNoValueDocumentNumberRanges_EmitsNoValueTag()
    {
        var src = new OverrideInstruction
        {
            DocumentNumberRanges = IppValue<Range[]>.NoValue
        };

        var attributes = _mapper.Map<IEnumerable<IppAttribute>>(src).ToList();

        var docNumAttr = attributes.Single(x => x.Name == "document-numbers");
        docNumAttr.Tag.Should().Be(Tag.NoValue);
        docNumAttr.Value.Should().Be(NoValue.Instance);
    }

    [TestMethod]
    public void Map_ToAttributes_WithNoValueDocumentCopyRanges_EmitsNoValueTag()
    {
        var src = new OverrideInstruction
        {
            DocumentCopyRanges = IppValue<Range[]>.NoValue
        };

        var attributes = _mapper.Map<IEnumerable<IppAttribute>>(src).ToList();

        var docCopyAttr = attributes.Single(x => x.Name == "document-copies");
        docCopyAttr.Tag.Should().Be(Tag.NoValue);
        docCopyAttr.Value.Should().Be(NoValue.Instance);
    }

    [TestMethod]
    public void Map_ToAttributes_WithAllNoValueRangesAndJobTemplate_EmitsAllExpectedAttributes()
    {
        var src = new OverrideInstruction
        {
            PageRanges = IppValue<Range[]>.NoValue,
            DocumentNumberRanges = IppValue<Range[]>.NoValue,
            DocumentCopyRanges = IppValue<Range[]>.NoValue,
            JobTemplateAttributes = new JobTemplateAttributes
            {
                Media = (Media)"iso_a4_210x297mm",
                Sides = Sides.OneSided
            }
        };

        var attributes = _mapper.Map<IEnumerable<IppAttribute>>(src).ToList();

        attributes.Should().ContainSingle(x => x.Name == "pages" && x.Tag == Tag.NoValue && x.Value is NoValue);
        attributes.Should().ContainSingle(x => x.Name == "document-numbers" && x.Tag == Tag.NoValue && x.Value is NoValue);
        attributes.Should().ContainSingle(x => x.Name == "document-copies" && x.Tag == Tag.NoValue && x.Value is NoValue);
        attributes.Should().ContainSingle(x => x.Name == IppAttributeNames.Media && (string)x.Value == "iso_a4_210x297mm");
        attributes.Should().ContainSingle(x => x.Name == IppAttributeNames.Sides && (string)x.Value == "one-sided");
    }

    [TestMethod]
    public void Map_ToIDictionary_MapsCorrectly()
    {
        var src = new OverrideInstruction
        {
            PageRanges = IppValue<Range[]>.NoValue,
            DocumentNumberRanges = new[] { new Range(1, 2) },
            DocumentCopyRanges = IppValue<Range[]>.NoValue,
            JobTemplateAttributes = new JobTemplateAttributes
            {
                Media = (Media)"iso_a4_210x297mm"
            }
        };

        var dict = _mapper.Map<IDictionary<string, IppAttribute[]>>(src);

        dict.Should().NotBeNull();
        dict.Should().ContainKey("pages");
        dict["pages"].Should().ContainSingle(x => x.Tag == Tag.NoValue && x.Value is NoValue);
        dict.Should().ContainKey("document-numbers");
        dict["document-numbers"].Should().ContainSingle(x => x.Tag == Tag.RangeOfInteger && Equals(x.Value, new Range(1, 2)));
        dict.Should().ContainKey("document-copies");
        dict["document-copies"].Should().ContainSingle(x => x.Tag == Tag.NoValue && x.Value is NoValue);
        dict.Should().ContainKey(IppAttributeNames.Media);
    }

    [TestMethod]
    public void Map_ToDictionary_MapsCorrectly()
    {
        var src = new OverrideInstruction
        {
            PageRanges = new[] { new Range(1, 5) },
            DocumentNumberRanges = IppValue<Range[]>.NoValue,
            DocumentCopyRanges = new[] { new Range(1, 1) },
            JobTemplateAttributes = new JobTemplateAttributes
            {
                Sides = Sides.TwoSidedLongEdge
            }
        };

        var dict = _mapper.Map<Dictionary<string, IppAttribute[]>>(src);

        dict.Should().NotBeNull();
        dict.Should().ContainKey("pages");
        dict["pages"].Should().ContainSingle(x => x.Tag == Tag.RangeOfInteger && Equals(x.Value, new Range(1, 5)));
        dict.Should().ContainKey("document-numbers");
        dict["document-numbers"].Should().ContainSingle(x => x.Tag == Tag.NoValue && x.Value is NoValue);
        dict.Should().ContainKey("document-copies");
        dict["document-copies"].Should().ContainSingle(x => x.Tag == Tag.RangeOfInteger && Equals(x.Value, new Range(1, 1)));
        dict.Should().ContainKey(IppAttributeNames.Sides);
    }

    [TestMethod]
    public void Map_ToList_MapsCorrectly()
    {
        var src = new OverrideInstruction
        {
            PageRanges = IppValue<Range[]>.NoValue,
            DocumentNumberRanges = IppValue<Range[]>.NoValue,
            DocumentCopyRanges = IppValue<Range[]>.NoValue
        };

        var list = _mapper.Map<List<IppAttribute>>(src);

        list.Should().NotBeNull();
        list.Should().HaveCount(3);
        list.Should().ContainSingle(x => x.Name == "pages" && x.Tag == Tag.NoValue && x.Value is NoValue);
        list.Should().ContainSingle(x => x.Name == "document-numbers" && x.Tag == Tag.NoValue && x.Value is NoValue);
        list.Should().ContainSingle(x => x.Name == "document-copies" && x.Tag == Tag.NoValue && x.Value is NoValue);
    }

    [TestMethod]
    public void Map_FromDictionary_WithNoValueAttributes_MapsToNoValueIppValues()
    {
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "pages", new[] { new IppAttribute(Tag.NoValue, "pages", NoValue.Instance) } },
            { "document-numbers", new[] { new IppAttribute(Tag.NoValue, "document-numbers", NoValue.Instance) } },
            { "document-copies", new[] { new IppAttribute(Tag.NoValue, "document-copies", NoValue.Instance) } },
            { "media", new[] { new IppAttribute(Tag.Keyword, "media", "iso_a4_210x297mm") } }
        };

        var result = _mapper.Map<OverrideInstruction>(dict);

        result.Should().NotBeNull();
        result.PageRanges.Should().NotBeNull();
        result.PageRanges!.Value.IsValue.Should().BeFalse();
        result.DocumentNumberRanges.Should().NotBeNull();
        result.DocumentNumberRanges!.Value.IsValue.Should().BeFalse();
        result.DocumentCopyRanges.Should().NotBeNull();
        result.DocumentCopyRanges!.Value.IsValue.Should().BeFalse();
        result.JobTemplateAttributes.Should().NotBeNull();
        result.JobTemplateAttributes!.Media.Should().Be((Media)"iso_a4_210x297mm");
    }

    [TestMethod]
    public void Map_FromDictionary_WithConcreteRanges_MapsToValueRanges()
    {
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "pages", new[] { new IppAttribute(Tag.RangeOfInteger, "pages", new Range(1, 3)) } },
            { "document-numbers", new[] { new IppAttribute(Tag.RangeOfInteger, "document-numbers", new Range(2, 4)) } },
            { "document-copies", new[] { new IppAttribute(Tag.RangeOfInteger, "document-copies", new Range(1, 2)) } },
            { "sides", new[] { new IppAttribute(Tag.Keyword, "sides", "one-sided") } }
        };

        var result = _mapper.Map<OverrideInstruction>(dict);

        result.Should().NotBeNull();
        result.PageRanges.Should().NotBeNull();
        result.PageRanges!.Value.IsValue.Should().BeTrue();
        result.PageRanges.Value.Value.Should().BeEquivalentTo(new[] { new Range(1, 3) });
        result.DocumentNumberRanges.Should().NotBeNull();
        result.DocumentNumberRanges!.Value.IsValue.Should().BeTrue();
        result.DocumentNumberRanges.Value.Value.Should().BeEquivalentTo(new[] { new Range(2, 4) });
        result.DocumentCopyRanges.Should().NotBeNull();
        result.DocumentCopyRanges!.Value.IsValue.Should().BeTrue();
        result.DocumentCopyRanges.Value.Value.Should().BeEquivalentTo(new[] { new Range(1, 2) });
        result.JobTemplateAttributes.Should().NotBeNull();
        result.JobTemplateAttributes!.Sides.Should().Be(Sides.OneSided);
    }

    [TestMethod]
    public void Map_FromDictionary_Empty_ReturnsOverrideInstructionWithNullProperties()
    {
        var dict = new Dictionary<string, IppAttribute[]>();

        var result = _mapper.Map<OverrideInstruction>(dict);

        result.Should().NotBeNull();
        result.PageRanges.Should().BeNull();
        result.DocumentNumberRanges.Should().BeNull();
        result.DocumentCopyRanges.Should().BeNull();
        result.JobTemplateAttributes.Should().BeNull();
    }

    [TestMethod]
    public void Map_RoundTrip_WithNoValueRanges_PreservesValues()
    {
        var original = new OverrideInstruction
        {
            PageRanges = IppValue<Range[]>.NoValue,
            DocumentNumberRanges = IppValue<Range[]>.NoValue,
            DocumentCopyRanges = IppValue<Range[]>.NoValue,
            JobTemplateAttributes = new JobTemplateAttributes
            {
                Media = (Media)"iso_a4_210x297mm",
                Sides = Sides.OneSided
            }
        };

        var dict = _mapper.Map<IDictionary<string, IppAttribute[]>>(original);
        var roundTripped = _mapper.Map<OverrideInstruction>(dict);

        roundTripped.PageRanges.Should().Be(original.PageRanges);
        roundTripped.DocumentNumberRanges.Should().Be(original.DocumentNumberRanges);
        roundTripped.DocumentCopyRanges.Should().Be(original.DocumentCopyRanges);
        roundTripped.JobTemplateAttributes!.Media.Should().Be(original.JobTemplateAttributes.Media);
        roundTripped.JobTemplateAttributes!.Sides.Should().Be(original.JobTemplateAttributes.Sides);
    }

    [TestMethod]
    public void Map_RoundTrip_WithConcreteRanges_PreservesValues()
    {
        var original = new OverrideInstruction
        {
            PageRanges = new[] { new Range(1, 2), new Range(5, 6) },
            DocumentNumberRanges = new[] { new Range(1, 1) },
            DocumentCopyRanges = new[] { new Range(2, 3) },
            JobTemplateAttributes = new JobTemplateAttributes
            {
                Copies = 2,
                Sides = Sides.TwoSidedLongEdge
            }
        };

        var dict = _mapper.Map<Dictionary<string, IppAttribute[]>>(original);
        var roundTripped = _mapper.Map<OverrideInstruction>(dict);

        roundTripped.PageRanges.Should().Be(original.PageRanges);
        roundTripped.DocumentNumberRanges.Should().Be(original.DocumentNumberRanges);
        roundTripped.DocumentCopyRanges.Should().Be(original.DocumentCopyRanges);
        roundTripped.JobTemplateAttributes!.Copies.Should().Be(original.JobTemplateAttributes.Copies);
        roundTripped.JobTemplateAttributes!.Sides.Should().Be(original.JobTemplateAttributes.Sides);
    }

    [TestMethod]
    public void Map_ToAttributes_WithEmptyOverrideInstruction_ReturnsEmptyList()
    {
        var src = new OverrideInstruction();

        var attributes = _mapper.Map<IEnumerable<IppAttribute>>(src).ToList();

        attributes.Should().BeEmpty();
    }
}