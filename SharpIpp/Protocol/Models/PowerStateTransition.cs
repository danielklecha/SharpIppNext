namespace SharpIpp.Protocol.Models;

/// <summary>
/// Member attributes for <c>power-state-transitions-col</c>.
/// See: PWG 5100.22-2025 Section 7.3.5
/// </summary>
public class PowerStateTransition : IIppCollection
{
    public bool IsValue { get; set; } = true;

    public PowerState? EndPowerState { get; set; }
    public PowerState? StartPowerState { get; set; }
    public int? StateTransitionSeconds { get; set; }
}