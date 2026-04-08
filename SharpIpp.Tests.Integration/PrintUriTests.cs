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
public class PrintUriTests : SharpIppIntegrationTestBase
{
    [TestMethod()]
    public async Task PrintUriAsync_WhenSendingMessageWithDocumentAccess_ServerReceivesSameRequest()
    {
        // Arrange
        var clientRequest = new PrintUriRequest
        {
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                ResourceIds = [41, 42],
                DocumentUri = new Uri("https://example.local/document.pdf"),
                DocumentAccess = new DocumentAccess
                {
                    AccessOAuthToken = "oauth-token",
                    AccessOAuthUri = "https://example.local/oauth",
                    AccessPassword = "password",
                    AccessPin = "1234",
                    AccessUserName = "user",
                    AccessX509Certificate = "certificate"
                }
            },
            JobTemplateAttributes = new()
            {
                Copies = 1,
                MaterialsCol =
                [
                    new Material
                    {
                        MaterialAmount = 6,
                        MaterialColor = "red",
                        MaterialDiameter = 7,
                        MaterialFillDensity = 8,
                        MaterialKey = "abs-red",
                        MaterialName = "ABS Red",
                        MaterialPurpose = "support",
                        MaterialRate = 9,
                        MaterialRateUnits = "mm-per-second",
                        MaterialShellThickness = 10,
                        MaterialTemperature = 230,
                        MaterialType = "abs"
                    }
                ],
                PrintObjects =
                [
                    new PrintObject
                    {
                        DocumentNumber = 2,
                        PrintObjectsSource = "https://example.local/objects/2",
                        TransformationMatrix = [0, 1, 0, 1, 0, 0]
                    }
                ]
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
    public async Task PrintUriAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        SharpIppServer server = new();
        PrintUriRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                ResourceIds = [43, 44],
                DocumentUri = new Uri("ftp://document.pdf"),
                DocumentName = "???????????????????????.pdf",
                DocumentFormat = "application/pdf"
            },
            JobTemplateAttributes = new() { Copies = 1 }
        };
        IIppRequest? serverRequest = null;
        PrintUriResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new PrintUriResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new() { StatusMessage = "successful-ok", DetailedStatusMessage = ["detail1"], DocumentAccessError = "none" },
                JobAttributes = new() { JobId = 456 },
                DocumentAttributes = new() { DocumentNumber = 1 }
            };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = statusCode, Content = new StreamContent(ms) };
        }
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        PrintUriResponse? clientResponse = await client.PrintUriAsync(clientRequest);
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}