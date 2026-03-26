using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

// ReSharper disable once UnusedMember.Global
internal class MediaSourcePropertiesProfile : IProfile
{
    private static bool IsOutOfBandNoValue(IDictionary<string, IppAttribute[]> src)
    {
        return src.Count == 1 && src.Values.First().Length == 1 && src.Values.First()[0].Tag.IsOutOfBand();
    }

    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<Dictionary<string, IppAttribute[]>, MediaSourceProperties>((src, map) =>
        {
            if (IsOutOfBandNoValue(src))
                return NoValue.GetNoValue<MediaSourceProperties>();

            return new MediaSourceProperties
            {
                MediaSourceFeedDirection = map.MapFromDicNullable<MediaSourceFeedDirection?>(src, nameof(MediaSourceProperties.MediaSourceFeedDirection).ConvertCamelCaseToKebabCase()),
                MediaSourceFeedOrientation = map.MapFromDicNullable<Orientation?>(src, nameof(MediaSourceProperties.MediaSourceFeedOrientation).ConvertCamelCaseToKebabCase())
            };
        });

        mapper.CreateMap<MediaSourceProperties, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, "media-source-properties", NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.MediaSourceFeedDirection.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(MediaSourceProperties.MediaSourceFeedDirection).ConvertCamelCaseToKebabCase(), map.Map<string>(src.MediaSourceFeedDirection.Value)));
            if (src.MediaSourceFeedOrientation.HasValue)
                attributes.Add(new IppAttribute(Tag.Enum, nameof(MediaSourceProperties.MediaSourceFeedOrientation).ConvertCamelCaseToKebabCase(), (int)src.MediaSourceFeedOrientation.Value));
            return attributes;
        });
    }
}
