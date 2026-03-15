namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the type of baling to apply to a collection of Media Sheets.
/// See: PWG 5100.1-2022 Section 5.2.1
/// </summary>
public class Baling : IIppCollection
{
    /// <inheritdoc />
    public bool IsNoValue { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// See: PWG 5100.1-2022 Section 5.2.1.1
    /// </summary>
    public BalingType? BalingType { get; set; }

    /// <summary>
    /// type2 keyword
    /// See: PWG 5100.1-2022 Section 5.2.1.2
    /// </summary>
    public BalingWhen? BalingWhen { get; set; }
}

