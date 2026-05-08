using System.Collections.Generic;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Parsed representation of a single <c>printer-finisher-supplies</c> value (Section 7.3).
/// </summary>
public class PrinterFinisherSupply
{
    public string? Class { get; set; }
    public string? Type { get; set; }
    public string? Unit { get; set; }
    public int? Max { get; set; }
    public int? Level { get; set; }
    public string? Color { get; set; }
    public int? Index { get; set; }
    public int? DeviceIndex { get; set; }
    public Dictionary<string, string>? Extensions { get; set; }
}
