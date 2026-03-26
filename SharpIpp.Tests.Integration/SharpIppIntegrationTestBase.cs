using Moq;
using Moq.Protected;
using System.Net;

namespace SharpIpp.Tests.Integration;

public abstract class SharpIppIntegrationTestBase
{
    protected static Mock<HttpMessageHandler> GetMockOfHttpMessageHandler(Func<Stream, CancellationToken, Task<HttpResponseMessage>> func)
    {
        Mock<HttpMessageHandler> handlerMock = new(MockBehavior.Strict);
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .Returns(async (HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                if (request.Content == null)
                {
                    return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest };
                }

                using var stream = await request.Content.ReadAsStreamAsync(cancellationToken);
                return await func.Invoke(stream, cancellationToken);
            });

        return handlerMock;
    }
}