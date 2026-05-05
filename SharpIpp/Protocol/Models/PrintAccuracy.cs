namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>print-accuracy</c> collection.
/// See: PWG 5100.21-2019 Section 6.8.14
/// </summary>
public class PrintAccuracy : IIppCollection
{
    bool IIppCollection.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((IIppCollection)this).IsValue;
    public AccuracyUnits? AccuracyUnits { get; set; }
    public int? XAccuracy { get; set; }
    public int? YAccuracy { get; set; }
    public int? ZAccuracy { get; set; }
}
