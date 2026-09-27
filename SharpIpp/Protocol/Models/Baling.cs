using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the type of baling to apply to a collection of Media Sheets.
/// See: PWG 5100.1-2022 Section 5.2.1
/// </summary>
[IppAttribute("baling")]
public class Baling : IIppCollection
{
    /// <inheritdoc />

    /// <summary>
    /// type2 keyword | name(MAX)
    /// See: PWG 5100.1-2022 Section 5.2.1.1
    /// </summary>
    public IppValue<BalingType>? BalingType { get; set; }

    /// <summary>
    /// type2 keyword
    /// See: PWG 5100.1-2022 Section 5.2.1.2
    /// </summary>
    public IppValue<BalingWhen>? BalingWhen { get; set; }
}
