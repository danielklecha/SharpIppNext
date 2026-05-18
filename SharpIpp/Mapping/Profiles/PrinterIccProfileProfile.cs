using System;
using System.Collections.Generic;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

internal class PrinterIccProfileProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PrinterIccProfile>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<PrinterIccProfile>();

            return new PrinterIccProfile
            {
                ProfileName = map.MapFromDicNullable<string?>(src, "profile-name"),
                ProfileUri = map.MapFromDicNullable<Uri?>(src, "profile-uri"),
            };
        });
        mapper.CreateMap<PrinterIccProfile, IEnumerable<IppAttribute>>((src, _) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, PrinterAttribute.PrinterIccProfiles, NoValue.Instance) };

            var attrs = new List<IppAttribute>();
            if (src.ProfileName != null) attrs.Add(new IppAttribute(Tag.NameWithoutLanguage, "profile-name", src.ProfileName));
            if (src.ProfileUri != null) attrs.Add(new IppAttribute(Tag.Uri, "profile-uri", src.ProfileUri.ToString()));
            return attrs;
        });
    }
}
