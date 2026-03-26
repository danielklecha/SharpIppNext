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
public class ResubmitJobTests : SharpIppIntegrationTestBase
{
    [TestMethod()]
    public async Task ResubmitJobAsync_WhenSendingMessage_ServerReceivesSameRequest()
    {
        // Arrange
        var clientRequest = new ResubmitJobRequest
        {
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                JobId = 123,
                IppAttributeFidelity = true,
                JobMandatoryAttributes = new[] { "copies" },
                DocumentFormatDetails = new DocumentFormatDetails
                {
                    DocumentSourceApplicationName = "ResubmitClient",
                    DocumentSourceApplicationVersion = "1.0.0",
                    DocumentSourceOsName = "Windows",
                    DocumentSourceOsVersion = "11"
                }
            },
            JobTemplateAttributes = new()
            {
                Copies = 2
            }
        };
        var client = new SharpIppClient();
        var server = new SharpIppServer();

        // Act
        var clientRawRequest = client.CreateRawRequest(clientRequest);
        var serverRequest = await server.ReceiveRequestAsync(clientRawRequest);

        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
    }

    [TestMethod()]
    public async Task ResubmitJobAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        var clientRequest = new ResubmitJobRequest
        {
            RequestId = 791,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                JobId = 222,
                IppAttributeFidelity = true,
                JobMandatoryAttributes = ["copies"]
            },
            JobTemplateAttributes = new()
            {
                Copies = 3
            }
        };
        ResubmitJobResponse? serverResponse = null;
        IIppRequest? serverRequest = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        var server = new SharpIppServer();
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new ResubmitJobResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["resubmit detail"],
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

        // Act
        ResubmitJobResponse? clientResponse = await client.ResubmitJobAsync(clientRequest);

        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}