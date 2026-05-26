using System.Collections.Generic;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

internal class JobPresetsSupportedProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobPresetsSupported>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<JobPresetsSupported>();

            return new JobPresetsSupported
            {
                PresetName = map.MapFromDicNullable<string?>(src, "preset-name"),
            };
        });
        mapper.CreateMap<JobPresetsSupported, IEnumerable<IppAttribute>>((src, _) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, IppAttributeNames.JobPresetsSupported, NoValue.Instance) };

            var attrs = new List<IppAttribute>();
            if (src.PresetName != null) attrs.Add(new IppAttribute(Tag.NameWithoutLanguage, "preset-name", src.PresetName));
            return attrs;
        });
    }
}
