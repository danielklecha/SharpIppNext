using System;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Attributes describing the state and status of a System object.
/// See: PWG 5100.22-2025 Section 7.3
/// </summary>
public class SystemStatusAttributes
{
    /// <summary>
    /// The current state of the System object. Uses the same values as <c>printer-state</c>
    /// (3 = idle, 4 = processing, 5 = stopped).
    /// See: PWG 5100.22-2025 Section 7.3.1
    /// </summary>
    /// <code>system-state</code>
    public PrinterState? SystemState { get; set; }

    /// <summary>
    /// Human readable system state message.
    /// See: PWG 5100.22-2025 Section 7.3.29
    /// </summary>
    /// <code>system-state-message</code>
    public string? SystemStateMessage { get; set; }

    /// <summary>
    /// One or more state reasons for the System object.
    /// See: PWG 5100.22-2025 Section 7.3.30
    /// </summary>
    /// <code>system-state-reasons</code>
    public SystemStateReason[]? SystemStateReasons { get; set; }

    /// <summary>
    /// System state change time since boot in seconds.
    /// See: PWG 5100.22-2025 Section 7.3.28
    /// </summary>
    /// <code>system-state-change-time</code>
    public int? SystemStateChangeTime { get; set; }

    /// <summary>
    /// System state change date-time.
    /// See: PWG 5100.22-2025 Section 7.3.27
    /// </summary>
    /// <code>system-state-change-date-time</code>
    public DateTimeOffset? SystemStateChangeDateTime { get; set; }

    /// <summary>
    /// System uptime in seconds.
    /// See: PWG 5100.22-2025 Section 7.3.31
    /// </summary>
    /// <code>system-up-time</code>
    public int? SystemUpTime { get; set; }

    /// <summary>
    /// System time source configured.
    /// See: PWG 5100.22-2025 Section 7.3.26
    /// </summary>
    public SystemTimeSourceConfigured? SystemTimeSourceConfigured { get; set; }

    /// <summary>
    /// Configured printers summary.
    /// See: PWG 5100.22-2025 Section 7.3.9
    /// </summary>
    public SystemConfiguredPrinter[]? SystemConfiguredPrinters { get; set; }

    /// <summary>
    /// Configured resources summary.
    /// See: PWG 5100.22-2025 Section 7.3.10
    /// </summary>
    public SystemConfiguredResource[]? SystemConfiguredResources { get; set; }

    /// <summary>
    /// Power log entries.
    /// See: PWG 5100.22-2025 Section 7.3.1
    /// </summary>
    public PowerLogEntry[]? PowerLogCol { get; set; }

    /// <summary>
    /// Power state capabilities collection.
    /// See: PWG 5100.22-2025 Section 7.3.2
    /// </summary>
    public PowerStateCapability[]? PowerStateCapabilitiesCol { get; set; }

    /// <summary>
    /// Power state counters collection.
    /// See: PWG 5100.22-2025 Section 7.3.3
    /// </summary>
    public PowerStateCounter[]? PowerStateCountersCol { get; set; }

    /// <summary>
    /// Power state monitor collection.
    /// See: PWG 5100.22-2025 Section 7.3.4
    /// </summary>
    public PowerStateMonitor[]? PowerStateMonitorCol { get; set; }

    /// <summary>
    /// Power state transitions collection.
    /// See: PWG 5100.22-2025 Section 7.3.5
    /// </summary>
    public PowerStateTransition[]? PowerStateTransitionsCol { get; set; }

    /// <summary>
    /// System unique identifier.
    /// See: PWG 5100.22-2025 Section 7.3.32
    /// </summary>
    /// <code>system-uuid</code>
    public Uri? SystemUuid { get; set; }

    /// <summary>
    /// Supported XRI authentication methods.
    /// See: PWG 5100.22-2025 Section 7.3.38
    /// </summary>
    public UriAuthentication[]? XriAuthenticationSupported { get; set; }

    /// <summary>
    /// Supported XRI security methods.
    /// See: PWG 5100.22-2025 Section 7.3.39
    /// </summary>
    public UriSecurity[]? XriSecuritySupported { get; set; }

    /// <summary>
    /// Supported XRI URI schemes.
    /// See: PWG 5100.22-2025 Section 7.3.40
    /// </summary>
    public UriScheme[]? XriUriSchemeSupported { get; set; }
}
