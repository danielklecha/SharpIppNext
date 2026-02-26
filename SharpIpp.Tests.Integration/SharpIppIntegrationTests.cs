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
                DocumentFormat = "application/pdf"
            },
            JobTemplateAttributes = new()
            {
                Copies = 1
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
            },
            JobTemplateAttributes = new()
            {
                Copies = 1,
                JobPriority = 1,
                JobSheets = JobSheets.None,
                JobHoldUntil = JobHoldUntil.NoHold,
                MultipleDocumentHandling = MultipleDocumentHandling.SeparateDocumentsUncollatedCopies,
                Finishings = Finishings.None,
                PageRanges = [new SharpIpp.Protocol.Models.Range(1, 2)],
                Sides = Sides.OneSided,
                NumberUp = 1,
                OrientationRequested = Orientation.Portrait,
                Media = "iso_a4_210x297mm",
                PrinterResolution = new Resolution(600, 600, ResolutionUnit.DotsPerInch),
                PrintQuality = PrintQuality.Normal,
                PrintScaling = PrintScaling.Auto,
                PrintColorMode = PrintColorMode.Color,
                MediaCol = new MediaCol
                {
                    MediaBackCoating = MediaCoating.Glossy,
                    MediaBottomMargin = 10,
                    MediaColor = "white",
                    MediaFrontCoating = MediaCoating.Glossy,
                    MediaGrain = MediaGrain.XDirection,
                    MediaHoleCount = 0,
                    MediaInfo = "test",
                    MediaKey = "test",
                    MediaLeftMargin = 10,
                    MediaOrderCount = 1,
                    MediaPrePrinted = MediaPrePrinted.Blank,
                    MediaRecycled = MediaRecycled.None,
                    MediaRightMargin = 10,
                    MediaSize = new MediaSize { XDimension = 21000, YDimension = 29700 }, // 1/100 mm for A4
                    MediaSizeName = "iso_a4_210x297mm",
                    MediaSource = MediaSource.Main,
                    MediaSourceProperties = new MediaSourceProperties { MediaSourceFeedDirection = MediaSourceFeedDirection.LongEdgeFirst, MediaSourceFeedOrientation = Orientation.Portrait },
                    MediaThickness = 10,
                    MediaTooth = MediaTooth.Medium,
                    MediaTopMargin = 10,
                    MediaType = "stationery",
                    MediaWeightMetric = 80
                }
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
                    JobUri = "http://127.0.0.1:631/456"
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
                Finishings = Finishings.None,
                PageRanges = [new SharpIpp.Protocol.Models.Range(1, 2)],
                Sides = Sides.OneSided,
                NumberUp = 1,
                OrientationRequested = Orientation.Portrait,
                Media = "iso_a4_210x297mm",
                PrinterResolution = new Resolution(600, 600, ResolutionUnit.DotsPerInch),
                PrintQuality = PrintQuality.Normal,
                PrintScaling = PrintScaling.Auto,
                PrintColorMode = PrintColorMode.Color,
                MediaCol = new MediaCol
                {
                    MediaBackCoating = MediaCoating.Glossy,
                    MediaBottomMargin = 10,
                    MediaColor = "white",
                    MediaFrontCoating = MediaCoating.Glossy,
                    MediaGrain = MediaGrain.XDirection,
                    MediaHoleCount = 0,
                    MediaInfo = "test",
                    MediaKey = "test",
                    MediaLeftMargin = 10,
                    MediaOrderCount = 1,
                    MediaPrePrinted = MediaPrePrinted.Blank,
                    MediaRecycled = MediaRecycled.None,
                    MediaRightMargin = 10,
                    MediaSize = new MediaSize { XDimension = 21000, YDimension = 29700 }, // 1/100 mm for A4
                    MediaSizeName = "iso_a4_210x297mm",
                    MediaSource = MediaSource.Main,
                    MediaSourceProperties = new MediaSourceProperties { MediaSourceFeedDirection = MediaSourceFeedDirection.LongEdgeFirst, MediaSourceFeedOrientation = Orientation.Portrait },
                    MediaThickness = 10,
                    MediaTooth = MediaTooth.Medium,
                    MediaTopMargin = 10,
                    MediaType = "stationery",
                    MediaWeightMetric = 80
                }
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
                    JobUri = "http://127.0.0.1:631/456"
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
                    JobUri = "http://127.0.0.1:631/456"
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
            },
            JobTemplateAttributes = new()
            {
                Copies = 1,
                JobPriority = 1,
                JobSheets = JobSheets.None,
                JobHoldUntil = JobHoldUntil.NoHold,
                MultipleDocumentHandling = MultipleDocumentHandling.SeparateDocumentsUncollatedCopies,
                Finishings = Finishings.None,
                PageRanges = [new SharpIpp.Protocol.Models.Range(1, 2)],
                Sides = Sides.OneSided,
                NumberUp = 1,
                OrientationRequested = Orientation.Portrait,
                Media = "iso_a4_210x297mm",
                PrinterResolution = new Resolution(600, 600, ResolutionUnit.DotsPerInch),
                PrintQuality = PrintQuality.Normal,
                PrintScaling = PrintScaling.Auto,
                PrintColorMode = PrintColorMode.Color,
                MediaCol = new MediaCol
                {
                    MediaBackCoating = MediaCoating.Glossy,
                    MediaBottomMargin = 10,
                    MediaColor = "white",
                    MediaFrontCoating = MediaCoating.Glossy,
                    MediaGrain = MediaGrain.XDirection,
                    MediaHoleCount = 0,
                    MediaInfo = "test",
                    MediaKey = "test",
                    MediaLeftMargin = 10,
                    MediaOrderCount = 1,
                    MediaPrePrinted = MediaPrePrinted.Blank,
                    MediaRecycled = MediaRecycled.None,
                    MediaRightMargin = 10,
                    MediaSize = new MediaSize { XDimension = 21000, YDimension = 29700 }, // 1/100 mm for A4
                    MediaSizeName = "iso_a4_210x297mm",
                    MediaSource = MediaSource.Main,
                    MediaSourceProperties = new MediaSourceProperties { MediaSourceFeedDirection = MediaSourceFeedDirection.LongEdgeFirst, MediaSourceFeedOrientation = Orientation.Portrait },
                    MediaThickness = 10,
                    MediaTooth = MediaTooth.Medium,
                    MediaTopMargin = 10,
                    MediaType = "stationery",
                    MediaWeightMetric = 80
                }
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
                    JobUri = "http://127.0.0.1:631/456"
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
                JobId = 1,
                JobUri = new Uri("http://127.0.0.1:631/jobs/1"),
                DocumentName = "test-document",
                Compression = Compression.None,
                DocumentFormat = "application/pdf",
                DocumentNaturalLanguage = "en",
                LastDocument = true,
            },
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
                    JobUri = "http://127.0.0.1:631/456"
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
                Finishings = Finishings.None,
                PageRanges = [new SharpIpp.Protocol.Models.Range(1, 2)],
                Sides = Sides.OneSided,
                NumberUp = 1,
                OrientationRequested = Orientation.Portrait,
                Media = "iso_a4_210x297mm",
                PrinterResolution = new Resolution(600, 600, ResolutionUnit.DotsPerInch),
                PrintQuality = PrintQuality.Normal,
                PrintScaling = PrintScaling.Auto,
                PrintColorMode = PrintColorMode.Color,
                MediaCol = new MediaCol
                {
                    MediaBackCoating = MediaCoating.Glossy,
                    MediaBottomMargin = 10,
                    MediaColor = "white",
                    MediaFrontCoating = MediaCoating.Glossy,
                    MediaGrain = MediaGrain.XDirection,
                    MediaHoleCount = 0,
                    MediaInfo = "test",
                    MediaKey = "test",
                    MediaLeftMargin = 10,
                    MediaOrderCount = 1,
                    MediaPrePrinted = MediaPrePrinted.Blank,
                    MediaRecycled = MediaRecycled.None,
                    MediaRightMargin = 10,
                    MediaSize = new MediaSize { XDimension = 21000, YDimension = 29700 }, // 1/100 mm for A4
                    MediaSizeName = "iso_a4_210x297mm",
                    MediaSource = MediaSource.Main,
                    MediaSourceProperties = new MediaSourceProperties { MediaSourceFeedDirection = MediaSourceFeedDirection.LongEdgeFirst, MediaSourceFeedOrientation = Orientation.Portrait },
                    MediaThickness = 10,
                    MediaTooth = MediaTooth.Medium,
                    MediaTopMargin = 10,
                    MediaType = "stationery",
                    MediaWeightMetric = 80
                },
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
                    JobUri = "http://127.0.0.1:631/456"
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
                    JobImpressionsCompleted = 0,
                    JobMediaSheets = 2,
                    JobMoreInfo = "more info",
                    NumberOfDocuments = 1,
                    NumberOfInterveningJobs = 0,
                    OutputDeviceAssigned = "printer",
                    JobMediaSheetsCompleted = 0,
                    JobStateMessage = "pending",
                    DateTimeAtCreation = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    DateTimeAtProcessing = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    DateTimeAtCompleted = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    TimeAtCreation = 100,
                    TimeAtProcessing = 110,
                    TimeAtCompleted = 120,
                    JobPrinterUpTime = 200,
                    JobKOctets = 20,
                    JobDetailedStatusMessages = ["message"],
                    JobDocumentAccessErrors = ["error"],
                    JobMessageFromOperator = "operator message"
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
                        TimeAtCreation = 100,
                        TimeAtProcessing = 110,
                        TimeAtCompleted = 120,
                        JobPrinterUpTime = 200,
                        JobKOctets = 20,
                        JobDetailedStatusMessages = ["message"],
                        JobDocumentAccessErrors = ["error"],
                        JobMessageFromOperator = "operator message"
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
                    PrinterStateReasons = ["none"],
                    PrinterStateMessage = "Idle",
                    IppVersionsSupported = [default],
                    OperationsSupported = [IppOperation.PrintJob],
                    MultipleDocumentJobsSupported = true,
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
                    PdlOverrideSupported = "attempted",
                    PrinterUpTime = 100,
                    PrinterCurrentTime = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    MultipleOperationTimeOut = 10,
                    CompressionSupported = [Compression.None],
                    JobKOctetsSupported = new SharpIpp.Protocol.Models.Range(1, 100),
                    JpegKOctetsSupported = new SharpIpp.Protocol.Models.Range(1, 100),
                    PdfKOctetsSupported = new SharpIpp.Protocol.Models.Range(1, 100),
                    JobImpressionsSupported = new SharpIpp.Protocol.Models.Range(1, 100),
                    JobMediaSheetsSupported = new SharpIpp.Protocol.Models.Range(1, 100),
                    PagesPerMinute = 10,
                    PagesPerMinuteColor = 10,
                    PrintScalingDefault = PrintScaling.Auto,
                    PrintScalingSupported = [PrintScaling.Auto],
                    MediaDefault = "iso_a4_210x297mm",
                    MediaSupported = ["iso_a4_210x297mm"],
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
                    OutputBinDefault = "face-down",
                    OutputBinSupported = ["face-down"],
                    MediaColDefault =  new MediaCol
                    {
                        MediaBackCoating = MediaCoating.Glossy,
                        MediaBottomMargin = 10,
                        MediaColor = "white",
                        MediaFrontCoating = MediaCoating.Glossy,
                        MediaGrain = MediaGrain.XDirection,
                        MediaHoleCount = 0,
                        MediaInfo = "test",
                        MediaKey = "test",
                        MediaLeftMargin = 10,
                        MediaOrderCount = 1,
                        MediaPrePrinted = MediaPrePrinted.Blank,
                        MediaRecycled = MediaRecycled.None,
                        MediaRightMargin = 10,
                        MediaSize = new MediaSize { XDimension = 21000, YDimension = 29700 }, // 1/100 mm for A4
                        MediaSizeName = "iso_a4_210x297mm",
                        MediaSource = MediaSource.Main,
                        MediaSourceProperties = new MediaSourceProperties { MediaSourceFeedDirection = MediaSourceFeedDirection.LongEdgeFirst, MediaSourceFeedOrientation = Orientation.Portrait },
                        MediaThickness = 10,
                        MediaTooth = MediaTooth.Medium,
                        MediaTopMargin = 10,
                        MediaType = "stationery",
                        MediaWeightMetric = 80
                    },
                    PrintColorModeDefault = PrintColorMode.Color,
                    PrintColorModeSupported = [PrintColorMode.Color],
                    WhichJobsSupported = [WhichJobs.Completed],
                    PrinterUUID = "{6541A875-C511-4273-909F-18CFBB38D9D0}"
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
                LastDocument = true,
                DocumentUri = new Uri("ftp://document.pdf"),
            },
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
                    JobUri = "http://127.0.0.1:631/456"
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
            },
            JobTemplateAttributes = new()
            {
                Copies = 1,
                JobPriority = 1,
                JobHoldUntil = JobHoldUntil.NoHold,
                MultipleDocumentHandling = MultipleDocumentHandling.SeparateDocumentsUncollatedCopies,
                Finishings = Finishings.None,
                PageRanges = [new SharpIpp.Protocol.Models.Range(1, 2)],
                Sides = Sides.OneSided,
                NumberUp = 1,
                OrientationRequested = Orientation.Portrait,
                Media = "iso_a4_210x297mm",
                PrinterResolution = new Resolution(600, 600, ResolutionUnit.DotsPerInch),
                PrintQuality = PrintQuality.Normal,
                PrintScaling = PrintScaling.Auto,
                PrintColorMode = PrintColorMode.Color,
                MediaCol = new MediaCol
                {
                    MediaBackCoating = MediaCoating.Glossy,
                    MediaBottomMargin = 10,
                    MediaColor = "white",
                    MediaFrontCoating = MediaCoating.Glossy,
                    MediaGrain = MediaGrain.XDirection,
                    MediaHoleCount = 0,
                    MediaInfo = "test",
                    MediaKey = "test",
                    MediaLeftMargin = 10,
                    MediaOrderCount = 1,
                    MediaPrePrinted = MediaPrePrinted.Blank,
                    MediaRecycled = MediaRecycled.None,
                    MediaRightMargin = 10,
                    MediaSize = new MediaSize { XDimension = 21000, YDimension = 29700 }, // 1/100 mm for A4
                    MediaSizeName = "iso_a4_210x297mm",
                    MediaSource = MediaSource.Main,
                    MediaSourceProperties = new MediaSourceProperties { MediaSourceFeedDirection = MediaSourceFeedDirection.LongEdgeFirst, MediaSourceFeedOrientation = Orientation.Portrait },
                    MediaThickness = 10,
                    MediaTooth = MediaTooth.Medium,
                    MediaTopMargin = 10,
                    MediaType = "stationery",
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
                        PrinterStateReasons = ["none"],
                        PrinterStateMessage = "Idle",
                        IppVersionsSupported = [default],
                        OperationsSupported = [IppOperation.PrintJob],
                        MultipleDocumentJobsSupported = true,
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
                        PdlOverrideSupported = "attempted",
                        PrinterUpTime = 100,
                        PrinterCurrentTime = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
                        MultipleOperationTimeOut = 10,
                        CompressionSupported = [Compression.None],
                        JobKOctetsSupported = new SharpIpp.Protocol.Models.Range(1, 100),
                        JpegKOctetsSupported = new SharpIpp.Protocol.Models.Range(1, 100),
                        PdfKOctetsSupported = new SharpIpp.Protocol.Models.Range(1, 100),
                        JobImpressionsSupported = new SharpIpp.Protocol.Models.Range(1, 100),
                        JobMediaSheetsSupported = new SharpIpp.Protocol.Models.Range(1, 100),
                        PagesPerMinute = 10,
                        PagesPerMinuteColor = 10,
                        PrintScalingDefault = PrintScaling.Auto,
                        PrintScalingSupported = [PrintScaling.Auto],
                        MediaDefault = "iso_a4_210x297mm",
                        MediaSupported = ["iso_a4_210x297mm"],
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
                        OutputBinDefault = "face-down",
                        OutputBinSupported = ["face-down"],
                        MediaColDefault =  new MediaCol
                        {
                            MediaBackCoating = MediaCoating.Glossy,
                            MediaBottomMargin = 10,
                            MediaColor = "white",
                            MediaFrontCoating = MediaCoating.Glossy,
                            MediaGrain = MediaGrain.XDirection,
                            MediaHoleCount = 0,
                            MediaInfo = "test",
                            MediaKey = "test",
                            MediaLeftMargin = 10,
                            MediaOrderCount = 1,
                            MediaPrePrinted = MediaPrePrinted.Blank,
                            MediaRecycled = MediaRecycled.None,
                            MediaRightMargin = 10,
                            MediaSize = new MediaSize { XDimension = 21000, YDimension = 29700 }, // 1/100 mm for A4
                            MediaSizeName = "iso_a4_210x297mm",
                            MediaSource = MediaSource.Main,
                            MediaSourceProperties = new MediaSourceProperties { MediaSourceFeedDirection = MediaSourceFeedDirection.LongEdgeFirst, MediaSourceFeedOrientation = Orientation.Portrait },
                            MediaThickness = 10,
                            MediaTooth = MediaTooth.Medium,
                            MediaTopMargin = 10,
                            MediaType = "stationery",
                            MediaWeightMetric = 80
                        },
                        PrintColorModeDefault = PrintColorMode.Color,
                        PrintColorModeSupported = [PrintColorMode.Color],
                        WhichJobsSupported = [WhichJobs.Completed],
                        PrinterUUID = "{6541A875-C511-4273-909F-18CFBB38D9D0}"
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
                new IppAttribute(Tag.NoValue, PrinterAttribute.WhichJobsSupported, NoValue.Instance)
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
                MediaColDefault = new MediaCol()
                {
                    MediaBottomMargin = NoValue.GetNoValue<int>(),
                    MediaHoleCount = NoValue.GetNoValue<int>(),
                    MediaLeftMargin = NoValue.GetNoValue<int>(),
                    MediaOrderCount = NoValue.GetNoValue<int>(),
                    MediaRightMargin = NoValue.GetNoValue<int>(),
                    MediaThickness = NoValue.GetNoValue<int>(),
                    MediaTopMargin = NoValue.GetNoValue<int>(),
                    MediaWeightMetric = NoValue.GetNoValue<int>(),
                    MediaBackCoating = NoValue.GetNoValue<MediaCoating>(),
                    MediaFrontCoating = NoValue.GetNoValue<MediaCoating>(),
                    MediaGrain = NoValue.GetNoValue<MediaGrain>(),
                    MediaPrePrinted = NoValue.GetNoValue<MediaPrePrinted>(),
                    MediaRecycled = NoValue.GetNoValue<MediaRecycled>(),
                    MediaSource = NoValue.GetNoValue<MediaSource>(),
                    MediaTooth = NoValue.GetNoValue<MediaTooth>()
                }
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
}
