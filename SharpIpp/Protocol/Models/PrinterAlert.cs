using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Structured model for the PWG 5100.9 "printer-alert" octet-string value.
/// See: PWG 5100.9-2009 Section 5.2.2
/// <para>
/// Parsing is intentionally lenient to handle real-world printers that deviate from the
/// strict ABNF grammar: a bare keyword as the first token (e.g. <c>other;severity=critical</c>)
/// is accepted and treated as the <c>code</c> value. A missing <c>code</c> element is also
/// tolerated rather than treated as a hard error.
/// </para>
/// </summary>
public sealed class PrinterAlert : IppStructuredString
{
    public PrinterAlert() : base(StringComparer.OrdinalIgnoreCase)
    {
    }

    /// <summary>
    /// Mapped from "code".
    /// This element is REQUIRED by the PWG 5100.9-2009 Section 5.2.1 (Table 5-3) specification.
    /// The parser accepts a bare keyword as the first token (e.g. <c>coverOpen;severity=critical</c>)
    /// in addition to the strict <c>code=coverOpen;severity=critical</c> form, to remain robust
    /// against real-world printers that omit the key name.
    /// </summary>
    public string? Code
    {
        get => Get("code");
        set => Set("code", value);
    }

    /// <summary>
    /// Mapped from "index".
    /// </summary>
    public int? Index
    {
        get => GetInt("index");
        set => SetInt("index", value);
    }

    /// <summary>
    /// Mapped from "severity".
    /// </summary>
    public string? Severity
    {
        get => Get("severity");
        set => Set("severity", value);
    }

    /// <summary>
    /// Mapped from "training".
    /// </summary>
    public string? Training
    {
        get => Get("training");
        set => Set("training", value);
    }

    /// <summary>
    /// Mapped from "group".
    /// </summary>
    public string? Group
    {
        get => Get("group");
        set => Set("group", value);
    }

    /// <summary>
    /// Mapped from "groupindex".
    /// </summary>
    public int? GroupIndex
    {
        get => GetInt("groupindex");
        set => SetInt("groupindex", value);
    }

    /// <summary>
    /// Mapped from "location".
    /// </summary>
    public int? Location
    {
        get => GetInt("location");
        set => SetInt("location", value);
    }

    /// <summary>
    /// Mapped from "time".
    /// </summary>
    public int? Time
    {
        get => GetInt("time");
        set => SetInt("time", value);
    }

    public override HashSet<string> StandardKeys { get; } = new(StringComparer.OrdinalIgnoreCase)
    {
        "code", "index", "severity", "training", "group", "groupindex", "location", "time"
    };

    /// <summary>
    /// Parses a printer-alert octet-string value into a <see cref="PrinterAlert"/>.
    /// Parsing is intentionally lenient:
    /// <list type="bullet">
    ///   <item>A bare keyword as the first token (e.g. <c>coverOpen;severity=critical</c>) is accepted
    ///   and treated as the <c>code</c> value, to accommodate real-world printers that omit the key name.</item>
    ///   <item>A missing <c>code</c> element is tolerated rather than treated as a parse error.</item>
    /// </list>
    /// </summary>
    public static PrinterAlert Parse(string value)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value));
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

    /// <summary>
    /// Attempts to parse a printer-alert octet-string value into a <see cref="PrinterAlert"/>.
    /// </summary>
    public static bool TryParse(string? value, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out PrinterAlert? result)
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
    /// Converts the <see cref="PrinterAlert"/> into its formatted printer-alert string representation.
    /// </summary>
    public override string ToString()
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

        // Serialize standard keys in a defined order
        foreach (var key in StandardKeys)
        {
            append(key, Dictionary.TryGetValue(key, out var val) ? val : null);
        }

        // Serialize extension keys
        foreach (var kvp in Dictionary)
        {
            if (StandardKeys.Contains(kvp.Key))
                continue;
            append(kvp.Key, kvp.Value);
        }

        return sb.ToString();
    }
}
