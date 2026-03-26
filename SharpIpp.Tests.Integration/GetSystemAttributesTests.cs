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
public class GetSystemAttributesTests : SharpIppIntegrationTestBase
{
    [TestMethod]
    public async Task GetSystemAttributesAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        var clientRequest = new GetSystemAttributesRequest
        {
            RequestId = 807,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                SystemUri = new Uri("ipp://127.0.0.1:8631/system"),
                RequestedAttributes = ["system-description"]
            }
        };

        IIppRequest? serverRequest = null;
        GetSystemAttributesResponse? serverResponse = null;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer();
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new GetSystemAttributesResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                SystemAttributes = new() { SystemState = PrinterState.Processing }
            };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        var clientResponse = await client.GetSystemAttributesAsync(clientRequest);
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod]
    public async Task CreateResponse_WhenMappingRawGetSystemAttributesResponse_ToSystemStatusAttributes_ReturnsExpectedProperties()
    {
        var clientRequest = new GetSystemAttributesRequest
        {
            RequestId = 807,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                SystemUri = new Uri("ipp://127.0.0.1:8631/system"),
                RequestedAttributes = ["system-state"]
            }
        };

        IIppRequest? serverRequest = null;
        GetSystemAttributesResponse? serverResponse = null;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer();
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new GetSystemAttributesResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                SystemAttributes = new() { SystemState = PrinterState.Stopped }
            };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        var rawRequest = client.CreateRawRequest(clientRequest);
        var rawResponse = await client.SendAsync(clientRequest.OperationAttributes.PrinterUri, rawRequest);
        var clientResponse = client.CreateResponse(typeof(SystemStatusAttributes), rawResponse);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeOfType<SystemStatusAttributes>();
        clientResponse.Should().BeEquivalentTo(serverResponse!.SystemAttributes);
    }
}