namespace SharpIpp.Protocol.Models;
public class MediaSize
{
    /// <summary>
    /// integer(0:MAX))
    /// </summary>
    public int? XDimension { get; set; }

    /// <summary>
    /// integer(0:MAX))
    /// </summary>
    public int? YDimension { get; set; }
    
    /// <summary>
    /// Range(0:MAX, 0:MAX))
    /// </summary>
    public Range? XDimensionRange { get; set; }

    /// <summary>
    /// Range(0:MAX, 0:MAX))
    /// </summary>
    public Range? YDimensionRange { get; set; }
}
