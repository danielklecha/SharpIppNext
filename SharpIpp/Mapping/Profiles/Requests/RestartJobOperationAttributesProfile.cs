using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class RestartJobOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, RestartJobOperationAttributes>((src, dst, map) =>
        {
            dst ??= new RestartJobOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, CancelJobOperationAttributes>(src, dst);
            dst.JobHoldUntil = map.MapFromDicNullable<JobHoldUntil?>(src, IppAttributeNames.JobHoldUntil);
            return dst;
        });

        mapper.CreateMap<RestartJobOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<CancelJobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.JobHoldUntil != null)
                dst.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.JobHoldUntil, map.Map<string>(src.JobHoldUntil.Value)));
            return dst;
        });
    }
}
