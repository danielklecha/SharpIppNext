namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the media-type attribute.
/// See: PWG 5101.1
/// </summary>
public readonly record struct MediaType(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// Standard office stationery (plain paper).
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaType Stationery = new("stationery");

    /// <summary>
    /// Transparency film for overhead projectors.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaType Transparency = new("transparency");

    /// <summary>
    /// Envelope media.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaType Envelope = new("envelope");

    /// <summary>
    /// Heavy cardstock media.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaType Cardstock = new("cardstock");

    /// <summary>
    /// Photographic media (generic).
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaType Photographic = new("photographic");

    /// <summary>
    /// Glossy photographic media.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaType PhotographicGlossy = new("photographic-glossy");

    /// <summary>
    /// Matte photographic media.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaType PhotographicMatte = new("photographic-matte");

    /// <summary>
    /// Satin photographic media.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaType PhotographicSatin = new("photographic-satin");

    /// <summary>
    /// Semi-gloss photographic media.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaType PhotographicSemiGloss = new("photographic-semi-gloss");

    /// <summary>
    /// Label media (self-adhesive).
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaType Labels = new("labels");

    /// <summary>
    /// Other media type not covered by the standard values.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaType Other = new("other");

    /// <summary>
    /// Multi-layer media (e.g., carbonless copy paper).
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaType MultiLayer = new("multi-layer");

    public override string ToString() => Value;
    public static implicit operator string(MediaType bin) => bin.Value;
    public static implicit operator MediaType(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
