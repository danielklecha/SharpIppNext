namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the events that can be subscribed to for notifications.
/// See: RFC 3995 Section 10
/// See: PWG 5100.5-2024 Section 9.1
/// See: PWG 5100.18-2025 Section 9.4
/// See: PWG 5100.22-2025 Section 9.2
/// </summary>
public readonly record struct NotifyEvent(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    // Job events (RFC 3995)

    /// <summary>
    /// A new Job has been created.
    /// See: RFC 3995 Section 10
    /// </summary>
    public static readonly NotifyEvent JobCreated = new("job-created");

    /// <summary>
    /// A Job has reached a terminal state ('aborted', 'canceled', or 'completed').
    /// See: RFC 3995 Section 10
    /// </summary>
    public static readonly NotifyEvent JobCompleted = new("job-completed");

    /// <summary>
    /// The Job state has changed.
    /// See: RFC 3995 Section 10
    /// </summary>
    public static readonly NotifyEvent JobStateChanged = new("job-state-changed");

    /// <summary>
    /// The Job Template or Description attributes have been changed.
    /// See: RFC 3995 Section 10
    /// </summary>
    public static readonly NotifyEvent JobConfigChanged = new("job-config-changed");

    /// <summary>
    /// The Job has made progress (e.g., pages completed).
    /// See: RFC 3995 Section 10
    /// </summary>
    public static readonly NotifyEvent JobProgress = new("job-progress");

    /// <summary>
    /// The Job has entered the 'processing-stopped' state.
    /// See: RFC 3995 Section 10
    /// </summary>
    public static readonly NotifyEvent JobStopped = new("job-stopped");

    /// <summary>
    /// The Job is now available via the Fetch-Job operation. This is a sub-event of 'job-state-changed'.
    /// See: PWG 5100.18-2025 Section 9.4
    /// </summary>
    public static readonly NotifyEvent JobFetchable = new("job-fetchable");

    // Printer events (RFC 3995 / PWG 5100.22)

    /// <summary>
    /// The Printer configuration has changed (any Printer Description attribute changed).
    /// See: RFC 3995 Section 10
    /// </summary>
    public static readonly NotifyEvent PrinterConfigChanged = new("printer-config-changed");

    /// <summary>
    /// A new Printer has been created on the System.
    /// See: RFC 3995 Section 10
    /// See: PWG 5100.22-2025 Section 9.2
    /// </summary>
    public static readonly NotifyEvent PrinterCreated = new("printer-created");

    /// <summary>
    /// A Printer has been deleted from the System.
    /// See: RFC 3995 Section 10
    /// See: PWG 5100.22-2025 Section 9.2
    /// </summary>
    public static readonly NotifyEvent PrinterDeleted = new("printer-deleted");

    /// <summary>
    /// The Printer state has changed.
    /// See: RFC 3995 Section 10
    /// </summary>
    public static readonly NotifyEvent PrinterStateChanged = new("printer-state-changed");

    /// <summary>
    /// The Printer has entered the 'stopped' state.
    /// See: RFC 3995 Section 10
    /// </summary>
    public static readonly NotifyEvent PrinterStopped = new("printer-stopped");

    // Resource events (PWG 5100.22)

    /// <summary>
    /// A Resource has been canceled.
    /// See: PWG 5100.22-2025 Section 9.2
    /// </summary>
    public static readonly NotifyEvent ResourceCanceled = new("resource-canceled");

    /// <summary>
    /// The Resource configuration has changed (any Resource Description attribute changed).
    /// See: PWG 5100.22-2025 Section 9.2
    /// </summary>
    public static readonly NotifyEvent ResourceConfigChanged = new("resource-config-changed");

    /// <summary>
    /// A new Resource has been created.
    /// See: PWG 5100.22-2025 Section 9.2
    /// </summary>
    public static readonly NotifyEvent ResourceCreated = new("resource-created");

    /// <summary>
    /// A Resource has been installed (allocated to the System or a Printer).
    /// See: PWG 5100.22-2025 Section 9.2
    /// </summary>
    public static readonly NotifyEvent ResourceInstalled = new("resource-installed");

    /// <summary>
    /// The Resource state has changed (the value of "resource-state" or "resource-state-reasons" changed).
    /// See: PWG 5100.22-2025 Section 9.2
    /// </summary>
    public static readonly NotifyEvent ResourceStateChanged = new("resource-state-changed");

    // System events (PWG 5100.22)

    /// <summary>
    /// The System configuration has changed (any System Description attribute changed).
    /// See: PWG 5100.22-2025 Section 9.2
    /// </summary>
    public static readonly NotifyEvent SystemConfigChanged = new("system-config-changed");

    /// <summary>
    /// The System has been booted or started up.
    /// See: PWG 5100.22-2025 Section 9.2
    /// </summary>
    public static readonly NotifyEvent SystemRestarted = new("system-restarted");

    /// <summary>
    /// The System is being shut down or powered off. This event is delivered before the System is down.
    /// See: PWG 5100.22-2025 Section 9.2
    /// </summary>
    public static readonly NotifyEvent SystemShutdown = new("system-shutdown");

    /// <summary>
    /// The System state has changed (the value of "system-state" or "system-state-reasons" changed).
    /// See: PWG 5100.22-2025 Section 9.2
    /// </summary>
    public static readonly NotifyEvent SystemStateChanged = new("system-state-changed");

    /// <summary>
    /// The System has entered the 'stopped' state.
    /// See: PWG 5100.22-2025 Section 9.2
    /// </summary>
    public static readonly NotifyEvent SystemStopped = new("system-stopped");

    // Document events (PWG 5100.5-2024 Section 9.1)

    /// <summary>
    /// The Document has reached a terminating state ('aborted', 'canceled', or 'completed').
    /// This is a sub-event of 'document-state-changed'.
    /// See: PWG 5100.5-2024 Section 9.1
    /// </summary>
    public static readonly NotifyEvent DocumentCompleted = new("document-completed");

    /// <summary>
    /// The Document Template or Description attributes have been changed.
    /// See: PWG 5100.5-2024 Section 9.1
    /// </summary>
    public static readonly NotifyEvent DocumentConfigChanged = new("document-config-changed");

    /// <summary>
    /// A new Document has been created.
    /// See: PWG 5100.5-2024 Section 9.1
    /// </summary>
    public static readonly NotifyEvent DocumentCreated = new("document-created");

    /// <summary>
    /// The Document state has changed.
    /// See: PWG 5100.5-2024 Section 9.1
    /// </summary>
    public static readonly NotifyEvent DocumentStateChanged = new("document-state-changed");

    /// <summary>
    /// The Document has entered the 'processing-stopped' state.
    /// This is a sub-event of 'document-state-changed'.
    /// See: PWG 5100.5-2024 Section 9.1
    /// </summary>
    public static readonly NotifyEvent DocumentStopped = new("document-stopped");

    /// <summary>
    /// The Document is now available via the Fetch-Document operation. This is a sub-event of 'document-state-changed'.
    /// See: PWG 5100.18-2025 Section 9.4
    /// </summary>
    public static readonly NotifyEvent DocumentFetchable = new("document-fetchable");

    public override string ToString() => Value;
    public static implicit operator string(NotifyEvent value) => value.Value;
    public static implicit operator NotifyEvent(string value) => new(value);
}
