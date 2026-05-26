using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class GetNextDocumentDataOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, GetNextDocumentDataOperationAttributes>((src, dst, map) =>
        {
            dst ??= new GetNextDocumentDataOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.JobId = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobId);
            dst.DocumentDataWait = map.MapFromDicNullable<bool?>(src, IppAttributeNames.DocumentDataWait);
            return dst;
        });

        mapper.CreateMap<GetNextDocumentDataOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.JobId.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.JobId, src.JobId.Value));
            if (src.DocumentDataWait.HasValue)
                dst.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.DocumentDataWait, src.DocumentDataWait.Value));
            return dst;
        });
    }
}
