namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>output-attributes</c> collection.
/// See: PWG 5100.17-2014 Section 6.2.8
/// </summary>
public class OutputAttributes : IIppCollection
{
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;
    /// <summary>
    /// The noise-removal member attribute.
    /// See: PWG 5100.17-2014 Section 6.2.8
    /// </summary>
    [System.ComponentModel.DataAnnotations.Range(0, 100)]
    public int? NoiseRemoval { get; set; }

    /// <summary>
    /// The output-compression-quality-factor member attribute.
    /// See: PWG 5100.17-2014 Section 6.2.8
    /// </summary>
    [System.ComponentModel.DataAnnotations.Range(0, 100)]
    public int? OutputCompressionQualityFactor { get; set; }
}
