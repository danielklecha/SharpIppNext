using System.Collections.Generic;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

// ReSharper disable once UnusedMember.Global
internal class MediaSizeSupportedProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        const string xDimension = "x-dimension";
        const string yDimension = "y-dimension";

        mapper.CreateMap<Dictionary<string, IppAttribute[]>, MediaSizeSupported>((src, map) =>
            new MediaSizeSupported
            {
                XDimension = map.MapFromDicNullable<Range?>(src, xDimension),
                YDimension = map.MapFromDicNullable<Range?>(src, yDimension)
            });

        mapper.CreateMap<MediaSizeSupported, IEnumerable<IppAttribute>>((src, map) =>
        {
            var attributes = new List<IppAttribute>();

            if (src.XDimension.HasValue)
            {
                var range = src.XDimension.Value;
                attributes.Add(range.Lower == range.Upper
                    ? new IppAttribute(Tag.Integer, xDimension, range.Lower)
                    : new IppAttribute(Tag.RangeOfInteger, xDimension, range));
            }

            if (src.YDimension.HasValue)
            {
                var range = src.YDimension.Value;
                attributes.Add(range.Lower == range.Upper
                    ? new IppAttribute(Tag.Integer, yDimension, range.Lower)
                    : new IppAttribute(Tag.RangeOfInteger, yDimension, range));
            }

            return attributes;
        });
    }
}
