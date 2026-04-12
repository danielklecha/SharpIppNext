using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
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
    public void Map_ToAttributes_WithNamedOutputBin_UsesNameWithoutLanguageTag()
    {
        var src = new DocumentTemplateAttributes
        {
            OutputBin = new OutputBin("custom-finisher-bin", false)
        };

        var attributes = _mapper.Map<System.Collections.Generic.List<IppAttribute>>(src);

        var outputBin = attributes.Single(a => a.Name == JobAttribute.OutputBin);
        outputBin.Tag.Should().Be(Tag.NameWithoutLanguage);
        outputBin.Value.Should().Be("custom-finisher-bin");
    }

    [TestMethod]
    public void Map_ToAttributes_WithExtensionKeywordOutputBin_UsesKeywordTag()
    {
        var src = new DocumentTemplateAttributes
        {
            OutputBin = new OutputBin("vendor-bin-42", true)
        };

        var attributes = _mapper.Map<System.Collections.Generic.List<IppAttribute>>(src);

        var outputBin = attributes.Single(a => a.Name == JobAttribute.OutputBin);
        outputBin.Tag.Should().Be(Tag.Keyword);
        outputBin.Value.Should().Be("vendor-bin-42");
    }

    [TestMethod]
    public void Map_FromAttributes_WithNamedOutputBin_PreservesNameIntent()
    {
        var attributes = new[]
        {
            new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.OutputBin, "custom-finisher-bin")
        }.ToIppDictionary();

        var mapped = _mapper.Map<DocumentTemplateAttributes>(attributes);

        mapped.OutputBin.Should().Be(new OutputBin("custom-finisher-bin", false));
    }

    [TestMethod]
    public void Map_ToAttributes_WithCustomMediaAndImpositionTemplate_UsesNameWithoutLanguageTag()
    {
        var src = new DocumentTemplateAttributes
        {
            Media = new Media("Accounting Team", false),
            ImpositionTemplate = new ImpositionTemplate("Layout A", false)
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
    public void Map_ToAttributes_WithPageOrderReceived_WritesKeywordAttribute()
    {
        var src = new DocumentTemplateAttributes
        {
            PageOrderReceived = PageOrderReceived.NTo1Order
        };

        var attributes = _mapper.Map<System.Collections.Generic.List<IppAttribute>>(src);

        var pageOrderReceived = attributes.Single(a => a.Name == JobAttribute.PageOrderReceived);
        pageOrderReceived.Tag.Should().Be(Tag.Keyword);
        pageOrderReceived.Value.Should().Be("n-to-1-order");
    }

    [TestMethod]
    public void Map_FromAttributes_WithPageOrderReceived_ReadsKeywordAttribute()
    {
        var attributes = new[]
        {
            new IppAttribute(Tag.Keyword, JobAttribute.PageOrderReceived, "1-to-n-order")
        }.ToIppDictionary();

        var documentTemplate = _mapper.Map<DocumentTemplateAttributes>(attributes);

        documentTemplate.PageOrderReceived.Should().Be(PageOrderReceived.OneToNOrder);
    }

    [TestMethod]
    public void Map_ToAttributes_WithFinishingsAndFinishingsCol_MapsAndValidatorRejects()
    {
        var src = new DocumentTemplateAttributes
        {
            Finishings = new[] { Finishings.Staple },
            FinishingsCol = new[] { new FinishingsCol { FinishingTemplate = FinishingTemplate.Staple } }
        };

        var request = new IppRequestMessage
        {
            IppOperation = IppOperation.SendDocument,
            RequestId = 123,
        };
        request.OperationAttributes.AddRange(
        [
            new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en"),
            new IppAttribute(Tag.Uri, JobAttribute.PrinterUri, "ipp://127.0.0.1:631/"),
            new IppAttribute(Tag.Boolean, JobAttribute.LastDocument, true)
        ]);
        request.DocumentAttributes.AddRange(_mapper.Map<System.Collections.Generic.List<IppAttribute>>(src));

        var validator = new IppRequestValidator
        {
            ValidateJobAttributesGroup = false,
        };

        Action act = () => validator.Validate(request);

        act.Should().Throw<SharpIpp.Exceptions.IppRequestException>()
            .WithMessage("'finishings' and 'finishings-col' are conflicting attributes and cannot be supplied together.");
    }
}
