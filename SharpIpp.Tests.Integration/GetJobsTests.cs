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
                AttributesCharset = Charset.Utf8,
                AttributesNaturalLanguage = (NaturalLanguage)"en-us",
                RequestingUserName = "test-user",
                RequestedAttributes = new[] { "job-id", "job-uri", "job-state" },
                WhichJobs = WhichJobs.Completed,
                Limit = 10,
                JobIds = new[] { 1, 2 },
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
                    DetailedStatusMessage = "detail1",
                    DocumentAccessError = "none"
                },
                JobsAttributes =
                [
                    new JobDescriptionAttributes
                    {
                        JobId = 1,
                        JobUri = new Uri("http://127.0.0.1:631/jobs/1"),
                        JobPrinterUri = new Uri("http://127.0.0.1:631"),
                        JobName = "Test Job",
                        JobOriginatingUserName = "test-user",
                        JobKOctetsProcessed = 10,
                        JobImpressions = 5,
                        JobImpressionsCompleted = 0,
                        JobMediaSheets = 2,
                        JobMoreInfo = new Uri("more info", UriKind.RelativeOrAbsolute),
                        NumberOfDocuments = 1,
                        NumberOfInterveningJobs = 0,
                        OutputDeviceAssigned = "printer",
                        JobMediaSheetsCompleted = 0,
                        JobState = JobState.Pending,
                        JobStateMessage = "pending",
                        JobStateReasons = new[] { JobStateReason.None },
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
                        JobDetailedStatusMessages = new[] { "message" },
                        JobDocumentAccessErrors = new[] { "error" },
                        JobMessageFromOperator = "operator message",
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
                        YSide2ImageShiftActual = new[] { 0 }
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
