using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Member attributes for <c>power-state-capabilities-col</c>.
/// See: PWG 5100.22-2025 Section 7.3.2
/// </summary>
[IppAttribute(IppAttributeNames.PowerStateCapabilitiesCol)]
public class PowerStateCapability : IIppCollection
{

    public IppValue<bool>? CanAcceptJobs { get; set; }
    public IppValue<bool>? CanProcessJobs { get; set; }
    public IppValue<int>? PowerActiveWatts { get; set; }
    public IppValue<int>? PowerInactiveWatts { get; set; }
    public IppValue<PowerState>? PowerState { get; set; }
}
