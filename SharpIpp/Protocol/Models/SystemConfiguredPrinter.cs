namespace SharpIpp.Protocol.Models;

/// <summary>
/// Member attributes for configured printer entries in System status.
/// See: PWG 5100.22-2025 Section 7.3.9
/// </summary>
public class SystemConfiguredPrinter : IIppCollection
{
    bool IIppCollection.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((IIppCollection)this).IsValue;

    public int? PrinterId { get; set; }
    public string? PrinterInfo { get; set; }
    public bool? PrinterIsAcceptingJobs { get; set; }
    public string? PrinterName { get; set; }
    public PrinterServiceType? PrinterServiceType { get; set; }
    public PrinterState? PrinterState { get; set; }
    public string[]? PrinterStateReasons { get; set; }
    public SystemXri[]? PrinterXriSupported { get; set; }
}