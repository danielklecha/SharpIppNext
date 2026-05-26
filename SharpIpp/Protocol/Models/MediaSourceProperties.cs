namespace SharpIpp.Protocol.Models;
public class MediaSourceProperties : IIppCollection
{
    /// <inheritdoc />
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;

    /// <summary>
    /// type2 keyword
    /// </summary>
    public MediaSourceFeedDirection? MediaSourceFeedDirection { get; set; }

    /// <summary>
    /// type2 enum
    /// </summary>
    public Orientation? MediaSourceFeedOrientation { get; set; }
}
