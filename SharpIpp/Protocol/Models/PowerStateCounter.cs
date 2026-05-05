namespace SharpIpp.Protocol.Models;

/// <summary>
/// Member attributes for <c>power-state-counters-col</c>.
/// See: PWG 5100.22-2025 Section 7.3.3
/// </summary>
public class PowerStateCounter : IIppCollection
{
    bool IIppCollection.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((IIppCollection)this).IsValue;

    public int? HibernateTransitions { get; set; }
    public int? OnTransitions { get; set; }
    public int? StandbyTransitions { get; set; }
    public int? SuspendTransitions { get; set; }
}