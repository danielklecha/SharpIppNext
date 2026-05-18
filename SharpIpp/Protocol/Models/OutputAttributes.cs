namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>output-attributes</c> collection.
/// See: PWG 5100.17-2014 Section 6.2.8
/// </summary>
public class OutputAttributes : IIppCollection
{
    bool IIppCollection.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((IIppCollection)this).IsValue;
    /// <summary>
    /// The noise-removal member attribute.
    /// Type: integer(0:100)
    /// See: PWG 5100.17-2014 Section 6.2.8
    /// </summary>
    public int? NoiseRemoval { get; set; }

    /// <summary>
    /// The output-compression-quality-factor member attribute.
    /// Type: integer(0:100)
    /// See: PWG 5100.17-2014 Section 6.2.8
    /// </summary>
    public int? OutputCompressionQualityFactor { get; set; }
}
