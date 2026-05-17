namespace SharpIpp.Protocol.Models;

/// <summary>
/// Represents one element of the print-color-mode-icc-profiles collection attribute.
/// Each element associates a print-color-mode keyword with an ICC profile resource.
/// See: PWG 5100.13-2023 Section 6.5.24
/// </summary>
public class PrintColorModeIccProfile : IIppCollection
{
    /// <inheritdoc />
    bool IIppCollection.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((IIppCollection)this).IsValue;

    /// <summary>
    /// print-color-mode — the print color mode keyword this profile applies to.
    /// See: PWG 5100.13-2023 Section 6.5.24
    /// </summary>
    public string? PrintColorMode { get; set; }

    /// <summary>
    /// icc-profile-resource-id — resource-id of the ICC profile (integer).
    /// See: PWG 5100.13-2023 Section 6.5.24
    /// </summary>
    public int? IccProfileResourceId { get; set; }
}
