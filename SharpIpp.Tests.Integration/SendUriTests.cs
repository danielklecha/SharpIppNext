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
public class SendUriTests : SharpIppIntegrationTestBase
{
    [TestMethod()]
    public async Task SendUriAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        SharpIppServer server = new();
        SendUriRequest clientRequest = new()
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
                DocumentName = "test-document",
                Compression = Compression.None,
                DocumentFormat = "application/pdf",
                DocumentNaturalLanguage = "en",
                DocumentCharset = "utf-8",
                LastDocument = true,
                DocumentUri = new Uri("ftp://document.pdf"),
                DocumentAccess = new DocumentAccess
                {
                    AccessUserName = "ftp-user",
                    AccessPassword = "ftp-pass",
                    AccessOAuthUri = "https://auth.example.com/token",
                    AccessOAuthToken = "token",
                    AccessPin = "1234",
                    AccessX509Certificate = "-----BEGIN CERTIFICATE-----"
                },
            },
            DocumentTemplateAttributes = new() { Copies = 1 }
        };
        IIppRequest? serverRequest = null;
        SendUriResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new SendUriResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new() { StatusMessage = "successful-ok", DetailedStatusMessage = ["detail1"], DocumentAccessError = "none" },
                JobAttributes = new() { JobId = 456 },
                DocumentAttributes = new() { DocumentNumber = 1 }
            };
            var responseStream = new MemoryStream();
            await server.SendResponseAsync(serverResponse, responseStream, c);
            responseStream.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = statusCode, Content = new StreamContent(responseStream) };
        }
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));

        SendUriResponse? clientResponse = await client.SendUriAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}