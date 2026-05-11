namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the events that can be subscribed to for notifications.
/// See: RFC 3995 Section 10, PWG 5100.22-2025 Section 7.2.12
/// </summary>
public readonly record struct NotifyEvent(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    // Job events (RFC 3995)
    public static readonly NotifyEvent JobCreated = new("job-created");
    public static readonly NotifyEvent JobCompleted = new("job-completed");
    public static readonly NotifyEvent JobStateChanged = new("job-state-changed");
    public static readonly NotifyEvent JobConfigChanged = new("job-config-changed");
    public static readonly NotifyEvent JobProgress = new("job-progress");
    public static readonly NotifyEvent JobStopped = new("job-stopped");

    // Printer events (RFC 3995 / PWG 5100.22)
    public static readonly NotifyEvent PrinterConfigChanged = new("printer-config-changed");
    public static readonly NotifyEvent PrinterCreated = new("printer-created");
    public static readonly NotifyEvent PrinterDeleted = new("printer-deleted");
    public static readonly NotifyEvent PrinterStateChanged = new("printer-state-changed");
    public static readonly NotifyEvent PrinterStopped = new("printer-stopped");

    // Resource events (PWG 5100.22)
    public static readonly NotifyEvent ResourceCanceled = new("resource-canceled");
    public static readonly NotifyEvent ResourceConfigChanged = new("resource-config-changed");
    public static readonly NotifyEvent ResourceCreated = new("resource-created");
    public static readonly NotifyEvent ResourceInstalled = new("resource-installed");
    public static readonly NotifyEvent ResourceStateChanged = new("resource-state-changed");

    // System events (PWG 5100.22)
    public static readonly NotifyEvent SystemConfigChanged = new("system-config-changed");
    public static readonly NotifyEvent SystemRestarted = new("system-restarted");
    public static readonly NotifyEvent SystemShutdown = new("system-shutdown");
    public static readonly NotifyEvent SystemStateChanged = new("system-state-changed");
    public static readonly NotifyEvent SystemStopped = new("system-stopped");

    // Document events (PWG 5100.5-2024 Section 9.1)
    /// <summary>'document-completed': The Document has reached a terminating state ('aborted', 'canceled', or 'completed').
    /// This is a sub-event of 'document-state-changed'.
    /// See: PWG 5100.5-2024 Section 9.1</summary>
    public static readonly NotifyEvent DocumentCompleted = new("document-completed");

    /// <summary>'document-config-changed': The Document Template or Description attributes have been changed.
    /// See: PWG 5100.5-2024 Section 9.1</summary>
    public static readonly NotifyEvent DocumentConfigChanged = new("document-config-changed");

    /// <summary>'document-created': The Document has been created.
    /// See: PWG 5100.5-2024 Section 9.1</summary>
    public static readonly NotifyEvent DocumentCreated = new("document-created");

    /// <summary>'document-state-changed': The Document state has changed.
    /// See: PWG 5100.5-2024 Section 9.1</summary>
    public static readonly NotifyEvent DocumentStateChanged = new("document-state-changed");

    /// <summary>'document-stopped': The Document has entered the 'processing-stopped' state.
    /// This is a sub-event of 'document-state-changed'.
    /// See: PWG 5100.5-2024 Section 9.1</summary>
    public static readonly NotifyEvent DocumentStopped = new("document-stopped");

    public override string ToString() => Value;
    public static implicit operator string(NotifyEvent value) => value.Value;
    public static explicit operator NotifyEvent(string value) => new(value);
}
