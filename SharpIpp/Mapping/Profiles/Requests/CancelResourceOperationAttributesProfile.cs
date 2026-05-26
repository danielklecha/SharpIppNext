using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class CancelResourceOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, CancelResourceOperationAttributes>((src, dst, map) =>
        {
            dst ??= new CancelResourceOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, SystemOperationAttributes>(src, dst);
            dst.ResourceId = map.MapFromDicNullable<int?>(src, IppAttributeNames.ResourceId);
            return dst;
        });

        mapper.CreateMap<CancelResourceOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<SystemOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.ResourceId.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.ResourceId, src.ResourceId.Value));
            return dst;
        });
    }
}
