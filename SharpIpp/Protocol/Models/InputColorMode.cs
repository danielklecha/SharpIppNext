namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>input-color-mode</c> member attribute.
/// See: PWG 5100.15-2013 Section 7.1.1.5
/// </summary>
public readonly record struct InputColorMode(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>
    /// The scanner automatically selects the color mode.
    /// See: PWG 5100.15-2013 Section 7.1.1.5
    /// </summary>
    public static readonly InputColorMode Auto = new("auto");

    /// <summary>
    /// The scanner captures bi-level (black and white only) images.
    /// See: PWG 5100.15-2013 Section 7.1.1.5
    /// </summary>
    public static readonly InputColorMode BiLevel = new("bi-level");

    /// <summary>
    /// The scanner captures full color images.
    /// See: PWG 5100.15-2013 Section 7.1.1.5
    /// </summary>
    public static readonly InputColorMode Color = new("color");

    /// <summary>
    /// The scanner captures monochrome (grayscale) images.
    /// See: PWG 5100.15-2013 Section 7.1.1.5
    /// </summary>
    public static readonly InputColorMode Monochrome = new("monochrome");

    /// <summary>
    /// The scanner captures bi-level images using the process color model.
    /// See: PWG 5100.15-2013 Section 7.1.1.5
    /// </summary>
    public static readonly InputColorMode ProcessBiLevel = new("process-bi-level");

    /// <summary>
    /// The scanner captures monochrome images using the process color model.
    /// See: PWG 5100.15-2013 Section 7.1.1.5
    /// </summary>
    public static readonly InputColorMode ProcessMonochrome = new("process-monochrome");

    public override string ToString() => Value;
    public static implicit operator string(InputColorMode value) => value.Value;
    public static implicit operator InputColorMode(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
