using System;
using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>destination-statuses</c> member collection.
/// See: PWG 5100.15-2013 Section 7.3.1.
/// </summary>
[IppAttribute(IppAttributeNames.DestinationStatuses)]
public class DestinationStatus : IIppCollection
{
    public IppValue<Uri>? DestinationUri { get; set; }
    public IppValue<int>? ImagesCompleted { get; set; }
    public IppValue<TransmissionStatus>? TransmissionStatus { get; set; }
}
