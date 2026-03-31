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
public class GetPrintersTests : SharpIppIntegrationTestBase
{
    [TestMethod]
    public async Task GetPrintersAsync_WhenSendingMessage_ServerReceivesSameRequest()
    {
        var clientRequest = new GetPrintersRequest
        {
            Version = new IppVersion(2, 0),
            OperationAttributes = new SystemOperationAttributes
            {
                SystemUri = new Uri("ipp://example.com/ipp/system"),
                PrinterId = 99,
                NotifyPrinterIds = new[] { 1, 2 },
                NotifyResourceId = 7,
                NotifySystemUpTime = 30,
                NotifySystemUri = new Uri("ipp://example.com/ipp/system/notify"),
                RestartGetInterval = 120,
                WhichPrinters = "all",
                RequestingUserName = "test-user",
                RequestingUserUri = new Uri("ipp://example.com/user/test"),
                ClientInfo = new[]
                {
                    new Protocol.Models.ClientInfo
                    {
                        ClientName = "MyClient",
                        ClientType = Protocol.Models.ClientType.Application,
                        ClientStringVersion = "1.2.3",
                        ClientVersion = "1.2.3",
                        ClientPatches = "patch-1"
                    }
                }
            }
        }; 

        var client = new SharpIppClient();
        var server = new SharpIppServer();

        var clientRawRequest = client.CreateRawRequest(clientRequest);
        var serverRequest = await server.ReceiveRequestAsync(clientRawRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
    }

    [TestMethod]
    public async Task GetPrintersAsync_WhenRoundtrip_ServerResponseMatchesClientResponse()
    {
        var clientRequest = new GetPrintersRequest
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new SystemOperationAttributes
            {
                SystemUri = new Uri("ipp://127.0.0.1:8631/system"),
                PrinterId = 99,
                NotifyPrinterIds = new[] { 1, 2 },
                NotifyResourceId = 7,
                NotifySystemUpTime = 30,
                NotifySystemUri = new Uri("ipp://127.0.0.1:8631/system/notify"),
                RestartGetInterval = 120,
                WhichPrinters = "all",
                RequestingUserName = "test-user",
                RequestingUserUri = new Uri("http://example.com/user/test"),
                ClientInfo = new[]
                {
                    new Protocol.Models.ClientInfo
                    {
                        ClientName = "MyClient",
                        ClientType = Protocol.Models.ClientType.Application,
                        ClientStringVersion = "1.2.3",
                        ClientVersion = "1.2.3",
                        ClientPatches = "patch-1"
                    }
                }
            }
        };

        IIppRequest? serverRequest = null;
        GetPrintersResponse? serverResponse = null;

        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var srv = new SharpIppServer();
            serverRequest = await srv.ReceiveRequestAsync(s, c);
            serverResponse = new GetPrintersResponse { Version = serverRequest.Version, RequestId = serverRequest.RequestId, StatusCode = IppStatusCode.SuccessfulOk };
            var ms = new MemoryStream();
            await srv.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        var clientResponse = await client.GetPrintersAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}
