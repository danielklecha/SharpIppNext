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
            dst.SystemUri = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.SystemUri);
            dst.ResourceIds = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.ResourceIds);
            dst.RequestedAttributes = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.RequestedAttributes);
            return dst;
        });

        mapper.CreateMap<GetResourcesOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<SystemOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.ResourceIds != null)
                dst.AddRange(src.ResourceIds.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.ResourceIds, x)));
            if (src.RequestedAttributes != null)
                dst.AddRange(src.RequestedAttributes.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.RequestedAttributes, x)));
            return dst;
        });
    }
}
