namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the type of laminating.
/// See: PWG 5100.1-2022 Section 5.2.6.2
/// </summary>
public readonly record struct LaminatingType(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly LaminatingType Archival = new("archival");
    public static readonly LaminatingType ArchivalGlossy = new("archival-glossy");
    public static readonly LaminatingType ArchivalMatte = new("archival-matte");
    public static readonly LaminatingType ArchivalSemiGloss = new("archival-semi-gloss");
    public static readonly LaminatingType Glossy = new("glossy");
    public static readonly LaminatingType HighGloss = new("high-gloss");
    public static readonly LaminatingType Matte = new("matte");
    public static readonly LaminatingType SemiGloss = new("semi-gloss");
    public static readonly LaminatingType Translucent = new("translucent");
    public static readonly LaminatingType WaterResistant = new("water-resistant");

    public override string ToString() => Value;
    public static implicit operator string(LaminatingType bin) => bin.Value;
    public static explicit operator LaminatingType(string value) => new(value);
}
