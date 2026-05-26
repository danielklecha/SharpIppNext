namespace SharpIpp.Protocol.Models;

/// <summary>
/// Represents the printer-supply collection attribute.
/// Each element describes one supply (ink, toner, paper, etc.) installed in the printer.
/// See: PWG 5100.13-2023 Section 6.6.11
/// </summary>
public class PrinterSupply : IIppCollection
{
    /// <inheritdoc />
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;

    /// <summary>
    /// type — keyword describing the supply type (e.g., "toner", "ink", "paper").
    /// See: PWG 5100.13-2023 Section 6.6.11
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// level — current fill level of the supply (integer, -2 = unknown, -3 = no-value).
    /// See: PWG 5100.13-2023 Section 6.6.11
    /// </summary>
    public int? Level { get; set; }

    /// <summary>
    /// max-capacity — maximum capacity of the supply container (integer).
    /// See: PWG 5100.13-2023 Section 6.6.11
    /// </summary>
    public int? MaxCapacity { get; set; }

    /// <summary>
    /// color-name — color of the supply (keyword | name).
    /// See: PWG 5100.13-2023 Section 6.6.11
    /// </summary>
    public string? ColorName { get; set; }

    /// <summary>
    /// marker-name — human-readable name of the supply (text).
    /// See: PWG 5100.13-2023 Section 6.6.11
    /// </summary>
    public string? MarkerName { get; set; }

    /// <summary>
    /// marker-type — type of marker (keyword).
    /// See: PWG 5100.13-2023 Section 6.6.11
    /// </summary>
    public string? MarkerType { get; set; }

    /// <summary>
    /// unit — unit of measure for level (keyword).
    /// See: PWG 5100.13-2023 Section 6.6.11
    /// </summary>
    public string? Unit { get; set; }
}
