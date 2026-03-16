namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the print-content-optimize.
/// See: PWG 5100.1-2022 Section 6.20
/// </summary>
public readonly record struct PrintContentOptimize(string Value)
{
    public static readonly PrintContentOptimize Auto = new("auto");
    public static readonly PrintContentOptimize Graphic = new("graphic");
    public static readonly PrintContentOptimize Photo = new("photo");
    public static readonly PrintContentOptimize Text = new("text");
    public static readonly PrintContentOptimize TextAndGraphic = new("text-and-graphic");

    public override string ToString() => Value;
    public static implicit operator string(PrintContentOptimize bin) => bin.Value;
    public static explicit operator PrintContentOptimize(string value) => new(value);
}
