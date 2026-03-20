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
            { PrinterAttribute.PdfVersionsSupported, [new IppAttribute(Tag.Keyword, PrinterAttribute.PdfVersionsSupported, "1.7"), new IppAttribute(Tag.Keyword, PrinterAttribute.PdfVersionsSupported, "2.0")] },
            { PrinterAttribute.IppFeaturesSupported, [new IppAttribute(Tag.Keyword, PrinterAttribute.IppFeaturesSupported, "subscription"), new IppAttribute(Tag.Keyword, PrinterAttribute.IppFeaturesSupported, "events")] },
            { PrinterAttribute.PrinterServiceType, [new IppAttribute(Tag.Keyword, PrinterAttribute.PrinterServiceType, "print"), new IppAttribute(Tag.Keyword, PrinterAttribute.PrinterServiceType, "fax") ] },
            { PrinterAttribute.CompressionDefault, [new IppAttribute(Tag.Keyword, PrinterAttribute.CompressionDefault, Compression.Deflate.ToString())] },
            { PrinterAttribute.PrinterResourceIds, [new IppAttribute(Tag.Integer, PrinterAttribute.PrinterResourceIds, 10), new IppAttribute(Tag.Integer, PrinterAttribute.PrinterResourceIds, 20)] }
        };

        var dst = _mapper.Map<IDictionary<string, IppAttribute[]>, PrinterDescriptionAttributes>(src);

        dst.PdfVersionsSupported.Should().BeEquivalentTo("1.7", "2.0");
        dst.IppFeaturesSupported.Should().BeEquivalentTo("subscription", "events");
        dst.PrinterServiceType.Should().BeEquivalentTo("print", "fax");
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
        dst.PrinterAlert.Should().BeEquivalentTo("alert-raw");
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
            PdfVersionsSupported = ["1.7", "2.0"],
            IppFeaturesSupported = ["subscription", "events"],
            PrinterServiceType = ["print", "fax"],
            CompressionDefault = Compression.Deflate,
            PrinterResourceIds = [10, 20]
        };

        var dst = _mapper.Map<PrinterDescriptionAttributes, IDictionary<string, IppAttribute[]>>(src);

        dst.Should().ContainKey(PrinterAttribute.PdfVersionsSupported);
        dst[PrinterAttribute.PdfVersionsSupported].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Keyword);
        dst[PrinterAttribute.PdfVersionsSupported].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["1.7", "2.0"]);

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
            PrinterAlert = ["alert-raw"],
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
}
