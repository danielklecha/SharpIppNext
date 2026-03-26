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
public class CancelJobsTests : SharpIppIntegrationTestBase
{
    [TestMethod()]
    public async Task CancelJobsAsync_WhenSendingMessage_ServerReceivesSameRequest()
    {
        // Arrange
        var clientRequest = new CancelJobsRequest
        {
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                JobIds = new[] { 1, 2 },
                Message = "test"
            }
        };
        var client = new SharpIppClient();
        var server = new SharpIppServer();

        // Act
        var clientRawRequest = client.CreateRawRequest(clientRequest);
        var serverRequest = await server.ReceiveRequestAsync(clientRawRequest);

        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
    }

    [TestMethod()]
    public async Task CancelJobsAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        var clientRequest = new CancelJobsRequest
        {
            RequestId = 789,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                JobIds = new[] { 3, 4 },
                Message = "cancel test"
            }
        };
        CancelJobsResponse? serverResponse = null;
        IIppRequest? serverRequest = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        var server = new SharpIppServer();
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new CancelJobsResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["cancel detail"],
                }
            };
            var memoryStream = new MemoryStream();
            await server.SendResponseAsync(serverResponse, memoryStream, c);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StreamContent(memoryStream)
            };
        }
        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));

        // Act
        CancelJobsResponse? clientResponse = await client.CancelJobsAsync(clientRequest);

        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}