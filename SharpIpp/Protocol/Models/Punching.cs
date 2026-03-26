namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the locations of holes to make in the hardcopy output.
/// See: PWG 5100.1-2022 Section 5.2.8
/// </summary>
public class Punching : IIppCollection
{
    /// <inheritdoc />
    bool IIppCollection.IsNoValue { get; set; }

    /// <summary>
    /// 1setOf integer(0:MAX) in hundredths of millimeters (1/2540th of an inch)
    /// See: PWG 5100.1-2022 Section 5.2.8.1
    /// </summary>
    public int[]? PunchingLocations { get; set; }

    /// <summary>
    /// integer(0:MAX) in hundredths of millimeters (1/2540th of an inch)
    /// See: PWG 5100.1-2022 Section 5.2.8.2
    /// </summary>
    public int? PunchingOffset { get; set; }

    /// <summary>
    /// type1 keyword
    /// See: PWG 5100.1-2022 Section 5.2.8.3
    /// </summary>
    public FinishingReferenceEdge? PunchingReferenceEdge { get; set; }
}

