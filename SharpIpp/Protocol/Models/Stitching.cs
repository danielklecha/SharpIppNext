using SharpIpp.Mapping;
using SharpIpp.Validation;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the locations of stitches, staples, or crimps.
/// See: PWG 5100.1-2022 Section 5.2.9
/// </summary>
[IppAttribute("stitching")]
public class Stitching : IIppCollection
{
    /// <inheritdoc />

    /// <summary>
    /// integer(0:359) degrees counterclockwise
    /// See: PWG 5100.1-2022 Section 5.2.9.1
    /// </summary>
    public IppValue<int>? StitchingAngle { get; set; }

    /// <summary>
    /// 1setOf integer(0:MAX) in hundredths of millimeters (1/2540th of an inch)
    /// See: PWG 5100.1-2022 Section 5.2.9.2
    /// </summary>
    [Range(0, int.MaxValue)]
    public IppValue<int[]>? StitchingLocations { get; set; }

    /// <summary>
    /// type2 keyword
    /// See: PWG 5100.1-2022 Section 5.2.9.3
    /// </summary>
    public IppValue<StitchingMethod>? StitchingMethod { get; set; }

    /// <summary>
    /// integer(0:MAX) in hundredths of millimeters (1/2540th of an inch)
    /// See: PWG 5100.1-2022 Section 5.2.9.4
    /// </summary>
    public IppValue<int>? StitchingOffset { get; set; }

    /// <summary>
    /// type1 keyword
    /// See: PWG 5100.1-2022 Section 5.2.9.5
    /// </summary>
    public IppValue<FinishingReferenceEdge>? StitchingReferenceEdge { get; set; }
}
