namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies member attribute names supported by <code>cover-back-supported</code> and <code>cover-front-supported</code>.
/// See: PWG 5100.3-2023 Sections 5.3.2 and 5.3.4
/// </summary>
public readonly record struct CoverMember(string Value, bool IsValue = true) : ISmartEnum 
{
    public static readonly CoverMember CoverType = new("cover-type");
    public static readonly CoverMember Media = new("media");
    public static readonly CoverMember MediaCol = new("media-col");

    public override string ToString() => Value;
    public static implicit operator string(CoverMember value) => value.Value;
    public static explicit operator CoverMember(string value) => new(value);
}
