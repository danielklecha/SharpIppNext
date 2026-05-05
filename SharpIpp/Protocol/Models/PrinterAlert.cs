using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Structured model for the PWG 5100.9 "printer-alert" octet-string value.
/// See: PWG 5100.9-2009 Section 5.2.2
/// </summary>
public sealed class PrinterAlert
{
    /// <summary>
    /// Mapped from "code" (required by ABNF in strict mode).
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Mapped from "index".
    /// </summary>
    public int? Index { get; set; }

    /// <summary>
    /// Mapped from "severity".
    /// </summary>
    public string? Severity { get; set; }

    /// <summary>
    /// Mapped from "training".
    /// </summary>
    public string? Training { get; set; }

    /// <summary>
    /// Mapped from "group".
    /// </summary>
    public string? Group { get; set; }

    /// <summary>
    /// Mapped from "groupindex".
    /// </summary>
    public int? GroupIndex { get; set; }

    /// <summary>
    /// Mapped from "location".
    /// </summary>
    public int? Location { get; set; }

    /// <summary>
    /// Mapped from "time".
    /// </summary>
    public int? Time { get; set; }

    /// <summary>
    /// Optional vendor-specific or unknown key/value pairs.
    /// </summary>
    public IDictionary<string, string>? Extensions { get; set; }

    public static bool TryParse(string value, out PrinterAlert? alert, PrinterAlertParseOptions? options = null)
    {
        options ??= PrinterAlertParseOptions.Relaxed;
        alert = null;

        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (options.RequireVisibleUsAscii && value.Any(ch => !IsVisibleUsAscii(ch)))
            return false;

        var dst = new PrinterAlert();
        var extensions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        var segments = value.Split([';'], StringSplitOptions.RemoveEmptyEntries);
        foreach (var segment in segments)
        {
            var trimmedSegment = segment.Trim();
            if (trimmedSegment.Length == 0)
                continue;

            var eq = trimmedSegment.IndexOf('=');
            if (eq < 0)
            {
                if (options.StrictConformance)
                    return false;
                continue;
            }

            var key = trimmedSegment.Substring(0, eq).Trim();
            var partValue = trimmedSegment.Substring(eq + 1).Trim();
            if (key.Length == 0 || partValue.Length == 0)
            {
                if (options.StrictConformance)
                    return false;
                continue;
            }

            switch (key.ToLowerInvariant())
            {
                case "code":
                    if (options.StrictConformance && !IsAsciiLetters(partValue))
                        return false;
                    dst.Code = partValue;
                    break;
                case "index":
                    if (!TryParseInt(partValue, options.StrictConformance, out var index))
                        return false;
                    if (index.HasValue)
                        dst.Index = index.Value;
                    else
                        extensions[key] = partValue;
                    break;
                case "severity":
                    if (options.StrictConformance && !IsAsciiLetters(partValue))
                        return false;
                    dst.Severity = partValue;
                    break;
                case "training":
                    if (options.StrictConformance && !IsAsciiLetters(partValue))
                        return false;
                    dst.Training = partValue;
                    break;
                case "group":
                    if (options.StrictConformance && !IsAsciiLetters(partValue))
                        return false;
                    dst.Group = partValue;
                    break;
                case "groupindex":
                    if (!TryParseInt(partValue, options.StrictConformance, out var groupIndex))
                        return false;
                    if (groupIndex.HasValue)
                        dst.GroupIndex = groupIndex.Value;
                    else
                        extensions[key] = partValue;
                    break;
                case "location":
                    if (!TryParseInt(partValue, options.StrictConformance, out var location))
                        return false;
                    if (location.HasValue)
                        dst.Location = location.Value;
                    else
                        extensions[key] = partValue;
                    break;
                case "time":
                    if (!TryParseInt(partValue, options.StrictConformance, out var time))
                        return false;
                    if (time.HasValue)
                        dst.Time = time.Value;
                    else
                        extensions[key] = partValue;
                    break;
                default:
                    if (!options.AllowUnknownElements)
                        return false;
                    extensions[key] = partValue;
                    break;
            }
        }

        if (options.RequireCode && string.IsNullOrWhiteSpace(dst.Code))
            return false;

        if (extensions.Count > 0)
            dst.Extensions = extensions;

        alert = dst;
        return true;
    }

    public static PrinterAlert Parse(string value, PrinterAlertParseOptions? options = null)
    {
        if (!TryParse(value, out var alert, options))
            throw new FormatException($"Invalid printer-alert value: '{value}'");

        return alert!;
    }

    public static string Serialize(PrinterAlert alert)
    {
        if (alert == null)
            throw new ArgumentNullException(nameof(alert));

        if (string.IsNullOrWhiteSpace(alert.Code))
        {
            throw new FormatException("The 'code' element is required for printer-alert serialization.");
        }

        var sb = new StringBuilder();
        Append(sb, "code", alert.Code);
        AppendInt(sb, "index", alert.Index);
        Append(sb, "severity", alert.Severity);
        Append(sb, "training", alert.Training);
        Append(sb, "group", alert.Group);
        AppendInt(sb, "groupindex", alert.GroupIndex);
        AppendInt(sb, "location", alert.Location);
        AppendInt(sb, "time", alert.Time);

        if (alert.Extensions != null)
        {
            foreach (var kv in alert.Extensions)
                Append(sb, kv.Key, kv.Value);
        }

        return sb.ToString();
    }

    private static bool TryParseInt(string value, bool strictConformance, out int? parsed)
    {
        parsed = null;
        if (!value.All(char.IsDigit))
            return !strictConformance;

        if (!int.TryParse(value, out var i))
            return !strictConformance;

        parsed = i;
        return true;
    }

    public static bool IsAsciiLetters(string value)
        => value.Length > 0 && value.All(ch => ch is >= 'A' and <= 'Z' or >= 'a' and <= 'z');

    private static bool IsVisibleUsAscii(char ch)
        => ch is >= ' ' and <= '~';

    private static void Append(StringBuilder sb, string key, string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return;

        if (sb.Length > 0)
            sb.Append(';');
        sb.Append(key).Append('=').Append(value);
    }

    private static void AppendInt(StringBuilder sb, string key, int? value)
    {
        if (value.HasValue)
            Append(sb, key, value.Value.ToString());
    }
}

/// <summary>
/// Parser options for structured printer-alert decoding.
/// </summary>
public sealed class PrinterAlertParseOptions
{
    public static PrinterAlertParseOptions Relaxed { get; } = new();

    public static PrinterAlertParseOptions Strict { get; } = new()
    {
        StrictConformance = true,
        RequireVisibleUsAscii = true,
        RequireCode = true,
        AllowUnknownElements = false,
    };

    /// <summary>
    /// Enables strict ABNF validation for known elements and value token classes.
    /// </summary>
    public bool StrictConformance { get; init; }

    /// <summary>
    /// Enforces visible US-ASCII subset only (no control characters).
    /// </summary>
    public bool RequireVisibleUsAscii { get; init; }

    /// <summary>
    /// Requires the mandatory "code" element.
    /// </summary>
    public bool RequireCode { get; init; } = true;

    /// <summary>
    /// Allows unknown extension elements when true.
    /// </summary>
    public bool AllowUnknownElements { get; init; } = true;
}