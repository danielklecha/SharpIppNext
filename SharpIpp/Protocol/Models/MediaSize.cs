using SharpIpp.Validation;

namespace SharpIpp.Protocol.Models;
public class MediaSize : IIppCollection
{
    /// <inheritdoc />
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;

    /// <summary>
    /// integer(1:MAX) | rangeOfInteger(1:MAX)
    /// </summary>
    [IppRange(1, int.MaxValue)]
    public Range? XDimension { get; set; }

    /// <summary>
    /// integer(0:MAX) | rangeOfInteger(0:MAX)
    /// </summary>
    [IppRange(0, int.MaxValue)]
    public Range? YDimension { get; set; }
}
