using Moq;
using Moq.Protected;
using SharpIpp.Models.Requests;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace SharpIpp.Tests.Integration;

[ExcludeFromCodeCoverage]
public abstract class SharpIppIntegrationTestBase
{
    protected static T GetSystemOperationAttributes<T>() where T : SystemOperationAttributes, new()
    {
        return new T
        {
            AttributesCharset = "utf-8",
            AttributesNaturalLanguage = "en",
            PrinterUri = new Uri("ipp://127.0.0.1:8631/printer"),
            RequestingUserName = "integration-user",
            RequestingUserUri = new Uri("mailto:integration-user@example.com"),
            SystemUri = new Uri("ipp://127.0.0.1:8631/system"),
            PrinterId = 77,
            NotifyPrinterIds = [77, 78],
            NotifyResourceId = 1001,
            RestartGetInterval = 5,
            WhichPrinters = "all",
            NotifySystemUpTime = 12345,
            NotifySystemUri = new Uri("ipp://127.0.0.1:8631/system")
        };
    }

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