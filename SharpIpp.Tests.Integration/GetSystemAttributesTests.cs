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
public class GetSystemAttributesTests : SharpIppIntegrationTestBase
{
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
                    SystemState = PrinterState.Processing,
                    SystemUuid = new Uri("urn:uuid:00000000-0000-0000-0000-000000000000"),
                    XriAuthenticationSupported = [UriAuthentication.None],
                    XriSecuritySupported = [UriSecurity.Tls],
                    XriUriSchemeSupported = [UriScheme.Https]
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
        clientResponse.Should().NotBeNull();
        clientResponse.RequestId.Should().Be(serverResponse.RequestId);
        clientResponse.Version.Should().Be(serverResponse.Version);
        clientResponse.StatusCode.Should().Be(serverResponse.StatusCode);
        clientResponse.SystemAttributes.Should().NotBeNull();
        clientResponse.SystemAttributes.SystemUuid.Should().Be(new Uri("urn:uuid:00000000-0000-0000-0000-000000000000"));
        clientResponse.SystemAttributes.XriAuthenticationSupported.Should().Contain(UriAuthentication.None);
        clientResponse.SystemAttributes.XriSecuritySupported.Should().Contain(UriSecurity.Tls);
        clientResponse.SystemAttributes.XriUriSchemeSupported.Should().Contain(UriScheme.Https);
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
                SystemAttributes = new() { SystemState = PrinterState.Stopped }
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
    public async Task CreateResponse_WhenMappingRawGetSystemAttributesResponse_ToSystemDescriptionAttributes_ReturnsExpectedProperties()
    {
        var clientRequest = new GetSystemAttributesRequest
        {
            RequestId = 810,
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
                SystemDescriptionAttributes = new()
                {
                    SystemConfigChanges = 15,
                    SystemStringsLanguagesSupported = ["en-us"],
                    IppVersionsSupported = [new IppVersion(2, 0)],
                    SystemInfo = "test-info",
                    SystemXriSupported = new[]
                    {
                        new SystemXri
                        {
                            XriUri = new Uri("ipp://127.0.0.1:631"),
                            XriAuthentication = "none",
                            XriSecurity = "tls"
                        }
                    }
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
        var mapped = client.CreateResponse(typeof(SystemDescriptionAttributes), rawResponse) as SystemDescriptionAttributes;

        clientRequest.Should().BeEquivalentTo(serverRequest);
        mapped.Should().NotBeNull();
        mapped!.SystemConfigChanges.Should().Be(15);
        mapped.SystemStringsLanguagesSupported.Should().Contain("en-us");
        mapped.IppVersionsSupported.Should().Contain(new IppVersion(2, 0));
        mapped.SystemInfo.Should().Be("test-info");
        mapped.SystemXriSupported.Should().ContainSingle(x => x.XriUri.ToString() == "ipp://127.0.0.1:631/" && x.XriAuthentication == "none" && x.XriSecurity == "tls");
    }

    [TestMethod]
    public async Task GetSystemAttributesResponseMapping_IncludesSystemConfiguredPrintersAndResources()
    {
        var clientRequest = new GetSystemAttributesRequest
        {
            RequestId = 809,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                SystemUri = new Uri("ipp://127.0.0.1:8631/system"),
                RequestedAttributes = ["system-status"]
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
                    SystemConfiguredPrinters = [new()
                    {
                        PrinterId = 101,
                        PrinterInfo = "configured-printer-info",
                        PrinterIsAcceptingJobs = true,
                        PrinterName = "configured-printer",
                        PrinterServiceType = PrinterServiceType.Print,
                        PrinterState = PrinterState.Idle,
                        PrinterStateReasons = ["printer-state-idle"],
                        PrinterXriSupported = new[]
                        {
                            new SystemXri
                            {
                                XriUri = new Uri("ipp://127.0.0.1:631"),
                                XriAuthentication = "none",
                                XriSecurity = "tls"
                            }
                        }
                    }],
                    SystemConfiguredResources = [new()
                    {
                        ResourceFormat = "application/pdf",
                        ResourceId = 202,
                        ResourceInfo = "configured-resource-info",
                        ResourceName = "configured-resource",
                        ResourceState = ResourceState.Available,
                        ResourceStateReasons = new[] { ResourceStateReason.None },
                        ResourceType = "document"
                    }],
                    PowerLogCol = [new()
                    {
                        LogId = 301,
                        PowerState = PowerState.On,
                        DateTimeAt = new DateTimeOffset(2026, 3, 29, 9, 0, 0, TimeSpan.Zero),
                        Message = "power-log-entry"
                    }],
                    PowerStateCapabilitiesCol = [new()
                    {
                        CanAcceptJobs = true,
                        CanProcessJobs = true,
                        PowerActiveWatts = 120,
                        PowerInactiveWatts = 5,
                        PowerState = PowerState.On
                    }],
                    PowerStateCountersCol = [new()
                    {
                        HibernateTransitions = 1,
                        OnTransitions = 2,
                        StandbyTransitions = 3,
                        SuspendTransitions = 4
                    }],
                    PowerStateMonitorCol = [new()
                    {
                        CurrentMonthKwh = 50,
                        CurrentWatts = 300,
                        LifetimeKwh = 1000,
                        MetersAreActual = true,
                        PowerState = PowerState.On,
                        PowerStateMessage = "monitoring",
                        PowerUsageIsRmsWatts = true,
                        ValidRequestPowerStates = new[] { IppOperation.PrintJob, IppOperation.PausePrinter }
                    }],
                    PowerStateTransitionsCol = [new()
                    {
                        EndPowerState = PowerState.OffSoft,
                        StartPowerState = PowerState.On,
                        StateTransitionSeconds = 180
                    }]
                }
            };

            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        var rawResponse = await client.SendAsync(clientRequest.OperationAttributes.PrinterUri, client.CreateRawRequest(clientRequest));
        var mapped = client.CreateResponse(typeof(GetSystemAttributesResponse), rawResponse) as GetSystemAttributesResponse;

        mapped.Should().NotBeNull();
        mapped!.SystemAttributes.Should().NotBeNull();
        mapped.SystemAttributes.SystemConfiguredPrinters.Should().ContainSingle(x =>
            x.PrinterId == 101 &&
            x.PrinterInfo == "configured-printer-info" &&
            x.PrinterIsAcceptingJobs == true &&
            x.PrinterName == "configured-printer" &&
            x.PrinterServiceType == PrinterServiceType.Print &&
            x.PrinterState == PrinterState.Idle &&
            x.PrinterStateReasons != null &&
            x.PrinterStateReasons.Contains("printer-state-idle") &&
            x.PrinterXriSupported != null &&
            x.PrinterXriSupported.Any(y =>
                y.XriUri.ToString() == "ipp://127.0.0.1:631/" &&
                y.XriAuthentication == "none" &&
                y.XriSecurity == "tls"));

        mapped.SystemAttributes.SystemConfiguredResources.Should().ContainSingle(x =>
            x.ResourceFormat == "application/pdf" &&
            x.ResourceId == 202 &&
            x.ResourceInfo == "configured-resource-info" &&
            x.ResourceName == "configured-resource" &&
            x.ResourceState == ResourceState.Available &&
            x.ResourceStateReasons != null &&
            x.ResourceStateReasons.Contains(ResourceStateReason.None) &&
            x.ResourceType == "document");

        mapped.SystemAttributes.PowerLogCol.Should().ContainSingle(x =>
            x.LogId == 301 &&
            x.PowerState == PowerState.On &&
            x.DateTimeAt == new DateTimeOffset(2026, 3, 29, 9, 0, 0, TimeSpan.Zero) &&
            x.Message == "power-log-entry");

        mapped.SystemAttributes.PowerStateCapabilitiesCol.Should().ContainSingle(x =>
            x.CanAcceptJobs == true &&
            x.CanProcessJobs == true &&
            x.PowerActiveWatts == 120 &&
            x.PowerInactiveWatts == 5 &&
            x.PowerState == PowerState.On);

        mapped.SystemAttributes.PowerStateCountersCol.Should().ContainSingle(x =>
            x.HibernateTransitions == 1 &&
            x.OnTransitions == 2 &&
            x.StandbyTransitions == 3 &&
            x.SuspendTransitions == 4);

        mapped.SystemAttributes.PowerStateMonitorCol.Should().ContainSingle(x =>
            x.CurrentMonthKwh == 50 &&
            x.CurrentWatts == 300 &&
            x.LifetimeKwh == 1000 &&
            x.MetersAreActual == true &&
            x.PowerState == PowerState.On &&
            x.PowerStateMessage == "monitoring" &&
            x.PowerUsageIsRmsWatts == true &&
            x.ValidRequestPowerStates != null &&
            x.ValidRequestPowerStates.Contains(IppOperation.PrintJob) &&
            x.ValidRequestPowerStates.Contains(IppOperation.PausePrinter));

        mapped.SystemAttributes.PowerStateTransitionsCol.Should().ContainSingle(x =>
            x.StartPowerState == PowerState.On &&
            x.EndPowerState == PowerState.OffSoft &&
            x.StateTransitionSeconds == 180);
    }

    [TestMethod]
    public async Task GetSystemAttributesResponseMapping_IncludesStatusAndDescriptionAttributes()
    {
        var clientRequest = new GetSystemAttributesRequest
        {
            RequestId = 808,
            Version = new IppVersion(2, 0),
            OperationAttributes = new()
            {
                PrinterUri = new Uri("http://127.0.0.1:631"),
                SystemUri = new Uri("ipp://127.0.0.1:8631/system"),
                RequestedAttributes = ["system-status", "system-description"]
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
                    SystemState = PrinterState.Processing,
                    SystemStateMessage = "Active",
                    SystemStateReasons = ["system-state-changed"],
                    SystemStateChangeTime = 13,
                    SystemUuid = new Uri("urn:uuid:1234"),
                    SystemStateChangeDateTime = new DateTimeOffset(2026, 3, 29, 12, 0, 0, TimeSpan.Zero),
                    SystemUpTime = 60,
                    SystemTimeSourceConfigured = "ntp",
                    XriAuthenticationSupported = [UriAuthentication.None],
                    XriSecuritySupported = [UriSecurity.Tls],
                    XriUriSchemeSupported = [UriScheme.Https]
                },
                SystemDescriptionAttributes = new()
                {
                    SystemConfigChanges = 7,
                    SystemConfiguredPrinters = 2,
                    SystemConfiguredResources = 5,
                    CharsetConfigured = "utf-8",
                    CharsetSupported = ["utf-8", "us-ascii"],
                    DocumentFormatSupported = ["application/pdf"],
                    GeneratedNaturalLanguageSupported = ["en-us"],
                    IppFeaturesSupported = ["ippget"],
                    IppVersionsSupported = new[] { new IppVersion(2, 0) },
                    MultipleDocumentPrintersSupported = true,
                    NaturalLanguageConfigured = "en-us",
                    IppGetEventLife = 900,
                    NotifyAttributesSupported = ["system-state"],
                    NotifyEventsDefault = ["system-state-changed"],
                    NotifyEventsSupported = ["system-state-changed"],
                    NotifyLeaseDurationDefault = 3600,
                    NotifyLeaseDurationSupported = ["60", "3600"],
                    NotifyMaxEventsSupported = 100,
                    NotifyPullMethodSupported = ["ippget"],
                    NotifySchemesSupported = [UriScheme.Http],
                    SystemStringsLanguagesSupported = ["en-us"],
                    SystemStringsUri = ["http://127.0.0.1:631/system-strings"],
                    SystemSerialNumber = "sn-1234",
                    SystemImpressionsCompleted = 50,
                    SystemImpressionsCompletedCol = 60,
                    SystemMediaSheetsCompleted = 70,
                    SystemMediaSheetsCompletedCol = 80,
                    SystemPagesCompleted = 90,
                    SystemPagesCompletedCol = 100,
                    SystemConfigChangeTime = 11,
                    SystemConfigChangeDateTime = new DateTimeOffset(2026, 3, 29, 11, 0, 0, TimeSpan.Zero),
                    SystemUpTime = 56775,
                    SystemUuid = new Uri("urn:uuid:1234"),
                    SystemGeoLocation = new Uri("geo:37.7749,-122.4194"),
                    SystemAssetTag = new byte[] { 1, 2, 3 },
                    SystemCurrentTime = new DateTimeOffset(2026, 3, 29, 12, 0, 0, TimeSpan.Zero),
                    SystemContactCol = new[]
                    {
                        new SystemContact
                        {
                            ContactName = "contact-1",
                            ContactUri = new Uri("mailto:contact-1@example.com"),
                            ContactVcard = new[] { "BEGIN:VCARD", "END:VCARD" }
                        }
                    },
                    SystemServiceContactCol = new[]
                    {
                        new SystemContact
                        {
                            ContactName = "service-1",
                            ContactUri = new Uri("mailto:service-1@example.com"),
                            ContactVcard = new[] { "BEGIN:VCARD", "END:VCARD" }
                        }
                    },
                    SystemXriSupported = new[]
                    {
                        new SystemXri
                        {
                            XriUri = new Uri("ipp://127.0.0.1:631"),
                            XriAuthentication = "none",
                            XriSecurity = "tls"
                        }
                    },
                    SystemInfo = "info",
                    SystemLocation = "location",
                    SystemMakeAndModel = "model",
                    SystemMessageFromOperator = "operator",
                    SystemName = "system-name",
                    SystemDefaultPrinterId = 1,
                    SystemDnsSdName = "printer._ipp._tcp.local",
                    SystemMandatoryPrinterAttributes = ["printer-name"],
                    SystemMandatoryRegistrationAttributes = ["system-state"],
                    SystemSettableAttributesSupported = ["system-state"],
                    SystemFirmwareName = ["firmware1"],
                    SystemFirmwarePatches = ["patch1"],
                    SystemFirmwareStringVersion = ["v1"],
                    SystemFirmwareVersion = ["1.0"],
                    SystemResidentApplicationName = ["app1"],
                    SystemResidentApplicationPatches = ["patch1"],
                    SystemResidentApplicationStringVersion = ["v1"],
                    SystemResidentApplicationVersion = ["1.0"],
                    SystemUserApplicationName = ["userapp1"],
                    SystemUserApplicationPatches = ["patch1"],
                    SystemUserApplicationStringVersion = ["v1"],
                    SystemUserApplicationVersion = ["1.0"],
                    OperationsSupported = new[] { IppOperation.GetSystemAttributes, IppOperation.RestartOnePrinter },
                    OutputDeviceX509TypeSupported = ["x509"],
                    PowerCalendarPolicyCol = new[]
                    {
                        new PowerCalendarPolicy
                        {
                            CalendarId = 1,
                            DayOfWeek = 1,
                            DayOfMonth = 1,
                            Month = 1,
                            Hour = 10,
                            Minute = 30,
                            RequestPowerState = PowerState.On,
                            RunOnce = true
                        }
                    },
                    PowerEventPolicyCol = new[]
                    {
                        new PowerEventPolicy
                        {
                            EventId = 2,
                            EventName = "power-event",
                            RequestPowerState = PowerState.Standby
                        }
                    },
                    PowerTimeoutPolicyCol = new[]
                    {
                        new PowerTimeoutPolicy
                        {
                            TimeoutId = 3,
                            TimeoutPredicate = "idle",
                            TimeoutSeconds = 120,
                            RequestPowerState = PowerState.Suspend,
                            StartPowerState = PowerState.On
                        }
                    },
                    PrinterCreationAttributesSupported = ["printer-name"],
                    PrinterServiceTypeSupported = [PrinterServiceType.Print],
                    ResourceFormatSupported = ["application/pdf"],
                    ResourceTypeSupported = ["document"],
                    ResourceSettableAttributesSupported = ["resource-state"],
                    SystemTimeSourceConfigured = "ntp"
                }
            };

            var ms = new MemoryStream();
            await server.SendResponseAsync(serverResponse, ms, c);
            ms.Seek(0, SeekOrigin.Begin);
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StreamContent(ms) };
        }

        var client = new SharpIppClient(new(GetMockOfHttpMessageHandler(func).Object));
        var rawResponse = await client.SendAsync(clientRequest.OperationAttributes.PrinterUri, client.CreateRawRequest(clientRequest));
        var mapped = client.CreateResponse(typeof(GetSystemAttributesResponse), rawResponse) as GetSystemAttributesResponse;

        mapped.Should().NotBeNull();
        mapped!.SystemAttributes.Should().NotBeNull();
        mapped.SystemAttributes!.SystemState.Should().Be(PrinterState.Processing);
        mapped.SystemAttributes.SystemStateMessage.Should().Be("Active");
        mapped.SystemAttributes.SystemStateReasons.Should().Contain("system-state-changed");

        mapped.SystemDescriptionAttributes.Should().NotBeNull();
        mapped.SystemDescriptionAttributes.SystemConfigChanges.Should().Be(7);
        mapped.SystemDescriptionAttributes.SystemConfiguredPrinters.Should().Be(2);
        mapped.SystemDescriptionAttributes.SystemConfiguredResources.Should().Be(5);
        mapped.SystemDescriptionAttributes.CharsetConfigured.Should().Be("utf-8");
        mapped.SystemDescriptionAttributes.CharsetSupported.Should().Contain("utf-8");
        mapped.SystemDescriptionAttributes.DocumentFormatSupported.Should().Contain("application/pdf");
        mapped.SystemDescriptionAttributes.GeneratedNaturalLanguageSupported.Should().Contain("en-us");
        mapped.SystemDescriptionAttributes.IppFeaturesSupported.Should().Contain("ippget");
        mapped.SystemDescriptionAttributes.IppVersionsSupported.Should().Contain(new IppVersion(2,0));
        mapped.SystemDescriptionAttributes.MultipleDocumentPrintersSupported.Should().BeTrue();
        mapped.SystemDescriptionAttributes.NaturalLanguageConfigured.Should().Be("en-us");
        mapped.SystemDescriptionAttributes.IppGetEventLife.Should().Be(900);
        mapped.SystemDescriptionAttributes.NotifyAttributesSupported.Should().Contain("system-state");
        mapped.SystemDescriptionAttributes.NotifyEventsDefault.Should().Contain("system-state-changed");
        mapped.SystemDescriptionAttributes.NotifyEventsSupported.Should().Contain("system-state-changed");
        mapped.SystemDescriptionAttributes.NotifyLeaseDurationDefault.Should().Be(3600);
        mapped.SystemDescriptionAttributes.NotifyLeaseDurationSupported.Should().Contain("60");
        mapped.SystemDescriptionAttributes.NotifyMaxEventsSupported.Should().Be(100);
        mapped.SystemDescriptionAttributes.NotifyPullMethodSupported.Should().Contain("ippget");
        mapped.SystemDescriptionAttributes.NotifySchemesSupported.Should().Contain(UriScheme.Http);
        mapped.SystemDescriptionAttributes.SystemStringsLanguagesSupported.Should().Contain("en-us");
        mapped.SystemDescriptionAttributes.SystemStringsUri.Should().Contain("http://127.0.0.1:631/system-strings");
        mapped.SystemDescriptionAttributes.SystemSerialNumber.Should().Be("sn-1234");
        mapped.SystemDescriptionAttributes.SystemImpressionsCompleted.Should().Be(50);
        mapped.SystemDescriptionAttributes.SystemImpressionsCompletedCol.Should().Be(60);
        mapped.SystemDescriptionAttributes.SystemMediaSheetsCompleted.Should().Be(70);
        mapped.SystemDescriptionAttributes.SystemMediaSheetsCompletedCol.Should().Be(80);
        mapped.SystemDescriptionAttributes.SystemPagesCompleted.Should().Be(90);
        mapped.SystemDescriptionAttributes.SystemPagesCompletedCol.Should().Be(100);
        mapped.SystemDescriptionAttributes.SystemConfigChangeTime.Should().Be(11);
        mapped.SystemDescriptionAttributes.SystemConfigChangeDateTime.Should().Be(new DateTimeOffset(2026, 3, 29, 11, 0, 0, TimeSpan.Zero));
        mapped.SystemDescriptionAttributes.SystemUpTime.Should().Be(60);
        mapped.SystemDescriptionAttributes.SystemUuid.Should().Be(new Uri("urn:uuid:1234"));
        mapped.SystemDescriptionAttributes.SystemGeoLocation.Should().Be(new Uri("geo:37.7749,-122.4194"));
        mapped.SystemDescriptionAttributes.SystemInfo.Should().Be("info");
        mapped.SystemDescriptionAttributes.SystemLocation.Should().Be("location");
        mapped.SystemDescriptionAttributes.SystemMakeAndModel.Should().Be("model");
        mapped.SystemDescriptionAttributes.SystemMessageFromOperator.Should().Be("operator");
        mapped.SystemDescriptionAttributes.SystemName.Should().Be("system-name");
        mapped.SystemDescriptionAttributes.SystemDefaultPrinterId.Should().Be(1);
        mapped.SystemDescriptionAttributes.SystemDnsSdName.Should().Be("printer._ipp._tcp.local");
        mapped.SystemDescriptionAttributes.SystemMandatoryPrinterAttributes.Should().Contain("printer-name");
        mapped.SystemDescriptionAttributes.SystemMandatoryRegistrationAttributes.Should().Contain("system-state");
        mapped.SystemDescriptionAttributes.SystemSettableAttributesSupported.Should().Contain("system-state");
        mapped.SystemDescriptionAttributes.PrinterCreationAttributesSupported.Should().Contain("printer-name");
        mapped.SystemDescriptionAttributes.PrinterServiceTypeSupported.Should().Contain(PrinterServiceType.Print);
        mapped.SystemDescriptionAttributes.ResourceFormatSupported.Should().Contain("application/pdf");
        mapped.SystemDescriptionAttributes.ResourceTypeSupported.Should().Contain("document");
        mapped.SystemDescriptionAttributes.ResourceSettableAttributesSupported.Should().Contain("resource-state");
        mapped.SystemDescriptionAttributes.OperationsSupported.Should().Contain(IppOperation.GetSystemAttributes);
        mapped.SystemDescriptionAttributes.OperationsSupported.Should().Contain(IppOperation.RestartOnePrinter);
        mapped.SystemDescriptionAttributes.OutputDeviceX509TypeSupported.Should().Contain("x509");
        mapped.SystemDescriptionAttributes.PowerCalendarPolicyCol.Should().ContainSingle(x => x.CalendarId == 1 && x.RequestPowerState == PowerState.On && x.RunOnce == true);
        mapped.SystemDescriptionAttributes.PowerEventPolicyCol.Should().ContainSingle(x => x.EventId == 2 && x.EventName == "power-event" && x.RequestPowerState == PowerState.Standby);
        mapped.SystemDescriptionAttributes.PowerTimeoutPolicyCol.Should().ContainSingle(x => x.TimeoutId == 3 && x.TimeoutPredicate == "idle" && x.TimeoutSeconds == 120 && x.RequestPowerState == PowerState.Suspend && x.StartPowerState == PowerState.On);
        mapped.SystemDescriptionAttributes.SystemFirmwareName.Should().Contain("firmware1");
        mapped.SystemDescriptionAttributes.SystemFirmwarePatches.Should().Contain("patch1");
        mapped.SystemDescriptionAttributes.SystemFirmwareStringVersion.Should().Contain("v1");
        mapped.SystemDescriptionAttributes.SystemFirmwareVersion.Should().Contain("1.0");
        mapped.SystemDescriptionAttributes.SystemResidentApplicationName.Should().Contain("app1");
        mapped.SystemDescriptionAttributes.SystemResidentApplicationPatches.Should().Contain("patch1");
        mapped.SystemDescriptionAttributes.SystemResidentApplicationStringVersion.Should().Contain("v1");
        mapped.SystemDescriptionAttributes.SystemResidentApplicationVersion.Should().Contain("1.0");
        mapped.SystemDescriptionAttributes.SystemUserApplicationName.Should().Contain("userapp1");
        mapped.SystemDescriptionAttributes.SystemUserApplicationPatches.Should().Contain("patch1");
        mapped.SystemDescriptionAttributes.SystemUserApplicationStringVersion.Should().Contain("v1");
        mapped.SystemDescriptionAttributes.SystemUserApplicationVersion.Should().Contain("1.0");
        mapped.SystemDescriptionAttributes.SystemTimeSourceConfigured.Should().Be("ntp");
        mapped.SystemDescriptionAttributes.SystemContactCol.Should().ContainSingle(x => x.ContactName == "contact-1" && x.ContactUri.ToString() == "mailto:contact-1@example.com");
        mapped.SystemDescriptionAttributes.SystemServiceContactCol.Should().ContainSingle(x => x.ContactName == "service-1" && x.ContactUri.ToString() == "mailto:service-1@example.com");
        mapped.SystemDescriptionAttributes.SystemXriSupported.Should().ContainSingle(x => x.XriUri.ToString() == "ipp://127.0.0.1:631/" && x.XriAuthentication == "none" && x.XriSecurity == "tls");
        mapped.SystemDescriptionAttributes.IppGetEventLife.Should().Be(900);
        mapped.SystemAttributes.SystemStateChangeTime.Should().Be(13);
        mapped.SystemAttributes.SystemStateChangeDateTime.Should().Be(new DateTimeOffset(2026, 3, 29, 12, 0, 0, TimeSpan.Zero));
        mapped.SystemAttributes.SystemUpTime.Should().Be(60);
        mapped.SystemAttributes.SystemTimeSourceConfigured.Should().Be("ntp");
        mapped.SystemAttributes.SystemUuid.Should().Be(new Uri("urn:uuid:1234"));
        mapped.SystemAttributes.XriAuthenticationSupported.Should().Contain(UriAuthentication.None);
        mapped.SystemAttributes.XriSecuritySupported.Should().Contain(UriSecurity.Tls);
        mapped.SystemAttributes.XriUriSchemeSupported.Should().Contain(UriScheme.Https);
    }
}