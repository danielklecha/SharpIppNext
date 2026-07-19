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
}
