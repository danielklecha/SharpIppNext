namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>material-color</c> member attribute (vendor-extensible keyword).
/// See: PWG 5100.21-2019 Section 6.8.11
/// </summary>
public readonly record struct MaterialColor(string Value, bool IsKeyword = true, bool IsValue = true) : IKeywordSmartEnum
{
    public static readonly MaterialColor Black = new("black");
    public static readonly MaterialColor Blue = new("blue");
    public static readonly MaterialColor Brown = new("brown");
    public static readonly MaterialColor Cyan = new("cyan");
    public static readonly MaterialColor Gray = new("gray");
    public static readonly MaterialColor Green = new("green");
    public static readonly MaterialColor Orange = new("orange");
    public static readonly MaterialColor Red = new("red");
    public static readonly MaterialColor Translucent = new("translucent");
    public static readonly MaterialColor Transparent = new("transparent");
    public static readonly MaterialColor White = new("white");
    public static readonly MaterialColor Yellow = new("yellow");

    public override string ToString() => Value;
    public static implicit operator string(MaterialColor value) => value.Value;
    public static explicit operator MaterialColor(string value) => new(value);
}
