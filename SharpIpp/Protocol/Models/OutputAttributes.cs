namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>output-attributes</c> collection.
/// See: PWG 5100.17-2014 Section 6.2.8
/// </summary>
public class OutputAttributes : IIppCollection
{
    public bool IsValue { get; set; } = true;
    public int? NoiseRemoval { get; set; }
    public int? OutputCompressionQualityFactor { get; set; }
}
