using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies a single element of the "media-size-supported" Printer Description attribute.
/// See: PWG 5100.7-2023 Section 6.9.50.
/// </summary>
[IppAttribute(IppAttributeNames.MediaSizeSupported)]
public class MediaSizeSupported : IIppCollection
{
    /// <inheritdoc />

    /// <summary>
    /// integer(1:MAX) | rangeOfInteger(1:MAX)
    /// </summary>
    public IppValue<Range>? XDimension { get; set; }

    /// <summary>
    /// integer(1:MAX) | rangeOfInteger(1:MAX)
    /// </summary>
    public IppValue<Range>? YDimension { get; set; }
}
