namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the cover type.
/// See: PWG 5100.3-2023 Section 5.2.1.3
/// </summary>
public readonly record struct CoverType(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// No cover sheet is added.
    /// See: PWG 5100.3-2023 Section 5.2.1.3
    /// </summary>
    public static readonly CoverType NoCover = new("no-cover");

    /// <summary>
    /// A cover sheet is added and printed on the back side.
    /// See: PWG 5100.3-2023 Section 5.2.1.3
    /// </summary>
    public static readonly CoverType PrintBack = new("print-back");

    /// <summary>
    /// A cover sheet is added and printed on both sides.
    /// See: PWG 5100.3-2023 Section 5.2.1.3
    /// </summary>
    public static readonly CoverType PrintBoth = new("print-both");

    /// <summary>
    /// A cover sheet is added and printed on the front side.
    /// See: PWG 5100.3-2023 Section 5.2.1.3
    /// </summary>
    public static readonly CoverType PrintFront = new("print-front");

    /// <summary>
    /// A cover sheet is added but not printed.
    /// See: PWG 5100.3-2023 Section 5.2.1.3
    /// </summary>
    public static readonly CoverType PrintNone = new("print-none");

    public override string ToString() => Value;
    public static implicit operator string(CoverType bin) => bin.Value;
    public static explicit operator CoverType(string value) => new(value);
}
