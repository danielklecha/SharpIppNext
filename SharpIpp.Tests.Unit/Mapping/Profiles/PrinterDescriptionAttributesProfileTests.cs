using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class PrinterDescriptionAttributesProfileTests
{
    private SimpleMapper _mapper = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _mapper = new SimpleMapper();
        var assembly = typeof(SimpleMapper).Assembly;
        _mapper.FillFromAssembly(assembly);
    }

    [TestMethod]
    public void Map_PrinterFinisherSupply_StringToModelAndBack_MapsCorrectly()
    {
        var raw = "class=supplyThatIsConsumed; type=staples; unit=items; max=500; level=100; color=silver; index=8; deviceIndex=3; test=test;";

        var supply = _mapper.Map<string, PrinterFinisherSupply>(raw);

        supply.Should().BeEquivalentTo(new PrinterFinisherSupply
        {
            Class = "supplyThatIsConsumed",
            Type = "staples",
            Unit = "items",
            Max = 500,
            Level = 100,
            Color = "silver",
            Index = 8,
            DeviceIndex = 3,
            Extensions = new Dictionary<string, string> { { "test", "test" } }
        });

        var serialized = _mapper.Map<PrinterFinisherSupply, string>(supply);

        serialized.Should().Be("class=supplyThatIsConsumed; type=staples; unit=items; max=500; level=100; color=silver; index=8; deviceIndex=3; test=test;");
    }

    [TestMethod]
    public void Map_PrinterDescriptionAttributes_ToDictionary_MapsNoValueFinishingsColDefaultsWithCorrectAttributeNames()
    {
        var src = new PrinterDescriptionAttributes
        {
            FinishingsColDefault = [NoValue.GetNoValue<FinishingsCol>()],
            FinishingsColDatabase = [NoValue.GetNoValue<FinishingsCol>()]
        };

        var dst = _mapper.Map<PrinterDescriptionAttributes, IDictionary<string, IppAttribute[]>>(src);

        dst.Should().ContainKey(PrinterAttribute.FinishingsColDefault);
        dst[PrinterAttribute.FinishingsColDefault].Should().ContainSingle();
        dst[PrinterAttribute.FinishingsColDefault][0].Tag.Should().Be(Tag.NoValue);
        dst[PrinterAttribute.FinishingsColDefault][0].Name.Should().Be(PrinterAttribute.FinishingsColDefault);

        dst.Should().ContainKey(PrinterAttribute.FinishingsColDatabase);
        dst[PrinterAttribute.FinishingsColDatabase].Should().ContainSingle();
        dst[PrinterAttribute.FinishingsColDatabase][0].Tag.Should().Be(Tag.NoValue);
        dst[PrinterAttribute.FinishingsColDatabase][0].Name.Should().Be(PrinterAttribute.FinishingsColDatabase);
    }

    [TestMethod]
    public void Map_PrinterDescriptionAttributes_FromDictionary_MapsNoValueFinishingsColDefaults()
    {
        var src = new Dictionary<string, IppAttribute[]>
        {
            { PrinterAttribute.FinishingsColDefault, [new IppAttribute(Tag.NoValue, PrinterAttribute.FinishingsColDefault, NoValue.Instance)] },
            { PrinterAttribute.FinishingsColDatabase, [new IppAttribute(Tag.NoValue, PrinterAttribute.FinishingsColDatabase, NoValue.Instance)] }
        };

        var dst = _mapper.Map<IDictionary<string, IppAttribute[]>, PrinterDescriptionAttributes>(src);

        dst.FinishingsColDefault.Should().NotBeNull();
        dst.FinishingsColDefault!.Should().ContainSingle();
        NoValue.IsNoValue(dst.FinishingsColDefault[0]).Should().BeTrue();

        dst.FinishingsColDatabase.Should().NotBeNull();
        dst.FinishingsColDatabase!.Should().ContainSingle();
        NoValue.IsNoValue(dst.FinishingsColDatabase[0]).Should().BeTrue();
    }

    [TestMethod]
    public void Map_PrinterDescriptionAttributes_FromDictionary_Maps3DPrinterAttributes()
    {
        var src = new Dictionary<string, IppAttribute[]>
        {
            {
                PrinterAttribute.AccuracyUnitsSupported,
                [
                    new IppAttribute(Tag.Keyword, PrinterAttribute.AccuracyUnitsSupported, "mm"),
                    new IppAttribute(Tag.Keyword, PrinterAttribute.AccuracyUnitsSupported, "um")
                ]
            },
            {
                PrinterAttribute.PrinterCameraImageUri,
                [new IppAttribute(Tag.Uri, PrinterAttribute.PrinterCameraImageUri, "https://printer.example/camera")]
            }
        };

        var dst = _mapper.Map<IDictionary<string, IppAttribute[]>, PrinterDescriptionAttributes>(src);

        dst.AccuracyUnitsSupported.Should().BeEquivalentTo("mm", "um");
        dst.PrinterCameraImageUri.Should().BeEquivalentTo("https://printer.example/camera");
    }

    [TestMethod]
    public void Map_PrinterDescriptionAttributes_FromDictionary_MapsPdfFeaturesServiceAndResources()
    {
        var src = new Dictionary<string, IppAttribute[]>
        {
            { PrinterAttribute.PdfVersionsSupported, [new IppAttribute(Tag.Keyword, PrinterAttribute.PdfVersionsSupported, "adobe-1.7"), new IppAttribute(Tag.Keyword, PrinterAttribute.PdfVersionsSupported, "iso-32000-2_2017")] },
            { PrinterAttribute.IppFeaturesSupported, [new IppAttribute(Tag.Keyword, PrinterAttribute.IppFeaturesSupported, "subscription"), new IppAttribute(Tag.Keyword, PrinterAttribute.IppFeaturesSupported, "events")] },
            { PrinterAttribute.PrinterServiceType, [new IppAttribute(Tag.Keyword, PrinterAttribute.PrinterServiceType, "print"), new IppAttribute(Tag.Keyword, PrinterAttribute.PrinterServiceType, "fax") ] },
            { PrinterAttribute.CompressionDefault, [new IppAttribute(Tag.Keyword, PrinterAttribute.CompressionDefault, Compression.Deflate.ToString())] },
            { PrinterAttribute.PrinterResourceIds, [new IppAttribute(Tag.Integer, PrinterAttribute.PrinterResourceIds, 10), new IppAttribute(Tag.Integer, PrinterAttribute.PrinterResourceIds, 20)] }
        };

        var dst = _mapper.Map<IDictionary<string, IppAttribute[]>, PrinterDescriptionAttributes>(src);

        dst.PdfVersionsSupported.Should().BeEquivalentTo([PdfVersion.Adobe17, PdfVersion.Iso3200022017]);
        dst.IppFeaturesSupported.Should().BeEquivalentTo([(IppFeature)"subscription", (IppFeature)"events"]);
        dst.PrinterServiceType.Should().BeEquivalentTo([PrinterServiceType.Print, (PrinterServiceType)"fax"]);
        dst.CompressionDefault.Should().Be(Compression.Deflate);
        dst.PrinterResourceIds.Should().BeEquivalentTo(new[] { 10, 20 });
    }

    [TestMethod]
    public void Map_PrinterDescriptionAttributes_FromDictionary_MapsAlertsSuppliesAndChangeTimes()
    {
        var stateChange = new DateTimeOffset(2024, 04, 01, 12, 00, 00, TimeSpan.Zero);
        var configChange = new DateTimeOffset(2024, 04, 02, 08, 30, 00, TimeSpan.Zero);

        var src = new Dictionary<string, IppAttribute[]>
        {
            { PrinterAttribute.PrinterStateChangeTime, [new IppAttribute(Tag.Integer, PrinterAttribute.PrinterStateChangeTime, 42)] },
            { PrinterAttribute.PrinterStateChangeDateTime, [new IppAttribute(Tag.DateTime, PrinterAttribute.PrinterStateChangeDateTime, stateChange)] },
            { PrinterAttribute.PrinterConfigChangeTime, [new IppAttribute(Tag.Integer, PrinterAttribute.PrinterConfigChangeTime, 84)] },
            { PrinterAttribute.PrinterConfigChangeDateTime, [new IppAttribute(Tag.DateTime, PrinterAttribute.PrinterConfigChangeDateTime, configChange)] },
            { PrinterAttribute.PrinterAlert, [new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, PrinterAttribute.PrinterAlert, "alert-raw")] },
            { PrinterAttribute.PrinterAlertDescription, [new IppAttribute(Tag.TextWithoutLanguage, PrinterAttribute.PrinterAlertDescription, "alert text")] },
            { PrinterAttribute.PrinterSupply, [new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, PrinterAttribute.PrinterSupply, "supply-raw")] },
            { PrinterAttribute.PrinterSupplyDescription, [new IppAttribute(Tag.TextWithoutLanguage, PrinterAttribute.PrinterSupplyDescription, "toner status")] }
        };

        var dst = _mapper.Map<IDictionary<string, IppAttribute[]>, PrinterDescriptionAttributes>(src);

        dst.PrinterStateChangeTime.Should().Be(42);
        dst.PrinterStateChangeDateTime.Should().Be(stateChange);
        dst.PrinterConfigChangeTime.Should().Be(84);
        dst.PrinterConfigChangeDateTime.Should().Be(configChange);
        dst.PrinterAlert.Should().NotBeNull();
        dst.PrinterAlert!.Should().ContainSingle();
        dst.PrinterAlert[0].RawValue.Should().Be("alert-raw");
        dst.PrinterAlert[0].Code.Should().BeNull();
        dst.PrinterAlertDescription.Should().BeEquivalentTo("alert text");
        dst.PrinterSupply.Should().BeEquivalentTo("supply-raw");
        dst.PrinterSupplyDescription.Should().BeEquivalentTo("toner status");
    }

    [TestMethod]
    public void Map_PrinterDescriptionAttributes_ToDictionary_Maps3DPrinterAttributes()
    {
        var src = new PrinterDescriptionAttributes
        {
            AccuracyUnitsSupported = ["mm", "um"],
            PrinterCameraImageUri = ["https://printer.example/camera"]
        };

        var dst = _mapper.Map<PrinterDescriptionAttributes, IDictionary<string, IppAttribute[]>>(src);

        dst.Should().ContainKey(PrinterAttribute.AccuracyUnitsSupported);
        dst[PrinterAttribute.AccuracyUnitsSupported].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Keyword);
        dst[PrinterAttribute.AccuracyUnitsSupported].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["mm", "um"]);

        dst.Should().ContainKey(PrinterAttribute.PrinterCameraImageUri);
        dst[PrinterAttribute.PrinterCameraImageUri].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Uri);
        dst[PrinterAttribute.PrinterCameraImageUri].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["https://printer.example/camera"]);
    }

    [TestMethod]
    public void Map_PrinterDescriptionAttributes_ToDictionary_MapsPdfFeaturesServiceAndResources()
    {
        var src = new PrinterDescriptionAttributes
        {
            PdfVersionsSupported = [PdfVersion.Adobe17, PdfVersion.Iso3200022017],
            IppFeaturesSupported = [(IppFeature)"subscription", (IppFeature)"events"],
            PrinterServiceType = [PrinterServiceType.Print, (PrinterServiceType)"fax"],
            CompressionDefault = Compression.Deflate,
            PrinterResourceIds = [10, 20]
        };

        var dst = _mapper.Map<PrinterDescriptionAttributes, IDictionary<string, IppAttribute[]>>(src);

        dst.Should().ContainKey(PrinterAttribute.PdfVersionsSupported);
        dst[PrinterAttribute.PdfVersionsSupported].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Keyword);
        dst[PrinterAttribute.PdfVersionsSupported].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["adobe-1.7", "iso-32000-2_2017"]);

        dst.Should().ContainKey(PrinterAttribute.IppFeaturesSupported);
        dst[PrinterAttribute.IppFeaturesSupported].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Keyword);
        dst[PrinterAttribute.IppFeaturesSupported].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["subscription", "events"]);

        dst.Should().ContainKey(PrinterAttribute.PrinterServiceType);
        dst[PrinterAttribute.PrinterServiceType].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Keyword);
        dst[PrinterAttribute.PrinterServiceType].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["print", "fax"]);

        dst.Should().ContainKey(PrinterAttribute.CompressionDefault);
        dst[PrinterAttribute.CompressionDefault].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Keyword);
        dst[PrinterAttribute.CompressionDefault].Select(x => x.Value?.ToString()).Should().BeEquivalentTo([Compression.Deflate.ToString()]);

        dst.Should().ContainKey(PrinterAttribute.PrinterResourceIds);
        dst[PrinterAttribute.PrinterResourceIds].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Integer);
        dst[PrinterAttribute.PrinterResourceIds].Select(x => (int)x.Value!).Should().BeEquivalentTo(new[] { 10, 20 });
    }

    [TestMethod]
    public void Map_PrinterDescriptionAttributes_ToDictionary_MapsAlertsSuppliesAndChangeTimes()
    {
        var stateChange = new DateTimeOffset(2024, 04, 01, 12, 00, 00, TimeSpan.Zero);
        var configChange = new DateTimeOffset(2024, 04, 02, 08, 30, 00, TimeSpan.Zero);

        var src = new PrinterDescriptionAttributes
        {
            PrinterStateChangeTime = 42,
            PrinterStateChangeDateTime = stateChange,
            PrinterConfigChangeTime = 84,
            PrinterConfigChangeDateTime = configChange,
            PrinterAlert = [new PrinterAlert { RawValue = "alert-raw" }],
            PrinterAlertDescription = ["alert text"],
            PrinterSupply = ["supply-raw"],
            PrinterSupplyDescription = ["toner status"]
        };

        var dst = _mapper.Map<PrinterDescriptionAttributes, IDictionary<string, IppAttribute[]>>(src);

        dst.Should().ContainKey(PrinterAttribute.PrinterStateChangeTime);
        dst[PrinterAttribute.PrinterStateChangeTime].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Integer);
        dst[PrinterAttribute.PrinterStateChangeTime].Select(x => (int)x.Value!).Should().BeEquivalentTo(new[] { 42 });

        dst.Should().ContainKey(PrinterAttribute.PrinterStateChangeDateTime);
        dst[PrinterAttribute.PrinterStateChangeDateTime].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.DateTime);
        dst[PrinterAttribute.PrinterStateChangeDateTime].Select(x => (DateTimeOffset)x.Value!).Should().BeEquivalentTo(new[] { stateChange });

        dst.Should().ContainKey(PrinterAttribute.PrinterConfigChangeTime);
        dst[PrinterAttribute.PrinterConfigChangeTime].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Integer);
        dst[PrinterAttribute.PrinterConfigChangeTime].Select(x => (int)x.Value!).Should().BeEquivalentTo(new[] { 84 });

        dst.Should().ContainKey(PrinterAttribute.PrinterConfigChangeDateTime);
        dst[PrinterAttribute.PrinterConfigChangeDateTime].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.DateTime);
        dst[PrinterAttribute.PrinterConfigChangeDateTime].Select(x => (DateTimeOffset)x.Value!).Should().BeEquivalentTo(new[] { configChange });

        dst.Should().ContainKey(PrinterAttribute.PrinterAlert);
        dst[PrinterAttribute.PrinterAlert].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.OctetStringWithAnUnspecifiedFormat);
        dst[PrinterAttribute.PrinterAlert].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["alert-raw"]);

        dst.Should().ContainKey(PrinterAttribute.PrinterAlertDescription);
        dst[PrinterAttribute.PrinterAlertDescription].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.TextWithoutLanguage);
        dst[PrinterAttribute.PrinterAlertDescription].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["alert text"]);

        dst.Should().ContainKey(PrinterAttribute.PrinterSupply);
        dst[PrinterAttribute.PrinterSupply].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.OctetStringWithAnUnspecifiedFormat);
        dst[PrinterAttribute.PrinterSupply].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["supply-raw"]);

        dst.Should().ContainKey(PrinterAttribute.PrinterSupplyDescription);
        dst[PrinterAttribute.PrinterSupplyDescription].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.TextWithoutLanguage);
        dst[PrinterAttribute.PrinterSupplyDescription].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["toner status"]);
    }

    [TestMethod]
    public void Map_PrinterDescriptionAttributes_ToDictionary_MapsStructuredPrinterAlertEntries()
    {
        var src = new PrinterDescriptionAttributes
        {
            PrinterAlert =
            [
                new PrinterAlert
                {
                    Code = "jam",
                    Index = 22,
                    Severity = "critical",
                    Group = "mediaPath",
                    GroupIndex = 4,
                    Location = 6
                }
            ]
        };

        var dst = _mapper.Map<PrinterDescriptionAttributes, IDictionary<string, IppAttribute[]>>(src);

        dst.Should().ContainKey(PrinterAttribute.PrinterAlert);
        dst[PrinterAttribute.PrinterAlert].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.OctetStringWithAnUnspecifiedFormat);
        dst[PrinterAttribute.PrinterAlert].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["code=jam;index=22;severity=critical;group=mediaPath;groupindex=4;location=6"]);
    }

    [TestMethod]
    public void Map_PrinterDescriptionAttributes_OutputBinExtension_RoundTripsKeywordAndNameValues()
    {
        var src = new PrinterDescriptionAttributes
        {
            OutputBinDefault = OutputBin.Stacker(3),
            OutputBinSupported = [OutputBin.Stacker(3), OutputBin.Mailbox(2), new OutputBin("vendor-bin-42", true), new OutputBin("Accounting Bin", false)]
        };

        var serialized = _mapper.Map<PrinterDescriptionAttributes, IDictionary<string, IppAttribute[]>>(src);

        serialized.Should().ContainKey(PrinterAttribute.OutputBinDefault);
        serialized[PrinterAttribute.OutputBinDefault].Single().Tag.Should().Be(Tag.Keyword);
        serialized[PrinterAttribute.OutputBinDefault].Single().Value.Should().Be("stacker-3");

        serialized.Should().ContainKey(PrinterAttribute.OutputBinSupported);
        serialized[PrinterAttribute.OutputBinSupported].Select(x => x.Tag).Should().BeEquivalentTo(
            [Tag.Keyword, Tag.Keyword, Tag.Keyword, Tag.NameWithoutLanguage],
            options => options.WithStrictOrdering());
        serialized[PrinterAttribute.OutputBinSupported].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(
            ["stacker-3", "mailbox-2", "vendor-bin-42", "Accounting Bin"],
            options => options.WithStrictOrdering());

        var roundTripped = _mapper.Map<IDictionary<string, IppAttribute[]>, PrinterDescriptionAttributes>(serialized);

        roundTripped.OutputBinDefault.Should().Be(OutputBin.Stacker(3));
        roundTripped.OutputBinSupported.Should().BeEquivalentTo(src.OutputBinSupported);
    }

    [TestMethod]
    public void Map_PrinterDescriptionAttributes_OutputBinExtension_ForcesNameTagWhenRequested()
    {
        var src = new PrinterDescriptionAttributes
        {
            OutputBinDefault = new OutputBin("custom-finisher-bin", false)
        };

        var serialized = _mapper.Map<PrinterDescriptionAttributes, IDictionary<string, IppAttribute[]>>(src);

        serialized.Should().ContainKey(PrinterAttribute.OutputBinDefault);
        serialized[PrinterAttribute.OutputBinDefault].Single().Tag.Should().Be(Tag.NameWithoutLanguage);
        serialized[PrinterAttribute.OutputBinDefault].Single().Value.Should().Be("custom-finisher-bin");

        var roundTripped = _mapper.Map<IDictionary<string, IppAttribute[]>, PrinterDescriptionAttributes>(serialized);
        roundTripped.OutputBinDefault.Should().Be(new OutputBin("custom-finisher-bin", false));
    }

    [TestMethod]
    public void Map_PrinterDescriptionAttributes_OverridesSupported_RoundTrips()
    {
        var src = new PrinterDescriptionAttributes
        {
            OverridesSupported =
            [
                OverrideSupported.Pages,
                OverrideSupported.DocumentNumbers,
                OverrideSupported.DocumentCopies,
                OverrideSupported.Sides,
                (OverrideSupported)"vendor-custom-override"
            ]
        };

        var serialized = _mapper.Map<PrinterDescriptionAttributes, IDictionary<string, IppAttribute[]>>(src);
        serialized.Should().ContainKey(PrinterAttribute.OverridesSupported);
        serialized[PrinterAttribute.OverridesSupported].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Keyword);
        serialized[PrinterAttribute.OverridesSupported].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(
            ["pages", "document-numbers", "document-copies", "sides", "vendor-custom-override"]);

        var roundTripped = _mapper.Map<IDictionary<string, IppAttribute[]>, PrinterDescriptionAttributes>(serialized);
        roundTripped.OverridesSupported.Should().BeEquivalentTo(src.OverridesSupported);
    }
}
