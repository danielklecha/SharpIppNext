namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the y-image-position.
/// See: PWG 5100.3-2023 Section 5.2.21
/// </summary>
public readonly record struct YImagePosition(string Value, bool IsValue = true) : ISmartEnum 
{
    public static readonly YImagePosition Center = new("center");
    public static readonly YImagePosition None = new("none");
    public static readonly YImagePosition Bottom = new("bottom");
    public static readonly YImagePosition Top = new("top");

    public override string ToString() => Value;
    public static implicit operator string(YImagePosition bin) => bin.Value;
    public static explicit operator YImagePosition(string value) => new(value);
}
