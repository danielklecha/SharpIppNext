namespace SharpIpp.Protocol.Models;

/// <summary>
/// Represents the printer-input-tray collection attribute.
/// Each element describes one input tray installed in the printer.
/// See: PWG 5100.13-2023 Section 6.6.9
/// </summary>
public class PrinterInputTray : IIppCollection
{
    /// <inheritdoc />
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;

    /// <summary>
    /// type — keyword describing the tray type (e.g., "sheetFeedAutoRemovableTray").
    /// See: PWG 5100.13-2023 Section 6.6.9
    /// </summary>
    public InputTrayType? Type { get; set; }

    /// <summary>
    /// level — current fill level of the tray (integer, -2 = unknown, -3 = no-value).
    /// See: PWG 5100.13-2023 Section 6.6.9
    /// </summary>
    public int? Level { get; set; }

    /// <summary>
    /// status — current status of the tray (keyword).
    /// See: PWG 5100.13-2023 Section 6.6.9
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// media-size-x — width of the media in the tray (integer, hundredths of mm).
    /// See: PWG 5100.13-2023 Section 6.6.9
    /// </summary>
    public int? MediaSizeX { get; set; }

    /// <summary>
    /// media-size-y — height of the media in the tray (integer, hundredths of mm).
    /// See: PWG 5100.13-2023 Section 6.6.9
    /// </summary>
    public int? MediaSizeY { get; set; }

    /// <summary>
    /// media-color — color of the media in the tray (keyword | name).
    /// See: PWG 5100.13-2023 Section 6.6.9
    /// </summary>
    public MediaColor? MediaColor { get; set; }

    /// <summary>
    /// media-info — human-readable description of the media in the tray (text).
    /// See: PWG 5100.13-2023 Section 6.6.9
    /// </summary>
    public string? MediaInfo { get; set; }

    /// <summary>
    /// media-type — type of media in the tray (keyword | name).
    /// See: PWG 5100.13-2023 Section 6.6.9
    /// </summary>
    public MediaType? MediaType { get; set; }

    /// <summary>
    /// unit — unit of measure for level (keyword).
    /// See: PWG 5100.13-2023 Section 6.6.9
    /// </summary>
    public CapacityUnit? Unit { get; set; }

    /// <summary>
    /// feed-orientation — orientation of media feed (keyword).
    /// See: PWG 5100.13-2023 Section 6.6.9
    /// </summary>
    public FeedOrientation? FeedOrientation { get; set; }
}
