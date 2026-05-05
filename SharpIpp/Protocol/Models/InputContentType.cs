namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>input-content-type</c> member attribute.
/// See: PWG 5100.15-2013 Section 7.1.1.6
/// </summary>
public readonly record struct InputContentType(string Value, bool IsKeyword = true, bool IsValue = true) : IKeywordSmartEnum
{
    public static readonly InputContentType Auto = new("auto");
    public static readonly InputContentType Halftone = new("halftone");
    public static readonly InputContentType LineArt = new("line-art");
    public static readonly InputContentType Magazine = new("magazine");
    public static readonly InputContentType Photo = new("photo");
    public static readonly InputContentType Text = new("text");
    public static readonly InputContentType TextAndPhoto = new("text-and-photo");

    public override string ToString() => Value;
    public static implicit operator string(InputContentType value) => value.Value;
    public static explicit operator InputContentType(string value) => new(value);
}
