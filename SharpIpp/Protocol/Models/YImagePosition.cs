namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the y-image-position.
/// See: PWG 5100.3-2023 Section 5.2.21
/// </summary>
public readonly record struct YImagePosition(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>The image is centered vertically on the media. See: PWG 5100.3-2023 Section 5.2.21</summary>
    public static readonly YImagePosition Center = new("center");
    /// <summary>No vertical image positioning is applied. See: PWG 5100.3-2023 Section 5.2.21</summary>
    public static readonly YImagePosition None = new("none");
    /// <summary>The image is positioned at the bottom edge of the media. See: PWG 5100.3-2023 Section 5.2.21</summary>
    public static readonly YImagePosition Bottom = new("bottom");
    /// <summary>The image is positioned at the top edge of the media. See: PWG 5100.3-2023 Section 5.2.21</summary>
    public static readonly YImagePosition Top = new("top");

    public override string ToString() => Value;
    public static implicit operator string(YImagePosition bin) => bin.Value;
    public static implicit operator YImagePosition(string value) => new(value);
}
