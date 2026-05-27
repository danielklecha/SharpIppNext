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
using SharpIpp.Tests.Unit.Mapping;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class JobTemplateAttributesProfileTests : MapperTestBase
{

    [TestMethod]
    public void Map_ToRequest_WithNoValueFinishingsCol_WritesOutOfBandNoValue()
    {
        var src = new JobTemplateAttributes
        {
            FinishingsCol = new[] { NoValue.GetNoValue<FinishingsCol>() }
        };

        var request = _mapper.Map<IppRequestMessage>(src);

        var finishingsCol = request.JobAttributes.Single(a => a.Name == IppAttributeNames.FinishingsCol);
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

        var outputBin = request.JobAttributes.Single(a => a.Name == IppAttributeNames.OutputBin);
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

        var outputBin = request.JobAttributes.Single(a => a.Name == IppAttributeNames.OutputBin);
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

        var outputBin = request.JobAttributes.Single(a => a.Name == IppAttributeNames.OutputBin);
        outputBin.Tag.Should().Be(Tag.Keyword);
        outputBin.Value.Should().Be("vendor-bin-42");
    }

    [TestMethod]
    public void Map_FromRequest_WithNamedOutputBin_PreservesNameIntent()
    {
        var request = new IppRequestMessage();
        request.JobAttributes.Add(new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.OutputBin, "custom-finisher-bin"));

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

            var outputBin = request.JobAttributes.Single(a => a.Name == IppAttributeNames.OutputBin);
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

        var media = request.JobAttributes.Single(a => a.Name == IppAttributeNames.Media);
        media.Tag.Should().Be(Tag.NameWithoutLanguage);
        media.Value.Should().Be("Accounting Team");

        var impositionTemplate = request.JobAttributes.Single(a => a.Name == IppAttributeNames.ImpositionTemplate);
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

        var finishings = request.JobAttributes.Where(a => a.Name == IppAttributeNames.Finishings).ToArray();
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
            new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"),
            new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "ipp://127.0.0.1:631/")
        ]);

        var validator = new IppRequestMessageValidator
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

        var mediaCol = request.JobAttributes.Single(a => a.Name == IppAttributeNames.MediaCol);
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
            PrintObjects = [new PrintObject { DocumentNumber = 1, PrintObjectsSource = new Uri("ipp://example/doc/1") }],
            PrintSupports = (PrintSupports?)"generated-supports",
            ChamberHumidity = 45,
            ChamberTemperature = 60
        };

        var request = _mapper.Map<IppRequestMessage>(src);

        request.JobAttributes.Should().Contain(a => a.Name == IppAttributeNames.MaterialsCol);
        request.JobAttributes.Should().Contain(a => a.Name == IppAttributeNames.MultipleObjectHandling && a.Tag == Tag.Keyword && Equals(a.Value, "abort-job"));
        request.JobAttributes.Should().Contain(a => a.Name == IppAttributeNames.PlatformTemperature && a.Tag == Tag.Integer && Equals(a.Value, 75));
        request.JobAttributes.Should().Contain(a => a.Name == IppAttributeNames.PrintAccuracy);
        request.JobAttributes.Should().Contain(a => a.Name == IppAttributeNames.PrintBase && a.Tag == Tag.Keyword && Equals(a.Value, "raft"));
        request.JobAttributes.Should().Contain(a => a.Name == IppAttributeNames.PrintObjects);
        request.JobAttributes.Should().Contain(a => a.Name == IppAttributeNames.PrintSupports && a.Tag == Tag.Keyword && Equals(a.Value, "generated-supports"));
        request.JobAttributes.Should().Contain(a => a.Name == IppAttributeNames.ChamberHumidity && a.Tag == Tag.Integer && Equals(a.Value, 45));
        request.JobAttributes.Should().Contain(a => a.Name == IppAttributeNames.ChamberTemperature && a.Tag == Tag.Integer && Equals(a.Value, 60));
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

        request.JobAttributes.Should().Contain(x => x.Name == IppAttributeNames.RetryInterval && x.Tag == Tag.Integer && (int)x.Value! == 15);
        request.JobAttributes.Should().Contain(x => x.Name == IppAttributeNames.RetryTimeOut && x.Tag == Tag.Integer && (int)x.Value! == 300);
    }

    [TestMethod]
    public void Map_FromRequest_With3DJobTemplateAttributes_ReadsJobAttributes()
    {
        var request = new IppRequestMessage();
        request.JobAttributes.AddRange(_mapper.Map<Material, IEnumerable<IppAttribute>>(new Material { MaterialName = "pla" }).ToBegCollection(IppAttributeNames.MaterialsCol));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.MultipleObjectHandling, "abort-job"));
        request.JobAttributes.Add(new IppAttribute(Tag.Integer, IppAttributeNames.PlatformTemperature, 75));
        request.JobAttributes.AddRange(_mapper.Map<PrintAccuracy, IEnumerable<IppAttribute>>(new PrintAccuracy { AccuracyUnits = (AccuracyUnits?)"mm", XAccuracy = 100, YAccuracy = 100, ZAccuracy = 50 }).ToBegCollection(IppAttributeNames.PrintAccuracy));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.PrintBase, "raft"));
        request.JobAttributes.AddRange(_mapper.Map<PrintObject, IEnumerable<IppAttribute>>(new PrintObject { DocumentNumber = 1, PrintObjectsSource = new Uri("ipp://example/doc/1") }).ToBegCollection(IppAttributeNames.PrintObjects));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.PrintSupports, "generated-supports"));
        request.JobAttributes.Add(new IppAttribute(Tag.Integer, IppAttributeNames.ChamberHumidity, 45));
        request.JobAttributes.Add(new IppAttribute(Tag.Integer, IppAttributeNames.ChamberTemperature, 60));

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
        dst.ChamberHumidity.Should().Be(45);
        dst.ChamberTemperature.Should().Be(60);
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
            dst.JobAttributes.Add(new IppAttribute(Tag.Integer, IppAttributeNames.Copies, 3));
            dst.JobAttributes.Add(new IppAttribute(Tag.Integer, IppAttributeNames.Overrides, 1));
            dst.JobAttributes.Add(new IppAttribute(Tag.Integer, IppAttributeNames.OverridesActual, 2));
            return dst;
        });

        var src = new OverrideInstruction
        {
            JobTemplateAttributes = new JobTemplateAttributes()
        };

        var attributes = mapper.Map<OverrideInstruction, IEnumerable<IppAttribute>>(src).ToArray();

        attributes.Should().ContainSingle(a => a.Name == IppAttributeNames.Copies && a.Tag == Tag.Integer && Equals(a.Value, 3));
        attributes.Should().NotContain(a => a.Name == IppAttributeNames.Overrides);
        attributes.Should().NotContain(a => a.Name == IppAttributeNames.OverridesActual);
    }

    [TestMethod]
    public void Map_ToRequest_WithJobCopiesAndJobCoversAndJobFinishings_WritesAttributes()
    {
        var src = new JobTemplateAttributes
        {
            JobCopies = 3,
            JobCoverBack = new Cover { CoverType = CoverType.PrintBack, Media = (Media)"iso_a4_210x297mm" },
            JobCoverFront = new Cover { CoverType = CoverType.PrintFront, Media = (Media)"iso_a4_210x297mm" },
            JobFinishings = new[] { Finishings.Staple },
            JobFinishingsCol = new[] { new FinishingsCol { FinishingTemplate = FinishingTemplate.Staple } },
            SheetCollate = "collated",
            PageOverrides = new[] { new OverrideInstruction { PageRanges = [new SharpIpp.Protocol.Models.Range(1, 1)], JobTemplateAttributes = new JobTemplateAttributes { Sides = Sides.OneSided } } },
            PagesPerSubset = new[] { 4, 8 },
            DocumentOverrides = new[] { new OverrideInstruction { PageRanges = [new SharpIpp.Protocol.Models.Range(1, 2)], JobTemplateAttributes = new JobTemplateAttributes { Copies = 2 } } },
            MediaSource = MediaSource.Main,
            MediaSourceFeedDirection = MediaSourceFeedDirection.LongEdgeFirst,
            MediaSourceFeedOrientation = Orientation.Portrait,
            RequestingUserUri = new Uri("mailto:user@example.com"),
            JobMandatoryAttributes = new[] { "copies", "sides" },
            JobIds = new[] { 101, 102 },
        };

        var request = _mapper.Map<IppRequestMessage>(src);

        request.JobAttributes.Should().Contain(a => a.Name == IppAttributeNames.JobCopies && a.Tag == Tag.Integer && Equals(a.Value, 3));
        request.JobAttributes.Should().Contain(a => a.Name == IppAttributeNames.JobCoverBack);
        request.JobAttributes.Should().Contain(a => a.Name == IppAttributeNames.JobCoverFront);
        request.JobAttributes.Should().Contain(a => a.Name == IppAttributeNames.JobFinishings && a.Tag == Tag.Enum && Equals(a.Value, (int)Finishings.Staple));
        request.JobAttributes.Should().Contain(a => a.Name == IppAttributeNames.JobFinishingsCol);
        request.JobAttributes.Should().Contain(a => a.Name == IppAttributeNames.SheetCollate && a.Tag == Tag.Keyword && Equals(a.Value, "collated"));
        request.JobAttributes.Should().Contain(a => a.Name == IppAttributeNames.PageOverrides);
        request.JobAttributes.Should().Contain(a => a.Name == IppAttributeNames.PagesPerSubset && a.Tag == Tag.Integer && Equals(a.Value, 4));
        request.JobAttributes.Should().Contain(a => a.Name == IppAttributeNames.PagesPerSubset && a.Tag == Tag.Integer && Equals(a.Value, 8));
        request.JobAttributes.Should().Contain(a => a.Name == IppAttributeNames.DocumentOverrides);
        request.JobAttributes.Should().Contain(a => a.Name == IppAttributeNames.MediaSource && a.Tag == Tag.Keyword);
        request.JobAttributes.Should().Contain(a => a.Name == IppAttributeNames.MediaSourceFeedDirection && a.Tag == Tag.Keyword);
        request.JobAttributes.Should().Contain(a => a.Name == IppAttributeNames.MediaSourceFeedOrientation && a.Tag == Tag.Enum);
        request.JobAttributes.Should().Contain(a => a.Name == IppAttributeNames.RequestingUserUri && a.Tag == Tag.Uri && Equals(a.Value, "mailto:user@example.com"));
        request.JobAttributes.Should().Contain(a => a.Name == IppAttributeNames.JobMandatoryAttributes && a.Tag == Tag.Keyword && Equals(a.Value, "copies"));
        request.JobAttributes.Should().Contain(a => a.Name == IppAttributeNames.JobMandatoryAttributes && a.Tag == Tag.Keyword && Equals(a.Value, "sides"));
        request.JobAttributes.Should().Contain(a => a.Name == IppAttributeNames.JobIds && a.Tag == Tag.Integer && Equals(a.Value, 101));
        request.JobAttributes.Should().Contain(a => a.Name == IppAttributeNames.JobIds && a.Tag == Tag.Integer && Equals(a.Value, 102));
    }

    [TestMethod]
    public void Map_FromRequest_WithJobCopiesAndJobCoversAndJobFinishings_ReadsAttributes()
    {
        var request = new IppRequestMessage();
        request.JobAttributes.Add(new IppAttribute(Tag.Integer, IppAttributeNames.JobCopies, 3));
        request.JobAttributes.AddRange(_mapper.Map<Cover, IEnumerable<IppAttribute>>(new Cover { CoverType = CoverType.PrintBack }).ToBegCollection(IppAttributeNames.JobCoverBack));
        request.JobAttributes.AddRange(_mapper.Map<Cover, IEnumerable<IppAttribute>>(new Cover { CoverType = CoverType.PrintFront }).ToBegCollection(IppAttributeNames.JobCoverFront));
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, IppAttributeNames.JobFinishings, (int)Finishings.Staple));
        request.JobAttributes.AddRange(_mapper.Map<FinishingsCol, IEnumerable<IppAttribute>>(new FinishingsCol { FinishingTemplate = FinishingTemplate.Staple }).ToBegCollection(IppAttributeNames.JobFinishingsCol));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.SheetCollate, "collated"));
        request.JobAttributes.Add(new IppAttribute(Tag.Integer, IppAttributeNames.PagesPerSubset, 4));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.MediaSource, "main"));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.MediaSourceFeedDirection, "long-edge-first"));
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, IppAttributeNames.MediaSourceFeedOrientation, (int)Orientation.Portrait));
        request.JobAttributes.Add(new IppAttribute(Tag.Uri, IppAttributeNames.RequestingUserUri, "mailto:user@example.com"));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.JobMandatoryAttributes, "copies"));
        request.JobAttributes.Add(new IppAttribute(Tag.Integer, IppAttributeNames.JobIds, 101));

        var dst = _mapper.Map<IIppRequestMessage, JobTemplateAttributes>((IIppRequestMessage)request);

        dst.JobCopies.Should().Be(3);
        dst.JobCoverBack.Should().NotBeNull();
        dst.JobCoverFront.Should().NotBeNull();
        dst.JobFinishings.Should().Contain(Finishings.Staple);
        dst.JobFinishingsCol.Should().NotBeNull();
        dst.SheetCollate.Should().Be(SheetCollate.Collated);
        dst.PagesPerSubset.Should().Contain(4);
        dst.MediaSource.Should().Be(MediaSource.Main);
        dst.MediaSourceFeedDirection.Should().Be(MediaSourceFeedDirection.LongEdgeFirst);
        dst.MediaSourceFeedOrientation.Should().Be(Orientation.Portrait);
        dst.RequestingUserUri.Should().Be(new Uri("mailto:user@example.com"));
        dst.JobMandatoryAttributes.Should().Contain("copies");
        dst.JobIds.Should().Contain(101);
    }
}
