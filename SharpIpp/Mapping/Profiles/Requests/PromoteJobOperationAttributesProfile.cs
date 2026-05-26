using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class PromoteJobOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, PromoteJobOperationAttributes>((src, dst, map) =>
        {
            dst ??= new PromoteJobOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(src, dst);
            dst.JobMessageFromOperator = map.MapFromDicNullable<string?>(src, IppAttributeNames.JobMessageFromOperator);
            return dst;
        });

        mapper.CreateMap<PromoteJobOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.JobMessageFromOperator != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.JobMessageFromOperator, src.JobMessageFromOperator));
            return dst;
        });
    }
}
