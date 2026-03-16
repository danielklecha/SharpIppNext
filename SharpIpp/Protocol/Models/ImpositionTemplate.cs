namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the imposition-template attribute.
/// See: PWG 5100.3
/// </summary>
public readonly record struct ImpositionTemplate(string Value)
{
    public static readonly ImpositionTemplate None = new("none");

    public override string ToString() => Value;
    public static implicit operator string(ImpositionTemplate bin) => bin.Value;
    public static explicit operator ImpositionTemplate(string value) => new(value);
}
