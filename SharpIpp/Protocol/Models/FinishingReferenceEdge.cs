namespace SharpIpp.Protocol.Models;

/// <summary>
/// Finishing Reference Edge for finishing operations.
/// See: PWG 5100.1-2022 Section 5.2
/// </summary>
public readonly record struct FinishingReferenceEdge(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// The bottom edge of the media sheet is the reference edge for finishing.
    /// See: PWG 5100.1-2022 Section 5.2
    /// </summary>
    public static readonly FinishingReferenceEdge Bottom = new("bottom");

    /// <summary>
    /// The left edge of the media sheet is the reference edge for finishing.
    /// See: PWG 5100.1-2022 Section 5.2
    /// </summary>
    public static readonly FinishingReferenceEdge Left = new("left");

    /// <summary>
    /// The right edge of the media sheet is the reference edge for finishing.
    /// See: PWG 5100.1-2022 Section 5.2
    /// </summary>
    public static readonly FinishingReferenceEdge Right = new("right");

    /// <summary>
    /// The top edge of the media sheet is the reference edge for finishing.
    /// See: PWG 5100.1-2022 Section 5.2
    /// </summary>
    public static readonly FinishingReferenceEdge Top = new("top");

    public override string ToString() => Value;
    public static implicit operator string(FinishingReferenceEdge bin) => bin.Value;
    public static implicit operator FinishingReferenceEdge(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
