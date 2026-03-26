using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;
using System.Diagnostics.CodeAnalysis;

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
        var client = new SharpIppClient();
        var server = new SharpIppServer();

        var clientRawRequest = client.CreateRawRequest(clientRequest);
        var serverRequest = await server.ReceiveRequestAsync(clientRawRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
    }
}