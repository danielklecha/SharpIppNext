using SharpIpp.Validation;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>print-objects</c> member collection.
/// See: PWG 5100.21-2019 Section 8.1.8
/// </summary>
public class PrintObject : IIppCollection
{
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;
    public int? DocumentNumber { get; set; }
    public System.Uri? PrintObjectsSource { get; set; }
    [ItemRange(int.MinValue, int.MaxValue)]
    public int[]? TransformationMatrix { get; set; }
}
