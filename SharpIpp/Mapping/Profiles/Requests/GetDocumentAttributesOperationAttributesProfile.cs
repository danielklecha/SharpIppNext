using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class GetDocumentAttributesOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, GetDocumentAttributesOperationAttributes>((src, dst, map) =>
        {
            dst ??= new GetDocumentAttributesOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(src, dst);
            dst.DocumentNumber = map.MapFromDicNullable<int?>(src, IppAttributeNames.DocumentNumber) ?? 0;
            var requestedAttributes = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.RequestedAttributes);
            if (requestedAttributes?.Any() ?? false)
                dst.RequestedAttributes = requestedAttributes;
            return dst;
        });

        mapper.CreateMap<GetDocumentAttributesOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.DocumentNumber, src.DocumentNumber));
            if (src.RequestedAttributes != null)
                dst.AddRange(src.RequestedAttributes.Select(requestedAttribute => new IppAttribute(Tag.Keyword, IppAttributeNames.RequestedAttributes, requestedAttribute)));
            return dst;
        });
    }
}
