using SharpIpp;
using SharpIpp.Exceptions;
using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace SharpIpp.Tests.Integration;

[TestClass]
[ExcludeFromCodeCoverage]
public class ValidateDocumentTests : SharpIppIntegrationTestBase
{
    [TestMethod]
    public async Task ValidateDocumentAsync_WhenDocumentPasswordIsProvided_ServerRejectsRequest()
    {
        var clientRequest = new ValidateDocumentRequest
        {
            RequestId = 456,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                DocumentPassword = "secret",
            },
            DocumentTemplateAttributes = new()
            {
                Copies = 1,
            }
        };

        var client = new SharpIppClient();
        var server = new SharpIppServer();
        var clientRawRequest = client.CreateRawRequest(clientRequest);

        Func<Task> act = async () => await server.ReceiveRequestAsync(clientRawRequest);

        var exception = await act.Should().ThrowAsync<IppRequestException>();
        exception.Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
        exception.Which.Message.Should().Be("document-password is not allowed for validate operations");
    }

    [TestMethod]
    public async Task ValidateDocumentAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        var clientRequest = new ValidateDocumentRequest
        {
            RequestId = 789,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                RequestingUserName = "test-user",
                RequestingUserUri = new Uri("mailto:test-user@example.com"),
                DocumentFormat = "application/pdf",
                DocumentName = "doc.pdf",
                DocumentMetadata = ["meta-1", "meta-2"],
            },
            DocumentTemplateAttributes = new()
            {
                Copies = 2,
                PrintQuality = PrintQuality.High,
            }
        };

        ValidateDocumentResponse? serverResponse = null;
        IIppRequest? serverRequest = null;
        var server = new SharpIppServer();
        HttpStatusCode statusCode = HttpStatusCode.OK;

        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new ValidateDocumentResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["validate-document detail"],
                }
            };

            var responseStream = new MemoryStream();
            await server.SendResponseAsync(serverResponse, responseStream, c);
            responseStream.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = statusCode, Content = new StreamContent(responseStream) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));

        ValidateDocumentResponse? clientResponse = await client.ValidateDocumentAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}