namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies a single element of the "media-size-supported" Printer Description attribute.
/// See: PWG 5100.7-2023 Section 6.9.50.
/// </summary>
public class MediaSizeSupported : IIppCollection
{
    /// <inheritdoc />
    bool IIppCollection.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((IIppCollection)this).IsValue;

    /// <summary>
    /// integer(1:MAX) | rangeOfInteger(1:MAX)
    /// </summary>
    public Range? XDimension { get; set; }

    /// <summary>
    /// integer(1:MAX) | rangeOfInteger(1:MAX)
    /// </summary>
    public Range? YDimension { get; set; }
}
