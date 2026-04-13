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
public class UpdateJobStatusTests : SharpIppIntegrationTestBase
{
    [TestMethod]
    public async Task UpdateJobStatusAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        var clientRequest = new UpdateJobStatusRequest
        {
            RequestId = 809,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                JobId = 18,
                OutputDeviceUuid = new Uri("urn:uuid:123e4567-e89b-12d3-a456-42661417400A")
            },
            JobAttributes = new JobDescriptionAttributes
            {
                OutputDeviceJobState = JobState.Processing,
                OutputDeviceJobStateMessage = "processing"
            }
        };

        IIppRequest? serverRequest = null;
        UpdateJobStatusResponse? serverResponse = null;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer();
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new UpdateJobStatusResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new() { StatusMessage = "successful-ok" }
            };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        var clientResponse = await client.UpdateJobStatusAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}
