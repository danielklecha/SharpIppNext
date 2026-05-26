using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class GetSystemSupportedValuesOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, GetSystemSupportedValuesOperationAttributes>((src, dst, map) =>
        {
            dst ??= new GetSystemSupportedValuesOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.SystemUri = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.SystemUri);
            dst.RequestedAttributes = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.RequestedAttributes);
            return dst;
        });

        mapper.CreateMap<GetSystemSupportedValuesOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<SystemOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.RequestedAttributes != null)
                dst.AddRange(src.RequestedAttributes.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.RequestedAttributes, x)));
            return dst;
        });
    }
}
