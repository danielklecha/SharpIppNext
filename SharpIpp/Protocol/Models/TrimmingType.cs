namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the type of trimming to perform.
/// See: PWG 5100.1-2022 Section 6.30
/// </summary>
public readonly record struct TrimmingType(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>
    /// Draw a line at the trim position without cutting.
    /// See: PWG 5100.1-2022 Section 6.30
    /// </summary>
    public static readonly TrimmingType DrawLine = new("draw-line");

    /// <summary>
    /// Full cut through the media.
    /// See: PWG 5100.1-2022 Section 6.30
    /// </summary>
    public static readonly TrimmingType Full = new("full");

    /// <summary>
    /// Partial cut through the media (not all the way through).
    /// See: PWG 5100.1-2022 Section 6.30
    /// </summary>
    public static readonly TrimmingType Partial = new("partial");

    /// <summary>
    /// Perforate the media at the trim position.
    /// See: PWG 5100.1-2022 Section 6.30
    /// </summary>
    public static readonly TrimmingType Perforate = new("perforate");

    /// <summary>
    /// Score (crease) the media at the trim position.
    /// See: PWG 5100.1-2022 Section 6.30
    /// </summary>
    public static readonly TrimmingType Score = new("score");

    /// <summary>
    /// Create a tab at the trim position.
    /// See: PWG 5100.1-2022 Section 6.30
    /// </summary>
    public static readonly TrimmingType Tab = new("tab");

    public override string ToString() => Value;
    public static implicit operator string(TrimmingType bin) => bin.Value;
    public static implicit operator TrimmingType(string value) => new(value);
}
