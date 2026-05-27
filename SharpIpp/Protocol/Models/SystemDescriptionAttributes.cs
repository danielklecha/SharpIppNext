using System;
using System.ComponentModel.DataAnnotations;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Description attributes for a System object.
/// See: PWG 5100.22-2025 Section 7.3
/// </summary>
public class SystemDescriptionAttributes
{
    /// <summary>
    /// <c>system-config-changes</c>
    /// See: PWG 5100.22-2025 Section 7.3.3
    /// </summary>
    public int? SystemConfigChanges { get; set; }

    /// <summary>
    /// <c>system-configured-printers</c>
    /// See: PWG 5100.22-2025 Section 7.3.4
    /// </summary>
    public int? SystemConfiguredPrinters { get; set; }

    /// <summary>
    /// <c>system-configured-resources</c>
    /// See: PWG 5100.22-2025 Section 7.3.5
    /// </summary>
    public int? SystemConfiguredResources { get; set; }

    /// <summary>
    /// <c>charset-configured</c>
    /// See: PWG 5100.22-2025 Section 7.3.6
    /// </summary>
    public string? CharsetConfigured { get; set; }

    /// <summary>
    /// <c>charset-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.7
    /// </summary>
    public string[]? CharsetSupported { get; set; }

    /// <summary>
    /// <c>document-format-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.8
    /// </summary>
    public string[]? DocumentFormatSupported { get; set; }

    /// <summary>
    /// <c>generated-natural-language-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.9
    /// </summary>
    public string[]? GeneratedNaturalLanguageSupported { get; set; }

    /// <summary>
    /// <c>ipp-features-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.10
    /// </summary>
    public IppFeature[]? IppFeaturesSupported { get; set; }

    /// <summary>
    /// <c>ipp-versions-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.11
    /// </summary>
    public IppVersion[]? IppVersionsSupported { get; set; }

    /// <summary>
    /// The IPP Get event life in seconds.
    /// Type: integer(15:MAX)
    /// See: PWG 5100.22-2025 Section 7.1.25
    /// </summary>
    public int? IppGetEventLife { get; set; }

    /// <summary>
    /// <c>multiple-document-printers-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.12
    /// </summary>
    public bool? MultipleDocumentPrintersSupported { get; set; }

    /// <summary>
    /// <c>notify-attributes-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.13
    /// </summary>
    public string[]? NotifyAttributesSupported { get; set; }

    /// <summary>
    /// <c>notify-events-default</c>
    /// See: PWG 5100.22-2025 Section 7.3.14
    /// </summary>
    public NotifyEvent[]? NotifyEventsDefault { get; set; }

    /// <summary>
    /// <c>notify-events-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.15
    /// </summary>
    public NotifyEvent[]? NotifyEventsSupported { get; set; }

    /// <summary>
    /// <c>notify-lease-duration-default</c>
    /// See: PWG 5100.22-2025 Section 7.3.16
    /// </summary>
    [Range(0, 67108863)]
    public int? NotifyLeaseDurationDefault { get; set; }

    /// <summary>
    /// <c>notify-lease-duration-supported</c>
    /// Type: rangeOfInteger(0:67108863)
    /// See: PWG 5100.22-2025 Section 7.3.17
    /// </summary>
    public string[]? NotifyLeaseDurationSupported { get; set; }

    /// <summary>
    /// <c>notify-max-events-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.18
    /// </summary>
    public int? NotifyMaxEventsSupported { get; set; }

    /// <summary>
    /// <c>notify-pull-method-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.19
    /// </summary>
    public NotifyPullMethod[]? NotifyPullMethodSupported { get; set; }

    /// <summary>
    /// <c>notify-schemes-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.20
    /// </summary>
    public UriScheme[]? NotifySchemesSupported { get; set; }

    /// <summary>
    /// <c>natural-language-configured</c>
    /// See: PWG 5100.22-2025 Section 7.3.21
    /// </summary>
    public string? NaturalLanguageConfigured { get; set; }

    /// <summary>
    /// <c>operations-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.22
    /// </summary>
    public IppOperation[]? OperationsSupported { get; set; }

    /// <summary>
    /// <c>output-device-x509-type-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.23
    /// </summary>
    public X509Type[]? OutputDeviceX509TypeSupported { get; set; }

    /// <summary>
    /// <c>power-calendar-policy-col</c>
    /// See: PWG 5100.22-2025 Section 7.3.24
    /// </summary>
    public PowerCalendarPolicy[]? PowerCalendarPolicyCol { get; set; }

    /// <summary>
    /// <c>power-event-policy-col</c>
    /// See: PWG 5100.22-2025 Section 7.3.25
    /// </summary>
    public PowerEventPolicy[]? PowerEventPolicyCol { get; set; }

    /// <summary>
    /// <c>power-timeout-policy-col</c>
    /// See: PWG 5100.22-2025 Section 7.3.26
    /// </summary>
    public PowerTimeoutPolicy[]? PowerTimeoutPolicyCol { get; set; }

    /// <summary>
    /// <c>printer-creation-attributes-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.27
    /// </summary>
    public PrinterCreationAttribute[]? PrinterCreationAttributesSupported { get; set; }

    /// <summary>
    /// <c>printer-service-type-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.28
    /// </summary>
    public PrinterServiceType[]? PrinterServiceTypeSupported { get; set; }

    /// <summary>
    /// <c>resource-format-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.29
    /// </summary>
    public ResourceFormat[]? ResourceFormatSupported { get; set; }

    /// <summary>
    /// <c>resource-type-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.30
    /// </summary>
    public ResourceType[]? ResourceTypeSupported { get; set; }

    /// <summary>
    /// <c>resource-settable-attributes-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.31
    /// </summary>
    public ResourceSettableAttribute[]? ResourceSettableAttributesSupported { get; set; }

    /// <summary>
    /// <c>system-strings-languages-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.32
    /// </summary>
    public string[]? SystemStringsLanguagesSupported { get; set; }

    /// <summary>
    /// <c>system-strings-uri</c>
    /// See: PWG 5100.22-2025 Section 7.3.33
    /// </summary>
    public string[]? SystemStringsUri { get; set; }

    /// <summary>
    /// <c>system-serial-number</c>
    /// See: PWG 5100.22-2025 Section 7.3.34
    /// </summary>
    public string? SystemSerialNumber { get; set; }

    /// <summary>
    /// <c>system-impressions-completed</c>
    /// See: PWG 5100.22-2025 Section 7.3.35
    /// </summary>
    public int? SystemImpressionsCompleted { get; set; }

    /// <summary>
    /// <c>system-impressions-completed-col</c>
    /// See: PWG 5100.22-2025 Section 7.3.36
    /// </summary>
    public int? SystemImpressionsCompletedCol { get; set; }

    /// <summary>
    /// <c>system-media-sheets-completed</c>
    /// See: PWG 5100.22-2025 Section 7.3.37
    /// </summary>
    public int? SystemMediaSheetsCompleted { get; set; }

    /// <summary>
    /// <c>system-media-sheets-completed-col</c>
    /// See: PWG 5100.22-2025 Section 7.3.38
    /// </summary>
    public int? SystemMediaSheetsCompletedCol { get; set; }

    /// <summary>
    /// <c>system-pages-completed</c>
    /// See: PWG 5100.22-2025 Section 7.3.39
    /// </summary>
    public int? SystemPagesCompleted { get; set; }

    /// <summary>
    /// <c>system-pages-completed-col</c>
    /// See: PWG 5100.22-2025 Section 7.3.40
    /// </summary>
    public int? SystemPagesCompletedCol { get; set; }

    /// <summary>
    /// <c>system-config-change-time</c>
    /// See: PWG 5100.22-2025 Section 7.3.41
    /// </summary>
    public int? SystemConfigChangeTime { get; set; }

    /// <summary>
    /// <c>system-config-change-date-time</c>
    /// See: PWG 5100.22-2025 Section 7.3.42
    /// </summary>
    public DateTimeOffset? SystemConfigChangeDateTime { get; set; }

    /// <summary>
    /// <c>system-up-time</c>
    /// Type: integer(1:MAX)
    /// See: PWG 5100.22-2025 Section 7.3.43
    /// </summary>
    public int? SystemUpTime { get; set; }

    /// <summary>
    /// <c>system-uuid</c>
    /// See: PWG 5100.22-2025 Section 7.3.44
    /// </summary>
    public Uri? SystemUuid { get; set; }

    /// <summary>
    /// <c>system-geo-location</c>
    /// See: PWG 5100.22-2025 Section 7.3.45
    /// </summary>
    public Uri? SystemGeoLocation { get; set; }

    /// <summary>
    /// <c>system-asset-tag</c>
    /// See: PWG 5100.22-2025 Section 7.3.46
    /// </summary>
    public OctetString? SystemAssetTag { get; set; }

    /// <summary>
    /// <c>system-current-time</c>
    /// See: PWG 5100.22-2025 Section 7.3.47
    /// </summary>
    public DateTimeOffset? SystemCurrentTime { get; set; }

    /// <summary>
    /// <c>system-contact-col</c>
    /// See: PWG 5100.22-2025 Section 7.3.48
    /// </summary>
    public SystemContact[]? SystemContactCol { get; set; }

    /// <summary>
    /// <c>system-service-contact-col</c>
    /// See: PWG 5100.22-2025 Section 7.3.49
    /// </summary>
    public SystemContact[]? SystemServiceContactCol { get; set; }

    /// <summary>
    /// <c>system-xri-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.50
    /// </summary>
    public SystemXri[]? SystemXriSupported { get; set; }

    /// <summary>
    /// <c>system-info</c>
    /// See: PWG 5100.22-2025 Section 7.3.51
    /// </summary>
    public string? SystemInfo { get; set; }

    /// <summary>
    /// <c>system-location</c>
    /// See: PWG 5100.22-2025 Section 7.3.52
    /// </summary>
    public string? SystemLocation { get; set; }

    /// <summary>
    /// <c>system-make-and-model</c>
    /// See: PWG 5100.22-2025 Section 7.3.53
    /// </summary>
    public string? SystemMakeAndModel { get; set; }

    /// <summary>
    /// <c>system-message-from-operator</c>
    /// See: PWG 5100.22-2025 Section 7.3.54
    /// </summary>
    public string? SystemMessageFromOperator { get; set; }

    /// <summary>
    /// <c>system-name</c>
    /// See: PWG 5100.22-2025 Section 7.3.55
    /// </summary>
    public string? SystemName { get; set; }

    /// <summary>
    /// <c>system-default-printer-id</c>
    /// Type: integer(1:65535)
    /// See: PWG 5100.22-2025 Section 7.3.56
    /// </summary>
    public int? SystemDefaultPrinterId { get; set; }

    /// <summary>
    /// <c>system-dns-sd-name</c>
    /// See: PWG 5100.22-2025 Section 7.3.57
    /// </summary>
    public string? SystemDnsSdName { get; set; }

    /// <summary>
    /// <c>system-mandatory-printer-attributes</c>
    /// See: PWG 5100.22-2025 Section 7.3.58
    /// </summary>
    public SystemMandatoryPrinterAttribute[]? SystemMandatoryPrinterAttributes { get; set; }

    /// <summary>
    /// <c>system-mandatory-registration-attributes</c>
    /// See: PWG 5100.22-2025 Section 7.3.59
    /// </summary>
    public SystemMandatoryRegistrationAttribute[]? SystemMandatoryRegistrationAttributes { get; set; }

    /// <summary>
    /// <c>system-settable-attributes-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.60
    /// </summary>
    public SystemSettableAttribute[]? SystemSettableAttributesSupported { get; set; }

    /// <summary>
    /// <c>system-firmware-name</c>
    /// See: PWG 5100.22-2025 Section 7.3.61
    /// </summary>
    public string[]? SystemFirmwareName { get; set; }

    /// <summary>
    /// <c>system-firmware-patches</c>
    /// See: PWG 5100.22-2025 Section 7.3.62
    /// </summary>
    public string[]? SystemFirmwarePatches { get; set; }

    /// <summary>
    /// <c>system-firmware-string-version</c>
    /// See: PWG 5100.22-2025 Section 7.3.63
    /// </summary>
    public string[]? SystemFirmwareStringVersion { get; set; }

    /// <summary>
    /// <c>system-firmware-version</c>
    /// See: PWG 5100.22-2025 Section 7.3.64
    /// </summary>
    public OctetString[]? SystemFirmwareVersion { get; set; }

    /// <summary>
    /// <c>system-resident-application-name</c>
    /// See: PWG 5100.22-2025 Section 7.3.65
    /// </summary>
    public string[]? SystemResidentApplicationName { get; set; }

    /// <summary>
    /// <c>system-resident-application-patches</c>
    /// See: PWG 5100.22-2025 Section 7.3.66
    /// </summary>
    public string[]? SystemResidentApplicationPatches { get; set; }

    /// <summary>
    /// <c>system-resident-application-string-version</c>
    /// See: PWG 5100.22-2025 Section 7.3.67
    /// </summary>
    public string[]? SystemResidentApplicationStringVersion { get; set; }

    /// <summary>
    /// <c>system-resident-application-version</c>
    /// See: PWG 5100.22-2025 Section 7.3.68
    /// </summary>
    public OctetString[]? SystemResidentApplicationVersion { get; set; }

    /// <summary>
    /// <c>system-user-application-name</c>
    /// See: PWG 5100.22-2025 Section 7.3.69
    /// </summary>
    public string[]? SystemUserApplicationName { get; set; }

    /// <summary>
    /// <c>system-user-application-patches</c>
    /// See: PWG 5100.22-2025 Section 7.3.70
    /// </summary>
    public string[]? SystemUserApplicationPatches { get; set; }

    /// <summary>
    /// <c>system-user-application-string-version</c>
    /// See: PWG 5100.22-2025 Section 7.3.71
    /// </summary>
    public string[]? SystemUserApplicationStringVersion { get; set; }

    /// <summary>
    /// <c>system-user-application-version</c>
    /// See: PWG 5100.22-2025 Section 7.3.72
    /// </summary>
    public OctetString[]? SystemUserApplicationVersion { get; set; }

    /// <summary>
    /// <c>system-time-source-configured</c>
    /// See: PWG 5100.22-2025 Section 7.3.73
    /// </summary>
    public SystemTimeSourceConfigured? SystemTimeSourceConfigured { get; set; }

    /// <summary>
    /// The current state of the System object (idle=3, processing=4, stopped=5).
    /// See: PWG 5100.22-2025 Section 7.3.26
    /// </summary>
    public PrinterState? SystemState { get; set; }

    /// <summary>
    /// One or more reasons for the current system state.
    /// See: PWG 5100.22-2025 Section 7.3.30
    /// </summary>
    public SystemStateReason[]? SystemStateReasons { get; set; }

    /// <summary>
    /// Human-readable message describing the current system state.
    /// See: PWG 5100.22-2025 Section 7.3.29
    /// </summary>
    public string? SystemStateMessage { get; set; }

    /// <summary>
    /// Time in seconds since system boot when the system state last changed.
    /// See: PWG 5100.22-2025 Section 7.3.28
    /// </summary>
    public int? SystemStateChangeTime { get; set; }

    /// <summary>
    /// Date and time when the system state last changed.
    /// See: PWG 5100.22-2025 Section 7.3.27
    /// </summary>
    public DateTimeOffset? SystemStateChangeDateTime { get; set; }
}
