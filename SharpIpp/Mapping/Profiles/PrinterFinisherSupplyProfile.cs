using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

// ReSharper disable once UnusedMember.Global
internal class PrinterFinisherSupplyProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateIppMap<string, PrinterFinisherSupply>((src, _) => Parse(src));
        mapper.CreateIppMap<PrinterFinisherSupply, string>((src, _) => Serialize(src));
    }

    private static PrinterFinisherSupply Parse(string value)
    {
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
                case "deviceindex":
                    supply.DeviceIndex = row.Value;
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

    private static string Serialize(PrinterFinisherSupply supply)
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

        append("class", supply.Class);
        append("type", supply.Type);
        append("unit", supply.Unit);
        appendInt("max", supply.Max);
        appendInt("level", supply.Level);
        append("color", supply.Color);
        appendInt("index", supply.Index);
        append("deviceIndex", supply.DeviceIndex);

        if (supply.Extensions != null)
        {
            foreach (var kvp in supply.Extensions)
            {
                append(kvp.Key, kvp.Value);
            }
        }

        if (builder.Length > 0)
            builder.Append(';');

        return builder.ToString();
    }
}

