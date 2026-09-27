using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace SharpIpp.Tests.Integration;

[TestClass]
[ExcludeFromCodeCoverage]
public class GetJobAttributesTests : SharpIppIntegrationTestBase
{
    [TestMethod()]
    public async Task GetJobAttributesAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        SharpIppServer server = new();
        GetJobAttributesRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                AttributesCharset = Charset.Utf8,
                AttributesNaturalLanguage = (NaturalLanguage)"en-us",
                RequestingUserName = "test-user",
                JobId = 1,
                JobUri = new Uri("http://127.0.0.1:631/jobs/1"),
                RequestedAttributes = new[] { "job-id", "job-uri", "job-state" },
            },
        };
        IIppRequest? serverRequest = null;
        GetJobAttributesResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new GetJobAttributesResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                JobAttributes = new JobDescriptionAttributes
                {
                    JobId = 1,
                    JobUri = new Uri("http://127.0.0.1:631/jobs/1"),
                    JobPrinterUri = new Uri("http://127.0.0.1:631"),
                    JobState = JobState.Pending,
                    JobStateReasons = new[] { JobStateReason.None },
                    JobName = "Test Job",
                    JobOriginatingUserName = "test-user",
                    JobKOctetsProcessed = 10,
                    JobImpressions = 5,
                    JobImpressionsCol = new JobCounter
                    {
                        Blank = 1,
                        BlankTwoSided = 2,
                        FullColor = 2,
                        FullColorTwoSided = 3,
                        HighlightColor = 4,
                        HighlightColorTwoSided = 5,
                        Monochrome = 6,
                        MonochromeTwoSided = 3
                    },
                    JobImpressionsCompleted = 0,
                    JobMediaSheets = 2,
                    JobMediaSheetsCol = new JobCounter
                    {
                        Blank = 4,
                        FullColor = 5,
                        MonochromeTwoSided = 6
                    },
                    JobMoreInfo = new Uri("more info", UriKind.RelativeOrAbsolute),
                    JobChargeInfo = "charge info",
                    DocumentFormatDetails = new DocumentFormatDetails
                    {
                        DocumentSourceApplicationName = "MyApp",
                        DocumentSourceOsName = "MyOS"
                    },
                    DocumentFormatDetailsDetected = new DocumentFormatDetails
                    {
                        DocumentSourceApplicationName = "DetectedApp",
                        DocumentSourceOsName = "DetectedOS"
                    },
                    NumberOfDocuments = 1,
                    NumberOfInterveningJobs = 0,
                    OutputDeviceAssigned = "printer",
                    JobMediaSheetsCompleted = 0,
                    JobStateMessage = "pending",
                    DateTimeAtCreation = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    DateTimeAtProcessing = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    DateTimeAtCompleted = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    DateTimeAtCompletedEstimated = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    DateTimeAtProcessingEstimated = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    TimeAtCreation = 100,
                    TimeAtProcessing = 110,
                    TimeAtCompleted = 120,
                    TimeAtCompletedEstimated = 120,
                    TimeAtProcessingEstimated = 110,
                    OutputDeviceJobState = JobState.Processing,
                    JobPrinterUpTime = 200,
                    JobKOctets = 20,
                    JobDetailedStatusMessages = new[] { "message" },
                    JobDocumentAccessErrors = new[] { "error" },
                    JobMessageFromOperator = "operator message",
                    JobPages = 10,
                    JobPagesCompleted = 5,
                    JobPagesCol = new JobCounter
                    {
                        Blank = 7,
                        FullColor = 8,
                        MonochromeTwoSided = 9
                    },
                    JobImpressionsCompletedCol = new JobCounter
                    {
                        Blank = 10,
                        FullColor = 11,
                        MonochromeTwoSided = 12
                    },
                    JobMediaSheetsCompletedCol = new JobCounter
                    {
                        Blank = 13,
                        FullColor = 14,
                        MonochromeTwoSided = 15
                    },
                    JobPagesCompletedCol = new JobCounter
                    {
                        Blank = 16,
                        FullColor = 17,
                        MonochromeTwoSided = 18
                    },
                    ClientInfo = new[] { new ClientInfo { ClientName = "MyClient", ClientType = ClientType.Application } },
                    JobSheetsCol = new JobSheetsCol
                    {
                        JobSheets = JobSheets.Standard,
                        Media = (Media)"iso_a4_210x297mm",
                        MediaCol = new MediaCol { MediaColor = (MediaColor)"blue" }
                    },
                    JobProcessingTime = 30,
                    ErrorsCount = 0,
                    WarningsCount = 1,
                    PrintContentOptimizeActual = new[] { PrintContentOptimize.Text },
                    CopiesActual = new[] { 1 },
                    FinishingsActual = new[] { Finishings.None },
                    CoverBackActual = new[] { new Cover { CoverType = CoverType.PrintBack } },
                    CoverFrontActual = new[] { new Cover { CoverType = CoverType.PrintFront } },
                    JobHoldUntilActual = new[] { JobHoldUntil.NoHold },
                    JobPriorityActual = new[] { 50 },
                    JobSheetsActual = new[] { JobSheets.None },
                    MediaActual = new[] { (Media)"iso_a4_210x297mm" },
                    ImpositionTemplateActual = new[] { (ImpositionTemplate)"none" },
                    InsertSheetActual = new[] { new InsertSheet { InsertAfterPageNumber = 1 } },
                    JobAccountIdActual = new[] { "acct-1" },
                    JobAccountingSheetsActual = new[] { new JobAccountingSheets { JobAccountingSheetsType = JobAccountingSheetsType.None } },
                    JobAccountingUserIdActual = new[] { "user-1" },
                    JobErrorSheetActual = new[] { new JobErrorSheet { JobErrorSheetType = JobErrorSheetType.None } },
                    JobMessageToOperatorActual = new[] { "operator message actual" },
                    JobSheetMessageActual = new[] { "sheet message actual" },
                    MediaColActual = new[] { new MediaCol { MediaSizeName = (Media)"iso_a4_210x297mm", MediaType = (MediaType)"stationery" } },
                    MediaInputTrayCheckActual = new[] { (MediaInputTrayCheck)"tray-1" },
                    MultipleDocumentHandlingActual = new[] { MultipleDocumentHandling.SeparateDocumentsUncollatedCopies },
                    NumberUpActual = new[] { 1 },
                    OrientationRequestedActual = new[] { Orientation.Portrait },
                    OutputBinActual = new[] { (OutputBin)"face-down" },
                    PageDeliveryActual = new[] { PageDelivery.ReverseOrderFaceDown },
                    PageOrderReceivedActual = new[] { PageOrderReceived.OneToNOrder },
                    PageRangesActual = new[] { new SharpIpp.Protocol.Models.Range(1, 2) },
                    PresentationDirectionNumberUpActual = new[] { PresentationDirectionNumberUp.TorightTobottom },
                    PrintQualityActual = new[] { PrintQuality.Normal },
                    PrinterResolutionActual = new[] { new Resolution(600, 600, ResolutionUnit.DotsPerInch) },
                    SidesActual = new[] { Sides.OneSided },
                    SeparatorSheetsActual = new[] { new SeparatorSheets { SeparatorSheetsType = new[] { SeparatorSheetsType.None } } },
                    XImagePositionActual = new[] { XImagePosition.None },
                    XImageShiftActual = new[] { 0 },
                    XSide1ImageShiftActual = new[] { 0 },
                    XSide2ImageShiftActual = new[] { 0 },
                    YImagePositionActual = new[] { YImagePosition.None },
                    YImageShiftActual = new[] { 0 },
                    YSide1ImageShiftActual = new[] { 0 },
                    YSide2ImageShiftActual = new[] { 0 },
                    OverridesActual = new[]
                    {
                        new OverrideInstruction
                        {
                            PageRanges = new[] { new SharpIpp.Protocol.Models.Range(1, 1) },
                            DocumentNumberRanges = new[] { new SharpIpp.Protocol.Models.Range(1, 1) },
                            DocumentCopyRanges = new[] { new SharpIpp.Protocol.Models.Range(1, 1) },
                            JobTemplateAttributes = new JobTemplateAttributes
                            {
                                Media = (Media)"iso_a4_210x297mm",
                                Sides = Sides.OneSided
                            }
                        }
                    },
                    FinishingsColActual = new[]
                    {
                        new FinishingsCol
                        {
                            FinishingTemplate = (FinishingTemplate)"staple",
                            Stitching = new Stitching
                            {
                                StitchingAngle = 90,
                                StitchingMethod = StitchingMethod.Wire,
                                StitchingReferenceEdge = FinishingReferenceEdge.Left,
                                StitchingLocations = new[] { 10, 20 },
                                StitchingOffset = 5
                            },
                            Binding = new Binding
                            {
                                BindingReferenceEdge = FinishingReferenceEdge.Left,
                                BindingType = BindingType.Perfect
                            }
                        }
                    },
                    MaterialsColActual = new[] { new Material { MaterialName = "matte-paper", MaterialColor = (MaterialColor?)"white" } },
                    ChamberHumidityActual = new[] { 35 },
                    ChamberTemperatureActual = new[] { 26 },
                    MultipleObjectHandlingActual3d = (MultipleObjectHandling?)"abort-job",
                    PlatformTemperatureActual = new[] { 70 },
                    PrintAccuracyActual3d = new PrintAccuracy
                    {
                        AccuracyUnits = (AccuracyUnits?)"mm",
                        XAccuracy = 100,
                        YAccuracy = 100,
                        ZAccuracy = 50
                    },
                    PrintBaseActual3d = new[] { PrintBase.Raft },
                    PrintObjectsActual3d = new[] { new PrintObject { DocumentNumber = 1 } },
                    PrintSupportsActual3d = new[] { PrintSupports.Standard },
                    DocumentFormatReady = new[] { "application/pdf" },
                    OutputDeviceJobStateReasons = new[] { JobStateReason.None },
                    OutputDeviceJobStateMessage = "processing on output device",
                    OutputDeviceUuidAssigned = new Uri("urn:uuid:12345678-1234-1234-1234-123456789012"),
                    DestinationStatuses = new[] { new DestinationStatus { DestinationUri = new Uri("test-uri", UriKind.RelativeOrAbsolute), ImagesCompleted = 10, TransmissionStatus = TransmissionStatus.Canceled } },
                    JobCopiesActual = new[] { 2 },
                    JobKOctetsCompleted = 15,
                    JobPassword = new OctetString(new byte[] {  0x01, 0x02, 0x03  }),
                    JobPasswordEncryption = JobPasswordEncryption.None,
                    JobMandatoryAttributes = new[] { "copies", "sides" },
                    JobIds = new[] { 101, 102 },
                    RequestingUserUri = new Uri("mailto:user@example.com"),
                    JobChargeInfoUri = new Uri("http://example.com/charge/123"),
                    JobPagesCompletedCurrentCopy = 8,
                    PagesCompletedCurrentCopy = 4,
                    PagesPerSubsetActual = new[] { 2, 3 },
                    ChamberHumidityCurrent = 42,
                    ChamberTemperatureCurrent = 55
                },
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = "detail1",
                    DocumentAccessError = "none"
                }
            };
            var responseStream = new MemoryStream();
            await server.SendResponseAsync(serverResponse, responseStream, c);
            responseStream.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = statusCode, Content = new StreamContent(responseStream) };
        }
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));

        GetJobAttributesResponse? clientResponse = await client.GetJobAttributesAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task GetJobAttributesResponseMapping_IncludesJobResourceIds()
    {
        SharpIppServer server = new();
        GetJobAttributesRequest clientRequest = new()
        {
            RequestId = 124,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                JobId = 1,
                RequestedAttributes = new[] { "job-id", "job-resource-ids" }
            }
        };

        IIppRequest? serverRequest = null;
        GetJobAttributesResponse? serverResponse = null;

        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new GetJobAttributesResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                JobAttributes = new JobDescriptionAttributes
                {
                    JobId = 1,
                    JobResourceIds = new[] { 101, 102, 103 }
                }
            };

            var responseStream = new MemoryStream();
            await server.SendResponseAsync(serverResponse, responseStream, c);
            responseStream.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(responseStream) };
        }

        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));

        GetJobAttributesResponse? clientResponse = await client.GetJobAttributesAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse!.JobAttributes!.JobResourceIds.Should().BeEquivalentTo(new[] { 101, 102, 103 });
    }

    [TestMethod()]
    public async Task GetJobAttributesAsync_WhenOverridesActualContainsNoValue_ServerSendsAndClientReceives()
    {
        SharpIppServer server = new();
        GetJobAttributesRequest clientRequest = new()
        {
            RequestId = 125,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                JobId = 42
            }
        };

        IIppRequest? serverRequest = null;
        GetJobAttributesResponse serverResponse = new()
        {
            RequestId = 125,
            Version = new IppVersion(2, 0),
            StatusCode = IppStatusCode.SuccessfulOk,
            JobAttributes = new()
            {
                OverridesActual = new[]
                {
                    new OverrideInstruction
                    {
                        PageRanges = IppValue<SharpIpp.Protocol.Models.Range[]>.NoValue,
                        DocumentNumberRanges = IppValue<SharpIpp.Protocol.Models.Range[]>.NoValue,
                        DocumentCopyRanges = IppValue<SharpIpp.Protocol.Models.Range[]>.NoValue,
                        JobTemplateAttributes = new JobTemplateAttributes
                        {
                            Media = (Media)"iso_a4_210x297mm",
                            Sides = Sides.OneSided
                        }
                    }
                }
            },
            OperationAttributes = new()
            {
                StatusMessage = "successful-ok"
            }
        };

        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = await server.ReceiveRequestAsync(s, c);
            var responseStream = new MemoryStream();
            await server.SendResponseAsync(serverResponse, responseStream, c);
            responseStream.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = statusCode, Content = new StreamContent(responseStream) };
        }

        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));

        GetJobAttributesResponse? clientResponse = await client.GetJobAttributesAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}