namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the media-color attribute.
/// See: PWG 5101.1
/// </summary>
public readonly record struct MediaColor(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// White media.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaColor White = new("white");

    /// <summary>
    /// Black media.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaColor Black = new("black");

    /// <summary>
    /// Blue media.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaColor Blue = new("blue");

    /// <summary>
    /// Green media.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaColor Green = new("green");

    /// <summary>
    /// Pink media.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaColor Pink = new("pink");

    /// <summary>
    /// Yellow media.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaColor Yellow = new("yellow");

    /// <summary>
    /// Buff (light tan) media.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaColor Buff = new("buff");

    /// <summary>
    /// Goldenrod (deep yellow) media.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaColor Goldenrod = new("goldenrod");

    /// <summary>
    /// Gray media.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaColor Gray = new("gray");

    /// <summary>
    /// Ivory (off-white) media.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaColor Ivory = new("ivory");

    /// <summary>
    /// Orange media.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaColor Orange = new("orange");

    /// <summary>
    /// Red media.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaColor Red = new("red");

    /// <summary>
    /// Silver media.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaColor Silver = new("silver");

    /// <summary>
    /// Gold media.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaColor Gold = new("gold");

    /// <summary>
    /// Multi-color media (e.g., pre-printed with multiple colors).
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaColor MultiColor = new("multi-color");

    /// <summary>
    /// Media with no specific color designation.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaColor NoColor = new("no-color");

    /// <summary>
    /// Transparent (clear) media.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaColor Transparent = new("transparent");

    public override string ToString() => Value;
    public static implicit operator string(MediaColor bin) => bin.Value;
    public static implicit operator MediaColor(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
