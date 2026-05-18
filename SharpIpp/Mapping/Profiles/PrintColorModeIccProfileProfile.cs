using System;
using System.Collections.Generic;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

internal class PrintColorModeIccProfileProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PrintColorModeIccProfile>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<PrintColorModeIccProfile>();

            return new PrintColorModeIccProfile
            {
                PrintColorMode = map.MapFromDicNullable<string?>(src, "print-color-mode"),
                ProfileUri = map.MapFromDicNullable<Uri?>(src, "profile-uri"),
            };
        });
        mapper.CreateMap<PrintColorModeIccProfile, IEnumerable<IppAttribute>>((src, _) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, PrinterAttribute.PrintColorModeIccProfiles, NoValue.Instance) };

            var attrs = new List<IppAttribute>();
            if (src.PrintColorMode != null) attrs.Add(new IppAttribute(Tag.Keyword, "print-color-mode", src.PrintColorMode));
            if (src.ProfileUri != null) attrs.Add(new IppAttribute(Tag.Uri, "profile-uri", src.ProfileUri.ToString()));
            return attrs;
        });
    }
}
