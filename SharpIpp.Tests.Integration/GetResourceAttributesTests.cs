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
public class GetResourceAttributesTests : SharpIppIntegrationTestBase
{
    [TestMethod]
    public async Task GetResourceAttributesAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        var clientRequest = new GetResourceAttributesRequest
        {
            RequestId = 810,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                SystemUri = new Uri("ipp://127.0.0.1:8631/system"),
                ResourceId = 100,
                RequestedAttributes = new[] { "resource-state", "resource-name" }
            }
        };

        IIppRequest? serverRequest = null;
        GetResourceAttributesResponse? serverResponse = null;

        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer();
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new GetResourceAttributesResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                ResourceAttributes = new ResourceStatusAttributes
                {
                    ResourceId = 100,
                    DateTimeAtCanceled = new DateTimeOffset(2026, 3, 29, 12, 0, 0, TimeSpan.Zero),
                    DateTimeAtCreation = new DateTimeOffset(2026, 3, 28, 12, 0, 0, TimeSpan.Zero),
                    DateTimeAtInstalled = new DateTimeOffset(2026, 3, 29, 10, 0, 0, TimeSpan.Zero),
                    ResourceState = ResourceState.Available,
                    ResourceStateMessage = "ready",
                    ResourceStateReasons = new[] { ResourceStateReason.None },
                    ResourceKOctets = 512,
                    ResourceNaturalLanguage = "en",
                    ResourcePatches = "patch-1\r\npatch-2",
                    ResourceSignature = new[] { new byte[] { 0x01, 0x02 }, new byte[] { 0x03, 0x04 } },
                    ResourceDataUri = new Uri("ipp://127.0.0.1:8631/resources/100"),
                    ResourceUseCount = 0,
                    TimeAtCanceled = 3600,
                    TimeAtCreation = 7200,
                    TimeAtInstalled = 10800,
                    ResourceUuid = new Uri("urn:uuid:12345678-1234-1234-1234-123456789abc"),
                    ResourceVersion = "1.0.0",
                    ResourceStringVersion = "1.0",
                    ResourceInfo = "Firmware image",
                    ResourceName = "Firmware",
                    ResourceFormat = "application/octet-stream",
                    ResourceFormats = new[] { "application/octet-stream", "application/x-firmware" },
                    ResourceType = "executable-firmware"
                }
            };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        var clientResponse = await client.GetResourceAttributesAsync(clientRequest);
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);

        // Additional coverage for ResourceStatusAttributes -> IppAttribute mapping branches
        clientResponse.ResourceAttributes!.ResourceUuid.Should().Be(new Uri("urn:uuid:12345678-1234-1234-1234-123456789abc"));
        clientResponse.ResourceAttributes.ResourceVersion.Should().Be("1.0.0");
        clientResponse.ResourceAttributes.ResourceStringVersion.Should().Be("1.0");
    }
}
