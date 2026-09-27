using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>print-accuracy</c> collection.
/// See: PWG 5100.21-2019 Section 8.1.6
/// </summary>
[IppAttribute(IppAttributeNames.PrintAccuracy)]
public class PrintAccuracy : IIppCollection
{
    public IppValue<AccuracyUnits>? AccuracyUnits { get; set; }
    public IppValue<int>? XAccuracy { get; set; }
    public IppValue<int>? YAccuracy { get; set; }
    public IppValue<int>? ZAccuracy { get; set; }
}
