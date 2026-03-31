using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Range = SharpIpp.Protocol.Models.Range;

namespace SharpIpp.Tests.Integration;

[TestClass]
[ExcludeFromCodeCoverage]
public class GetPrinterAttributesTests : SharpIppIntegrationTestBase
{
    [TestMethod()]
    public async Task GetPrinterAttributesAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        SharpIppServer server = new();
        GetPrinterAttributesRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
                RequestedAttributes = ["printer-uri", "printer-state", "printer-name"],
                DocumentFormat = "application/pdf",
            },
        };
        IIppRequest? serverRequest = null;
        GetPrinterAttributesResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = await server.ReceiveRequestAsync(s, c);
                serverResponse = new GetPrinterAttributesResponse
                {
                    RequestId = serverRequest.RequestId,
                    Version = serverRequest.Version,
                    StatusCode = IppStatusCode.SuccessfulOk,
                    PrinterAttributes = new()
                    {
                        PrinterUriSupported = new[] { "http://127.0.0.1:631" },
                        UriSecuritySupported = new[] { UriSecurity.None },
                        UriAuthenticationSupported = new[] { UriAuthentication.None },
                        PrinterName = "Test Printer",
                        PrinterLocation = "Office",
                        PrinterInfo = "Test Printer Info",
                        PrinterMoreInfo = "http://127.0.0.1:631",
                        PrinterDriverInstaller = "installer",
                        PrinterMakeAndModel = "SharpIpp Virtual Printer",
                        PrinterMoreInfoManufacturer = "http://manufacturer.com",
                        PrinterState = PrinterState.Idle,
                        PrinterStateReasons = new[] { PrinterStateReason.None },
                        PrinterStateMessage = "Idle",
                        PrinterStateChangeTime = 123,
                        PrinterStateChangeDateTime = new DateTimeOffset(2024, 01, 02, 3, 4, 5, TimeSpan.Zero),
                        PrinterDetailedStatusMessages = new[] { "detailed-1" },
                        MaxClientInfoSupported = 16,
                        PrinterConfigChangeTime = 456,
                        PrinterConfigChangeDateTime = new DateTimeOffset(2024, 01, 02, 3, 4, 6, TimeSpan.Zero),
                        MultipleOperationTimeOut = 60,
                        IppVersionsSupported = new IppVersion[] { new IppVersion(1,1) },
                        OperationsSupported = new[] { IppOperation.PrintJob },
                        MultipleDocumentJobsSupported = true,
                        MultipleDocumentHandlingDefault = MultipleDocumentHandling.SeparateDocumentsUncollatedCopies,
                        MultipleDocumentHandlingSupported = new[] { MultipleDocumentHandling.SeparateDocumentsUncollatedCopies, MultipleDocumentHandling.SingleDocument },
                        CharsetConfigured = "utf-8",
                        CharsetSupported = new[] { "utf-8" },
                        NaturalLanguageConfigured = "en-us",
                        GeneratedNaturalLanguageSupported = new[] { "en-us" },
                        DocumentFormatDefault = "application/pdf",
                        DocumentFormatSupported = new[] { "application/pdf" },
                        PrinterIsAcceptingJobs = true,
                        QueuedJobCount = 0,
                        PrinterMessageFromOperator = "message",
                        ColorSupported = true,
                        ReferenceUriSchemesSupported = new[] { UriScheme.Ftp },
                        PdlOverrideSupported = PdlOverride.Attempted,
                        PrinterUpTime = 100,
                        PrinterCurrentTime = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
                        JpegKOctetsSupported = new Range(1, 8),
                        PdfKOctetsSupported = new Range(1, 16),
                        PagesPerMinute = 10,
                        PagesPerMinuteColor = 10,
                        PrinterResolutionDefault = new Resolution(600, 600, ResolutionUnit.DotsPerInch),
                        PrintQualityDefault = PrintQuality.Normal,
                        PrintColorModeDefault = PrintColorMode.Color,
                        WhichJobsSupported = new[] { WhichJobs.Completed },
                        PrintScalingDefault = PrintScaling.Fit,
                        PrintScalingSupported = new[] { PrintScaling.Fit, PrintScaling.None },
                        MediaDefault = Media.IsoA4210x297mm,
                        MediaSupported = new[] { Media.IsoA4210x297mm },
                        MediaReady = new[] { Media.IsoA4210x297mm },
                        SidesDefault = Sides.OneSided,
                        SidesSupported = new[] { Sides.OneSided, Sides.TwoSidedLongEdge },
                        FinishingsDefault = Finishings.None,
                        FinishingsSupported = new[] { Finishings.None },
                        PrinterResolutionSupported = new[] { new Resolution(600, 600, ResolutionUnit.DotsPerInch) },
                        PrintQualitySupported = new[] { PrintQuality.Normal },
                        JobPriorityDefault = 50,
                        JobPrioritySupported = 100,
                        CopiesDefault = 1,
                        CopiesSupported = new Range(1, 100),
                        OrientationRequestedDefault = Orientation.Portrait,
                        OrientationRequestedSupported = new[] { Orientation.Portrait, Orientation.Landscape },
                        PageRangesSupported = true,
                        JobHoldUntilDefault = JobHoldUntil.NoHold,
                        JobHoldUntilSupported = new[] { JobHoldUntil.NoHold },
                        JobHoldUntilTimeSupported = true,
                        JobDelayOutputUntilDefault = JobHoldUntil.NoHold,
                        JobDelayOutputUntilSupported = new[] { JobHoldUntil.NoHold },
                        JobDelayOutputUntilTimeSupported = new Range(0, 3600),
                        JobHistoryAttributesConfigured = new[] { "attribute1" },
                        JobHistoryAttributesSupported = new[] { "attribute1" },
                        JobHistoryIntervalConfigured = 60,
                        JobHistoryIntervalSupported = new Range(0, 3600),
                        JobRetainUntilDefault = JobHoldUntil.NoHold,
                        JobRetainUntilIntervalDefault = 3600,
                        JobRetainUntilIntervalSupported = new Range(0, 86400),
                        JobRetainUntilSupported = new[] { JobHoldUntil.NoHold },
                        JobRetainUntilTimeSupported = true,
                        OutputBinDefault = OutputBin.Auto,
                        OutputBinSupported = new[] { OutputBin.Auto },
                        MediaColDefault = new MediaCol { MediaSizeName = Media.IsoA4210x297mm },
                        MediaColDatabase = new[] { new MediaCol { MediaSizeName = Media.IsoA4210x297mm } },
                        MediaColReady = new[] { new MediaCol { MediaSizeName = Media.IsoA4210x297mm } },
                        MediaColSupported = new[] { MediaColMember.MediaSizeName },
                        MediaSizeSupported = new[] { new MediaSizeSupported { XDimension = new Range(210,210), YDimension = new Range(297,297) } },
                        MediaKeySupported = new[] { (MediaKey)"key1" },
                        MediaSourceSupported = new[] { MediaSource.Auto },
                        MediaTypeSupported = new[] { MediaType.Stationery },
                        MediaBackCoatingSupported = new[] { MediaCoating.None },
                        MediaFrontCoatingSupported = new[] { MediaCoating.None },
                        MediaColorSupported = new[] { MediaColor.White },
                        MediaGrainSupported = new[] { MediaGrain.XDirection },
                        MediaToothSupported = new[] { MediaTooth.Fine },
                        MediaPrePrintedSupported = new[] { MediaPrePrinted.Blank },
                        MediaRecycledSupported = new[] { MediaRecycled.None },
                        MediaHoleCountSupported = new[] { new Range(0,0) },
                        MediaOrderCountSupported = new[] { new Range(1,1) },
                        MediaThicknessSupported = new[] { new Range(1,10) },
                        MediaWeightMetricSupported = new[] { new Range(60,200) },
                        MediaBottomMarginSupported = new[] { 0 },
                        MediaLeftMarginSupported = new[] { 0 },
                        MediaRightMarginSupported = new[] { 0 },
                        MediaTopMarginSupported = new[] { 0 },
                        PrintColorModeSupported = new[] { PrintColorMode.Color },
                        JobCreationAttributesSupported = new[] { JobCreationAttribute.Copies, JobCreationAttribute.Finishings, JobCreationAttribute.Media },
                        PrinterUUID = "uuid-1234",
                        DocumentCreationAttributesSupported = new[] { DocumentCreationAttribute.DocumentName },
                        JobAccountIdDefault = "account-1",
                        JobAccountTypeDefault = JobAccountType.None,
                        JobAccountTypeSupported = new[] { JobAccountType.None },
                        JobAccountIdSupported = true,
                        JobAccountingUserIdDefault = "user-1",
                        JobAccountingUserIdSupported = true,
                        JobPasswordEncryptionSupported = new[] { JobPasswordEncryption.None },
                        JobAuthorizationUriSupported = true,
                        PrinterChargeInfo = "charge-info",
                        PrinterChargeInfoUri = "http://charge.info",
                        PrinterMandatoryJobAttributes = new[] { "copies" },
                        PrinterAlert = new[] { "alert1" },
                        PrinterAlertDescription = new[] { "alert-desc" },
                        PrinterSupply = new[] { "supply1" },
                        PrinterSupplyDescription = new[] { "supply-desc" },
                        JobCancelAfterDefault = 0,
                        JobCancelAfterSupported = new Range(0, 3600),
                        JobSpoolingSupported = JobSpooling.Automatic,
                        MaxPageRangesSupported = 5,
                        PrintContentOptimizeDefault = PrintContentOptimize.Auto,
                        PrintContentOptimizeSupported = new[] { PrintContentOptimize.Auto },
                        OutputDeviceSupported = new[] { (OutputDevice)"device-1" },
                        OutputDeviceUuidSupported = new[] { "http://device.uuid/1" },
                        PrinterRequestedClientType = new[] { ClientType.OperatingSystem },
                        PdfVersionsSupported = new[] { PdfVersion.Adobe17 },
                        PrinterServiceType = new[] { (PrinterServiceType)"office-print" },
                        PrinterCameraImageUri = new[] { "http://camera.example.com/image" },
                        PrinterResourceIds = new[] { 42 },
                        FinishingTemplateSupported = new[] { FinishingTemplate.None },
                        FinishingsColSupported = new[] { FinishingsColMember.FinishingTemplate },
                        FinishingsColDefault = new[] { new FinishingsCol { } },
                        FinishingsColReady = new[] { new FinishingsCol { } },
                        BalingTypeSupported = new[] { BalingType.Wrap },
                        BalingWhenSupported = new[] { BalingWhen.AfterJob },
                        BindingReferenceEdgeSupported = new[] { FinishingReferenceEdge.Left },
                        BindingTypeSupported = new[] { BindingType.Flat },
                        CoatingSidesSupported = new[] { CoatingSides.Both },
                        CoatingTypeSupported = new[] { CoatingType.Matte },
                        CoveringNameSupported = new[] { CoveringName.Plain },
                        FinishingsColDatabase = new[] { new FinishingsCol { } },
                        FoldingDirectionSupported = new[] { FoldingDirection.Inward },
                        FoldingOffsetSupported = new[] { new Range(0,0) },
                        FoldingReferenceEdgeSupported = new[] { FinishingReferenceEdge.Left },
                        LaminatingSidesSupported = new[] { CoatingSides.Both },
                        LaminatingTypeSupported = new[] { LaminatingType.Matte },
                        PunchingLocationsSupported = new[] { new Range(0,0) },
                        PunchingOffsetSupported = new[] { new Range(0,0) },
                        PunchingReferenceEdgeSupported = new[] { FinishingReferenceEdge.Left },
                        StitchingAngleSupported = new[] { new Range(0,0) },
                        StitchingLocationsSupported = new[] { new Range(0,0) },
                        StitchingMethodSupported = new[] { StitchingMethod.Auto },
                        StitchingOffsetSupported = new[] { new Range(0,0) },
                        StitchingReferenceEdgeSupported = new[] { FinishingReferenceEdge.Left },
                        TrimmingOffsetSupported = new[] { new Range(0,0) },
                        TrimmingReferenceEdgeSupported = new[] { FinishingReferenceEdge.Left },
                        TrimmingTypeSupported = new[] { TrimmingType.Full },
                        TrimmingWhenSupported = new[] { TrimmingWhen.AfterJob },
                        CoverBackDefault = new Cover
                        {
                            CoverType = CoverType.NoCover,
                            Media = Media.Default,
                            MediaCol = new MediaCol
                            {
                                MediaBackCoating = MediaCoating.None,
                                MediaBottomMargin = 10,
                                MediaLeftMargin = 10,
                                MediaRightMargin = 10,
                                MediaColor = MediaColor.Black,
                                MediaFrontCoating = MediaCoating.None,
                                MediaGrain = MediaGrain.XDirection,
                                MediaHoleCount = 1,
                                MediaInfo = "media-info",
                                MediaKey = (MediaKey)"media-key"
                            }
                        },
                        CoverBackSupported = new[] { CoverMember.Media },
                        CoverFrontDefault = new Cover
                        {
                            CoverType = CoverType.NoCover,
                            Media = Media.Default,
                            MediaCol = new MediaCol
                            {
                                MediaBackCoating = MediaCoating.None,
                                MediaBottomMargin = 10,
                                MediaLeftMargin = 10,
                                MediaRightMargin = 10,
                                MediaColor = MediaColor.Black,
                                MediaFrontCoating = MediaCoating.None,
                                MediaGrain = MediaGrain.XDirection,
                                MediaHoleCount = 1,
                                MediaInfo = "media-info",
                                MediaKey = (MediaKey)"media-key"
                            }
                        },
                        CoverFrontSupported = new[] { CoverMember.Media },
                        CoverTypeSupported = new[] { CoverType.NoCover },
                        ForceFrontSideSupported = new Range(0,0),
                        ImageOrientationDefault = Orientation.Portrait,
                        ImageOrientationSupported = new[] { Orientation.Portrait },
                        ImpositionTemplateDefault = ImpositionTemplate.None,
                        ImpositionTemplateSupported = new[] { ImpositionTemplate.None },
                        InsertCountSupported = new Range(0,0),
                        InsertSheetDefault = new[] { new InsertSheet { } },
                        InsertSheetSupported = new[] { InsertSheetMember.InsertCount },
                        JobAccountingOutputBinSupported = new[] { (OutputBin)"bin1" },
                        JobAccountingSheetsDefault = new JobAccountingSheets
                        {
                            JobAccountingOutputBin = OutputBin.Auto,
                            JobAccountingSheetsType = JobSheetsType.None,
                            Media = Media.Default,
                            MediaCol = new MediaCol
                            {
                                MediaBackCoating = MediaCoating.None,
                                MediaBottomMargin = 10,
                                MediaLeftMargin = 10,
                                MediaRightMargin = 10,
                                MediaColor = MediaColor.Black,
                                MediaFrontCoating = MediaCoating.None,
                                MediaGrain = MediaGrain.XDirection,
                                MediaHoleCount = 1,
                                MediaInfo = "media-info",
                                MediaKey = (MediaKey)"media-key"
                            }
                        },
                        JobAccountingSheetsSupported = new[] { JobAccountingSheetsMember.JobAccountingSheetsType },
                        JobAccountingSheetsTypeSupported = new[] { JobSheetsType.None },
                        JobCompleteBeforeSupported = new[] { JobHoldUntil.None },
                        JobCompleteBeforeTimeSupported = true,
                        JobErrorSheetDefault = new JobErrorSheet
                        {
                            JobErrorSheetType = JobSheetsType.None,
                            JobErrorSheetWhen = JobErrorSheetWhen.OnError,
                            Media = Media.Default,
                            MediaCol = new MediaCol
                            {
                                MediaBackCoating = MediaCoating.None,
                                MediaBottomMargin = 10,
                                MediaLeftMargin = 10,
                                MediaRightMargin = 10,
                                MediaColor = MediaColor.Black,
                                MediaFrontCoating = MediaCoating.None,
                                MediaGrain = MediaGrain.XDirection,
                                MediaHoleCount = 1,
                                MediaInfo = "media-info",
                                MediaKey = (MediaKey)"media-key"
                            }
                        },
                        JobErrorSheetSupported = new[] { JobErrorSheetMember.JobErrorSheetType },
                        JobErrorSheetTypeSupported = new[] { JobSheetsType.None },
                        JobErrorSheetWhenSupported = new[] { JobErrorSheetWhen.OnError },
                        JobMessageToOperatorSupported = true,
                        JobPhoneNumberDefault = "12345",
                        JobPhoneNumberSchemeSupported = new[] { JobPhoneNumberScheme.Tel },
                        JobPhoneNumberSupported = true,
                        JobRecipientNameSupported = true,
                        JobSheetMessageSupported = true,
                        PageDeliveryDefault = PageDelivery.SameOrderFaceUp,
                        PageDeliverySupported = new[] { PageDelivery.SameOrderFaceUp },
                        PresentationDirectionNumberUpDefault = PresentationDirectionNumberUp.ToleftTobottom,
                        PresentationDirectionNumberUpSupported = new[] { PresentationDirectionNumberUp.ToleftTobottom },
                        SeparatorSheetsDefault = new SeparatorSheets
                        {
                            SeparatorSheetsType = [SeparatorSheetsType.None],
                            Media = Media.Default,
                            MediaCol = new MediaCol
                            {
                                MediaBackCoating = MediaCoating.None,
                                MediaBottomMargin = 10,
                                MediaLeftMargin = 10,
                                MediaRightMargin = 10,
                                MediaColor = MediaColor.Black,
                                MediaFrontCoating = MediaCoating.None,
                                MediaGrain = MediaGrain.XDirection,
                                MediaHoleCount = 1,
                                MediaInfo = "media-info",
                                MediaKey = (MediaKey)"media-key"
                            }
                        },
                        SeparatorSheetsSupported = new[] { SeparatorSheetsMember.SeparatorSheetsType },
                        SeparatorSheetsTypeSupported = new[] { SeparatorSheetsType.None },
                        XImagePositionDefault = XImagePosition.Left,
                        XImagePositionSupported = new[] { XImagePosition.Left },
                        XImageShiftDefault = 0,
                        XImageShiftSupported = new Range(0,0),
                        XSide1ImageShiftDefault = 0,
                        XSide2ImageShiftDefault = 0,
                        YImagePositionDefault = YImagePosition.Top,
                        YImagePositionSupported = new[] { YImagePosition.Top },
                        YImageShiftDefault = 0,
                        YImageShiftSupported = new Range(0,0),
                        YSide1ImageShiftDefault = 0,
                        YSide2ImageShiftDefault = 0,
                        AccuracyUnitsSupported = ["test"],
                        ClientInfoSupported = ["test"],
                        CompressionDefault = Compression.None,
                        CompressionSupported = [Compression.None],
                        DocumentCharsetDefault = "en",
                        DocumentCharsetSupported = ["en"],
                        DocumentFormatDetailsSupported = ["test"],
                        DocumentNaturalLanguageDefault = "en",
                        DocumentNaturalLanguageSupported = ["en"],
                        IppFeaturesSupported = [(IppFeature)"test"],
                        JobIdsSupported = true,
                        JobImpressionsSupported = new Range(0,1),
                        JobKOctetsSupported = new Range(0,1),
                        JobMandatoryAttributesSupported = true,
                        JobMediaSheetsSupported = new Range(0,1),
                        JobPagesPerSetSupported = true,
                        PrinterFinisher = [new PrinterFinisher {
                            Capacity = 10,
                            Index = 0,
                            MaxCapacity = 10,
                            PresentOnOff = "test",
                            Status = 0,
                            Type = "test",
                            Unit = "test",
                            Extensions = new Dictionary<string, string> { { "test", "test" } }
                        }],
                        JobSheetsColDefault = new JobSheetsCol
                        {
                            JobSheets = JobSheets.Standard,
                            Media = Media.Default,
                            MediaCol = new MediaCol
                            {
                                MediaBackCoating = MediaCoating.None,
                                MediaBottomMargin = 10,
                                MediaLeftMargin = 10,
                                MediaRightMargin = 10,
                                MediaColor = MediaColor.Black,
                                MediaFrontCoating = MediaCoating.None,
                                MediaGrain = MediaGrain.XDirection,
                                MediaHoleCount = 1,
                                MediaInfo = "media-info",
                                MediaKey = (MediaKey)"media-key"
                            }
                        },
                        JobSheetsSupported = new[] { JobSheets.Standard },
                        JobSheetsColSupported = [(JobSheetsColMember)"test"],
                        JobSheetsDefault = JobSheets.Standard,
                        NumberUpDefault = 1,
                        NumberUpSupported = new[] { new Range(1, 4) },
                        PunchingHoleDiameterConfigured = 2,
                        PrinterFinisherDescription = new[] { "finisher-desc" },
                        PrinterFinisherSupplies = new[] { new PrinterFinisherSupply { Type = "toner", Unit = "percent", Max = 100, Level = 80 } },
                        PrinterFinisherSuppliesDescription = new[] { "finisher-supplies-desc" },
                        PrinterConfigChanges = 1,
                        PrinterContactCol = [new() {
                            ContactName = "user",
                            ContactUri = new Uri("http://test.com"),
                            ContactVcard = ["vcard"]
                        }],
                        PrinterGeoLocation = new Uri("http://test.com"),
                        PrinterIds = [1],
                        PrinterImpressionsCompleted = 2,
                        PrinterImpressionsCompletedCol = 2,
                        PrinterMediaSheetsCompleted = 3,
                        PrinterMediaSheetsCompletedCol = 3,
                        PrinterPagesCompleted = 4,
                        PrinterPagesCompletedCol = 5
                    },
                    OperationAttributes = new()
                    {
                        StatusMessage = "successful-ok",
                        DetailedStatusMessage = ["detail1"],
                        DocumentAccessError = "none"
                    }
                };
            var responseStream = new MemoryStream();
            await server.SendResponseAsync(serverResponse, responseStream, c);
            responseStream.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = statusCode, Content = new StreamContent(responseStream) };
        }
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));

        GetPrinterAttributesResponse? clientResponse = await client.GetPrinterAttributesAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task GetPrinterAttributesAsync_NoValueEverywhere_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        SharpIppServer server = new();
        GetPrinterAttributesRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
                RequestedAttributes = ["printer-state", "queued-job-count"],
                DocumentFormat = "application/pdf",
            },
        };

        IIppRequestMessage? serverRawRequest = null;
        GetPrinterAttributesResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRawRequest = await server.ReceiveRawRequestAsync(s, c);

            var serverRawResponse = new IppResponseMessage
            {
                RequestId = serverRawRequest.RequestId,
                Version = serverRawRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
            };

            serverRawResponse.OperationAttributes.Add([
                new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"),
                new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en"),
                new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.StatusMessage, "successful-ok"),
                new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.DetailedStatusMessage, "detail1"),
                new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.DocumentAccessError, "none")
            ]);

            serverRawResponse.PrinterAttributes.Add([
                new IppAttribute(Tag.NoValue, PrinterAttribute.PrinterName, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PrinterLocation, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PrinterInfo, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.QueuedJobCount, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PrinterCurrentTime, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PrinterState, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.JobKOctetsSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PrinterResolutionDefault, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.MediaColDefault, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.MultipleDocumentJobsSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PrinterIsAcceptingJobs, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.ColorSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PrinterUpTime, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.MultipleOperationTimeOut, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.JpegKOctetsSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PdfKOctetsSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.JobImpressionsSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.JobMediaSheetsSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PagesPerMinute, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PagesPerMinuteColor, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PrinterResolutionSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.JobPriorityDefault, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.JobPrioritySupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.CopiesDefault, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.CopiesSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PageRangesSupported, NoValue.Instance)
            ]);

            var memoryStream = new MemoryStream();
            await server.SendRawResponseAsync(serverRawResponse, memoryStream, c);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = statusCode, Content = new StreamContent(memoryStream) };
        }

        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        serverResponse = new GetPrinterAttributesResponse
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            StatusCode = IppStatusCode.SuccessfulOk,
            OperationAttributes = new()
            {
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en",
                StatusMessage = "successful-ok",
                DetailedStatusMessage = ["detail1"],
                DocumentAccessError = "none"
            },
            PrinterAttributes = new()
            {
                MultipleDocumentJobsSupported = NoValue.GetNoValue<bool?>(),
                PrinterIsAcceptingJobs = NoValue.GetNoValue<bool?>(),
                ColorSupported = NoValue.GetNoValue<bool?>(),
                PrinterUpTime = NoValue.GetNoValue<int>(),
                MultipleOperationTimeOut = NoValue.GetNoValue<int>(),
                JpegKOctetsSupported = NoValue.GetNoValue<SharpIpp.Protocol.Models.Range>(),
                PdfKOctetsSupported = NoValue.GetNoValue<SharpIpp.Protocol.Models.Range>(),
                JobImpressionsSupported = NoValue.GetNoValue<SharpIpp.Protocol.Models.Range>(),
                JobMediaSheetsSupported = NoValue.GetNoValue<SharpIpp.Protocol.Models.Range>(),
                PagesPerMinute = NoValue.GetNoValue<int>(),
                PagesPerMinuteColor = NoValue.GetNoValue<int>(),
                PrinterResolutionSupported = [NoValue.GetNoValue<Resolution>()],
                JobPriorityDefault = NoValue.GetNoValue<int>(),
                JobPrioritySupported = NoValue.GetNoValue<int>(),
                CopiesDefault = NoValue.GetNoValue<int>(),
                CopiesSupported = NoValue.GetNoValue<SharpIpp.Protocol.Models.Range>(),
                PageRangesSupported = NoValue.GetNoValue<bool?>(),
                PrinterName = NoValue.GetNoValue<string>(),
                PrinterLocation = NoValue.GetNoValue<string>(),
                PrinterInfo = NoValue.GetNoValue<string>(),
                QueuedJobCount = NoValue.GetNoValue<int>(),
                PrinterCurrentTime = NoValue.GetNoValue<DateTimeOffset>(),
                PrinterState = NoValue.GetNoValue<PrinterState>(),
                JobKOctetsSupported = NoValue.GetNoValue<SharpIpp.Protocol.Models.Range>(),
                PrinterResolutionDefault = NoValue.GetNoValue<Resolution>(),
                MediaColDefault = NoValue.GetNoValue<MediaCol>()
            }
        };

        var clientRawRequest = client.CreateRawRequest(clientRequest);
        GetPrinterAttributesResponse? clientResponse = await client.GetPrinterAttributesAsync(clientRequest);

        clientRawRequest.Should().NotBeNull().And.BeEquivalentTo(serverRawRequest, options => options.Excluding(x => x!.Document));
        clientResponse.Should().BeEquivalentTo(serverResponse);
        clientResponse!.PrinterAttributes.PrinterName.Should().Be(NoValue.GetNoValue<string>());
    }
}