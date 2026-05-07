using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;


internal class InsertSheetProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, InsertSheet>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<InsertSheet>();

            var dst = new InsertSheet
            {
                InsertAfterPageNumber = map.MapFromDicNullable<int?>(src, nameof(InsertSheet.InsertAfterPageNumber).ConvertCamelCaseToKebabCase()),
                InsertCount = map.MapFromDicNullable<int?>(src, nameof(InsertSheet.InsertCount).ConvertCamelCaseToKebabCase()),
                Media = map.MapFromDicNullable<string, Media?>(src, nameof(InsertSheet.Media).ConvertCamelCaseToKebabCase(), (attribute, value) => new Media(value, attribute.Tag == Tag.Keyword))
            };
            if (src.ContainsKey(nameof(InsertSheet.MediaCol).ConvertCamelCaseToKebabCase()))
                dst.MediaCol = map.Map<MediaCol>(src[nameof(InsertSheet.MediaCol).ConvertCamelCaseToKebabCase()].FromBegCollection().ToIppDictionary());
            return dst;
        });
        mapper.CreateMap<InsertSheet, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, "insert-sheet", NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.InsertAfterPageNumber.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(InsertSheet.InsertAfterPageNumber).ConvertCamelCaseToKebabCase(), src.InsertAfterPageNumber.Value));
            if (src.InsertCount.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(InsertSheet.InsertCount).ConvertCamelCaseToKebabCase(), src.InsertCount.Value));
            if (src.Media != null)
                attributes.Add(new IppAttribute(src.Media.Value.ToIppTag(), nameof(InsertSheet.Media).ConvertCamelCaseToKebabCase(), map.Map<string>(src.Media.Value)));
            if (src.MediaCol != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(nameof(InsertSheet.MediaCol).ConvertCamelCaseToKebabCase()));
            return attributes;
        });
    }
}
