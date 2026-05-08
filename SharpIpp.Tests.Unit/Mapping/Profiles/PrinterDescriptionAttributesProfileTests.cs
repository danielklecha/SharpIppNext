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
            },
            {
                PrinterAttribute.PlatformShape,
                [new IppAttribute(Tag.Keyword, PrinterAttribute.PlatformShape, "rectangular")]
            },
            {
                PrinterAttribute.PrintBaseDefault,
                [new IppAttribute(Tag.Keyword, PrinterAttribute.PrintBaseDefault, "raft")]
            },
            {
                PrinterAttribute.PrintSupportsSupported,
                [new IppAttribute(Tag.Keyword, PrinterAttribute.PrintSupportsSupported, "none"), new IppAttribute(Tag.Keyword, PrinterAttribute.PrintSupportsSupported, "standard")]
            }
        };

        var dst = _mapper.Map<IDictionary<string, IppAttribute[]>, PrinterDescriptionAttributes>(src);

        dst.AccuracyUnitsSupported.Should().BeEquivalentTo(new[] { (AccuracyUnits)"mm", (AccuracyUnits)"um" });
        dst.PrinterCameraImageUri.Should().BeEquivalentTo("https://printer.example/camera");
        dst.PlatformShape.Should().Be((PlatformShape?)"rectangular");
        dst.PrintBaseDefault.Should().Be((PrintBase?)"raft");
        dst.PrintSupportsSupported.Should().BeEquivalentTo(new[] { (PrintSupports)"none", (PrintSupports)"standard" });
    }

    [TestMethod]
    public void Map_PrinterDescriptionAttributes_FromDictionary_MapsPrinterRequestedJobAttributes()
    {
        var src = new Dictionary<string, IppAttribute[]>
        {
            { PrinterAttribute.PrinterRequestedJobAttributes, [new IppAttribute(Tag.Keyword, PrinterAttribute.PrinterRequestedJobAttributes, "job-account-id"), new IppAttribute(Tag.Keyword, PrinterAttribute.PrinterRequestedJobAttributes, "job-account-type")] }
        };

        var dst = _mapper.Map<IDictionary<string, IppAttribute[]>, PrinterDescriptionAttributes>(src);

        dst.PrinterRequestedJobAttributes!.Select(x => x.Value).Should().BeEquivalentTo("job-account-id", "job-account-type");
    }

    [TestMethod]
    public void Map_PrinterDescriptionAttributes_FromDictionary_MapsPdfFeaturesServiceAndResources()
    {
        var src = new Dictionary<string, IppAttribute[]>
        {
            { PrinterAttribute.PdfVersionsSupported, [new IppAttribute(Tag.Keyword, PrinterAttribute.PdfVersionsSupported, "adobe-1.7"), new IppAttribute(Tag.Keyword, PrinterAttribute.PdfVersionsSupported, "iso-32000-2_2017")] },
            { PrinterAttribute.IppFeaturesSupported, [new IppAttribute(Tag.Keyword, PrinterAttribute.IppFeaturesSupported, "subscription"), new IppAttribute(Tag.Keyword, PrinterAttribute.IppFeaturesSupported, "events"), new IppAttribute(Tag.Keyword, PrinterAttribute.IppFeaturesSupported, "faxout")] },
            { PrinterAttribute.PrinterServiceType, [new IppAttribute(Tag.Keyword, PrinterAttribute.PrinterServiceType, "print"), new IppAttribute(Tag.Keyword, PrinterAttribute.PrinterServiceType, "fax") ] },
            { PrinterAttribute.CompressionDefault, [new IppAttribute(Tag.Keyword, PrinterAttribute.CompressionDefault, Compression.Deflate.ToString())] },
            { PrinterAttribute.PrinterResourceIds, [new IppAttribute(Tag.Integer, PrinterAttribute.PrinterResourceIds, 10), new IppAttribute(Tag.Integer, PrinterAttribute.PrinterResourceIds, 20)] }
        };

        var dst = _mapper.Map<IDictionary<string, IppAttribute[]>, PrinterDescriptionAttributes>(src);

        dst.PdfVersionsSupported.Should().BeEquivalentTo([PdfVersion.Adobe17, PdfVersion.Iso3200022017]);
        dst.IppFeaturesSupported.Should().BeEquivalentTo([(IppFeature)"subscription", (IppFeature)"events", IppFeature.FaxOut]);
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
            { PrinterAttribute.PrinterAlert, [new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, PrinterAttribute.PrinterAlert, "code=jam;severity=critical")] },
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
        dst.PrinterAlert[0].Code.Should().Be("jam");
        dst.PrinterAlert[0].Severity.Should().Be("critical");
        dst.PrinterAlertDescription.Should().BeEquivalentTo("alert text");
        dst.PrinterSupply.Should().BeEquivalentTo("supply-raw");
        dst.PrinterSupplyDescription.Should().BeEquivalentTo("toner status");
    }

    [TestMethod]
    public void Map_PrinterDescriptionAttributes_ToDictionary_Maps3DPrinterAttributes()
    {
        var src = new PrinterDescriptionAttributes
        {
            AccuracyUnitsSupported = new[] { (AccuracyUnits)"mm", (AccuracyUnits)"um" },
            PlatformShape = (PlatformShape?)"rectangular",
            PrintBaseDefault = (PrintBase?)"raft",
            PrintSupportsSupported = new[] { (PrintSupports)"none", (PrintSupports)"standard" },
            PrinterVolumeSupported = new PrinterVolumeSupported
            {
                XDimension = 20000,
                YDimension = 20000,
                ZDimension = 18000
            },
            PrinterCameraImageUri = ["https://printer.example/camera"]
        };

        var dst = _mapper.Map<PrinterDescriptionAttributes, IDictionary<string, IppAttribute[]>>(src);

        dst.Should().ContainKey(PrinterAttribute.AccuracyUnitsSupported);
        dst[PrinterAttribute.AccuracyUnitsSupported].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Keyword);
        dst[PrinterAttribute.AccuracyUnitsSupported].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["mm", "um"]);

        dst.Should().ContainKey(PrinterAttribute.PrinterCameraImageUri);
        dst[PrinterAttribute.PrinterCameraImageUri].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Uri);
        dst[PrinterAttribute.PrinterCameraImageUri].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["https://printer.example/camera"]);

        dst.Should().ContainKey(PrinterAttribute.PlatformShape);
        dst[PrinterAttribute.PlatformShape].Single().Tag.Should().Be(Tag.Keyword);
        dst[PrinterAttribute.PlatformShape].Single().Value.Should().Be("rectangular");

        dst.Should().ContainKey(PrinterAttribute.PrintBaseDefault);
        dst[PrinterAttribute.PrintBaseDefault].Single().Tag.Should().Be(Tag.Keyword);
        dst[PrinterAttribute.PrintBaseDefault].Single().Value.Should().Be("raft");

        dst.Should().ContainKey(PrinterAttribute.PrintSupportsSupported);
        dst[PrinterAttribute.PrintSupportsSupported].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["none", "standard"]);

        dst.Should().ContainKey(PrinterAttribute.PrinterVolumeSupported);
        dst[PrinterAttribute.PrinterVolumeSupported].Any(x => x.Tag == Tag.BegCollection).Should().BeTrue();
        dst[PrinterAttribute.PrinterVolumeSupported].Any(x => x.Tag == Tag.MemberAttrName && Equals(x.Value, "x-dimension")).Should().BeTrue();
        dst[PrinterAttribute.PrinterVolumeSupported].Any(x => x.Tag == Tag.MemberAttrName && Equals(x.Value, "y-dimension")).Should().BeTrue();
        dst[PrinterAttribute.PrinterVolumeSupported].Any(x => x.Tag == Tag.MemberAttrName && Equals(x.Value, "z-dimension")).Should().BeTrue();
        dst[PrinterAttribute.PrinterVolumeSupported].Count(x => x.Tag == Tag.Integer).Should().Be(3);
    }

    [TestMethod]
    public void Map_PrinterDescriptionAttributes_ToDictionary_MapsPrinterRequestedJobAttributes()
    {
        var src = new PrinterDescriptionAttributes
        {
            PrinterRequestedJobAttributes = [(PrinterRequestedJobAttribute)"job-account-id", (PrinterRequestedJobAttribute)"job-account-type"]
        };

        var dst = _mapper.Map<PrinterDescriptionAttributes, IDictionary<string, IppAttribute[]>>(src);

        dst.Should().ContainKey(PrinterAttribute.PrinterRequestedJobAttributes);
        dst[PrinterAttribute.PrinterRequestedJobAttributes].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Keyword);
        dst[PrinterAttribute.PrinterRequestedJobAttributes].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["job-account-id", "job-account-type"]);
    }

    [TestMethod]
    public void Map_PrinterDescriptionAttributes_ToDictionary_MapsPdfFeaturesServiceAndResources()
    {
        var src = new PrinterDescriptionAttributes
        {
            PdfVersionsSupported = [PdfVersion.Adobe17, PdfVersion.Iso3200022017],
            IppFeaturesSupported = [(IppFeature)"subscription", (IppFeature)"events", IppFeature.FaxOut],
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
        dst[PrinterAttribute.IppFeaturesSupported].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["subscription", "events", "faxout"]);

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
            PrinterAlert = [new PrinterAlert { Code = "jam", Severity = "critical" }],
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
        dst[PrinterAttribute.PrinterAlert].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["code=jam;severity=critical"]);

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
    public void Map_PrinterDescriptionAttributes_FromDictionary_MapsPwg51017ScanAttributes()
    {
        var src = new Dictionary<string, IppAttribute[]>
        {
            {
                PrinterAttribute.DestinationAccessesSupported,
                [
                    new IppAttribute(Tag.Keyword, PrinterAttribute.DestinationAccessesSupported, "access-user-name"),
                    new IppAttribute(Tag.Keyword, PrinterAttribute.DestinationAccessesSupported, "access-password")
                ]
            },
            {
                PrinterAttribute.JobDestinationSpoolingSupported,
                [new IppAttribute(Tag.Keyword, PrinterAttribute.JobDestinationSpoolingSupported, "automatic")]
            },
            {
                PrinterAttribute.OutputAttributesDefault,
                [
                    new IppAttribute(Tag.BegCollection, PrinterAttribute.OutputAttributesDefault, NoValue.Instance),
                    new IppAttribute(Tag.MemberAttrName, "", "noise-removal"),
                    new IppAttribute(Tag.Integer, "", 50),
                    new IppAttribute(Tag.MemberAttrName, "", "output-compression-quality-factor"),
                    new IppAttribute(Tag.Integer, "", 70),
                    new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
                ]
            },
            {
                PrinterAttribute.OutputAttributesSupported,
                [
                    new IppAttribute(Tag.Keyword, PrinterAttribute.OutputAttributesSupported, "noise-removal"),
                    new IppAttribute(Tag.Keyword, PrinterAttribute.OutputAttributesSupported, "output-compression-quality-factor")
                ]
            }
        };

        var dst = _mapper.Map<IDictionary<string, IppAttribute[]>, PrinterDescriptionAttributes>(src);

        dst.DestinationAccessesSupported!.Select(x => x.Value).Should().BeEquivalentTo("access-user-name", "access-password");
        dst.JobDestinationSpoolingSupported.Should().Be(JobSpooling.Automatic);
        dst.OutputAttributesDefault.Should().NotBeNull();
        dst.OutputAttributesDefault!.NoiseRemoval.Should().Be(50);
        dst.OutputAttributesDefault.OutputCompressionQualityFactor.Should().Be(70);
        dst.OutputAttributesSupported!.Select(x => x.Value).Should().BeEquivalentTo("noise-removal", "output-compression-quality-factor");
    }

    [TestMethod]
    public void Map_PrinterDescriptionAttributes_ToDictionary_MapsPwg51017ScanAttributes()
    {
        var src = new PrinterDescriptionAttributes
        {
            DestinationAccessesSupported = [DestinationAccessMember.AccessUserName, DestinationAccessMember.AccessPassword],
            JobDestinationSpoolingSupported = JobSpooling.Automatic,
            OutputAttributesDefault = new OutputAttributes
            {
                NoiseRemoval = 50,
                OutputCompressionQualityFactor = 70
            },
            OutputAttributesSupported = [(OutputAttributesMember)"noise-removal", (OutputAttributesMember)"output-compression-quality-factor"]
        };

        var dst = _mapper.Map<PrinterDescriptionAttributes, IDictionary<string, IppAttribute[]>>(src);

        dst.Should().ContainKey(PrinterAttribute.DestinationAccessesSupported);
        dst[PrinterAttribute.DestinationAccessesSupported].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Keyword);
        dst[PrinterAttribute.DestinationAccessesSupported].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["access-user-name", "access-password"]);

        dst.Should().ContainKey(PrinterAttribute.JobDestinationSpoolingSupported);
        dst[PrinterAttribute.JobDestinationSpoolingSupported].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Keyword);
        dst[PrinterAttribute.JobDestinationSpoolingSupported].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["automatic"]);

        dst.Should().ContainKey(PrinterAttribute.OutputAttributesDefault);
        dst[PrinterAttribute.OutputAttributesDefault].Should().Contain(x => x.Tag == Tag.MemberAttrName && x.Value!.Equals("noise-removal"));
        dst[PrinterAttribute.OutputAttributesDefault].Should().Contain(x => x.Tag == Tag.MemberAttrName && x.Value!.Equals("output-compression-quality-factor"));
        dst[PrinterAttribute.OutputAttributesDefault].Should().Contain(x => x.Tag == Tag.Integer && x.Value!.Equals(50));
        dst[PrinterAttribute.OutputAttributesDefault].Should().Contain(x => x.Tag == Tag.Integer && x.Value!.Equals(70));

        dst.Should().ContainKey(PrinterAttribute.OutputAttributesSupported);
        dst[PrinterAttribute.OutputAttributesSupported].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Keyword);
        dst[PrinterAttribute.OutputAttributesSupported].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["noise-removal", "output-compression-quality-factor"]);
    }

    [TestMethod]
    public void Map_PrinterDescriptionAttributes_FromDictionary_MapsDestinationUriReady()
    {
        var src = new Dictionary<string, IppAttribute[]>
        {
            {
                PrinterAttribute.DestinationUriReady,
                [
                    new IppAttribute(Tag.BegCollection, PrinterAttribute.DestinationUriReady, NoValue.Instance),
                    new IppAttribute(Tag.MemberAttrName, "", "destination-uri"),
                    new IppAttribute(Tag.Uri, "", "https://example.test/upload"),
                    new IppAttribute(Tag.MemberAttrName, "", "destination-name"),
                    new IppAttribute(Tag.NameWithoutLanguage, "", "Inbox"),
                    new IppAttribute(Tag.MemberAttrName, "", "destination-is-directory"),
                    new IppAttribute(Tag.Boolean, "", true),
                    new IppAttribute(Tag.MemberAttrName, "", "destination-mandatory-access-attributes"),
                    new IppAttribute(Tag.Keyword, "", "access-user-name"),
                    new IppAttribute(Tag.MemberAttrName, "", "destination-mandatory-access-attributes"),
                    new IppAttribute(Tag.Keyword, "", "access-password"),
                    new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
                ]
            }
        };

        var dst = _mapper.Map<IDictionary<string, IppAttribute[]>, PrinterDescriptionAttributes>(src);

        dst.DestinationUriReady.Should().NotBeNull();
        dst.DestinationUriReady!.Should().ContainSingle();
        dst.DestinationUriReady[0].DestinationUri.Should().Be("https://example.test/upload");
        dst.DestinationUriReady[0].DestinationName.Should().Be("Inbox");
        dst.DestinationUriReady[0].DestinationIsDirectory.Should().BeTrue();
        dst.DestinationUriReady[0].DestinationMandatoryAccessAttributes.Should().BeEquivalentTo("access-user-name", "access-password");
    }

    [TestMethod]
    public void Map_PrinterDescriptionAttributes_ToDictionary_MapsDestinationUriReady()
    {
        var src = new PrinterDescriptionAttributes
        {
            DestinationUriReady =
            [
                new DestinationUriReady
                {
                    DestinationUri = "https://example.test/upload",
                    DestinationName = "Inbox",
                    DestinationIsDirectory = true,
                    DestinationMandatoryAccessAttributes = ["access-user-name", "access-password"]
                }
            ]
        };

        var dst = _mapper.Map<PrinterDescriptionAttributes, IDictionary<string, IppAttribute[]>>(src);

        dst.Should().ContainKey(PrinterAttribute.DestinationUriReady);
        dst[PrinterAttribute.DestinationUriReady].Should().Contain(x => x.Tag == Tag.BegCollection && x.Name == PrinterAttribute.DestinationUriReady);
        dst[PrinterAttribute.DestinationUriReady].Should().Contain(x => x.Tag == Tag.MemberAttrName && x.Value!.Equals("destination-uri"));
        dst[PrinterAttribute.DestinationUriReady].Should().Contain(x => x.Tag == Tag.Uri && x.Value!.Equals("https://example.test/upload"));
        dst[PrinterAttribute.DestinationUriReady].Should().Contain(x => x.Tag == Tag.MemberAttrName && x.Value!.Equals("destination-name"));
        dst[PrinterAttribute.DestinationUriReady].Should().Contain(x => x.Tag == Tag.NameWithoutLanguage && x.Value!.Equals("Inbox"));
        dst[PrinterAttribute.DestinationUriReady].Should().Contain(x => x.Tag == Tag.MemberAttrName && x.Value!.Equals("destination-is-directory"));
        dst[PrinterAttribute.DestinationUriReady].Should().Contain(x => x.Tag == Tag.Boolean && x.Value!.Equals(true));
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
