using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;


internal class SystemContactProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SystemContact>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<SystemContact>();

            return new SystemContact
            {
                ContactName = map.MapFromDicNullable<string?>(src, "contact-name"),
                ContactUri = map.MapFromDicNullable<Uri?>(src, "contact-uri"),
                ContactVcard = map.MapFromDicSetNullable<string[]?>(src, "contact-vcard")
            };
        });

        mapper.CreateMap<SystemContact, IEnumerable<IppAttribute>>((src, map) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, "system-contact-col", NoValue.Instance) };

            var attributes = new List<IppAttribute>();
            if (src.ContactName != null)
                attributes.Add(new IppAttribute(Tag.NameWithoutLanguage, "contact-name", src.ContactName));
            if (src.ContactUri != null)
                attributes.Add(new IppAttribute(Tag.Uri, "contact-uri", src.ContactUri.ToString()));
            if (src.ContactVcard != null)
                attributes.AddRange(src.ContactVcard.Select(x => new IppAttribute(Tag.TextWithoutLanguage, "contact-vcard", x)));
            return attributes;
        });
    }
}
