using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using SharpIpp.Models.Requests;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class CollectionProfilesTests
{
    private readonly IMapper _mapper;

    public CollectionProfilesTests()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);
        _mapper = mapper;
    }

    [TestMethod]
    [DataRow(true, true, true, DisplayName = "All present")]
    [DataRow(true, false, false, DisplayName = "Only CoverType")]
    [DataRow(false, true, false, DisplayName = "Only Media")]
    [DataRow(false, false, true, DisplayName = "Only MediaCol")]
    [DataRow(false, false, false, DisplayName = "None present")]
    public void Map_Dictionary_To_Cover_Coverage(bool includeCoverType, bool includeMedia, bool includeMediaCol)
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>();
        if (includeCoverType)
            dict.Add("cover-type", new[] { new IppAttribute(Tag.Keyword, "cover-type", "print-both") });
        if (includeMedia)
            dict.Add("media", new[] { new IppAttribute(Tag.Keyword, "media", "iso_a4_210x297mm") });
        if (includeMediaCol)
            dict.Add("media-col", new[] { new IppAttribute(Tag.BegCollection, "media-col", NoValue.Instance), new IppAttribute(Tag.MemberAttrName, "", "media-color"), new IppAttribute(Tag.Keyword, "", "blue"), new IppAttribute(Tag.EndCollection, "", NoValue.Instance) });

        // Act
        var result = _mapper.Map<Cover>(dict);

        // Assert
        if (includeCoverType) result.CoverType.Should().Be(CoverType.PrintBoth); else result.CoverType.Should().BeNull();
        if (includeMedia) result.Media.Should().Be((Media)"iso_a4_210x297mm"); else result.Media.Should().BeNull();
        if (includeMediaCol) result.MediaCol.Should().NotBeNull(); else result.MediaCol.Should().BeNull();
    }

    [TestMethod]
    [DataRow(true, true, true, DisplayName = "All present")]
    [DataRow(true, false, false, DisplayName = "Only CoverType")]
    [DataRow(false, true, false, DisplayName = "Only Media")]
    [DataRow(false, false, true, DisplayName = "Only MediaCol")]
    [DataRow(false, false, false, DisplayName = "None present")]
    public void Map_Cover_To_Attributes_Coverage(bool hasCoverType, bool hasMedia, bool hasMediaCol)
    {
        // Arrange
        var cover = new Cover
        {
            CoverType = hasCoverType ? CoverType.PrintBoth : null,
            Media = hasMedia ? (Media)"iso_a4_210x297mm" : null,
            MediaCol = hasMediaCol ? new MediaCol { MediaColor = (MediaColor)"blue" } : null
        };

        // Act
        var result = _mapper.Map<IEnumerable<IppAttribute>>(cover).ToList();

        // Assert
        if (hasCoverType) result.Should().Contain(a => a.Name == "cover-type"); else result.Should().NotContain(a => a.Name == "cover-type");
        if (hasMedia) result.Should().Contain(a => a.Name == "media"); else result.Should().NotContain(a => a.Name == "media");
        if (hasMediaCol) result.Should().Contain(a => a.Name == "media-col"); else result.Should().NotContain(a => a.Name == "media-col");
    }

    [TestMethod]
    [DataRow(true, true, true, true, DisplayName = "All present")]
    [DataRow(true, false, false, false, DisplayName = "Only InsertAfterPageNumber")]
    [DataRow(false, true, false, false, DisplayName = "Only InsertCount")]
    [DataRow(false, false, true, false, DisplayName = "Only Media")]
    [DataRow(false, false, false, true, DisplayName = "Only MediaCol")]
    [DataRow(false, false, false, false, DisplayName = "None present")]
    public void Map_Dictionary_To_InsertSheet_Coverage(bool h1, bool h2, bool h3, bool h4)
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>();
        if (h1) dict.Add("insert-after-page-number", new[] { new IppAttribute(Tag.Integer, "insert-after-page-number", 1) });
        if (h2) dict.Add("insert-count", new[] { new IppAttribute(Tag.Integer, "insert-count", 2) });
        if (h3) dict.Add("media", new[] { new IppAttribute(Tag.Keyword, "media", "iso_a4_210x297mm") });
        if (h4) dict.Add("media-col", new[] { new IppAttribute(Tag.BegCollection, "media-col", NoValue.Instance), new IppAttribute(Tag.MemberAttrName, "", "media-color"), new IppAttribute(Tag.Keyword, "", "blue"), new IppAttribute(Tag.EndCollection, "", NoValue.Instance) });

        // Act
        var result = _mapper.Map<InsertSheet>(dict);

        // Assert
        if (h1) result.InsertAfterPageNumber.Should().Be(1); else result.InsertAfterPageNumber.Should().BeNull();
        if (h2) result.InsertCount.Should().Be(2); else result.InsertCount.Should().BeNull();
        if (h3) result.Media.Should().Be((Media)"iso_a4_210x297mm"); else result.Media.Should().BeNull();
        if (h4) result.MediaCol.Should().NotBeNull(); else result.MediaCol.Should().BeNull();
    }

    [TestMethod]
    [DataRow(true, true, true, true, DisplayName = "All present")]
    [DataRow(false, false, false, false, DisplayName = "None present")]
    public void Map_InsertSheet_To_Attributes_Coverage(bool h1, bool h2, bool h3, bool h4)
    {
        // Arrange
        var sheet = new InsertSheet
        {
            InsertAfterPageNumber = h1 ? 1 : null,
            InsertCount = h2 ? 2 : null,
            Media = h3 ? (Media)"iso_a4_210x297mm" : null,
            MediaCol = h4 ? new MediaCol { MediaColor = (MediaColor)"blue" } : null
        };

        // Act
        var result = _mapper.Map<IEnumerable<IppAttribute>>(sheet).ToList();

        // Assert
        if (h1) result.Should().Contain(a => a.Name == "insert-after-page-number"); else result.Should().NotContain(a => a.Name == "insert-after-page-number");
        if (h2) result.Should().Contain(a => a.Name == "insert-count"); else result.Should().NotContain(a => a.Name == "insert-count");
        if (h3) result.Should().Contain(a => a.Name == "media"); else result.Should().NotContain(a => a.Name == "media");
        if (h4) result.Should().Contain(a => a.Name == "media-col"); else result.Should().NotContain(a => a.Name == "media-col");
    }

    [TestMethod]
    [DataRow(true, true, true, true, DisplayName = "All present")]
    [DataRow(false, false, false, false, DisplayName = "None present")]
    public void Map_Dictionary_To_JobAccountingSheets_Coverage(bool h1, bool h2, bool h3, bool h4)
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>();
        if (h1) dict.Add("job-accounting-output-bin", new[] { new IppAttribute(Tag.Keyword, "job-accounting-output-bin", "top") });
        if (h2) dict.Add("job-accounting-sheets-type", new[] { new IppAttribute(Tag.Keyword, "job-accounting-sheets-type", "standard") });
        if (h3) dict.Add("media", new[] { new IppAttribute(Tag.Keyword, "media", "iso_a4_210x297mm") });
        if (h4) dict.Add("media-col", new[] { new IppAttribute(Tag.BegCollection, "media-col", NoValue.Instance), new IppAttribute(Tag.MemberAttrName, "", "media-color"), new IppAttribute(Tag.Keyword, "", "blue"), new IppAttribute(Tag.EndCollection, "", NoValue.Instance) });

        // Act
        var result = _mapper.Map<JobAccountingSheets>(dict);

        // Assert
        if (h1) result.JobAccountingOutputBin.Should().Be((OutputBin)"top"); else result.JobAccountingOutputBin.Should().BeNull();
        if (h2) result.JobAccountingSheetsType.Should().Be(JobSheetsType.Standard); else result.JobAccountingSheetsType.Should().BeNull();
        if (h3) result.Media.Should().Be((Media)"iso_a4_210x297mm"); else result.Media.Should().BeNull();
        if (h4) result.MediaCol.Should().NotBeNull(); else result.MediaCol.Should().BeNull();
    }

    [TestMethod]
    [DataRow(true, true, true, true, DisplayName = "All present")]
    [DataRow(false, false, false, false, DisplayName = "None present")]
    public void Map_JobAccountingSheets_To_Attributes_Coverage(bool h1, bool h2, bool h3, bool h4)
    {
        // Arrange
        var sheets = new JobAccountingSheets
        {
            JobAccountingOutputBin = h1 ? (OutputBin)"top" : null,
            JobAccountingSheetsType = h2 ? JobSheetsType.Standard : null,
            Media = h3 ? (Media)"iso_a4_210x297mm" : null,
            MediaCol = h4 ? new MediaCol { MediaColor = (MediaColor)"blue" } : null
        };

        // Act
        var result = _mapper.Map<IEnumerable<IppAttribute>>(sheets).ToList();

        // Assert
        if (h1) result.Should().Contain(a => a.Name == "job-accounting-output-bin"); else result.Should().NotContain(a => a.Name == "job-accounting-output-bin");
        if (h2) result.Should().Contain(a => a.Name == "job-accounting-sheets-type"); else result.Should().NotContain(a => a.Name == "job-accounting-sheets-type");
        if (h3) result.Should().Contain(a => a.Name == "media"); else result.Should().NotContain(a => a.Name == "media");
        if (h4) result.Should().Contain(a => a.Name == "media-col"); else result.Should().NotContain(a => a.Name == "media-col");
    }

    [TestMethod]
    [DataRow(true, true, true, true, DisplayName = "All present")]
    [DataRow(false, false, false, false, DisplayName = "None present")]
    public void Map_Dictionary_To_JobErrorSheet_Coverage(bool h1, bool h2, bool h3, bool h4)
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>();
        if (h1) dict.Add("job-error-sheet-type", new[] { new IppAttribute(Tag.Keyword, "job-error-sheet-type", "standard") });
        if (h2) dict.Add("job-error-sheet-when", new[] { new IppAttribute(Tag.Keyword, "job-error-sheet-when", "on-error") });
        if (h3) dict.Add("media", new[] { new IppAttribute(Tag.Keyword, "media", "iso_a4_210x297mm") });
        if (h4) dict.Add("media-col", new[] { new IppAttribute(Tag.BegCollection, "media-col", NoValue.Instance), new IppAttribute(Tag.MemberAttrName, "", "media-color"), new IppAttribute(Tag.Keyword, "", "blue"), new IppAttribute(Tag.EndCollection, "", NoValue.Instance) });

        // Act
        var result = _mapper.Map<JobErrorSheet>(dict);

        // Assert
        if (h1) result.JobErrorSheetType.Should().Be(JobSheetsType.Standard); else result.JobErrorSheetType.Should().BeNull();
        if (h2) result.JobErrorSheetWhen.Should().Be(JobErrorSheetWhen.OnError); else result.JobErrorSheetWhen.Should().BeNull();
        if (h3) result.Media.Should().Be((Media)"iso_a4_210x297mm"); else result.Media.Should().BeNull();
        if (h4) result.MediaCol.Should().NotBeNull(); else result.MediaCol.Should().BeNull();
    }

    [TestMethod]
    [DataRow(true, true, true, true, DisplayName = "All present")]
    [DataRow(false, false, false, false, DisplayName = "None present")]
    public void Map_JobErrorSheet_To_Attributes_Coverage(bool h1, bool h2, bool h3, bool h4)
    {
        // Arrange
        var sheet = new JobErrorSheet
        {
            JobErrorSheetType = h1 ? JobSheetsType.Standard : null,
            JobErrorSheetWhen = h2 ? JobErrorSheetWhen.OnError : null,
            Media = h3 ? (Media)"iso_a4_210x297mm" : null,
            MediaCol = h4 ? new MediaCol { MediaColor = (MediaColor)"blue" } : null
        };

        // Act
        var result = _mapper.Map<IEnumerable<IppAttribute>>(sheet).ToList();

        // Assert
        if (h1) result.Should().Contain(a => a.Name == "job-error-sheet-type"); else result.Should().NotContain(a => a.Name == "job-error-sheet-type");
        if (h2) result.Should().Contain(a => a.Name == "job-error-sheet-when"); else result.Should().NotContain(a => a.Name == "job-error-sheet-when");
        if (h3) result.Should().Contain(a => a.Name == "media"); else result.Should().NotContain(a => a.Name == "media");
        if (h4) result.Should().Contain(a => a.Name == "media-col"); else result.Should().NotContain(a => a.Name == "media-col");
    }

    [TestMethod]
    public void Map_Dictionary_To_JobCounter_Coverage()
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "blank", [new IppAttribute(Tag.Integer, "blank", 1)] },
            { "full-color", [new IppAttribute(Tag.Integer, "full-color", 2)] },
            { "monochrome-two-sided", [new IppAttribute(Tag.Integer, "monochrome-two-sided", 3)] },
        };

        // Act
        var result = _mapper.Map<JobCounter>(dict);

        // Assert
        result.Blank.Should().Be(1);
        result.FullColor.Should().Be(2);
        result.MonochromeTwoSided.Should().Be(3);
        result.HighlightColor.Should().BeNull();
    }

    [TestMethod]
    public void Map_Dictionary_To_SystemConfiguredPrinter_ShouldMapAllFields()
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "printer-id", [new IppAttribute(Tag.Integer, "printer-id", 42)] },
            { "printer-info", [new IppAttribute(Tag.TextWithoutLanguage, "printer-info", "Test Printer")] },
            { "printer-is-accepting-jobs", [new IppAttribute(Tag.Boolean, "printer-is-accepting-jobs", true)] },
            { "printer-name", [new IppAttribute(Tag.NameWithoutLanguage, "printer-name", "Printer ABC")] },
            { "printer-service-type", [new IppAttribute(Tag.Keyword, "printer-service-type", "print") ] },
            { "printer-state", [new IppAttribute(Tag.Enum, "printer-state", (int)PrinterState.Idle)] },
            { "printer-state-reasons", [new IppAttribute(Tag.Keyword, "printer-state-reasons", "none")] },
            { "printer-xri-supported", [new IppAttribute(Tag.BegCollection, "printer-xri-supported", NoValue.Instance), new IppAttribute(Tag.MemberAttrName, "", "xri-uri"), new IppAttribute(Tag.Uri, "", "ipp://test"), new IppAttribute(Tag.EndCollection, "", NoValue.Instance)] }
        };

        // Act
        var result = _mapper.Map<SystemConfiguredPrinter>(dict);

        // Assert
        result.PrinterId.Should().Be(42);
        result.PrinterInfo.Should().Be("Test Printer");
        result.PrinterIsAcceptingJobs.Should().BeTrue();
        result.PrinterName.Should().Be("Printer ABC");
        result.PrinterServiceType.Should().Be(PrinterServiceType.Print);
        result.PrinterState.Should().Be(PrinterState.Idle);
        result.PrinterStateReasons.Should().Contain("none");
        result.PrinterXriSupported.Should().NotBeNull();
    }

    [TestMethod]
    public void Map_Dictionary_To_DestinationUri_UsesIntegerT33Subaddress()
    {
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "destination-uri", [new IppAttribute(Tag.Uri, "destination-uri", "fax:+12025550123")] },
            { "t33-subaddress", [new IppAttribute(Tag.Integer, "t33-subaddress", 99)] }
        };

        var result = _mapper.Map<DestinationUri>(dict);

        result.DestinationUriValue.Should().Be("fax:+12025550123");
        result.T33Subaddress.Should().Be(99);
    }

    [TestMethod]
    public void Map_DestinationStatus_To_Attributes_WritesTransmissionStatusEnum()
    {
        var status = new DestinationStatus
        {
            DestinationUri = "fax:+12025550123",
            ImagesCompleted = 2,
            TransmissionStatus = TransmissionStatus.Processing
        };

        var attrs = _mapper.Map<IEnumerable<IppAttribute>>(status).ToList();

        attrs.Should().Contain(x => x.Name == "destination-uri" && x.Tag == Tag.Uri);
        attrs.Should().Contain(x => x.Name == "images-completed" && x.Tag == Tag.Integer && (int)x.Value! == 2);
        attrs.Should().Contain(x => x.Name == "transmission-status" && x.Tag == Tag.Enum && (int)x.Value! == (int)TransmissionStatus.Processing);
    }

    [TestMethod]
    public void Map_SystemConfiguredPrinter_To_Attributes_ShouldContainCollectionName()
    {
        // Arrange
        var src = new SystemConfiguredPrinter
        {
            PrinterId = 42,
            PrinterInfo = "Test Printer",
            PrinterIsAcceptingJobs = true,
            PrinterName = "Printer ABC",
            PrinterServiceType = PrinterServiceType.Print,
            PrinterState = PrinterState.Idle,
            PrinterStateReasons = new[] { "none" },
            PrinterXriSupported = new[] { new SystemXri { XriUri = new Uri("ipp://test") } }
        };

        // Act
        var result = _mapper.Map<IEnumerable<IppAttribute>>(src).ToList();

        // Assert
        result.Should().Contain(a => a.Name == "printer-id");
        result.Should().Contain(a => a.Name == "printer-info");
        result.Should().Contain(a => a.Name == "printer-is-accepting-jobs");
        result.Should().Contain(a => a.Name == "printer-name");
        result.Should().Contain(a => a.Name == "printer-service-type");
        result.Should().Contain(a => a.Name == "printer-state");
        result.Should().Contain(a => a.Name == "printer-state-reasons");
        result.Should().Contain(a => a.Name == "printer-xri-supported");
    }

    [TestMethod]
    public void Map_Dictionary_To_PowerLogEntry_ShouldMapPowerStateDateTime()
    {
        // Arrange
        var dateTime = new DateTimeOffset(2026, 3, 29, 9, 0, 0, TimeSpan.Zero);
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "log-id", [new IppAttribute(Tag.Integer, "log-id", 301)] },
            { "power-state", [new IppAttribute(Tag.Keyword, "power-state", PowerState.On.Value)] },
            { "power-state-date-time", [new IppAttribute(Tag.DateTime, "power-state-date-time", dateTime)] },
            { "power-state-message", [new IppAttribute(Tag.TextWithoutLanguage, "power-state-message", "power-log-entry")] }
        };

        // Act
        var result = _mapper.Map<PowerLogEntry>(dict);

        // Assert
        result.LogId.Should().Be(301);
        result.PowerState.Should().Be(PowerState.On);
        result.PowerStateDateTime.Should().Be(dateTime);
        result.PowerStateMessage.Should().Be("power-log-entry");
    }

    [TestMethod]
    public void Map_PowerLogEntry_To_Attributes_ShouldEmitPowerStateDateTime()
    {
        // Arrange
        var dateTime = new DateTimeOffset(2026, 3, 29, 9, 0, 0, TimeSpan.Zero);
        var src = new PowerLogEntry
        {
            LogId = 301,
            PowerState = PowerState.On,
            PowerStateDateTime = dateTime,
            PowerStateMessage = "power-log-entry"
        };

        // Act
        var result = _mapper.Map<IEnumerable<IppAttribute>>(src).ToList();

        // Assert
        result.Should().Contain(a => a.Name == "power-state-date-time" && a.Tag == Tag.DateTime && Equals(a.Value, dateTime));
        result.Should().NotContain(a => a.Name == "date-time-at");
    }

    [TestMethod]
    public void Map_Dictionary_To_PowerLogEntry_WithLegacyDateTimeAt_ShouldIgnoreLegacyField()
    {
        // Arrange
        var dateTime = new DateTimeOffset(2026, 3, 29, 9, 0, 0, TimeSpan.Zero);
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "log-id", [new IppAttribute(Tag.Integer, "log-id", 301)] },
            { "power-state", [new IppAttribute(Tag.Keyword, "power-state", PowerState.On.Value)] },
            { "date-time-at", [new IppAttribute(Tag.DateTime, "date-time-at", dateTime)] },
            { "power-state-message", [new IppAttribute(Tag.TextWithoutLanguage, "power-state-message", "power-log-entry")] }
        };

        // Act
        var result = _mapper.Map<PowerLogEntry>(dict);

        // Assert
        result.PowerStateDateTime.Should().BeNull();
    }

    [TestMethod]
public void Map_Dictionary_To_ResourceStatusAttributes_ShouldMapResourceUuidAndTimes()
        {
            // Arrange
            var dict = new Dictionary<string, IppAttribute[]>
            {
                { "resource-id", [new IppAttribute(Tag.Integer, "resource-id", 55)] },
                { "resource-uuid", [new IppAttribute(Tag.Uri, "resource-uuid", "urn:uuid:123e4567-e89b-12d3-a456-426614174000")] },
                { "time-at-canceled", [new IppAttribute(Tag.Integer, "time-at-canceled", 8)] },
                { "time-at-creation", [new IppAttribute(Tag.Integer, "time-at-creation", 16)] },
                { "time-at-installed", [new IppAttribute(Tag.Integer, "time-at-installed", 24)] }
            };

            // Act
            var result = _mapper.Map<ResourceStatusAttributes>(dict);

            // Assert
            result.ResourceId.Should().Be(55);
            result.ResourceUuid.Should().Be(new Uri("urn:uuid:123e4567-e89b-12d3-a456-426614174000"));
            result.TimeAtCanceled.Should().Be(8);
            result.TimeAtCreation.Should().Be(16);
            result.TimeAtInstalled.Should().Be(24);
    }

    [TestMethod]
    public void Map_ResourceStatusAttributes_To_Attributes_ShouldIncludeResourceUuidAndVersion()
    {
        // Arrange
        var src = new ResourceStatusAttributes
        {
            ResourceUuid = new Uri("urn:uuid:123e4567-e89b-12d3-a456-426614174000"),
            ResourceVersion = "1.0.0",
            ResourceStringVersion = "1.0"
        };

        // Act
        var result = _mapper.Map<IDictionary<string, IppAttribute[]>>(src);

        // Assert
        result.Should().ContainKey("resource-uuid");
        result.Should().ContainKey("resource-version");
        result.Should().ContainKey("resource-string-version");
        result["resource-uuid"].Single().Value.Should().Be("urn:uuid:123e4567-e89b-12d3-a456-426614174000");
        result["resource-version"].Single().Value.Should().Be("1.0.0");
        result["resource-string-version"].Single().Value.Should().Be("1.0");
    }

    [TestMethod]
    public void Map_Dictionary_To_CreatePrinterOperationAttributes_ShouldMapPrinterXriRequestedAsSystemXri()
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "system-uri", [new IppAttribute(Tag.Uri, "system-uri", "http://127.0.0.1:631")] },
            { "printer-xri-requested", [
                new IppAttribute(Tag.BegCollection, "printer-xri-requested", NoValue.Instance),
                new IppAttribute(Tag.MemberAttrName, "", "xri-uri"),
                new IppAttribute(Tag.Uri, "", "ipp://example"),
                new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
            ] }
        };

        // Act
        var result = _mapper.Map<CreatePrinterOperationAttributes>(dict);

        // Assert
        result.PrinterXriRequested.Should().NotBeNull();
        result.PrinterXriRequested.Should().ContainSingle();
        result.PrinterXriRequested![0].XriUri.Should().Be(new Uri("ipp://example"));
    }

    [TestMethod]
    public void Map_CreatePrinterOperationAttributes_To_Attributes_ShouldEmitPrinterXriRequestedCollection()
    {
        // Arrange
        var src = new CreatePrinterOperationAttributes
        {
            SystemUri = new Uri("http://127.0.0.1:631"),
            PrinterXriRequested = new[] { new SystemXri { XriUri = new Uri("ipp://example") } }
        };

        // Act
        var attrs = _mapper.Map<List<IppAttribute>>(src);

        // Assert
        attrs.Should().Contain(a => a.Name == "printer-xri-requested");
        attrs.Should().Contain(a => a.Tag == Tag.BegCollection && a.Name == "printer-xri-requested");
    }

    [TestMethod]
    public void Map_SystemOperationAttributes_WithNotifyFields_To_Attributes_ShouldEmitNotifyAttributes()
    {
        // Arrange
        var src = new SystemOperationAttributes
        {
            NotifyPrinterIds = new[] { 1, 2 },
            NotifyResourceId = 42,
            RestartGetInterval = 15,
            WhichPrinters = "all"
        };

        // Act
        var attrs = _mapper.Map<List<IppAttribute>>(src);

        // Assert
        attrs.Should().Contain(a => a.Name == "notify-printer-ids" && a.Tag == Tag.Integer);
        attrs.Should().Contain(a => a.Name == "notify-resource-id" && a.Tag == Tag.Integer);
        attrs.Should().Contain(a => a.Name == "restart-get-interval" && a.Tag == Tag.Integer);
        attrs.Should().Contain(a => a.Name == "which-printers" && a.Tag == Tag.Keyword);
    }

    [TestMethod]
    public void Map_Dictionary_To_JobSheetsCol_Coverage()
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "job-sheets", [new IppAttribute(Tag.Keyword, "job-sheets", "standard")] },
            { "media", [new IppAttribute(Tag.Keyword, "media", "iso_a4_210x297mm")] },
            { "media-col", [
                new IppAttribute(Tag.BegCollection, "media-col", NoValue.Instance),
                new IppAttribute(Tag.MemberAttrName, "", "media-color"),
                new IppAttribute(Tag.Keyword, "", "blue"),
                new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
            ] },
        };

        // Act
        var result = _mapper.Map<JobSheetsCol>(dict);

        // Assert
        result.JobSheets.Should().Be(JobSheets.Standard);
        result.Media.Should().Be((Media)"iso_a4_210x297mm");
        result.MediaCol.Should().NotBeNull();
    }

    [TestMethod]
    public void Map_JobSheetsCol_To_Attributes_Coverage()
    {
        // Arrange
        var sheets = new JobSheetsCol
        {
            JobSheets = JobSheets.Standard,
            Media = (Media)"iso_a4_210x297mm",
            MediaCol = new MediaCol { MediaColor = (MediaColor)"blue" }
        };

        // Act
        var result = _mapper.Map<IEnumerable<IppAttribute>>(sheets).ToList();

        // Assert
        result.Should().Contain(a => a.Name == "job-sheets");
        result.Should().Contain(a => a.Name == "media");
        result.Should().Contain(a => a.Name == "media-col");
    }

    [TestMethod]
    [DataRow(true, true, true, DisplayName = "All present")]
    [DataRow(false, false, false, DisplayName = "None present")]
    public void Map_Dictionary_To_SeparatorSheets_Coverage(bool h1, bool h2, bool h3)
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>();
        if (h1) dict.Add("media", new[] { new IppAttribute(Tag.Keyword, "media", "iso_a4_210x297mm") });
        if (h2) dict.Add("separator-sheets-type", new[] { new IppAttribute(Tag.Keyword, "separator-sheets-type", "slip-sheets") });
        if (h3) dict.Add("media-col", new[] { new IppAttribute(Tag.BegCollection, "media-col", NoValue.Instance), new IppAttribute(Tag.MemberAttrName, "", "media-color"), new IppAttribute(Tag.Keyword, "", "blue"), new IppAttribute(Tag.EndCollection, "", NoValue.Instance) });

        // Act
        var result = _mapper.Map<SeparatorSheets>(dict);

        // Assert
        if (h1) result.Media.Should().Be((Media)"iso_a4_210x297mm"); else result.Media.Should().BeNull();
        if (h2) result.SeparatorSheetsType.Should().Contain(SeparatorSheetsType.SlipSheets); else result.SeparatorSheetsType.Should().BeNull();
        if (h3) result.MediaCol.Should().NotBeNull(); else result.MediaCol.Should().BeNull();
    }

    [TestMethod]
    [DataRow(true, true, true, DisplayName = "All present")]
    [DataRow(false, false, false, DisplayName = "None present")]
    public void Map_SeparatorSheets_To_Attributes_Coverage(bool h1, bool h2, bool h3)
    {
        // Arrange
        var sheets = new SeparatorSheets
        {
            Media = h1 ? (Media)"iso_a4_210x297mm" : null,
            SeparatorSheetsType = h2 ? new[] { SeparatorSheetsType.SlipSheets } : null,
            MediaCol = h3 ? new MediaCol { MediaColor = (MediaColor)"blue" } : null
        };

        // Act
        var result = _mapper.Map<IEnumerable<IppAttribute>>(sheets).ToList();

        // Assert
        if (h1) result.Should().Contain(a => a.Name == "media"); else result.Should().NotContain(a => a.Name == "media");
        if (h2) result.Should().Contain(a => a.Name == "separator-sheets-type"); else result.Should().NotContain(a => a.Name == "separator-sheets-type");
        if (h3) result.Should().Contain(a => a.Name == "media-col"); else result.Should().NotContain(a => a.Name == "media-col");
    }

    [TestMethod]
    public void Map_Dictionary_To_ProofPrint_Should_Map_MediaCol()
    {
        // Arrange
        var mediaCol = new MediaCol { MediaLeftMargin = 5 };
        var mediaColCollection = _mapper.Map<IEnumerable<IppAttribute>>(mediaCol).ToBegCollection("media-col").ToArray();
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "media-col", mediaColCollection }
        };

        // Act
        var result = _mapper.Map<ProofPrint>(dict);

        // Assert
        result.MediaCol.Should().NotBeNull();
        result.MediaCol!.MediaLeftMargin.Should().Be(5);
    }

    [TestMethod]
    public void Map_ProofPrint_To_Attributes_Should_Include_MediaCol()
    {
        // Arrange
        var proofPrint = new ProofPrint
        {
            MediaCol = new MediaCol { MediaBottomMargin = 7 }
        };

        // Act
        var result = _mapper.Map<IEnumerable<IppAttribute>>(proofPrint).ToList();

        // Assert
        result.Should().Contain(a => a.Name == "media-col" && a.Tag == Tag.BegCollection);
        result.Should().Contain(a => a.Tag == Tag.MemberAttrName && a.Value!.Equals("media-bottom-margin"));
        result.Should().Contain(a => a.Tag == Tag.Integer && a.Value!.Equals(7));
    }

    [TestMethod]
    public void Map_CoverSheetInfo_To_Attributes_Should_Include_TextFields()
    {
        // Arrange
        var coverSheetInfo = new CoverSheetInfo
        {
            FromName = "from",
            Logo = "logo",
            Message = "message",
            OrganizationName = "org",
            Subject = "subject",
            ToName = "to"
        };

        // Act
        var result = _mapper.Map<IEnumerable<IppAttribute>>(coverSheetInfo).ToList();

        // Assert
        result.Should().Contain(a => a.Name == "logo" && a.Tag == Tag.TextWithoutLanguage && a.Value!.Equals("logo"));
        result.Should().Contain(a => a.Name == "message" && a.Tag == Tag.TextWithoutLanguage && a.Value!.Equals("message"));
        result.Should().Contain(a => a.Name == "organization-name" && a.Tag == Tag.TextWithoutLanguage && a.Value!.Equals("org"));
    }

    [TestMethod]
    public void Map_OutputAttributes_NoiseRemoval_UsesIntegerSyntax()
    {
        // Arrange
        var outputAttributes = new OutputAttributes
        {
            NoiseRemoval = 80,
            OutputCompressionQualityFactor = 65
        };

        // Act
        var serialized = _mapper.Map<IEnumerable<IppAttribute>>(outputAttributes).ToList();
        var parsed = _mapper.Map<OutputAttributes>(serialized.ToIppDictionary());

        // Assert
        serialized.Should().Contain(a => a.Name == "noise-removal" && a.Tag == Tag.Integer && a.Value!.Equals(80));
        parsed.NoiseRemoval.Should().Be(80);
        parsed.OutputCompressionQualityFactor.Should().Be(65);
    }

    [TestMethod]
    public void Map_DestinationUriReady_RoundTripsWithNestedDestinationAttributes()
    {
        // Arrange
        var destinationUriReady = new DestinationUriReady
        {
            DestinationUri = "https://example.test/upload",
            DestinationName = "Inbox",
            DestinationIsDirectory = true,
            DestinationAttributesSupported = ["job-name"],
            DestinationMandatoryAccessAttributes = ["access-user-name"],
            DestinationAttributes =
            [
                new Dictionary<string, IppAttribute[]>
                {
                    { "job-name", [new IppAttribute(Tag.NameWithoutLanguage, "job-name", "Scanned document")] }
                }
            ]
        };

        // Act
        var serialized = _mapper.Map<IEnumerable<IppAttribute>>(destinationUriReady).ToList();
        var parsed = _mapper.Map<DestinationUriReady>(serialized.ToIppDictionary());

        // Assert
        serialized.Should().Contain(x => x.Tag == Tag.Uri && x.Name == "destination-uri" && x.Value!.Equals("https://example.test/upload"));
        serialized.Should().Contain(x => x.Tag == Tag.BegCollection && x.Name == "destination-attributes");
        parsed.DestinationUri.Should().Be("https://example.test/upload");
        parsed.DestinationName.Should().Be("Inbox");
        parsed.DestinationIsDirectory.Should().BeTrue();
        parsed.DestinationAttributesSupported.Should().BeEquivalentTo("job-name");
        parsed.DestinationMandatoryAccessAttributes.Should().BeEquivalentTo("access-user-name");
        parsed.DestinationAttributes.Should().NotBeNull();
        parsed.DestinationAttributes!.Should().ContainSingle();
        parsed.DestinationAttributes[0].Should().ContainKey("job-name");
    }
}
