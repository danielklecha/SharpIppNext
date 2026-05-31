using System;
using System.Collections.Generic;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

internal class DestinationUriProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, DestinationUri>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<DestinationUri>();

            return new DestinationUri
            {
                DestinationUriValue = map.MapFromDicNullable<Uri?>(src, "destination-uri"),
                PostDialString = map.MapFromDicNullable<string?>(src, nameof(DestinationUri.PostDialString).ConvertCamelCaseToKebabCase()),
                PreDialString = map.MapFromDicNullable<string?>(src, nameof(DestinationUri.PreDialString).ConvertCamelCaseToKebabCase()),
                T33Subaddress = map.MapFromDicNullable<int?>(src, nameof(DestinationUri.T33Subaddress).ConvertCamelCaseToKebabCase())
            };
        });

        mapper.CreateMap<DestinationUri, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, IppAttributeNames.DestinationUris, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.DestinationUriValue != null) attributes.Add(new IppAttribute(Tag.Uri, "destination-uri", src.DestinationUriValue.ToString()));
            if (src.PostDialString != null) attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(DestinationUri.PostDialString).ConvertCamelCaseToKebabCase(), src.PostDialString));
            if (src.PreDialString != null) attributes.Add(new IppAttribute(Tag.TextWithoutLanguage, nameof(DestinationUri.PreDialString).ConvertCamelCaseToKebabCase(), src.PreDialString));
            if (src.T33Subaddress.HasValue) attributes.Add(new IppAttribute(Tag.Integer, nameof(DestinationUri.T33Subaddress).ConvertCamelCaseToKebabCase(), src.T33Subaddress.Value));
            return attributes;
        });
    }
}
