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
public class AllocatePrinterResourcesTests : SharpIppIntegrationTestBase
{
    [TestMethod()]
    public async Task AllocatePrinterResourcesAsync_WhenSendingMessage_ServerReceivesSameRequest()
    {
        var clientRequest = new AllocatePrinterResourcesRequest
        {
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                SystemUri = new Uri("ipp://example.com/ipp/system"),
                PrinterId = 42,
                ResourceIds = new[] { 10, 11 }
            }
        };
        var client = new SharpIppClient();
        var server = new SharpIppServer();

        var clientRawRequest = client.CreateRawRequest(clientRequest);
        var serverRequest = await server.ReceiveRequestAsync(clientRawRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
    }

    [TestMethod]
    public async Task AllocatePrinterResourcesAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        var clientRequest = new AllocatePrinterResourcesRequest
        {
            RequestId = 805,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                SystemUri = new Uri("ipp://127.0.0.1:8631/system"),
                PrinterId = 99,
                ResourceIds = [1, 2]
            }
        };

        IIppRequest? serverRequest = null;
        AllocatePrinterResourcesResponse? serverResponse = null;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer();
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new AllocatePrinterResourcesResponse { RequestId = serverRequest.RequestId, Version = serverRequest.Version, StatusCode = IppStatusCode.SuccessfulOk, PrinterResourceIds = [1, 2] };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        var clientResponse = await client.AllocatePrinterResourcesAsync(clientRequest);
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}