namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>destination-statuses</c> member collection.
/// See: PWG 5100.15-2013 Section 7.3.1.
/// </summary>
public class DestinationStatus : IIppCollection
{
    bool IIppCollection.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((IIppCollection)this).IsValue;
    public string? DestinationUri { get; set; }
    public int? ImagesCompleted { get; set; }
    public TransmissionStatus? TransmissionStatus { get; set; }
}
