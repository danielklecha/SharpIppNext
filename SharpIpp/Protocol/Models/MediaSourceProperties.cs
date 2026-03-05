namespace SharpIpp.Protocol.Models;
public class MediaSourceProperties : IIppCollection
{
    /// <inheritdoc />
    public bool IsNoValue { get; set; }

    /// <summary>
    /// type2 keyword
    /// </summary>
    public MediaSourceFeedDirection? MediaSourceFeedDirection { get; set; }

    /// <summary>
    /// type2 enum
    /// </summary>
    public Orientation? MediaSourceFeedOrientation { get; set; }
}
