namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>input-content-type</c> member attribute.
/// See: PWG 5100.15-2013 Section 7.1.1.6
/// </summary>
public readonly record struct InputContentType(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>
    /// The scanner automatically selects the content type optimization.
    /// See: PWG 5100.15-2013 Section 7.1.1.6
    /// </summary>
    public static readonly InputContentType Auto = new("auto");

    /// <summary>
    /// The content is a halftone image (e.g., a newspaper photograph).
    /// See: PWG 5100.15-2013 Section 7.1.1.6
    /// </summary>
    public static readonly InputContentType Halftone = new("halftone");

    /// <summary>
    /// The content is line art (e.g., drawings or diagrams).
    /// See: PWG 5100.15-2013 Section 7.1.1.6
    /// </summary>
    public static readonly InputContentType LineArt = new("line-art");

    /// <summary>
    /// The content is from a magazine (mixed text and halftone images).
    /// See: PWG 5100.15-2013 Section 7.1.1.6
    /// </summary>
    public static readonly InputContentType Magazine = new("magazine");

    /// <summary>
    /// The content is a photograph.
    /// See: PWG 5100.15-2013 Section 7.1.1.6
    /// </summary>
    public static readonly InputContentType Photo = new("photo");

    /// <summary>
    /// The content is primarily text.
    /// See: PWG 5100.15-2013 Section 7.1.1.6
    /// </summary>
    public static readonly InputContentType Text = new("text");

    /// <summary>
    /// The content is a mix of text and photographs.
    /// See: PWG 5100.15-2013 Section 7.1.1.6
    /// </summary>
    public static readonly InputContentType TextAndPhoto = new("text-and-photo");

    public override string ToString() => Value;
    public static implicit operator string(InputContentType value) => value.Value;
    public static explicit operator InputContentType(string value) => new(value);
}
