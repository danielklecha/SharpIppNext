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
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
                JobId = 1,
                JobUri = new Uri("http://127.0.0.1:631/jobs/1"),
                RequestedAttributes = ["job-id", "job-uri", "job-state"],
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
                    JobUri = "http://127.0.0.1:631/jobs/1",
                    JobPrinterUri = new Uri("http://127.0.0.1:631"),
                    JobState = JobState.Pending,
                    JobStateReasons = [JobStateReason.None],
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
                    JobMoreInfo = "more info",
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
                    JobDetailedStatusMessages = ["message"],
                    JobDocumentAccessErrors = ["error"],
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
                    ClientInfo = [new ClientInfo { ClientName = "MyClient", ClientType = ClientType.Application }],
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
                    CopiesActual = [1],
                    FinishingsActual = [Finishings.None],
                    CoverBackActual = [new Cover { CoverType = CoverType.PrintBack }],
                    CoverFrontActual = [new Cover { CoverType = CoverType.PrintFront }],
                    JobHoldUntilActual = [JobHoldUntil.NoHold],
                    JobPriorityActual = [50],
                    JobSheetsActual = [JobSheets.None],
                    MediaActual = [(Media)"iso_a4_210x297mm"],
                    ImpositionTemplateActual = [(ImpositionTemplate)"none"],
                    InsertSheetActual = [new InsertSheet { InsertAfterPageNumber = 1 }],
                    JobAccountIdActual = ["acct-1"],
                    JobAccountingSheetsActual = [new JobAccountingSheets { JobAccountingSheetsType = JobSheetsType.None }],
                    JobAccountingUserIdActual = ["user-1"],
                    JobErrorSheetActual = [new JobErrorSheet { JobErrorSheetType = JobSheetsType.None }],
                    JobMessageToOperatorActual = ["operator message actual"],
                    JobSheetMessageActual = ["sheet message actual"],
                    MediaColActual = [new MediaCol { MediaSizeName = (Media)"iso_a4_210x297mm", MediaType = (MediaType)"stationery" }],
                    MediaInputTrayCheckActual = [(MediaInputTrayCheck)"tray-1"],
                    MultipleDocumentHandlingActual = [MultipleDocumentHandling.SeparateDocumentsUncollatedCopies],
                    NumberUpActual = [1],
                    OrientationRequestedActual = [Orientation.Portrait],
                    OutputBinActual = [(OutputBin)"face-down"],
                    PageDeliveryActual = [PageDelivery.ReverseOrderFaceDown],
                    PageOrderReceivedActual = [PageOrderReceived.OneToNOrder],
                    PageRangesActual = [new SharpIpp.Protocol.Models.Range(1, 2)],
                    PresentationDirectionNumberUpActual = [PresentationDirectionNumberUp.TorightTobottom],
                    PrintQualityActual = [PrintQuality.Normal],
                    PrinterResolutionActual = [new Resolution(600, 600, ResolutionUnit.DotsPerInch)],
                    SidesActual = [Sides.OneSided],
                    SeparatorSheetsActual = [new SeparatorSheets { SeparatorSheetsType = [SeparatorSheetsType.None] }],
                    XImagePositionActual = [XImagePosition.None],
                    XImageShiftActual = [0],
                    XSide1ImageShiftActual = [0],
                    XSide2ImageShiftActual = [0],
                    YImagePositionActual = [YImagePosition.None],
                    YImageShiftActual = [0],
                    YSide1ImageShiftActual = [0],
                    YSide2ImageShiftActual = [0],
                    OverridesActual =
                    [
                        new OverrideInstruction
                        {
                            PageRanges = [new SharpIpp.Protocol.Models.Range(1, 1)],
                            DocumentNumberRanges = [new SharpIpp.Protocol.Models.Range(1, 1)],
                            DocumentCopyRanges = [new SharpIpp.Protocol.Models.Range(1, 1)],
                            JobTemplateAttributes = new JobTemplateAttributes
                            {
                                Media = (Media)"iso_a4_210x297mm",
                                Sides = Sides.OneSided
                            }
                        }
                    ],
                    FinishingsColActual = [
                        new FinishingsCol
                        {
                            FinishingTemplate = (FinishingTemplate)"staple",
                            Stitching = new Stitching
                            {
                                StitchingAngle = 90,
                                StitchingMethod = StitchingMethod.Wire,
                                StitchingReferenceEdge = FinishingReferenceEdge.Left,
                                StitchingLocations = [10, 20],
                                StitchingOffset = 5
                            },
                            Binding = new Binding
                            {
                                BindingReferenceEdge = FinishingReferenceEdge.Left,
                                BindingType = BindingType.Perfect
                            }
                        }
                    ],
                    MaterialsColActual = [new Material { MaterialName = "matte-paper", MaterialColor = (MaterialColor?)"white" }],
                    ChamberHumidityActual = [35],
                    ChamberTemperatureActual = [26],
                    MultipleObjectHandlingActual3d = (MultipleObjectHandling?)"abort-job",
                    PlatformTemperatureActual = [70],
                    PrintAccuracyActual3d = new PrintAccuracy
                    {
                        AccuracyUnits = (AccuracyUnits?)"mm",
                        XAccuracy = 100,
                        YAccuracy = 100,
                        ZAccuracy = 50
                    },
                    PrintBaseActual3d = [PrintBase.Raft],
                    PrintObjectsActual3d = [new PrintObject { DocumentNumber = 1 }],
                    PrintSupportsActual3d = [PrintSupports.Standard],
                    DocumentFormatReady = ["application/pdf"],
                    OutputDeviceJobStateReasons = [JobStateReason.None],
                    OutputDeviceUuidAssigned = new Uri("urn:uuid:12345678-1234-1234-1234-123456789012"),
                    DestinationStatuses = [new DestinationStatus { DestinationUri = "test-uri", ImagesCompleted = 10, TransmissionStatus = TransmissionStatus.Canceled }]
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
                RequestedAttributes = ["job-id", "job-resource-ids"]
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
}
