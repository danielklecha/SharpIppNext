using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class GetResourcesOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, GetResourcesOperationAttributes>((src, dst, map) =>
        {
            dst ??= new GetResourcesOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.SystemUri = map.MapFromDicNullable<Uri?>(src, SystemAttribute.SystemUri);
            dst.ResourceIds = map.MapFromDicSetNullable<int[]?>(src, SystemAttribute.ResourceIds);
            dst.RequestedAttributes = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.RequestedAttributes);
            return dst;
        });

        mapper.CreateMap<GetResourcesOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<SystemOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.ResourceIds != null)
                dst.AddRange(src.ResourceIds.Select(x => new IppAttribute(Tag.Integer, SystemAttribute.ResourceIds, x)));
            if (src.RequestedAttributes != null)
                dst.AddRange(src.RequestedAttributes.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.RequestedAttributes, x)));
            return dst;
        });
    }
}
