namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies whether sheets are pushed outward or pulled inward for the fold.
/// See: PWG 5100.1-2022 Section 5.2.6.1
/// </summary>
public readonly record struct FoldingDirection(string Value)
{
    public static readonly FoldingDirection Inward = new("inward");
    public static readonly FoldingDirection Outward = new("outward");

    public override string ToString() => Value;
    public static implicit operator string(FoldingDirection bin) => bin.Value;
    public static explicit operator FoldingDirection(string value) => new(value);
}
