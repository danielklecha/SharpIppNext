namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the name of the covering.
/// See: PWG 5100.1-2022 Section 5.2.4.1
/// </summary>
public readonly record struct CoveringName(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly CoveringName Plain = new("plain");
    public static readonly CoveringName PreCut = new("pre-cut");
    public static readonly CoveringName PrePrinted = new("pre-printed");

    public override string ToString() => Value;
    public static implicit operator string(CoveringName bin) => bin.Value;
    public static explicit operator CoveringName(string value) => new(value);
}
