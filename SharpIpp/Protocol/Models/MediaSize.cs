namespace SharpIpp.Protocol.Models;
public class MediaSize : IIppCollection
{
    /// <inheritdoc />
    public bool IsNoValue { get; set; }

    /// <summary>
    /// integer(0:MAX))
    /// </summary>
    public int? XDimension { get; set; }

    /// <summary>
    /// integer(0:MAX))
    /// </summary>
    public int? YDimension { get; set; }
}
