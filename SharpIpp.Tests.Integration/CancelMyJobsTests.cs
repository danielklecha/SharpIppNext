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
public class CancelMyJobsTests : SharpIppIntegrationTestBase
{
    [TestMethod()]
    public async Task CancelMyJobsAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        var clientRequest = new CancelMyJobsRequest
        {
            RequestId = 790,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                JobIds = [5, 6],
                Message = "cancel my jobs test"
            }
        };
        CancelMyJobsResponse? serverResponse = null;
        IIppRequest? serverRequest = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        var server = new SharpIppServer();
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new CancelMyJobsResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["cancel-my-jobs detail"],
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
        CancelMyJobsResponse? clientResponse = await client.CancelMyJobsAsync(clientRequest);

        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}