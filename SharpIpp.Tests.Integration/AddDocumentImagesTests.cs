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
public class AddDocumentImagesTests : SharpIppIntegrationTestBase
{
    [TestMethod]
    public async Task AddDocumentImagesAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        var clientRequest = new AddDocumentImagesRequest
        {
            RequestId = 811,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                JobId = 20,
                InputAttributes = new DocumentTemplateAttributes
                {
                    Media = (Media)"iso_a4_210x297mm",
                    InputAutoExposure = true,
                    InputAutoScaling = false,
                    InputAutoSkewCorrection = true,
                    InputBrightness = 50,
                    InputColorMode = "auto",
                    InputContentType = "image/jpeg",
                    InputContrast = 10,
                    InputFilmScanMode = "negative",
                    InputImagesToTransfer = 5,
                    InputOrientationRequested = Orientation.Landscape,
                    InputQuality = PrintQuality.High,
                    InputResolution = new Resolution(300, 300, ResolutionUnit.DotsPerInch),
                    InputScalingHeight = 200,
                    InputScalingWidth = 300,
                    InputSharpness = 25,
                    InputSides = Sides.TwoSidedLongEdge,
                    InputSource = "document-feeder"
                }
            }
        };

        IIppRequest? serverRequest = null;
        AddDocumentImagesResponse? serverResponse = null;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer();
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new AddDocumentImagesResponse { RequestId = serverRequest.RequestId, Version = serverRequest.Version, StatusCode = IppStatusCode.SuccessfulOk };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        var clientResponse = await client.AddDocumentImagesAsync(clientRequest);
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}