using System.Collections.Generic;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

internal class JobConstraintsSupportedProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, JobConstraintsSupported>((src, map) =>
        {
            if (src.IsOutOfBandNoValue())
                return NoValue.GetNoValue<JobConstraintsSupported>();

            return new JobConstraintsSupported
            {
                ResolverName = map.MapFromDicNullable<string?>(src, "resolver-name"),
            };
        });
        mapper.CreateMap<JobConstraintsSupported, IEnumerable<IppAttribute>>((src, _) =>
        {
            if (NoValue.IsNoValue(src))
                return new[] { new IppAttribute(Tag.NoValue, PrinterAttribute.JobConstraintsSupported, NoValue.Instance) };

            var attrs = new List<IppAttribute>();
            if (src.ResolverName != null) attrs.Add(new IppAttribute(Tag.NameWithoutLanguage, "resolver-name", src.ResolverName));
            return attrs;
        });
    }
}
