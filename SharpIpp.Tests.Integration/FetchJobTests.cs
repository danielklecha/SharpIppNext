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
public class FetchJobTests : SharpIppIntegrationTestBase
{
    [TestMethod]
    public async Task FetchJobAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        var clientRequest = new FetchJobRequest
        {
            RequestId = 807,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                JobId = 18,
                OutputDeviceUuid = new Uri("urn:uuid:123e4567-e89b-12d3-a456-426614174008")
            }
        };

        IIppRequest? serverRequest = null;
        FetchJobResponse? serverResponse = null;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer();
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new FetchJobResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new() { StatusMessage = "successful-ok" },
                JobAttributes = new JobDescriptionAttributes { JobId = 18, JobState = JobState.Pending }
            };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        var clientResponse = await client.FetchJobAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}
