using System;
using System.Collections.Generic;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

internal class ProofPrintProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, ProofPrint>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<ProofPrint>();

            var dst = new ProofPrint
            {
                ProofPrintCopies = map.MapFromDicNullable<int?>(src, nameof(ProofPrint.ProofPrintCopies).ConvertCamelCaseToKebabCase()),
                Media = map.MapFromDicNullable<string, Media?>(src, nameof(ProofPrint.Media).ConvertCamelCaseToKebabCase(), (attribute, value) => new Media(value, attribute.Tag == Tag.Keyword))
            };
            if (src.ContainsKey(nameof(ProofPrint.MediaCol).ConvertCamelCaseToKebabCase()))
                dst.MediaCol = map.Map<MediaCol>(src[nameof(ProofPrint.MediaCol).ConvertCamelCaseToKebabCase()].FromBegCollection().ToIppDictionary());
            return dst;
        });

        mapper.CreateMap<ProofPrint, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, IppAttributeNames.ProofPrint, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.ProofPrintCopies.HasValue)
                attributes.Add(new IppAttribute(Tag.Integer, nameof(ProofPrint.ProofPrintCopies).ConvertCamelCaseToKebabCase(), src.ProofPrintCopies.Value));
            if (src.Media != null)
                attributes.Add(new IppAttribute(src.Media.Value.ToIppTag(), nameof(ProofPrint.Media).ConvertCamelCaseToKebabCase(), map.Map<string>(src.Media.Value)));
            if (src.MediaCol != null)
                attributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.MediaCol).ToBegCollection(nameof(ProofPrint.MediaCol).ConvertCamelCaseToKebabCase()));
            return attributes;
        });
    }
}
