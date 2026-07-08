namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the x-image-position.
/// See: PWG 5100.3-2023 Section 5.2.17
/// </summary>
public readonly record struct XImagePosition(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>The image is centered horizontally on the media. See: PWG 5100.3-2023 Section 5.2.17</summary>
    public static readonly XImagePosition Center = new("center");
    /// <summary>No horizontal image positioning is applied. See: PWG 5100.3-2023 Section 5.2.17</summary>
    public static readonly XImagePosition None = new("none");
    /// <summary>The image is positioned at the left edge of the media. See: PWG 5100.3-2023 Section 5.2.17</summary>
    public static readonly XImagePosition Left = new("left");
    /// <summary>The image is positioned at the right edge of the media. See: PWG 5100.3-2023 Section 5.2.17</summary>
    public static readonly XImagePosition Right = new("right");

    public override string ToString() => Value;
    public static implicit operator string(XImagePosition bin) => bin.Value;
    public static implicit operator XImagePosition(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
