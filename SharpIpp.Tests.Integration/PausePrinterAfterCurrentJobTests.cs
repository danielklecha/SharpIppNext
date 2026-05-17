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
public class PausePrinterAfterCurrentJobTests : SharpIppIntegrationTestBase
{
    [TestMethod]
    public async Task PausePrinterAfterCurrentJobAsync_RoundTrip_ShouldSucceed()
    {
        var clientRequest = new PausePrinterAfterCurrentJobRequest
        {
            RequestId = 7,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("ipp://127.0.0.1:631/"),
                PrinterMessageFromOperator = "Pausing after current job"
            }
        };

        IIppRequest? serverRequest = null;
        PausePrinterAfterCurrentJobResponse? serverResponse = null;
        async Task<HttpResponseMessage> handler(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer();
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new PausePrinterAfterCurrentJobResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
            };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(handler).Object));
        var clientResponse = await client.PausePrinterAfterCurrentJobAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}
