using Moq;
using Moq.Protected;
using SharpIpp;
using SharpIpp.Models;
using SharpIpp.Protocol.Models;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Threading;

namespace SharpIpp.Tests;

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
                JobKOctetsProcessed = 10,
                JobImpressions = 5,
                JobMediaSheets = 2,
                Compression = Compression.None,
                DocumentNaturalLanguage = "en",
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
                JobState = JobState.Pending,
                StatusCode = IppStatusCode.SuccessfulOk,
                JobStateReasons = [JobStateReason.None],
                JobId = 456,
                JobUri = "http://127.0.0.1:631/456"
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
        clientResponse.Should().BeEquivalentTo(serverResponse, options => options.Excluding(ctx => ctx!.Sections));
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
                JobKOctetsProcessed = 10,
                JobImpressions = 5,
                JobMediaSheets = 2,
                Compression = Compression.None,
                DocumentNaturalLanguage = "en",
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
                JobState = JobState.Pending,
                StatusCode = IppStatusCode.SuccessfulOk,
                JobStateReasons = [JobStateReason.None],
                JobId = 456,
                JobUri = "http://127.0.0.1:631/456"
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
        clientResponse.Should().BeEquivalentTo(serverResponse, options => options.Excluding(ctx => ctx!.Sections));
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
                AdditionalAttributes = [new IppAttribute(Tag.Keyword, "extra-op-att", "test")],
            },
            AdditionalJobAttributes = [new IppAttribute(Tag.Keyword, "extra-job-att", "test")],
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
                JobState = JobState.Pending,
                StatusCode = IppStatusCode.SuccessfulOk,
                JobStateReasons = [JobStateReason.None],
                JobId = 456,
                JobUri = "http://127.0.0.1:631/456"
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
        clientRequest.Should().BeEquivalentTo(serverRequest, options => options
            .Excluding(req => req.OperationAttributes.AdditionalAttributes)
            .Excluding(req => req.AdditionalJobAttributes));
        clientResponse.Should().BeEquivalentTo(serverResponse, options => options.Excluding(ctx => ctx!.Sections));
    
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
                AdditionalAttributes = [new IppAttribute(Tag.Keyword, "extra-op-att", "test")],
            },
            AdditionalJobAttributes = [new IppAttribute(Tag.Keyword, "extra-job-att", "test")],
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
        clientRequest.Should().BeEquivalentTo(serverRequest, options => options
            .Excluding(req => req.OperationAttributes.AdditionalAttributes)
            .Excluding(req => req.AdditionalJobAttributes));
        clientResponse.Should().BeEquivalentTo(serverResponse, options => options.Excluding(ctx => ctx!.Sections));
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
                JobKOctetsProcessed = 10,
                JobImpressions = 5,
                JobMediaSheets = 2,
                AdditionalAttributes = [new IppAttribute(Tag.Keyword, "extra-op-att", "test")],
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
                    MediaWeightMetric = 80
                },
                AdditionalJobAttributes = [new IppAttribute(Tag.Keyword, "extra-job-att", "test")],
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
                JobState = JobState.Pending,
                StatusCode = IppStatusCode.SuccessfulOk,
                JobStateReasons = [JobStateReason.None],
                JobId = 456,
                JobUri = "http://127.0.0.1:631/456"
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
        clientRequest.Should().BeEquivalentTo(serverRequest, options => options
            .Excluding(req => req.OperationAttributes.AdditionalAttributes)
            .Excluding(req => ((CreateJobRequest)req).JobTemplateAttributes!.AdditionalJobAttributes));
        clientResponse.Should().BeEquivalentTo(serverResponse, options => options.Excluding(ctx => ctx!.Sections));
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
                AdditionalAttributes = [new IppAttribute(Tag.Keyword, "extra-op-att", "test")],
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
                    JobState = JobState.Pending,
                    JobStateReasons = [JobStateReason.None],
                    JobName = "Test Job",
                    JobOriginatingUserName = "test-user"
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
        clientRequest.Should().BeEquivalentTo(serverRequest, options => options
            .Excluding(req => req.OperationAttributes.AdditionalAttributes));
        clientResponse.Should().BeEquivalentTo(serverResponse, options => options.Excluding(ctx => ctx!.Sections));
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
                JobId = 1,
                JobUri = new Uri("http://127.0.0.1:631/jobs/1"),
                RequestedAttributes = ["job-id", "job-uri", "job-state"],
                WhichJobs = WhichJobs.Completed,
                Limit = 10,
                MyJobs = true,
                AdditionalAttributes = [new IppAttribute(Tag.Keyword, "extra-op-att", "test")],
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
                Jobs = 
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
        clientRequest.Should().BeEquivalentTo(serverRequest, options => options
            .Excluding(req => req.OperationAttributes.AdditionalAttributes));
        clientResponse.Should().BeEquivalentTo(serverResponse, options => options.Excluding(ctx => ctx!.Sections));
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
                DocumentFormat = "application/octet-stream",
                AdditionalAttributes = [new IppAttribute(Tag.Keyword, "extra-op-att", "test")],
            },
            AdditionalJobAttributes = [new IppAttribute(Tag.Keyword, "extra-job-att", "test")],
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
                PrinterState = PrinterState.Idle,
                PrinterStateReasons = ["none"],
                PrinterName = "Test Printer",
                PrinterLocation = "Office",
                PrinterInfo = "Test Printer Info",
                PrinterMoreInfo = "http://127.0.0.1:631",
                PrinterDriverInstaller = "installer",
                PrinterMakeAndModel = "SharpIpp Virtual Printer",
                PrinterMoreInfoManufacturer = "http://manufacturer.com",
                PrinterStateMessage = "Idle",
                QueuedJobCount = 0
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
        clientRequest.Should().BeEquivalentTo(serverRequest, options => options
            .Excluding(req => req.OperationAttributes.AdditionalAttributes)
            .Excluding(req => req.AdditionalJobAttributes));
        clientResponse.Should().BeEquivalentTo(serverResponse, options => options.Excluding(ctx => ctx!.Sections));
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
                AdditionalAttributes = [new IppAttribute(Tag.Keyword, "extra-op-att", "test")],
            },
            AdditionalJobAttributes = [new IppAttribute(Tag.Keyword, "extra-job-att", "test")],
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
        clientRequest.Should().BeEquivalentTo(serverRequest, options => options
            .Excluding(req => req.OperationAttributes.AdditionalAttributes)
            .Excluding(req => req.AdditionalJobAttributes));
        clientResponse.Should().BeEquivalentTo(serverResponse, options => options.Excluding(ctx => ctx!.Sections));
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
                AdditionalAttributes = [new IppAttribute(Tag.Keyword, "extra-op-att", "test")],
            },
            AdditionalJobAttributes = [new IppAttribute(Tag.Keyword, "extra-job-att", "test")],
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
        clientRequest.Should().BeEquivalentTo(serverRequest, options => options
            .Excluding(req => req.OperationAttributes.AdditionalAttributes)
            .Excluding(req => req.AdditionalJobAttributes));
        clientResponse.Should().BeEquivalentTo(serverResponse, options => options.Excluding(ctx => ctx!.Sections));
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
                AdditionalAttributes = [new IppAttribute(Tag.Keyword, "extra-op-att", "test")],
            },
            AdditionalJobAttributes = [new IppAttribute(Tag.Keyword, "extra-job-att", "test")],
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
        clientRequest.Should().BeEquivalentTo(serverRequest, options => options
            .Excluding(req => req.OperationAttributes.AdditionalAttributes)
            .Excluding(req => req.AdditionalJobAttributes));
        clientResponse.Should().BeEquivalentTo(serverResponse, options => options.Excluding(ctx => ctx!.Sections));
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
                Message = "message",
                AdditionalAttributes = [new IppAttribute(Tag.Keyword, "extra-op-att", "test")],
            },
            AdditionalJobAttributes = [new IppAttribute(Tag.Keyword, "extra-job-att", "test")],
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
        clientRequest.Should().BeEquivalentTo(serverRequest, options => options
            .Excluding(req => req.OperationAttributes.AdditionalAttributes)
            .Excluding(req => req.AdditionalJobAttributes));
        clientResponse.Should().BeEquivalentTo(serverResponse, options => options.Excluding(ctx => ctx!.Sections));
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
                AdditionalAttributes = [new IppAttribute(Tag.Keyword, "extra-op-att", "test")],
            },
            AdditionalJobAttributes = [new IppAttribute(Tag.Keyword, "extra-job-att", "test")],
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
        clientRequest.Should().BeEquivalentTo(serverRequest, options => options
            .Excluding(req => req.OperationAttributes.AdditionalAttributes)
            .Excluding(req => req.AdditionalJobAttributes));
        clientResponse.Should().BeEquivalentTo(serverResponse, options => options.Excluding(ctx => ctx!.Sections));
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
                AdditionalAttributes = [new IppAttribute(Tag.Keyword, "extra-op-att", "test")],
            },
            AdditionalJobAttributes = [new IppAttribute(Tag.Keyword, "extra-job-att", "test")],
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
        clientRequest.Should().BeEquivalentTo(serverRequest, options => options
            .Excluding(req => req.OperationAttributes.AdditionalAttributes)
            .Excluding(req => req.AdditionalJobAttributes));
        clientResponse.Should().BeEquivalentTo(serverResponse, options => options.Excluding(ctx => ctx!.Sections));
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
                AdditionalAttributes = [new IppAttribute(Tag.Keyword, "extra-op-att", "test")],
            },
            AdditionalJobAttributes = [new IppAttribute(Tag.Keyword, "extra-job-att", "test")],
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
                JobState = JobState.Pending,
                StatusCode = IppStatusCode.SuccessfulOk,
                JobStateReasons = [JobStateReason.None],
                JobId = 456,
                JobUri = "http://127.0.0.1:631/456"
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
        clientRequest.Should().BeEquivalentTo(serverRequest, options => options
            .Excluding(req => req.OperationAttributes.AdditionalAttributes)
            .Excluding(req => req.AdditionalJobAttributes));
        clientResponse.Should().BeEquivalentTo(serverResponse, options => options.Excluding(ctx => ctx!.Sections));
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
            Document = memoryStream,
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                DocumentName = "test-document.pdf",
                DocumentFormat = "application/pdf",
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en-us",
                RequestingUserName = "test-user",
                JobName = "Test Job",
                IppAttributeFidelity = true,
                JobKOctetsProcessed = 10,
                JobImpressions = 5,
                JobMediaSheets = 2,
                Compression = Compression.None,
                DocumentNaturalLanguage = "en",
                AdditionalAttributes = [new IppAttribute(Tag.Keyword, "extra-op-att", "test")],
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
                    MediaWeightMetric = 80
                },
                AdditionalJobAttributes = [new IppAttribute(Tag.Keyword, "extra-job-att", "test")],
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
        clientRequest.Should().BeEquivalentTo(serverRequest, options => options
             .Excluding(req => req.OperationAttributes.AdditionalAttributes)
             .Excluding(req => ((ValidateJobRequest)req).JobTemplateAttributes!.AdditionalJobAttributes));
        clientResponse.Should().BeEquivalentTo(serverResponse, options => options.Excluding(ctx => ctx!.Sections));
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
                AdditionalAttributes = [new IppAttribute(Tag.Keyword, "extra-op-att", "test")],
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
                Jobs = 
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
        CUPSGetPrintersResponse? clientResponse = await client.GetCUPSPrintersAsync(clientRequest);
        // Assert
        clientRequest.Should().BeEquivalentTo(serverRequest, options => options
            .Excluding(req => req.OperationAttributes.AdditionalAttributes));
        clientResponse.Should().BeEquivalentTo(serverResponse, options => options.Excluding(ctx => ctx!.Sections));
    }
}
