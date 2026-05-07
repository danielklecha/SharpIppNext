using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;


internal class CoverProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, Cover>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<Cover>();

            var dst = new Cover
            {
                CoverType = map.MapFromDicNullable<CoverType?>(src, nameof(Cover.CoverType).ConvertCamelCaseToKebabCase()),
                Media = map.MapFromDicNullable<string, Media?>(src, nameof(Cover.Media).ConvertCamelCaseToKebabCase(), (attribute, value) => new Media(value, attribute.Tag == Tag.Keyword))
            };
            if (src.ContainsKey(nameof(Cover.MediaCol).ConvertCamelCaseToKebabCase()))
                dst.MediaCol = map.Map<MediaCol>(src[nameof(Cover.MediaCol).ConvertCamelCaseToKebabCase()].FromBegCollection().ToIppDictionary());
            return dst;
        });
        mapper.CreateMap<Cover, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, "cover", NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.CoverType.HasValue)
                attributes.Add(new IppAttribute(Tag.Keyword, nameof(Cover.CoverType).ConvertCamelCaseToKebabCase(), map.Map<string>(src.CoverType.Value)));
            if (src.Media != null)
                attributes.Add(new IppAttribute(src.Media.Value.ToIppTag(), nameof(Cover.Media).ConvertCamelCaseToKebabCase(), map.Map<string>(src.Media.Value)));
            if (src.MediaCol != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(nameof(Cover.MediaCol).ConvertCamelCaseToKebabCase()));
            return attributes;
        });
    }
}
