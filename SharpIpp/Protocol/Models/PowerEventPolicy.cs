namespace SharpIpp.Protocol.Models;

/// <summary>
/// Member attributes for <c>power-event-policy-col</c>.
/// See: PWG 5100.22-2025 Section 7.2.21
/// </summary>
public class PowerEventPolicy : IIppCollection
{
    public bool IsValue { get; set; } = true;

    public int? EventId { get; set; }
    public string? EventName { get; set; }
    public PowerState? RequestPowerState { get; set; }
}