namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>material-color</c> member attribute (vendor-extensible keyword).
/// See: PWG 5100.21-2019 Section 8.1.3.3
/// </summary>
public readonly record struct MaterialColor(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>Black material color. See: PWG 5100.21-2019 Section 8.1.3.3</summary>
    public static readonly MaterialColor Black = new("black");
    /// <summary>Blue material color. See: PWG 5100.21-2019 Section 8.1.3.3</summary>
    public static readonly MaterialColor Blue = new("blue");
    /// <summary>Brown material color. See: PWG 5100.21-2019 Section 8.1.3.3</summary>
    public static readonly MaterialColor Brown = new("brown");
    /// <summary>Cyan material color. See: PWG 5100.21-2019 Section 8.1.3.3</summary>
    public static readonly MaterialColor Cyan = new("cyan");
    /// <summary>Gray material color. See: PWG 5100.21-2019 Section 8.1.3.3</summary>
    public static readonly MaterialColor Gray = new("gray");
    /// <summary>Green material color. See: PWG 5100.21-2019 Section 8.1.3.3</summary>
    public static readonly MaterialColor Green = new("green");
    /// <summary>Orange material color. See: PWG 5100.21-2019 Section 8.1.3.3</summary>
    public static readonly MaterialColor Orange = new("orange");
    /// <summary>Red material color. See: PWG 5100.21-2019 Section 8.1.3.3</summary>
    public static readonly MaterialColor Red = new("red");
    /// <summary>Translucent material color. See: PWG 5100.21-2019 Section 8.1.3.3</summary>
    public static readonly MaterialColor Translucent = new("translucent");
    /// <summary>Transparent (clear) material color. See: PWG 5100.21-2019 Section 8.1.3.3</summary>
    public static readonly MaterialColor Transparent = new("transparent");
    /// <summary>White material color. See: PWG 5100.21-2019 Section 8.1.3.3</summary>
    public static readonly MaterialColor White = new("white");
    /// <summary>Yellow material color. See: PWG 5100.21-2019 Section 8.1.3.3</summary>
    public static readonly MaterialColor Yellow = new("yellow");

    public override string ToString() => Value;
    public static implicit operator string(MaterialColor value) => value.Value;
    public static explicit operator MaterialColor(string value) => new(value);
}
