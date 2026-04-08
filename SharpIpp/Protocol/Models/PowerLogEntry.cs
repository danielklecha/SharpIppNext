using System;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Member attributes for <c>power-log-col</c>.
/// See: PWG 5100.22-2025 Section 7.3.1
/// </summary>
public class PowerLogEntry : IIppCollection
{
    public bool IsValue { get; set; } = true;

    public int? LogId { get; set; }
    public PowerState? PowerState { get; set; }
    public DateTimeOffset? PowerStateDateTime { get; set; }
    public string? PowerStateMessage { get; set; }
}