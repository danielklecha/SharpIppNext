namespace SharpIpp.Protocol.Models;

/// <summary>
/// Represents one element of the printer-icc-profiles collection attribute.
/// Each element describes an ICC profile available on the printer.
/// See: PWG 5100.13-2023 Section 6.5.34
/// </summary>
public class PrinterIccProfile : IIppCollection
{
    /// <inheritdoc />
    bool IIppCollection.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((IIppCollection)this).IsValue;

    /// <summary>
    /// profile-name — human-readable name of the ICC profile (name).
    /// See: PWG 5100.13-2023 Section 6.5.34
    /// </summary>
    public string? ProfileName { get; set; }

    /// <summary>
    /// icc-profile-resource-id — resource-id of the ICC profile (integer).
    /// See: PWG 5100.13-2023 Section 6.5.34
    /// </summary>
    public int? IccProfileResourceId { get; set; }
}
