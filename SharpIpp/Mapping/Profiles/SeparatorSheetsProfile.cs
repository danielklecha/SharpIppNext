using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;


internal class SeparatorSheetsProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SeparatorSheets>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<SeparatorSheets>();

            var dst = new SeparatorSheets
            {
                Media = map.MapFromDicNullable<string, Media?>(src, nameof(SeparatorSheets.Media).ConvertCamelCaseToKebabCase(), (attribute, value) => new Media(value, attribute.Tag == Tag.Keyword)),
                SeparatorSheetsType = map.MapFromDicSetNullable<SeparatorSheetsType[]?>(src, nameof(SeparatorSheets.SeparatorSheetsType).ConvertCamelCaseToKebabCase())
            };
            if (src.ContainsKey(nameof(SeparatorSheets.MediaCol).ConvertCamelCaseToKebabCase()))
                dst.MediaCol = map.Map<MediaCol>(src[nameof(SeparatorSheets.MediaCol).ConvertCamelCaseToKebabCase()].FromBegCollection().ToIppDictionary());
            return dst;
        });
        mapper.CreateMap<SeparatorSheets, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.SeparatorSheets, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.Media != null)
                attributes.Add(new IppAttribute(src.Media.Value.ToIppTag(), nameof(SeparatorSheets.Media).ConvertCamelCaseToKebabCase(), map.Map<string>(src.Media.Value)));
            if (src.MediaCol != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(nameof(SeparatorSheets.MediaCol).ConvertCamelCaseToKebabCase()));
            if (src.SeparatorSheetsType != null)
                attributes.AddRange(src.SeparatorSheetsType.Select(x => new IppAttribute(Tag.Keyword, nameof(SeparatorSheets.SeparatorSheetsType).ConvertCamelCaseToKebabCase(), map.Map<string>(x))));
            return attributes;
        });
    }
}
