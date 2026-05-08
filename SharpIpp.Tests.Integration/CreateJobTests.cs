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
public class CreateJobTests : SharpIppIntegrationTestBase
{
    [TestMethod()]
    public async Task CreateJobAsync_WhenUsingPwg51007OperationAttributes_ServerReceivesSameRequest()
    {
        var clientRequest = new CreateJobRequest
        {
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                ResourceIds = [11, 12],
                JobMandatoryAttributes = new[] { "copies" },
                ClientInfo =
                [
                    new ClientInfo
                    {
                        ClientName = "MyClient",
                        ClientPatches = "Patch-A",
                        ClientStringVersion = "1.2.3",
                        ClientVersion = "010203",
                        ClientType = ClientType.Application
                    }
                ],
                DocumentFormatDetails = new DocumentFormatDetails
                {
                    DocumentSourceApplicationName = "MyApp",
                    DocumentSourceApplicationVersion = "2.0.0",
                    DocumentSourceOsName = "MyOS",
                    DocumentSourceOsVersion = "12.1"
                },
                DestinationUris =
                [
                    new DestinationUri { DestinationUriValue = "https://example.test/upload" }
                ],
                DestinationAccesses =
                [
                    new DocumentAccess { AccessUserName = "scan-user", AccessPassword = "secret" }
                ],
                OutputAttributes = new OutputAttributes
                {
                    NoiseRemoval = 60,
                    OutputCompressionQualityFactor = 85
                }
            },
            JobTemplateAttributes = new()
            {
                JobSheetsCol = new JobSheetsCol
                {
                    JobSheets = JobSheets.Standard,
                    Media = (Media)"iso_a4_210x297mm",
                    MediaCol = new MediaCol { MediaColor = (MediaColor)"blue" }
                }
            }
        };

        IIppRequest? serverRequest = null;
        CreateJobResponse? serverResponse = null;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer();
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new CreateJobResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk
            };

            var responseStream = new MemoryStream();
            await server.SendResponseAsync(serverResponse, responseStream, c);
            responseStream.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(responseStream) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        var clientResponse = await client.CreateJobAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}
