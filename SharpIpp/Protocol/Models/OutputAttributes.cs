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
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;
    /// <summary>
    /// The noise-removal member attribute.
    /// See: PWG 5100.17-2014 Section 6.2.8
    /// </summary>
    [Range(0, 100)]
    public int? NoiseRemoval { get; set; }

    /// <summary>
    /// The output-compression-quality-factor member attribute.
    /// See: PWG 5100.17-2014 Section 6.2.8
    /// </summary>
    [Range(0, 100)]
    public int? OutputCompressionQualityFactor { get; set; }
}
