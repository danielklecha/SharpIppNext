using SharpIpp.Mapping;
using SharpIpp.Validation;

namespace SharpIpp.Protocol.Models;

[IppAttribute("media-size")]
public class MediaSize : IIppCollection
{
    /// <inheritdoc />

    /// <summary>
    /// integer(1:MAX) | rangeOfInteger(1:MAX)
    /// </summary>
    [Range(1, int.MaxValue)]
    public IppValue<Range>? XDimension { get; set; }

    /// <summary>
    /// integer(0:MAX) | rangeOfInteger(0:MAX)
    /// </summary>
    [Range(0, int.MaxValue)]
    public IppValue<Range>? YDimension { get; set; }
}
