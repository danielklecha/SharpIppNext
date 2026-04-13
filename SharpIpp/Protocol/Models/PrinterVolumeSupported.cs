namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>printer-volume-supported</c> collection.
/// See: PWG 5100.21-2019 Section 8.3.33
/// </summary>
public class PrinterVolumeSupported : IIppCollection
{
    public bool IsValue { get; set; } = true;
    public int? XDimension { get; set; }
    public int? YDimension { get; set; }
    public int? ZDimension { get; set; }
}
