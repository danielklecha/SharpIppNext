using SharpIpp.Mapping;
using System;
using SharpIpp.Validation;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Description attributes for a System object.
/// See: PWG 5100.22-2025 Section 7.3
/// </summary>
[IppAttribute]
[IppSection(SectionTag.SystemAttributesTag)]
public class SystemDescriptionAttributes
{
    /// <summary>
    /// <c>system-config-changes</c>
    /// See: PWG 5100.22-2025 Section 7.3.3
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemConfigChanges, Tag = Tag.Integer)]
    public int? SystemConfigChanges { get; set; }

    /// <summary>
    /// <c>system-configured-printers</c>
    /// See: PWG 5100.22-2025 Section 7.3.4
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemConfiguredPrinters, Tag = Tag.Integer)]
    public int? SystemConfiguredPrinters { get; set; }

    /// <summary>
    /// <c>system-configured-resources</c>
    /// See: PWG 5100.22-2025 Section 7.3.5
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemConfiguredResources, Tag = Tag.Integer)]
    public int? SystemConfiguredResources { get; set; }

    /// <summary>
    /// <c>charset-configured</c>
    /// See: PWG 5100.22-2025 Section 7.3.6
    /// </summary>
    [IppAttribute(IppAttributeNames.CharsetConfigured, Tag = Tag.Charset)]
    public string? CharsetConfigured { get; set; }

    /// <summary>
    /// <c>charset-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.7
    /// </summary>
    [IppAttribute(IppAttributeNames.CharsetSupported, Tag = Tag.Charset)]
    public string[]? CharsetSupported { get; set; }

    /// <summary>
    /// <c>document-format-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.8
    /// </summary>
    [IppAttribute(IppAttributeNames.DocumentFormatSupported, Tag = Tag.MimeMediaType)]
    public string[]? DocumentFormatSupported { get; set; }

    /// <summary>
    /// <c>generated-natural-language-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.9
    /// </summary>
    [IppAttribute(IppAttributeNames.GeneratedNaturalLanguageSupported, Tag = Tag.NaturalLanguage)]
    public string[]? GeneratedNaturalLanguageSupported { get; set; }

    /// <summary>
    /// <c>ipp-features-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.10
    /// </summary>
    [IppAttribute(IppAttributeNames.IppFeaturesSupported, Tag = Tag.Keyword)]
    public IppFeature[]? IppFeaturesSupported { get; set; }

    /// <summary>
    /// <c>ipp-versions-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.11
    /// </summary>
    [IppAttribute(IppAttributeNames.IppVersionsSupported, Tag = Tag.Keyword)]
    public IppVersion[]? IppVersionsSupported { get; set; }

    /// <summary>
    /// The IPP Get event life in seconds.
    /// Type: integer(15:MAX)
    /// See: PWG 5100.22-2025 Section 7.1.25
    /// </summary>
    [IppAttribute(IppAttributeNames.IppGetEventLife, Tag = Tag.Integer)]
    public int? IppGetEventLife { get; set; }

    /// <summary>
    /// <c>multiple-document-printers-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.12
    /// </summary>
    [IppAttribute(IppAttributeNames.MultipleDocumentPrintersSupported, Tag = Tag.Boolean)]
    public bool? MultipleDocumentPrintersSupported { get; set; }

    /// <summary>
    /// <c>notify-attributes-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.13
    /// </summary>
    [IppAttribute(IppAttributeNames.NotifyAttributesSupported, Tag = Tag.Keyword)]
    public string[]? NotifyAttributesSupported { get; set; }

    /// <summary>
    /// <c>notify-events-default</c>
    /// See: PWG 5100.22-2025 Section 7.3.14
    /// </summary>
    [IppAttribute(IppAttributeNames.NotifyEventsDefault, Tag = Tag.Keyword)]
    public NotifyEvent[]? NotifyEventsDefault { get; set; }

    /// <summary>
    /// <c>notify-events-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.15
    /// </summary>
    [IppAttribute(IppAttributeNames.NotifyEventsSupported, Tag = Tag.Keyword)]
    public NotifyEvent[]? NotifyEventsSupported { get; set; }

    /// <summary>
    /// <c>notify-lease-duration-default</c>
    /// See: PWG 5100.22-2025 Section 7.3.16
    /// </summary>
    [Range(0, 67108863)]
    [IppAttribute(IppAttributeNames.NotifyLeaseDurationDefault, Tag = Tag.Integer)]
    public int? NotifyLeaseDurationDefault { get; set; }

    /// <summary>
    /// <c>notify-lease-duration-supported</c>
    /// Type: rangeOfInteger(0:67108863)
    /// See: PWG 5100.22-2025 Section 7.3.17
    /// </summary>
    [IppAttribute(IppAttributeNames.NotifyLeaseDurationSupported, Tag = Tag.Keyword)]
    public string[]? NotifyLeaseDurationSupported { get; set; }

    /// <summary>
    /// <c>notify-max-events-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.18
    /// </summary>
    [IppAttribute(IppAttributeNames.NotifyMaxEventsSupported, Tag = Tag.Integer)]
    public int? NotifyMaxEventsSupported { get; set; }

    /// <summary>
    /// <c>notify-pull-method-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.19
    /// </summary>
    [IppAttribute(IppAttributeNames.NotifyPullMethodSupported, Tag = Tag.Keyword)]
    public NotifyPullMethod[]? NotifyPullMethodSupported { get; set; }

    /// <summary>
    /// <c>notify-schemes-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.20
    /// </summary>
    [IppAttribute(IppAttributeNames.NotifySchemesSupported, Tag = Tag.Keyword)]
    public UriScheme[]? NotifySchemesSupported { get; set; }

    /// <summary>
    /// <c>natural-language-configured</c>
    /// See: PWG 5100.22-2025 Section 7.3.21
    /// </summary>
    [IppAttribute(IppAttributeNames.NaturalLanguageConfigured, Tag = Tag.NaturalLanguage)]
    public string? NaturalLanguageConfigured { get; set; }

    /// <summary>
    /// <c>operations-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.22
    /// </summary>
    [IppAttribute(IppAttributeNames.OperationsSupported, Tag = Tag.Enum)]
    public IppOperation[]? OperationsSupported { get; set; }

    /// <summary>
    /// <c>output-device-x509-type-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.23
    /// </summary>
    [IppAttribute(IppAttributeNames.OutputDeviceX509TypeSupported, Tag = Tag.Keyword)]
    public X509Type[]? OutputDeviceX509TypeSupported { get; set; }

    /// <summary>
    /// <c>power-calendar-policy-col</c>
    /// See: PWG 5100.22-2025 Section 7.3.24
    /// </summary>
    [IppAttribute(IppAttributeNames.PowerCalendarPolicyCol)]
    public PowerCalendarPolicy[]? PowerCalendarPolicyCol { get; set; }

    /// <summary>
    /// <c>power-event-policy-col</c>
    /// See: PWG 5100.22-2025 Section 7.3.25
    /// </summary>
    [IppAttribute(IppAttributeNames.PowerEventPolicyCol)]
    public PowerEventPolicy[]? PowerEventPolicyCol { get; set; }

    /// <summary>
    /// <c>power-timeout-policy-col</c>
    /// See: PWG 5100.22-2025 Section 7.3.26
    /// </summary>
    [IppAttribute(IppAttributeNames.PowerTimeoutPolicyCol)]
    public PowerTimeoutPolicy[]? PowerTimeoutPolicyCol { get; set; }

    /// <summary>
    /// <c>printer-creation-attributes-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.27
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterCreationAttributesSupported, Tag = Tag.Keyword)]
    public PrinterCreationAttribute[]? PrinterCreationAttributesSupported { get; set; }

    /// <summary>
    /// <c>printer-service-type-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.28
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterServiceTypeSupported, Tag = Tag.Keyword)]
    public PrinterServiceType[]? PrinterServiceTypeSupported { get; set; }

    /// <summary>
    /// <c>resource-format-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.29
    /// </summary>
    [IppAttribute(IppAttributeNames.ResourceFormatSupported, Tag = Tag.MimeMediaType)]
    public ResourceFormat[]? ResourceFormatSupported { get; set; }

    /// <summary>
    /// <c>resource-type-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.30
    /// </summary>
    [IppAttribute(IppAttributeNames.ResourceTypeSupported, Tag = Tag.Keyword)]
    public ResourceType[]? ResourceTypeSupported { get; set; }

    /// <summary>
    /// <c>resource-settable-attributes-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.31
    /// </summary>
    [IppAttribute(IppAttributeNames.ResourceSettableAttributesSupported, Tag = Tag.Keyword)]
    public ResourceSettableAttribute[]? ResourceSettableAttributesSupported { get; set; }

    /// <summary>
    /// <c>system-strings-languages-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.32
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemStringsLanguagesSupported, Tag = Tag.Keyword)]
    public string[]? SystemStringsLanguagesSupported { get; set; }

    /// <summary>
    /// <c>system-strings-uri</c>
    /// See: PWG 5100.22-2025 Section 7.3.33
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemStringsUri, Tag = Tag.Uri)]
    public string[]? SystemStringsUri { get; set; }

    /// <summary>
    /// <c>system-serial-number</c>
    /// See: PWG 5100.22-2025 Section 7.3.34
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemSerialNumber, Tag = Tag.TextWithoutLanguage)]
    public string? SystemSerialNumber { get; set; }

    /// <summary>
    /// <c>system-impressions-completed</c>
    /// See: PWG 5100.22-2025 Section 7.3.35
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemImpressionsCompleted, Tag = Tag.Integer)]
    public int? SystemImpressionsCompleted { get; set; }

    /// <summary>
    /// <c>system-impressions-completed-col</c>
    /// See: PWG 5100.22-2025 Section 7.3.36
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemImpressionsCompletedCol, Tag = Tag.Integer)]
    public int? SystemImpressionsCompletedCol { get; set; }

    /// <summary>
    /// <c>system-media-sheets-completed</c>
    /// See: PWG 5100.22-2025 Section 7.3.37
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemMediaSheetsCompleted, Tag = Tag.Integer)]
    public int? SystemMediaSheetsCompleted { get; set; }

    /// <summary>
    /// <c>system-media-sheets-completed-col</c>
    /// See: PWG 5100.22-2025 Section 7.3.38
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemMediaSheetsCompletedCol, Tag = Tag.Integer)]
    public int? SystemMediaSheetsCompletedCol { get; set; }

    /// <summary>
    /// <c>system-pages-completed</c>
    /// See: PWG 5100.22-2025 Section 7.3.39
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemPagesCompleted, Tag = Tag.Integer)]
    public int? SystemPagesCompleted { get; set; }

    /// <summary>
    /// <c>system-pages-completed-col</c>
    /// See: PWG 5100.22-2025 Section 7.3.40
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemPagesCompletedCol, Tag = Tag.Integer)]
    public int? SystemPagesCompletedCol { get; set; }

    /// <summary>
    /// <c>system-config-change-time</c>
    /// See: PWG 5100.22-2025 Section 7.3.41
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemConfigChangeTime, Tag = Tag.Integer)]
    public int? SystemConfigChangeTime { get; set; }

    /// <summary>
    /// <c>system-config-change-date-time</c>
    /// See: PWG 5100.22-2025 Section 7.3.42
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemConfigChangeDateTime, Tag = Tag.DateTime)]
    public DateTimeOffset? SystemConfigChangeDateTime { get; set; }

    /// <summary>
    /// <c>system-up-time</c>
    /// Type: integer(1:MAX)
    /// See: PWG 5100.22-2025 Section 7.3.43
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemUpTime, Tag = Tag.Integer)]
    public int? SystemUpTime { get; set; }

    /// <summary>
    /// <c>system-uuid</c>
    /// See: PWG 5100.22-2025 Section 7.3.44
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemUuid, Tag = Tag.Uri)]
    public Uri? SystemUuid { get; set; }

    /// <summary>
    /// <c>system-geo-location</c>
    /// See: PWG 5100.22-2025 Section 7.3.45
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemGeoLocation, Tag = Tag.Uri)]
    public Uri? SystemGeoLocation { get; set; }

    /// <summary>
    /// <c>system-asset-tag</c>
    /// See: PWG 5100.22-2025 Section 7.3.46
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemAssetTag, Tag = Tag.OctetStringWithAnUnspecifiedFormat)]
    public OctetString? SystemAssetTag { get; set; }

    /// <summary>
    /// <c>system-current-time</c>
    /// See: PWG 5100.22-2025 Section 7.3.47
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemCurrentTime, Tag = Tag.DateTime)]
    public DateTimeOffset? SystemCurrentTime { get; set; }

    /// <summary>
    /// <c>system-contact-col</c>
    /// See: PWG 5100.22-2025 Section 7.3.48
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemContactCol)]
    public SystemContact[]? SystemContactCol { get; set; }

    /// <summary>
    /// <c>system-service-contact-col</c>
    /// See: PWG 5100.22-2025 Section 7.3.49
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemServiceContactCol)]
    public SystemContact[]? SystemServiceContactCol { get; set; }

    /// <summary>
    /// <c>system-xri-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.50
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemXriSupported)]
    public SystemXri[]? SystemXriSupported { get; set; }

    /// <summary>
    /// <c>system-info</c>
    /// See: PWG 5100.22-2025 Section 7.3.51
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemInfo, Tag = Tag.TextWithoutLanguage)]
    public string? SystemInfo { get; set; }

    /// <summary>
    /// <c>system-location</c>
    /// See: PWG 5100.22-2025 Section 7.3.52
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemLocation, Tag = Tag.TextWithoutLanguage)]
    public string? SystemLocation { get; set; }

    /// <summary>
    /// <c>system-make-and-model</c>
    /// See: PWG 5100.22-2025 Section 7.3.53
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemMakeAndModel, Tag = Tag.TextWithoutLanguage)]
    public string? SystemMakeAndModel { get; set; }

    /// <summary>
    /// <c>system-message-from-operator</c>
    /// See: PWG 5100.22-2025 Section 7.3.54
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemMessageFromOperator, Tag = Tag.TextWithoutLanguage)]
    public string? SystemMessageFromOperator { get; set; }

    /// <summary>
    /// <c>system-name</c>
    /// See: PWG 5100.22-2025 Section 7.3.55
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemName, Tag = Tag.NameWithoutLanguage)]
    public string? SystemName { get; set; }

    /// <summary>
    /// <c>system-default-printer-id</c>
    /// Type: integer(1:65535)
    /// See: PWG 5100.22-2025 Section 7.3.56
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemDefaultPrinterId, Tag = Tag.Integer)]
    public int? SystemDefaultPrinterId { get; set; }

    /// <summary>
    /// <c>system-dns-sd-name</c>
    /// See: PWG 5100.22-2025 Section 7.3.57
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemDnsSdName, Tag = Tag.NameWithoutLanguage)]
    public string? SystemDnsSdName { get; set; }

    /// <summary>
    /// <c>system-mandatory-printer-attributes</c>
    /// See: PWG 5100.22-2025 Section 7.3.58
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemMandatoryPrinterAttributes, Tag = Tag.Keyword)]
    public SystemMandatoryPrinterAttribute[]? SystemMandatoryPrinterAttributes { get; set; }

    /// <summary>
    /// <c>system-mandatory-registration-attributes</c>
    /// See: PWG 5100.22-2025 Section 7.3.59
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemMandatoryRegistrationAttributes, Tag = Tag.Keyword)]
    public SystemMandatoryRegistrationAttribute[]? SystemMandatoryRegistrationAttributes { get; set; }

    /// <summary>
    /// <c>system-settable-attributes-supported</c>
    /// See: PWG 5100.22-2025 Section 7.3.60
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemSettableAttributesSupported, Tag = Tag.Keyword)]
    public SystemSettableAttribute[]? SystemSettableAttributesSupported { get; set; }

    /// <summary>
    /// <c>system-firmware-name</c>
    /// See: PWG 5100.22-2025 Section 7.3.61
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemFirmwareName, Tag = Tag.NameWithoutLanguage)]
    public string[]? SystemFirmwareName { get; set; }

    /// <summary>
    /// <c>system-firmware-patches</c>
    /// See: PWG 5100.22-2025 Section 7.3.62
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemFirmwarePatches, Tag = Tag.TextWithoutLanguage)]
    public string[]? SystemFirmwarePatches { get; set; }

    /// <summary>
    /// <c>system-firmware-string-version</c>
    /// See: PWG 5100.22-2025 Section 7.3.63
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemFirmwareStringVersion, Tag = Tag.TextWithoutLanguage)]
    public string[]? SystemFirmwareStringVersion { get; set; }

    /// <summary>
    /// <c>system-firmware-version</c>
    /// See: PWG 5100.22-2025 Section 7.3.64
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemFirmwareVersion, Tag = Tag.OctetStringWithAnUnspecifiedFormat)]
    public OctetString[]? SystemFirmwareVersion { get; set; }

    /// <summary>
    /// <c>system-resident-application-name</c>
    /// See: PWG 5100.22-2025 Section 7.3.65
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemResidentApplicationName, Tag = Tag.NameWithoutLanguage)]
    public string[]? SystemResidentApplicationName { get; set; }

    /// <summary>
    /// <c>system-resident-application-patches</c>
    /// See: PWG 5100.22-2025 Section 7.3.66
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemResidentApplicationPatches, Tag = Tag.TextWithoutLanguage)]
    public string[]? SystemResidentApplicationPatches { get; set; }

    /// <summary>
    /// <c>system-resident-application-string-version</c>
    /// See: PWG 5100.22-2025 Section 7.3.67
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemResidentApplicationStringVersion, Tag = Tag.TextWithoutLanguage)]
    public string[]? SystemResidentApplicationStringVersion { get; set; }

    /// <summary>
    /// <c>system-resident-application-version</c>
    /// See: PWG 5100.22-2025 Section 7.3.68
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemResidentApplicationVersion, Tag = Tag.OctetStringWithAnUnspecifiedFormat)]
    public OctetString[]? SystemResidentApplicationVersion { get; set; }

    /// <summary>
    /// <c>system-user-application-name</c>
    /// See: PWG 5100.22-2025 Section 7.3.69
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemUserApplicationName, Tag = Tag.NameWithoutLanguage)]
    public string[]? SystemUserApplicationName { get; set; }

    /// <summary>
    /// <c>system-user-application-patches</c>
    /// See: PWG 5100.22-2025 Section 7.3.70
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemUserApplicationPatches, Tag = Tag.TextWithoutLanguage)]
    public string[]? SystemUserApplicationPatches { get; set; }

    /// <summary>
    /// <c>system-user-application-string-version</c>
    /// See: PWG 5100.22-2025 Section 7.3.71
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemUserApplicationStringVersion, Tag = Tag.TextWithoutLanguage)]
    public string[]? SystemUserApplicationStringVersion { get; set; }

    /// <summary>
    /// <c>system-user-application-version</c>
    /// See: PWG 5100.22-2025 Section 7.3.72
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemUserApplicationVersion, Tag = Tag.OctetStringWithAnUnspecifiedFormat)]
    public OctetString[]? SystemUserApplicationVersion { get; set; }

    /// <summary>
    /// <c>system-time-source-configured</c>
    /// See: PWG 5100.22-2025 Section 7.3.73
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemTimeSourceConfigured, Tag = Tag.Keyword)]
    public SystemTimeSourceConfigured? SystemTimeSourceConfigured { get; set; }

    /// <summary>
    /// The current state of the System object (idle=3, processing=4, stopped=5).
    /// See: PWG 5100.22-2025 Section 7.3.26
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemState, Tag = Tag.Enum)]
    public PrinterState? SystemState { get; set; }

    /// <summary>
    /// One or more reasons for the current system state.
    /// See: PWG 5100.22-2025 Section 7.3.30
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemStateReasons, Tag = Tag.Keyword)]
    public SystemStateReason[]? SystemStateReasons { get; set; }

    /// <summary>
    /// Human-readable message describing the current system state.
    /// See: PWG 5100.22-2025 Section 7.3.29
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemStateMessage, Tag = Tag.TextWithoutLanguage)]
    public string? SystemStateMessage { get; set; }

    /// <summary>
    /// Time in seconds since system boot when the system state last changed.
    /// See: PWG 5100.22-2025 Section 7.3.28
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemStateChangeTime, Tag = Tag.Integer)]
    public int? SystemStateChangeTime { get; set; }

    /// <summary>
    /// Date and time when the system state last changed.
    /// See: PWG 5100.22-2025 Section 7.3.27
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemStateChangeDateTime, Tag = Tag.DateTime)]
    public DateTimeOffset? SystemStateChangeDateTime { get; set; }
}
