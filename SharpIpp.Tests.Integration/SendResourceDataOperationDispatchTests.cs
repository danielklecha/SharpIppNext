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
public class SendResourceDataOperationDispatchTests : SharpIppIntegrationTestBase
{
    [TestMethod]
    public async Task ReceiveRequestAsync_SendResourceData_ServerReceivesSameRequest()
    {
        var operationAttributes = GetSystemOperationAttributes<SendResourceDataOperationAttributes>();
        operationAttributes.ResourceId = 101;
        operationAttributes.ResourceKOctets = 2048;
        operationAttributes.ResourceSignature =
        [
            [0x01, 0x02, 0x03],
            [0xAA, 0xBB, 0xCC, 0xDD]
        ];

        var clientRequest = new SendResourceDataRequest
        {
            RequestId = 1204,
            Version = new IppVersion(2, 0),
            OperationAttributes = operationAttributes
        };

        IIppRequest? serverRequest = null;
        IIppResponse? serverResponse = null;

        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer();
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new SendResourceDataResponse
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
        var clientResponse = await client.SendResourceDataAsync(clientRequest);

        serverRequest.Should().BeEquivalentTo(clientRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}
