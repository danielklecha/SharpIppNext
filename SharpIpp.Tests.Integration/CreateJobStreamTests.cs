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
                Finishings = new[] { Finishings.None },
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