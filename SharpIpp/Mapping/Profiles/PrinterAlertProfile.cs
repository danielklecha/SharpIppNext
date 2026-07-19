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

    /// <summary>
    /// Parses a printer-alert octet-string value into a <see cref="PrinterAlert"/>.
    /// Parsing is intentionally lenient in two ways that go beyond the strict PWG 5100.9-2009 §5.2.2 ABNF:
    /// <list type="bullet">
    ///   <item>A bare keyword as the first token (e.g. <c>coverOpen;severity=critical</c>) is accepted
    ///   and treated as the <c>code</c> value, to accommodate real-world printers that omit the key name.</item>
    ///   <item>A missing <c>code</c> element is tolerated rather than treated as a parse error.
    ///   Per the spec, <c>code</c> is REQUIRED (Table 5-3); callers that need strict validation should
    ///   check <see cref="PrinterAlert.Code"/> for <see langword="null"/> after parsing.</item>
    /// </list>
    /// </summary>
    private static PrinterAlert Parse(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new FormatException("Invalid printer-alert value: empty string");

        var alert = new PrinterAlert();

        var parts = value.Split([';'], StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < parts.Length; i++)
        {
            var part = parts[i].Trim();
            if (string.IsNullOrWhiteSpace(part))
                continue;

            var eqIndex = part.IndexOf('=');
            if (eqIndex > 0)
            {
                var key = part.Substring(0, eqIndex).Trim();
                var val = part.Substring(eqIndex + 1).Trim();
                if (!string.IsNullOrWhiteSpace(val))
                {
                    alert.Dictionary[key] = val;
                }
            }
            else
            {
                // Lenient: accept a bare keyword (no '=') as the first token and treat it as the
                // 'code' value. Per PWG 5100.9-2009 §5.2.2 the strict ABNF requires "code=<value>",
                // but some real-world printers emit the code value without the key name.
                if (i == 0 && string.IsNullOrWhiteSpace(alert.Code))
                {
                    alert.Code = part;
                }
            }
        }

        return alert;
    }

    private static string Serialize(PrinterAlert alert)
    {
        var sb = new StringBuilder();
        void append(string key, string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return;
            if (sb.Length > 0)
                sb.Append(';');
            sb.Append(key).Append('=').Append(value);
        }

        if (!string.IsNullOrWhiteSpace(alert.Code))
        {
            append("code", alert.Code);
        }

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
