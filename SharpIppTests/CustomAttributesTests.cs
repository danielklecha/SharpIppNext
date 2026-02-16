using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using SharpIpp;
using SharpIpp.Models;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CustomAttributesTests
    {
        private static Mock<HttpMessageHandler> GetMockOfHttpMessageHandler(Func<Stream, CancellationToken, Task<HttpResponseMessage>> func)
        {
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
                   using var stream = await request.Content.ReadAsStreamAsync(cancellationToken);
                   return await func.Invoke(stream, cancellationToken);
               });
            return handlerMock;
        }

        [TestMethod]
        public async Task CreateRawRequest_And_ReceiveRawRequestAsync_ShouldSupportCustomAttributes()
        {
            // Arrange
            SharpIppServer server = new();
            var clientRequest = new GetPrinterAttributesRequest
            {
                RequestId = 123,
                Version = new IppVersion(2, 0),
                OperationAttributes = new()
                {
                    PrinterUri = new Uri("http://127.0.0.1:631"),
                    AttributesCharset = "utf-8",
                    AttributesNaturalLanguage = "en-us",
                    RequestedAttributes = ["printer-uri", "printer-state", "printer-name"],
                }
            };

            IIppRequestMessage? serverRequest = null;
            GetPrinterAttributesResponse? serverResponse = null;
            HttpStatusCode statusCode = HttpStatusCode.OK;

            async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
            {
                // Server side receives RAW request
                serverRequest = await server.ReceiveRawRequestAsync(s, c);
                
                // Just return dummy response to satisfy the client call
                serverResponse = new GetPrinterAttributesResponse
                {
                    RequestId = serverRequest.RequestId,
                    Version = serverRequest.Version,
                    StatusCode = IppStatusCode.SuccessfulOk,
                    PrinterState = PrinterState.Idle,
                    PrinterStateReasons = ["none"],
                };
                var memoryStream = new MemoryStream();
                await server.SendResponseAsync(serverResponse, memoryStream, c);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return new HttpResponseMessage()
                {
                    StatusCode = statusCode,
                    Content = new StreamContent(memoryStream)
                };
            }

            using var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));

            // Act
            // 1. Create Raw Request from Typed Request
            var rawRequest = client.CreateRawRequest(clientRequest);
            
            // 2. Add Custom Attributes manually
            rawRequest.OperationAttributes.Add(new IppAttribute(Tag.Keyword, "custom-op-attr", "custom-value"));
            
            // 3. Send Raw Request using SendAsync (which accepts IIppRequestMessage)
            var response = await client.SendAsync(clientRequest.OperationAttributes.PrinterUri, rawRequest);

            // Assert
            serverRequest.Should().NotBeNull();
            serverRequest!.OperationAttributes.Should().Contain(x => x.Name == "custom-op-attr" && (string)x.Value == "custom-value");
        }
    }
}
