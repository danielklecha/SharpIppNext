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
                IccProfileResourceId = map.MapFromDicNullable<int?>(src, "icc-profile-resource-id"),
            };
        });
        mapper.CreateMap<PrintColorModeIccProfile, IEnumerable<IppAttribute>>((src, _) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, PrinterAttribute.PrintColorModeIccProfiles, NoValue.Instance) };

            var attrs = new List<IppAttribute>();
            if (src.PrintColorMode != null) attrs.Add(new IppAttribute(Tag.Keyword, "print-color-mode", src.PrintColorMode));
            if (src.IccProfileResourceId.HasValue) attrs.Add(new IppAttribute(Tag.Integer, "icc-profile-resource-id", src.IccProfileResourceId.Value));
            return attrs;
        });
    }
}
