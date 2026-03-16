namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the type of laminating.
/// See: PWG 5100.1-2022 Section 5.2.6.2
/// </summary>
public readonly record struct LaminatingType(string Value)
{
    public static readonly LaminatingType Archival = new("archival");
    public static readonly LaminatingType Glossy = new("glossy");
    public static readonly LaminatingType HighGloss = new("high-gloss");
    public static readonly LaminatingType Matte = new("matte");
    public static readonly LaminatingType SemiGloss = new("semi-gloss");
    public static readonly LaminatingType Thermal = new("thermal");

    public override string ToString() => Value;
    public static implicit operator string(LaminatingType bin) => bin.Value;
    public static explicit operator LaminatingType(string value) => new(value);
}
