using SharpIpp;
using SharpIpp.Exceptions;
using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;

namespace SharpIpp.Tests.Integration;

[TestClass]
[ExcludeFromCodeCoverage]
public class PrintJobTests : SharpIppIntegrationTestBase
{
    [TestMethod()]
    public async Task PrintJobAsync_WhenOverridePageRangeStartsBelowOne_ServerRejectsBadRequest()
    {
        using MemoryStream memoryStream = new();
        var clientRequest = new PrintJobRequest
        {
            Document = memoryStream,
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631")
            },
            JobTemplateAttributes = new()
            {
                Overrides =
                [
                    new OverrideInstruction
                    {
                        PageRanges = [new SharpIpp.Protocol.Models.Range(0, 1)],
                        JobTemplateAttributes = new JobTemplateAttributes
                        {
                            Sides = Sides.OneSided
                        }
                    }
                ]
            }
        };

        var client = new SharpIppClient();
        var server = new SharpIppServer();
        var clientRawRequest = client.CreateRawRequest(clientRequest);

        Func<Task> act = async () => await server.ReceiveRequestAsync(clientRawRequest);

        var exception = await act.Should().ThrowAsync<IppRequestException>();
        exception.Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
        exception.Which.Message.Should().Be("invalid overrides: 'pages' ranges must be within 1:MAX");
    }

    [TestMethod()]
    public async Task PrintJobAsync_WhenOverrideContainsDocumentScopeMember_ServerRejectsUnsupportedValues()
    {
        using MemoryStream memoryStream = new();
        var clientRequest = new PrintJobRequest
        {
            Document = memoryStream,
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631")
            },
            JobTemplateAttributes = new()
            {
                Overrides =
                [
                    new OverrideInstruction
                    {
                        PageRanges = [new SharpIpp.Protocol.Models.Range(1, 1)],
                        JobTemplateAttributes = new JobTemplateAttributes
                        {
                            Copies = 2
                        }
                    }
                ]
            }
        };

        var client = new SharpIppClient();
        var server = new SharpIppServer();
        var clientRawRequest = client.CreateRawRequest(clientRequest);

        Func<Task> act = async () => await server.ReceiveRequestAsync(clientRawRequest);

        var exception = await act.Should().ThrowAsync<IppRequestException>();
        exception.Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
        exception.Which.Message.Should().Be("invalid overrides: member(s) not supported by target printer: copies");
    }

    [TestMethod()]
    public async Task PrintJobAsync_WhenSendingMessage_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        using MemoryStream memoryStream = new();
        var clientRequest = new PrintJobRequest
        {
            Version = new IppVersion(2, 0),
            Document = memoryStream,
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                ResourceIds = [31, 32],
                DocumentName = "???????????????????????.pdf",
                DocumentFormat = "application/pdf",
                JobPassword = "hashed-secret",
                JobPasswordEncryption = JobPasswordEncryption.Sha2256,
                JobReleaseAction = JobReleaseAction.JobPassword,
                JobAuthorizationUri = new Uri("https://example.local/auth/abc"),
                JobImpressionsEstimated = 12,
                ChargeInfoMessage = "charge preview",
                ProofCopies = 1,
                JobStorage = new JobStorage { JobStorageAccess = (JobStorageAccess?)"owner", JobStorageDisposition = (JobStorageDisposition?)"store-only", JobStorageGroup = "default" },
                ProofPrint = new ProofPrint { ProofPrintCopies = 1, Media = (Media)"iso_a4_210x297mm" },
                CoverSheetInfo = new CoverSheetInfo { FromName = "sender", ToName = "receiver", Subject = "subject" },
                DestinationUris = [new DestinationUri { DestinationUriValue = "tel:+123456789", PostDialString = "#", PreDialString = "9", T33Subaddress = 12345 }],
                OutputAttributes = new OutputAttributes { NoiseRemoval = 100, OutputCompressionQualityFactor = 80 }
            },
            JobTemplateAttributes = new()
            {
                Copies = 1,
                PrintRenderingIntent = PrintRenderingIntent.Relative,
                JobErrorAction = JobErrorAction.ContinueJob,
                JobAccountType = JobAccountType.General,
                ConfirmationSheetPrint = true,
                NumberOfRetries = 2,
                MaterialsCol =
                [
                    new Material
                    {
                        MaterialAmount = 1,
                        MaterialColor = (MaterialColor?)"blue",
                        MaterialDiameter = 2,
                        MaterialFillDensity = 3,
                        MaterialKey = (MaterialKey?)"pla-blue",
                        MaterialName = "PLA Blue",
                        MaterialPurpose = [(MaterialPurpose)"model"],
                        MaterialRate = 4,
                        MaterialRateUnits = (MaterialRateUnits?)"mm-per-second",
                        MaterialShellThickness = 5,
                        MaterialTemperature = 200,
                        MaterialType = (MaterialType?)"pla"
                    }
                ],
                PrintObjects =
                [
                    new PrintObject
                    {
                        DocumentNumber = 1,
                        PrintObjectsSource = "https://example.local/objects/1",
                        TransformationMatrix = [1, 0, 0, 0, 1, 0]
                    }
                ],
                Overrides =
                [
                    new OverrideInstruction
                    {
                        PageRanges = [new SharpIpp.Protocol.Models.Range(1, 2)],
                        DocumentNumberRanges = [new SharpIpp.Protocol.Models.Range(1, 1)],
                        DocumentCopyRanges = [new SharpIpp.Protocol.Models.Range(1, 1)],
                        JobTemplateAttributes = new JobTemplateAttributes
                        {
                            Media = (Media)"iso_a4_210x297mm",
                            Sides = Sides.OneSided
                        }
                    }
                ]
            }
        };
        var client = new SharpIppClient();
        var server = new SharpIppServer();

        // Act
        var clientRawRequest = client.CreateRawRequest(clientRequest);
        var serverRequest = await server.ReceiveRequestAsync(clientRawRequest);

        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
    }

    [TestMethod()]
    public async Task PrintJobAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // ...moved from SharpIppIntegrationTests.cs
        using MemoryStream memoryStream = new();
        SharpIppServer server = new();
        PrintJobRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            Document = memoryStream,
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                ResourceIds = [33, 34],
                DocumentName = "???????????????????????.pdf",
                DocumentFormat = "application/pdf",
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
                JobName = "Test Job",
                IppAttributeFidelity = true,
                JobKOctets = 12,
                JobImpressions = 5,
                JobMediaSheets = 2,
                Compression = Compression.None,
                DocumentNaturalLanguage = "en",
                DocumentCharset = "utf-8",
            },
            JobTemplateAttributes = new() { Copies = 1, JobPriority = 1 }
        };
        IIppRequest? serverRequest = null;
        PrintJobResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new PrintJobResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new() { StatusMessage = "successful-ok", DetailedStatusMessage = ["detail1"], DocumentAccessError = "none" },
                JobAttributes = new() { JobId = 456 },
                DocumentAttributes = new() { DocumentNumber = 1 }
            };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = statusCode, Content = new StreamContent(ms) };
        }
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        PrintJobResponse? clientResponse = await client.PrintJobAsync(clientRequest);
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task PrintJobAsync_LongWayWhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        using MemoryStream memoryStream = new();
        SharpIppServer server = new();
        PrintJobRequest clientRequest = new() { RequestId = 123, Version = new IppVersion(2, 0), Document = memoryStream, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631") }, JobTemplateAttributes = new() { Copies = 1 } };
        IIppRequest? serverRequest = null;
        PrintJobResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new PrintJobResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk
            };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = statusCode, Content = new StreamContent(ms) };
        }
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        var clientRawRequest = client.CreateRawRequest(clientRequest);
        var clientRawResponse = await client.SendAsync(clientRequest.OperationAttributes.PrinterUri, clientRawRequest).ConfigureAwait(false);
        var clientResponse = client.CreateResponse<PrintJobResponse>(clientRawResponse);
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task PrintJobAsync_TurnOffReadDocumentStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        using MemoryStream memoryStream = new(Encoding.ASCII.GetBytes("Lorem"));
        IppProtocol ippProtocol = new() { ReadDocumentStream = false };
        SharpIppServer server = new(ippProtocol);
        PrintJobRequest clientRequest = new() { RequestId = 123, Version = new IppVersion(2, 0), Document = memoryStream, OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631") }, JobTemplateAttributes = new() };
        IIppRequest? serverRequest = null;
        PrintJobResponse? serverResponse = null;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new PrintJobResponse { RequestId = serverRequest.RequestId, Version = serverRequest.Version, StatusCode = IppStatusCode.SuccessfulOk };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object), ippProtocol);
        _ = await client.PrintJobAsync(clientRequest);
        serverRequest.As<PrintJobRequest>().Document.Should().BeSameAs(Stream.Null);
    }
}
