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
public class GetSubscriptionsTests : SharpIppIntegrationTestBase
{
    [TestMethod]
    public async Task GetSubscriptionsAsync_ServerRoundtripAsync()
    {
        var clientRequest = new GetSubscriptionsRequest
        {
            Version = new IppVersion(2,0),
            OperationAttributes = new SystemOperationAttributes { SystemUri = new Uri("ipp://127.0.0.1:8631/system") }
        };

        IIppRequest? serverRequest = null;
        GetSubscriptionsResponse? serverResponse = null;

        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var srv = new SharpIppServer();
            serverRequest = await srv.ReceiveRequestAsync(s, c);
            serverResponse = new GetSubscriptionsResponse
            {
                Version = serverRequest.Version,
                RequestId = serverRequest.RequestId,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new SharpIpp.Models.Responses.OperationAttributes { AttributesCharset = "utf-8", AttributesNaturalLanguage = "en" }
            };
            var ms = new MemoryStream();
            await srv.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        var clientResponse = await client.GetSubscriptionsAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Version.Should().Be(serverResponse!.Version);
        clientResponse.RequestId.Should().Be(serverResponse.RequestId);
        clientResponse.StatusCode.Should().Be(serverResponse.StatusCode);
    }
}
