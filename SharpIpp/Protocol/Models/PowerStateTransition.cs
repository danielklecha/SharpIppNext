namespace SharpIpp.Protocol.Models;

/// <summary>
/// Member attributes for <c>power-state-transitions-col</c>.
/// See: PWG 5100.22-2025 Section 7.3.5
/// </summary>
public class PowerStateTransition : IIppCollection
{
    bool IIppCollection.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((IIppCollection)this).IsValue;

    public PowerState? EndPowerState { get; set; }
    public PowerState? StartPowerState { get; set; }
    public int? StateTransitionSeconds { get; set; }
}
