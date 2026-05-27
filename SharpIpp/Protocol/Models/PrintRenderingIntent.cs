namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>print-rendering-intent</c> keyword values.
/// See: PWG 5100.13-2023 Section 6.2.4
/// </summary>
public readonly record struct PrintRenderingIntent(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// Absolute colorimetric rendering intent; colors outside the gamut are clipped.
    /// See: PWG 5100.13-2023 Section 6.2.4
    /// </summary>
    public static readonly PrintRenderingIntent Absolute = new("absolute");

    /// <summary>
    /// The Printer automatically selects the rendering intent based on the document content.
    /// See: PWG 5100.13-2023 Section 6.2.4
    /// </summary>
    public static readonly PrintRenderingIntent Auto = new("auto");

    /// <summary>
    /// Perceptual rendering intent; the entire gamut is compressed or expanded to fit the output gamut.
    /// See: PWG 5100.13-2023 Section 6.2.4
    /// </summary>
    public static readonly PrintRenderingIntent Perceptual = new("perceptual");

    /// <summary>
    /// Relative colorimetric rendering intent; colors outside the gamut are mapped to the nearest in-gamut color.
    /// See: PWG 5100.13-2023 Section 6.2.4
    /// </summary>
    public static readonly PrintRenderingIntent Relative = new("relative");

    /// <summary>
    /// Relative colorimetric rendering intent with black point compensation.
    /// See: PWG 5100.13-2023 Section 6.2.4
    /// </summary>
    public static readonly PrintRenderingIntent RelativeBpc = new("relative-bpc");

    /// <summary>
    /// Saturation rendering intent; vivid colors are preserved at the expense of accuracy.
    /// See: PWG 5100.13-2023 Section 6.2.4
    /// </summary>
    public static readonly PrintRenderingIntent Saturation = new("saturation");

    public override string ToString() => Value;
    public static implicit operator string(PrintRenderingIntent value) => value.Value;
    public static implicit operator PrintRenderingIntent(string value) => new(value);
}
