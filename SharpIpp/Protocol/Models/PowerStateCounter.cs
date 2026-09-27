using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Member attributes for <c>power-state-counters-col</c>.
/// See: PWG 5100.22-2025 Section 7.3.3
/// </summary>
[IppAttribute(IppAttributeNames.PowerStateCountersCol)]
public class PowerStateCounter : IIppCollection
{

    public IppValue<int>? HibernateTransitions { get; set; }
    public IppValue<int>? OnTransitions { get; set; }
    public IppValue<int>? StandbyTransitions { get; set; }
    public IppValue<int>? SuspendTransitions { get; set; }
}
