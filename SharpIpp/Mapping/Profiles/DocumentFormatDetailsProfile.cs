using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;


internal class DocumentFormatDetailsProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, DocumentFormatDetails>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<DocumentFormatDetails>();

            var dst = new DocumentFormatDetails
            {
                DocumentSourceApplicationName = map.MapFromDicNullable<string?>(src, nameof(DocumentFormatDetails.DocumentSourceApplicationName).ConvertCamelCaseToKebabCase()),
                DocumentSourceApplicationVersion = map.MapFromDicNullable<string?>(src, nameof(DocumentFormatDetails.DocumentSourceApplicationVersion).ConvertCamelCaseToKebabCase()),
                DocumentSourceOsName = map.MapFromDicNullable<string?>(src, nameof(DocumentFormatDetails.DocumentSourceOsName).ConvertCamelCaseToKebabCase()),
                DocumentSourceOsVersion = map.MapFromDicNullable<string?>(src, nameof(DocumentFormatDetails.DocumentSourceOsVersion).ConvertCamelCaseToKebabCase()),
            };
            return dst;
        });
        mapper.CreateMap<DocumentFormatDetails, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, JobAttribute.DocumentFormatDetails, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.DocumentSourceApplicationName != null)
                attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, nameof(DocumentFormatDetails.DocumentSourceApplicationName).ConvertCamelCaseToKebabCase(), src.DocumentSourceApplicationName));
            if (src.DocumentSourceApplicationVersion != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(DocumentFormatDetails.DocumentSourceApplicationVersion).ConvertCamelCaseToKebabCase(), src.DocumentSourceApplicationVersion));
            if (src.DocumentSourceOsName != null)
                attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, nameof(DocumentFormatDetails.DocumentSourceOsName).ConvertCamelCaseToKebabCase(), src.DocumentSourceOsName));
            if (src.DocumentSourceOsVersion != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(DocumentFormatDetails.DocumentSourceOsVersion).ConvertCamelCaseToKebabCase(), src.DocumentSourceOsVersion));
            return attributes;
        });
    }
}
