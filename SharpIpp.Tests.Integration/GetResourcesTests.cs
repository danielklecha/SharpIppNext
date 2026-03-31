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
public class GetResourcesTests : SharpIppIntegrationTestBase
{
    [TestMethod]
    public async Task GetResourcesAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        var clientRequest = new GetResourcesRequest
        {
            RequestId = 809,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                SystemUri = new Uri("ipp://127.0.0.1:8631/system"),
                RequestedAttributes = new[] { "resource-id", "resource-name" },
                ResourceIds = [1, 2, 3]
            }
        };

        IIppRequest? serverRequest = null;
        GetResourcesResponse? serverResponse = null;

        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer();
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new GetResourcesResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                ResourcesAttributes = new[]
                {
                    new ResourceDescriptionAttributes {
                        ResourceId = 100,
                        ResourceName = "Firmware",
                        ResourceFormat = "application/octet-stream",
                        ResourceFormats = new[] { "application/octet-stream", "application/zip" },
                        ResourceInfo = "Firmware image for device",
                        ResourceStates = new[] { ResourceState.Pending },
                        ResourceType = "firmware",
                        ResourceVersion = "1.0.0"
                    }
                }
            };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        var clientResponse = await client.GetResourcesAsync(clientRequest);
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}
