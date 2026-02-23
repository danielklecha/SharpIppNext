using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

// ReSharper disable once UnusedMember.Global
internal class MediaColProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<Dictionary<string, IppAttribute[]>, MediaCol>((src, map) =>
        {
            var mediaCol = new MediaCol
            {
                MediaBackCoating = map.MapFromDicNullable<MediaCoating?>(src, nameof(MediaCol.MediaBackCoating).ConvertCamelCaseToKebabCase()),
                MediaBottomMargin = map.MapFromDicNullable<int?>(src, nameof(MediaCol.MediaBottomMargin).ConvertCamelCaseToKebabCase()),
                MediaColor = map.MapFromDicNullable<string?>(src, nameof(MediaCol.MediaColor).ConvertCamelCaseToKebabCase()),
                MediaFrontCoating = map.MapFromDicNullable<MediaCoating?>(src, nameof(MediaCol.MediaFrontCoating).ConvertCamelCaseToKebabCase()),
                MediaGrain = map.MapFromDicNullable<MediaGrain?>(src, nameof(MediaCol.MediaGrain).ConvertCamelCaseToKebabCase()),
                MediaHoleCount = map.MapFromDicNullable<int?>(src, nameof(MediaCol.MediaHoleCount).ConvertCamelCaseToKebabCase()),
                MediaInfo = map.MapFromDicNullable<string?>(src, nameof(MediaCol.MediaInfo).ConvertCamelCaseToKebabCase()),
                MediaKey = map.MapFromDicNullable<string?>(src, nameof(MediaCol.MediaKey).ConvertCamelCaseToKebabCase()),
                MediaLeftMargin = map.MapFromDicNullable<int?>(src, nameof(MediaCol.MediaLeftMargin).ConvertCamelCaseToKebabCase()),
                MediaOrderCount = map.MapFromDicNullable<int?>(src, nameof(MediaCol.MediaOrderCount).ConvertCamelCaseToKebabCase()),
                MediaPrePrinted = map.MapFromDicNullable<MediaPrePrinted?>(src, nameof(MediaCol.MediaPrePrinted).ConvertCamelCaseToKebabCase()),
                MediaRecycled = map.MapFromDicNullable<MediaRecycled?>(src, nameof(MediaCol.MediaRecycled).ConvertCamelCaseToKebabCase()),
                MediaRightMargin = map.MapFromDicNullable<int?>(src, nameof(MediaCol.MediaRightMargin).ConvertCamelCaseToKebabCase()),
                MediaSizeName = map.MapFromDicNullable<string?>(src, nameof(MediaCol.MediaSizeName).ConvertCamelCaseToKebabCase()),
                MediaSource = map.MapFromDicNullable<MediaSource?>(src, nameof(MediaCol.MediaSource).ConvertCamelCaseToKebabCase()),
                MediaThickness = map.MapFromDicNullable<int?>(src, nameof(MediaCol.MediaThickness).ConvertCamelCaseToKebabCase()),
                MediaTooth = map.MapFromDicNullable<MediaTooth?>(src, nameof(MediaCol.MediaTooth).ConvertCamelCaseToKebabCase()),
                MediaTopMargin = map.MapFromDicNullable<int?>(src, nameof(MediaCol.MediaTopMargin).ConvertCamelCaseToKebabCase()),
                MediaType = map.MapFromDicNullable<string?>(src, nameof(MediaCol.MediaType).ConvertCamelCaseToKebabCase()),
                MediaWeightMetric = map.MapFromDicNullable<int?>(src, nameof(MediaCol.MediaWeightMetric).ConvertCamelCaseToKebabCase())
            };
            if (src.ContainsKey(nameof(MediaCol.MediaSize).ConvertCamelCaseToKebabCase()))
                mediaCol.MediaSize = map.Map<MediaSize>(src[nameof(MediaCol.MediaSize).ConvertCamelCaseToKebabCase()].FromBegCollection().ToIppDictionary());
            if (src.ContainsKey(nameof(MediaCol.MediaSourceProperties).ConvertCamelCaseToKebabCase()))
                mediaCol.MediaSourceProperties = map.Map<MediaSourceProperties>(src[nameof(MediaCol.MediaSourceProperties).ConvertCamelCaseToKebabCase()].FromBegCollection().ToIppDictionary());
            return mediaCol;
        });

        mapper.CreateMap<MediaCol, IEnumerable<IppAttribute>>((src, map) =>
        {
            var attributes = new List<IppAttribute>();
            if (src.MediaBackCoating.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(MediaCol.MediaBackCoating).ConvertCamelCaseToKebabCase(), map.Map<string>(src.MediaBackCoating.Value)));
            if (src.MediaBottomMargin.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(MediaCol.MediaBottomMargin).ConvertCamelCaseToKebabCase(), src.MediaBottomMargin.Value));
            if (src.MediaColor != null)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(MediaCol.MediaColor).ConvertCamelCaseToKebabCase(), src.MediaColor));
            if (src.MediaFrontCoating.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(MediaCol.MediaFrontCoating).ConvertCamelCaseToKebabCase(), map.Map<string>(src.MediaFrontCoating.Value)));
            if (src.MediaGrain.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(MediaCol.MediaGrain).ConvertCamelCaseToKebabCase(), map.Map<string>(src.MediaGrain.Value)));
            if (src.MediaHoleCount.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(MediaCol.MediaHoleCount).ConvertCamelCaseToKebabCase(), src.MediaHoleCount.Value));
            if (src.MediaInfo != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(MediaCol.MediaInfo).ConvertCamelCaseToKebabCase(), src.MediaInfo));
            if (src.MediaKey != null)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(MediaCol.MediaKey).ConvertCamelCaseToKebabCase(), src.MediaKey));
            if (src.MediaLeftMargin.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(MediaCol.MediaLeftMargin).ConvertCamelCaseToKebabCase(), src.MediaLeftMargin.Value));
            if (src.MediaOrderCount.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(MediaCol.MediaOrderCount).ConvertCamelCaseToKebabCase(), src.MediaOrderCount.Value));
            if (src.MediaPrePrinted.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(MediaCol.MediaPrePrinted).ConvertCamelCaseToKebabCase(), map.Map<string>(src.MediaPrePrinted.Value)));
            if (src.MediaRecycled.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(MediaCol.MediaRecycled).ConvertCamelCaseToKebabCase(), map.Map<string>(src.MediaRecycled.Value)));
            if (src.MediaRightMargin.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(MediaCol.MediaRightMargin).ConvertCamelCaseToKebabCase(), src.MediaRightMargin.Value));
            if (src.MediaSize != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaSize).ToBegCollection(nameof(MediaCol.MediaSize).ConvertCamelCaseToKebabCase()));
            if (src.MediaSizeName != null)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(MediaCol.MediaSizeName).ConvertCamelCaseToKebabCase(), src.MediaSizeName));
            if (src.MediaSource.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(MediaCol.MediaSource).ConvertCamelCaseToKebabCase(), map.Map<string>(src.MediaSource.Value)));
            if (src.MediaSourceProperties != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaSourceProperties).ToBegCollection(nameof(MediaCol.MediaSourceProperties).ConvertCamelCaseToKebabCase()));
            if (src.MediaThickness.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(MediaCol.MediaThickness).ConvertCamelCaseToKebabCase(), src.MediaThickness.Value));
            if (src.MediaTooth.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(MediaCol.MediaTooth).ConvertCamelCaseToKebabCase(), map.Map<string>(src.MediaTooth.Value)));
            if (src.MediaTopMargin.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(MediaCol.MediaTopMargin).ConvertCamelCaseToKebabCase(), src.MediaTopMargin.Value));
            if (src.MediaType != null)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(MediaCol.MediaType).ConvertCamelCaseToKebabCase(), src.MediaType));
            if (src.MediaWeightMetric.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(MediaCol.MediaWeightMetric).ConvertCamelCaseToKebabCase(), src.MediaWeightMetric.Value));
            return attributes;
        });
    }
}
