namespace SharpIpp.Protocol.Models;

/// <summary>
/// Member attributes for <c>power-state-monitor-col</c>.
/// See: PWG 5100.22-2025 Section 7.3.4
/// </summary>
public class PowerStateMonitor : IIppCollection
{
    bool IIppCollection.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((IIppCollection)this).IsValue;

    public int? CurrentMonthKwh { get; set; }
    public int? CurrentWatts { get; set; }
    public int? LifetimeKwh { get; set; }
    public bool? MetersAreActual { get; set; }
    public PowerState? PowerState { get; set; }
    public string? PowerStateMessage { get; set; }
    public bool? PowerUsageIsRmsWatts { get; set; }
    public IppOperation[]? ValidRequestPowerStates { get; set; }
}