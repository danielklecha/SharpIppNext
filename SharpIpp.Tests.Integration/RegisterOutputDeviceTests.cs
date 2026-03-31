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
public class RegisterOutputDeviceTests : SharpIppIntegrationTestBase
{
    [TestMethod]
    public async Task RegisterOutputDeviceAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        var clientRequest = new RegisterOutputDeviceRequest
        {
            RequestId = 809,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                SystemUri = new Uri("ipp://127.0.0.1:8631/system"),
                OutputDeviceUuid = new Uri("urn:uuid:123e4567-e89b-12d3-a456-426614174999"),
                OutputDeviceX509Certificate = ["-----BEGIN CERTIFICATE-----"],
                OutputDeviceX509Request = ["-----BEGIN CERTIFICATE REQUEST-----"],
                PrinterServiceType = [(PrinterServiceType)"print-ws"],
                PrinterXriRequested = [new SystemXri { XriSecurity = "xri-security" }]
            }
        };

        IIppRequest? serverRequest = null;
        RegisterOutputDeviceResponse? serverResponse = null;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer();
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new RegisterOutputDeviceResponse { RequestId = serverRequest.RequestId, Version = serverRequest.Version, StatusCode = IppStatusCode.SuccessfulOk };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        var clientResponse = await client.RegisterOutputDeviceAsync(clientRequest);
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}