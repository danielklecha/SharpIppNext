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
public class GetDocumentAttributesTests : SharpIppIntegrationTestBase
{
    [TestMethod()]
    public async Task GetDocumentAttributesAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        SharpIppServer server = new();
        GetDocumentAttributesRequest clientRequest = new()
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
                DocumentNumber = 1,
                RequestedAttributes = ["document-number", "document-state", "document-name"],
            },
        };
        IIppRequest? serverRequest = null;
        GetDocumentAttributesResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new GetDocumentAttributesResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                DocumentAttributes = new DocumentAttributes
                {
                    DocumentNumber = 1,
                    DocumentState = DocumentState.Completed,
                    DocumentName = "Test Document",
                    DocumentJobId = 1,
                    DocumentPrinterUri = "http://127.0.0.1:631",
                    AttributesCharset = "utf-8",
                    AttributesNaturalLanguage = "en-us",
                    DocumentFormatDetails = new DocumentFormatDetails { DocumentSourceApplicationName = "MyApp", DocumentSourceOsName = "MyOS" },
                    DocumentFormatDetailsDetected = new DocumentFormatDetails { DocumentSourceApplicationName = "DetectedApp", DocumentSourceOsName = "DetectedOS" },
                    ErrorsCount = 0,
                    WarningsCount = 1,
                    PrintContentOptimizeActual = [PrintContentOptimize.Text],
                    DocumentStateReasons = [DocumentStateReason.None],
                    DocumentStateMessage = "completed",
                    PrintContentOptimize = PrintContentOptimize.Text,
                    DetailedStatusMessages = ["detail-message"],
                    DocumentAccessErrors = ["access-error"],
                    DocumentCharset = "utf-8",
                    DocumentFormat = "application/pdf",
                    DocumentFormatDetected = "application/pdf",
                    DocumentJobUri = "http://127.0.0.1:631/jobs/1",
                    DocumentMessage = "document-message",
                    DocumentNaturalLanguage = "en-us",
                    DocumentUri = "http://127.0.0.1:631/jobs/1/documents/1",
                    Impressions = 1,
                    ImpressionsCompleted = 1,
                    KOctets = 1,
                    KOctetsProcessed = 1,
                    LastDocument = true,
                    MediaSheets = 1,
                    MediaSheetsCompleted = 1,
                    MoreInfo = "http://127.0.0.1:631/more-info",
                    OutputDeviceAssigned = "printer",
                    PrinterUpTime = 100,
                    TimeAtCreation = 100,
                    TimeAtProcessing = 110,
                    TimeAtCompleted = 120,
                    DateTimeAtCreation = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    DateTimeAtProcessing = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    DateTimeAtCompleted = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
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

        GetDocumentAttributesResponse? clientResponse = await client.GetDocumentAttributesAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task GetDocumentAttributesAsync_WhenDocumentAttributesAreMissing_ReturnsNoValue()
    {
        SharpIppServer server = new();
        GetDocumentAttributesRequest clientRequest = new()
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
                DocumentNumber = 1,
            },
        };
        IIppRequest? serverRequest = null;
        GetDocumentAttributesResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new GetDocumentAttributesResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                DocumentAttributes = null,
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

        GetDocumentAttributesResponse? clientResponse = await client.GetDocumentAttributesAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.DocumentAttributes.Should().BeNull();
    }
}