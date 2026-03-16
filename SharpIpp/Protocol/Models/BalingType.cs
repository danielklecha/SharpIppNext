namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the type of baling.
/// See: PWG 5100.1-2022 Section 5.2.1.1
/// </summary>
public readonly record struct BalingType(string Value)
{
    public static readonly BalingType Band = new("band");
    public static readonly BalingType ShrinkWrap = new("shrink-wrap");
    public static readonly BalingType Wrap = new("wrap");

    public override string ToString() => Value;
    public static implicit operator string(BalingType bin) => bin.Value;
    public static explicit operator BalingType(string value) => new(value);
}
