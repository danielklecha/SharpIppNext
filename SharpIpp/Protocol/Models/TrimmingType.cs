namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the type of trimming to perform.
/// See: PWG 5100.1-2022 Section 6.30
/// </summary>
public readonly record struct TrimmingType(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly TrimmingType DrawLine = new("draw-line");
    public static readonly TrimmingType Full = new("full");
    public static readonly TrimmingType Partial = new("partial");
    public static readonly TrimmingType Perforate = new("perforate");
    public static readonly TrimmingType Score = new("score");
    public static readonly TrimmingType Tab = new("tab");

    public override string ToString() => Value;
    public static implicit operator string(TrimmingType bin) => bin.Value;
    public static explicit operator TrimmingType(string value) => new(value);
}
