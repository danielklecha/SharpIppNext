using System;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Represents one element of the print-color-mode-icc-profiles collection attribute.
/// Each element associates a print-color-mode keyword with an ICC profile resource.
/// See: PWG 5100.13-2023 Section 6.5.24
/// </summary>
public class PrintColorModeIccProfile : IIppCollection
{
    /// <inheritdoc />
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;

    /// <summary>
    /// print-color-mode — the print color mode keyword this profile applies to.
    /// See: PWG 5100.13-2023 Section 6.5.24
    /// </summary>
    public PrintColorMode? PrintColorMode { get; set; }

    /// <summary>
    /// profile-uri — reference to the ICC color profile (uri).
    /// See: PWG 5100.13-2023 Section 6.5.24
    /// </summary>
    public Uri? ProfileUri { get; set; }
}
