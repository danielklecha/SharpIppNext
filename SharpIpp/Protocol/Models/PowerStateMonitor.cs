using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Member attributes for <c>power-state-monitor-col</c>.
/// See: PWG 5100.22-2025 Section 7.3.4
/// </summary>
[IppAttribute(IppAttributeNames.PowerStateMonitorCol)]
public class PowerStateMonitor : IIppCollection
{
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;

    public int? CurrentMonthKwh { get; set; }
    public int? CurrentWatts { get; set; }
    public int? LifetimeKwh { get; set; }
    public bool? MetersAreActual { get; set; }
    public PowerState? PowerState { get; set; }

    [IppAttribute("power-state-message", Tag = Tag.TextWithoutLanguage)]
    public string? PowerStateMessage { get; set; }

    public bool? PowerUsageIsRmsWatts { get; set; }
    public PowerState[]? ValidRequestPowerStates { get; set; }
}
