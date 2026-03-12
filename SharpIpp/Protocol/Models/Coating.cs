namespace SharpIpp.Protocol.Models;

/// <summary>
///     Specifies the coating to apply to Media Sheets.
///     See: PWG 5100.1-2022 Section 5.2.3
/// </summary>
public class Coating : IIppCollection
{
    /// <inheritdoc />
    public bool IsNoValue { get; set; }

    /// <summary>
    /// type1 keyword
    /// See: PWG 5100.1-2022 Section 5.2.3.1
    /// </summary>
    public CoatingSides? CoatingSides { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// See: PWG 5100.1-2022 Section 5.2.3.2
    /// </summary>
    public CoatingType? CoatingType { get; set; }
}

