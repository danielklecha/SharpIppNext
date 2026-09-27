using SharpIpp.Mapping;
using SharpIpp.Validation;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the locations of holes to make in the hardcopy output.
/// See: PWG 5100.1-2022 Section 5.2.8
/// </summary>
[IppAttribute("punching")]
public class Punching : IIppCollection
{
    /// <inheritdoc />

    /// <summary>
    /// 1setOf integer(0:MAX) in hundredths of millimeters (1/2540th of an inch)
    /// See: PWG 5100.1-2022 Section 5.2.8.1
    /// </summary>
    [Range(0, int.MaxValue)]
    public IppValue<int[]>? PunchingLocations { get; set; }

    /// <summary>
    /// integer(0:MAX) in hundredths of millimeters (1/2540th of an inch)
    /// See: PWG 5100.1-2022 Section 5.2.8.2
    /// </summary>
    public IppValue<int>? PunchingOffset { get; set; }

    /// <summary>
    /// type1 keyword
    /// See: PWG 5100.1-2022 Section 5.2.8.3
    /// </summary>
    public IppValue<FinishingReferenceEdge>? PunchingReferenceEdge { get; set; }
}
