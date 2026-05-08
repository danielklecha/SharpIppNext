using System.Collections.Generic;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Parsed representation of a single <c>printer-finisher</c> value (Section 7.1).
/// </summary>
public class PrinterFinisher
{
    public string? Type { get; set; }
    public string? Unit { get; set; }
    public int? MaxCapacity { get; set; }
    public int? Index { get; set; }
    public string? PresentOnOff { get; set; }
    public int? Status { get; set; }
    public int? Capacity { get; set; }
    public Dictionary<string, string>? Extensions { get; set; }
}
