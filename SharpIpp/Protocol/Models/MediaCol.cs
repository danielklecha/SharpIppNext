using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

[IppAttribute(IppAttributeNames.MediaCol)]
public class MediaCol : IIppCollection
{
    /// <inheritdoc />

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public IppValue<MediaCoating>? MediaBackCoating { get; set; }

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public IppValue<int>? MediaBottomMargin { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// See: PWG Media Standardized Names v2.0 (MSN2) [PWG5101.1]
    /// </summary>
    public IppValue<MediaColor>? MediaColor { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public IppValue<MediaCoating>? MediaFrontCoating { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public IppValue<MediaGrain>? MediaGrain { get; set; }

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public IppValue<int>? MediaHoleCount { get; set; }

    /// <summary>
    /// text(255)
    /// </summary>
    [IppAttribute("media-info", Tag = Tag.TextWithoutLanguage)]
    public StringWithLanguage? MediaInfo { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public IppValue<MediaKey>? MediaKey { get; set; }

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public IppValue<int>? MediaLeftMargin { get; set; }

    /// <summary>
    /// integer(1:MAX)
    /// </summary>
    public IppValue<int>? MediaOrderCount { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public IppValue<MediaPrePrinted>? MediaPrePrinted { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public IppValue<MediaRecycled>? MediaRecycled { get; set; }

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public IppValue<int>? MediaRightMargin { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public IppValue<MediaSize>? MediaSize { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// See: PWG media size name [PWG5101.1]
    /// </summary>
    public IppValue<Media>? MediaSizeName { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public IppValue<MediaSource>? MediaSource { get; set; }

    /// <summary>
    /// collection
    /// </summary>
    public IppValue<MediaSourceProperties>? MediaSourceProperties { get; set; }

    /// <summary>
    /// integer(1:MAX)
    /// </summary>
    public IppValue<int>? MediaThickness { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public IppValue<MediaTooth>? MediaTooth { get; set; }

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public IppValue<int>? MediaTopMargin { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public IppValue<MediaType>? MediaType { get; set; }

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public IppValue<int>? MediaWeightMetric { get; set; }
}
