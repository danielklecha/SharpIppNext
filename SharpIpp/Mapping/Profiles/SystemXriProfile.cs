using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;


internal class SystemXriProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SystemXri>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<SystemXri>();

            return new SystemXri
            {
                XriUri = map.MapFromDicNullable<Uri?>(src, "xri-uri"),
                XriAuthentication = map.MapFromDicNullable<UriAuthentication?>(src, "xri-authentication"),
                XriSecurity = map.MapFromDicNullable<UriSecurity?>(src, "xri-security")
            };
        });

        mapper.CreateMap<SystemXri, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, SystemAttribute.SystemXriSupported, NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.XriUri != null)
                attributes.Add(new IppAttribute(Tag.Uri, "xri-uri", src.XriUri.ToString()));
            if (src.XriAuthentication != null)
                attributes.Add(new IppAttribute(Tag.Keyword, "xri-authentication", src.XriAuthentication.Value));
            if (src.XriSecurity != null)
                attributes.Add(new IppAttribute(Tag.Keyword, "xri-security", src.XriSecurity.Value));
            return attributes;
        });
    }
}
