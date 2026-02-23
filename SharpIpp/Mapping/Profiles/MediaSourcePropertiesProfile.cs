using System.Collections.Generic;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

// ReSharper disable once UnusedMember.Global
internal class MediaSourcePropertiesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<Dictionary<string, IppAttribute[]>, MediaSourceProperties>((src, map) =>
            new MediaSourceProperties
            {
                MediaSourceFeedDirection = map.MapFromDicNullable<MediaSourceFeedDirection?>(src, nameof(MediaSourceProperties.MediaSourceFeedDirection).ConvertCamelCaseToKebabCase()),
                MediaSourceFeedOrientation = map.MapFromDicNullable<Orientation?>(src, nameof(MediaSourceProperties.MediaSourceFeedOrientation).ConvertCamelCaseToKebabCase())
            });

        mapper.CreateMap<MediaSourceProperties, IEnumerable<IppAttribute>>((src, map) =>
        {
            var attributes = new List<IppAttribute>();
            if (src.MediaSourceFeedDirection.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(MediaSourceProperties.MediaSourceFeedDirection).ConvertCamelCaseToKebabCase(), map.Map<string>(src.MediaSourceFeedDirection.Value)));
            if (src.MediaSourceFeedOrientation.HasValue)
                attributes.Add(new IppAttribute(Tag.Enum, nameof(MediaSourceProperties.MediaSourceFeedOrientation).ConvertCamelCaseToKebabCase(), (int)src.MediaSourceFeedOrientation.Value));
            return attributes;
        });
    }
}
