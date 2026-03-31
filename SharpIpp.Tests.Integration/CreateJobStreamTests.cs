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
public class CreateJobStreamTests : SharpIppIntegrationTestBase
{
    [TestMethod()]
    public async Task CreateJobAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        SharpIppServer server = new();
        CreateJobRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
                JobName = "Test Job",
                IppAttributeFidelity = true,
                JobKOctets = 12,
                JobImpressions = 5,
                JobMediaSheets = 2,
            },
            JobTemplateAttributes = new()
            {
                Copies = 1,
                JobPriority = 1,
                JobSheets = JobSheets.None,
                JobHoldUntil = JobHoldUntil.NoHold,
                MultipleDocumentHandling = MultipleDocumentHandling.SeparateDocumentsUncollatedCopies,
                FinishingsCol = [new FinishingsCol
                {
                    FinishingTemplate = (FinishingTemplate)"staple",
                    Baling = new Baling { BalingType = BalingType.Band, BalingWhen = BalingWhen.AfterJob },
                    Binding = new Binding { BindingReferenceEdge = FinishingReferenceEdge.Left, BindingType = BindingType.Perfect },
                    Coating = new Coating { CoatingSides = CoatingSides.Front, CoatingType = CoatingType.Glossy },
                    Covering = new Covering { CoveringName = CoveringName.Plain },
                    Folding = [new Folding { FoldingDirection = FoldingDirection.Inward, FoldingOffset = 500, FoldingReferenceEdge = FinishingReferenceEdge.Bottom }],
                    Laminating = new Laminating { LaminatingSides = CoatingSides.Both, LaminatingType = LaminatingType.Matte },
                    Punching = new Punching { PunchingLocations = [15, 30], PunchingOffset = 20, PunchingReferenceEdge = FinishingReferenceEdge.Right },
                    Stitching = new Stitching { StitchingAngle = 90, StitchingMethod = StitchingMethod.Wire, StitchingReferenceEdge = FinishingReferenceEdge.Left, StitchingLocations = [10, 20], StitchingOffset = 5 },
                    Trimming = [new Trimming { TrimmingOffset = [100, 200], TrimmingReferenceEdge = FinishingReferenceEdge.Top, TrimmingType = TrimmingType.Full, TrimmingWhen = TrimmingWhen.AfterJob }],
                    ImpositionTemplate = (ImpositionTemplate)"signature",
                    MediaSheetsSupported = new SharpIpp.Protocol.Models.Range(1, 10),
                    MediaSizeName = (Media)"iso_a4_210x297mm",
                    MediaSize = new MediaSize { XDimension = 21000, YDimension = 29700 }
                }],
                PageRanges = [new SharpIpp.Protocol.Models.Range(1, 2)],
                Sides = Sides.OneSided,
                NumberUp = 1,
                OrientationRequested = Orientation.Portrait,
                Media = (Media)"iso_a4_210x297mm",
                PrinterResolution = new Resolution(600, 600, ResolutionUnit.DotsPerInch),
                PrintQuality = PrintQuality.Normal,
                PrintScaling = PrintScaling.Auto,
                PrintColorMode = PrintColorMode.Color,
                MediaCol = new MediaCol
                {
                    MediaBackCoating = MediaCoating.Glossy,
                    MediaBottomMargin = 10,
                    MediaColor = (MediaColor)"white",
                    MediaFrontCoating = MediaCoating.Glossy,
                    MediaGrain = MediaGrain.XDirection,
                    MediaHoleCount = 0,
                    MediaInfo = "test",
                    MediaKey = (MediaKey)"test",
                    MediaLeftMargin = 10,
                    MediaOrderCount = 1,
                    MediaPrePrinted = MediaPrePrinted.Blank,
                    MediaRecycled = MediaRecycled.None,
                    MediaRightMargin = 10,
                    MediaSize = new MediaSize { XDimension = 21000, YDimension = 29700 },
                    MediaSizeName = (Media)"iso_a4_210x297mm",
                    MediaSource = MediaSource.Main,
                    MediaSourceProperties = new MediaSourceProperties { MediaSourceFeedDirection = MediaSourceFeedDirection.LongEdgeFirst, MediaSourceFeedOrientation = Orientation.Portrait },
                    MediaThickness = 10,
                    MediaTooth = MediaTooth.Medium,
                    MediaTopMargin = 10,
                    MediaType = (MediaType)"stationery",
                    MediaWeightMetric = 80
                },
                OutputBin = (OutputBin)"face-down",
                JobAccountId = "acct123",
                JobAccountingUserId = "acct-user",
                JobCancelAfter = 300,
                JobDelayOutputUntil = JobHoldUntil.DayTime,
                JobDelayOutputUntilTime = new DateTimeOffset(2025, 12, 31, 8, 0, 0, TimeSpan.Zero),
                JobHoldUntilTime = new DateTimeOffset(2025, 12, 31, 9, 0, 0, TimeSpan.Zero),
                JobRetainUntil = JobHoldUntil.EndOfDay,
                JobRetainUntilInterval = 600,
                JobRetainUntilTime = new DateTimeOffset(2025, 12, 31, 18, 0, 0, TimeSpan.Zero),
                JobSheetMessage = "Strip message",
                OutputDevice = "output-device-1",
                PrintContentOptimize = PrintContentOptimize.Photo,
                JobPagesPerSet = 25,
                CoverBack = new Cover { Media = (Media)"iso_a4_210x297mm" },
                CoverFront = new Cover { Media = (Media)"iso_a4_210x297mm" },
                ForceFrontSide = new[] { 1, 2, 3 },
                ImageOrientation = Orientation.Portrait,
                InsertSheet = new[] { new InsertSheet { Media = (Media)"iso_a4_210x297mm" } },
                JobAccountingSheets = new JobAccountingSheets { JobAccountingOutputBin = OutputBin.Top, JobAccountingSheetsType = JobSheetsType.Standard },
                JobCompleteBefore = JobHoldUntil.DayTime,
                JobCompleteBeforeTime = new DateTimeOffset(2025, 12, 31, 10, 0, 0, TimeSpan.Zero),
                JobErrorSheet = new JobErrorSheet { JobErrorSheetType = JobSheetsType.JobEndSheet, JobErrorSheetWhen = JobErrorSheetWhen.OnError },
                JobMessageToOperator = "Please check output",
                JobPhoneNumber = "+1234567890",
                JobRecipientName = "Recipient Name",
                MediaInputTrayCheck = (MediaInputTrayCheck)"tray-1",
                PageDelivery = PageDelivery.SameOrderFaceUp,
                PresentationDirectionNumberUp = PresentationDirectionNumberUp.TorightTobottom,
                SeparatorSheets = new SeparatorSheets { Media = (Media)"iso_a4_210x297mm", SeparatorSheetsType = new[] { SeparatorSheetsType.StartSheet } },
                XImagePosition = XImagePosition.Center,
                XImageShift = 1,
                XSide1ImageShift = 2,
                XSide2ImageShift = 3,
                YImagePosition = YImagePosition.Center,
                YImageShift = 4,
                YSide1ImageShift = 5,
                YSide2ImageShift = 6
            }
        };
        IIppRequest? serverRequest = null;
        CreateJobResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new CreateJobResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["detail1"],
                    DocumentAccessError = "none"
                },
                JobAttributes = new()
                {
                    JobState = JobState.Pending,
                    JobStateReasons = [JobStateReason.None],
                    JobStateMessage = "pending",
                    NumberOfInterveningJobs = 0,
                    JobId = 456,
                    JobUri = "http://127.0.0.1:631/456",
                    ClientInfo = [new ClientInfo { ClientName = "SharpIppTests", ClientType = ClientType.Application }],
                    JobImpressionsCompletedCol = new JobCounter { Monochrome = 5 },
                    JobMediaSheetsCompletedCol = new JobCounter { Monochrome = 4 },
                    JobPagesCompleted = 3,
                    JobPagesCompletedCol = new JobCounter { Monochrome = 3 },
                    JobProcessingTime = 120
                }
            };
            var responseStream = new MemoryStream();
            await server.SendResponseAsync(serverResponse, responseStream, c);
            responseStream.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = statusCode, Content = new StreamContent(responseStream) };
        }
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));

        CreateJobResponse? clientResponse = await client.CreateJobAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}