using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Member attributes for <c>power-event-policy-col</c>.
/// See: PWG 5100.22-2025 Section 7.2.21
/// </summary>
[IppAttribute(IppAttributeNames.PowerEventPolicyCol)]
public class PowerEventPolicy : IIppCollection
{
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;

    public int? EventId { get; set; }

    [IppAttribute("event-name", Tag = Tag.TextWithoutLanguage)]
    public string? EventName { get; set; }

    public PowerState? RequestPowerState { get; set; }
}
