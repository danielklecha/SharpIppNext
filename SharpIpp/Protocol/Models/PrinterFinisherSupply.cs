using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    /// <summary>
    /// Parses a printer-finisher-supplies octet-string value into a <see cref="PrinterFinisherSupply"/>.
    /// </summary>
    public static PrinterFinisherSupply Parse(string value)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value));
        if (string.IsNullOrWhiteSpace(value))
            throw new FormatException("Invalid printer-finisher-supplies value: empty string");

        var supply = new PrinterFinisherSupply();
        var extensions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        var query = value.Split([';'], StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Split(['='], 2))
            .Where(x => x.Length == 2)
            .Select(x => new { Key = x[0].Trim(), Value = x[1].Trim() });

        foreach (var row in query)
        {
            switch (row.Key.ToLowerInvariant())
            {
                case "class":
                    supply.Class = row.Value;
                    break;
                case "type":
                    supply.Type = row.Value;
                    break;
                case "unit":
                    supply.Unit = row.Value;
                    break;
                case "max" when int.TryParse(row.Value, out var max):
                    supply.Max = max;
                    break;
                case "level" when int.TryParse(row.Value, out var level):
                    supply.Level = level;
                    break;
                case "color":
                    supply.Color = row.Value;
                    break;
                case "index" when int.TryParse(row.Value, out var index):
                    supply.Index = index;
                    break;
                case "deviceindex" when int.TryParse(row.Value, out var deviceIndex):
                    supply.DeviceIndex = deviceIndex;
                    break;
                default:
                    extensions[row.Key] = row.Value;
                    break;
            }
        }

        if (extensions.Count > 0)
        {
            supply.Extensions = extensions;
        }

        return supply;
    }

    /// <summary>
    /// Attempts to parse a printer-finisher-supplies octet-string value into a <see cref="PrinterFinisherSupply"/>.
    /// </summary>
    public static bool TryParse(string? value, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out PrinterFinisherSupply? result)
    {
        try
        {
            result = Parse(value!);
            return true;
        }
        catch
        {
            result = null;
            return false;
        }
    }

    /// <summary>
    /// Converts the <see cref="PrinterFinisherSupply"/> into its formatted printer-finisher-supplies string representation.
    /// </summary>
    public override string ToString()
    {
        var builder = new StringBuilder();

        void append(string key, string? value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (builder.Length > 0)
                    builder.Append("; ");
                builder.Append(key).Append('=').Append(value);
            }
        }

        void appendInt(string key, int? value)
        {
            if (value.HasValue)
                append(key, value.Value.ToString());
        }

        append("class", Class);
        append("type", Type);
        append("unit", Unit);
        appendInt("max", Max);
        appendInt("level", Level);
        append("color", Color);
        appendInt("index", Index);
        appendInt("deviceIndex", DeviceIndex);

        if (Extensions != null)
        {
            foreach (var kvp in Extensions)
            {
                append(kvp.Key, kvp.Value);
            }
        }

        if (builder.Length > 0)
            builder.Append(';');

        return builder.ToString();
    }
}
