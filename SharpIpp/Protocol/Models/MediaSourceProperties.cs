using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

[IppAttribute("media-source-properties")]
public class MediaSourceProperties : IIppCollection
{
    /// <inheritdoc />

    /// <summary>
    /// type2 keyword
    /// </summary>
    public IppValue<MediaSourceFeedDirection>? MediaSourceFeedDirection { get; set; }

    /// <summary>
    /// type2 enum
    /// </summary>
    public IppValue<Orientation>? MediaSourceFeedOrientation { get; set; }
}
