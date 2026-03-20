using Moq;
using Moq.Protected;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace SharpIpp.Tests.Integration;

[TestClass]
[ExcludeFromCodeCoverage]
public class SharpIppIntegrationTests
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
                DocumentName = "のデフォルト値を保存するメソッドを呼び出します.pdf",
                DocumentFormat = "application/pdf",
                JobPassword = "hashed-secret",
                JobPasswordEncryption = JobPasswordEncryption.Sha2256,
                JobReleaseAction = JobReleaseAction.JobPassword,
                JobAuthorizationUri = new Uri("https://example.local/auth/abc"),
                JobImpressionsEstimated = 12,
                ChargeInfoMessage = "charge preview",
                ProofCopies = 1,
                JobStorage = new JobStorage { JobStorageAccess = "owner", JobStorageDisposition = "store-only", JobStorageGroup = "default" },
                ProofPrint = new ProofPrint { ProofPrintCopies = 1, Media = (Media)"iso_a4_210x297mm" },
                CoverSheetInfo = new CoverSheetInfo { FromName = "sender", ToName = "receiver", Subject = "subject" },
                DestinationUris = [new DestinationUri { DestinationUriValue = "tel:+123456789", PostDialString = "#", PreDialString = "9", T33Subaddress = "12345" }],
                OutputAttributes = new OutputAttributes { NoiseRemoval = true, OutputCompressionQualityFactor = 80 }
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
                        MaterialColor = "blue",
                        MaterialDiameter = 2,
                        MaterialFillDensity = 3,
                        MaterialKey = "pla-blue",
                        MaterialName = "PLA Blue",
                        MaterialPurpose = "model",
                        MaterialRate = 4,
                        MaterialRateUnits = "mm-per-second",
                        MaterialShellThickness = 5,
                        MaterialTemperature = 200,
                        MaterialType = "pla"
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
                Overrides = [new OverrideInstruction { Pages = "1-2", DocumentCopies = 1, DocumentNumbers = [1] }]
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
    public async Task PrintUriAsync_WhenSendingMessageWithDocumentAccess_ServerReceivesSameRequest()
    {
        // Arrange
        var clientRequest = new PrintUriRequest
        {
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                DocumentUri = new Uri("https://example.local/document.pdf"),
                DocumentAccess = new DocumentAccess
                {
                    AccessOAuthToken = "oauth-token",
                    AccessOAuthUri = "https://example.local/oauth",
                    AccessPassword = "password",
                    AccessPin = "1234",
                    AccessUserName = "user",
                    AccessX509Certificate = "certificate"
                }
            },
            JobTemplateAttributes = new()
            {
                Copies = 1,
                MaterialsCol =
                [
                    new Material
                    {
                        MaterialAmount = 6,
                        MaterialColor = "red",
                        MaterialDiameter = 7,
                        MaterialFillDensity = 8,
                        MaterialKey = "abs-red",
                        MaterialName = "ABS Red",
                        MaterialPurpose = "support",
                        MaterialRate = 9,
                        MaterialRateUnits = "mm-per-second",
                        MaterialShellThickness = 10,
                        MaterialTemperature = 230,
                        MaterialType = "abs"
                    }
                ],
                PrintObjects =
                [
                    new PrintObject
                    {
                        DocumentNumber = 2,
                        PrintObjectsSource = "https://example.local/objects/2",
                        TransformationMatrix = [0, 1, 0, 1, 0, 0]
                    }
                ]
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

    [TestMethod]
    public async Task IdentifyPrinterAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        var clientRequest = new IdentifyPrinterRequest
        {
            RequestId = 794,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                IdentifyActions = [IdentifyAction.Display, IdentifyAction.Sound],
                OutputDeviceUuid = new Uri("urn:uuid:123e4567-e89b-12d3-a456-426614174000"),
                JobId = 12
            }
        };
        IdentifyPrinterResponse? serverResponse = null;
        IIppRequest? serverRequest = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        var server = new SharpIppServer();
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new IdentifyPrinterResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["identify detail"],
                }
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
        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));

        // Act
        IdentifyPrinterResponse? clientResponse = await client.IdentifyPrinterAsync(clientRequest);

        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task CancelJobsAsync_WhenSendingMessage_ServerReceivesSameRequest()
    {
        // Arrange
        var clientRequest = new CancelJobsRequest
        {
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                JobIds = new[] { 1, 2 },
                Message = "test"
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
    public async Task CancelJobsAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        var clientRequest = new CancelJobsRequest
        {
            RequestId = 789,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                JobIds = new[] { 3, 4 },
                Message = "cancel test"
            }
        };
        CancelJobsResponse? serverResponse = null;
        IIppRequest? serverRequest = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        var server = new SharpIppServer();
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new CancelJobsResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["cancel detail"],
                }
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
        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        // Act
        CancelJobsResponse? clientResponse = await client.CancelJobsAsync(clientRequest);
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task CancelMyJobsAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        var clientRequest = new CancelMyJobsRequest
        {
            RequestId = 790,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                JobIds = [5, 6],
                Message = "cancel my jobs test"
            }
        };
        CancelMyJobsResponse? serverResponse = null;
        IIppRequest? serverRequest = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        var server = new SharpIppServer();
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new CancelMyJobsResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["cancel-my-jobs detail"],
                }
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
        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));

        // Act
        CancelMyJobsResponse? clientResponse = await client.CancelMyJobsAsync(clientRequest);

        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task ResubmitJobAsync_WhenSendingMessage_ServerReceivesSameRequest()
    {
        // Arrange
        var clientRequest = new ResubmitJobRequest
        {
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                JobId = 123,
                IppAttributeFidelity = true,
                JobMandatoryAttributes = new[] { "copies" },
                DocumentFormatDetails = new DocumentFormatDetails
                {
                    DocumentSourceApplicationName = "ResubmitClient",
                    DocumentSourceApplicationVersion = "1.0.0",
                    DocumentSourceOsName = "Windows",
                    DocumentSourceOsVersion = "11"
                }
            },
            JobTemplateAttributes = new()
            {
                Copies = 2
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
    public async Task ResubmitJobAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        var clientRequest = new ResubmitJobRequest
        {
            RequestId = 791,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                JobId = 222,
                IppAttributeFidelity = true,
                JobMandatoryAttributes = ["copies"]
            },
            JobTemplateAttributes = new()
            {
                Copies = 3
            }
        };
        ResubmitJobResponse? serverResponse = null;
        IIppRequest? serverRequest = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        var server = new SharpIppServer();
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new ResubmitJobResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["resubmit detail"],
                }
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
        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));

        // Act
        ResubmitJobResponse? clientResponse = await client.ResubmitJobAsync(clientRequest);

        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task CloseJobAsync_WhenSendingMessage_ServerReceivesSameRequest()
    {
        // Arrange
        var clientRequest = new CloseJobRequest
        {
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                JobId = 123
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
    public async Task CloseJobAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        var clientRequest = new CloseJobRequest
        {
            RequestId = 792,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                JobId = 333
            }
        };
        CloseJobResponse? serverResponse = null;
        IIppRequest? serverRequest = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        var server = new SharpIppServer();
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new CloseJobResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["close-job detail"],
                }
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
        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));

        // Act
        CloseJobResponse? clientResponse = await client.CloseJobAsync(clientRequest);

        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task SetJobAttributesAsync_WhenSendingMessage_ServerReceivesSameRequest()
    {
        // Arrange
        var clientRequest = new SetJobAttributesRequest
        {
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                JobId = 444
            },
            JobTemplateAttributes = new()
            {
                Copies = 2,
                JobPriority = 50
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
    public async Task SetJobAttributesAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        var clientRequest = new SetJobAttributesRequest
        {
            RequestId = 793,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                JobId = 445
            },
            JobTemplateAttributes = new()
            {
                Copies = 3,
                JobPriority = 60
            }
        };
        SetJobAttributesResponse? serverResponse = null;
        IIppRequest? serverRequest = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        var server = new SharpIppServer();
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new SetJobAttributesResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["set-job-attributes detail"],
                }
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
        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));

        // Act
        SetJobAttributesResponse? clientResponse = await client.SetJobAttributesAsync(clientRequest);

        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task SetPrinterAttributesAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        var clientRequest = new SetPrinterAttributesRequest
        {
            RequestId = 794,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631")
            }
        };
        SetPrinterAttributesResponse? serverResponse = null;
        IIppRequest? serverRequest = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        var server = new SharpIppServer();
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new SetPrinterAttributesResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["set-printer-attributes detail"],
                }
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
        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));

        // Act
        SetPrinterAttributesResponse? clientResponse = await client.SetPrinterAttributesAsync(clientRequest);

        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task CreateJobAsync_WhenUsingPwg51007OperationAttributes_ServerReceivesSameRequest()
    {
        // Arrange
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

        // Act
        var clientRawRequest = client.CreateRawRequest(clientRequest);
        var serverRequest = (await server.ReceiveRequestAsync(clientRawRequest));

        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
    }

    [TestMethod()]
    public async Task SendAsync_AllSections_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
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
            serverRawRequest = (await server.ReceiveRawRequestAsync(s, c));
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
            return new HttpResponseMessage()
            {
                StatusCode = statusCode,
                Content = new StreamContent(memoryStream)
            };
        }
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        // Act
        IIppResponseMessage? clientRawResponse = await client.SendAsync(printerUri, clientRawRequest);
        // Assert
        clientRawRequest.Should().NotBeNull().And.BeEquivalentTo(serverRawRequest, options => options.Excluding(x => x!.Document));
        clientRawResponse.Should().BeEquivalentTo(serverRawResponse);
    }

    [TestMethod()]
    public async Task AllocatePrinterResourcesAsync_WhenSendingMessage_ServerReceivesSameRequest()
    {
        // Arrange
        var clientRequest = new AllocatePrinterResourcesRequest
        {
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                SystemUri = new Uri("ipp://example.com/ipp/system"),
                PrinterId = 42,
                ResourceIds = new[] { 10, 11 }
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
                DocumentName = "のデフォルト値を保存するメソッドを呼び出します.pdf",
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
            JobTemplateAttributes = new()
            {
                Copies = 1,
                JobPriority = 1,
                JobSheets = JobSheets.None,
                JobHoldUntil = JobHoldUntil.NoHold,
                MultipleDocumentHandling = MultipleDocumentHandling.SeparateDocumentsUncollatedCopies,
                PageRanges = [new SharpIpp.Protocol.Models.Range(1, 2)],
                Sides = Sides.OneSided,
                NumberUp = 1,
                OrientationRequested = Orientation.Portrait,
                Media = (Media)"iso_a4_210x297mm",
                PrinterResolution = new Resolution(600, 600, ResolutionUnit.DotsPerInch),
                PrintQuality = PrintQuality.Normal,
                PrintScaling = PrintScaling.Auto,
                PrintColorMode = PrintColorMode.Color,
                MediaCol = new MediaCol
                {
                    MediaBackCoating = MediaCoating.Glossy,
                    MediaBottomMargin = 10,
                    MediaColor = (MediaColor)"white",
                    MediaFrontCoating = MediaCoating.Glossy,
                    MediaGrain = MediaGrain.XDirection,
                    MediaHoleCount = 0,
                    MediaInfo = "test",
                    MediaKey = (MediaKey)"test",
                    MediaLeftMargin = 10,
                    MediaOrderCount = 1,
                    MediaPrePrinted = MediaPrePrinted.Blank,
                    MediaRecycled = MediaRecycled.None,
                    MediaRightMargin = 10,
                    MediaSize = new MediaSize { XDimension = 21000, YDimension = 29700 }, // 1/100 mm for A4
                    MediaSizeName = (Media)"iso_a4_210x297mm",
                    MediaSource = MediaSource.Main,
                    MediaSourceProperties = new MediaSourceProperties { MediaSourceFeedDirection = MediaSourceFeedDirection.LongEdgeFirst, MediaSourceFeedOrientation = Orientation.Portrait },
                    MediaThickness = 10,
                    MediaTooth = MediaTooth.Medium,
                    MediaTopMargin = 10,
                    MediaType = (MediaType)"stationery",
                    MediaWeightMetric = 80
                },
                FinishingsCol = [
                    new FinishingsCol
                    {
                        FinishingTemplate = (FinishingTemplate)"staple",
                        ImpositionTemplate = (ImpositionTemplate)"signature",
                        MediaSheetsSupported = new SharpIpp.Protocol.Models.Range(1, 10),
                        MediaSizeName = (Media)"iso_a4_210x297mm",
                        MediaSize = new MediaSize { XDimension = 21000, YDimension = 29700 },
                        Stitching = new Stitching
                        {
                            StitchingAngle = 90,
                            StitchingMethod = StitchingMethod.Wire,
                            StitchingReferenceEdge = FinishingReferenceEdge.Left,
                            StitchingLocations = [10, 20],
                            StitchingOffset = 5
                        },
                        Binding = new Binding
                        {
                            BindingReferenceEdge = FinishingReferenceEdge.Left,
                            BindingType = BindingType.Perfect
                        }
                    },
                    new FinishingsCol
                    {
                        Folding = [
                            new Folding
                            {
                                FoldingDirection = FoldingDirection.Inward,
                                FoldingOffset = 100,
                                FoldingReferenceEdge = FinishingReferenceEdge.Top
                            }
                        ]
                    },
                    new FinishingsCol
                    {
                        Baling = new Baling
                        {
                            BalingType = BalingType.Band,
                            BalingWhen = BalingWhen.AfterJob
                        },
                        Coating = new Coating
                        {
                            CoatingSides = CoatingSides.Both,
                            CoatingType = CoatingType.Glossy
                        },
                        Covering = new Covering
                        {
                            CoveringName = CoveringName.Plain
                        },
                        Laminating = new Laminating
                        {
                            LaminatingSides = CoatingSides.Front,
                            LaminatingType = LaminatingType.Archival
                        },
                        Punching = new Punching
                        {
                            PunchingLocations = [50, 100],
                            PunchingOffset = 10,
                            PunchingReferenceEdge = FinishingReferenceEdge.Top
                        },
                        Trimming = [
                            new Trimming
                            {
                                TrimmingOffset = [5],
                                TrimmingReferenceEdge = FinishingReferenceEdge.Right,
                                TrimmingType = TrimmingType.DrawLine,
                                TrimmingWhen = TrimmingWhen.AfterJob
                            }
                        ]
                    }
                ],
                JobPagesPerSet = 1,
                OutputBin = (OutputBin)"face-down",
                JobAccountId = "account-123",
                JobAccountingUserId = "user-456",
                JobCancelAfter = 3600,
                JobDelayOutputUntil = JobHoldUntil.NoHold,
                JobDelayOutputUntilTime = new DateTimeOffset(2024, 6, 1, 12, 0, 0, TimeSpan.Zero),
                JobHoldUntilTime = new DateTimeOffset(2024, 6, 1, 12, 0, 0, TimeSpan.Zero),
                JobRetainUntil = JobHoldUntil.NoHold,
                JobRetainUntilInterval = 600,
                JobRetainUntilTime = new DateTimeOffset(2024, 6, 1, 12, 0, 0, TimeSpan.Zero),
                JobSheetMessage = "Please deliver to room 101",
                OutputDevice = "printer-1",
                PrintContentOptimize = PrintContentOptimize.Text,
                CoverFront = new Cover
                {
                    CoverType = CoverType.PrintBoth,
                    Media = (Media)"iso_a4_210x297mm",
                    MediaCol = new MediaCol { MediaColor = (MediaColor)"blue" }
                },
                CoverBack = new Cover
                {
                    CoverType = CoverType.PrintNone,
                    Media = (Media)"iso_a4_210x297mm"
                },
                InsertSheet = [
                    new InsertSheet
                    {
                        InsertAfterPageNumber = 1,
                        InsertCount = 2,
                        Media = (Media)"iso_a4_210x297mm",
                        MediaCol = new MediaCol { MediaColor = (MediaColor)"red" }
                    }
                ],
                JobAccountingSheets = new JobAccountingSheets
                {
                    JobAccountingOutputBin = (OutputBin)"top",
                    JobAccountingSheetsType = JobSheetsType.Standard,
                    Media = (Media)"iso_a4_210x297mm",
                    MediaCol = new MediaCol { MediaColor = (MediaColor)"green" }
                },
                JobErrorSheet = new JobErrorSheet
                {
                    JobErrorSheetType = JobSheetsType.Standard,
                    JobErrorSheetWhen = JobErrorSheetWhen.OnError,
                    Media = (Media)"iso_a4_210x297mm",
                    MediaCol = new MediaCol { MediaColor = (MediaColor)"yellow" }
                },
                SeparatorSheets = new SeparatorSheets
                {
                    SeparatorSheetsType = [SeparatorSheetsType.SlipSheets],
                    Media = (Media)"iso_a4_210x297mm",
                    MediaCol = new MediaCol { MediaColor = (MediaColor)"black" }
                },
                ForceFrontSide = [1, 2],
                ImageOrientation = Orientation.Portrait,
                ImpositionTemplate = (ImpositionTemplate)"imp-template",
                JobCompleteBefore = JobHoldUntil.NoHold,
                JobCompleteBeforeTime = new DateTimeOffset(2024, 6, 2, 12, 0, 0, TimeSpan.Zero),
                JobMessageToOperator = "Please check finishing",
                JobPhoneNumber = "tel:+123456789",
                JobRecipientName = "recipient-name",
                MediaInputTrayCheck = (MediaInputTrayCheck)"main",
                PageDelivery = PageDelivery.SameOrderFaceUp,
                PresentationDirectionNumberUp = PresentationDirectionNumberUp.TobottomToleft,
                XImagePosition = XImagePosition.Center,
                XImageShift = 5,
                XSide1ImageShift = 6,
                XSide2ImageShift = 7,
                YImagePosition = YImagePosition.Center,
                YImageShift = 8,
                YSide1ImageShift = 9,
                YSide2ImageShift = 10
            }
        };
        IIppRequest? serverRequest = null;
        PrintJobResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new PrintJobResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["detail1"],
                    DocumentAccessError = "none"
                },
                JobAttributes = new()
                {
                    JobState = JobState.Pending,
                    JobStateReasons = [JobStateReason.None],
                    JobStateMessage = "pending",
                    NumberOfInterveningJobs = 0,
                    JobId = 456,
                    JobUri = "http://127.0.0.1:631/456",
                    ClientInfo = [new ClientInfo { ClientName = "SharpIppTests", ClientType = ClientType.Application }],
                    JobImpressionsCompletedCol = new JobCounter { Monochrome = 5 },
                    JobMediaSheetsCompletedCol = new JobCounter { Monochrome = 4 },
                    JobPagesCompleted = 3,
                    JobPagesCompletedCol = new JobCounter { Monochrome = 3 },
                    JobProcessingTime = 120
                },
                DocumentAttributes = new()
                {
                    DocumentNumber = 1,
                    DocumentState = DocumentState.Pending,
                    DocumentStateReasons = [DocumentStateReason.None],
                    DocumentStateMessage = "pending",
                    AttributesCharset = "utf-8",
                    AttributesNaturalLanguage = "en-us",
                    DateTimeAtCompleted = new DateTimeOffset(2024, 6, 1, 12, 0, 0, TimeSpan.Zero),
                    DateTimeAtCreation = new DateTimeOffset(2024, 5, 31, 12, 0, 0, TimeSpan.Zero),
                    DateTimeAtProcessing = new DateTimeOffset(2024, 6, 1, 11, 0, 0, TimeSpan.Zero),
                    DetailedStatusMessages = ["detail1", "detail2"],
                    DocumentAccessErrors = ["none"],
                    DocumentCharset = "utf-8",
                    DocumentFormat = "application/pdf",
                    DocumentFormatDetected = "application/pdf",
                    DocumentJobId = 456,
                    DocumentJobUri = "http://127.0.0.1:631/456",
                    DocumentMessage = "doc message",
                    DocumentName = "docname.pdf",
                    DocumentNaturalLanguage = "en",
                    DocumentPrinterUri = "ipp://127.0.0.1:631/print",
                    DocumentUri = "http://127.0.0.1:631/document/1",
                    Impressions = 10,
                    ImpressionsCompleted = 5,
                    KOctets = 100,
                    KOctetsProcessed = 50,
                    LastDocument = true,
                    MediaSheets = 2,
                    MediaSheetsCompleted = 1,
                    MoreInfo = "http://info",
                    OutputDeviceAssigned = "tray1",
                    PrinterUpTime = 3600,
                    TimeAtCompleted = 120,
                    TimeAtCreation = 60,
                    TimeAtProcessing = 30,
                    Pages = 3,
                    PagesCompleted = 2,
                    IsNoValue = false
                }
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
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        // Act
        PrintJobResponse? clientResponse = await client.PrintJobAsync(clientRequest);
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task PrintJobAsync_LongWayWhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
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
                DocumentName = "のデフォルト値を保存するメソッドを呼び出します.pdf",
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
            },
            JobTemplateAttributes = new()
            {
                Copies = 1,
                JobPriority = 1,
                JobSheets = JobSheets.None,
                JobHoldUntil = JobHoldUntil.NoHold,
                MultipleDocumentHandling = MultipleDocumentHandling.SeparateDocumentsUncollatedCopies,
                PageRanges = [new SharpIpp.Protocol.Models.Range(1, 2)],
                Sides = Sides.OneSided,
                NumberUp = 1,
                OrientationRequested = Orientation.Portrait,
                Media = (Media)"iso_a4_210x297mm",
                PrinterResolution = new Resolution(600, 600, ResolutionUnit.DotsPerInch),
                PrintQuality = PrintQuality.Normal,
                PrintScaling = PrintScaling.Auto,
                PrintColorMode = PrintColorMode.Color,
                MediaCol = new MediaCol
                {
                    MediaBackCoating = MediaCoating.Glossy,
                    MediaBottomMargin = 10,
                    MediaColor = (MediaColor)"white",
                    MediaFrontCoating = MediaCoating.Glossy,
                    MediaGrain = MediaGrain.XDirection,
                    MediaHoleCount = 0,
                    MediaInfo = "test",
                    MediaKey = (MediaKey)"test",
                    MediaLeftMargin = 10,
                    MediaOrderCount = 1,
                    MediaPrePrinted = MediaPrePrinted.Blank,
                    MediaRecycled = MediaRecycled.None,
                    MediaRightMargin = 10,
                    MediaSize = new MediaSize { XDimension = 21000, YDimension = 29700 }, // 1/100 mm for A4
                    MediaSizeName = (Media)"iso_a4_210x297mm",
                    MediaSource = MediaSource.Main,
                    MediaSourceProperties = new MediaSourceProperties { MediaSourceFeedDirection = MediaSourceFeedDirection.LongEdgeFirst, MediaSourceFeedOrientation = Orientation.Portrait },
                    MediaThickness = 10,
                    MediaTooth = MediaTooth.Medium,
                    MediaTopMargin = 10,
                    MediaType = (MediaType)"stationery",
                    MediaWeightMetric = 80
                },
                FinishingsCol = [
                    new FinishingsCol
                    {
                        MediaSize = new MediaSize { XDimension = 21000, YDimension = 29700 },
                        Baling = new Baling
                        {
                            BalingType = BalingType.Band,
                            BalingWhen = BalingWhen.AfterJob
                        },
                        Coating = new Coating
                        {
                            CoatingSides = CoatingSides.Both,
                            CoatingType = CoatingType.Glossy
                        },
                        Covering = new Covering
                        {
                            CoveringName = CoveringName.Plain
                        },
                        Folding = [
                            new Folding
                            {
                                FoldingDirection = FoldingDirection.Inward,
                                FoldingOffset = 100,
                                FoldingReferenceEdge = FinishingReferenceEdge.Top
                            }
                        ],
                        Laminating = new Laminating
                        {
                            LaminatingSides = CoatingSides.Front,
                            LaminatingType = LaminatingType.Archival
                        },
                        Punching = new Punching
                        {
                            PunchingLocations = [50, 100],
                            PunchingOffset = 10,
                            PunchingReferenceEdge = FinishingReferenceEdge.Top
                        },
                        Trimming = [
                            new Trimming
                            {
                                TrimmingOffset = [5],
                                TrimmingReferenceEdge = FinishingReferenceEdge.Right,
                                TrimmingType = TrimmingType.DrawLine,
                                TrimmingWhen = TrimmingWhen.AfterJob
                            }
                        ]
                    }
                ],
                JobPagesPerSet = 1
            }
        };
        IIppRequest? serverRequest = null;
        PrintJobResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new PrintJobResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["detail1"],
                    DocumentAccessError = "none"
                },
                JobAttributes = new()
                {
                    JobState = JobState.Pending,
                    JobStateReasons = [JobStateReason.None],
                    JobStateMessage = "pending",
                    NumberOfInterveningJobs = 0,
                    JobId = 456,
                    JobUri = "http://127.0.0.1:631/456",
                    ClientInfo = [new ClientInfo { ClientName = "SharpIppTests", ClientType = ClientType.Application }],
                    JobImpressionsCompletedCol = new JobCounter { Monochrome = 5 },
                    JobMediaSheetsCompletedCol = new JobCounter { Monochrome = 4 },
                    JobPagesCompleted = 3,
                    JobPagesCompletedCol = new JobCounter { Monochrome = 3 },
                    JobProcessingTime = 120
                },
                DocumentAttributes = new()
                {
                    DocumentNumber = 1,
                    DocumentState = DocumentState.Pending,
                    DocumentStateReasons = [DocumentStateReason.None],
                    DocumentStateMessage = "pending",
                    AttributesCharset = "utf-8",
                    AttributesNaturalLanguage = "en-us",
                    DateTimeAtCompleted = new DateTimeOffset(2024, 6, 1, 12, 0, 0, TimeSpan.Zero),
                    DateTimeAtCreation = new DateTimeOffset(2024, 5, 31, 12, 0, 0, TimeSpan.Zero),
                    DateTimeAtProcessing = new DateTimeOffset(2024, 6, 1, 11, 0, 0, TimeSpan.Zero),
                    DetailedStatusMessages = ["detail1", "detail2"],
                    DocumentAccessErrors = ["none"],
                    DocumentCharset = "utf-8",
                    DocumentFormat = "application/pdf",
                    DocumentFormatDetected = "application/pdf",
                    DocumentJobId = 456,
                    DocumentJobUri = "http://127.0.0.1:631/456",
                    DocumentMessage = "doc message",
                    DocumentName = "docname.pdf",
                    DocumentNaturalLanguage = "en",
                    DocumentPrinterUri = "ipp://127.0.0.1:631/print",
                    DocumentUri = "http://127.0.0.1:631/document/1",
                    Impressions = 10,
                    ImpressionsCompleted = 5,
                    KOctets = 100,
                    KOctetsProcessed = 50,
                    LastDocument = true,
                    MediaSheets = 2,
                    MediaSheetsCompleted = 1,
                    MoreInfo = "http://info",
                    OutputDeviceAssigned = "tray1",
                    PrinterUpTime = 3600,
                    TimeAtCompleted = 120,
                    TimeAtCreation = 60,
                    TimeAtProcessing = 30,
                    Pages = 3,
                    PagesCompleted = 2,
                    IsNoValue = false
                }
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
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        // Act
        var clientRawRequest = client.CreateRawRequest(clientRequest);
        var clientRawResponse = await client.SendAsync(clientRequest.OperationAttributes.PrinterUri, clientRawRequest).ConfigureAwait(false);
        var clientResponse = client.CreateResponse<PrintJobResponse>(clientRawResponse);
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task PrintJobAsync_TurnOffReadDocumentStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        using MemoryStream memoryStream = new(Encoding.ASCII.GetBytes("Lorem"));
        IppProtocol ippProtocol = new()
        {
            ReadDocumentStream = false
        };
        SharpIppServer server = new(ippProtocol);
        PrintJobRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            Document = memoryStream,
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                DocumentName = "のデフォルト値を保存するメソッドを呼び出します.pdf"
            },
            JobTemplateAttributes = new()
        };
        IIppRequest? serverRequest = null;
        PrintJobResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new PrintJobResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["detail1"],
                    DocumentAccessError = "none"
                },
                JobAttributes = new()
                {
                    JobState = JobState.Pending,
                    JobStateReasons = [JobStateReason.None],
                    JobStateMessage = "pending",
                    NumberOfInterveningJobs = 0,
                    JobId = 456,
                    JobUri = "http://127.0.0.1:631/456",
                    ClientInfo = [new ClientInfo { ClientName = "SharpIppTests", ClientType = ClientType.Application }],
                    JobImpressionsCompletedCol = new JobCounter { Monochrome = 5 },
                    JobMediaSheetsCompletedCol = new JobCounter { Monochrome = 4 },
                    JobPagesCompleted = 3,
                    JobPagesCompletedCol = new JobCounter { Monochrome = 3 },
                    JobProcessingTime = 120
                },
                DocumentAttributes = new()
                {
                    DocumentNumber = 1,
                    DocumentState = DocumentState.Pending,
                    DocumentStateReasons = [DocumentStateReason.None],
                    DocumentStateMessage = "pending",
                    Pages = 3,
                    PagesCompleted = 2,
                    IsNoValue = false
                }
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
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object), ippProtocol);
        // Act
        PrintJobResponse? clientResponse = await client.PrintJobAsync(clientRequest);
        // Assert
        serverRequest.As<PrintJobRequest>().Document.Should().BeSameAs(Stream.Null);
    }

    [TestMethod()]
    public async Task PrintUriAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        SharpIppServer server = new();
        PrintUriRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                DocumentUri = new Uri("ftp://document.pdf"),
                DocumentName = "のデフォルト値を保存するメソッドを呼び出します.pdf",
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
            JobTemplateAttributes = new()
            {
                Copies = 1,
                JobPriority = 1,
                JobSheets = JobSheets.None,
                JobHoldUntil = JobHoldUntil.NoHold,
                MultipleDocumentHandling = MultipleDocumentHandling.SeparateDocumentsUncollatedCopies,
                Finishings = new[] { Finishings.None },
                PageRanges = [new SharpIpp.Protocol.Models.Range(1, 2)],
                Sides = Sides.OneSided,
                NumberUp = 1,
                OrientationRequested = Orientation.Portrait,
                Media = (Media)"iso_a4_210x297mm",
                PrinterResolution = new Resolution(600, 600, ResolutionUnit.DotsPerInch),
                PrintQuality = PrintQuality.Normal,
                PrintScaling = PrintScaling.Auto,
                PrintColorMode = PrintColorMode.Color,
                MediaCol = new MediaCol
                {
                    MediaBackCoating = MediaCoating.Glossy,
                    MediaBottomMargin = 10,
                    MediaColor = (MediaColor)"white",
                    MediaFrontCoating = MediaCoating.Glossy,
                    MediaGrain = MediaGrain.XDirection,
                    MediaHoleCount = 0,
                    MediaInfo = "test",
                    MediaKey = (MediaKey)"test",
                    MediaLeftMargin = 10,
                    MediaOrderCount = 1,
                    MediaPrePrinted = MediaPrePrinted.Blank,
                    MediaRecycled = MediaRecycled.None,
                    MediaRightMargin = 10,
                    MediaSize = new MediaSize { XDimension = 21000, YDimension = 29700 }, // 1/100 mm for A4
                    MediaSizeName = (Media)"iso_a4_210x297mm",
                    MediaSource = MediaSource.Main,
                    MediaSourceProperties = new MediaSourceProperties { MediaSourceFeedDirection = MediaSourceFeedDirection.LongEdgeFirst, MediaSourceFeedOrientation = Orientation.Portrait },
                    MediaThickness = 10,
                    MediaTooth = MediaTooth.Medium,
                    MediaTopMargin = 10,
                    MediaType = (MediaType)"stationery",
                    MediaWeightMetric = 80
                },
                JobPagesPerSet = 1,
                CoverFront = new Cover
                {
                    CoverType = CoverType.PrintBoth,
                    Media = (Media)"iso_a4_210x297mm",
                    MediaCol = new MediaCol { MediaColor = (MediaColor)"blue" }
                },
                CoverBack = new Cover
                {
                    CoverType = CoverType.PrintNone,
                    Media = (Media)"iso_a4_210x297mm"
                },
                InsertSheet = [
                    new InsertSheet
                    {
                        InsertAfterPageNumber = 1,
                        InsertCount = 2,
                        Media = (Media)"iso_a4_210x297mm",
                        MediaCol = new MediaCol { MediaColor = (MediaColor)"red" }
                    }
                ],
                JobAccountingSheets = new JobAccountingSheets
                {
                    JobAccountingOutputBin = (OutputBin)"top",
                    JobAccountingSheetsType = JobSheetsType.Standard,
                    Media = (Media)"iso_a4_210x297mm",
                    MediaCol = new MediaCol { MediaColor = (MediaColor)"green" }
                },
                JobErrorSheet = new JobErrorSheet
                {
                    JobErrorSheetType = JobSheetsType.Standard,
                    JobErrorSheetWhen = JobErrorSheetWhen.OnError,
                    Media = (Media)"iso_a4_210x297mm",
                    MediaCol = new MediaCol { MediaColor = (MediaColor)"yellow" }
                },
                SeparatorSheets = new SeparatorSheets
                {
                    SeparatorSheetsType = [SeparatorSheetsType.SlipSheets],
                    Media = (Media)"iso_a4_210x297mm",
                    MediaCol = new MediaCol { MediaColor = (MediaColor)"black" }
                },
                ForceFrontSide = [1, 2],
                ImageOrientation = Orientation.Portrait,
                ImpositionTemplate = (ImpositionTemplate)"imp-template",
                JobCompleteBefore = JobHoldUntil.NoHold,
                JobCompleteBeforeTime = new DateTimeOffset(2024, 6, 2, 12, 0, 0, TimeSpan.Zero),
                JobMessageToOperator = "Please check finishing",
                JobPhoneNumber = "tel:+123456789",
                JobRecipientName = "recipient-name",
                MediaInputTrayCheck = (MediaInputTrayCheck)"main",
                PageDelivery = PageDelivery.SameOrderFaceUp,
                PresentationDirectionNumberUp = PresentationDirectionNumberUp.TobottomToleft,
                XImagePosition = XImagePosition.Center,
                XImageShift = 5,
                XSide1ImageShift = 6,
                XSide2ImageShift = 7,
                YImagePosition = YImagePosition.Center,
                YImageShift = 8,
                YSide1ImageShift = 9,
                YSide2ImageShift = 10
            }
        };
        IIppRequest? serverRequest = null;
        PrintUriResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new PrintUriResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["detail1"],
                    DocumentAccessError = "none"
                },
                JobAttributes = new()
                {
                    JobState = JobState.Pending,
                    JobStateReasons = [JobStateReason.None],
                    JobStateMessage = "pending",
                    NumberOfInterveningJobs = 0,
                    JobId = 456,
                    JobUri = "http://127.0.0.1:631/456",
                    ClientInfo = [new ClientInfo { ClientName = "SharpIppTests", ClientType = ClientType.Application }],
                    JobImpressionsCompletedCol = new JobCounter { Monochrome = 5 },
                    JobMediaSheetsCompletedCol = new JobCounter { Monochrome = 4 },
                    JobPagesCompleted = 3,
                    JobPagesCompletedCol = new JobCounter { Monochrome = 3 },
                    JobProcessingTime = 120
                },
                DocumentAttributes = new()
                {
                    DocumentNumber = 1,
                    DocumentState = DocumentState.Pending,
                    DocumentStateReasons = [DocumentStateReason.None],
                    DocumentStateMessage = "pending",
                    Pages = 3,
                    PagesCompleted = 2,
                    IsNoValue = false
                }
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
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        // Act
        PrintUriResponse? clientResponse = await client.PrintUriAsync(clientRequest);
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task SendDocumentAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        using MemoryStream memoryStream = new();
        SharpIppServer server = new();
        SendDocumentRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            Document = memoryStream,
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
                RequestingUserUri = new Uri("mailto:test-user@example.com"),
                JobId = 1,
                JobUri = new Uri("http://127.0.0.1:631/jobs/1"),
                ClientInfo =
                [
                    new ClientInfo
                    {
                        ClientName = "SharpIppTests",
                        ClientType = ClientType.Application
                    }
                ],
                DocumentFormatDetails = new DocumentFormatDetails
                {
                    DocumentSourceApplicationName = "SharpIppTests",
                    DocumentSourceApplicationVersion = "1.0.0",
                    DocumentSourceOsName = "Windows",
                    DocumentSourceOsVersion = "11"
                },
                DocumentName = "test-document",
                Compression = Compression.None,
                DocumentFormat = "application/pdf",
                DocumentNaturalLanguage = "en",
                DocumentCharset = "utf-8",
                DocumentMessage = "document-message",
                LastDocument = true,
            },
            DocumentTemplateAttributes = new()
            {
                Copies = 1,
                CoverBack = new Cover
                {
                    CoverType = CoverType.PrintNone,
                    Media = (Media)"iso_a4_210x297mm"
                },
                CoverFront = new Cover
                {
                    CoverType = CoverType.PrintBoth,
                    Media = (Media)"iso_a4_210x297mm",
                    MediaCol = new MediaCol { MediaColor = (MediaColor)"blue" }
                },
                FinishingsCol = [
                    new FinishingsCol
                    {
                        FinishingTemplate = (FinishingTemplate)"staple",
                        ImpositionTemplate = (ImpositionTemplate)"signature",
                        MediaSheetsSupported = new SharpIpp.Protocol.Models.Range(1, 10),
                        MediaSizeName = (Media)"iso_a4_210x297mm",
                        MediaSize = new MediaSize { XDimension = 21000, YDimension = 29700 },
                        Stitching = new Stitching
                        {
                            StitchingAngle = 90,
                            StitchingMethod = StitchingMethod.Wire,
                            StitchingReferenceEdge = FinishingReferenceEdge.Left,
                            StitchingLocations = [10, 20],
                            StitchingOffset = 5
                        },
                        Binding = new Binding
                        {
                            BindingReferenceEdge = FinishingReferenceEdge.Left,
                            BindingType = BindingType.Perfect
                        }
                    }
                ],
                ForceFrontSide = [1, 2],
                ImpositionTemplate = (ImpositionTemplate)"imp-template",
                Media = (Media)"iso_a4_210x297mm",
                MediaCol = new MediaCol
                {
                    MediaBackCoating = MediaCoating.Glossy,
                    MediaBottomMargin = 10,
                    MediaColor = (MediaColor)"white",
                    MediaFrontCoating = MediaCoating.Glossy,
                    MediaGrain = MediaGrain.XDirection,
                    MediaHoleCount = 0,
                    MediaInfo = "test",
                    MediaKey = (MediaKey)"test",
                    MediaLeftMargin = 10,
                    MediaOrderCount = 1,
                    MediaPrePrinted = MediaPrePrinted.Blank,
                    MediaRecycled = MediaRecycled.None,
                    MediaRightMargin = 10,
                    MediaSize = new MediaSize { XDimension = 21000, YDimension = 29700 }, // 1/100 mm for A4
                    MediaSizeName = (Media)"iso_a4_210x297mm",
                    MediaSource = MediaSource.Main,
                    MediaSourceProperties = new MediaSourceProperties { MediaSourceFeedDirection = MediaSourceFeedDirection.LongEdgeFirst, MediaSourceFeedOrientation = Orientation.Portrait },
                    MediaThickness = 10,
                    MediaTooth = MediaTooth.Medium,
                    MediaTopMargin = 10,
                    MediaType = (MediaType)"stationery",
                    MediaWeightMetric = 80
                },
                MediaInputTrayCheck = (MediaInputTrayCheck)"main",
                NumberUp = 1,
                OrientationRequested = Orientation.Portrait,
                OutputBin = (OutputBin)"face-down",
                PageDelivery = PageDelivery.SameOrderFaceUp,
                PageRanges = [new SharpIpp.Protocol.Models.Range(1, 2)],
                PresentationDirectionNumberUp = PresentationDirectionNumberUp.TobottomToleft,
                PrintQuality = PrintQuality.Normal,
                PrinterResolution = new Resolution(600, 600, ResolutionUnit.DotsPerInch),
                Sides = Sides.OneSided,
                XImagePosition = XImagePosition.Center,
                XImageShift = 5,
                XSide1ImageShift = 6,
                XSide2ImageShift = 7,
                YImagePosition = YImagePosition.Center,
                YImageShift = 8,
                YSide1ImageShift = 9,
                YSide2ImageShift = 10
            }
        };
        IIppRequest? serverRequest = null;
        SendDocumentResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new SendDocumentResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["detail1"],
                    DocumentAccessError = "none"
                },
                JobAttributes = new()
                {
                    JobState = JobState.Pending,
                    JobStateReasons = [JobStateReason.None],
                    JobStateMessage = "pending",
                    NumberOfInterveningJobs = 0,
                    JobId = 456,
                    JobUri = "http://127.0.0.1:631/456",
                    ClientInfo = [new ClientInfo { ClientName = "SharpIppTests", ClientType = ClientType.Application }],
                    JobImpressionsCompletedCol = new JobCounter { Monochrome = 5 },
                    JobMediaSheetsCompletedCol = new JobCounter { Monochrome = 4 },
                    JobPagesCompleted = 3,
                    JobPagesCompletedCol = new JobCounter { Monochrome = 3 },
                    JobProcessingTime = 120
                },
                DocumentAttributes = new()
                {
                    DocumentNumber = 1,
                    DocumentState = DocumentState.Pending,
                    DocumentStateReasons = [DocumentStateReason.None],
                    DocumentStateMessage = "pending",
                    Pages = 3,
                    PagesCompleted = 2,
                    IsNoValue = false
                }
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
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        // Act
        SendDocumentResponse? clientResponse = await client.SendDocumentAsync(clientRequest);
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
        clientResponse.Should().NotBeNull();
        clientResponse!.JobAttributes.Should().NotBeNull();
        clientResponse.JobAttributes!.JobId.Should().Be(456);
        clientResponse.DocumentAttributes.Should().NotBeNull();
        clientResponse.DocumentAttributes!.DocumentNumber.Should().Be(1);
    
    }

    [TestMethod()]
    public async Task CancelDocumentAsync_WhenSendingRequest_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        SharpIppServer server = new();
        CancelDocumentRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
                JobId = 1,
                JobUri = new Uri("http://127.0.0.1:631/jobs/1"),
                DocumentNumber = 1,
                DocumentMessage = "cancel-message",
            },
        };
        IIppRequest? serverRequest = null;
        CancelDocumentResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new CancelDocumentResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["detail1"],
                    DocumentAccessError = "none"
                },
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
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        // Act
        CancelDocumentResponse? clientResponse = await client.CancelDocumentAsync(clientRequest);
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task SetDocumentAttributesAsync_WhenSendingRequest_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        SharpIppServer server = new();
        SetDocumentAttributesRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
                JobId = 1,
                JobUri = new Uri("ipp://localhost:631/jobs/1"),
                DocumentNumber = 1,
            },
            DocumentTemplateAttributes = new()
            {
                Copies = 1,
                Sides = Sides.TwoSidedLongEdge,
                OrientationRequested = Orientation.Landscape,
                CoverBack = new Cover
                {
                    CoverType = CoverType.PrintNone,
                    Media = (Media)"iso_a4_210x297mm"
                },
                CoverFront = new Cover
                {
                    CoverType = CoverType.PrintBoth,
                    Media = (Media)"iso_a4_210x297mm",
                    MediaCol = new MediaCol { MediaColor = (MediaColor)"blue" }
                },
                FinishingsCol = [
                    new FinishingsCol
                    {
                        FinishingTemplate = (FinishingTemplate)"staple",
                        ImpositionTemplate = (ImpositionTemplate)"signature",
                        MediaSheetsSupported = new SharpIpp.Protocol.Models.Range(1, 10),
                        MediaSizeName = (Media)"iso_a4_210x297mm",
                        MediaSize = new MediaSize { XDimension = 21000, YDimension = 29700 },
                        Stitching = new Stitching
                        {
                            StitchingAngle = 90,
                            StitchingMethod = StitchingMethod.Wire,
                            StitchingReferenceEdge = FinishingReferenceEdge.Left,
                            StitchingLocations = [10, 20],
                            StitchingOffset = 5
                        },
                        Binding = new Binding
                        {
                            BindingReferenceEdge = FinishingReferenceEdge.Left,
                            BindingType = BindingType.Perfect
                        }
                    }
                ],
                MediaCol = new MediaCol
                {
                    MediaBackCoating = MediaCoating.Glossy,
                    MediaBottomMargin = 10,
                    MediaColor = (MediaColor)"white",
                    MediaFrontCoating = MediaCoating.Glossy,
                    MediaGrain = MediaGrain.XDirection,
                    MediaHoleCount = 0,
                    MediaInfo = "test",
                    MediaKey = (MediaKey)"test",
                    MediaLeftMargin = 10,
                    MediaOrderCount = 1,
                    MediaPrePrinted = MediaPrePrinted.Blank,
                    MediaRecycled = MediaRecycled.None,
                    MediaRightMargin = 10,
                    MediaSize = new MediaSize { XDimension = 21000, YDimension = 29700 }, // 1/100 mm for A4
                    MediaSizeName = (Media)"iso_a4_210x297mm",
                    MediaSource = MediaSource.Main,
                    MediaSourceProperties = new MediaSourceProperties { MediaSourceFeedDirection = MediaSourceFeedDirection.LongEdgeFirst, MediaSourceFeedOrientation = Orientation.Portrait },
                    MediaThickness = 10,
                    MediaTooth = MediaTooth.Medium,
                    MediaTopMargin = 10,
                    MediaType = (MediaType)"stationery",
                    MediaWeightMetric = 80
                },
                ForceFrontSide = [1, 2],
                ImpositionTemplate = (ImpositionTemplate)"none",
                Media = (Media)"iso_a4_210x297mm",
                MediaInputTrayCheck = (MediaInputTrayCheck)"main",
                NumberUp = 1,
                OutputBin = (OutputBin)"top",
                PageDelivery = PageDelivery.SameOrderFaceUp,
                PageRanges = [new SharpIpp.Protocol.Models.Range(1, 1)],
                PresentationDirectionNumberUp = PresentationDirectionNumberUp.TobottomToleft,
                PrintQuality = PrintQuality.High,
                PrinterResolution = new Resolution(600, 600, ResolutionUnit.DotsPerInch),
                XImagePosition = XImagePosition.Center,
                XImageShift = 0,
                XSide1ImageShift = 0,
                XSide2ImageShift = 0,
                YImagePosition = YImagePosition.Center,
                YImageShift = 0,
                YSide1ImageShift = 0,
                YSide2ImageShift = 0,
            }
        };
        IIppRequest? serverRequest = null;
        SetDocumentAttributesResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new SetDocumentAttributesResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["detail1"],
                    DocumentAccessError = "none"
                },
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
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        // Act
        SetDocumentAttributesResponse? clientResponse = await client.SetDocumentAttributesAsync(clientRequest);
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task GetDocumentsAsync_WhenSendingRequest_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        SharpIppServer server = new();
        GetDocumentsRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
                JobId = 1,
                JobUri = new Uri("http://127.0.0.1:631/jobs/1"),
                Limit = 10,
                RequestedAttributes = ["document-name", "document-state"]
            },
        };
        IIppRequest? serverRequest = null;
        GetDocumentsResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new GetDocumentsResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["detail1"],
                    DocumentAccessError = "none"
                },
                Documents =
                [
                    new DocumentAttributes
                    {
                        DocumentNumber = 1,
                        DocumentName = "doc1",
                        DocumentState = DocumentState.Completed
                    }
                ]
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
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        // Act
        GetDocumentsResponse? clientResponse = await client.GetDocumentsAsync(clientRequest);
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task CancelJobAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        SharpIppServer server = new();
        CancelJobRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
                JobId = 1,
                JobUri = new Uri("http://127.0.0.1:631/jobs/1"),
                Message = "message",
            },
        };
        IIppRequest? serverRequest = null;
        CancelJobResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new CancelJobResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["detail1"],
                    DocumentAccessError = "none"
                },
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
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        // Act
        CancelJobResponse? clientResponse = await client.CancelJobAsync(clientRequest);
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task CancelDocumentAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        SharpIppServer server = new();
        CancelDocumentRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
                JobId = 1,
                DocumentNumber = 2,
                DocumentMessage = "cancel doc message"
            },
        };
        IIppRequest? serverRequest = null;
        CancelDocumentResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new CancelDocumentResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["detail1"],
                    DocumentAccessError = "none"
                },
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
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        // Act
        CancelDocumentResponse? clientResponse = await client.CancelDocumentAsync(clientRequest);
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task CancelDocumentAsync_WhenDocumentMessageIsNull_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        SharpIppServer server = new();
        CancelDocumentRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
                JobId = 1,
                DocumentNumber = 2,
                DocumentMessage = null
            },
        };
        IIppRequest? serverRequest = null;
        CancelDocumentResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new CancelDocumentResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["detail1"],
                    DocumentAccessError = "none"
                },
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
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        // Act
        CancelDocumentResponse? clientResponse = await client.CancelDocumentAsync(clientRequest);
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }



    [TestMethod()]
    public async Task CreateJobAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        SharpIppServer server = new();
        CreateJobRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
                JobName = "Test Job",
                IppAttributeFidelity = true,
                JobKOctets = 12,
                JobImpressions = 5,
                JobMediaSheets = 2,
            },
            JobTemplateAttributes = new()
            {
                Copies = 1,
                JobPriority = 1,
                JobSheets = JobSheets.None,
                JobHoldUntil = JobHoldUntil.NoHold,
                MultipleDocumentHandling = MultipleDocumentHandling.SeparateDocumentsUncollatedCopies,
                Finishings = new[] { Finishings.None },
                PageRanges = [new SharpIpp.Protocol.Models.Range(1, 2)],
                Sides = Sides.OneSided,
                NumberUp = 1,
                OrientationRequested = Orientation.Portrait,
                Media = (Media)"iso_a4_210x297mm",
                PrinterResolution = new Resolution(600, 600, ResolutionUnit.DotsPerInch),
                PrintQuality = PrintQuality.Normal,
                PrintScaling = PrintScaling.Auto,
                PrintColorMode = PrintColorMode.Color,
                MediaCol = new MediaCol
                {
                    MediaBackCoating = MediaCoating.Glossy,
                    MediaBottomMargin = 10,
                    MediaColor = (MediaColor)"white",
                    MediaFrontCoating = MediaCoating.Glossy,
                    MediaGrain = MediaGrain.XDirection,
                    MediaHoleCount = 0,
                    MediaInfo = "test",
                    MediaKey = (MediaKey)"test",
                    MediaLeftMargin = 10,
                    MediaOrderCount = 1,
                    MediaPrePrinted = MediaPrePrinted.Blank,
                    MediaRecycled = MediaRecycled.None,
                    MediaRightMargin = 10,
                    MediaSize = new MediaSize { XDimension = 21000, YDimension = 29700 }, // 1/100 mm for A4
                    MediaSizeName = (Media)"iso_a4_210x297mm",
                    MediaSource = MediaSource.Main,
                    MediaSourceProperties = new MediaSourceProperties { MediaSourceFeedDirection = MediaSourceFeedDirection.LongEdgeFirst, MediaSourceFeedOrientation = Orientation.Portrait },
                    MediaThickness = 10,
                    MediaTooth = MediaTooth.Medium,
                    MediaTopMargin = 10,
                    MediaType = (MediaType)"stationery",
                    MediaWeightMetric = 80
                },
                OutputBin = (OutputBin)"face-down",
            }
        };
        IIppRequest? serverRequest = null;
        CreateJobResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new CreateJobResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["detail1"],
                    DocumentAccessError = "none"
                },
                JobAttributes = new()
                {
                    JobState = JobState.Pending,
                    JobStateReasons = [JobStateReason.None],
                    JobStateMessage = "pending",
                    NumberOfInterveningJobs = 0,
                    JobId = 456,
                    JobUri = "http://127.0.0.1:631/456",
                    ClientInfo = [new ClientInfo { ClientName = "SharpIppTests", ClientType = ClientType.Application }],
                    JobImpressionsCompletedCol = new JobCounter { Monochrome = 5 },
                    JobMediaSheetsCompletedCol = new JobCounter { Monochrome = 4 },
                    JobPagesCompleted = 3,
                    JobPagesCompletedCol = new JobCounter { Monochrome = 3 },
                    JobProcessingTime = 120
                }
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
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        // Act
        CreateJobResponse? clientResponse = await client.CreateJobAsync(clientRequest);
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task GetJobAttributesAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        SharpIppServer server = new();
        GetJobAttributesRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
                JobId = 1,
                JobUri = new Uri("http://127.0.0.1:631/jobs/1"),
                RequestedAttributes = ["job-id", "job-uri", "job-state"],
            },
        };
        IIppRequest? serverRequest = null;
        GetJobAttributesResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new GetJobAttributesResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                JobAttributes = new JobDescriptionAttributes
                {
                    JobId = 1,
                    JobUri = "http://127.0.0.1:631/jobs/1",
                    JobPrinterUri = "http://127.0.0.1:631",
                    JobState = JobState.Pending,
                    JobStateReasons = [JobStateReason.None],
                    JobName = "Test Job",
                    JobOriginatingUserName = "test-user",
                    JobKOctetsProcessed = 10,
                    JobImpressions = 5,
                    JobImpressionsCol = new JobCounter
                    {
                        Blank = 1,
                        BlankTwoSided = 2,
                        FullColor = 2,
                        FullColorTwoSided = 3,
                        HighlightColor = 4,
                        HighlightColorTwoSided = 5,
                        Monochrome = 6,
                        MonochromeTwoSided = 3
                    },
                    JobImpressionsCompleted = 0,
                    JobMediaSheets = 2,
                    JobMediaSheetsCol = new JobCounter
                    {
                        Blank = 4,
                        FullColor = 5,
                        MonochromeTwoSided = 6
                    },
                    JobMoreInfo = "more info",
                    JobChargeInfo = "charge info",
                    DocumentFormatDetails = new DocumentFormatDetails
                    {
                        DocumentSourceApplicationName = "MyApp",
                        DocumentSourceOsName = "MyOS"
                    },
                    DocumentFormatDetailsDetected = new DocumentFormatDetails
                    {
                        DocumentSourceApplicationName = "DetectedApp",
                        DocumentSourceOsName = "DetectedOS"
                    },
                    NumberOfDocuments = 1,
                    NumberOfInterveningJobs = 0,
                    OutputDeviceAssigned = "printer",
                    JobMediaSheetsCompleted = 0,
                    JobStateMessage = "pending",
                    DateTimeAtCreation = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    DateTimeAtProcessing = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    DateTimeAtCompleted = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    DateTimeAtCompletedEstimated = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    DateTimeAtProcessingEstimated = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    TimeAtCreation = 100,
                    TimeAtProcessing = 110,
                    TimeAtCompleted = 120,
                    TimeAtCompletedEstimated = 120,
                    TimeAtProcessingEstimated = 110,
                    OutputDeviceJobState = JobState.Processing,
                    JobPrinterUpTime = 200,
                    JobKOctets = 20,
                    JobDetailedStatusMessages = ["message"],
                    JobDocumentAccessErrors = ["error"],
                    JobMessageFromOperator = "operator message",
                    JobPages = 10,
                    JobPagesCompleted = 5,
                    JobPagesCol = new JobCounter
                    {
                        Blank = 7,
                        FullColor = 8,
                        MonochromeTwoSided = 9
                    },
                    JobImpressionsCompletedCol = new JobCounter
                    {
                        Blank = 10,
                        FullColor = 11,
                        MonochromeTwoSided = 12
                    },
                    JobMediaSheetsCompletedCol = new JobCounter
                    {
                        Blank = 13,
                        FullColor = 14,
                        MonochromeTwoSided = 15
                    },
                    JobPagesCompletedCol = new JobCounter
                    {
                        Blank = 16,
                        FullColor = 17,
                        MonochromeTwoSided = 18
                    },
                    ClientInfo =
                    [
                        new ClientInfo
                        {
                            ClientName = "MyClient",
                            ClientType = ClientType.Application
                        }
                    ],
                    JobSheetsCol = new JobSheetsCol
                    {
                        JobSheets = JobSheets.Standard,
                        Media = (Media)"iso_a4_210x297mm",
                        MediaCol = new MediaCol { MediaColor = (MediaColor)"blue" }
                    },
                    JobProcessingTime = 30,
                    ErrorsCount = 0,
                    WarningsCount = 1,
                    PrintContentOptimizeActual = new[] { PrintContentOptimize.Text },
                    CopiesActual = [1],
                    FinishingsActual = [Finishings.None],
                    JobHoldUntilActual = [JobHoldUntil.NoHold],
                    JobPriorityActual = [50],
                    JobSheetsActual = [JobSheets.None],
                    MediaActual = [(Media)"iso_a4_210x297mm"],
                    MediaColActual = [new MediaCol
                    {
                        MediaSizeName = (Media)"iso_a4_210x297mm",
                        MediaType = (MediaType)"stationery"
                    }],
                    MultipleDocumentHandlingActual = [MultipleDocumentHandling.SeparateDocumentsUncollatedCopies],
                    NumberUpActual = [1],
                    OrientationRequestedActual = [Orientation.Portrait],
                    OutputBinActual = [(OutputBin)"face-down"],
                    PageRangesActual = [new SharpIpp.Protocol.Models.Range(1, 2)],
                    PrintQualityActual = [PrintQuality.Normal],
                    PrinterResolutionActual = [new Resolution(600, 600, ResolutionUnit.DotsPerInch)],
                    SidesActual = [Sides.OneSided],
                    FinishingsColActual = [
                        new FinishingsCol
                        {
                            FinishingTemplate = (FinishingTemplate)"staple",
                            Stitching = new Stitching
                            {
                                StitchingAngle = 90,
                                StitchingMethod = StitchingMethod.Wire,
                                StitchingReferenceEdge = FinishingReferenceEdge.Left,
                                StitchingLocations = [10, 20],
                                StitchingOffset = 5
                            },
                            Binding = new Binding
                            {
                                BindingReferenceEdge = FinishingReferenceEdge.Left,
                                BindingType = BindingType.Perfect
                            }
                        }
                    ],
                    MaterialsColActual =
                    [
                        new Material
                        {
                            MaterialName = "matte-paper",
                            MaterialColor = "white"
                        }
                    ]
                },
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["detail1"],
                    DocumentAccessError = "none"
                }
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
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        // Act
        GetJobAttributesResponse? clientResponse = await client.GetJobAttributesAsync(clientRequest);
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task GetDocumentAttributesAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        SharpIppServer server = new();
        GetDocumentAttributesRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
                JobId = 1,
                DocumentNumber = 1,
                RequestedAttributes = ["document-number", "document-state", "document-name"],
            },
        };
        IIppRequest? serverRequest = null;
        GetDocumentAttributesResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new GetDocumentAttributesResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                DocumentAttributes = new DocumentAttributes
                {
                    DocumentNumber = 1,
                    DocumentState = DocumentState.Completed,
                    DocumentName = "Test Document",
                    DocumentJobId = 1,
                    DocumentPrinterUri = "http://127.0.0.1:631",
                    AttributesCharset = "utf-8",
                    AttributesNaturalLanguage = "en-us",
                    DocumentFormatDetails = new DocumentFormatDetails
                    {
                        DocumentSourceApplicationName = "MyApp",
                        DocumentSourceOsName = "MyOS"
                    },
                    DocumentFormatDetailsDetected = new DocumentFormatDetails
                    {
                        DocumentSourceApplicationName = "DetectedApp",
                        DocumentSourceOsName = "DetectedOS"
                    },
                    ErrorsCount = 0,
                    WarningsCount = 1,
                    PrintContentOptimizeActual = [PrintContentOptimize.Text],
                    DocumentStateReasons = [DocumentStateReason.None],
                    DocumentStateMessage = "completed",
                    PrintContentOptimize = PrintContentOptimize.Text,
                    DetailedStatusMessages = ["detail-message"],
                    DocumentAccessErrors = ["access-error"],
                    DocumentCharset = "utf-8",
                    DocumentFormat = "application/pdf",
                    DocumentFormatDetected = "application/pdf",
                    DocumentJobUri = "http://127.0.0.1:631/jobs/1",
                    DocumentMessage = "document-message",
                    DocumentNaturalLanguage = "en-us",
                    DocumentUri = "http://127.0.0.1:631/jobs/1/documents/1",
                    Impressions = 1,
                    ImpressionsCompleted = 1,
                    KOctets = 1,
                    KOctetsProcessed = 1,
                    LastDocument = true,
                    MediaSheets = 1,
                    MediaSheetsCompleted = 1,
                    MoreInfo = "http://127.0.0.1:631/more-info",
                    OutputDeviceAssigned = "printer",
                    PrinterUpTime = 100,
                    TimeAtCreation = 100,
                    TimeAtProcessing = 110,
                    TimeAtCompleted = 120,
                    DateTimeAtCreation = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    DateTimeAtProcessing = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    DateTimeAtCompleted = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
                },
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["detail1"],
                    DocumentAccessError = "none"
                }
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
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        // Act
        GetDocumentAttributesResponse? clientResponse = await client.GetDocumentAttributesAsync(clientRequest);
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
    [TestMethod()]
    public async Task GetDocumentAttributesAsync_WhenDocumentAttributesAreMissing_ReturnsNoValue()
    {
        // Arrange
        SharpIppServer server = new();
        GetDocumentAttributesRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
                JobId = 1,
                DocumentNumber = 1,
            },
        };
        IIppRequest? serverRequest = null;
        GetDocumentAttributesResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new GetDocumentAttributesResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                DocumentAttributes = null,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["detail1"],
                    DocumentAccessError = "none"
                }
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
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        // Act
        GetDocumentAttributesResponse? clientResponse = await client.GetDocumentAttributesAsync(clientRequest);
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.DocumentAttributes.Should().BeNull();
    }

    [TestMethod()]
    public async Task GetDocumentsAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        SharpIppServer server = new();
        GetDocumentsRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
                JobId = 1,
                RequestedAttributes = ["document-number", "document-state", "document-name"],
                Limit = 10,
            },
        };
        IIppRequest? serverRequest = null;
        GetDocumentsResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new GetDocumentsResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                Documents = 
                [ 
                    new DocumentAttributes
                    {
                        DocumentNumber = 1,
                        DocumentState = DocumentState.Completed,
                        DocumentName = "Test Document 1",
                        DocumentJobId = 1,
                        DocumentPrinterUri = "http://127.0.0.1:631",
                        AttributesCharset = "utf-8",
                        AttributesNaturalLanguage = "en-us",
                        DocumentStateReasons = [DocumentStateReason.None],
                        DocumentStateMessage = "completed",
                    },
                    new DocumentAttributes
                    {
                        DocumentNumber = 2,
                        DocumentState = DocumentState.Processing,
                        DocumentName = "Test Document 2",
                        DocumentJobId = 1,
                        DocumentPrinterUri = "http://127.0.0.1:631",
                        AttributesCharset = "utf-8",
                        AttributesNaturalLanguage = "en-us",
                        DocumentStateReasons = [DocumentStateReason.None],
                        DocumentStateMessage = "processing",
                    }
                ],
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["detail1"],
                    DocumentAccessError = "none"
                }
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
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        // Act
        GetDocumentsResponse? clientResponse = await client.GetDocumentsAsync(clientRequest);
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task GetJobsAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        SharpIppServer server = new();
        GetJobsRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
                RequestedAttributes = ["job-id", "job-uri", "job-state"],
                WhichJobs = WhichJobs.Completed,
                Limit = 10,
                JobIds = [1, 2],
            },
        };
        IIppRequest? serverRequest = null;
        GetJobsResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new GetJobsResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["detail1"],
                    DocumentAccessError = "none"
                },
                JobsAttributes = 
                [
                    new JobDescriptionAttributes
                    {
                        JobId = 1,
                        JobUri = "http://127.0.0.1:631/jobs/1",
                        JobPrinterUri = "http://127.0.0.1:631",
                        JobName = "Test Job",
                        JobOriginatingUserName = "test-user",
                        JobKOctetsProcessed = 10,
                        JobImpressions = 5,
                        JobImpressionsCompleted = 0,
                        JobMediaSheets = 2,
                        JobMoreInfo = "more info",
                        NumberOfDocuments = 1,
                        NumberOfInterveningJobs = 0,
                        OutputDeviceAssigned = "printer",
                        JobMediaSheetsCompleted = 0,
                        JobState = JobState.Pending,
                        JobStateMessage = "pending",
                        JobStateReasons = [JobStateReason.None],
                        DateTimeAtCreation = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
                        DateTimeAtProcessing = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
                        DateTimeAtCompleted = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
                        DateTimeAtCompletedEstimated = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
                        DateTimeAtProcessingEstimated = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
                        TimeAtCreation = 100,
                        TimeAtProcessing = 110,
                        TimeAtCompleted = 120,
                        TimeAtCompletedEstimated = 120,
                        TimeAtProcessingEstimated = 110,
                        JobPrinterUpTime = 200,
                        JobKOctets = 20,
                        JobDetailedStatusMessages = ["message"],
                        JobDocumentAccessErrors = ["error"],
                        JobMessageFromOperator = "operator message",
                        CopiesActual = [1],
                        FinishingsActual = [Finishings.None],
                        JobHoldUntilActual = [JobHoldUntil.NoHold],
                        JobPriorityActual = [50],
                        JobSheetsActual = [JobSheets.None],
                        MediaActual = [(Media)"iso_a4_210x297mm"],
                        MultipleDocumentHandlingActual = [MultipleDocumentHandling.SeparateDocumentsUncollatedCopies],
                        NumberUpActual = [1],
                        OrientationRequestedActual = [Orientation.Portrait],
                        OutputBinActual = [(OutputBin)"face-down"],
                        PageRangesActual = [new SharpIpp.Protocol.Models.Range(1, 2)],
                        PrintQualityActual = [PrintQuality.Normal],
                        PrinterResolutionActual = [new Resolution(600, 600, ResolutionUnit.DotsPerInch)],
                        SidesActual = [Sides.OneSided]
                    }
                ]
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
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        // Act
        GetJobsResponse? clientResponse = await client.GetJobsAsync(clientRequest);
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
    [TestMethod()]
    public async Task GetPrinterAttributesAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        SharpIppServer server = new();
        GetPrinterAttributesRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
                RequestedAttributes = ["printer-uri", "printer-state", "printer-name"],
                DocumentFormat = "application/pdf",
            },
        };
        IIppRequest? serverRequest = null;
        GetPrinterAttributesResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new GetPrinterAttributesResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                PrinterAttributes = new()
                {
                    PrinterUriSupported = ["http://127.0.0.1:631"],
                    UriSecuritySupported = [UriSecurity.None],
                    UriAuthenticationSupported = [UriAuthentication.None],
                    PrinterName = "Test Printer",
                    PrinterLocation = "Office",
                    PrinterInfo = "Test Printer Info",
                    PrinterMoreInfo = "http://127.0.0.1:631",
                    PrinterDriverInstaller = "installer",
                    PrinterMakeAndModel = "SharpIpp Virtual Printer",
                    PrinterMoreInfoManufacturer = "http://manufacturer.com",
                    PrinterState = PrinterState.Idle,
                    PrinterStateReasons = [PrinterStateReason.None],
                    PrinterStateMessage = "Idle",
                    IppVersionsSupported = [default],
                    OperationsSupported = [IppOperation.PrintJob],
                    MultipleDocumentJobsSupported = true,
                    MultipleDocumentHandlingDefault = MultipleDocumentHandling.SeparateDocumentsUncollatedCopies,
                    MultipleDocumentHandlingSupported = [MultipleDocumentHandling.SeparateDocumentsUncollatedCopies, MultipleDocumentHandling.SingleDocument],
                    CharsetConfigured = "utf-8",
                    CharsetSupported = ["utf-8"],
                    NaturalLanguageConfigured = "en-us",
                    GeneratedNaturalLanguageSupported = ["en-us"],
                    ClientInfoSupported = ["client-name", "client-type"],
                    MaxClientInfoSupported = 10,
                    DocumentCharsetDefault = "utf-8",
                    DocumentCharsetSupported = ["utf-8"],
                    DocumentNaturalLanguageDefault = "en-us",
                    DocumentNaturalLanguageSupported = ["en-us"],
                    DocumentFormatDetailsSupported = ["document-source-application-name", "document-source-os-name"],
                    JobIdsSupported = true,
                    JobMandatoryAttributesSupported = true,
                    JobSheetsColDefault = new JobSheetsCol
                    {
                        JobSheets = JobSheets.Standard,
                        Media = (Media)"iso_a4_210x297mm",
                        MediaCol = new MediaCol { MediaColor = (MediaColor)"blue" }
                    },
                    JobSheetsColSupported = ["job-sheets", "media", "media-col"],
                    DocumentFormatDefault = "application/pdf",
                    DocumentFormatSupported = ["application/pdf"],
                    PrinterIsAcceptingJobs = true,
                    QueuedJobCount = 0,
                    PrinterMessageFromOperator = "message",
                    ColorSupported = true,
                    ReferenceUriSchemesSupported = [UriScheme.Ftp],
                    PdlOverrideSupported = PdlOverride.Attempted,
                    PrinterUpTime = 100,
                    PrinterCurrentTime = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    MultipleOperationTimeOut = 10,
                    CompressionSupported = [Compression.None],
                    JobKOctetsSupported = new SharpIpp.Protocol.Models.Range(1, 100),
                    JpegKOctetsSupported = new SharpIpp.Protocol.Models.Range(1, 100),
                    PdfKOctetsSupported = new SharpIpp.Protocol.Models.Range(1, 100),
                    JobImpressionsSupported = new SharpIpp.Protocol.Models.Range(1, 100),
                    JobMediaSheetsSupported = new SharpIpp.Protocol.Models.Range(1, 100),
                    JobSheetsDefault = JobSheets.None,
                    JobSheetsSupported = [JobSheets.None, JobSheets.Standard],
                    NumberUpDefault = 1,
                    NumberUpSupported = [new SharpIpp.Protocol.Models.Range(1, 4)],
                    PagesPerMinute = 10,
                    PagesPerMinuteColor = 10,
                    PrintScalingDefault = PrintScaling.Auto,
                    PrintScalingSupported = [PrintScaling.Auto],
                    MediaDefault = (Media)"iso_a4_210x297mm",
                    MediaSupported = [(Media)"iso_a4_210x297mm"],
                    MediaReady = [(Media)"iso_a4_210x297mm", (Media)"na_letter_8.5x11in"],
                    SidesDefault = Sides.OneSided,
                    SidesSupported = [Sides.OneSided],
                    FinishingsDefault = Finishings.None,
                    FinishingsSupported = [Finishings.None],
                    PrinterResolutionDefault = new Resolution(600, 600, ResolutionUnit.DotsPerInch),
                    PrinterResolutionSupported = [new Resolution(600, 600, ResolutionUnit.DotsPerInch)],
                    PrintQualityDefault = PrintQuality.Normal,
                    PrintQualitySupported = [PrintQuality.Normal],
                    JobPriorityDefault = 1,
                    JobPrioritySupported = 1,
                    CopiesDefault = 1,
                    CopiesSupported = new SharpIpp.Protocol.Models.Range(1, 100),
                    OrientationRequestedDefault = Orientation.Portrait,
                    OrientationRequestedSupported = [Orientation.Portrait],
                    PageRangesSupported = true,
                    JobHoldUntilSupported = [JobHoldUntil.NoHold],
                    JobHoldUntilDefault = JobHoldUntil.NoHold,
                    JobHoldUntilTimeSupported = true,
                    JobDelayOutputUntilDefault = JobHoldUntil.NoHold,
                    JobDelayOutputUntilSupported = [JobHoldUntil.NoHold, JobHoldUntil.Indefinite],
                    JobDelayOutputUntilTimeSupported = new SharpIpp.Protocol.Models.Range(0, 86400),
                    JobHistoryAttributesConfigured = ["job-id", "job-state"],
                    JobHistoryAttributesSupported = ["job-id", "job-state", "job-state-reasons"],
                    JobHistoryIntervalConfigured = 60,
                    JobHistoryIntervalSupported = new SharpIpp.Protocol.Models.Range(1, 3600),
                    JobRetainUntilDefault = JobHoldUntil.NoHold,
                    JobRetainUntilIntervalDefault = 0,
                    JobRetainUntilIntervalSupported = new SharpIpp.Protocol.Models.Range(0, 86400),
                    JobRetainUntilSupported = [JobHoldUntil.NoHold, JobHoldUntil.Indefinite],
                    JobRetainUntilTimeSupported = true,
                    OutputBinDefault = (OutputBin)"face-down",
                    OutputBinSupported = [(OutputBin)"face-down"],
                    MediaColDefault =  new MediaCol
                    {
                        MediaBackCoating = MediaCoating.Glossy,
                        MediaBottomMargin = 10,
                        MediaColor = (MediaColor)"white",
                        MediaFrontCoating = MediaCoating.Glossy,
                        MediaGrain = MediaGrain.XDirection,
                        MediaHoleCount = 0,
                        MediaInfo = "test",
                        MediaKey = (MediaKey)"test",
                        MediaLeftMargin = 10,
                        MediaOrderCount = 1,
                        MediaPrePrinted = MediaPrePrinted.Blank,
                        MediaRecycled = MediaRecycled.None,
                        MediaRightMargin = 10,
                        MediaSize = new MediaSize { XDimension = 21000, YDimension = 29700 }, // 1/100 mm for A4
                        MediaSizeName = (Media)"iso_a4_210x297mm",
                        MediaSource = MediaSource.Main,
                        MediaSourceProperties = new MediaSourceProperties { MediaSourceFeedDirection = MediaSourceFeedDirection.LongEdgeFirst, MediaSourceFeedOrientation = Orientation.Portrait },
                        MediaThickness = 10,
                        MediaTooth = MediaTooth.Medium,
                        MediaTopMargin = 10,
                        MediaType = (MediaType)"stationery",
                        MediaWeightMetric = 80
                    },
                    MediaColDatabase =
                    [
                        new MediaCol { MediaKey = (MediaKey)"media-1", MediaSizeName = (Media)"iso_a4_210x297mm" },
                        new MediaCol { MediaKey = (MediaKey)"media-2", MediaSizeName = (Media)"na_letter_8.5x11in" }
                    ],
                    MediaColReady =
                    [
                        new MediaCol { MediaKey = (MediaKey)"ready-1", MediaSizeName = (Media)"iso_a4_210x297mm" }
                    ],
                    MediaColSupported = ["media-key", "media-size-name", "media-size"],
                    MediaSizeSupported =
                    [
                        new MediaSizeSupported { XDimension = new SharpIpp.Protocol.Models.Range(21000, 21000), YDimension = new SharpIpp.Protocol.Models.Range(29700, 29700) },
                        new MediaSizeSupported { XDimension = new SharpIpp.Protocol.Models.Range(21590, 21590), YDimension = new SharpIpp.Protocol.Models.Range(27940, 27940) }
                    ],
                    MediaKeySupported = ["media-1", "media-2"],
                    MediaSourceSupported = [MediaSource.Main],
                    MediaTypeSupported = [(MediaType)"stationery"],
                    MediaBackCoatingSupported = [MediaCoating.Glossy, (MediaCoating)"custom"],
                    MediaFrontCoatingSupported = [MediaCoating.Glossy],
                    MediaColorSupported = [(MediaColor)"white", (MediaColor)"blue"],
                    MediaGrainSupported = [MediaGrain.XDirection, MediaGrain.YDirection],
                    MediaToothSupported = [MediaTooth.Medium],
                    MediaPrePrintedSupported = [MediaPrePrinted.Blank],
                    MediaRecycledSupported = [MediaRecycled.None],
                    MediaHoleCountSupported = [new SharpIpp.Protocol.Models.Range(0, 0), new SharpIpp.Protocol.Models.Range(2, 4)],
                    MediaOrderCountSupported = [new SharpIpp.Protocol.Models.Range(1, 10)],
                    MediaThicknessSupported = [new SharpIpp.Protocol.Models.Range(1, 100)],
                    MediaWeightMetricSupported = [new SharpIpp.Protocol.Models.Range(1, 200)],
                    MediaBottomMarginSupported = [0, 10],
                    MediaLeftMarginSupported = [0, 10],
                    MediaRightMarginSupported = [0, 10],
                    MediaTopMarginSupported = [0, 10],
                    PrintColorModeDefault = PrintColorMode.Color,
                    PrintColorModeSupported = [PrintColorMode.Color],
                    WhichJobsSupported = [WhichJobs.Completed],
                    PrinterUUID = "{6541A875-C511-4273-909F-18CFBB38D9D0}",
                    JobAccountIdDefault = "default-account",
                    JobAccountIdSupported = true,
                    JobAccountingUserIdDefault = "default-user",
                    JobAccountingUserIdSupported = true,
                    JobCancelAfterDefault = 0,
                    JobCancelAfterSupported = new SharpIpp.Protocol.Models.Range(0, 86400),
                    JobSpoolingSupported = JobSpooling.Automatic,
                    MaxPageRangesSupported = 10,
                    PrintContentOptimizeDefault = PrintContentOptimize.Text,
                    PrintContentOptimizeSupported = [PrintContentOptimize.Text, PrintContentOptimize.Graphic, PrintContentOptimize.Photo, PrintContentOptimize.TextAndGraphic],
                    OutputDeviceSupported = ["printer-1", "printer-2"],
                    JobCreationAttributesSupported = ["copies", "finishings", "media"],
                    PrinterRequestedClientType = [ClientType.Application, ClientType.Driver]
                },
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["detail1"],
                    DocumentAccessError = "none"
                }
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
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        // Act
        GetPrinterAttributesResponse? clientResponse = await client.GetPrinterAttributesAsync(clientRequest);
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task HoldJobAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        SharpIppServer server = new();
        HoldJobRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
                JobId = 1,
                JobUri = new Uri("http://127.0.0.1:631/jobs/1"),
                Message = "message",
                JobHoldUntil = JobHoldUntil.Indefinite,
                JobHoldUntilTime = new DateTimeOffset(2024, 7, 1, 12, 0, 0, TimeSpan.Zero),
            },
        };
        IIppRequest? serverRequest = null;
        HoldJobResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new HoldJobResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["detail1"],
                    DocumentAccessError = "none"
                },
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
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        // Act
        HoldJobResponse? clientResponse = await client.HoldJobAsync(clientRequest);
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
    [TestMethod()]
    public async Task PausePrinterAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        SharpIppServer server = new();
        PausePrinterRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
            },
        };
        IIppRequest? serverRequest = null;
        PausePrinterResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new PausePrinterResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["detail1"],
                    DocumentAccessError = "none"
                },
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
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        // Act
        PausePrinterResponse? clientResponse = await client.PausePrinterAsync(clientRequest);
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task PurgeJobsAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        SharpIppServer server = new();
        PurgeJobsRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
            },
        };
        IIppRequest? serverRequest = null;
        PurgeJobsResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new PurgeJobsResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["detail1"],
                    DocumentAccessError = "none"
                },
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
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        // Act
        PurgeJobsResponse? clientResponse = await client.PurgeJobsAsync(clientRequest);
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task ReleaseJobAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        SharpIppServer server = new();
        ReleaseJobRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
                JobId = 1,
                JobUri = new Uri("http://127.0.0.1:631/jobs/1"),
            },
        };
        IIppRequest? serverRequest = null;
        ReleaseJobResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new ReleaseJobResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["detail1"],
                    DocumentAccessError = "none"
                },
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
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        // Act
        ReleaseJobResponse? clientResponse = await client.ReleaseJobAsync(clientRequest);
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);

    }

    [TestMethod()]
    public async Task ResumePrinterAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        SharpIppServer server = new();
        ResumePrinterRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
            },
        };
        IIppRequest? serverRequest = null;
        ResumePrinterResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new ResumePrinterResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["detail1"],
                    DocumentAccessError = "none"
                },
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
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        // Act
        ResumePrinterResponse? clientResponse = await client.ResumePrinterAsync(clientRequest);
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task RestartJobAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        SharpIppServer server = new();
        RestartJobRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
                JobId = 1,
                JobUri = new Uri("http://127.0.0.1:631/jobs/1"),
                Message = "message",
                JobHoldUntil = JobHoldUntil.Indefinite,
            },
        };
        IIppRequest? serverRequest = null;
        RestartJobResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new RestartJobResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["detail1"],
                    DocumentAccessError = "none"
                },
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
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        // Act
        RestartJobResponse? clientResponse = await client.RestartJobAsync(clientRequest);
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task SendUriAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        SharpIppServer server = new();
        SendUriRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
                JobId = 1,
                JobUri = new Uri("http://127.0.0.1:631/jobs/1"),
                DocumentName = "test-document",
                Compression = Compression.None,
                DocumentFormat = "application/pdf",
                DocumentNaturalLanguage = "en",
                DocumentCharset = "utf-8",
                LastDocument = true,
                DocumentUri = new Uri("ftp://document.pdf"),
                DocumentAccess = new DocumentAccess
                {
                    AccessUserName = "ftp-user",
                    AccessPassword = "ftp-pass",
                    AccessOAuthUri = "https://auth.example.com/token",
                    AccessOAuthToken = "token",
                    AccessPin = "1234",
                    AccessX509Certificate = "-----BEGIN CERTIFICATE-----"
                },
            },
            DocumentTemplateAttributes = new()
            {
                Copies = 1,
            }
        };
        IIppRequest? serverRequest = null;
        SendUriResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new SendUriResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["detail1"],
                    DocumentAccessError = "none"
                },
                JobAttributes = new()
                {
                    JobState = JobState.Pending,
                    JobStateReasons = [JobStateReason.None],
                    JobStateMessage = "pending",
                    NumberOfInterveningJobs = 0,
                    JobId = 456,
                    JobUri = "http://127.0.0.1:631/456",
                    ClientInfo = [new ClientInfo { ClientName = "SharpIppTests", ClientType = ClientType.Application }],
                    JobImpressionsCompletedCol = new JobCounter { Monochrome = 5 },
                    JobMediaSheetsCompletedCol = new JobCounter { Monochrome = 4 },
                    JobPagesCompleted = 3,
                    JobPagesCompletedCol = new JobCounter { Monochrome = 3 },
                    JobProcessingTime = 120
                },
                DocumentAttributes = new()
                {
                    DocumentNumber = 1,
                    DocumentState = DocumentState.Pending,
                    DocumentStateReasons = [DocumentStateReason.None],
                    DocumentStateMessage = "pending",
                    IsNoValue = false
                }
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
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        // Act
        SendUriResponse? clientResponse = await client.SendUriAsync(clientRequest);
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
    [TestMethod()]
    public async Task ValidateJobAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        using MemoryStream memoryStream = new();
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
                JobName = "Test Job",
                IppAttributeFidelity = true,
                JobImpressions = 5,
                JobMediaSheets = 2,
                DocumentCharset = "utf-8",
                JobPassword = "hashed-secret",
                JobPasswordEncryption = JobPasswordEncryption.Sha2256,
                JobReleaseAction = JobReleaseAction.JobPassword,
                JobAuthorizationUri = new Uri("https://example.local/auth/validate"),
                JobImpressionsEstimated = 11,
                ChargeInfoMessage = "charge preview",
                ProofCopies = 2,
            },
            JobTemplateAttributes = new()
            {
                Copies = 1,
                JobPriority = 1,
                JobHoldUntil = JobHoldUntil.NoHold,
                MultipleDocumentHandling = MultipleDocumentHandling.SeparateDocumentsUncollatedCopies,
                Finishings = new[] { Finishings.None },
                PageRanges = [new SharpIpp.Protocol.Models.Range(1, 2)],
                Sides = Sides.OneSided,
                NumberUp = 1,
                OrientationRequested = Orientation.Portrait,
                Media = (Media)"iso_a4_210x297mm",
                PrinterResolution = new Resolution(600, 600, ResolutionUnit.DotsPerInch),
                PrintQuality = PrintQuality.Normal,
                PrintScaling = PrintScaling.Auto,
                PrintColorMode = PrintColorMode.Color,
                MediaCol = new MediaCol
                {
                    MediaBackCoating = MediaCoating.Glossy,
                    MediaBottomMargin = 10,
                    MediaColor = (MediaColor)"white",
                    MediaFrontCoating = MediaCoating.Glossy,
                    MediaGrain = MediaGrain.XDirection,
                    MediaHoleCount = 0,
                    MediaInfo = "test",
                    MediaKey = (MediaKey)"test",
                    MediaLeftMargin = 10,
                    MediaOrderCount = 1,
                    MediaPrePrinted = MediaPrePrinted.Blank,
                    MediaRecycled = MediaRecycled.None,
                    MediaRightMargin = 10,
                    MediaSize = new MediaSize { XDimension = 21000, YDimension = 29700 }, // 1/100 mm for A4
                    MediaSizeName = (Media)"iso_a4_210x297mm",
                    MediaSource = MediaSource.Main,
                    MediaSourceProperties = new MediaSourceProperties { MediaSourceFeedDirection = MediaSourceFeedDirection.LongEdgeFirst, MediaSourceFeedOrientation = Orientation.Portrait },
                    MediaThickness = 10,
                    MediaTooth = MediaTooth.Medium,
                    MediaTopMargin = 10,
                    MediaType = (MediaType)"stationery",
                },
            }
        };
        IIppRequest? serverRequest = null;
        ValidateJobResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new ValidateJobResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["detail1"],
                    DocumentAccessError = "none"
                },
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
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        // Act
        ValidateJobResponse? clientResponse = await client.ValidateJobAsync(clientRequest);
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
    [TestMethod()]
    public async Task GetCUPSPrintersAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        SharpIppServer server = new();
        CUPSGetPrintersRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
                FirstPrinterName = "printer-1",
                Limit = 10,
                PrinterId = 1,
                PrinterLocation = "Office",
                PrinterType = PrinterType.DefaultPrinter,
                PrinterTypeMask = PrinterType.DefaultPrinter,
                RequestedAttributes = ["printer-name", "printer-uri", "printer-state"],
            },
        };
        IIppRequest? serverRequest = null;
        CUPSGetPrintersResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = (await server.ReceiveRequestAsync(s, c));
            serverResponse = new CUPSGetPrintersResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = ["detail1"],
                    DocumentAccessError = "none"
                },
                PrintersAttributes = 
                [
                    new PrinterDescriptionAttributes
                    {
                        PrinterUriSupported = ["http://127.0.0.1:631"],
                        UriSecuritySupported = [UriSecurity.None],
                        UriAuthenticationSupported = [UriAuthentication.None],
                        PrinterName = "printer-1",
                        PrinterLocation = "Office",
                        PrinterInfo = "Test Printer Info",
                        PrinterMoreInfo = "http://127.0.0.1:631",
                        PrinterDriverInstaller = "installer",
                        PrinterMakeAndModel = "SharpIpp Virtual Printer",
                        PrinterMoreInfoManufacturer = "http://manufacturer.com",
                        PrinterState = PrinterState.Idle,
                        PrinterStateReasons = [PrinterStateReason.None],
                        PrinterStateMessage = "Idle",
                        IppVersionsSupported = [default],
                        OperationsSupported = [IppOperation.PrintJob],
                        MultipleDocumentJobsSupported = true,
                        MultipleDocumentHandlingDefault = MultipleDocumentHandling.SeparateDocumentsUncollatedCopies,
                        MultipleDocumentHandlingSupported = [MultipleDocumentHandling.SeparateDocumentsUncollatedCopies, MultipleDocumentHandling.SingleDocument],
                        CharsetConfigured = "utf-8",
                        CharsetSupported = ["utf-8"],
                        NaturalLanguageConfigured = "en-us",
                        GeneratedNaturalLanguageSupported = ["en-us"],
                        DocumentFormatDefault = "application/pdf",
                        DocumentFormatSupported = ["application/pdf"],
                        PrinterIsAcceptingJobs = true,
                        QueuedJobCount = 0,
                        PrinterMessageFromOperator = "message",
                        ColorSupported = true,
                        ReferenceUriSchemesSupported = [UriScheme.Ftp],
                        PdlOverrideSupported = PdlOverride.Attempted,
                        PrinterUpTime = 100,
                        PrinterCurrentTime = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
                        MultipleOperationTimeOut = 10,
                        CompressionSupported = [Compression.None],
                        JobKOctetsSupported = new SharpIpp.Protocol.Models.Range(1, 100),
                        JpegKOctetsSupported = new SharpIpp.Protocol.Models.Range(1, 100),
                        PdfKOctetsSupported = new SharpIpp.Protocol.Models.Range(1, 100),
                        JobImpressionsSupported = new SharpIpp.Protocol.Models.Range(1, 100),
                        JobMediaSheetsSupported = new SharpIpp.Protocol.Models.Range(1, 100),
                        JobSheetsDefault = JobSheets.None,
                        JobSheetsSupported = [JobSheets.None, JobSheets.Standard],
                        NumberUpDefault = 1,
                        NumberUpSupported = [new SharpIpp.Protocol.Models.Range(1, 4)],
                        PagesPerMinute = 10,
                        PagesPerMinuteColor = 10,
                        PrintScalingDefault = PrintScaling.Auto,
                        PrintScalingSupported = [PrintScaling.Auto],
                        MediaDefault = (Media)"iso_a4_210x297mm",
                        MediaSupported = [(Media)"iso_a4_210x297mm"],
                        MediaReady = [(Media)"iso_a4_210x297mm", (Media)"na_letter_8.5x11in"],
                        SidesDefault = Sides.OneSided,
                        SidesSupported = [Sides.OneSided],
                        FinishingsDefault = Finishings.None,
                        FinishingsSupported = [Finishings.None],
                        PrinterResolutionDefault = new Resolution(600, 600, ResolutionUnit.DotsPerInch),
                        PrinterResolutionSupported = [new Resolution(600, 600, ResolutionUnit.DotsPerInch)],
                        PrintQualityDefault = PrintQuality.Normal,
                        PrintQualitySupported = [PrintQuality.Normal],
                        JobPriorityDefault = 1,
                        JobPrioritySupported = 1,
                        CopiesDefault = 1,
                        CopiesSupported = new SharpIpp.Protocol.Models.Range(1, 100),
                        OrientationRequestedDefault = Orientation.Portrait,
                        OrientationRequestedSupported = [Orientation.Portrait],
                        PageRangesSupported = true,
                        JobHoldUntilSupported = [JobHoldUntil.NoHold],
                        JobHoldUntilDefault = JobHoldUntil.NoHold,
                        OutputBinDefault = (OutputBin)"face-down",
                        OutputBinSupported = [(OutputBin)"face-down"],
                        MediaColDefault =  new MediaCol
                        {
                            MediaBackCoating = MediaCoating.Glossy,
                            MediaBottomMargin = 10,
                            MediaColor = (MediaColor)"white",
                            MediaFrontCoating = MediaCoating.Glossy,
                            MediaGrain = MediaGrain.XDirection,
                            MediaHoleCount = 0,
                            MediaInfo = "test",
                            MediaKey = (MediaKey)"test",
                            MediaLeftMargin = 10,
                            MediaOrderCount = 1,
                            MediaPrePrinted = MediaPrePrinted.Blank,
                            MediaRecycled = MediaRecycled.None,
                            MediaRightMargin = 10,
                            MediaSize = new MediaSize { XDimension = 21000, YDimension = 29700 }, // 1/100 mm for A4
                            MediaSizeName = (Media)"iso_a4_210x297mm",
                            MediaSource = MediaSource.Main,
                            MediaSourceProperties = new MediaSourceProperties { MediaSourceFeedDirection = MediaSourceFeedDirection.LongEdgeFirst, MediaSourceFeedOrientation = Orientation.Portrait },
                            MediaThickness = 10,
                            MediaTooth = MediaTooth.Medium,
                            MediaTopMargin = 10,
                            MediaType = (MediaType)"stationery",
                            MediaWeightMetric = 80
                        },
                        PrintColorModeDefault = PrintColorMode.Color,
                        PrintColorModeSupported = [PrintColorMode.Color],
                        WhichJobsSupported = [WhichJobs.Completed],
                        PrinterUUID = "{6541A875-C511-4273-909F-18CFBB38D9D0}",
                        DocumentCreationAttributesSupported = ["copies", "finishings", "sides"],
                        JobAccountIdDefault = "account-123",
                        JobAccountTypeDefault = JobAccountType.General,
                        JobAccountTypeSupported = [JobAccountType.General, JobAccountType.None],
                        JobAccountIdSupported = true,
                        JobAccountingUserIdDefault = "user-456",
                        JobAccountingUserIdSupported = true,
                        JobPasswordEncryptionSupported = [JobPasswordEncryption.None, JobPasswordEncryption.Sha2256],
                        JobAuthorizationUriSupported = true,
                        PrinterChargeInfo = "charge-info",
                        PrinterChargeInfoUri = "https://example.local/printer-charge-info",
                        PrinterMandatoryJobAttributes = ["job-name", "copies"],
                        JobCancelAfterDefault = 3600,
                        JobCancelAfterSupported = new SharpIpp.Protocol.Models.Range(10, 3600),
                        JobSpoolingSupported = JobSpooling.Automatic,
                        MaxPageRangesSupported = 3,
                        PrintContentOptimizeDefault = PrintContentOptimize.Text,
                        PrintContentOptimizeSupported = [PrintContentOptimize.Text, PrintContentOptimize.TextAndGraphic],
                        OutputDeviceSupported = ["device-1"],
                        OutputDeviceUuidSupported = ["urn:uuid:123e4567-e89b-12d3-a456-426614174000"],
                        JobCreationAttributesSupported = ["copies"],
                        PunchingHoleDiameterConfigured = 5,
                        PrinterFinisher = ["finisher=1", "finisher=2"],
                        PrinterFinisherDescription = ["stapler"],
                        PrinterFinisherSupplies = ["marker-supply=1", "marker-supply=2"],
                        PrinterFinisherSuppliesDescription = ["blue ink"],
                        FinishingTemplateSupported = [(FinishingTemplate)"staple", (FinishingTemplate)"punch"],
                        FinishingsColSupported = ["baling", "binding", "coating", "covering", "folding", "laminating", "punching", "stitching", "trimming"],
                        JobPagesPerSetSupported = true,
                        FinishingsColDefault = [new FinishingsCol { FinishingTemplate = (FinishingTemplate)"staple" }],
                        FinishingsColReady = [new FinishingsCol { FinishingTemplate = (FinishingTemplate)"punch" }],
                        BalingTypeSupported = [BalingType.Band, BalingType.Wrap],
                        BalingWhenSupported = [BalingWhen.AfterJob],
                        BindingReferenceEdgeSupported = [FinishingReferenceEdge.Left],
                        BindingTypeSupported = [BindingType.Adhesive],
                        CoatingSidesSupported = [CoatingSides.Both],
                        CoatingTypeSupported = [CoatingType.Glossy],
                        CoveringNameSupported = [CoveringName.Plain],
                        FinishingsColDatabase = [new FinishingsCol { FinishingTemplate = (FinishingTemplate)"staple" }],
                        FoldingDirectionSupported = [FoldingDirection.Inward],
                        FoldingOffsetSupported = [new SharpIpp.Protocol.Models.Range(1, 10)],
                        FoldingReferenceEdgeSupported = [FinishingReferenceEdge.Top],
                        LaminatingSidesSupported = [CoatingSides.Both],
                        LaminatingTypeSupported = [LaminatingType.Archival],
                        PunchingLocationsSupported = [new SharpIpp.Protocol.Models.Range(10, 20)],
                        PunchingOffsetSupported = [new SharpIpp.Protocol.Models.Range(0, 100)],
                        PunchingReferenceEdgeSupported = [FinishingReferenceEdge.Left],
                        StitchingAngleSupported = [new SharpIpp.Protocol.Models.Range(0, 90)],
                        StitchingLocationsSupported = [new SharpIpp.Protocol.Models.Range(10, 20)],
                        StitchingMethodSupported = [StitchingMethod.Wire],
                        StitchingOffsetSupported = [new SharpIpp.Protocol.Models.Range(0, 5)],
                        StitchingReferenceEdgeSupported = [FinishingReferenceEdge.Bottom],
                        TrimmingOffsetSupported = [new SharpIpp.Protocol.Models.Range(0, 2)],
                        TrimmingReferenceEdgeSupported = [FinishingReferenceEdge.Right],
                        TrimmingTypeSupported = [TrimmingType.DrawLine, TrimmingType.Full, TrimmingType.Partial],
                        TrimmingWhenSupported = [TrimmingWhen.AfterDocuments, TrimmingWhen.AfterSheets, TrimmingWhen.AfterSets],
                        CoverBackDefault = new Cover { CoverType = CoverType.PrintBack, Media = (Media)"iso_a4_210x297mm" },
                        CoverBackSupported = [(Media)"iso_a4_210x297mm"],
                        CoverFrontDefault = new Cover { CoverType = CoverType.PrintFront, Media = (Media)"iso_a4_210x297mm" },
                        CoverFrontSupported = [(Media)"iso_a4_210x297mm"],
                        CoverTypeSupported = [CoverType.PrintBoth],
                        ForceFrontSideSupported = new SharpIpp.Protocol.Models.Range(1, 10),
                        ImageOrientationDefault = Orientation.Portrait,
                        ImageOrientationSupported = [Orientation.Portrait],
                        ImpositionTemplateDefault = (ImpositionTemplate)"stamp",
                        ImpositionTemplateSupported = [(ImpositionTemplate)"stamp"],
                        InsertCountSupported = new SharpIpp.Protocol.Models.Range(1, 10),
                        InsertSheetDefault = [new InsertSheet { InsertAfterPageNumber = 1, InsertCount = 1 }],
                        InsertSheetSupported = [(Media)"iso_a4_210x297mm"],
                        JobAccountingOutputBinSupported = [(OutputBin)"face-down"],
                        JobAccountingSheetsDefault = new JobAccountingSheets { JobAccountingOutputBin = (OutputBin)"face-down", JobAccountingSheetsType = JobSheetsType.Standard },
                        JobAccountingSheetsSupported = [(Media)"iso_a4_210x297mm"],
                        JobAccountingSheetsTypeSupported = [JobSheetsType.Standard],
                        JobCompleteBeforeSupported = [JobHoldUntil.NoHold],
                        JobCompleteBeforeTimeSupported = true,
                        JobErrorSheetDefault = new JobErrorSheet { JobErrorSheetType = JobSheetsType.Standard, JobErrorSheetWhen = JobErrorSheetWhen.Always },
                        JobErrorSheetSupported = [(Media)"iso_a4_210x297mm"],
                        JobErrorSheetTypeSupported = [JobSheetsType.Standard],
                        JobErrorSheetWhenSupported = [JobErrorSheetWhen.Always],
                        JobMessageToOperatorSupported = true,
                        JobPhoneNumberDefault = "123456789",
                        JobPhoneNumberSchemeSupported = [(JobPhoneNumberScheme)"tel"],
                        JobPhoneNumberSupported = true,
                        JobRecipientNameSupported = true,
                        JobSheetMessageSupported = true,
                        PageDeliveryDefault = PageDelivery.SystemSpecified,
                        PageDeliverySupported = [PageDelivery.SystemSpecified, PageDelivery.SameOrderFaceUp],
                        PresentationDirectionNumberUpDefault = PresentationDirectionNumberUp.TobottomToleft,
                        PresentationDirectionNumberUpSupported = [PresentationDirectionNumberUp.TobottomToleft],
                        SeparatorSheetsDefault = new SeparatorSheets { SeparatorSheetsType = [SeparatorSheetsType.SlipSheets] },
                        SeparatorSheetsSupported = ["iso_a4_210x297mm"],
                        SeparatorSheetsTypeSupported = [SeparatorSheetsType.SlipSheets],
                        XImagePositionDefault = XImagePosition.Center,
                        XImagePositionSupported = [XImagePosition.Center],
                        XImageShiftDefault = 1,
                        XImageShiftSupported = new SharpIpp.Protocol.Models.Range(0, 10),
                        XSide1ImageShiftDefault = 1,
                        XSide2ImageShiftDefault = 1,
                        YImagePositionDefault = YImagePosition.Center,
                        YImagePositionSupported = [YImagePosition.Center],
                        YImageShiftDefault = 1,
                        YImageShiftSupported = new SharpIpp.Protocol.Models.Range(0, 10),
                        YSide1ImageShiftDefault = 1,
                        YSide2ImageShiftDefault = 1,
                    }
                ]
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
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        // Act
        CUPSGetPrintersResponse? clientResponse = await client.GetCUPSPrintersAsync(clientRequest);
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod()]
    public async Task GetPrinterAttributesAsync_NoValueEverywhere_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        // Arrange
        SharpIppServer server = new();
        GetPrinterAttributesRequest clientRequest = new()
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
                RequestedAttributes = ["printer-state", "queued-job-count"],
                DocumentFormat = "application/pdf",
            },
        };

        IIppRequestMessage? serverRawRequest = null;
        GetPrinterAttributesResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRawRequest = (await server.ReceiveRawRequestAsync(s, c));
            
            // Set up sever raw response with NoValue for specific properties 
            // the user requested to be mapped to MinValue/default instead of null
            var serverRawResponse = new IppResponseMessage
            {
                RequestId = serverRawRequest.RequestId,
                Version = serverRawRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
            };
            
            serverRawResponse.OperationAttributes.Add([
                new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"),
                new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en"),
                new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.StatusMessage, "successful-ok"),
                new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.DetailedStatusMessage, "detail1"),
                new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.DocumentAccessError, "none")
            ]);
            
            serverRawResponse.PrinterAttributes.Add([
                new IppAttribute(Tag.NoValue, PrinterAttribute.PrinterName, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PrinterLocation, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PrinterInfo, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.QueuedJobCount, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PrinterCurrentTime, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PrinterState, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.JobKOctetsSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PrinterResolutionDefault, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.MediaColDefault, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.MultipleDocumentJobsSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PrinterIsAcceptingJobs, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.ColorSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PrinterUpTime, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.MultipleOperationTimeOut, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.JpegKOctetsSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PdfKOctetsSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.JobImpressionsSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.JobMediaSheetsSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PagesPerMinute, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PagesPerMinuteColor, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PrinterResolutionSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.JobPriorityDefault, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.JobPrioritySupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.CopiesDefault, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.CopiesSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PageRangesSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.UriSecuritySupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.UriAuthenticationSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.OperationsSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.ReferenceUriSchemesSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.CompressionSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PrintScalingDefault, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PrintScalingSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.SidesDefault, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.SidesSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.FinishingsDefault, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.FinishingsSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PrintQualityDefault, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PrintQualitySupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.OrientationRequestedDefault, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.OrientationRequestedSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.JobHoldUntilSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.JobHoldUntilDefault, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PrintColorModeDefault, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PrintColorModeSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.WhichJobsSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.BalingTypeSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.BalingWhenSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.BindingReferenceEdgeSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.BindingTypeSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.CoatingSidesSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.CoatingTypeSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.CoveringNameSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.FinishingsColDatabase, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.FoldingDirectionSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.FoldingOffsetSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.FoldingReferenceEdgeSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.LaminatingSidesSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.LaminatingTypeSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PunchingLocationsSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PunchingOffsetSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.PunchingReferenceEdgeSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.StitchingAngleSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.StitchingLocationsSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.StitchingMethodSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.StitchingOffsetSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.StitchingReferenceEdgeSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.TrimmingOffsetSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.TrimmingReferenceEdgeSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.TrimmingTypeSupported, NoValue.Instance),
                new IppAttribute(Tag.NoValue, PrinterAttribute.TrimmingWhenSupported, NoValue.Instance)
            ]);

            var memoryStream = new MemoryStream();
            await server.SendRawResponseAsync(serverRawResponse, memoryStream, c);
            memoryStream.Seek(0, SeekOrigin.Begin);
            
            memoryStream.Position = 0;
            // The client parses the response, not the server
            // But we don't have the client instance here yet.
            // Let's just return the stream directly and let the client parse it naturally via the HTTP handler.
            memoryStream.Position = 0;
            return new HttpResponseMessage()
            {
                StatusCode = statusCode,
                Content = new StreamContent(memoryStream)
            };
        }

        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));
        
        // Let's create an expected serverResponse directly in code logic to compare.
        serverResponse = new GetPrinterAttributesResponse
        {
            RequestId = 123,
            Version = new IppVersion(2, 0),
            StatusCode = IppStatusCode.SuccessfulOk,
            OperationAttributes = new()
            {
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en",
                StatusMessage = "successful-ok",
                DetailedStatusMessage = ["detail1"],
                DocumentAccessError = "none"
            },
            PrinterAttributes = new()
            {
                MultipleDocumentJobsSupported = NoValue.GetNoValue<bool?>(),
                PrinterIsAcceptingJobs = NoValue.GetNoValue<bool?>(),
                ColorSupported = NoValue.GetNoValue<bool?>(),
                PrinterUpTime = NoValue.GetNoValue<int>(),
                MultipleOperationTimeOut = NoValue.GetNoValue<int>(),
                JpegKOctetsSupported = NoValue.GetNoValue<SharpIpp.Protocol.Models.Range>(),
                PdfKOctetsSupported = NoValue.GetNoValue<SharpIpp.Protocol.Models.Range>(),
                JobImpressionsSupported = NoValue.GetNoValue<SharpIpp.Protocol.Models.Range>(),
                JobMediaSheetsSupported = NoValue.GetNoValue<SharpIpp.Protocol.Models.Range>(),
                PagesPerMinute = NoValue.GetNoValue<int>(),
                PagesPerMinuteColor = NoValue.GetNoValue<int>(),
                PrinterResolutionSupported = [NoValue.GetNoValue<Resolution>()],
                JobPriorityDefault = NoValue.GetNoValue<int>(),
                JobPrioritySupported = NoValue.GetNoValue<int>(),
                CopiesDefault = NoValue.GetNoValue<int>(),
                CopiesSupported = NoValue.GetNoValue<SharpIpp.Protocol.Models.Range>(),
                PageRangesSupported = NoValue.GetNoValue<bool?>(),

                PrinterName = NoValue.GetNoValue<string>(),
                PrinterLocation = NoValue.GetNoValue<string>(),
                PrinterInfo = NoValue.GetNoValue<string>(),
                QueuedJobCount = NoValue.GetNoValue<int>(),
                PrinterCurrentTime = NoValue.GetNoValue<DateTimeOffset>(),
                PrinterState = NoValue.GetNoValue<PrinterState>(),
                JobKOctetsSupported = NoValue.GetNoValue<SharpIpp.Protocol.Models.Range>(),
                PrinterResolutionDefault = NoValue.GetNoValue<Resolution>(),
                UriSecuritySupported = [NoValue.GetNoValue<UriSecurity>()],
                UriAuthenticationSupported = [NoValue.GetNoValue<UriAuthentication>()],
                OperationsSupported = [NoValue.GetNoValue<IppOperation>()],
                ReferenceUriSchemesSupported = [NoValue.GetNoValue<UriScheme>()],
                CompressionSupported = [NoValue.GetNoValue<Compression>()],
                PrintScalingDefault = NoValue.GetNoValue<PrintScaling>(),
                PrintScalingSupported = [NoValue.GetNoValue<PrintScaling>()],
                SidesDefault = NoValue.GetNoValue<Sides>(),
                SidesSupported = [NoValue.GetNoValue<Sides>()],
                FinishingsDefault = NoValue.GetNoValue<Finishings>(),
                FinishingsSupported = [NoValue.GetNoValue<Finishings>()],
                PrintQualityDefault = NoValue.GetNoValue<PrintQuality>(),
                PrintQualitySupported = [NoValue.GetNoValue<PrintQuality>()],
                OrientationRequestedDefault = NoValue.GetNoValue<Orientation>(),
                OrientationRequestedSupported = [NoValue.GetNoValue<Orientation>()],
                JobHoldUntilSupported = [NoValue.GetNoValue<JobHoldUntil>()],
                JobHoldUntilDefault = NoValue.GetNoValue<JobHoldUntil>(),
                PrintColorModeDefault = NoValue.GetNoValue<PrintColorMode>(),
                PrintColorModeSupported = [NoValue.GetNoValue<PrintColorMode>()],
                WhichJobsSupported = [NoValue.GetNoValue<WhichJobs>()],
                BalingTypeSupported = [NoValue.GetNoValue<BalingType>()],
                BalingWhenSupported = [NoValue.GetNoValue<BalingWhen>()],
                BindingReferenceEdgeSupported = [NoValue.GetNoValue<FinishingReferenceEdge>()],
                BindingTypeSupported = [NoValue.GetNoValue<BindingType>()],
                CoatingSidesSupported = [NoValue.GetNoValue<CoatingSides>()],
                CoatingTypeSupported = [NoValue.GetNoValue<CoatingType>()],
                CoveringNameSupported = [NoValue.GetNoValue<CoveringName>()],
                FinishingsColDatabase = [NoValue.GetNoValue<FinishingsCol>()],
                FoldingDirectionSupported = [NoValue.GetNoValue<FoldingDirection>()],
                FoldingOffsetSupported = [NoValue.GetNoValue<SharpIpp.Protocol.Models.Range>()],
                FoldingReferenceEdgeSupported = [NoValue.GetNoValue<FinishingReferenceEdge>()],
                LaminatingSidesSupported = [NoValue.GetNoValue<CoatingSides>()],
                LaminatingTypeSupported = [NoValue.GetNoValue<LaminatingType>()],
                PunchingLocationsSupported = [NoValue.GetNoValue<SharpIpp.Protocol.Models.Range>()],
                PunchingOffsetSupported = [NoValue.GetNoValue<SharpIpp.Protocol.Models.Range>()],
                PunchingReferenceEdgeSupported = [NoValue.GetNoValue<FinishingReferenceEdge>()],
                StitchingAngleSupported = [NoValue.GetNoValue<SharpIpp.Protocol.Models.Range>()],
                StitchingLocationsSupported = [NoValue.GetNoValue<SharpIpp.Protocol.Models.Range>()],
                StitchingMethodSupported = [NoValue.GetNoValue<StitchingMethod>()],
                StitchingOffsetSupported = [NoValue.GetNoValue<SharpIpp.Protocol.Models.Range>()],
                StitchingReferenceEdgeSupported = [NoValue.GetNoValue<FinishingReferenceEdge>()],
                TrimmingOffsetSupported = [NoValue.GetNoValue<SharpIpp.Protocol.Models.Range>()],
                TrimmingReferenceEdgeSupported = [NoValue.GetNoValue<FinishingReferenceEdge>()],
                TrimmingTypeSupported = [NoValue.GetNoValue<TrimmingType>()],
                TrimmingWhenSupported = [NoValue.GetNoValue<TrimmingWhen>()],
                MediaColDefault = NoValue.GetNoValue<MediaCol>()
            }
        };
        
        // Act
        var clientRawRequest = client.CreateRawRequest(clientRequest);
        GetPrinterAttributesResponse? clientResponse = await client.GetPrinterAttributesAsync(clientRequest);
        
        // Assert
        clientRawRequest.Should().NotBeNull().And.BeEquivalentTo(serverRawRequest, options => options.Excluding(x => x!.Document));
        clientResponse.Should().BeEquivalentTo(serverResponse);
        
        // Explicitly check for "NoValue" mapped min/default values
        clientResponse.Should().NotBeNull();
        clientResponse!.PrinterAttributes.PrinterName.Should().Be(NoValue.GetNoValue<string>());
        clientResponse.PrinterAttributes.PrinterLocation.Should().Be(NoValue.GetNoValue<string>());
        clientResponse.PrinterAttributes.PrinterInfo.Should().Be(NoValue.GetNoValue<string>());
        clientResponse.PrinterAttributes.QueuedJobCount.Should().Be(NoValue.GetNoValue<int>());
        clientResponse.PrinterAttributes.PrinterCurrentTime.Should().Be(NoValue.GetNoValue<DateTimeOffset>());
        clientResponse.PrinterAttributes.PrinterState.Should().Be(NoValue.GetNoValue<PrinterState>());
        clientResponse.PrinterAttributes.JobKOctetsSupported.Should().NotBeNull();
        clientResponse.PrinterAttributes.JobKOctetsSupported.HasValue.Should().BeTrue();
        clientResponse.PrinterAttributes.JobKOctetsSupported!.Value.Lower.Should().Be(NoValue.GetNoValue<SharpIpp.Protocol.Models.Range>().Lower);
        clientResponse.PrinterAttributes.JobKOctetsSupported!.Value.Upper.Should().Be(NoValue.GetNoValue<SharpIpp.Protocol.Models.Range>().Upper);
        clientResponse.PrinterAttributes.PrinterResolutionDefault.Should().NotBeNull();
        clientResponse.PrinterAttributes.PrinterResolutionDefault.HasValue.Should().BeTrue();
        clientResponse.PrinterAttributes.PrinterResolutionDefault!.Value.Width.Should().Be(NoValue.GetNoValue<Resolution>().Width);
        clientResponse.PrinterAttributes.PrinterResolutionDefault!.Value.Height.Should().Be(NoValue.GetNoValue<Resolution>().Height);
    }

    [TestMethod]
    public async Task AcknowledgeDocumentAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        var clientRequest = new AcknowledgeDocumentRequest
        {
            RequestId = 800,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                JobId = 10,
                DocumentNumber = 1,
                OutputDeviceUuid = new Uri("urn:uuid:123e4567-e89b-12d3-a456-426614174001"),
                FetchStatusCode = IppStatusCode.ClientErrorNotFound,
                FetchStatusMessage = "document-not-found"
            }
        };

        IIppRequest? serverRequest = null;
        AcknowledgeDocumentResponse? serverResponse = null;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer();
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new AcknowledgeDocumentResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new() { StatusMessage = "successful-ok" }
            };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        var clientResponse = await client.AcknowledgeDocumentAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod]
    public async Task AcknowledgeJobAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        var clientRequest = new AcknowledgeJobRequest
        {
            RequestId = 801,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                JobId = 11,
                OutputDeviceUuid = new Uri("urn:uuid:123e4567-e89b-12d3-a456-426614174002"),
                OutputDeviceJobStates = [JobState.Processing]
            }
        };

        IIppRequest? serverRequest = null;
        AcknowledgeJobResponse? serverResponse = null;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer();
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new AcknowledgeJobResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new() { StatusMessage = "successful-ok" }
            };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        var clientResponse = await client.AcknowledgeJobAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod]
    public async Task FetchDocumentAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        var clientRequest = new FetchDocumentRequest
        {
            RequestId = 802,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                JobId = 12,
                DocumentNumber = 1,
                CompressionAccepted = [Compression.None],
                DocumentFormatAccepted = ["application/pdf"]
            }
        };

        IIppRequest? serverRequest = null;
        FetchDocumentResponse? serverResponse = null;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer();
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new FetchDocumentResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new() { StatusMessage = "successful-ok" }
            };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        var clientResponse = await client.FetchDocumentAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod]
    public async Task GetOutputDeviceAttributesAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        var clientRequest = new GetOutputDeviceAttributesRequest
        {
            RequestId = 803,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                RequestedAttributes = ["all"],
                OutputDeviceUuid = new Uri("urn:uuid:123e4567-e89b-12d3-a456-426614174003")
            }
        };

        IIppRequest? serverRequest = null;
        GetOutputDeviceAttributesResponse? serverResponse = null;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer();
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new GetOutputDeviceAttributesResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new() { StatusMessage = "successful-ok" }
            };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        var clientResponse = await client.GetOutputDeviceAttributesAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod]
    public async Task UpdateActiveJobsAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        var clientRequest = new UpdateActiveJobsRequest
        {
            RequestId = 804,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                OutputDeviceUuid = new Uri("urn:uuid:123e4567-e89b-12d3-a456-426614174004"),
                OutputDeviceJobStates = [JobState.Pending, JobState.Processing]
            }
        };

        IIppRequest? serverRequest = null;
        UpdateActiveJobsResponse? serverResponse = null;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer();
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new UpdateActiveJobsResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new() { StatusMessage = "successful-ok" }
            };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        var clientResponse = await client.UpdateActiveJobsAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod]
    public async Task AllocatePrinterResourcesAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        var clientRequest = new AllocatePrinterResourcesRequest
        {
            RequestId = 805,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                SystemUri = new Uri("ipp://127.0.0.1:8631/system"),
                PrinterId = 99,
                ResourceIds = [1, 2]
            }
        };

        IIppRequest? serverRequest = null;
        AllocatePrinterResourcesResponse? serverResponse = null;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer();
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new AllocatePrinterResourcesResponse { RequestId = serverRequest.RequestId, Version = serverRequest.Version, StatusCode = IppStatusCode.SuccessfulOk, PrinterResourceIds = [1, 2] };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        var clientResponse = await client.AllocatePrinterResourcesAsync(clientRequest);
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod]
    public async Task CreatePrinterAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        var clientRequest = new CreatePrinterRequest
        {
            RequestId = 806,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                SystemUri = new Uri("ipp://127.0.0.1:8631/system"),
                ResourceIds = [10],
                PrinterServiceType = ["print-ws"],
                PrinterXriRequested = ["xri-security"]
            }
        };

        IIppRequest? serverRequest = null;
        CreatePrinterResponse? serverResponse = null;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer();
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new CreatePrinterResponse { RequestId = serverRequest.RequestId, Version = serverRequest.Version, StatusCode = IppStatusCode.SuccessfulOk };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        var clientResponse = await client.CreatePrinterAsync(clientRequest);
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod]
    public async Task GetSystemAttributesAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        var clientRequest = new GetSystemAttributesRequest
        {
            RequestId = 807,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                SystemUri = new Uri("ipp://127.0.0.1:8631/system"),
                RequestedAttributes = ["system-description"]
            }
        };

        IIppRequest? serverRequest = null;
        GetSystemAttributesResponse? serverResponse = null;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer();
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new GetSystemAttributesResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                SystemAttributes = new()
                {
                    SystemState = PrinterState.Processing
                }
            };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        var clientResponse = await client.GetSystemAttributesAsync(clientRequest);
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod]
    public async Task CreateResponse_WhenMappingRawGetSystemAttributesResponse_ToSystemStatusAttributes_ReturnsExpectedProperties()
    {
        var clientRequest = new GetSystemAttributesRequest
        {
            RequestId = 807,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                SystemUri = new Uri("ipp://127.0.0.1:8631/system"),
                RequestedAttributes = ["system-state"]
            }
        };

        IIppRequest? serverRequest = null;
        GetSystemAttributesResponse? serverResponse = null;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer();
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new GetSystemAttributesResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                SystemAttributes = new()
                {
                    SystemState = PrinterState.Stopped
                }
            };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        var rawRequest = client.CreateRawRequest(clientRequest);
        var rawResponse = await client.SendAsync(clientRequest.OperationAttributes.PrinterUri, rawRequest);
        var clientResponse = client.CreateResponse(typeof(SystemStatusAttributes), rawResponse);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeOfType<SystemStatusAttributes>();
        clientResponse.Should().BeEquivalentTo(serverResponse!.SystemAttributes);
    }

    [TestMethod]
    public async Task GetSystemSupportedValuesAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        var clientRequest = new GetSystemSupportedValuesRequest
        {
            RequestId = 808,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                SystemUri = new Uri("ipp://127.0.0.1:8631/system"),
                RequestedAttributes = ["system-status"]
            }
        };

        IIppRequest? serverRequest = null;
        GetSystemSupportedValuesResponse? serverResponse = null;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer();
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new GetSystemSupportedValuesResponse { RequestId = serverRequest.RequestId, Version = serverRequest.Version, StatusCode = IppStatusCode.SuccessfulOk };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        var clientResponse = await client.GetSystemSupportedValuesAsync(clientRequest);
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod]
    public async Task RegisterOutputDeviceAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        var clientRequest = new RegisterOutputDeviceRequest
        {
            RequestId = 809,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                SystemUri = new Uri("ipp://127.0.0.1:8631/system"),
                OutputDeviceUuid = new Uri("urn:uuid:123e4567-e89b-12d3-a456-426614174999"),
                OutputDeviceX509Certificate = ["-----BEGIN CERTIFICATE-----"],
                OutputDeviceX509Request = ["-----BEGIN CERTIFICATE REQUEST-----"],
                PrinterServiceType = ["print-ws"],
                PrinterXriRequested = ["xri-security"]
            }
        };

        IIppRequest? serverRequest = null;
        RegisterOutputDeviceResponse? serverResponse = null;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer();
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new RegisterOutputDeviceResponse { RequestId = serverRequest.RequestId, Version = serverRequest.Version, StatusCode = IppStatusCode.SuccessfulOk };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        var clientResponse = await client.RegisterOutputDeviceAsync(clientRequest);
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

    [TestMethod]
    public async Task GetUserPrinterAttributesAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        var clientRequest = new GetUserPrinterAttributesRequest
        {
            RequestId = 810,
            Version = new IppVersion(2, 0),
            OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), RequestedAttributes = ["printer-name"] }
        };

        IIppRequest? serverRequest = null;
        GetUserPrinterAttributesResponse? serverResponse = null;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer();
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new GetUserPrinterAttributesResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                PrinterAttributes = new PrinterDescriptionAttributes { PrinterName = "UserScopedPrinter" }
            };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        var clientResponse = await client.GetUserPrinterAttributesAsync(clientRequest);
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }

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
                InputAttributes = new DocumentTemplateAttributes { Media = (Media)"iso_a4_210x297mm" }
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

    [TestMethod]
    public async Task GetNextDocumentDataAsync_WhenSendingRequestAndReceivingResponse_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
        var clientRequest = new GetNextDocumentDataRequest
        {
            RequestId = 812,
            Version = new IppVersion(2, 0),
            OperationAttributes = new() { PrinterUri = new Uri("http://127.0.0.1:631"), JobId = 21, DocumentDataWait = true }
        };

        IIppRequest? serverRequest = null;
        GetNextDocumentDataResponse? serverResponse = null;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            var server = new SharpIppServer();
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new GetNextDocumentDataResponse { RequestId = serverRequest.RequestId, Version = serverRequest.Version, StatusCode = IppStatusCode.SuccessfulOk };
            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        var clientResponse = await client.GetNextDocumentDataAsync(clientRequest);
        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}

