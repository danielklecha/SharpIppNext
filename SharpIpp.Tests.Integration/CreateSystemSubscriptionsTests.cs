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
public class CreateSystemSubscriptionsTests : SharpIppIntegrationTestBase
{
    // CreateSystemSubscriptions requires a subscription-attributes-group (RFC 3995 / PWG 5100.22).
    // The typed CreateSystemSubscriptionsRequest has no property for subscription attributes, so
    // ValidateOperationSpecificRules is disabled on both client and server to allow the typed
    // method to be used end-to-end without manually constructing a raw request.
    private static readonly IppRequestMessageValidator NoOpSpecificRulesValidator =
        new() { ValidateOperationSpecificRules = false };

    [TestMethod]
    public async Task CreateSystemSubscriptionsAsync_WhenSendingRequestWithAllProperties_ServerReceivesSameRequest()
    {
        var clientRequest = new CreateSystemSubscriptionsRequest
        {
            RequestId = 907,
            Version = new IppVersion(2, 0),
            OperationAttributes = new SystemOperationAttributes
            {
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en",
                PrinterUri = new Uri("ipp://127.0.0.1:631/printer"),
                SystemUri = new Uri("ipp://127.0.0.1:8631/system"),
                RequestingUserName = "test-user",
                RequestingUserUri = new Uri("mailto:test-user@example.com"),
                PrinterId = 99,
                NotifyPrinterIds = [99, 100],
                NotifyResourceId = 42,
                RestartGetInterval = 30,
                WhichPrinters = WhichPrinters.All,
                NotifySystemUpTime = 9999,
                NotifySystemUri = new Uri("ipp://127.0.0.1:8631/system/notify"),
                NotifySubscriptionId = 7,
                NotifyPullMethod = "ippget",
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
            var serverResponse = new CreateSystemSubscriptionsResponse
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

        await client.CreateSystemSubscriptionsAsync(clientRequest);

        serverRequest.Should().NotBeNull();
        serverRequest!.RequestId.Should().Be(clientRequest.RequestId);
        serverRequest.Version.Should().Be(clientRequest.Version);
        serverRequest.Should().BeEquivalentTo(clientRequest,
            options => options
                .Excluding(x => x.Path.StartsWith("OperationAttributes.AttributesCharset"))
                .Excluding(x => x.Path.StartsWith("OperationAttributes.AttributesNaturalLanguage")));
    }

    [TestMethod]
    public async Task CreateSystemSubscriptionsAsync_WhenSendingMinimalRequest_ServerReceivesSameRequest()
    {
        var clientRequest = new CreateSystemSubscriptionsRequest
        {
            RequestId = 908,
            Version = new IppVersion(1, 1),
            OperationAttributes = new SystemOperationAttributes
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/printer"),
                SystemUri = new Uri("ipp://127.0.0.1:8631/system"),
                RequestingUserName = "minimal-user"
            }
        };

        IIppRequest? serverRequest = null;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer(new IppProtocol(), NoOpSpecificRulesValidator);
            serverRequest = await server.ReceiveRequestAsync(s, c);
            var serverResponse = new CreateSystemSubscriptionsResponse
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

        await client.CreateSystemSubscriptionsAsync(clientRequest);

        serverRequest.Should().NotBeNull();
        serverRequest!.RequestId.Should().Be(clientRequest.RequestId);
        serverRequest.Version.Should().Be(clientRequest.Version);
    }

    [TestMethod]
    public async Task CreateSystemSubscriptionsAsync_WhenSendingRequestWithMultipleOperationAttributes_ServerReceivesAllAttributes()
    {
        var clientRequest = new CreateSystemSubscriptionsRequest
        {
            RequestId = 909,
            Version = new IppVersion(2, 0),
            OperationAttributes = new SystemOperationAttributes
            {
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en",
                PrinterUri = new Uri("ipp://127.0.0.1:631/printer"),
                SystemUri = new Uri("ipp://127.0.0.1:8631/system"),
                RequestingUserName = "test-user",
                RequestingUserUri = new Uri("mailto:test-user@example.com"),
                NotifySystemUri = new Uri("ipp://127.0.0.1:8631/system/notify"),
                NotifySystemUpTime = 12345,
                NotifyPullMethod = "ippget",
                NotifySubscriptionId = 3,
                RestartGetInterval = 60,
                WhichPrinters = WhichPrinters.All,
                NotifyPrinterIds = [1, 2, 3],
                NotifyResourceId = 10,
                PrinterId = 1
            }
        };

        IIppRequest? serverRequest = null;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer(new IppProtocol(), NoOpSpecificRulesValidator);
            serverRequest = await server.ReceiveRequestAsync(s, c);
            var serverResponse = new CreateSystemSubscriptionsResponse
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

        await client.CreateSystemSubscriptionsAsync(clientRequest);

        serverRequest.Should().NotBeNull();
        serverRequest!.RequestId.Should().Be(clientRequest.RequestId);
        serverRequest.Version.Should().Be(clientRequest.Version);
        serverRequest.Should().BeEquivalentTo(clientRequest,
            options => options
                .Excluding(x => x.Path.StartsWith("OperationAttributes.AttributesCharset"))
                .Excluding(x => x.Path.StartsWith("OperationAttributes.AttributesNaturalLanguage")));
    }

    [TestMethod]
    public async Task CreateSystemSubscriptionsAsync_WhenSendingRequestAndReceivingResponse_ResponseRoundTripsCorrectly()
    {
        var clientRequest = new CreateSystemSubscriptionsRequest
        {
            RequestId = 910,
            Version = new IppVersion(2, 0),
            OperationAttributes = new SystemOperationAttributes
            {
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en",
                PrinterUri = new Uri("ipp://127.0.0.1:631/printer"),
                SystemUri = new Uri("ipp://127.0.0.1:8631/system"),
                RequestingUserName = "test-user",
                RequestingUserUri = new Uri("mailto:test-user@example.com"),
                NotifySystemUri = new Uri("ipp://127.0.0.1:8631/system/notify"),
                NotifySystemUpTime = 9999,
                NotifyPullMethod = "ippget",
                NotifySubscriptionId = 7,
                RestartGetInterval = 30,
                WhichPrinters = WhichPrinters.All,
                NotifyPrinterIds = [99, 100],
                NotifyResourceId = 42,
                PrinterId = 99,
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
        CreateSystemSubscriptionsResponse? serverResponse = null;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer(new IppProtocol(), NoOpSpecificRulesValidator);
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new CreateSystemSubscriptionsResponse
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

        var clientResponse = await client.CreateSystemSubscriptionsAsync(clientRequest);

        serverRequest.Should().NotBeNull();
        serverRequest!.RequestId.Should().Be(clientRequest.RequestId);
        serverRequest.Version.Should().Be(clientRequest.Version);
        clientResponse.Should().NotBeNull();
        clientResponse.StatusCode.Should().Be(IppStatusCode.SuccessfulOk);
        clientResponse.RequestId.Should().Be(clientRequest.RequestId);
        clientResponse.Version.Should().Be(clientRequest.Version);
    }
}
