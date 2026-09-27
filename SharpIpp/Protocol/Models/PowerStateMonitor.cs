using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Member attributes for <c>power-state-monitor-col</c>.
/// See: PWG 5100.22-2025 Section 7.3.4
/// </summary>
[IppAttribute(IppAttributeNames.PowerStateMonitorCol)]
public class PowerStateMonitor : IIppCollection
{

    public IppValue<int>? CurrentMonthKwh { get; set; }
    public IppValue<int>? CurrentWatts { get; set; }
    public IppValue<int>? LifetimeKwh { get; set; }
    public IppValue<bool>? MetersAreActual { get; set; }
    public IppValue<PowerState>? PowerState { get; set; }

    [IppAttribute("power-state-message", Tag = Tag.TextWithoutLanguage)]
    public IppValue<string>? PowerStateMessage { get; set; }

    public IppValue<bool>? PowerUsageIsRmsWatts { get; set; }
    public IppValue<PowerState[]>? ValidRequestPowerStates { get; set; }
}
