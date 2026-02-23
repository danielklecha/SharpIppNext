namespace SharpIpp.Protocol.Models;
public class MediaCol
{
    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public MediaCoating? MediaBackCoating { get; set; }

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public int? MediaBottomMargin { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// PWG Media Standardized Names v2.0 (MSN2) [PWG5101.1]
    /// </summary>
    public string? MediaColor { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public MediaCoating? MediaFrontCoating { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public MediaGrain? MediaGrain { get; set; }

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public int? MediaHoleCount { get; set; }

    /// <summary>
    /// text(255)
    /// </summary>
    public string? MediaInfo { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public string? MediaKey { get; set; }

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public int? MediaLeftMargin { get; set; }

    /// <summary>
    /// integer(1:MAX)
    /// </summary>
    public int? MediaOrderCount { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public MediaPrePrinted? MediaPrePrinted { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public MediaRecycled? MediaRecycled { get; set; }

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public int? MediaRightMargin { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public MediaSize? MediaSize { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// PWG media size name [PWG5101.1]
    /// </summary>
    public string? MediaSizeName { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public MediaSource? MediaSource { get; set; }

    /// <summary>
    /// collection
    /// </summary>
    public MediaSourceProperties? MediaSourceProperties { get; set; }

    /// <summary>
    /// integer(1:MAX)
    /// </summary>
    public int? MediaThickness { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public MediaTooth? MediaTooth { get; set; }

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public int? MediaTopMargin { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public string? MediaType { get; set; }

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public int? MediaWeightMetric { get; set; }
}
