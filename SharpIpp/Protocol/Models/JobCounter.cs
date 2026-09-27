using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Job Counter member attributes for the "job-*-col" attributes.
/// See: PWG 5100.7-2023 Section 6.6.1 / Table 9.
/// </summary>
[IppAttribute("job-counter")]
public class JobCounter : IIppCollection
{
    /// <inheritdoc />

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public IppValue<int>? Blank { get; set; }

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public IppValue<int>? BlankTwoSided { get; set; }

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public IppValue<int>? FullColor { get; set; }

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public IppValue<int>? FullColorTwoSided { get; set; }

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public IppValue<int>? HighlightColor { get; set; }

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public IppValue<int>? HighlightColorTwoSided { get; set; }

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public IppValue<int>? Monochrome { get; set; }

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public IppValue<int>? MonochromeTwoSided { get; set; }
}
