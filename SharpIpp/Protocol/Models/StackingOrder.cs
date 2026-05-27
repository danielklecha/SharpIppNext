namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the stacking order of output.
/// See: PWG 5100.13-2023 Section 6.6.10
/// </summary>
public readonly record struct StackingOrder(string Value, bool IsValue = true) : ISmartEnum
{
    public static readonly StackingOrder FirstToLast = new("firstToLast");
    public static readonly StackingOrder LastToFirst = new("lastToFirst");

    public override string ToString() => Value;
    public static implicit operator string(StackingOrder bin) => bin.Value;
    public static implicit operator StackingOrder(string value) => new(value);
}
