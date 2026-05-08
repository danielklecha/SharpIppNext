namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the type of coating.
/// See: PWG 5100.1-2022 Section 5.2.3.2
/// </summary>
public readonly record struct CoatingType(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly CoatingType Archival = new("archival");
    public static readonly CoatingType ArchivalGlossy = new("archival-glossy");
    public static readonly CoatingType ArchivalMatte = new("archival-matte");
    public static readonly CoatingType ArchivalSemiGloss = new("archival-semi-gloss");
    public static readonly CoatingType Glossy = new("glossy");
    public static readonly CoatingType HighGloss = new("high-gloss");
    public static readonly CoatingType Matte = new("matte");
    public static readonly CoatingType SemiGloss = new("semi-gloss");
    public static readonly CoatingType Translucent = new("translucent");
    public static readonly CoatingType WaterResistant = new("water-resistant");

    public override string ToString() => Value;
    public static implicit operator string(CoatingType bin) => bin.Value;
    public static explicit operator CoatingType(string value) => new(value);
}
