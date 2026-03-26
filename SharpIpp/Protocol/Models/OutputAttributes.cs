namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>output-attributes</c> collection.
/// See: PWG 5100.17-2014 Section 6.2.8
/// </summary>
public class OutputAttributes : IIppCollection
{
    bool IIppCollection.IsNoValue { get; set; }
    public bool? NoiseRemoval { get; set; }
    public int? OutputCompressionQualityFactor { get; set; }
}
