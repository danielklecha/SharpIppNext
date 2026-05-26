using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Structured model for the PWG 5100.9 "printer-alert" octet-string value.
/// See: PWG 5100.9-2009 Section 5.2.2
/// </summary>
public sealed class PrinterAlert : IppStructuredString
{
    public PrinterAlert() : base(StringComparer.OrdinalIgnoreCase)
    {
    }

    /// <summary>
    /// Mapped from "code" (required by ABNF in strict mode).
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
