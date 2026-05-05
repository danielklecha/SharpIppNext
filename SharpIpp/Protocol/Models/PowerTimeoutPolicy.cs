namespace SharpIpp.Protocol.Models;

/// <summary>
/// Member attributes for <c>power-timeout-policy-col</c>.
/// See: PWG 5100.22-2025 Section 7.2.22
/// </summary>
public class PowerTimeoutPolicy : IIppCollection
{
    bool IIppCollection.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((IIppCollection)this).IsValue;

    public PowerState? RequestPowerState { get; set; }
    public PowerState? StartPowerState { get; set; }
    public int? TimeoutId { get; set; }
    public string? TimeoutPredicate { get; set; }
    public int? TimeoutSeconds { get; set; }
}