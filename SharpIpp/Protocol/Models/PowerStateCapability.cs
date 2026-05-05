namespace SharpIpp.Protocol.Models;

/// <summary>
/// Member attributes for <c>power-state-capabilities-col</c>.
/// See: PWG 5100.22-2025 Section 7.3.2
/// </summary>
public class PowerStateCapability : IIppCollection
{
    bool IIppCollection.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((IIppCollection)this).IsValue;

    public bool? CanAcceptJobs { get; set; }
    public bool? CanProcessJobs { get; set; }
    public int? PowerActiveWatts { get; set; }
    public int? PowerInactiveWatts { get; set; }
    public PowerState? PowerState { get; set; }
}