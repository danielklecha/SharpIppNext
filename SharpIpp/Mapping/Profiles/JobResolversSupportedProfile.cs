using System.Collections.Generic;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

internal class JobResolversSupportedProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobResolversSupported>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<JobResolversSupported>();

            return new JobResolversSupported
            {
                ResolverName = map.MapFromDicNullable<string?>(src, "resolver-name"),
            };
        });
        mapper.CreateMap<JobResolversSupported, IEnumerable<IppAttribute>>((src, _) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, PrinterAttribute.JobResolversSupported, NoValue.Instance) };

            var attrs = new List<IppAttribute>();
            if (src.ResolverName != null) attrs.Add(new IppAttribute(Tag.NameWithoutLanguage, "resolver-name", src.ResolverName));
            return attrs;
        });
    }
}
