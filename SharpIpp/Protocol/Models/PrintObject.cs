namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>print-objects</c> member collection.
/// See: PWG 5100.21-2019 Section 6.8.16
/// </summary>
public class PrintObject : IIppCollection
{
    bool IIppCollection.IsNoValue { get; set; }
    public int? DocumentNumber { get; set; }
    public string? PrintObjectsSource { get; set; }
    public int[]? TransformationMatrix { get; set; }
}
