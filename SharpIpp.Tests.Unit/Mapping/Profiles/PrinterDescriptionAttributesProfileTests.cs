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

        dst.Should().ContainKey(IppAttributeNames.FinishingsColDefault);
        dst[IppAttributeNames.FinishingsColDefault].Should().ContainSingle();
        dst[IppAttributeNames.FinishingsColDefault][0].Tag.Should().Be(Tag.NoValue);
        dst[IppAttributeNames.FinishingsColDefault][0].Name.Should().Be(IppAttributeNames.FinishingsColDefault);

        dst.Should().ContainKey(IppAttributeNames.FinishingsColDatabase);
        dst[IppAttributeNames.FinishingsColDatabase].Should().ContainSingle();
        dst[IppAttributeNames.FinishingsColDatabase][0].Tag.Should().Be(Tag.NoValue);
        dst[IppAttributeNames.FinishingsColDatabase][0].Name.Should().Be(IppAttributeNames.FinishingsColDatabase);
    }

    [TestMethod]
    public void Map_PrinterDescriptionAttributes_FromDictionary_MapsNoValueFinishingsColDefaults()
    {
        var src = new Dictionary<string, IppAttribute[]>
        {
            { IppAttributeNames.FinishingsColDefault, [new IppAttribute(Tag.NoValue, IppAttributeNames.FinishingsColDefault, NoValue.Instance)] },
            { IppAttributeNames.FinishingsColDatabase, [new IppAttribute(Tag.NoValue, IppAttributeNames.FinishingsColDatabase, NoValue.Instance)] }
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
                IppAttributeNames.AccuracyUnitsSupported,
                [
                    new IppAttribute(Tag.Keyword, IppAttributeNames.AccuracyUnitsSupported, "mm"),
                    new IppAttribute(Tag.Keyword, IppAttributeNames.AccuracyUnitsSupported, "um")
                ]
            },
            {
                IppAttributeNames.PrinterCameraImageUri,
                [new IppAttribute(Tag.Uri, IppAttributeNames.PrinterCameraImageUri, "https://printer.example/camera")]
            },
            {
                IppAttributeNames.PlatformShape,
                [new IppAttribute(Tag.Keyword, IppAttributeNames.PlatformShape, "rectangular")]
            },
            {
                IppAttributeNames.PrintBaseDefault,
                [new IppAttribute(Tag.Keyword, IppAttributeNames.PrintBaseDefault, "raft")]
            },
            {
                IppAttributeNames.PrintSupportsSupported,
                [new IppAttribute(Tag.Keyword, IppAttributeNames.PrintSupportsSupported, "none"), new IppAttribute(Tag.Keyword, IppAttributeNames.PrintSupportsSupported, "standard")]
            }
        };

        var dst = _mapper.Map<IDictionary<string, IppAttribute[]>, PrinterDescriptionAttributes>(src);

        dst.AccuracyUnitsSupported.Should().BeEquivalentTo(new[] { (AccuracyUnits)"mm", (AccuracyUnits)"um" });
        dst.PrinterCameraImageUri.Should().BeEquivalentTo([new Uri("https://printer.example/camera")]);
        dst.PlatformShape.Should().Be((PlatformShape?)"rectangular");
        dst.PrintBaseDefault.Should().Be((PrintBase?)"raft");
        dst.PrintSupportsSupported.Should().BeEquivalentTo(new[] { (PrintSupports)"none", (PrintSupports)"standard" });
    }

    [TestMethod]
    public void Map_PrinterDescriptionAttributes_FromDictionary_MapsPrinterRequestedJobAttributes()
    {
        var src = new Dictionary<string, IppAttribute[]>
        {
            { IppAttributeNames.PrinterRequestedJobAttributes, [new IppAttribute(Tag.Keyword, IppAttributeNames.PrinterRequestedJobAttributes, "job-account-id"), new IppAttribute(Tag.Keyword, IppAttributeNames.PrinterRequestedJobAttributes, "job-account-type")] }
        };

        var dst = _mapper.Map<IDictionary<string, IppAttribute[]>, PrinterDescriptionAttributes>(src);

        dst.PrinterRequestedJobAttributes!.Select(x => x.Value).Should().BeEquivalentTo("job-account-id", "job-account-type");
    }

    [TestMethod]
    public void Map_PrinterDescriptionAttributes_FromDictionary_MapsPdfFeaturesServiceAndResources()
    {
        var src = new Dictionary<string, IppAttribute[]>
        {
            { IppAttributeNames.PdfVersionsSupported, [new IppAttribute(Tag.Keyword, IppAttributeNames.PdfVersionsSupported, "adobe-1.7"), new IppAttribute(Tag.Keyword, IppAttributeNames.PdfVersionsSupported, "iso-32000-2_2017")] },
            { IppAttributeNames.IppFeaturesSupported, [new IppAttribute(Tag.Keyword, IppAttributeNames.IppFeaturesSupported, "subscription"), new IppAttribute(Tag.Keyword, IppAttributeNames.IppFeaturesSupported, "events"), new IppAttribute(Tag.Keyword, IppAttributeNames.IppFeaturesSupported, "faxout")] },
            { IppAttributeNames.PrinterServiceType, [new IppAttribute(Tag.Keyword, IppAttributeNames.PrinterServiceType, "print"), new IppAttribute(Tag.Keyword, IppAttributeNames.PrinterServiceType, "fax") ] },
            { IppAttributeNames.CompressionDefault, [new IppAttribute(Tag.Keyword, IppAttributeNames.CompressionDefault, Compression.Deflate.ToString())] },
            { IppAttributeNames.PrinterResourceIds, [new IppAttribute(Tag.Integer, IppAttributeNames.PrinterResourceIds, 10), new IppAttribute(Tag.Integer, IppAttributeNames.PrinterResourceIds, 20)] }
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
            { IppAttributeNames.PrinterStateChangeTime, [new IppAttribute(Tag.Integer, IppAttributeNames.PrinterStateChangeTime, 42)] },
            { IppAttributeNames.PrinterStateChangeDateTime, [new IppAttribute(Tag.DateTime, IppAttributeNames.PrinterStateChangeDateTime, stateChange)] },
            { IppAttributeNames.PrinterConfigChangeTime, [new IppAttribute(Tag.Integer, IppAttributeNames.PrinterConfigChangeTime, 84)] },
            { IppAttributeNames.PrinterConfigChangeDateTime, [new IppAttribute(Tag.DateTime, IppAttributeNames.PrinterConfigChangeDateTime, configChange)] },
            { IppAttributeNames.PrinterAlert, [new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.PrinterAlert, "code=jam;severity=critical")] },
            { IppAttributeNames.PrinterAlertDescription, [new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.PrinterAlertDescription, "alert text")] },
            // PrinterSupply read mapping deferred to Task 7 (now PrinterSupply[]? collection type)
            { IppAttributeNames.PrinterSupplyDescription, [new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.PrinterSupplyDescription, "toner status")] }
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
        // PrinterSupply read mapping deferred to Task 7
        dst.PrinterSupply.Should().BeNull();
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
            PrinterCameraImageUri = [new Uri("https://printer.example/camera")]
        };

        var dst = _mapper.Map<PrinterDescriptionAttributes, IDictionary<string, IppAttribute[]>>(src);

        dst.Should().ContainKey(IppAttributeNames.AccuracyUnitsSupported);
        dst[IppAttributeNames.AccuracyUnitsSupported].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Keyword);
        dst[IppAttributeNames.AccuracyUnitsSupported].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["mm", "um"]);

        dst.Should().ContainKey(IppAttributeNames.PrinterCameraImageUri);
        dst[IppAttributeNames.PrinterCameraImageUri].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Uri);
        dst[IppAttributeNames.PrinterCameraImageUri].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["https://printer.example/camera"]);

        dst.Should().ContainKey(IppAttributeNames.PlatformShape);
        dst[IppAttributeNames.PlatformShape].Single().Tag.Should().Be(Tag.Keyword);
        dst[IppAttributeNames.PlatformShape].Single().Value.Should().Be("rectangular");

        dst.Should().ContainKey(IppAttributeNames.PrintBaseDefault);
        dst[IppAttributeNames.PrintBaseDefault].Single().Tag.Should().Be(Tag.Keyword);
        dst[IppAttributeNames.PrintBaseDefault].Single().Value.Should().Be("raft");

        dst.Should().ContainKey(IppAttributeNames.PrintSupportsSupported);
        dst[IppAttributeNames.PrintSupportsSupported].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["none", "standard"]);

        dst.Should().ContainKey(IppAttributeNames.PrinterVolumeSupported);
        dst[IppAttributeNames.PrinterVolumeSupported].Any(x => x.Tag == Tag.BegCollection).Should().BeTrue();
        dst[IppAttributeNames.PrinterVolumeSupported].Any(x => x.Tag == Tag.MemberAttrName && Equals(x.Value, "x-dimension")).Should().BeTrue();
        dst[IppAttributeNames.PrinterVolumeSupported].Any(x => x.Tag == Tag.MemberAttrName && Equals(x.Value, "y-dimension")).Should().BeTrue();
        dst[IppAttributeNames.PrinterVolumeSupported].Any(x => x.Tag == Tag.MemberAttrName && Equals(x.Value, "z-dimension")).Should().BeTrue();
        dst[IppAttributeNames.PrinterVolumeSupported].Count(x => x.Tag == Tag.Integer).Should().Be(3);
    }

    [TestMethod]
    public void Map_PrinterDescriptionAttributes_ToDictionary_MapsPrinterRequestedJobAttributes()
    {
        var src = new PrinterDescriptionAttributes
        {
            PrinterRequestedJobAttributes = [(PrinterRequestedJobAttribute)"job-account-id", (PrinterRequestedJobAttribute)"job-account-type"]
        };

        var dst = _mapper.Map<PrinterDescriptionAttributes, IDictionary<string, IppAttribute[]>>(src);

        dst.Should().ContainKey(IppAttributeNames.PrinterRequestedJobAttributes);
        dst[IppAttributeNames.PrinterRequestedJobAttributes].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Keyword);
        dst[IppAttributeNames.PrinterRequestedJobAttributes].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["job-account-id", "job-account-type"]);
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

        dst.Should().ContainKey(IppAttributeNames.PdfVersionsSupported);
        dst[IppAttributeNames.PdfVersionsSupported].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Keyword);
        dst[IppAttributeNames.PdfVersionsSupported].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["adobe-1.7", "iso-32000-2_2017"]);

        dst.Should().ContainKey(IppAttributeNames.IppFeaturesSupported);
        dst[IppAttributeNames.IppFeaturesSupported].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Keyword);
        dst[IppAttributeNames.IppFeaturesSupported].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["subscription", "events", "faxout"]);

        dst.Should().ContainKey(IppAttributeNames.PrinterServiceType);
        dst[IppAttributeNames.PrinterServiceType].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Keyword);
        dst[IppAttributeNames.PrinterServiceType].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["print", "fax"]);

        dst.Should().ContainKey(IppAttributeNames.CompressionDefault);
        dst[IppAttributeNames.CompressionDefault].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Keyword);
        dst[IppAttributeNames.CompressionDefault].Select(x => x.Value?.ToString()).Should().BeEquivalentTo([Compression.Deflate.ToString()]);

        dst.Should().ContainKey(IppAttributeNames.PrinterResourceIds);
        dst[IppAttributeNames.PrinterResourceIds].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Integer);
        dst[IppAttributeNames.PrinterResourceIds].Select(x => (int)x.Value!).Should().BeEquivalentTo(new[] { 10, 20 });
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
            // PrinterSupply is now PrinterSupply[]? — write mapping deferred to Task 7
            PrinterSupplyDescription = ["toner status"],
            NaturalLanguageConfigured = "en-us"
        };

        var dst = _mapper.Map<PrinterDescriptionAttributes, IDictionary<string, IppAttribute[]>>(src);

        dst.Should().ContainKey(IppAttributeNames.PrinterStateChangeTime);
        dst[IppAttributeNames.PrinterStateChangeTime].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Integer);
        dst[IppAttributeNames.PrinterStateChangeTime].Select(x => (int)x.Value!).Should().BeEquivalentTo(new[] { 42 });

        dst.Should().ContainKey(IppAttributeNames.PrinterStateChangeDateTime);
        dst[IppAttributeNames.PrinterStateChangeDateTime].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.DateTime);
        dst[IppAttributeNames.PrinterStateChangeDateTime].Select(x => (DateTimeOffset)x.Value!).Should().BeEquivalentTo(new[] { stateChange });

        dst.Should().ContainKey(IppAttributeNames.PrinterConfigChangeTime);
        dst[IppAttributeNames.PrinterConfigChangeTime].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Integer);
        dst[IppAttributeNames.PrinterConfigChangeTime].Select(x => (int)x.Value!).Should().BeEquivalentTo(new[] { 84 });

        dst.Should().ContainKey(IppAttributeNames.PrinterConfigChangeDateTime);
        dst[IppAttributeNames.PrinterConfigChangeDateTime].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.DateTime);
        dst[IppAttributeNames.PrinterConfigChangeDateTime].Select(x => (DateTimeOffset)x.Value!).Should().BeEquivalentTo(new[] { configChange });

        dst.Should().ContainKey(IppAttributeNames.PrinterAlert);
        dst[IppAttributeNames.PrinterAlert].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.OctetStringWithAnUnspecifiedFormat);
        dst[IppAttributeNames.PrinterAlert].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["code=jam;severity=critical"]);

        dst.Should().ContainKey(IppAttributeNames.PrinterAlertDescription);
        dst[IppAttributeNames.PrinterAlertDescription].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.TextWithoutLanguage);
        dst[IppAttributeNames.PrinterAlertDescription].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["alert text"]);

        // PrinterSupply write mapping deferred to Task 7 (now PrinterSupply[]? collection type)
        dst.Should().NotContainKey(IppAttributeNames.PrinterSupply);

        dst.Should().ContainKey(IppAttributeNames.PrinterSupplyDescription);
        dst[IppAttributeNames.PrinterSupplyDescription].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.TextWithoutLanguage);
        dst[IppAttributeNames.PrinterSupplyDescription].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["toner status"]);

        dst.Should().ContainKey(IppAttributeNames.NaturalLanguageConfigured);
        dst[IppAttributeNames.NaturalLanguageConfigured].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.NaturalLanguage);
        dst[IppAttributeNames.NaturalLanguageConfigured].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["en-us"]);
    }

    [TestMethod]
    public void Map_PrinterDescriptionAttributes_FromDictionary_MapsPwg51017ScanAttributes()
    {
        var src = new Dictionary<string, IppAttribute[]>
        {
            {
                IppAttributeNames.DestinationAccessesSupported,
                [
                    new IppAttribute(Tag.Keyword, IppAttributeNames.DestinationAccessesSupported, "access-user-name"),
                    new IppAttribute(Tag.Keyword, IppAttributeNames.DestinationAccessesSupported, "access-password")
                ]
            },
            {
                IppAttributeNames.JobDestinationSpoolingSupported,
                [new IppAttribute(Tag.Keyword, IppAttributeNames.JobDestinationSpoolingSupported, "automatic")]
            },
            {
                IppAttributeNames.OutputAttributesDefault,
                [
                    new IppAttribute(Tag.BegCollection, IppAttributeNames.OutputAttributesDefault, NoValue.Instance),
                    new IppAttribute(Tag.MemberAttrName, "", "noise-removal"),
                    new IppAttribute(Tag.Integer, "", 50),
                    new IppAttribute(Tag.MemberAttrName, "", "output-compression-quality-factor"),
                    new IppAttribute(Tag.Integer, "", 70),
                    new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
                ]
            },
            {
                IppAttributeNames.OutputAttributesSupported,
                [
                    new IppAttribute(Tag.Keyword, IppAttributeNames.OutputAttributesSupported, "noise-removal"),
                    new IppAttribute(Tag.Keyword, IppAttributeNames.OutputAttributesSupported, "output-compression-quality-factor")
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

        dst.Should().ContainKey(IppAttributeNames.DestinationAccessesSupported);
        dst[IppAttributeNames.DestinationAccessesSupported].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Keyword);
        dst[IppAttributeNames.DestinationAccessesSupported].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["access-user-name", "access-password"]);

        dst.Should().ContainKey(IppAttributeNames.JobDestinationSpoolingSupported);
        dst[IppAttributeNames.JobDestinationSpoolingSupported].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Keyword);
        dst[IppAttributeNames.JobDestinationSpoolingSupported].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["automatic"]);

        dst.Should().ContainKey(IppAttributeNames.OutputAttributesDefault);
        dst[IppAttributeNames.OutputAttributesDefault].Should().Contain(x => x.Tag == Tag.MemberAttrName && x.Value!.Equals("noise-removal"));
        dst[IppAttributeNames.OutputAttributesDefault].Should().Contain(x => x.Tag == Tag.MemberAttrName && x.Value!.Equals("output-compression-quality-factor"));
        dst[IppAttributeNames.OutputAttributesDefault].Should().Contain(x => x.Tag == Tag.Integer && x.Value!.Equals(50));
        dst[IppAttributeNames.OutputAttributesDefault].Should().Contain(x => x.Tag == Tag.Integer && x.Value!.Equals(70));

        dst.Should().ContainKey(IppAttributeNames.OutputAttributesSupported);
        dst[IppAttributeNames.OutputAttributesSupported].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Keyword);
        dst[IppAttributeNames.OutputAttributesSupported].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["noise-removal", "output-compression-quality-factor"]);
    }

    [TestMethod]
    public void Map_PrinterDescriptionAttributes_FromDictionary_MapsDestinationUriReady()
    {
        var src = new Dictionary<string, IppAttribute[]>
        {
            {
                IppAttributeNames.DestinationUriReady,
                [
                    new IppAttribute(Tag.BegCollection, IppAttributeNames.DestinationUriReady, NoValue.Instance),
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
        dst.DestinationUriReady[0].DestinationUri.Should().Be(new Uri("https://example.test/upload"));
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
                    DestinationUri = new Uri("https://example.test/upload"),
                    DestinationName = "Inbox",
                    DestinationIsDirectory = true,
                    DestinationMandatoryAccessAttributes = ["access-user-name", "access-password"]
                }
            ]
        };

        var dst = _mapper.Map<PrinterDescriptionAttributes, IDictionary<string, IppAttribute[]>>(src);

        dst.Should().ContainKey(IppAttributeNames.DestinationUriReady);
        dst[IppAttributeNames.DestinationUriReady].Should().Contain(x => x.Tag == Tag.BegCollection && x.Name == IppAttributeNames.DestinationUriReady);
        dst[IppAttributeNames.DestinationUriReady].Should().Contain(x => x.Tag == Tag.MemberAttrName && x.Value!.Equals("destination-uri"));
        dst[IppAttributeNames.DestinationUriReady].Should().Contain(x => x.Tag == Tag.Uri && x.Value!.Equals("https://example.test/upload"));
        dst[IppAttributeNames.DestinationUriReady].Should().Contain(x => x.Tag == Tag.MemberAttrName && x.Value!.Equals("destination-name"));
        dst[IppAttributeNames.DestinationUriReady].Should().Contain(x => x.Tag == Tag.NameWithoutLanguage && x.Value!.Equals("Inbox"));
        dst[IppAttributeNames.DestinationUriReady].Should().Contain(x => x.Tag == Tag.MemberAttrName && x.Value!.Equals("destination-is-directory"));
        dst[IppAttributeNames.DestinationUriReady].Should().Contain(x => x.Tag == Tag.Boolean && x.Value!.Equals(true));
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

        dst.Should().ContainKey(IppAttributeNames.PrinterAlert);
        dst[IppAttributeNames.PrinterAlert].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.OctetStringWithAnUnspecifiedFormat);
        dst[IppAttributeNames.PrinterAlert].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(["code=jam;index=22;severity=critical;group=mediaPath;groupindex=4;location=6"]);
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

        serialized.Should().ContainKey(IppAttributeNames.OutputBinDefault);
        serialized[IppAttributeNames.OutputBinDefault].Single().Tag.Should().Be(Tag.Keyword);
        serialized[IppAttributeNames.OutputBinDefault].Single().Value.Should().Be("stacker-3");

        serialized.Should().ContainKey(IppAttributeNames.OutputBinSupported);
        serialized[IppAttributeNames.OutputBinSupported].Select(x => x.Tag).Should().BeEquivalentTo(
            [Tag.Keyword, Tag.Keyword, Tag.Keyword, Tag.NameWithoutLanguage],
            options => options.WithStrictOrdering());
        serialized[IppAttributeNames.OutputBinSupported].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(
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

        serialized.Should().ContainKey(IppAttributeNames.OutputBinDefault);
        serialized[IppAttributeNames.OutputBinDefault].Single().Tag.Should().Be(Tag.NameWithoutLanguage);
        serialized[IppAttributeNames.OutputBinDefault].Single().Value.Should().Be("custom-finisher-bin");

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
        serialized.Should().ContainKey(IppAttributeNames.OverridesSupported);
        serialized[IppAttributeNames.OverridesSupported].Select(x => x.Tag).Should().OnlyContain(x => x == Tag.Keyword);
        serialized[IppAttributeNames.OverridesSupported].Select(x => x.Value?.ToString()).Should().BeEquivalentTo(
            ["pages", "document-numbers", "document-copies", "sides", "vendor-custom-override"]);

        var roundTripped = _mapper.Map<IDictionary<string, IppAttribute[]>, PrinterDescriptionAttributes>(serialized);
        roundTripped.OverridesSupported.Should().BeEquivalentTo(src.OverridesSupported);
    }
}
