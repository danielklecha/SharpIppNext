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
    public void Map_ToRequest_WithNamedOutputBin_UsesNameWithoutLanguageTag()
    {
        var src = new JobTemplateAttributes
        {
            OutputBin = new OutputBin("custom-finisher-bin", false)
        };

        var request = _mapper.Map<IppRequestMessage>(src);

        var outputBin = request.JobAttributes.Single(a => a.Name == JobAttribute.OutputBin);
        outputBin.Tag.Should().Be(Tag.NameWithoutLanguage);
        outputBin.Value.Should().Be("custom-finisher-bin");
    }

    [TestMethod]
    public void Map_ToRequest_WithExtensionKeywordOutputBin_UsesKeywordTag()
    {
        var src = new JobTemplateAttributes
        {
            OutputBin = new OutputBin("vendor-bin-42", true)
        };

        var request = _mapper.Map<IppRequestMessage>(src);

        var outputBin = request.JobAttributes.Single(a => a.Name == JobAttribute.OutputBin);
        outputBin.Tag.Should().Be(Tag.Keyword);
        outputBin.Value.Should().Be("vendor-bin-42");
    }

    [TestMethod]
    public void Map_FromRequest_WithNamedOutputBin_PreservesNameIntent()
    {
        var request = new IppRequestMessage();
        request.JobAttributes.Add(new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.OutputBin, "custom-finisher-bin"));

        var mapped = _mapper.Map<IIppRequestMessage, JobTemplateAttributes>(request);

        mapped.OutputBin.Should().Be(new OutputBin("custom-finisher-bin", false));
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
            Media = new Media("Accounting Team", false),
            ImpositionTemplate = new ImpositionTemplate("Layout A", false)
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
    public void Map_ToRequest_WithFinishingsAndFinishingsCol_MapsAndValidatorRejects()
    {
        var src = new JobTemplateAttributes
        {
            Finishings = new[] { Finishings.Staple },
            FinishingsCol = new[] { new FinishingsCol { FinishingTemplate = FinishingTemplate.Staple } }
        };

        var request = _mapper.Map<IppRequestMessage>(src);
        request.IppOperation = IppOperation.CreateJob;
        request.RequestId = 123;
        request.OperationAttributes.AddRange(
        [
            new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en"),
            new IppAttribute(Tag.Uri, JobAttribute.PrinterUri, "ipp://127.0.0.1:631/")
        ]);

        var validator = new IppRequestValidator
        {
            ValidateOperationSpecificRules = false
        };

        Action act = () => validator.Validate(request);

        act.Should().Throw<SharpIpp.Exceptions.IppRequestException>()
            .WithMessage("'finishings' and 'finishings-col' are conflicting attributes and cannot be supplied together.");
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
            PrintObjects = [new PrintObject { DocumentNumber = 1, PrintObjectsSource = "ipp://example/doc/1" }],
            PrintSupports = (PrintSupports?)"generated-supports"
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
    public void Map_ToRequest_WithFaxOutRetryFields_WritesIntegerAttributes()
    {
        var src = new JobTemplateAttributes
        {
            RetryInterval = 15,
            RetryTimeOut = 300
        };

        var request = _mapper.Map<IppRequestMessage>(src);

        request.JobAttributes.Should().Contain(x => x.Name == JobAttribute.RetryInterval && x.Tag == Tag.Integer && (int)x.Value! == 15);
        request.JobAttributes.Should().Contain(x => x.Name == JobAttribute.RetryTimeOut && x.Tag == Tag.Integer && (int)x.Value! == 300);
    }

    [TestMethod]
    public void Map_FromRequest_With3DJobTemplateAttributes_ReadsJobAttributes()
    {
        var request = new IppRequestMessage();
        request.JobAttributes.AddRange(_mapper.Map<Material, IEnumerable<IppAttribute>>(new Material { MaterialName = "pla" }).ToBegCollection(JobAttribute.MaterialsCol));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.MultipleObjectHandling, "abort-job"));
        request.JobAttributes.Add(new IppAttribute(Tag.Integer, JobAttribute.PlatformTemperature, 75));
        request.JobAttributes.AddRange(_mapper.Map<PrintAccuracy, IEnumerable<IppAttribute>>(new PrintAccuracy { AccuracyUnits = (AccuracyUnits?)"mm", XAccuracy = 100, YAccuracy = 100, ZAccuracy = 50 }).ToBegCollection(JobAttribute.PrintAccuracy));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.PrintBase, "raft"));
        request.JobAttributes.AddRange(_mapper.Map<PrintObject, IEnumerable<IppAttribute>>(new PrintObject { DocumentNumber = 1, PrintObjectsSource = "ipp://example/doc/1" }).ToBegCollection(JobAttribute.PrintObjects));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.PrintSupports, "generated-supports"));

        var dst = _mapper.Map<IIppRequestMessage, JobTemplateAttributes>((IIppRequestMessage)request);

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
    }

    [TestMethod]
    public void Map_OverrideInstruction_WithRangeSelectorsAndOverrideMembers_RoundTrips()
    {
        var src = new JobTemplateAttributes
        {
            Overrides =
            [
                new OverrideInstruction
                {
                    PageRanges =
                    [
                        new SharpIpp.Protocol.Models.Range(1, 1),
                        new SharpIpp.Protocol.Models.Range(3, 4)
                    ],
                    DocumentNumberRanges = [new SharpIpp.Protocol.Models.Range(1, 2147483647)],
                    DocumentCopyRanges = [new SharpIpp.Protocol.Models.Range(2, 2)],
                    JobTemplateAttributes = new JobTemplateAttributes
                    {
                        Media = (Media)"iso_a4_210x297mm",
                        Sides = Sides.OneSided,
                        NumberUp = 2
                    }
                }
            ]
        };

        var request = _mapper.Map<IppRequestMessage>(src);
        request.JobAttributes.Should().Contain(a => a.Tag == Tag.RangeOfInteger && Equals(a.Value, new SharpIpp.Protocol.Models.Range(1, 1)));
        request.JobAttributes.Should().Contain(a => a.Tag == Tag.RangeOfInteger && Equals(a.Value, new SharpIpp.Protocol.Models.Range(3, 4)));
        request.JobAttributes.Should().Contain(a => a.Tag == Tag.RangeOfInteger && Equals(a.Value, new SharpIpp.Protocol.Models.Range(1, 2147483647)));
        request.JobAttributes.Should().Contain(a => a.Tag == Tag.RangeOfInteger && Equals(a.Value, new SharpIpp.Protocol.Models.Range(2, 2)));

        var roundTripped = _mapper.Map<IIppRequestMessage, JobTemplateAttributes>((IIppRequestMessage)request);
        roundTripped.Overrides.Should().NotBeNull();
        roundTripped.Overrides!.Should().HaveCount(1);
        roundTripped.Overrides[0].PageRanges.Should().BeEquivalentTo(src.Overrides![0].PageRanges);
        roundTripped.Overrides[0].DocumentNumberRanges.Should().BeEquivalentTo(src.Overrides[0].DocumentNumberRanges);
        roundTripped.Overrides[0].DocumentCopyRanges.Should().BeEquivalentTo(src.Overrides[0].DocumentCopyRanges);
        roundTripped.Overrides[0].JobTemplateAttributes.Should().NotBeNull();
        roundTripped.Overrides[0].JobTemplateAttributes!.Media.Should().Be((Media)"iso_a4_210x297mm");
        roundTripped.Overrides[0].JobTemplateAttributes!.Sides.Should().Be(Sides.OneSided);
        roundTripped.Overrides[0].JobTemplateAttributes!.NumberUp.Should().Be(2);
    }

    [TestMethod]
    public void Map_OverrideInstruction_FiltersNestedOverridesAndOverridesActualMembers()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        mapper.CreateMap<JobTemplateAttributes, IppRequestMessage>((_, dst, _) =>
        {
            dst ??= new IppRequestMessage();
            dst.JobAttributes.Add(new IppAttribute(Tag.Integer, JobAttribute.Copies, 3));
            dst.JobAttributes.Add(new IppAttribute(Tag.Integer, JobAttribute.Overrides, 1));
            dst.JobAttributes.Add(new IppAttribute(Tag.Integer, JobAttribute.OverridesActual, 2));
            return dst;
        });

        var src = new OverrideInstruction
        {
            JobTemplateAttributes = new JobTemplateAttributes()
        };

        var attributes = mapper.Map<OverrideInstruction, IEnumerable<IppAttribute>>(src).ToArray();

        attributes.Should().ContainSingle(a => a.Name == JobAttribute.Copies && a.Tag == Tag.Integer && Equals(a.Value, 3));
        attributes.Should().NotContain(a => a.Name == JobAttribute.Overrides);
        attributes.Should().NotContain(a => a.Name == JobAttribute.OverridesActual);
    }
}
