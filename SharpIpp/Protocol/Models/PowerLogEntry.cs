using System;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Member attributes for <c>power-log-col</c>.
/// See: PWG 5100.22-2025 Section 7.3.1
/// </summary>
public class PowerLogEntry : IIppCollection
{
    bool IIppCollection.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((IIppCollection)this).IsValue;

    public int? LogId { get; set; }
    public PowerState? PowerState { get; set; }
    public DateTimeOffset? PowerStateDateTime { get; set; }
    public string? PowerStateMessage { get; set; }
}
