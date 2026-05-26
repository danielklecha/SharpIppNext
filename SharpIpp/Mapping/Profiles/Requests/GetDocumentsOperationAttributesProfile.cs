using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class GetDocumentsOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, GetDocumentsOperationAttributes>((src, dst, map) =>
        {
            dst ??= new GetDocumentsOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(src, dst);
            dst.FirstIndex = map.MapFromDicNullable<int?>(src, IppAttributeNames.FirstIndex);
            dst.Limit = map.MapFromDicNullable<int?>(src, IppAttributeNames.Limit);
            dst.RequestedAttributes = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.RequestedAttributes);
            return dst;
        });

        mapper.CreateMap<GetDocumentsOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.FirstIndex.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.FirstIndex, src.FirstIndex.Value));
            if (src.Limit.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.Limit, src.Limit.Value));
            if (src.RequestedAttributes != null)
                dst.AddRange(src.RequestedAttributes.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.RequestedAttributes, x)));
            return dst;
        });
    }
}
