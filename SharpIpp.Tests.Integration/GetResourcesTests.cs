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
                        ResourceFormats = new ResourceFormat[] { "application/octet-stream", "application/zip" },
                        ResourceInfo = "Firmware image for device",
                        ResourceStates = new[] { ResourceState.Pending },
                        ResourceType = (ResourceType)"firmware",
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

    [TestMethod]
    public async Task GetResourcesAsync_WithAllExtendedProperties_RoundTripsCorrectly()
    {
        var clientRequest = new GetResourcesRequest
        {
            RequestId = 810,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                SystemUri = new Uri("ipp://127.0.0.1:8631/system"),
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
                    new ResourceDescriptionAttributes
                    {
                        ResourceId = 200,
                        ResourceName = "ICC Profile",
                        ResourceFormat = "application/vnd.iccprofile",
                        ResourceFormats = new ResourceFormat[] { "application/vnd.iccprofile" },
                        ResourceInfo = "sRGB color profile",
                        ResourceType = (ResourceType)"icc-profile",
                        ResourceVersion = "2.1.0",
                        ResourceStringVersion = "2.1.0-release",
                        ResourcePatches = "patch-1 patch-2",
                        ResourceNaturalLanguage = "en-us",
                        ResourceState = ResourceState.Available,
                        ResourceStates = new[] { ResourceState.Available },
                        ResourceStateReasons = new[] { ResourceStateReason.None },
                        ResourceStateMessage = "available for use",
                        ResourceKOctets = 512,
                        ResourceUseCount = 3,
                        ResourceDataUri = new Uri("ipp://127.0.0.1:8631/resources/200/data"),
                        ResourceUuid = new OctetString("urn:uuid:12345678-1234-1234-1234-123456789abc"),
                        ResourceSignature = new OctetString[] { new byte[] { 0x01, 0x02, 0x03 } },
                        DateTimeAtCreation = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
                        DateTimeAtInstalled = new DateTimeOffset(2024, 1, 2, 0, 0, 0, TimeSpan.Zero),
                        DateTimeAtCanceled = new DateTimeOffset(2024, 1, 3, 0, 0, 0, TimeSpan.Zero),
                        TimeAtCreation = 1000,
                        TimeAtInstalled = 2000,
                        TimeAtCanceled = 3000,
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
