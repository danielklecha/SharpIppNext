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
    public void Map_ToRequest_WithGeneratedKeywordOutputBins_UsesKeywordTag()
    {
        var sources = new[]
        {
            OutputBin.Stacker(3),
            OutputBin.Mailbox(12),
            OutputBin.Tray(5)
        };

        foreach (var source in sources)
        {
            var request = _mapper.Map<IppRequestMessage>(new JobTemplateAttributes
            {
                OutputBin = source
            });

            var outputBin = request.JobAttributes.Single(a => a.Name == JobAttribute.OutputBin);
            outputBin.Tag.Should().Be(Tag.Keyword);
            outputBin.Value.Should().Be(source.Value);
        }
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

    [TestMethod]
    public void Map_ToRequest_WithNoValueMediaCol_WritesOutOfBandNoValue()
    {
        var src = new JobTemplateAttributes
        {
            MediaCol = NoValue.GetNoValue<MediaCol>()
        };

        var request = _mapper.Map<IppRequestMessage>(src);

        var mediaCol = request.JobAttributes.Single(a => a.Name == JobAttribute.MediaCol);
        mediaCol.Tag.Should().Be(Tag.NoValue);
        mediaCol.Value.Should().Be(NoValue.Instance);
    }

    [TestMethod]
    public void Map_ToRequest_With3DJobTemplateAttributes_WritesJobAttributes()
    {
        var src = new JobTemplateAttributes
        {
            MaterialsCol = [new Material { MaterialName = "pla" }],
            MultipleObjectHandling = "abort-job",
            PlatformTemperature = 75,
            PrintAccuracy = new PrintAccuracy
            {
                AccuracyUnits = "mm",
                XAccuracy = 100,
                YAccuracy = 100,
                ZAccuracy = 50
            },
            PrintBase = "raft",
            PrintObjects = [new PrintObject { DocumentNumber = 1, PrintObjectsSource = "ipp://example/doc/1" }],
            PrintSupports = "generated-supports"
        };

        var request = _mapper.Map<IppRequestMessage>(src);

        request.JobAttributes.Should().Contain(a => a.Name == JobAttribute.MaterialsCol);
        request.JobAttributes.Should().Contain(a => a.Name == JobAttribute.MultipleObjectHandling && a.Tag == Tag.Keyword && Equals(a.Value, "abort-job"));
        request.JobAttributes.Should().Contain(a => a.Name == JobAttribute.PlatformTemperature && a.Tag == Tag.Integer && Equals(a.Value, 75));
        request.JobAttributes.Should().Contain(a => a.Name == JobAttribute.PrintAccuracy);
        request.JobAttributes.Should().Contain(a => a.Name == JobAttribute.PrintBase && a.Tag == Tag.Keyword && Equals(a.Value, "raft"));
        request.JobAttributes.Should().Contain(a => a.Name == JobAttribute.PrintObjects);
        request.JobAttributes.Should().Contain(a => a.Name == JobAttribute.PrintSupports && a.Tag == Tag.Keyword && Equals(a.Value, "generated-supports"));
    }

    [TestMethod]
    public void Map_FromRequest_With3DJobTemplateAttributes_ReadsJobAttributes()
    {
        var request = new IppRequestMessage();
        request.JobAttributes.AddRange(_mapper.Map<Material, IEnumerable<IppAttribute>>(new Material { MaterialName = "pla" }).ToBegCollection(JobAttribute.MaterialsCol));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.MultipleObjectHandling, "abort-job"));
        request.JobAttributes.Add(new IppAttribute(Tag.Integer, JobAttribute.PlatformTemperature, 75));
        request.JobAttributes.AddRange(_mapper.Map<PrintAccuracy, IEnumerable<IppAttribute>>(new PrintAccuracy { AccuracyUnits = "mm", XAccuracy = 100, YAccuracy = 100, ZAccuracy = 50 }).ToBegCollection(JobAttribute.PrintAccuracy));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.PrintBase, "raft"));
        request.JobAttributes.AddRange(_mapper.Map<PrintObject, IEnumerable<IppAttribute>>(new PrintObject { DocumentNumber = 1, PrintObjectsSource = "ipp://example/doc/1" }).ToBegCollection(JobAttribute.PrintObjects));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.PrintSupports, "generated-supports"));

        var dst = _mapper.Map<IIppRequestMessage, JobTemplateAttributes>((IIppRequestMessage)request);

        dst.MaterialsCol.Should().NotBeNull();
        dst.MaterialsCol![0].MaterialName.Should().Be("pla");
        dst.MultipleObjectHandling.Should().Be("abort-job");
        dst.PlatformTemperature.Should().Be(75);
        dst.PrintAccuracy.Should().NotBeNull();
        dst.PrintAccuracy!.AccuracyUnits.Should().Be("mm");
        dst.PrintBase.Should().Be("raft");
        dst.PrintObjects.Should().NotBeNull();
        dst.PrintObjects![0].DocumentNumber.Should().Be(1);
        dst.PrintSupports.Should().Be("generated-supports");
    }
}
