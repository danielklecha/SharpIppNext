using SharpIpp;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace SharpIpp.Tests.Integration;

[TestClass]
[ExcludeFromCodeCoverage]
public class SendTests : SharpIppIntegrationTestBase
{
    [TestMethod()]
    public async Task SendAsync_AllSections_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        var printerUri = new Uri("http://127.0.0.1:631");

        using MemoryStream memoryStream = new();
        SharpIppServer server = new();

        IIppRequestMessage clientRawRequest = new IppRequestMessage()
        {
            IppOperation = (IppOperation)0x000A,
            RequestId = 123,
            Version = new IppVersion(2, 0),
            Document = memoryStream
        };
        clientRawRequest.OperationAttributes.AddRange([
            new IppAttribute(Tag.Charset, "attributes-charset", "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, "attributes-natural-language", "en"),
            new IppAttribute(Tag.NameWithoutLanguage, "requesting-user-name", "test"),
            new IppAttribute(Tag.Uri, "printer-uri", "ipp://localhost:631"),
            new IppAttribute(Tag.Integer, "job-id", 1)]);
        clientRawRequest.PrinterAttributes.Add(new IppAttribute(Tag.NameWithoutLanguage, "request", "test"));
        clientRawRequest.ResourceAttributes.Add(new IppAttribute(Tag.NameWithoutLanguage, "request", "test"));
        clientRawRequest.SubscriptionAttributes.Add(new IppAttribute(Tag.NameWithoutLanguage, "request", "test"));
        clientRawRequest.SystemAttributes.Add(new IppAttribute(Tag.NameWithoutLanguage, "request", "test"));
        clientRawRequest.UnsupportedAttributes.Add(new IppAttribute(Tag.NameWithoutLanguage, "request", "test"));
        clientRawRequest.DocumentAttributes.Add(new IppAttribute(Tag.NameWithoutLanguage, "request", "test"));
        clientRawRequest.EventNotificationAttributes.Add(new IppAttribute(Tag.NameWithoutLanguage, "request", "test"));
        clientRawRequest.JobAttributes.Add(new IppAttribute(Tag.NameWithoutLanguage, "request", "test"));
        IIppRequestMessage? serverRawRequest = null;
        IIppResponseMessage? serverRawResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRawRequest = await server.ReceiveRawRequestAsync(s, c);
            serverRawResponse = new IppResponseMessage
            {
                RequestId = serverRawRequest.RequestId,
                Version = serverRawRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk
            };
            serverRawResponse.OperationAttributes.Add([new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"), new IppAttribute(Tag.NameWithoutLanguage, "response", "test")]);
            serverRawResponse.PrinterAttributes.Add([new IppAttribute(Tag.NameWithoutLanguage, "response", "test")]);
            serverRawResponse.ResourceAttributes.Add([new IppAttribute(Tag.NameWithoutLanguage, "response", "test")]);
            serverRawResponse.SubscriptionAttributes.Add([new IppAttribute(Tag.NameWithoutLanguage, "response", "test")]);
            serverRawResponse.SystemAttributes.Add([new IppAttribute(Tag.NameWithoutLanguage, "response", "test")]);
            serverRawResponse.UnsupportedAttributes.Add([new IppAttribute(Tag.NameWithoutLanguage, "response", "test")]);
            serverRawResponse.DocumentAttributes.Add([new IppAttribute(Tag.NameWithoutLanguage, "response", "test")]);
            serverRawResponse.EventNotificationAttributes.Add([new IppAttribute(Tag.NameWithoutLanguage, "response", "test")]);
            serverRawResponse.JobAttributes.Add([new IppAttribute(Tag.NameWithoutLanguage, "response", "test")]);
            var memoryStream = new MemoryStream();
            await server.SendRawResponseAsync(serverRawResponse, memoryStream, c);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StreamContent(memoryStream)
            };
        }
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));

        IIppResponseMessage? clientRawResponse = await client.SendAsync(printerUri, clientRawRequest);

        clientRawRequest.Should().NotBeNull().And.BeEquivalentTo(serverRawRequest, options => options.Excluding(x => x!.Document));
        clientRawResponse.Should().BeEquivalentTo(serverRawResponse);
    }
}
