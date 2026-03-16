namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the media-type attribute.
/// See: PWG 5101.1
/// </summary>
public readonly record struct MediaType(string Value)
{
    public static readonly MediaType Stationery = new("stationery");
    public static readonly MediaType Transparency = new("transparency");
    public static readonly MediaType Envelope = new("envelope");
    public static readonly MediaType Cardstock = new("cardstock");
    public static readonly MediaType Photographic = new("photographic");
    public static readonly MediaType PhotographicGlossy = new("photographic-glossy");
    public static readonly MediaType PhotographicMatte = new("photographic-matte");
    public static readonly MediaType PhotographicSatin = new("photographic-satin");
    public static readonly MediaType PhotographicSemiGloss = new("photographic-semi-gloss");
    public static readonly MediaType Labels = new("labels");
    public static readonly MediaType Other = new("other");
    public static readonly MediaType MultiLayer = new("multi-layer");

    public override string ToString() => Value;
    public static implicit operator string(MediaType bin) => bin.Value;
    public static explicit operator MediaType(string value) => new(value);
}
