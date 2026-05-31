using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SharpIpp.Tests.Unit.Mapping;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class DocumentTemplateAttributesProfileTests : MapperTestBase
{

    [TestMethod]
    public void Map_ToAttributes_WithNoValueFinishingsCol_WritesOutOfBandNoValue()
    {
        var src = new DocumentTemplateAttributes
        {
            FinishingsCol = new[] { NoValue.GetNoValue<FinishingsCol>() }
        };

        var attributes = _mapper.Map<System.Collections.Generic.List<IppAttribute>>(src);

        var finishingsCol = attributes.Single(a => a.Name == IppAttributeNames.FinishingsCol);
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

        var outputBin = attributes.Single(a => a.Name == IppAttributeNames.OutputBin);
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

        var outputBin = attributes.Single(a => a.Name == IppAttributeNames.OutputBin);
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

        var outputBin = attributes.Single(a => a.Name == IppAttributeNames.OutputBin);
        outputBin.Tag.Should().Be(Tag.Keyword);
        outputBin.Value.Should().Be("vendor-bin-42");
    }

    [TestMethod]
    public void Map_FromAttributes_WithNamedOutputBin_PreservesNameIntent()
    {
        var attributes = new[]
        {
            new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.OutputBin, "custom-finisher-bin")
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

        var media = attributes.Single(a => a.Name == IppAttributeNames.Media);
        media.Tag.Should().Be(Tag.NameWithoutLanguage);
        media.Value.Should().Be("Accounting Team");

        var impositionTemplate = attributes.Single(a => a.Name == IppAttributeNames.ImpositionTemplate);
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

        var finishings = attributes.Where(a => a.Name == IppAttributeNames.Finishings).ToArray();
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

        var pageOrderReceived = attributes.Single(a => a.Name == IppAttributeNames.PageOrderReceived);
        pageOrderReceived.Tag.Should().Be(Tag.Keyword);
        pageOrderReceived.Value.Should().Be("n-to-1-order");
    }

    [TestMethod]
    public void Map_FromAttributes_WithPageOrderReceived_ReadsKeywordAttribute()
    {
        var attributes = new[]
        {
            new IppAttribute(Tag.Keyword, IppAttributeNames.PageOrderReceived, "1-to-n-order")
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
            new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"),
            new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "ipp://127.0.0.1:631/"),
            new IppAttribute(Tag.Boolean, IppAttributeNames.LastDocument, true)
        ]);
        request.DocumentAttributes.AddRange(_mapper.Map<System.Collections.Generic.List<IppAttribute>>(src));

        var validator = new IppRequestMessageValidator
        {
            ValidateJobAttributesGroup = false,
        };

        Action act = () => validator.Validate(request);

        act.Should().Throw<SharpIpp.Exceptions.IppRequestException>()
            .WithMessage("'finishings' and 'finishings-col' are conflicting attributes and cannot be supplied together.");
    }

    [TestMethod]
    public void Map_DocumentTemplateAttributes_ToAttributes_With3DAttributes_WritesCorrectAttributes()
    {
        var src = new DocumentTemplateAttributes
        {
            MaterialsCol = [new Material { MaterialName = "pla" }],
            MultipleObjectHandling = (MultipleObjectHandling?)"abort-job",
            PlatformTemperature = 75,
            PrintAccuracy = new PrintAccuracy
            {
                AccuracyUnits = (AccuracyUnits?)"mm",
                XAccuracy = 100,
                YAccuracy = 100,
                ZAccuracy = 50
            },
            PrintBase = (PrintBase?)"raft",
            PrintObjects = [new PrintObject { DocumentNumber = 1, PrintObjectsSource = new Uri("ipp://example/doc/1") }],
            PrintSupports = (PrintSupports?)"generated-supports",
            ChamberHumidity = 45,
            ChamberTemperature = 60
        };

        var attributes = _mapper.Map<System.Collections.Generic.List<IppAttribute>>(src);

        attributes.Should().Contain(a => a.Name == IppAttributeNames.MaterialsCol);
        attributes.Should().Contain(a => a.Name == IppAttributeNames.MultipleObjectHandling && a.Tag == Tag.Keyword && Equals(a.Value, "abort-job"));
        attributes.Should().Contain(a => a.Name == IppAttributeNames.PlatformTemperature && a.Tag == Tag.Integer && Equals(a.Value, 75));
        attributes.Should().Contain(a => a.Name == IppAttributeNames.PrintAccuracy);
        attributes.Should().Contain(a => a.Name == IppAttributeNames.PrintBase && a.Tag == Tag.Keyword && Equals(a.Value, "raft"));
        attributes.Should().Contain(a => a.Name == IppAttributeNames.PrintObjects);
        attributes.Should().Contain(a => a.Name == IppAttributeNames.PrintSupports && a.Tag == Tag.Keyword && Equals(a.Value, "generated-supports"));
        attributes.Should().Contain(a => a.Name == IppAttributeNames.ChamberHumidity && a.Tag == Tag.Integer && Equals(a.Value, 45));
        attributes.Should().Contain(a => a.Name == IppAttributeNames.ChamberTemperature && a.Tag == Tag.Integer && Equals(a.Value, 60));
    }

    [TestMethod]
    public void Map_DocumentTemplateAttributes_FromAttributes_With3DAttributes_ReadsCorrectAttributes()
    {
        var attributesList = new System.Collections.Generic.List<IppAttribute>();
        attributesList.AddRange(_mapper.Map<Material, System.Collections.Generic.IEnumerable<IppAttribute>>(new Material { MaterialName = "pla" }).ToBegCollection(IppAttributeNames.MaterialsCol));
        attributesList.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.MultipleObjectHandling, "abort-job"));
        attributesList.Add(new IppAttribute(Tag.Integer, IppAttributeNames.PlatformTemperature, 75));
        attributesList.AddRange(_mapper.Map<PrintAccuracy, System.Collections.Generic.IEnumerable<IppAttribute>>(new PrintAccuracy { AccuracyUnits = (AccuracyUnits?)"mm", XAccuracy = 100, YAccuracy = 100, ZAccuracy = 50 }).ToBegCollection(IppAttributeNames.PrintAccuracy));
        attributesList.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.PrintBase, "raft"));
        attributesList.AddRange(_mapper.Map<PrintObject, System.Collections.Generic.IEnumerable<IppAttribute>>(new PrintObject { DocumentNumber = 1, PrintObjectsSource = new Uri("ipp://example/doc/1") }).ToBegCollection(IppAttributeNames.PrintObjects));
        attributesList.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.PrintSupports, "generated-supports"));
        attributesList.Add(new IppAttribute(Tag.Integer, IppAttributeNames.ChamberHumidity, 45));
        attributesList.Add(new IppAttribute(Tag.Integer, IppAttributeNames.ChamberTemperature, 60));

        var attributes = attributesList.ToIppDictionary();
        var dst = _mapper.Map<DocumentTemplateAttributes>(attributes);

        dst.MaterialsCol.Should().NotBeNull();
        dst.MaterialsCol![0].MaterialName.Should().Be("pla");
        dst.MultipleObjectHandling.Should().Be((MultipleObjectHandling?)"abort-job");
        dst.PlatformTemperature.Should().Be(75);
        dst.PrintAccuracy.Should().NotBeNull();
        dst.PrintAccuracy!.AccuracyUnits.Should().Be((AccuracyUnits?)"mm");
        dst.PrintBase.Should().Be((PrintBase?)"raft");
        dst.PrintObjects.Should().NotBeNull();
        dst.PrintObjects![0].DocumentNumber.Should().Be(1);
        dst.PrintSupports.Should().Be((PrintSupports?)"generated-supports");
        dst.ChamberHumidity.Should().Be(45);
        dst.ChamberTemperature.Should().Be(60);
    }
}
