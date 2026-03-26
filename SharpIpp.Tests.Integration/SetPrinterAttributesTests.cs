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
public class SetPrinterAttributesTests : SharpIppIntegrationTestBase
{
    [TestMethod()]
    public async Task SetPrinterAttributesAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        var clientRequest = new SetPrinterAttributesRequest
        {
            RequestId = 794,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631")
            }
        };
        SetPrinterAttributesResponse? serverResponse = null;
        IIppRequest? serverRequest = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        var server = new SharpIppServer();
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new SetPrinterAttributesResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["set-printer-attributes detail"],
                }
            };
            var memoryStream = new MemoryStream();
            await server.SendResponseAsync(serverResponse, memoryStream, c);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StreamContent(memoryStream)
            };
        }
        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));

        SetPrinterAttributesResponse? clientResponse = await client.SetPrinterAttributesAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}