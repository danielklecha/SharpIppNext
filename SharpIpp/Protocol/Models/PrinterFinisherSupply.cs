using System;
using System.Collections.Generic;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Parsed representation of a single <c>printer-finisher-supplies</c> value (Section 7.3).
/// </summary>
public class PrinterFinisherSupply : IppStructuredString
{
    public PrinterFinisherSupply() : base(StringComparer.OrdinalIgnoreCase)
    {
    }

    public FinisherSupplyClass? Class
    {
        get => GetSmartEnum<FinisherSupplyClass>("class");
        set => SetSmartEnum("class", value);
    }

    public FinisherSupplyType? Type
    {
        get => GetSmartEnum<FinisherSupplyType>("type");
        set => SetSmartEnum("type", value);
    }

    public CapacityUnit? Unit
    {
        get => GetSmartEnum<CapacityUnit>("unit");
        set => SetSmartEnum("unit", value);
    }

    public int? Max
    {
        get => GetInt("max");
        set => SetInt("max", value);
    }

    public int? Level
    {
        get => GetInt("level");
        set => SetInt("level", value);
    }

    public string? Color
    {
        get => Get("color");
        set => Set("color", value);
    }

    public int? Index
    {
        get => GetInt("index");
        set => SetInt("index", value);
    }

    public int? DeviceIndex
    {
        get => GetInt("deviceindex");
        set => SetInt("deviceindex", value);
    }

    public override HashSet<string> StandardKeys { get; } = new(StringComparer.OrdinalIgnoreCase)
    {
        "class", "type", "unit", "max", "level", "color", "index", "deviceindex"
    };
}
