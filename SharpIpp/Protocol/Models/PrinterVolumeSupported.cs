using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>printer-volume-supported</c> collection.
/// See: PWG 5100.21-2019 Section 8.3.33
/// </summary>
[IppAttribute(IppAttributeNames.PrinterVolumeSupported)]
public class PrinterVolumeSupported : IIppCollection
{
    public IppValue<int>? XDimension { get; set; }
    public IppValue<int>? YDimension { get; set; }
    public IppValue<int>? ZDimension { get; set; }
}
