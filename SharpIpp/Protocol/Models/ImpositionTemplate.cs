namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>imposition-template</c> attribute.
/// See: PWG 5100.3-2023 Section 5.2.4
/// </summary>
public readonly record struct ImpositionTemplate(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum 
{
    /// <summary>
    /// Banner imposition template.
    /// See: PWG 5100.3-2023 Section 5.2.4
    /// </summary>
    public static readonly ImpositionTemplate Banner = new("banner");

    /// <summary>
    /// Compressed banner imposition template.
    /// See: PWG 5100.3-2023 Section 5.2.4
    /// </summary>
    public static readonly ImpositionTemplate BannerCompressed = new("banner-compressed");

    /// <summary>
    /// Booklet imposition template (pages arranged for folding into a booklet).
    /// See: PWG 5100.3-2023 Section 5.2.4
    /// </summary>
    public static readonly ImpositionTemplate Booklet = new("booklet");

    /// <summary>
    /// No imposition template is applied.
    /// See: PWG 5100.3-2023 Section 5.2.4
    /// </summary>
    public static readonly ImpositionTemplate None = new("none");

    /// <summary>
    /// Signature imposition template (pages arranged for signature binding).
    /// See: PWG 5100.3-2023 Section 5.2.4
    /// </summary>
    public static readonly ImpositionTemplate Signature = new("signature");

    /// <summary>
    /// Tile imposition template (multiple copies of the same page on one sheet).
    /// See: PWG 5100.3-2023 Section 5.2.4
    /// </summary>
    public static readonly ImpositionTemplate Tile = new("tile");

    // See: PWG 5100.3-2023 Section 5.2.4

    /// <summary>
    /// Position the content at the left-top of the sheet.
    /// See: PWG 5100.3-2023 Section 5.2.4
    /// </summary>
    public static readonly ImpositionTemplate PositionLeftTop = new("position_left_top");

    /// <summary>
    /// Position the content at the left-middle of the sheet.
    /// See: PWG 5100.3-2023 Section 5.2.4
    /// </summary>
    public static readonly ImpositionTemplate PositionLeftMiddle = new("position_left_middle");

    /// <summary>
    /// Position the content at the left-bottom of the sheet.
    /// See: PWG 5100.3-2023 Section 5.2.4
    /// </summary>
    public static readonly ImpositionTemplate PositionLeftBottom = new("position_left_bottom");

    /// <summary>
    /// Position the content at the center-top of the sheet.
    /// See: PWG 5100.3-2023 Section 5.2.4
    /// </summary>
    public static readonly ImpositionTemplate PositionCenterTop = new("position_center_top");

    /// <summary>
    /// Position the content at the center-middle of the sheet.
    /// See: PWG 5100.3-2023 Section 5.2.4
    /// </summary>
    public static readonly ImpositionTemplate PositionCenterMiddle = new("position_center_middle");

    /// <summary>
    /// Position the content at the center-bottom of the sheet.
    /// See: PWG 5100.3-2023 Section 5.2.4
    /// </summary>
    public static readonly ImpositionTemplate PositionCenterBottom = new("position_center_bottom");

    /// <summary>
    /// Position the content at the right-top of the sheet.
    /// See: PWG 5100.3-2023 Section 5.2.4
    /// </summary>
    public static readonly ImpositionTemplate PositionRightTop = new("position_right_top");

    /// <summary>
    /// Position the content at the right-middle of the sheet.
    /// See: PWG 5100.3-2023 Section 5.2.4
    /// </summary>
    public static readonly ImpositionTemplate PositionRightMiddle = new("position_right_middle");

    /// <summary>
    /// Position the content at the right-bottom of the sheet.
    /// See: PWG 5100.3-2023 Section 5.2.4
    /// </summary>
    public static readonly ImpositionTemplate PositionRightBottom = new("position_right_bottom");

    // See: PWG 5100.3-2023 Section 5.2.4 / 11.3

    /// <summary>
    /// Same-up 2-up on 2Ă—3.5 in media.
    /// See: PWG 5100.3-2023 Section 11.3
    /// </summary>
    public static readonly ImpositionTemplate SameUp2x2_3_5x5in = new("same-up_2_2_3.5x5in");

    /// <summary>
    /// Same-up 2-up on 104Ă—148 mm media.
    /// See: PWG 5100.3-2023 Section 11.3
    /// </summary>
    public static readonly ImpositionTemplate SameUp2x2_104x148mm = new("same-up_2_2_104x148mm");

    /// <summary>
    /// Same-up 2-up on 105Ă—148 mm media.
    /// See: PWG 5100.3-2023 Section 11.3
    /// </summary>
    public static readonly ImpositionTemplate SameUp2x2_105x148mm = new("same-up_2_2_105x148mm");

    /// <summary>
    /// Same-up 4-up on 2Ă—3.5 in media.
    /// See: PWG 5100.3-2023 Section 11.3
    /// </summary>
    public static readonly ImpositionTemplate SameUp4x3_2x3_5in = new("same-up_4_3_2x3.5in");

    public override string ToString() => Value;
    public static implicit operator string(ImpositionTemplate bin) => bin.Value;
    public static implicit operator ImpositionTemplate(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
