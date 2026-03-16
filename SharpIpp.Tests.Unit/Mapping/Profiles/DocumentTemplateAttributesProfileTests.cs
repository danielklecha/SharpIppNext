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
public class DocumentTemplateAttributesProfileTests
{
    private readonly IMapper _mapper;

    public DocumentTemplateAttributesProfileTests()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);
        _mapper = mapper;
    }

    [TestMethod]
    public void Map_ToAttributes_WithNoValueFinishingsCol_WritesOutOfBandNoValue()
    {
        var src = new DocumentTemplateAttributes
        {
            FinishingsCol = new[] { NoValue.GetNoValue<FinishingsCol>() }
        };

        var attributes = _mapper.Map<System.Collections.Generic.List<IppAttribute>>(src);

        var finishingsCol = attributes.Single(a => a.Name == JobAttribute.FinishingsCol);
        finishingsCol.Tag.Should().Be(Tag.NoValue);
        finishingsCol.Value.Should().Be(NoValue.Instance);
    }

    [TestMethod]
    public void Map_ToAttributes_WithKeywordOutputBin_UsesKeywordTag()
    {
        var src = new DocumentTemplateAttributes
        {
            OutputBin = OutputBin.Top
        };

        var attributes = _mapper.Map<System.Collections.Generic.List<IppAttribute>>(src);

        var outputBin = attributes.Single(a => a.Name == JobAttribute.OutputBin);
        outputBin.Tag.Should().Be(Tag.Keyword);
        outputBin.Value.Should().Be("top");
    }

    [TestMethod]
    public void Map_ToAttributes_WithNameOutputBin_UsesNameWithoutLanguageTag()
    {
        var src = new DocumentTemplateAttributes
        {
            OutputBin = (OutputBin)"custom-finisher-bin"
        };

        var attributes = _mapper.Map<System.Collections.Generic.List<IppAttribute>>(src);

        var outputBin = attributes.Single(a => a.Name == JobAttribute.OutputBin);
        outputBin.Tag.Should().Be(Tag.NameWithoutLanguage);
        outputBin.Value.Should().Be("custom-finisher-bin");
    }

    [TestMethod]
    public void Map_ToAttributes_WithCustomMediaAndImpositionTemplate_UsesNameWithoutLanguageTag()
    {
        var src = new DocumentTemplateAttributes
        {
            Media = (Media)"Accounting Team",
            ImpositionTemplate = (ImpositionTemplate)"Layout A"
        };

        var attributes = _mapper.Map<System.Collections.Generic.List<IppAttribute>>(src);

        var media = attributes.Single(a => a.Name == JobAttribute.Media);
        media.Tag.Should().Be(Tag.NameWithoutLanguage);
        media.Value.Should().Be("Accounting Team");

        var impositionTemplate = attributes.Single(a => a.Name == JobAttribute.ImpositionTemplate);
        impositionTemplate.Tag.Should().Be(Tag.NameWithoutLanguage);
        impositionTemplate.Value.Should().Be("Layout A");
    }

    [TestMethod]
    public void Map_ToAttributes_WithNoneAndOtherFinishings_IgnoresNoneValue()
    {
        var src = new DocumentTemplateAttributes
        {
            Finishings = new[] { Finishings.None, Finishings.Staple }
        };

        var attributes = _mapper.Map<System.Collections.Generic.List<IppAttribute>>(src);

        var finishings = attributes.Where(a => a.Name == JobAttribute.Finishings).ToArray();
        finishings.Should().HaveCount(1);
        finishings[0].Tag.Should().Be(Tag.Enum);
        finishings[0].Value.Should().Be((int)Finishings.Staple);
    }

    [TestMethod]
    public void Map_ToAttributes_WithFinishingsAndFinishingsCol_Throws()
    {
        var src = new DocumentTemplateAttributes
        {
            Finishings = new[] { Finishings.Staple },
            FinishingsCol = new[] { new FinishingsCol { FinishingTemplate = FinishingTemplate.Staple } }
        };

        var act = () => _mapper.Map<System.Collections.Generic.List<IppAttribute>>(src);

        act.Should().Throw<ArgumentException>();
    }
}
