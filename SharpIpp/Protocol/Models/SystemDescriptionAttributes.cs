using System;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Description attributes for a System object.
/// See: PWG 5100.22-2025 Section 7.3
/// </summary>
public class SystemDescriptionAttributes
{
    public int? SystemConfigChanges { get; set; }
    public int? SystemConfiguredPrinters { get; set; }
    public int? SystemConfiguredResources { get; set; }

    public string? CharsetConfigured { get; set; }
    public string[]? CharsetSupported { get; set; }
    public string[]? DocumentFormatSupported { get; set; }
    public string[]? GeneratedNaturalLanguageSupported { get; set; }
    public string[]? IppFeaturesSupported { get; set; }
    public IppVersion[]? IppVersionsSupported { get; set; }

    /// <summary>
    /// The IPP Get event life in seconds.
    /// See: PWG 5100.22-2025 Section 7.1.25
    /// </summary>
    public int? IppGetEventLife { get; set; }

    public bool? MultipleDocumentPrintersSupported { get; set; }

    public string[]? NotifyAttributesSupported { get; set; }
    public string[]? NotifyEventsDefault { get; set; }
    public string[]? NotifyEventsSupported { get; set; }
    public int? NotifyLeaseDurationDefault { get; set; }
    public string[]? NotifyLeaseDurationSupported { get; set; }
    public int? NotifyMaxEventsSupported { get; set; }
    public string[]? NotifyPullMethodSupported { get; set; }
    public UriScheme[]? NotifySchemesSupported { get; set; }
    public string? NaturalLanguageConfigured { get; set; }
    public IppOperation[]? OperationsSupported { get; set; }
    public string[]? OutputDeviceX509TypeSupported { get; set; }

    public PowerCalendarPolicy[]? PowerCalendarPolicyCol { get; set; }
    public PowerEventPolicy[]? PowerEventPolicyCol { get; set; }
    public PowerTimeoutPolicy[]? PowerTimeoutPolicyCol { get; set; }

    public string[]? PrinterCreationAttributesSupported { get; set; }
    public PrinterServiceType[]? PrinterServiceTypeSupported { get; set; }
    public string[]? ResourceFormatSupported { get; set; }
    public string[]? ResourceTypeSupported { get; set; }
    public string[]? ResourceSettableAttributesSupported { get; set; }

    public string[]? SystemStringsLanguagesSupported { get; set; }
    public string[]? SystemStringsUri { get; set; }
    public string? SystemSerialNumber { get; set; }
    public int? SystemImpressionsCompleted { get; set; }
    public int? SystemImpressionsCompletedCol { get; set; }
    public int? SystemMediaSheetsCompleted { get; set; }
    public int? SystemMediaSheetsCompletedCol { get; set; }
    public int? SystemPagesCompleted { get; set; }
    public int? SystemPagesCompletedCol { get; set; }
    public int? SystemConfigChangeTime { get; set; }
    public DateTimeOffset? SystemConfigChangeDateTime { get; set; }
    public int? SystemUpTime { get; set; }
    public Uri? SystemUuid { get; set; }

    public Uri? SystemGeoLocation { get; set; }
    public byte[]? SystemAssetTag { get; set; }
    public DateTimeOffset? SystemCurrentTime { get; set; }
    public SystemContact[]? SystemContactCol { get; set; }
    public SystemContact[]? SystemServiceContactCol { get; set; }
    public SystemXri[]? SystemXriSupported { get; set; }
    public string? SystemInfo { get; set; }
    public string? SystemLocation { get; set; }
    public string? SystemMakeAndModel { get; set; }
    public string? SystemMessageFromOperator { get; set; }
    public string? SystemName { get; set; }
    public int? SystemDefaultPrinterId { get; set; }
    public string? SystemDnsSdName { get; set; }
    public string[]? SystemMandatoryPrinterAttributes { get; set; }
    public string[]? SystemMandatoryRegistrationAttributes { get; set; }
    public string[]? SystemSettableAttributesSupported { get; set; }

    public string[]? SystemFirmwareName { get; set; }
    public string[]? SystemFirmwarePatches { get; set; }
    public string[]? SystemFirmwareStringVersion { get; set; }
    public string[]? SystemFirmwareVersion { get; set; }
    public string[]? SystemResidentApplicationName { get; set; }
    public string[]? SystemResidentApplicationPatches { get; set; }
    public string[]? SystemResidentApplicationStringVersion { get; set; }
    public string[]? SystemResidentApplicationVersion { get; set; }
    public string[]? SystemUserApplicationName { get; set; }
    public string[]? SystemUserApplicationPatches { get; set; }
    public string[]? SystemUserApplicationStringVersion { get; set; }
    public string[]? SystemUserApplicationVersion { get; set; }

    public string? SystemTimeSourceConfigured { get; set; }
}