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
}
