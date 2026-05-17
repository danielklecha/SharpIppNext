using System.Collections.Generic;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

internal class JobTriggersSupportedProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobTriggersSupported>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<JobTriggersSupported>();

            return new JobTriggersSupported
            {
                TriggerName = map.MapFromDicNullable<string?>(src, "trigger-name"),
            };
        });
        mapper.CreateMap<JobTriggersSupported, IEnumerable<IppAttribute>>((src, _) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, PrinterAttribute.JobTriggersSupported, NoValue.Instance) };

            var attrs = new List<IppAttribute>();
            if (src.TriggerName != null) attrs.Add(new IppAttribute(Tag.NameWithoutLanguage, "trigger-name", src.TriggerName));
            return attrs;
        });
    }
}
