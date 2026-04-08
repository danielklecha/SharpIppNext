namespace SharpIpp.Protocol.Models;

/// <summary>
/// Member attributes for <c>power-timeout-policy-col</c>.
/// See: PWG 5100.22-2025 Section 7.2.22
/// </summary>
public class PowerTimeoutPolicy : IIppCollection
{
    public bool IsValue { get; set; } = true;

    public PowerState? RequestPowerState { get; set; }
    public PowerState? StartPowerState { get; set; }
    public int? TimeoutId { get; set; }
    public string? TimeoutPredicate { get; set; }
    public int? TimeoutSeconds { get; set; }
}