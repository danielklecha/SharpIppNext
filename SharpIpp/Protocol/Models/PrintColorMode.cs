using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the print-color-mode attribute, which controls whether the Printer uses color or monochrome output.
/// See: PWG 5100.13-2023 Section 6.2.27
/// </summary>
public readonly record struct PrintColorMode(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// The Printer automatically selects color or monochrome output based on the document content.
    /// See: PWG 5100.13-2023 Section 6.2.27
    /// </summary>
    public static readonly PrintColorMode Auto = new("auto");

    /// <summary>
    /// The Printer automatically selects monochrome output based on the document content.
    /// See: PWG 5100.13-2023 Section 6.2.27
    /// </summary>
    public static readonly PrintColorMode AutoMonochrome = new("auto-monochrome");

    /// <summary>
    /// The Printer produces bi-level (black and white only, no gray) output.
    /// See: PWG 5100.13-2023 Section 6.2.27
    /// </summary>
    public static readonly PrintColorMode BiLevel = new("bi-level");

    /// <summary>
    /// The Printer produces full color output.
    /// See: PWG 5100.13-2023 Section 6.2.27
    /// </summary>
    public static readonly PrintColorMode Color = new("color");

    /// <summary>
    /// The Printer uses a single highlight color in addition to black.
    /// See: PWG 5100.13-2023 Section 6.2.27
    /// </summary>
    public static readonly PrintColorMode Highlight = new("highlight");

    /// <summary>
    /// The Printer produces monochrome (single-color) output.
    /// See: PWG 5100.13-2023 Section 6.2.27
    /// </summary>
    public static readonly PrintColorMode Monochrome = new("monochrome");

    /// <summary>
    /// The Printer produces bi-level output using the process color model.
    /// See: PWG 5100.13-2023 Section 6.2.27
    /// </summary>
    public static readonly PrintColorMode ProcessBiLevel = new("process-bi-level");

    /// <summary>
    /// The Printer produces monochrome output using the process color model.
    /// See: PWG 5100.13-2023 Section 6.2.27
    /// </summary>
    public static readonly PrintColorMode ProcessMonochrome = new("process-monochrome");

    public override string ToString() => Value;
    public static implicit operator string(PrintColorMode bin) => bin.Value;
    public static explicit operator PrintColorMode(string value) => new(value);
}
