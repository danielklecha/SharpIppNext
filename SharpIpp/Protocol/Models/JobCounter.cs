namespace SharpIpp.Protocol.Models;

/// <summary>
/// Job Counter member attributes for the "job-*-col" attributes.
/// See: PWG 5100.7-2023 Section 6.6.1 / Table 9.
/// </summary>
public class JobCounter : IIppCollection
{
    /// <inheritdoc />
    bool IIppCollection.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((IIppCollection)this).IsValue;

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public int? Blank { get; set; }

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public int? BlankTwoSided { get; set; }

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public int? FullColor { get; set; }

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public int? FullColorTwoSided { get; set; }

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public int? HighlightColor { get; set; }

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public int? HighlightColorTwoSided { get; set; }

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public int? Monochrome { get; set; }

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public int? MonochromeTwoSided { get; set; }
}
