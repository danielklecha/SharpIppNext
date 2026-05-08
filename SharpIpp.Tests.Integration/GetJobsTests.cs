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
public class GetJobsTests : SharpIppIntegrationTestBase
{
    [TestMethod()]
    public async Task GetJobsAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        SharpIppServer server = new();
        GetJobsRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
                RequestedAttributes = ["job-id", "job-uri", "job-state"],
                WhichJobs = WhichJobs.Completed,
                Limit = 10,
                JobIds = [1, 2],
                FirstIndex = 1,
                MyJobs = true,
            },
        };
        IIppRequest? serverRequest = null;
        GetJobsResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new GetJobsResponse
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
                JobsAttributes =
                [
                    new JobDescriptionAttributes
                    {
                        JobId = 1,
                        JobUri = "http://127.0.0.1:631/jobs/1",
                        JobPrinterUri = new Uri("http://127.0.0.1:631"),
                        JobName = "Test Job",
                        JobOriginatingUserName = "test-user",
                        JobKOctetsProcessed = 10,
                        JobImpressions = 5,
                        JobImpressionsCompleted = 0,
                        JobMediaSheets = 2,
                        JobMoreInfo = "more info",
                        NumberOfDocuments = 1,
                        NumberOfInterveningJobs = 0,
                        OutputDeviceAssigned = "printer",
                        JobMediaSheetsCompleted = 0,
                        JobState = JobState.Pending,
                        JobStateMessage = "pending",
                        JobStateReasons = [JobStateReason.None],
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
                        JobPrinterUpTime = 200,
                        JobKOctets = 20,
                        JobDetailedStatusMessages = ["message"],
                        JobDocumentAccessErrors = ["error"],
                        JobMessageFromOperator = "operator message",
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
                        YSide2ImageShiftActual = [0]
                    }
                ]
            };
            var responseStream = new MemoryStream();
            await server.SendResponseAsync(serverResponse, responseStream, c);
            responseStream.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = statusCode, Content = new StreamContent(responseStream) };
        }
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));

        GetJobsResponse? clientResponse = await client.GetJobsAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}
