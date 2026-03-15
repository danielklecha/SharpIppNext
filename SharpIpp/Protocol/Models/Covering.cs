namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies which cover to apply over the hardcopy output.
/// See: PWG 5100.1-2022 Section 5.2.4
/// </summary>
public class Covering : IIppCollection
{
    /// <inheritdoc />
    public bool IsNoValue { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// See: PWG 5100.1-2022 Section 5.2.4.1
    /// </summary>
    public CoveringName? CoveringName { get; set; }
}

