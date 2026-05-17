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
public class CreatePrinterSubscriptionsTests : SharpIppIntegrationTestBase
{
    // CreatePrinterSubscriptions requires a subscription-attributes-group (RFC 3995 Section 5.1).
    // The typed CreatePrinterSubscriptionsRequest has no property for subscription attributes, so
    // ValidateOperationSpecificRules is disabled on both client and server to allow the typed
    // method to be used end-to-end without manually constructing a raw request.
    private static readonly IppRequestValidator NoOpSpecificRulesValidator =
        new() { ValidateOperationSpecificRules = false };

    [TestMethod]
    public async Task CreatePrinterSubscriptionsAsync_WhenSendingRequestWithAllProperties_ServerReceivesSameRequest()
    {
        var clientRequest = new CreatePrinterSubscriptionsRequest
        {
            RequestId = 801,
            Version = new IppVersion(2, 0),
            OperationAttributes = new CreatePrinterSubscriptionsOperationAttributes
            {
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en",
                PrinterUri = new Uri("ipp://127.0.0.1:631/printer"),
                RequestingUserName = "test-user",
                RequestingUserUri = new Uri("mailto:test-user@example.com"),
                ClientInfo =
                [
                    new ClientInfo
                    {
                        ClientName = "test-client",
                        ClientVersion = "1.0"
                    }
                ],
                JobHoldUntilTime = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero)
            }
        };

        IIppRequest? serverRequest = null;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer(new IppProtocol(), NoOpSpecificRulesValidator);
            serverRequest = await server.ReceiveRequestAsync(s, c);
            var serverResponse = new CreatePrinterSubscriptionsResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk
            };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(
            new HttpClient(GetMockOfHttpMessageHandler(func).Object),
            new IppProtocol(),
            NoOpSpecificRulesValidator);

        await client.CreatePrinterSubscriptionsAsync(clientRequest);

        serverRequest.Should().NotBeNull();
        serverRequest!.RequestId.Should().Be(clientRequest.RequestId);
        serverRequest.Version.Should().Be(clientRequest.Version);
        serverRequest.Should().BeEquivalentTo(clientRequest,
            options => options
                .Excluding(x => x.Path.StartsWith("OperationAttributes.AttributesCharset"))
                .Excluding(x => x.Path.StartsWith("OperationAttributes.AttributesNaturalLanguage")));
    }

    [TestMethod]
    public async Task CreatePrinterSubscriptionsAsync_WhenSendingMinimalRequest_ServerReceivesSameRequest()
    {
        var clientRequest = new CreatePrinterSubscriptionsRequest
        {
            RequestId = 802,
            Version = new IppVersion(1, 1),
            OperationAttributes = new CreatePrinterSubscriptionsOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/printer"),
                RequestingUserName = "minimal-user"
            }
        };

        IIppRequest? serverRequest = null;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer(new IppProtocol(), NoOpSpecificRulesValidator);
            serverRequest = await server.ReceiveRequestAsync(s, c);
            var serverResponse = new CreatePrinterSubscriptionsResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk
            };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(
            new HttpClient(GetMockOfHttpMessageHandler(func).Object),
            new IppProtocol(),
            NoOpSpecificRulesValidator);

        await client.CreatePrinterSubscriptionsAsync(clientRequest);

        serverRequest.Should().NotBeNull();
        serverRequest!.RequestId.Should().Be(clientRequest.RequestId);
        serverRequest.Version.Should().Be(clientRequest.Version);
    }

    [TestMethod]
    public async Task CreatePrinterSubscriptionsAsync_WhenSendingRequestAndReceivingResponse_ResponseRoundTripsCorrectly()
    {
        var clientRequest = new CreatePrinterSubscriptionsRequest
        {
            RequestId = 803,
            Version = new IppVersion(2, 0),
            OperationAttributes = new CreatePrinterSubscriptionsOperationAttributes
            {
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en",
                PrinterUri = new Uri("ipp://127.0.0.1:631/printer"),
                RequestingUserName = "test-user",
                RequestingUserUri = new Uri("mailto:test-user@example.com"),
                ClientInfo =
                [
                    new ClientInfo
                    {
                        ClientName = "test-client",
                        ClientVersion = "1.0"
                    }
                ],
                JobHoldUntilTime = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero)
            }
        };

        IIppRequest? serverRequest = null;
        CreatePrinterSubscriptionsResponse? serverResponse = null;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer(new IppProtocol(), NoOpSpecificRulesValidator);
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new CreatePrinterSubscriptionsResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk
            };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(
            new HttpClient(GetMockOfHttpMessageHandler(func).Object),
            new IppProtocol(),
            NoOpSpecificRulesValidator);

        var clientResponse = await client.CreatePrinterSubscriptionsAsync(clientRequest);

        serverRequest.Should().NotBeNull();
        serverRequest!.RequestId.Should().Be(clientRequest.RequestId);
        serverRequest.Version.Should().Be(clientRequest.Version);
        clientResponse.Should().NotBeNull();
        clientResponse.StatusCode.Should().Be(IppStatusCode.SuccessfulOk);
        clientResponse.RequestId.Should().Be(clientRequest.RequestId);
        clientResponse.Version.Should().Be(clientRequest.Version);
    }
}
