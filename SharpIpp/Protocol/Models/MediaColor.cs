namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the media-color attribute.
/// See: PWG 5101.1
/// </summary>
public readonly record struct MediaColor(string Value, bool IsValue = true) : ISmartEnum 
{
    public static readonly MediaColor White = new("white");
    public static readonly MediaColor Black = new("black");
    public static readonly MediaColor Blue = new("blue");
    public static readonly MediaColor Green = new("green");
    public static readonly MediaColor Pink = new("pink");
    public static readonly MediaColor Yellow = new("yellow");
    public static readonly MediaColor Buff = new("buff");
    public static readonly MediaColor Goldenrod = new("goldenrod");
    public static readonly MediaColor Gray = new("gray");
    public static readonly MediaColor Ivory = new("ivory");
    public static readonly MediaColor Orange = new("orange");
    public static readonly MediaColor Red = new("red");
    public static readonly MediaColor Silver = new("silver");
    public static readonly MediaColor Gold = new("gold");
    public static readonly MediaColor MultiColor = new("multi-color");
    public static readonly MediaColor NoColor = new("no-color");
    public static readonly MediaColor Transparent = new("transparent");

    public override string ToString() => Value;
    public static implicit operator string(MediaColor bin) => bin.Value;
    public static explicit operator MediaColor(string value) => new(value);
}
