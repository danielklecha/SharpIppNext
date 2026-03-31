namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// PWG 5100.22-2025 - IPP System Service attribute name constants.
    /// </summary>
    public static class SystemAttribute
    {
        /// <summary>
        /// The IPP client event lifetime in seconds.
        /// See: PWG 5100.22-2025 Section 7.1.25
        /// </summary>
        public const string IppGetEventLife = "ippget-event-life";

        /// <summary>
        /// The URI of the target System object.
        /// See: PWG 5100.22-2025 Section 7.1.26
        /// </summary>
        public const string SystemUri = "system-uri";

        /// <summary>
        /// The notified system up time.
        /// See: PWG 5100.22-2025 Section 7.1.
        /// </summary>
        public const string NotifySystemUpTime = "notify-system-up-time";

        /// <summary>
        /// The notified system URI.
        /// See: PWG 5100.22-2025 Section 7.1.
        /// </summary>
        public const string NotifySystemUri = "notify-system-uri";

        /// <summary>
        /// The current state of the System object.
        /// See: PWG 5100.22-2025 Section 7.3.26
        /// </summary>
        public const string SystemState = "system-state";

        /// <summary>
        /// The number of times the System configuration has changed.
        /// See: PWG 5100.22-2025 Section 7.3.2
        /// </summary>
        public const string SystemConfigChanges = "system-config-changes";

        /// <summary>
        /// The number of configured printers.
        /// See: PWG 5100.22-2025 Section 7.3.3
        /// </summary>
        public const string SystemConfiguredPrinters = "system-configured-printers";

        /// <summary>
        /// The number of configured resources.
        /// See: PWG 5100.22-2025 Section 7.3.4
        /// </summary>
        public const string SystemConfiguredResources = "system-configured-resources";

        /// <summary>
        /// The system strings languages supported.
        /// See: PWG 5100.22-2025 Section 7.3.5
        /// </summary>
        public const string SystemStringsLanguagesSupported = "system-strings-languages-supported";

        /// <summary>
        /// URI for system strings.
        /// See: PWG 5100.22-2025 Section 7.3.6
        /// </summary>
        public const string SystemStringsUri = "system-strings-uri";

        /// <summary>
        /// charset-configured
        /// See: PWG 5100.22-2025 Section 7.2.1
        /// </summary>
        public const string CharsetConfigured = "charset-configured";

        /// <summary>
        /// charset-supported
        /// See: PWG 5100.22-2025 Section 7.2.2
        /// </summary>
        public const string CharsetSupported = "charset-supported";

        /// <summary>
        /// document-format-supported
        /// See: PWG 5100.22-2025 Section 7.2.3
        /// </summary>
        public const string DocumentFormatSupported = "document-format-supported";

        /// <summary>
        /// generated-natural-language-supported
        /// See: PWG 5100.22-2025 Section 7.2.9
        /// </summary>
        public const string GeneratedNaturalLanguageSupported = "generated-natural-language-supported";

        /// <summary>
        /// ipp-features-supported
        /// See: PWG 5100.22-2025 Section 7.2.5
        /// </summary>
        public const string IppFeaturesSupported = "ipp-features-supported";

        /// <summary>
        /// ipp-versions-supported
        /// See: PWG 5100.22-2025 Section 7.2.6
        /// </summary>
        public const string IppVersionsSupported = "ipp-versions-supported";

        /// <summary>
        /// multiple-document-printers-supported
        /// See: PWG 5100.22-2025 Section 7.2.7
        /// </summary>
        public const string MultipleDocumentPrintersSupported = "multiple-document-printers-supported";

        /// <summary>
        /// natural-language-configured
        /// See: PWG 5100.22-2025 Section 7.2.8
        /// </summary>
        public const string NaturalLanguageConfigured = "natural-language-configured";

        /// <summary>
        /// operations-supported
        /// See: PWG 5100.22-2025 Section 7.2.18
        /// </summary>
        public const string OperationsSupported = "operations-supported";

        /// <summary>
        /// output-device-x509-type-supported
        /// See: PWG 5100.22-2025 Section 7.2.19
        /// </summary>
        public const string OutputDeviceX509TypeSupported = "output-device-x509-type-supported";

        /// <summary>
        /// power-calendar-policy-col
        /// See: PWG 5100.22-2025 Section 7.2.20
        /// </summary>
        public const string PowerCalendarPolicyCol = "power-calendar-policy-col";

        /// <summary>
        /// power-event-policy-col
        /// See: PWG 5100.22-2025 Section 7.2.21
        /// </summary>
        public const string PowerEventPolicyCol = "power-event-policy-col";

        /// <summary>
        /// power-timeout-policy-col
        /// See: PWG 5100.22-2025 Section 7.2.22
        /// </summary>
        public const string PowerTimeoutPolicyCol = "power-timeout-policy-col";

        /// <summary>
        /// printer-creation-attributes-supported
        /// See: PWG 5100.22-2025 Section 7.2.23
        /// </summary>
        public const string PrinterCreationAttributesSupported = "printer-creation-attributes-supported";

        /// <summary>
        /// printer-service-type-supported
        /// See: PWG 5100.22-2025 Section 7.2.24
        /// </summary>
        public const string PrinterServiceTypeSupported = "printer-service-type-supported";

        /// <summary>
        /// resource-format-supported
        /// See: PWG 5100.22-2025 Section 7.2.25
        /// </summary>
        public const string ResourceFormatSupported = "resource-format-supported";

        /// <summary>
        /// resource-types (for write operations)
        /// See: PWG 5100.22-2025 Section 7.1.20
        /// </summary>
        public const string ResourceStates = "resource-states";

        /// <summary>
        /// resource-type-supported
        /// See: PWG 5100.22-2025 Section 7.2.26
        /// </summary>
        public const string ResourceTypeSupported = "resource-type-supported";

        /// <summary>
        /// resource-settable-attributes-supported
        /// See: PWG 5100.22-2025 Section 7.2.27
        /// </summary>
        public const string ResourceSettableAttributesSupported = "resource-settable-attributes-supported";

        /// <summary>
        /// System serial number.
        /// See: PWG 5100.22-2025 Section 7.3.19
        /// </summary>
        public const string SystemSerialNumber = "system-serial-number";

        /// <summary>
        /// Total system impressions completed.
        /// See: PWG 5100.22-2025 Section 7.3.20
        /// </summary>
        public const string SystemImpressionsCompleted = "system-impressions-completed";

        /// <summary>
        /// Total system impressions completed (colon
        /// and corresponding color detail),
        /// See: PWG 5100.22-2025 Section 7.3.21
        /// </summary>
        public const string SystemImpressionsCompletedCol = "system-impressions-completed-col";

        /// <summary>
        /// Total system media sheets completed.
        /// See: PWG 5100.22-2025 Section 7.3.22
        /// </summary>
        public const string SystemMediaSheetsCompleted = "system-media-sheets-completed";

        /// <summary>
        /// Total system media sheets completed color variants.
        /// See: PWG 5100.22-2025 Section 7.3.23
        /// </summary>
        public const string SystemMediaSheetsCompletedCol = "system-media-sheets-completed-col";

        /// <summary>
        /// Total system pages completed.
        /// See: PWG 5100.22-2025 Section 7.3.24
        /// </summary>
        public const string SystemPagesCompleted = "system-pages-completed";

        /// <summary>
        /// Total system pages completed (color variants).
        /// See: PWG 5100.22-2025 Section 7.3.25
        /// </summary>
        public const string SystemPagesCompletedCol = "system-pages-completed-col";

        /// <summary>
        /// The unique identifier for a resource (singular).
        /// See: PWG 5100.22-2025 Section 7.9.6
        /// </summary>
        public const string ResourceId = "resource-id";

        /// <summary>
        /// The UUID for a resource.
        /// See: PWG 5100.22-2025 Section 7.9.13
        /// </summary>
        public const string ResourceUuid = "resource-uuid";

        /// <summary>
        /// The list of resource identifiers (operation attribute).
        /// See: PWG 5100.22-2025 Section 7.1.15
        /// </summary>
        public const string ResourceIds = "resource-ids";

        /// <summary>
        /// System firmware names
        /// See: PWG 5100.22-2025 Section 7.3.11
        /// </summary>
        public const string SystemFirmwareName = "system-firmware-name";

        /// <summary>
        /// System firmware patches
        /// See: PWG 5100.22-2025 Section 7.3.12
        /// </summary>
        public const string SystemFirmwarePatches = "system-firmware-patches";

        /// <summary>
        /// System firmware string version
        /// See: PWG 5100.22-2025 Section 7.3.13
        /// </summary>
        public const string SystemFirmwareStringVersion = "system-firmware-string-version";

        /// <summary>
        /// System firmware version
        /// See: PWG 5100.22-2025 Section 7.3.14
        /// </summary>
        public const string SystemFirmwareVersion = "system-firmware-version";

        /// <summary>
        /// System state change date-time.
        /// See: PWG 5100.22-2025 Section 7.3.27
        /// </summary>
        public const string SystemStateChangeDateTime = "system-state-change-date-time";

        /// <summary>
        /// System config change date-time.
        /// See: PWG 5100.22-2025 Section 7.3.8
        /// </summary>
        public const string SystemConfigChangeDateTime = "system-config-change-date-time";

        /// <summary>
        /// System config change time.
        /// See: PWG 5100.22-2025 Section 7.3.9
        /// </summary>
        public const string SystemConfigChangeTime = "system-config-change-time";

        /// <summary>
        /// System state change time.
        /// See: PWG 5100.22-2025 Section 7.3.28
        /// </summary>
        public const string SystemStateChangeTime = "system-state-change-time";

        /// <summary>
        /// System state message.
        /// See: PWG 5100.22-2025 Section 7.3.29
        /// </summary>
        public const string SystemStateMessage = "system-state-message";

        /// <summary>
        /// System state reasons.
        /// See: PWG 5100.22-2025 Section 7.3.30
        /// </summary>
        public const string SystemStateReasons = "system-state-reasons";

        /// <summary>
        /// Power log collection.
        /// See: PWG 5100.22-2025 Section 7.3.1
        /// </summary>
        public const string PowerLogCol = "power-log-col";

        /// <summary>
        /// Power state capabilities collection.
        /// See: PWG 5100.22-2025 Section 7.3.2
        /// </summary>
        public const string PowerStateCapabilitiesCol = "power-state-capabilities-col";

        /// <summary>
        /// Power state counters collection.
        /// See: PWG 5100.22-2025 Section 7.3.3
        /// </summary>
        public const string PowerStateCountersCol = "power-state-counters-col";

        /// <summary>
        /// Power state monitor collection.
        /// See: PWG 5100.22-2025 Section 7.3.4
        /// </summary>
        public const string PowerStateMonitorCol = "power-state-monitor-col";

        /// <summary>
        /// Power state transitions collection.
        /// See: PWG 5100.22-2025 Section 7.3.5
        /// </summary>
        public const string PowerStateTransitionsCol = "power-state-transitions-col";

        /// <summary>
        /// System up time.
        /// See: PWG 5100.22-2025 Section 7.3.31
        /// </summary>
        public const string SystemUpTime = "system-up-time";

        /// <summary>
        /// System UUID.
        /// See: PWG 5100.22-2025 Section 7.3.32
        /// </summary>
        public const string SystemUuid = "system-uuid";

        /// <summary>
        /// System geo location
        /// See: PWG 5100.22-2025 Section 7.2.33
        /// </summary>
        public const string SystemGeoLocation = "system-geo-location";

        /// <summary>
        /// System asset tag
        /// See: PWG 5100.22-2025 Section 7.2.28
        /// </summary>
        public const string SystemAssetTag = "system-asset-tag";

        /// <summary>
        /// System current time
        /// See: PWG 5100.22-2025 Section 7.2.30
        /// </summary>
        public const string SystemCurrentTime = "system-current-time";

        /// <summary>
        /// System info
        /// See: PWG 5100.22-2025 Section 7.2.34
        /// </summary>
        public const string SystemInfo = "system-info";

        /// <summary>
        /// System location
        /// See: PWG 5100.22-2025 Section 7.2.35
        /// </summary>
        public const string SystemLocation = "system-location";

        /// <summary>
        /// System make and model
        /// See: PWG 5100.22-2025 Section 7.2.38
        /// </summary>
        public const string SystemMakeAndModel = "system-make-and-model";

        /// <summary>
        /// System message from operator
        /// See: PWG 5100.22-2025 Section 7.2.39
        /// </summary>
        public const string SystemMessageFromOperator = "system-message-from-operator";

        /// <summary>
        /// System name
        /// See: PWG 5100.22-2025 Section 7.2.40
        /// </summary>
        public const string SystemName = "system-name";

        /// <summary>
        /// System contact list
        /// See: PWG 5100.22-2025 Section 7.2.29
        /// </summary>
        public const string SystemContactCol = "system-contact-col";

        /// <summary>
        /// System service contact list
        /// See: PWG 5100.22-2025 Section 7.2.41
        /// </summary>
        public const string SystemServiceContactCol = "system-service-contact-col";

        /// <summary>
        /// System XRI supported
        /// See: PWG 5100.22-2025 Section 7.2.45
        /// </summary>
        public const string SystemXriSupported = "system-xri-supported";

        /// <summary>
        /// XRI authentication supported
        /// See: PWG 5100.22-2025 Section 7.3.38
        /// </summary>
        public const string XriAuthenticationSupported = "xri-authentication-supported";

        /// <summary>
        /// XRI security supported
        /// See: PWG 5100.22-2025 Section 7.3.39
        /// </summary>
        public const string XriSecuritySupported = "xri-security-supported";

        /// <summary>
        /// XRI URI scheme supported
        /// See: PWG 5100.22-2025 Section 7.3.40
        /// </summary>
        public const string XriUriSchemeSupported = "xri-uri-scheme-supported";

        /// <summary>
        /// System default printer id
        /// See: PWG 5100.22-2025 Section 7.2.31
        /// </summary>
        public const string SystemDefaultPrinterId = "system-default-printer-id";

        /// <summary>
        /// System DNS-SD name
        /// See: PWG 5100.22-2025 Section 7.2.32
        /// </summary>
        public const string SystemDnsSdName = "system-dns-sd-name";

        /// <summary>
        /// resource-format-accepted
        /// See: PWG 5100.22-2025 Section 7.1.12
        /// </summary>
        public const string ResourceFormatAccepted = "resource-format-accepted";

        /// <summary>
        /// resource-types
        /// See: PWG 5100.22-2025 Section 7.1.23
        /// </summary>
        public const string ResourceTypes = "resource-types";

        /// <summary>
        /// restart-get-interval
        /// See: PWG 5100.22-2025 Section 7.1.25
        /// </summary>
        public const string RestartGetInterval = "restart-get-interval";

        /// <summary>
        /// which-printers
        /// See: PWG 5100.22-2025 Section 7.1.27
        /// </summary>
        public const string WhichPrinters = "which-printers";

        /// <summary>
        /// notify-printer-ids
        /// See: PWG 5100.22-2025 Section 7.1.1
        /// </summary>
        public const string NotifyPrinterIds = "notify-printer-ids";

        /// <summary>
        /// notify-resource-id
        /// See: PWG 5100.22-2025 Section 7.1.2
        /// </summary>
        public const string NotifyResourceId = "notify-resource-id";

        /// <summary>
        /// notify-attributes-supported
        /// See: PWG 5100.22-2025 Section 7.2.10
        /// </summary>
        public const string NotifyAttributesSupported = "notify-attributes-supported";

        /// <summary>
        /// notify-events-default
        /// See: PWG 5100.22-2025 Section 7.2.11
        /// </summary>
        public const string NotifyEventsDefault = "notify-events-default";

        /// <summary>
        /// notify-events-supported
        /// See: PWG 5100.22-2025 Section 7.2.12
        /// </summary>
        public const string NotifyEventsSupported = "notify-events-supported";

        /// <summary>
        /// notify-lease-duration-default
        /// See: PWG 5100.22-2025 Section 7.2.13
        /// </summary>
        public const string NotifyLeaseDurationDefault = "notify-lease-duration-default";

        /// <summary>
        /// notify-lease-duration-supported
        /// See: PWG 5100.22-2025 Section 7.2.14
        /// </summary>
        public const string NotifyLeaseDurationSupported = "notify-lease-duration-supported";

        /// <summary>
        /// notify-max-events-supported
        /// See: PWG 5100.22-2025 Section 7.2.15
        /// </summary>
        public const string NotifyMaxEventsSupported = "notify-max-events-supported";

        /// <summary>
        /// notify-pull-method-supported
        /// See: PWG 5100.22-2025 Section 7.2.16
        /// </summary>
        public const string NotifyPullMethodSupported = "notify-pull-method-supported";

        /// <summary>
        /// notify-schemes-supported
        /// See: PWG 5100.22-2025 Section 7.2.17
        /// </summary>
        public const string NotifySchemesSupported = "notify-schemes-supported";

        /// <summary>
        /// System mandatory printer attributes
        /// See: PWG 5100.22-2025 Section 7.2.36
        /// </summary>
        public const string SystemMandatoryPrinterAttributes = "system-mandatory-printer-attributes";

        /// <summary>
        /// System mandatory registration attributes
        /// See: PWG 5100.22-2025 Section 7.2.37
        /// </summary>
        public const string SystemMandatoryRegistrationAttributes = "system-mandatory-registration-attributes";

        /// <summary>
        /// System settable attributes supported
        /// See: PWG 5100.22-2025 Section 7.2.42
        /// </summary>
        public const string SystemSettableAttributesSupported = "system-settable-attributes-supported";

        /// <summary>
        /// System time source configured
        /// See: PWG 5100.22-2025 Section 7.3.26
        /// </summary>
        public const string SystemTimeSourceConfigured = "system-time-source-configured";

        public const string SystemResidentApplicationName = "system-resident-application-name";
        public const string SystemResidentApplicationPatches = "system-resident-application-patches";
        public const string SystemResidentApplicationStringVersion = "system-resident-application-string-version";
        public const string SystemResidentApplicationVersion = "system-resident-application-version";

        public const string SystemUserApplicationName = "system-user-application-name";
        public const string SystemUserApplicationPatches = "system-user-application-patches";
        public const string SystemUserApplicationStringVersion = "system-user-application-string-version";
        public const string SystemUserApplicationVersion = "system-user-application-version";

        /// <summary>
        /// Resource format (MIME media type).
        /// See: PWG 5100.22-2025 Section 7.9.1
        /// </summary>
        public const string ResourceFormat = "resource-format";

        /// <summary>
        /// Resource formats supported
        /// See: PWG 5100.22-2025 Section 7.9.2
        /// </summary>
        public const string ResourceFormats = "resource-formats";

        /// <summary>
        /// Resource name
        /// See: PWG 5100.22-2025 Section 7.9.5
        /// </summary>
        public const string ResourceName = "resource-name";

        /// <summary>
        /// Resource info
        /// See: PWG 5100.22-2025 Section 7.9.4
        /// </summary>
        public const string ResourceInfo = "resource-info";

        /// <summary>
        /// Resource state
        /// See: PWG 5100.22-2025 Section 7.5.1
        /// </summary>
        public const string ResourceState = "resource-state";

        /// <summary>
        /// Resource state reasons
        /// See: PWG 5100.22-2025 Section 7.5.2
        /// </summary>
        public const string ResourceStateReasons = "resource-state-reasons";

        /// <summary>
        /// Resource date-time at canceled
        /// See: PWG 5100.22-2025 Section 7.9.1
        /// </summary>
        public const string ResourceDateTimeAtCanceled = "date-time-at-canceled";

        /// <summary>
        /// Resource date-time at creation
        /// See: PWG 5100.22-2025 Section 7.9.2
        /// </summary>
        public const string ResourceDateTimeAtCreation = "date-time-at-creation";

        /// <summary>
        /// Resource date-time at installed
        /// See: PWG 5100.22-2025 Section 7.9.3
        /// </summary>
        public const string ResourceDateTimeAtInstalled = "date-time-at-installed";

        /// <summary>
        /// Resource time at canceled
        /// See: PWG 5100.22-2025 Section 7.9.1
        /// </summary>
        public const string ResourceTimeAtCanceled = "time-at-canceled";

        /// <summary>
        /// Resource time at creation
        /// See: PWG 5100.22-2025 Section 7.9.2
        /// </summary>
        public const string ResourceTimeAtCreation = "time-at-creation";

        /// <summary>
        /// Resource time at installed
        /// See: PWG 5100.22-2025 Section 7.9.3
        /// </summary>
        public const string ResourceTimeAtInstalled = "time-at-installed";

        /// <summary>
        /// Resource natural language
        /// See: PWG 5100.22-2025 Section 7.9.8
        /// </summary>
        public const string ResourceNaturalLanguage = "resource-natural-language";

        /// <summary>
        /// Resource patches
        /// See: PWG 5100.22-2025 Section 7.9.9
        /// </summary>
        public const string ResourcePatches = "resource-patches";

        /// <summary>
        /// Resource signature
        /// See: PWG 5100.22-2025 Section 7.9.10
        /// </summary>
        public const string ResourceSignature = "resource-signature";

        /// <summary>
        /// Resource string version
        /// See: PWG 5100.22-2025 Section 7.9.14
        /// </summary>
        public const string ResourceStringVersion = "resource-string-version";

        /// <summary>
        /// Resource type
        /// See: PWG 5100.22-2025 Section 7.9.11
        /// </summary>
        public const string ResourceType = "resource-type";

        /// <summary>
        /// Resource version
        /// See: PWG 5100.22-2025 Section 7.9.10
        /// </summary>
        public const string ResourceVersion = "resource-version";

        /// <summary>
        /// Resource state message
        /// See: PWG 5100.22-2025 Section 7.9.12
        /// </summary>
        public const string ResourceStateMessage = "resource-state-message";

        /// <summary>
        /// Resource data URI
        /// See: PWG 5100.22-2025 Section 7.9.4
        /// </summary>
        public const string ResourceDataUri = "resource-data-uri";

        /// <summary>
        /// Resource size in kilobytes
        /// See: PWG 5100.22-2025 Section 7.9.7
        /// </summary>
        public const string ResourceKOctets = "resource-k-octets";

        /// <summary>
        /// Resource use count
        /// See: PWG 5100.22-2025 Section 7.9.16
        /// </summary>
        public const string ResourceUseCount = "resource-use-count";
    }
}
