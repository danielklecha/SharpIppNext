using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

// ReSharper disable once UnusedMember.Global
internal class PrinterFinisherProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateIppMap<string, PrinterFinisher>((src, _) => Parse(src));
        mapper.CreateIppMap<PrinterFinisher, string>((src, _) => Serialize(src));
        mapper.CreateIppMap<byte[], PrinterFinisher>((src, map) => map.Map<PrinterFinisher>(Encoding.UTF8.GetString(src)));
    }

    private static PrinterFinisher Parse(string value)
    {
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

    private static string Serialize(PrinterFinisher finisher)
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

        append("type", finisher.Type);
        append("unit", finisher.Unit);
        appendInt("maxcapacity", finisher.MaxCapacity);
        appendInt("index", finisher.Index);
        append("presentonoff", finisher.PresentOnOff);
        appendInt("status", finisher.Status);
        appendInt("capacity", finisher.Capacity);

        if (finisher.Extensions != null)
        {
            foreach (var kvp in finisher.Extensions)
            {
                append(kvp.Key, kvp.Value);
            }
        }

        if (builder.Length > 0)
            builder.Append(';');

        return builder.ToString();
    }
}
