namespace SharpIpp.Protocol.Models;

/// <summary>
/// Finishing Reference Edge for finishing operations.
/// See: PWG 5100.1-2022 Section 5.2
/// </summary>
public readonly record struct FinishingReferenceEdge(string Value)
{
    public static readonly FinishingReferenceEdge Bottom = new("bottom");
    public static readonly FinishingReferenceEdge Left = new("left");
    public static readonly FinishingReferenceEdge Right = new("right");
    public static readonly FinishingReferenceEdge Top = new("top");

    public override string ToString() => Value;
    public static implicit operator string(FinishingReferenceEdge bin) => bin.Value;
    public static explicit operator FinishingReferenceEdge(string value) => new(value);
}
