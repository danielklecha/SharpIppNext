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
public class SetDocumentAttributesTests : SharpIppIntegrationTestBase
{
    [TestMethod()]
    public async Task SetDocumentAttributesAsync_WhenSendingRequest_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
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
                CoverBack = new Cover { CoverType = CoverType.PrintNone, Media = (Media)"iso_a4_210x297mm" },
                CoverFront = new Cover { CoverType = CoverType.PrintBoth, Media = (Media)"iso_a4_210x297mm", MediaCol = new MediaCol { MediaColor = (MediaColor)"blue" } },
                // Either Finishings OR FinishingsCol is allowed (they are mutually exclusive in mapping), so use FinishingsCol here to keep a rich case.
                FinishingsCol = [new FinishingsCol
                {
                    FinishingTemplate = (FinishingTemplate)"staple",
                    Baling = new Baling { BalingType = BalingType.Band, BalingWhen = BalingWhen.AfterJob },
                    Binding = new Binding { BindingReferenceEdge = FinishingReferenceEdge.Left, BindingType = BindingType.Perfect },
                    Coating = new Coating { CoatingSides = CoatingSides.Front, CoatingType = CoatingType.Glossy },
                    Covering = new Covering { CoveringName = CoveringName.Plain },
                    Folding = [new Folding { FoldingDirection = FoldingDirection.Inward, FoldingOffset = 500, FoldingReferenceEdge = FinishingReferenceEdge.Bottom }],
                    Laminating = new Laminating { LaminatingSides = CoatingSides.Both, LaminatingType = LaminatingType.Matte },
                    Punching = new Punching { PunchingLocations = [15, 30], PunchingOffset = 20, PunchingReferenceEdge = FinishingReferenceEdge.Right },
                    Stitching = new Stitching { StitchingAngle = 90, StitchingMethod = StitchingMethod.Wire, StitchingReferenceEdge = FinishingReferenceEdge.Left, StitchingLocations = [10, 20], StitchingOffset = 5 },
                    Trimming = [new Trimming { TrimmingOffset = [100, 200], TrimmingReferenceEdge = FinishingReferenceEdge.Top, TrimmingType = TrimmingType.Full, TrimmingWhen = TrimmingWhen.AfterJob }],
                    ImpositionTemplate = (ImpositionTemplate)"signature",
                    MediaSheetsSupported = new SharpIpp.Protocol.Models.Range(1, 10),
                    MediaSizeName = (Media)"iso_a4_210x297mm",
                    MediaSize = new MediaSize { XDimension = 21000, YDimension = 29700 }
                }],
                ForceFrontSide = [1, 2],
                ImpositionTemplate = (ImpositionTemplate)"none",
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
                    MediaSize = new MediaSize { XDimension = 21000, YDimension = 29700 },
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
                OrientationRequested = Orientation.Landscape,
                OutputBin = (OutputBin)"top",
                PageDelivery = PageDelivery.SameOrderFaceUp,
                PageRanges = [new SharpIpp.Protocol.Models.Range(1, 1)],
                PresentationDirectionNumberUp = PresentationDirectionNumberUp.TobottomToleft,
                PrintQuality = PrintQuality.High,
                PrinterResolution = new Resolution(600, 600, ResolutionUnit.DotsPerInch),
                Sides = Sides.TwoSidedLongEdge,
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
            serverRequest = await server.ReceiveRequestAsync(s, c);
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
            var responseStream = new MemoryStream();
            await server.SendResponseAsync(serverResponse, responseStream, c);
            responseStream.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = statusCode, Content = new StreamContent(responseStream) };
        }
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));

        SetDocumentAttributesResponse? clientResponse = await client.SetDocumentAttributesAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}