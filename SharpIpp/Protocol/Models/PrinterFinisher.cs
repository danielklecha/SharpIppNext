using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    /// <summary>
    /// Parses a printer-finisher octet-string value into a <see cref="PrinterFinisher"/>.
    /// </summary>
    public static PrinterFinisher Parse(string value)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value));
        if (string.IsNullOrWhiteSpace(value))
            throw new FormatException("Invalid printer-finisher value: empty string");

        var finisher = new PrinterFinisher();
        var extensions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        var query = value.Split([';'], StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Split(['='], 2))
            .Where(x => x.Length == 2)
            .Select(x => new { Key = x[0].Trim(), Value = x[1].Trim() });

        foreach (var row in query)
        {
            switch (row.Key.ToLowerInvariant())
            {
                case "type":
                    finisher.Type = row.Value;
                    break;
                case "unit":
                    finisher.Unit = row.Value;
                    break;
                case "maxcapacity" when int.TryParse(row.Value, out var maxcapacity):
                    finisher.MaxCapacity = maxcapacity;
                    break;
                case "capacity" when int.TryParse(row.Value, out var capacity):
                    finisher.Capacity = capacity;
                    break;
                case "index" when int.TryParse(row.Value, out var index):
                    finisher.Index = index;
                    break;
                case "presentonoff":
                    finisher.PresentOnOff = row.Value;
                    break;
                case "status" when int.TryParse(row.Value, out var status):
                    finisher.Status = status;
                    break;
                default:
                    extensions[row.Key] = row.Value;
                    break;
            }
        }

        if (extensions.Count > 0)
        {
            finisher.Extensions = extensions;
        }

        return finisher;
    }

    /// <summary>
    /// Attempts to parse a printer-finisher octet-string value into a <see cref="PrinterFinisher"/>.
    /// </summary>
    public static bool TryParse(string? value, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out PrinterFinisher? result)
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
    /// Converts the <see cref="PrinterFinisher"/> into its formatted printer-finisher string representation.
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

        append("type", Type);
        append("unit", Unit);
        appendInt("maxcapacity", MaxCapacity);
        appendInt("index", Index);
        append("presentonoff", PresentOnOff);
        appendInt("status", Status);
        appendInt("capacity", Capacity);

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
