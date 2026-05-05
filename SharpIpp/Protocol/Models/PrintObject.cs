namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>print-objects</c> member collection.
/// See: PWG 5100.21-2019 Section 6.8.16
/// </summary>
public class PrintObject : IIppCollection
{
    bool IIppCollection.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((IIppCollection)this).IsValue;
    public int? DocumentNumber { get; set; }
    public string? PrintObjectsSource { get; set; }
    public int[]? TransformationMatrix { get; set; }
}
