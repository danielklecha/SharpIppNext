using System;
using System.Collections.Generic;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Parsed representation of a single <c>printer-finisher</c> value (Section 7.1).
/// </summary>
public class PrinterFinisher : IppStructuredString
{
    public PrinterFinisher() : base(StringComparer.OrdinalIgnoreCase)
    {
    }

    public FinisherType? Type
    {
        get => GetSmartEnum<FinisherType>("type");
        set => SetSmartEnum("type", value);
    }

    public CapacityUnit? Unit
    {
        get => GetSmartEnum<CapacityUnit>("unit");
        set => SetSmartEnum("unit", value);
    }

    public int? MaxCapacity
    {
        get => GetInt("maxcapacity");
        set => SetInt("maxcapacity", value);
    }

    public int? Index
    {
        get => GetInt("index");
        set => SetInt("index", value);
    }

    public PresentOnOff? PresentOnOff
    {
        get => GetSmartEnum<PresentOnOff>("presentonoff");
        set => SetSmartEnum("presentonoff", value);
    }

    public int? Status
    {
        get => GetInt("status");
        set => SetInt("status", value);
    }

    public int? Capacity
    {
        get => GetInt("capacity");
        set => SetInt("capacity", value);
    }

    public override HashSet<string> StandardKeys { get; } = new(StringComparer.OrdinalIgnoreCase)
    {
        "type", "unit", "maxcapacity", "index", "presentonoff", "status", "capacity"
    };
}
