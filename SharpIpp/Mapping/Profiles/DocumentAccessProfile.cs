using System;
using System.Collections.Generic;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

internal class DocumentAccessProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, DocumentAccess>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<DocumentAccess>();

            return new DocumentAccess
            {
                AccessOAuthToken = map.MapFromDicNullable<string?>(src, nameof(DocumentAccess.AccessOAuthToken).ConvertCamelCaseToKebabCase()),
                AccessOAuthUri = map.MapFromDicNullable<Uri?>(src, nameof(DocumentAccess.AccessOAuthUri).ConvertCamelCaseToKebabCase()),
                AccessPassword = map.MapFromDicNullable<string?>(src, nameof(DocumentAccess.AccessPassword).ConvertCamelCaseToKebabCase()),
                AccessPin = map.MapFromDicNullable<string?>(src, nameof(DocumentAccess.AccessPin).ConvertCamelCaseToKebabCase()),
                AccessUserName = map.MapFromDicNullable<string?>(src, nameof(DocumentAccess.AccessUserName).ConvertCamelCaseToKebabCase()),
                AccessX509Certificate = map.MapFromDicNullable<string?>(src, nameof(DocumentAccess.AccessX509Certificate).ConvertCamelCaseToKebabCase())
            };
        });

        mapper.CreateMap<DocumentAccess, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, IppAttributeNames.DocumentAccess, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.AccessOAuthToken != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(DocumentAccess.AccessOAuthToken).ConvertCamelCaseToKebabCase(), src.AccessOAuthToken));
            if (src.AccessOAuthUri != null)
                attributes.Add(new IppAttribute(Tag.Uri, nameof(DocumentAccess.AccessOAuthUri).ConvertCamelCaseToKebabCase(), src.AccessOAuthUri.ToString()));
            if (src.AccessPassword != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(DocumentAccess.AccessPassword).ConvertCamelCaseToKebabCase(), src.AccessPassword));
            if (src.AccessPin != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(DocumentAccess.AccessPin).ConvertCamelCaseToKebabCase(), src.AccessPin));
            if (src.AccessUserName != null)
                attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, nameof(DocumentAccess.AccessUserName).ConvertCamelCaseToKebabCase(), src.AccessUserName));
            if (src.AccessX509Certificate != null)
                attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(DocumentAccess.AccessX509Certificate).ConvertCamelCaseToKebabCase(), src.AccessX509Certificate));
            return attributes;
        });
    }
}
