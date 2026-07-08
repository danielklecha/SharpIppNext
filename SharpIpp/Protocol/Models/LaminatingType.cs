namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the type of laminating.
/// See: PWG 5100.1-2022 Section 5.2.6.2
/// </summary>
public readonly record struct LaminatingType(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>
    /// Archival-quality laminating.
    /// See: PWG 5100.1-2022 Section 5.2.6.2
    /// </summary>
    public static readonly LaminatingType Archival = new("archival");

    /// <summary>
    /// Archival-quality glossy laminating.
    /// See: PWG 5100.1-2022 Section 5.2.6.2
    /// </summary>
    public static readonly LaminatingType ArchivalGlossy = new("archival-glossy");

    /// <summary>
    /// Archival-quality matte laminating.
    /// See: PWG 5100.1-2022 Section 5.2.6.2
    /// </summary>
    public static readonly LaminatingType ArchivalMatte = new("archival-matte");

    /// <summary>
    /// Archival-quality semi-gloss laminating.
    /// See: PWG 5100.1-2022 Section 5.2.6.2
    /// </summary>
    public static readonly LaminatingType ArchivalSemiGloss = new("archival-semi-gloss");

    /// <summary>
    /// Glossy laminating.
    /// See: PWG 5100.1-2022 Section 5.2.6.2
    /// </summary>
    public static readonly LaminatingType Glossy = new("glossy");

    /// <summary>
    /// High-gloss laminating.
    /// See: PWG 5100.1-2022 Section 5.2.6.2
    /// </summary>
    public static readonly LaminatingType HighGloss = new("high-gloss");

    /// <summary>
    /// Matte laminating.
    /// See: PWG 5100.1-2022 Section 5.2.6.2
    /// </summary>
    public static readonly LaminatingType Matte = new("matte");

    /// <summary>
    /// Semi-gloss laminating.
    /// See: PWG 5100.1-2022 Section 5.2.6.2
    /// </summary>
    public static readonly LaminatingType SemiGloss = new("semi-gloss");

    /// <summary>
    /// Translucent laminating.
    /// See: PWG 5100.1-2022 Section 5.2.6.2
    /// </summary>
    public static readonly LaminatingType Translucent = new("translucent");

    /// <summary>
    /// Water-resistant laminating.
    /// See: PWG 5100.1-2022 Section 5.2.6.2
    /// </summary>
    public static readonly LaminatingType WaterResistant = new("water-resistant");

    public override string ToString() => Value;
    public static implicit operator string(LaminatingType bin) => bin.Value;
    public static implicit operator LaminatingType(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
