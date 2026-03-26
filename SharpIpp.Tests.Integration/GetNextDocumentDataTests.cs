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
public class GetNextDocumentDataTests : SharpIppIntegrationTestBase
{
    [TestMethod]
    public async Task GetNextDocumentDataAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        var clientRequest = new GetNextDocumentDataRequest
        {
            RequestId = 812,
            Version = new IppVersion(2, 0),
            OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), JobId = 21, DocumentDataWait = true }
        };

        IIppRequest? serverRequest = null;
        GetNextDocumentDataResponse? serverResponse = null;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer();
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new GetNextDocumentDataResponse { RequestId = serverRequest.RequestId, Version = serverRequest.Version, StatusCode = IppStatusCode.SuccessfulOk };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        var clientResponse = await client.GetNextDocumentDataAsync(clientRequest);
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}