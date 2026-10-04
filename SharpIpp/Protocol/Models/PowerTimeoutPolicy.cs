using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Member attributes for <c>power-timeout-policy-col</c>.
/// See: PWG 5100.22-2025 Section 7.2.22
/// </summary>
[IppAttribute(IppAttributeNames.PowerTimeoutPolicyCol)]
public class PowerTimeoutPolicy : IIppCollection
{

    public IppValue<PowerState>? RequestPowerState { get; set; }
    public IppValue<PowerState>? StartPowerState { get; set; }
    public IppValue<int>? TimeoutId { get; set; }

    [IppAttribute("timeout-predicate", Tag = Tag.TextWithoutLanguage)]
    public StringWithLanguage? TimeoutPredicate { get; set; }

    public IppValue<int>? TimeoutSeconds { get; set; }
}
