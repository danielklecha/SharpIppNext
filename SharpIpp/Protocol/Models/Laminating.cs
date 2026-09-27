using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies which material to apply to the hardcopy output.
/// See: PWG 5100.1-2022 Section 5.2.7
/// </summary>
[IppAttribute("laminating")]
public class Laminating : IIppCollection
{
    /// <inheritdoc />

    /// <summary>
    /// type2 keyword
    /// See: PWG 5100.1-2022 Section 5.2.7.1
    /// </summary>
    public IppValue<CoatingSides>? LaminatingSides { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// See: PWG 5100.1-2022 Section 5.2.7.2
    /// </summary>
    public IppValue<LaminatingType>? LaminatingType { get; set; }
}
