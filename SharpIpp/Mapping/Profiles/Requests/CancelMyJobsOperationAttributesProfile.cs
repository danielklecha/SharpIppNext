using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class CancelMyJobsOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, CancelMyJobsOperationAttributes>((src, dst, map) =>
        {
            dst ??= new CancelMyJobsOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.JobIds = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.JobIds);
            dst.Message = map.MapFromDicNullable<string?>(src, IppAttributeNames.Message);
            return dst;
        });

        mapper.CreateMap<CancelMyJobsOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.JobIds != null)
                dst.AddRange(src.JobIds.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.JobIds, x)));
            if (src.Message != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.Message, src.Message));
            return dst;
        });
    }
}
