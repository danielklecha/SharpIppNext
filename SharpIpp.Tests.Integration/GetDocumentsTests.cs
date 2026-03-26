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
public class GetDocumentsTests : SharpIppIntegrationTestBase
{
    [TestMethod()]
    public async Task GetDocumentsAsync_WhenSendingRequest_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        SharpIppServer server = new();
        GetDocumentsRequest clientRequest = new()
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
                Limit = 10,
                RequestedAttributes = ["document-name", "document-state"]
            },
        };
        IIppRequest? serverRequest = null;
        GetDocumentsResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new GetDocumentsResponse
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
                Documents = [new DocumentAttributes { DocumentNumber = 1, DocumentName = "doc1", DocumentState = DocumentState.Completed }]
            };
            var responseStream = new MemoryStream();
            await server.SendResponseAsync(serverResponse, responseStream, c);
            responseStream.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = statusCode, Content = new StreamContent(responseStream) };
        }
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));

        GetDocumentsResponse? clientResponse = await client.GetDocumentsAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task GetDocumentsAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        SharpIppServer server = new();
        GetDocumentsRequest clientRequest = new()
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
                RequestedAttributes = ["document-number", "document-state", "document-name"],
                Limit = 10,
            },
        };
        IIppRequest? serverRequest = null;
        GetDocumentsResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new GetDocumentsResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                Documents =
                [
                    new DocumentAttributes
                    {
                        DocumentNumber = 1,
                        DocumentState = DocumentState.Completed,
                        DocumentName = "Test Document 1",
                        DocumentJobId = 1,
                        DocumentPrinterUri = "http://127.0.0.1:631",
                        AttributesCharset = "utf-8",
                        AttributesNaturalLanguage = "en-us",
                        DocumentStateReasons = [DocumentStateReason.None],
                        DocumentStateMessage = "completed",
                    },
                    new DocumentAttributes
                    {
                        DocumentNumber = 2,
                        DocumentState = DocumentState.Processing,
                        DocumentName = "Test Document 2",
                        DocumentJobId = 1,
                        DocumentPrinterUri = "http://127.0.0.1:631",
                        AttributesCharset = "utf-8",
                        AttributesNaturalLanguage = "en-us",
                        DocumentStateReasons = [DocumentStateReason.None],
                        DocumentStateMessage = "processing",
                    }
                ],
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

        GetDocumentsResponse? clientResponse = await client.GetDocumentsAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}