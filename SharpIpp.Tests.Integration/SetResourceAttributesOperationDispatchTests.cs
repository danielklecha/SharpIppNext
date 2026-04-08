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
public class SetResourceAttributesOperationDispatchTests : SharpIppIntegrationTestBase
{
    [TestMethod]
    public async Task ReceiveRequestAsync_SetResourceAttributes_ServerReceivesSameRequest()
    {
        var operationAttributes = GetSystemOperationAttributes<SetResourceAttributesOperationAttributes>();
        operationAttributes.ResourceId = 501;
        operationAttributes.ResourceName = "firmware-v2";
        operationAttributes.ResourceInfo = "firmware payload metadata";
        operationAttributes.ResourceNaturalLanguage = "en";
        operationAttributes.ResourcePatches = "none";
        operationAttributes.ResourceStringVersion = "2.0.1";
        operationAttributes.ResourceType = "firmware";
        operationAttributes.ResourceVersion = "2.0.1";

        var clientRequest = new SetResourceAttributesRequest
        {
            RequestId = 1205,
            Version = new IppVersion(2, 0),
            OperationAttributes = operationAttributes
        };

        IIppRequest? serverRequest = null;
        IIppResponse? serverResponse = null;

        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer();
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new SetResourceAttributesResponse
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
        var clientResponse = await client.SetResourceAttributesAsync(clientRequest);

        serverRequest.Should().BeEquivalentTo(clientRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}
