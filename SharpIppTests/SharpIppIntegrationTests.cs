using Moq;
using Moq.Protected;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
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
                JobKOctetsProcessed = 10,
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
                JobId = 1,
                JobUri = new Uri("http://127.0.0.1:631/jobs/1"),
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
                    IppVersionsSupported = [IppVersion.V1_1],
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
                        IppVersionsSupported = [IppVersion.V1_1],
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
}
