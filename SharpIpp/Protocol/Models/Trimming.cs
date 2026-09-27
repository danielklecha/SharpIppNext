using SharpIpp.Mapping;
using SharpIpp.Validation;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies where to cut, perforate, or score the Media Sheets.
/// See: PWG 5100.1-2022 Section 5.2.10
/// </summary>
[IppAttribute("trimming")]
public class Trimming : IIppCollection
{
    /// <inheritdoc />

    /// <summary>
    /// 1setOf integer(0:MAX) in hundredths of millimeters (1/2540th of an inch)
    /// See: PWG 5100.1-2022 Section 5.2.10.1
    /// </summary>
    [Range(0, int.MaxValue)]
    public IppValue<int[]>? TrimmingOffset { get; set; }

    /// <summary>
    /// type1 keyword
    /// See: PWG 5100.1-2022 Section 5.2.10.2
    /// </summary>
    public IppValue<FinishingReferenceEdge>? TrimmingReferenceEdge { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// See: PWG 5100.1-2022 Section 5.2.10.3
    /// </summary>
    public IppValue<TrimmingType>? TrimmingType { get; set; }

    /// <summary>
    /// type2 keyword
    /// See: PWG 5100.1-2022 Section 5.2.10.4
    /// </summary>
    public IppValue<TrimmingWhen>? TrimmingWhen { get; set; }
}
