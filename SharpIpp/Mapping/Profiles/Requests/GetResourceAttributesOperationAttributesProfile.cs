using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class GetResourceAttributesOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, GetResourceAttributesOperationAttributes>((src, dst, map) =>
        {
            dst ??= new GetResourceAttributesOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.SystemUri = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.SystemUri);
            dst.ResourceId = map.MapFromDicNullable<int?>(src, IppAttributeNames.ResourceId);
            dst.RequestedAttributes = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.RequestedAttributes);
            return dst;
        });

        mapper.CreateMap<GetResourceAttributesOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<SystemOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.ResourceId.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.ResourceId, src.ResourceId.Value));
            if (src.RequestedAttributes != null)
                dst.AddRange(src.RequestedAttributes.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.RequestedAttributes, x)));
            return dst;
        });
    }
}
