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
public class InstallResourceOperationDispatchTests : SharpIppIntegrationTestBase
{
    [TestMethod]
    public async Task ReceiveRequestAsync_InstallResource_ServerReceivesSameRequest()
    {
        var clientRequest = new InstallResourceRequest
        {
            RequestId = 1203,
            Version = new IppVersion(2, 0),
            OperationAttributes = GetSystemOperationAttributes<SystemOperationAttributes>()
        };

        IIppRequest? serverRequest = null;
        IIppResponse? serverResponse = null;

        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer();
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new InstallResourceResponse
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

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        var rawRequest = client.CreateRawRequest(clientRequest);
        var rawResponse = await client.SendAsync(clientRequest.OperationAttributes!.SystemUri!, rawRequest);
        var clientResponse = client.CreateResponse<InstallResourceResponse>(rawResponse);

        serverRequest.Should().BeEquivalentTo(clientRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}
