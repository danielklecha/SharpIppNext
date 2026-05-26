using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

// ReSharper disable once UnusedMember.Global
internal class PrinterAlertProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateIppMap<string, PrinterAlert>((src, _) => Parse(src));
        mapper.CreateIppMap<byte[], PrinterAlert>((src, map) => map.Map<PrinterAlert>(Encoding.UTF8.GetString(src)));
        mapper.CreateIppMap<PrinterAlert, string>((src, _) => Serialize(src));
        mapper.CreateIppMap<PrinterAlert, byte[]>((src, map) => Encoding.UTF8.GetBytes(map.Map<string>(src)));
        mapper.CreateIppMap<OctetString, PrinterAlert>((src, map) => map.Map<PrinterAlert>(src.Value));
        mapper.CreateIppMap<PrinterAlert, OctetString>((src, map) => new OctetString(map.Map<byte[]>(src)));
    }

    private static PrinterAlert Parse(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new FormatException("Invalid printer-alert value: empty string");

        var alert = new PrinterAlert();

        var query = value.Split([';'], StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Split(['='], 2))
            .Where(x => x.Length == 2)
            .Select(x => new { Key = x[0].Trim(), Value = x[1].Trim() })
            .Where(x => !string.IsNullOrWhiteSpace(x.Key) && !string.IsNullOrWhiteSpace(x.Value));

        foreach (var row in query)
        {
            alert.Dictionary[row.Key] = row.Value;
        }

        if (string.IsNullOrWhiteSpace(alert.Code))
        {
            throw new FormatException("The 'code' element is required for printer-alert.");
        }

        return alert;
    }

    private static string Serialize(PrinterAlert alert)
    {
        if (string.IsNullOrWhiteSpace(alert.Code))
        {
            throw new FormatException("The 'code' element is required for printer-alert serialization.");
        }

        var sb = new StringBuilder();
        void append(string key, string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return;
            if (sb.Length > 0)
                sb.Append(';');
            sb.Append(key).Append('=').Append(value);
        }

        append("code", alert.Code);

        // Serialize standard keys in a defined order for backward compatibility / cleanliness
        foreach (var key in alert.StandardKeys)
        {
            if (key.Equals("code", StringComparison.OrdinalIgnoreCase))
                continue;
            append(key, alert.Dictionary.TryGetValue(key, out var val) ? val : null);
        }

        // Serialize extension keys
        foreach (var kvp in alert.Dictionary)
        {
            if (alert.StandardKeys.Contains(kvp.Key))
                continue;
            append(kvp.Key, kvp.Value);
        }

        return sb.ToString();
    }
}
