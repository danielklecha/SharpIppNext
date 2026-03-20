namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>print-rendering-intent</c> keyword values.
/// See: PWG 5100.13-2023 Section 6.2.4
/// </summary>
public readonly record struct PrintRenderingIntent(string Value)
{
    public static readonly PrintRenderingIntent Absolute = new("absolute");
    public static readonly PrintRenderingIntent Auto = new("auto");
    public static readonly PrintRenderingIntent Perceptual = new("perceptual");
    public static readonly PrintRenderingIntent Relative = new("relative");
    public static readonly PrintRenderingIntent RelativeBpc = new("relative-bpc");
    public static readonly PrintRenderingIntent Saturation = new("saturation");

    public override string ToString() => Value;
    public static implicit operator string(PrintRenderingIntent value) => value.Value;
    public static explicit operator PrintRenderingIntent(string value) => new(value);
}
