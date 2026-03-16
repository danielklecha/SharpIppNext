namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the x-image-position.
/// See: PWG 5100.1-2022 Section 6.27
/// </summary>
public readonly record struct XImagePosition(string Value)
{
    public static readonly XImagePosition Center = new("center");
    public static readonly XImagePosition None = new("none");
    public static readonly XImagePosition Left = new("left");
    public static readonly XImagePosition Right = new("right");

    public override string ToString() => Value;
    public static implicit operator string(XImagePosition bin) => bin.Value;
    public static explicit operator XImagePosition(string value) => new(value);
}
