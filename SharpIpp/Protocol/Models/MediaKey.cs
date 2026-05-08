namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the media-key member attribute.
/// See: PWG 5100.13-2023 Section 6.1.13
/// </summary>
public readonly record struct MediaKey(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public override string ToString() => Value;
    public static implicit operator string(MediaKey bin) => bin.Value;
    public static explicit operator MediaKey(string value) => new(value);
}
