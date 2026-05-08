namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>imposition-template</c> attribute.
/// See: PWG 5100.3-2023 Section 5.2.4
/// </summary>
public readonly record struct ImpositionTemplate(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum 
{
    public static readonly ImpositionTemplate Banner = new("banner");
    public static readonly ImpositionTemplate BannerCompressed = new("banner-compressed");
    public static readonly ImpositionTemplate Booklet = new("booklet");
    public static readonly ImpositionTemplate None = new("none");
    public static readonly ImpositionTemplate Signature = new("signature");
    public static readonly ImpositionTemplate Tile = new("tile");

    // See: PWG 5100.3-2023 Section 5.2.4
    public static readonly ImpositionTemplate PositionLeftTop = new("position_left_top");
    public static readonly ImpositionTemplate PositionLeftMiddle = new("position_left_middle");
    public static readonly ImpositionTemplate PositionLeftBottom = new("position_left_bottom");
    public static readonly ImpositionTemplate PositionCenterTop = new("position_center_top");
    public static readonly ImpositionTemplate PositionCenterMiddle = new("position_center_middle");
    public static readonly ImpositionTemplate PositionCenterBottom = new("position_center_bottom");
    public static readonly ImpositionTemplate PositionRightTop = new("position_right_top");
    public static readonly ImpositionTemplate PositionRightMiddle = new("position_right_middle");
    public static readonly ImpositionTemplate PositionRightBottom = new("position_right_bottom");

    // See: PWG 5100.3-2023 Section 5.2.4 / 11.3
    public static readonly ImpositionTemplate SameUp2x2_3_5x5in = new("same-up_2_2_3.5x5in");
    public static readonly ImpositionTemplate SameUp2x2_104x148mm = new("same-up_2_2_104x148mm");
    public static readonly ImpositionTemplate SameUp2x2_105x148mm = new("same-up_2_2_105x148mm");
    public static readonly ImpositionTemplate SameUp4x3_2x3_5in = new("same-up_4_3_2x3.5in");

    public override string ToString() => Value;
    public static implicit operator string(ImpositionTemplate bin) => bin.Value;
    public static explicit operator ImpositionTemplate(string value) => new(value);
}
