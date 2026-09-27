using System;
using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Represents one element of the print-color-mode-icc-profiles collection attribute.
/// Each element associates a print-color-mode keyword with an ICC profile resource.
/// See: PWG 5100.13-2023 Section 6.5.24
/// </summary>
[IppAttribute(IppAttributeNames.PrintColorModeIccProfiles)]
public class PrintColorModeIccProfile : IIppCollection
{
    /// <inheritdoc />

    /// <summary>
    /// print-color-mode — the print color mode keyword this profile applies to.
    /// See: PWG 5100.13-2023 Section 6.5.24
    /// </summary>
    public IppValue<PrintColorMode>? PrintColorMode { get; set; }

    /// <summary>
    /// profile-uri — reference to the ICC color profile (uri).
    /// See: PWG 5100.13-2023 Section 6.5.24
    /// </summary>
    public IppValue<Uri>? ProfileUri { get; set; }
}
