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
public class ScheduleJobAfterTests : SharpIppIntegrationTestBase
{
    [TestMethod]
    public async Task ScheduleJobAfterAsync_RoundTrip_ShouldSucceed()
    {
        var clientRequest = new ScheduleJobAfterRequest
        {
            RequestId = 23,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/"),
                JobId = 45,
                PredecessorJobId = 44,
                JobMessageFromOperator = "Scheduling job after 44"
            }
        };

        IIppRequest? serverRequest = null;
        ScheduleJobAfterResponse? serverResponse = null;
        async Task<HttpResponseMessage> handler(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer();
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new ScheduleJobAfterResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
            };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(handler).Object));
        var clientResponse = await client.ScheduleJobAfterAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}
