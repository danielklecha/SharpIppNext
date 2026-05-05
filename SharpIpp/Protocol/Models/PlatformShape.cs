namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>platform-shape</c> attribute.
/// See: PWG 5100.21-2019 Section 8.3.23
/// </summary>
public readonly record struct PlatformShape(string Value, bool IsKeyword = true, bool IsValue = true) : IKeywordSmartEnum
{
    public static readonly PlatformShape Round = new("round");
    public static readonly PlatformShape Square = new("square");

    public override string ToString() => Value;
    public static implicit operator string(PlatformShape value) => value.Value;
    public static explicit operator PlatformShape(string value) => new(value);
}
