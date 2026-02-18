using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpIpp.Protocol.Models;
public class MediaCol
{
    public static MediaCol Create(Dictionary<string, IppAttribute[]> dict, IMapperApplier mapper)
    {
        return new MediaCol
        {
            MediaBackCoating = mapper.MapFromDic<MediaCoating?>(dict, nameof(MediaBackCoating).ConvertCamelCaseToKebabCase()),
            MediaBottomMargin = mapper.MapFromDic<int?>(dict, nameof(MediaBottomMargin).ConvertCamelCaseToKebabCase()),
            MediaColor = mapper.MapFromDic<string?>(dict, nameof(MediaColor).ConvertCamelCaseToKebabCase()),
            MediaFrontCoating = mapper.MapFromDic<MediaCoating?>(dict, nameof(MediaFrontCoating).ConvertCamelCaseToKebabCase()),
            MediaGrain = mapper.MapFromDic<MediaGrain?>(dict, nameof(MediaGrain).ConvertCamelCaseToKebabCase()),
            MediaHoleCount = mapper.MapFromDic<int?>(dict, nameof(MediaHoleCount).ConvertCamelCaseToKebabCase()),
            MediaInfo = mapper.MapFromDic<string?>(dict, nameof(MediaInfo).ConvertCamelCaseToKebabCase()),
            MediaKey = mapper.MapFromDic<string?>(dict, nameof(MediaKey).ConvertCamelCaseToKebabCase()),
            MediaLeftMargin = mapper.MapFromDic<int?>(dict, nameof(MediaLeftMargin).ConvertCamelCaseToKebabCase()),
            MediaOrderCount = mapper.MapFromDic<int?>(dict, nameof(MediaOrderCount).ConvertCamelCaseToKebabCase()),
            MediaPrePrinted = mapper.MapFromDic<MediaPrePrinted?>(dict, nameof(MediaPrePrinted).ConvertCamelCaseToKebabCase()),
            MediaRecycled = mapper.MapFromDic<MediaRecycled?>(dict, nameof(MediaRecycled).ConvertCamelCaseToKebabCase()),
            MediaRightMargin = mapper.MapFromDic<int?>(dict, nameof(MediaRightMargin).ConvertCamelCaseToKebabCase()),
            MediaSize = dict.ContainsKey(nameof(MediaSize).ConvertCamelCaseToKebabCase()) ? MediaSize.Create(dict[nameof(MediaSize).ConvertCamelCaseToKebabCase()].FromBegCollection().ToIppDictionary(), mapper) : null,
            MediaSizeName = mapper.MapFromDic<string?>(dict, nameof(MediaSizeName).ConvertCamelCaseToKebabCase()),
            MediaSource = mapper.MapFromDic<MediaSource?>(dict, nameof(MediaSource).ConvertCamelCaseToKebabCase()),
            MediaSourceProperties = dict.ContainsKey(nameof(MediaSourceProperties).ConvertCamelCaseToKebabCase()) ? MediaSourceProperties.Create(mapper, dict[nameof(MediaSourceProperties).ConvertCamelCaseToKebabCase()].FromBegCollection().ToIppDictionary()) : null,
            MediaThickness = mapper.MapFromDic<int?>(dict, nameof(MediaThickness).ConvertCamelCaseToKebabCase()),
            MediaTooth = mapper.MapFromDic<MediaTooth?>(dict, nameof(MediaTooth).ConvertCamelCaseToKebabCase()),
            MediaTopMargin = mapper.MapFromDic<int?>(dict, nameof(MediaTopMargin).ConvertCamelCaseToKebabCase()),
            MediaType = mapper.MapFromDic<string?>(dict, nameof(MediaType).ConvertCamelCaseToKebabCase()),
            MediaWeightMetric = mapper.MapFromDic<int?>(dict, nameof(MediaWeightMetric).ConvertCamelCaseToKebabCase())
        };
    }

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

    public IEnumerable<IppAttribute> GetIppAttributes(IMapperApplier mapper)
    {
        if (MediaBackCoating.HasValue)
            yield return new IppAttribute(Tag.Keyword, nameof(MediaBackCoating).ConvertCamelCaseToKebabCase(), mapper.Map<string>(MediaBackCoating.Value));
        if (MediaBottomMargin.HasValue)
            yield return IppAttribute.CreateInt(nameof(MediaBottomMargin).ConvertCamelCaseToKebabCase(), MediaBottomMargin.Value);
        if (MediaColor != null)
            yield return new IppAttribute(Tag.Keyword, nameof(MediaColor).ConvertCamelCaseToKebabCase(), MediaColor);
        if (MediaFrontCoating.HasValue)
            yield return new IppAttribute(Tag.Keyword, nameof(MediaFrontCoating).ConvertCamelCaseToKebabCase(), mapper.Map<string>(MediaFrontCoating.Value));
        if (MediaGrain.HasValue)
            yield return new IppAttribute(Tag.Keyword, nameof(MediaGrain).ConvertCamelCaseToKebabCase(), mapper.Map<string>(MediaGrain.Value));
        if (MediaHoleCount.HasValue)
            yield return IppAttribute.CreateInt(nameof(MediaHoleCount).ConvertCamelCaseToKebabCase(), MediaHoleCount.Value);
        if (MediaInfo != null)
            yield return new IppAttribute(Tag.TextWithoutLanguage, nameof(MediaInfo).ConvertCamelCaseToKebabCase(), MediaInfo);
        if (MediaKey != null)
            yield return new IppAttribute(Tag.Keyword, nameof(MediaKey).ConvertCamelCaseToKebabCase(), MediaKey);
        if (MediaLeftMargin.HasValue)
            yield return IppAttribute.CreateInt(nameof(MediaLeftMargin).ConvertCamelCaseToKebabCase(), MediaLeftMargin.Value);
        if (MediaOrderCount.HasValue)
            yield return IppAttribute.CreateInt(nameof(MediaOrderCount).ConvertCamelCaseToKebabCase(), MediaOrderCount.Value);
        if (MediaPrePrinted.HasValue)
            yield return new IppAttribute(Tag.Keyword, nameof(MediaPrePrinted).ConvertCamelCaseToKebabCase(), mapper.Map<string>(MediaPrePrinted.Value));
        if (MediaRecycled.HasValue)
            yield return new IppAttribute(Tag.Keyword, nameof(MediaRecycled).ConvertCamelCaseToKebabCase(), mapper.Map<string>(MediaRecycled.Value));
        if (MediaRightMargin.HasValue)
            yield return IppAttribute.CreateInt(nameof(MediaRightMargin).ConvertCamelCaseToKebabCase(), MediaRightMargin.Value);
        if (MediaSize != null)
            foreach (var attribute in MediaSize.GetIppAttributes(mapper).ToBegCollection(nameof(MediaSize).ConvertCamelCaseToKebabCase()))
                yield return attribute;
        if (MediaSizeName != null)
            yield return new IppAttribute(Tag.Keyword, nameof(MediaSizeName).ConvertCamelCaseToKebabCase(), MediaSizeName);
        if (MediaSource.HasValue)
            yield return new IppAttribute(Tag.Keyword, nameof(MediaSource).ConvertCamelCaseToKebabCase(), mapper.Map<string>(MediaSource.Value));
        if (MediaSourceProperties != null)
            foreach (var attribute in MediaSourceProperties.GetIppAttributes(mapper).ToBegCollection(nameof(MediaSourceProperties).ConvertCamelCaseToKebabCase()))
                yield return attribute;
        if (MediaThickness.HasValue)
            yield return IppAttribute.CreateInt(nameof(MediaThickness).ConvertCamelCaseToKebabCase(), MediaThickness.Value);
        if (MediaTooth.HasValue)
            yield return new IppAttribute(Tag.Keyword, nameof(MediaTooth).ConvertCamelCaseToKebabCase(), mapper.Map<string>(MediaTooth.Value));
        if (MediaTopMargin.HasValue)
            yield return IppAttribute.CreateInt(nameof(MediaTopMargin).ConvertCamelCaseToKebabCase(), MediaTopMargin.Value);
        if (MediaType != null)
            yield return new IppAttribute(Tag.Keyword, nameof(MediaType).ConvertCamelCaseToKebabCase(), MediaType);
        if (MediaWeightMetric.HasValue)
            yield return IppAttribute.CreateInt(nameof(MediaWeightMetric).ConvertCamelCaseToKebabCase(), MediaWeightMetric.Value);
    }
}
