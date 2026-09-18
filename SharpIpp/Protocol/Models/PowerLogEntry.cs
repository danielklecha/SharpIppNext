using System;
using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Member attributes for <c>power-log-col</c>.
/// See: PWG 5100.22-2025 Section 7.3.1
/// </summary>
[IppAttribute(IppAttributeNames.PowerLogCol)]
public class PowerLogEntry : IIppCollection
{
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;

    public int? LogId { get; set; }
    public PowerState? PowerState { get; set; }
    public DateTimeOffset? PowerStateDateTime { get; set; }

    [IppAttribute("power-state-message", Tag = Tag.TextWithoutLanguage)]
    public string? PowerStateMessage { get; set; }
}
