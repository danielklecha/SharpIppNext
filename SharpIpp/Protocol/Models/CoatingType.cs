namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the type of coating.
/// See: PWG 5100.1-2022 Section 5.2.3.2
/// </summary>
public readonly record struct CoatingType(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>
    /// Archival-quality coating.
    /// See: PWG 5100.1-2022 Section 5.2.3.2
    /// </summary>
    public static readonly CoatingType Archival = new("archival");

    /// <summary>
    /// Archival-quality glossy coating.
    /// See: PWG 5100.1-2022 Section 5.2.3.2
    /// </summary>
    public static readonly CoatingType ArchivalGlossy = new("archival-glossy");

    /// <summary>
    /// Archival-quality matte coating.
    /// See: PWG 5100.1-2022 Section 5.2.3.2
    /// </summary>
    public static readonly CoatingType ArchivalMatte = new("archival-matte");

    /// <summary>
    /// Archival-quality semi-gloss coating.
    /// See: PWG 5100.1-2022 Section 5.2.3.2
    /// </summary>
    public static readonly CoatingType ArchivalSemiGloss = new("archival-semi-gloss");

    /// <summary>
    /// Glossy coating.
    /// See: PWG 5100.1-2022 Section 5.2.3.2
    /// </summary>
    public static readonly CoatingType Glossy = new("glossy");

    /// <summary>
    /// High-gloss coating.
    /// See: PWG 5100.1-2022 Section 5.2.3.2
    /// </summary>
    public static readonly CoatingType HighGloss = new("high-gloss");

    /// <summary>
    /// Matte coating.
    /// See: PWG 5100.1-2022 Section 5.2.3.2
    /// </summary>
    public static readonly CoatingType Matte = new("matte");

    /// <summary>
    /// Semi-gloss coating.
    /// See: PWG 5100.1-2022 Section 5.2.3.2
    /// </summary>
    public static readonly CoatingType SemiGloss = new("semi-gloss");

    /// <summary>
    /// Translucent coating.
    /// See: PWG 5100.1-2022 Section 5.2.3.2
    /// </summary>
    public static readonly CoatingType Translucent = new("translucent");

    /// <summary>
    /// Water-resistant coating.
    /// See: PWG 5100.1-2022 Section 5.2.3.2
    /// </summary>
    public static readonly CoatingType WaterResistant = new("water-resistant");

    public override string ToString() => Value;
    public static implicit operator string(CoatingType bin) => bin.Value;
    public static implicit operator CoatingType(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
