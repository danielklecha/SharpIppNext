using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class DeleteDocumentOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, DeleteDocumentOperationAttributes>((src, dst, map) =>
        {
            dst ??= new DeleteDocumentOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(src, dst);
            dst.DocumentNumber = map.MapFromDicNullable<int?>(src, IppAttributeNames.DocumentNumber);
            return dst;
        });

        mapper.CreateMap<DeleteDocumentOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.DocumentNumber.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.DocumentNumber, src.DocumentNumber.Value));
            return dst;
        });
    }
}
