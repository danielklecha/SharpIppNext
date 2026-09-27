using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the location and type of binding to apply.
/// See: PWG 5100.1-2022 Section 5.2.2
/// </summary>
[IppAttribute("binding")]
public class Binding : IIppCollection
{
    /// <inheritdoc />

    /// <summary>
    /// type1 keyword
    /// See: PWG 5100.1-2022 Section 5.2.2.1
    /// </summary>
    public IppValue<FinishingReferenceEdge>? BindingReferenceEdge { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// See: PWG 5100.1-2022 Section 5.2.2.2
    /// </summary>
    public IppValue<BindingType>? BindingType { get; set; }
}
