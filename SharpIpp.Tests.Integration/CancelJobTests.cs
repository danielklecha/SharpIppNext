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
public class CancelJobTests : SharpIppIntegrationTestBase
{
    [TestMethod()]
    public async Task CancelJobAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        SharpIppServer server = new();
        CancelJobRequest clientRequest = new()
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
                Message = "message",
            },
        };
        IIppRequest? serverRequest = null;
        CancelJobResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new CancelJobResponse
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
            };
            var responseStream = new MemoryStream();
            await server.SendResponseAsync(serverResponse, responseStream, c);
            responseStream.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = statusCode, Content = new StreamContent(responseStream) };
        }
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));

        CancelJobResponse? clientResponse = await client.CancelJobAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}