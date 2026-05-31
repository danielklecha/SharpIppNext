using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

// ReSharper disable once UnusedMember.Global
internal class MediaSizeProfile : IProfile
{public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<Dictionary<string, IppAttribute[]>, MediaSize>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<MediaSize>();

            return new MediaSize
            {
                XDimension = map.MapFromDicNullable<Range?>(src, nameof(MediaSize.XDimension).ConvertCamelCaseToKebabCase()),
                YDimension = map.MapFromDicNullable<Range?>(src, nameof(MediaSize.YDimension).ConvertCamelCaseToKebabCase())
            };
        });

        mapper.CreateMap<MediaSize, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, "media-size", NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.XDimension.HasValue)
            {
                var range = src.XDimension.Value;
                attributes.Add(range.Lower == range.Upper
                    ? new IppAttribute(Tag.Integer, nameof(MediaSize.XDimension).ConvertCamelCaseToKebabCase(), range.Lower)
                    : new IppAttribute(Tag.RangeOfInteger, nameof(MediaSize.XDimension).ConvertCamelCaseToKebabCase(), range));
            }
            if (src.YDimension.HasValue)
            {
                var range = src.YDimension.Value;
                attributes.Add(range.Lower == range.Upper
                    ? new IppAttribute(Tag.Integer, nameof(MediaSize.YDimension).ConvertCamelCaseToKebabCase(), range.Lower)
                    : new IppAttribute(Tag.RangeOfInteger, nameof(MediaSize.YDimension).ConvertCamelCaseToKebabCase(), range));
            }
            return attributes;
        });
    }
}
