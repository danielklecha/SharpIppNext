namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the type of coating.
/// See: PWG 5100.1-2022 Section 5.2.3.2
/// </summary>
public readonly record struct CoatingType(string Value)
{
    public static readonly CoatingType Archival = new("archival");
    public static readonly CoatingType Glossy = new("glossy");
    public static readonly CoatingType HighGloss = new("high-gloss");
    public static readonly CoatingType Matte = new("matte");
    public static readonly CoatingType SemiGloss = new("semi-gloss");
    public static readonly CoatingType Silicone = new("silicone");
    public static readonly CoatingType Thermographic = new("thermographic");
    public static readonly CoatingType UV = new("uv");
    public static readonly CoatingType Wax = new("wax");

    public override string ToString() => Value;
    public static implicit operator string(CoatingType bin) => bin.Value;
    public static explicit operator CoatingType(string value) => new(value);
}
