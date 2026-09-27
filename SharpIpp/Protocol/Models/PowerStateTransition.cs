using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Member attributes for <c>power-state-transitions-col</c>.
/// See: PWG 5100.22-2025 Section 7.3.5
/// </summary>
[IppAttribute(IppAttributeNames.PowerStateTransitionsCol)]
public class PowerStateTransition : IIppCollection
{

    public IppValue<PowerState>? EndPowerState { get; set; }
    public IppValue<PowerState>? StartPowerState { get; set; }
    public IppValue<int>? StateTransitionSeconds { get; set; }
}
