namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies which material to apply to the hardcopy output.
/// See: PWG 5100.1-2022 Section 5.2.7
/// </summary>
public class Laminating : IIppCollection
{
    /// <inheritdoc />
    bool IIppCollection.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((IIppCollection)this).IsValue;

    /// <summary>
    /// type2 keyword
    /// See: PWG 5100.1-2022 Section 5.2.7.1
    /// </summary>
    public CoatingSides? LaminatingSides { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// See: PWG 5100.1-2022 Section 5.2.7.2
    /// </summary>
    public LaminatingType? LaminatingType { get; set; }
}
