using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class JobTemplateAttributesProfileTests
{
    private readonly IMapper _mapper;

    public JobTemplateAttributesProfileTests()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);
        _mapper = mapper;
    }

    [TestMethod]
    public void Map_ToRequest_WithNoValueFinishingsCol_WritesOutOfBandNoValue()
    {
        var src = new JobTemplateAttributes
        {
            FinishingsCol = new[] { NoValue.GetNoValue<FinishingsCol>() }
        };

        var request = _mapper.Map<IppRequestMessage>(src);

        var finishingsCol = request.JobAttributes.Single(a => a.Name == JobAttribute.FinishingsCol);
        finishingsCol.Tag.Should().Be(Tag.NoValue);
        finishingsCol.Value.Should().Be(NoValue.Instance);
    }

    [TestMethod]
    public void Map_ToRequest_WithKeywordOutputBin_UsesKeywordTag()
    {
        var src = new JobTemplateAttributes
        {
            OutputBin = OutputBin.Top
        };

        var request = _mapper.Map<IppRequestMessage>(src);

        var outputBin = request.JobAttributes.Single(a => a.Name == JobAttribute.OutputBin);
        outputBin.Tag.Should().Be(Tag.Keyword);
        outputBin.Value.Should().Be("top");
    }

    [TestMethod]
    public void Map_ToRequest_WithNameOutputBin_UsesNameWithoutLanguageTag()
    {
        var src = new JobTemplateAttributes
        {
            OutputBin = (OutputBin)"custom-finisher-bin"
        };

        var request = _mapper.Map<IppRequestMessage>(src);

        var outputBin = request.JobAttributes.Single(a => a.Name == JobAttribute.OutputBin);
        outputBin.Tag.Should().Be(Tag.NameWithoutLanguage);
        outputBin.Value.Should().Be("custom-finisher-bin");
    }

    [TestMethod]
    public void Map_ToRequest_WithCustomMediaAndImpositionTemplate_UsesNameWithoutLanguageTag()
    {
        var src = new JobTemplateAttributes
        {
            Media = (Media)"Accounting Team",
            ImpositionTemplate = (ImpositionTemplate)"Layout A"
        };

        var request = _mapper.Map<IppRequestMessage>(src);

        var media = request.JobAttributes.Single(a => a.Name == JobAttribute.Media);
        media.Tag.Should().Be(Tag.NameWithoutLanguage);
        media.Value.Should().Be("Accounting Team");

        var impositionTemplate = request.JobAttributes.Single(a => a.Name == JobAttribute.ImpositionTemplate);
        impositionTemplate.Tag.Should().Be(Tag.NameWithoutLanguage);
        impositionTemplate.Value.Should().Be("Layout A");
    }

    [TestMethod]
    public void Map_ToRequest_WithNoneAndOtherFinishings_IgnoresNoneValue()
    {
        var src = new JobTemplateAttributes
        {
            Finishings = new[] { Finishings.None, Finishings.Staple }
        };

        var request = _mapper.Map<IppRequestMessage>(src);

        var finishings = request.JobAttributes.Where(a => a.Name == JobAttribute.Finishings).ToArray();
        finishings.Should().HaveCount(1);
        finishings[0].Tag.Should().Be(Tag.Enum);
        finishings[0].Value.Should().Be((int)Finishings.Staple);
    }

    [TestMethod]
    public void Map_ToRequest_WithFinishingsAndFinishingsCol_Throws()
    {
        var src = new JobTemplateAttributes
        {
            Finishings = new[] { Finishings.Staple },
            FinishingsCol = new[] { new FinishingsCol { FinishingTemplate = FinishingTemplate.Staple } }
        };

        var act = () => _mapper.Map<IppRequestMessage>(src);

        act.Should().Throw<ArgumentException>();
    }
}
