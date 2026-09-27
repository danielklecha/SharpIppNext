using SharpIpp.Mapping;
using System;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Attributes describing the state and status of a System object.
/// See: PWG 5100.22-2025 Section 7.3
/// </summary>
[IppAttribute]
[IppSection(SectionTag.SystemAttributesTag)]
public class SystemStatusAttributes
{
    /// <summary>
    /// The current state of the System object. Uses the same values as <c>printer-state</c>
    /// (3 = idle, 4 = processing, 5 = stopped).
    /// See: PWG 5100.22-2025 Section 7.3.26
    /// </summary>
    /// <code>system-state</code>
    [IppAttribute(IppAttributeNames.SystemState)]
    public IppValue<PrinterState>? SystemState { get; set; }

    /// <summary>
    /// Human readable system state message.
    /// See: PWG 5100.22-2025 Section 7.3.29
    /// </summary>
    /// <code>system-state-message</code>
    [IppAttribute(IppAttributeNames.SystemStateMessage)]
    public IppValue<string>? SystemStateMessage { get; set; }

    /// <summary>
    /// One or more state reasons for the System object.
    /// See: PWG 5100.22-2025 Section 7.3.30
    /// </summary>
    /// <code>system-state-reasons</code>
    [IppAttribute(IppAttributeNames.SystemStateReasons)]
    public IppValue<SystemStateReason[]>? SystemStateReasons { get; set; }

    /// <summary>
    /// System state change time since boot in seconds.
    /// See: PWG 5100.22-2025 Section 7.3.28
    /// </summary>
    /// <code>system-state-change-time</code>
    [IppAttribute(IppAttributeNames.SystemStateChangeTime)]
    public IppValue<int>? SystemStateChangeTime { get; set; }

    /// <summary>
    /// System state change date-time.
    /// See: PWG 5100.22-2025 Section 7.3.27
    /// </summary>
    /// <code>system-state-change-date-time</code>
    [IppAttribute(IppAttributeNames.SystemStateChangeDateTime)]
    public IppValue<DateTimeOffset>? SystemStateChangeDateTime { get; set; }

    /// <summary>
    /// System uptime in seconds.
    /// Type: integer(1:MAX)
    /// See: PWG 5100.22-2025 Section 7.3.32
    /// </summary>
    /// <code>system-up-time</code>
    [IppAttribute(IppAttributeNames.SystemUpTime)]
    public IppValue<int>? SystemUpTime { get; set; }

    /// <summary>
    /// System time source configured.
    /// See: PWG 5100.22-2025 Section 7.3.31
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemTimeSourceConfigured)]
    public IppValue<SystemTimeSourceConfigured>? SystemTimeSourceConfigured { get; set; }

    /// <summary>
    /// Configured printers summary.
    /// See: PWG 5100.22-2025 Section 7.3.9
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemConfiguredPrinters)]
    public IppValue<SystemConfiguredPrinter[]>? SystemConfiguredPrinters { get; set; }

    /// <summary>
    /// Configured resources summary.
    /// See: PWG 5100.22-2025 Section 7.3.10
    /// </summary>
    [IppAttribute(IppAttributeNames.SystemConfiguredResources)]
    public IppValue<SystemConfiguredResource[]>? SystemConfiguredResources { get; set; }

    /// <summary>
    /// Power log entries.
    /// See: PWG 5100.22-2025 Section 7.3.1
    /// </summary>
    [IppAttribute(IppAttributeNames.PowerLogCol)]
    public IppValue<PowerLogEntry[]>? PowerLogCol { get; set; }

    /// <summary>
    /// Power state capabilities collection.
    /// See: PWG 5100.22-2025 Section 7.3.2
    /// </summary>
    [IppAttribute(IppAttributeNames.PowerStateCapabilitiesCol)]
    public IppValue<PowerStateCapability[]>? PowerStateCapabilitiesCol { get; set; }

    /// <summary>
    /// Power state counters collection.
    /// See: PWG 5100.22-2025 Section 7.3.3
    /// </summary>
    [IppAttribute(IppAttributeNames.PowerStateCountersCol)]
    public IppValue<PowerStateCounter[]>? PowerStateCountersCol { get; set; }

    /// <summary>
    /// Power state monitor collection.
    /// See: PWG 5100.22-2025 Section 7.3.4
    /// </summary>
    [IppAttribute(IppAttributeNames.PowerStateMonitorCol)]
    public IppValue<PowerStateMonitor[]>? PowerStateMonitorCol { get; set; }

    /// <summary>
    /// Power state transitions collection.
    /// See: PWG 5100.22-2025 Section 7.3.5
    /// </summary>
    [IppAttribute(IppAttributeNames.PowerStateTransitionsCol)]
    public IppValue<PowerStateTransition[]>? PowerStateTransitionsCol { get; set; }

    /// <summary>
    /// System unique identifier.
    /// See: PWG 5100.22-2025 Section 7.3.37
    /// </summary>
    /// <code>system-uuid</code>
    [IppAttribute(IppAttributeNames.SystemUuid)]
    public IppValue<Uri>? SystemUuid { get; set; }

    /// <summary>
    /// Supported XRI authentication methods.
    /// See: PWG 5100.22-2025 Section 7.3.38
    /// </summary>
    [IppAttribute(IppAttributeNames.XriAuthenticationSupported)]
    public IppValue<UriAuthentication[]>? XriAuthenticationSupported { get; set; }

    /// <summary>
    /// Supported XRI security methods.
    /// See: PWG 5100.22-2025 Section 7.3.39
    /// </summary>
    [IppAttribute(IppAttributeNames.XriSecuritySupported)]
    public IppValue<UriSecurity[]>? XriSecuritySupported { get; set; }

    /// <summary>
    /// Supported XRI URI schemes.
    /// See: PWG 5100.22-2025 Section 7.3.40
    /// </summary>
    [IppAttribute(IppAttributeNames.XriUriSchemeSupported)]
    public IppValue<UriScheme[]>? XriUriSchemeSupported { get; set; }
}
