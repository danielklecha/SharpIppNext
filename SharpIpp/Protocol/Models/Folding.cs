namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the location and direction of a fold.
/// See: PWG 5100.1-2022 Section 5.2.6
/// </summary>
public class Folding : IIppCollection
{
    /// <inheritdoc />
    bool IIppCollection.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((IIppCollection)this).IsValue;

    /// <summary>
    /// type1 keyword
    /// See: PWG 5100.1-2022 Section 5.2.6.1
    /// </summary>
    public FoldingDirection? FoldingDirection { get; set; }

    /// <summary>
    /// integer(0:MAX) in hundredths of millimeters (1/2540th of an inch)
    /// See: PWG 5100.1-2022 Section 5.2.6.2
    /// </summary>
    public int? FoldingOffset { get; set; }

    /// <summary>
    /// type1 keyword
    /// See: PWG 5100.1-2022 Section 5.2.6.3
    /// </summary>
    public FinishingReferenceEdge? FoldingReferenceEdge { get; set; }
}

