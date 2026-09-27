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
public class SendDocumentTests : SharpIppIntegrationTestBase
{
    [TestMethod()]
    public async Task SendDocumentAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
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
                AttributesCharset = (SharpIpp.Protocol.Models.Charset)"utf-8",
                AttributesNaturalLanguage = (NaturalLanguage)"en-us",
                RequestingUserName = "test-user",
                RequestingUserUri = new Uri("mailto:test-user@example.com"),
                JobId = 1,
                JobUri = new Uri("http://127.0.0.1:631/jobs/1"),
                ResourceIds = new[] { 21, 22 },
                ClientInfo = new[]
                {
                    new ClientInfo
                    {
                        ClientName = "SharpIppTests",
                        ClientType = ClientType.Application
                    }
                },
                DocumentFormatDetails = new DocumentFormatDetails
                {
                    DocumentSourceApplicationName = "SharpIppTests",
                    DocumentSourceApplicationVersion = "1.0.0",
                    DocumentSourceOsName = "Windows",
                    DocumentSourceOsVersion = "11"
                },
                DocumentName = "test-document",
                Compression = Compression.None,
                DocumentFormat = (SharpIpp.Protocol.Models.DocumentFormat)"application/pdf",
                DocumentNaturalLanguage = (NaturalLanguage)"en",
                DocumentCharset = (Charset)"utf-8",
                DocumentMessage = "document-message",
                LastDocument = true,
                DocumentMetadata = GetTestDocumentMetadata(),
                DocumentPassword = (OctetString)"test-password",
            },
            DocumentTemplateAttributes = new DocumentTemplateAttributes
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
                    MediaCol = new MediaCol { MediaColor = (MediaColor)"blue" }
                },
                FinishingsCol = new[]
                {
                    new FinishingsCol
                    {
                        FinishingTemplate = (FinishingTemplate)"staple",
                        ImpositionTemplate = (ImpositionTemplate)"signature",
                        MediaSheetsSupported = new SharpIpp.Protocol.Models.Range(1, 10),
                        MediaSizeName = (Media)"iso_a4_210x297mm",
                        MediaSize = new MediaSize { XDimension = new SharpIpp.Protocol.Models.Range(21000, 21000), YDimension = new SharpIpp.Protocol.Models.Range(29700, 29700) },
                        Stitching = new Stitching
                        {
                            StitchingAngle = 90,
                            StitchingMethod = StitchingMethod.Wire,
                            StitchingReferenceEdge = FinishingReferenceEdge.Left,
                            StitchingLocations = new[] { 10, 20 },
                            StitchingOffset = 5
                        },
                        Binding = new Binding
                        {
                            BindingReferenceEdge = FinishingReferenceEdge.Left,
                            BindingType = BindingType.Perfect
                        }
                    }
                },
                ForceFrontSide = new[] { 1, 2 },
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
                    MediaSize = new MediaSize { XDimension = new SharpIpp.Protocol.Models.Range(21000, 21000), YDimension = new SharpIpp.Protocol.Models.Range(29700, 29700) },
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
                PageOrderReceived = PageOrderReceived.OneToNOrder,
                PageRanges = new[] { new SharpIpp.Protocol.Models.Range(1, 2) },
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
                YSide2ImageShift = 10,
                InputAutoExposure = true,
                InputAutoScaling = false,
                InputAutoSkewCorrection = true,
                InputBrightness = 50,
                InputColorMode = (InputColorMode?)"auto",
                InputContentType = (InputContentType?)"image/jpeg",
                InputContrast = 10,
                InputFilmScanMode = (InputFilmScanMode?)"negative",
                InputImagesToTransfer = 5,
                InputMedia = (Media)"iso_a4_210x297mm",
                InputOrientationRequested = Orientation.Landscape,
                InputQuality = PrintQuality.High,
                InputResolution = new Resolution(300, 300, ResolutionUnit.DotsPerInch),
                InputScalingHeight = 200,
                InputScalingWidth = 300,
                InputSharpness = 25,
                InputSides = Sides.TwoSidedLongEdge,
                InputSource = (InputSource?)"document-feeder",
                DocumentCharset = (Charset)"utf-8",
                DocumentFormat = (SharpIpp.Protocol.Models.DocumentFormat)"application/pdf",
                DocumentFormatDetails = new DocumentFormatDetails
                {
                    DocumentSourceApplicationName = "ScanApp",
                    DocumentSourceOsName = "Windows"
                },
                DocumentMessage = "scan message",
                DocumentMetadata = GetTestDocumentMetadata(),
                DocumentName = "scanned-document",
                DocumentNaturalLanguage = (NaturalLanguage)"en-us",
                DocumentPassword = new OctetString(new byte[] {  0x04, 0x05  }),
                DocumentUri = new Uri("http://example.com/doc.pdf"),
                LastDocument = false,
                JobPassword = new OctetString(new byte[] {  0x01, 0x02  }),
                JobPasswordEncryption = JobPasswordEncryption.None,
                MaterialsCol = new[]
                {
                    new Material
                    {
                        MaterialAmount = 1,
                        MaterialColor = (MaterialColor?)"blue",
                        MaterialDiameter = 2,
                        MaterialFillDensity = 3,
                        MaterialKey = (MaterialKey?)"pla-blue",
                        MaterialName = "PLA Blue",
                        MaterialPurpose = new[] { (MaterialPurpose)"model" },
                        MaterialRate = 4,
                        MaterialRateUnits = (MaterialRateUnits?)"mm-per-second",
                        MaterialShellThickness = 5,
                        MaterialTemperature = 200,
                        MaterialType = (MaterialType?)"pla"
                    }
                },
                MultipleObjectHandling = (MultipleObjectHandling?)"abort-job",
                PlatformTemperature = 75,
                PrintAccuracy = new PrintAccuracy
                {
                    AccuracyUnits = (AccuracyUnits?)"mm",
                    XAccuracy = 100,
                    YAccuracy = 100,
                    ZAccuracy = 50
                },
                PrintBase = (PrintBase?)"raft",
                PrintObjects = new[]
                {
                    new PrintObject
                    {
                        DocumentNumber = 1,
                        PrintObjectsSource = new Uri("https://example.local/objects/1"),
                        TransformationMatrix = new[] { 1, 0, 0, 0, 1, 0 }
                    }
                },
                PrintSupports = (PrintSupports?)"generated-supports",
                ChamberHumidity = 45,
                ChamberTemperature = 60
            }
        };
        IIppRequest? serverRequest = null;
        SendDocumentResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new SendDocumentResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new()
                {
                    StatusMessage = "successful-ok",
                    DetailedStatusMessage = "detail1",
                    DocumentAccessError = "none"
                },
                JobAttributes = new()
                {
                    JobState = JobState.Pending,
                    JobStateReasons = new[] { JobStateReason.None },
                    JobStateMessage = "pending",
                    NumberOfInterveningJobs = 0,
                    JobId = 456,
                    JobUri = new Uri("http://127.0.0.1:631/456"),
                    ClientInfo = new[] { new ClientInfo { ClientName = "SharpIppTests", ClientType = ClientType.Application } },
                    JobImpressionsCompletedCol = new JobCounter { Monochrome = 5 },
                    JobMediaSheetsCompletedCol = new JobCounter { Monochrome = 4 },
                    JobPagesCompleted = 3,
                    JobPagesCompletedCol = new JobCounter { Monochrome = 3 },
                    JobProcessingTime = 120
                },
                DocumentAttributes = new DocumentAttributes
                {
                    DocumentNumber = 1,
                    DocumentState = DocumentState.Pending,
                    DocumentStateReasons = new[] { DocumentStateReason.None },
                    DocumentStateMessage = "pending",
                    Pages = 3,
                    PagesCompleted = 2
                }
            };
            var responseStream = new MemoryStream();
            await server.SendResponseAsync(serverResponse, responseStream, c);
            responseStream.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StreamContent(responseStream)
            };
        }
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));

        SendDocumentResponse? clientResponse = await client.SendDocumentAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
        clientResponse.Should().NotBeNull();
        clientResponse!.JobAttributes.Should().NotBeNull();
        clientResponse.JobAttributes!.JobId.Should().Be(456);
        clientResponse.DocumentAttributes.Should().NotBeNull();
        clientResponse.DocumentAttributes!.Value.Value.DocumentNumber!.Value.Value.Should().Be(1);
    }
}
