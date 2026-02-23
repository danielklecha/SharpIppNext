using System.Collections.Generic;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

// ReSharper disable once UnusedMember.Global
internal class MediaSizeProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<Dictionary<string, IppAttribute[]>, MediaSize>((src, map) =>
            new MediaSize
            {
                XDimension = map.MapFromDicNullable<int?>(src, nameof(MediaSize.XDimension).ConvertCamelCaseToKebabCase()),
                YDimension = map.MapFromDicNullable<int?>(src, nameof(MediaSize.YDimension).ConvertCamelCaseToKebabCase())
            });

        mapper.CreateMap<MediaSize, IEnumerable<IppAttribute>>((src, map) =>
        {
            var attributes = new List<IppAttribute>();
            if (src.XDimension.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(MediaSize.XDimension).ConvertCamelCaseToKebabCase(), src.XDimension.Value));
            if (src.YDimension.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(MediaSize.YDimension).ConvertCamelCaseToKebabCase(), src.YDimension.Value));
            return attributes;
        });
    }
}
