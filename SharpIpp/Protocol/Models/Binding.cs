namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the location and type of binding to apply.
/// See: PWG 5100.1-2022 Section 5.2.2
/// </summary>
public class Binding : IIppCollection
{
    /// <inheritdoc />
    bool IIppCollection.IsNoValue { get; set; }

    /// <summary>
    /// type1 keyword
    /// See: PWG 5100.1-2022 Section 5.2.2.1
    /// </summary>
    public FinishingReferenceEdge? BindingReferenceEdge { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// See: PWG 5100.1-2022 Section 5.2.2.2
    /// </summary>
    public BindingType? BindingType { get; set; }
}

