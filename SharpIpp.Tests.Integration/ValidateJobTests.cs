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
public class ValidateJobTests : SharpIppIntegrationTestBase
{
    [TestMethod()]
    public async Task ValidateJobAsync_WhenJobErrorSheetContainsMediaAndMediaCol_ServerRejectsRequest()
    {
        var clientRequest = new ValidateJobRequest
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631")
            },
            JobTemplateAttributes = new()
            {
                JobErrorSheet = new JobErrorSheet
                {
                    JobErrorSheetType = JobSheetsType.Standard,
                    JobErrorSheetWhen = JobErrorSheetWhen.OnError,
                    Media = (Media)"iso_a4_210x297mm",
                    MediaCol = new MediaCol
                    {
                        MediaColor = (MediaColor)"blue"
                    }
                }
            }
        };

        var client = new SharpIppClient();
        var server = new SharpIppServer();
        var clientRawRequest = client.CreateRawRequest(clientRequest);

        Func<Task> act = async () => await server.ReceiveRequestAsync(clientRawRequest);

        var exception = await act.Should().ThrowAsync<IppRequestException>();
        exception.Which.StatusCode.Should().Be(IppStatusCode.ClientErrorConflictingAttributes);
        exception.Which.Message.Should().Be("invalid job-error-sheet: 'media' and 'media-col' member attributes are mutually exclusive");
    }

    [TestMethod()]
    public async Task ValidateJobAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        SharpIppServer server = new();
        ValidateJobRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
                ClientInfo =
                [
                    new ClientInfo
                    {
                        ClientName = "SharpIpp Tests",
                        ClientType = ClientType.Application,
                    },
                ],
                DocumentFormatDetails = new DocumentFormatDetails
                {
                    DocumentSourceApplicationName = "SharpIpp",
                    DocumentSourceApplicationVersion = "1.0",
                    DocumentSourceOsName = "Windows",
                },
                JobMandatoryAttributes = ["copies", "media"],
                JobName = "Test Job",
                IppAttributeFidelity = true,
                JobImpressions = 5,
                JobMediaSheets = 2,
                JobKOctets = 100,
                DocumentCharset = "utf-8",
                JobPassword = "secret",
                JobPasswordEncryption = JobPasswordEncryption.Sha2256,
                JobReleaseAction = JobReleaseAction.JobPassword,
                JobAuthorizationUri = new Uri("urn:uuid:00000000-0000-0000-0000-000000000001"),
                JobImpressionsEstimated = 10,
                ChargeInfoMessage = "charge-info",
                ProofCopies = 1
            },
            JobTemplateAttributes = new() { Copies = 1 }
        };
        IIppRequest? serverRequest = null;
        ValidateJobResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new ValidateJobResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new() { StatusMessage = "successful-ok", DetailedStatusMessage = ["detail1"], DocumentAccessError = "none" },
            };
            var responseStream = new MemoryStream();
            await server.SendResponseAsync(serverResponse, responseStream, c);
            responseStream.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = statusCode, Content = new StreamContent(responseStream) };
        }
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));

        ValidateJobResponse? clientResponse = await client.ValidateJobAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}