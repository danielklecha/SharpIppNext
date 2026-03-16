namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the cover type.
/// See: PWG 5100.1-2022 Section 6.5
/// </summary>
public readonly record struct CoverType(string Value)
{
    public static readonly CoverType NoCover = new("no-cover");
    public static readonly CoverType PrintBack = new("print-back");
    public static readonly CoverType PrintBoth = new("print-both");
    public static readonly CoverType PrintFront = new("print-front");
    public static readonly CoverType PrintNone = new("print-none");

    public override string ToString() => Value;
    public static implicit operator string(CoverType bin) => bin.Value;
    public static explicit operator CoverType(string value) => new(value);
}
