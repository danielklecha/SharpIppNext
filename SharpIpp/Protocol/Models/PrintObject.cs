using SharpIpp.Mapping;
using SharpIpp.Validation;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>print-objects</c> member collection.
/// See: PWG 5100.21-2019 Section 8.1.8
/// </summary>
[IppAttribute(IppAttributeNames.PrintObjects)]
public class PrintObject : IIppCollection
{
    public IppValue<int>? DocumentNumber { get; set; }
    public IppValue<System.Uri>? PrintObjectsSource { get; set; }
    [Range(int.MinValue, int.MaxValue)]
    public IppValue<int[]>? TransformationMatrix { get; set; }
}
