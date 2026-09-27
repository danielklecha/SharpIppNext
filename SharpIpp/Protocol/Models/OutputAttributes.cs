using SharpIpp.Mapping;
using SharpIpp.Validation;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>output-attributes</c> collection.
/// See: PWG 5100.17-2014 Section 6.2.8
/// </summary>
[IppAttribute(IppAttributeNames.OutputAttributes)]
public class OutputAttributes : IIppCollection
{
    /// <summary>
    /// The noise-removal member attribute.
    /// See: PWG 5100.17-2014 Section 6.2.8
    /// </summary>
    [Range(0, 100)]
    public IppValue<int>? NoiseRemoval { get; set; }

    /// <summary>
    /// The output-compression-quality-factor member attribute.
    /// See: PWG 5100.17-2014 Section 6.2.8
    /// </summary>
    [Range(0, 100)]
    public IppValue<int>? OutputCompressionQualityFactor { get; set; }
}
