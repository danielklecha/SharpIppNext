using Moq;
using Moq.Protected;
using SharpIpp;
using SharpIpp.Models;
using SharpIpp.Protocol.Models;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace SharpIpp.Tests;

[TestClass]
[ExcludeFromCodeCoverage]
public class SharpIppIntegrationTests
{
    [TestMethod()]
    public async Task PrintJobAsync_WhenSendingMessage_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        MemoryStream memoryStream = new();
        var clientRequest = new PrintJobRequest
        {
            Version = new IppVersion(2, 0),
            Document = memoryStream,
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                DocumentName = "のデフォルト値を保存するメソッドを呼び出します.pdf",
                DocumentFormat = "application/pdf"
            },
            JobTemplateAttributes = new()
            {
                Copies = 1
            }
        };
        var client = new SharpIppClient();
        var server = new SharpIppServer();
        // Act
        var clientRawRequest = client.CreateRawRequest(clientRequest);
        var serverRequest = (await server.ReceiveRequestAsync(clientRawRequest));
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
    }

    [TestMethod()]
    public async Task PrintJobAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        MemoryStream memoryStream = new();
        SharpIppServer server = new();
        PrintJobRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            Document = memoryStream,
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                DocumentName = "のデフォルト値を保存するメソッドを呼び出します.pdf",
                DocumentFormat = "application/pdf"
            },
            JobTemplateAttributes = new()
            {
                Copies = 1
            }
        };
        IIppRequest? serverRequest = null;
        PrintJobResponse? serverResponse = null;
        Stream stream = Stream.Null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        Mock<HttpMessageHandler> handlerMock = new(MockBehavior.Strict);
        handlerMock
           .Protected()
           .Setup<Task<HttpResponseMessage>>(
              "SendAsync",
              ItExpr.IsAny<HttpRequestMessage>(),
              ItExpr.IsAny<CancellationToken>()
           )
           .Returns(async (HttpRequestMessage request, CancellationToken cancellationToken) =>
           {
               if (request.Content == null)
                   return new HttpResponseMessage() { StatusCode = HttpStatusCode.BadRequest };
               stream = await request.Content.ReadAsStreamAsync(cancellationToken);
               serverRequest = (await server.ReceiveRequestAsync(stream, cancellationToken));
               serverResponse = new PrintJobResponse
               {
                   RequestId = serverRequest.RequestId,
                   Version = serverRequest.Version,
                   JobState = JobState.Pending,
                   StatusCode = IppStatusCode.SuccessfulOk,
                   JobStateReasons = [JobStateReason.None],
                   JobId = 456,
                   JobUri = "http://127.0.0.1:631/456"
               };
               var memoryStream = new MemoryStream();
               await server.SendResponseAsync(serverResponse, memoryStream, cancellationToken);
               memoryStream.Seek(0, SeekOrigin.Begin);
               return new HttpResponseMessage()
               {
                   StatusCode = statusCode,
                   Content = new StreamContent(memoryStream)
               };
           });
        SharpIppClient client = new(new(handlerMock.Object));
        // Act
        PrintJobResponse? clientResponse = await client.PrintJobAsync(clientRequest);
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse, options => options.Excluding(ctx => ctx!.Sections));
    }
}
