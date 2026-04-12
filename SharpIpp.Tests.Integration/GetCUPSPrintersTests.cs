using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace SharpIpp.Tests.Integration;

[TestClass]
[ExcludeFromCodeCoverage]
public class GetCUPSPrintersTests : SharpIppIntegrationTestBase
{
    [TestMethod()]
    public async Task GetCUPSPrintersAsync_WhenSendingStream_ServerReceivesSameRequestAndReturnsExpectedResponse()
    {
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
                RequestingUserUri = new Uri("http://127.0.0.1/users/test-user"),
                ClientInfo =
                [
                    new ClientInfo
                    {
                        ClientName = "SharpIpp.Tests",
                        ClientPatches = "patch-1",
                        ClientStringVersion = "1.0.0",
                        ClientType = ClientType.OperatingSystem,
                        ClientVersion = "010000",
                    }
                ],
                JobHoldUntilTime = new DateTimeOffset(2024, 1, 2, 3, 4, 5, TimeSpan.Zero),
            },
        };
        IIppRequest? serverRequest = null;
        CUPSGetPrintersResponse? serverResponse = null;
        HttpStatusCode statusCode = HttpStatusCode.OK;
        async Task<HttpResponseMessage> func(Stream s, CancellationToken c)
        {
            serverRequest = await server.ReceiveRequestAsync(s, c);
            serverResponse = new CUPSGetPrintersResponse
            {
                RequestId = serverRequest.RequestId,
                Version = serverRequest.Version,
                StatusCode = IppStatusCode.SuccessfulOk,
                OperationAttributes = new() { StatusMessage = "successful-ok", DetailedStatusMessage = ["detail1"], DocumentAccessError = "none" },
                PrintersAttributes = [new PrinterDescriptionAttributes {
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
                    OverridesSupported = [OverrideSupported.Pages, OverrideSupported.DocumentNumbers, OverrideSupported.Sides],
                    PrinterUpTime = 100,
                    PrinterCurrentTime = new DateTimeOffset(2024, 1, 1, 1, 1, 1, TimeSpan.Zero),
                    PagesPerMinute = 10,
                    PagesPerMinuteColor = 10,
                    PrinterResolutionDefault = new Resolution(600, 600, ResolutionUnit.DotsPerInch),
                    PrintQualityDefault = PrintQuality.Normal,
                    PrintColorModeDefault = PrintColorMode.Color,
                    WhichJobsSupported = [WhichJobs.Completed],
                    JobCreationAttributesSupported = [JobCreationAttribute.Copies, JobCreationAttribute.Finishings, JobCreationAttribute.Media],
                }]
            };
            var responseStream = new MemoryStream();
            await server.SendResponseAsync(serverResponse, responseStream, c);
            responseStream.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = statusCode, Content = new StreamContent(responseStream) };
        }
        SharpIppClient client = new(new(GetMockOfHttpMessageHandler(func).Object));

        CUPSGetPrintersResponse? clientResponse = await client.GetCUPSPrintersAsync(clientRequest);

        clientRequest.Should().BeEquivalentTo(serverRequest);
        clientResponse.Should().BeEquivalentTo(serverResponse);
    }
}