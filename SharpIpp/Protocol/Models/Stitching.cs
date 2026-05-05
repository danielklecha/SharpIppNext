namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the locations of stitches, staples, or crimps.
/// See: PWG 5100.1-2022 Section 5.2.9
/// </summary>
public class Stitching : IIppCollection
{
    /// <inheritdoc />
    bool IIppCollection.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((IIppCollection)this).IsValue;

    /// <summary>
    /// integer(0:359) degrees counterclockwise
    /// See: PWG 5100.1-2022 Section 5.2.9.1
    /// </summary>
    public int? StitchingAngle { get; set; }

    /// <summary>
    /// 1setOf integer(0:MAX) in hundredths of millimeters (1/2540th of an inch)
    /// See: PWG 5100.1-2022 Section 5.2.9.2
    /// </summary>
    public int[]? StitchingLocations { get; set; }

    /// <summary>
    /// type2 keyword
    /// See: PWG 5100.1-2022 Section 5.2.9.3
    /// </summary>
    public StitchingMethod? StitchingMethod { get; set; }

    /// <summary>
    /// integer(0:MAX) in hundredths of millimeters (1/2540th of an inch)
    /// See: PWG 5100.1-2022 Section 5.2.9.4
    /// </summary>
    public int? StitchingOffset { get; set; }

    /// <summary>
    /// type1 keyword
    /// See: PWG 5100.1-2022 Section 5.2.9.5
    /// </summary>
    public FinishingReferenceEdge? StitchingReferenceEdge { get; set; }
}

