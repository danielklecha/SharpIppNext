using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class AllocatePrinterResourcesOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, AllocatePrinterResourcesOperationAttributes>((src, dst, map) =>
        {
            dst ??= new AllocatePrinterResourcesOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.SystemUri = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.SystemUri);
            dst.PrinterId = map.MapFromDicNullable<int?>(src, IppAttributeNames.PrinterId);
            dst.ResourceIds = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.ResourceIds);
            return dst;
        });

        mapper.CreateMap<AllocatePrinterResourcesOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<SystemOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.PrinterId.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.PrinterId, src.PrinterId.Value));
            if (src.ResourceIds != null)
                dst.AddRange(src.ResourceIds.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.ResourceIds, x)));
            return dst;
        });
    }
}
