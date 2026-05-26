using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class ResumeJobOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, ResumeJobOperationAttributes>((src, dst, map) =>
        {
            dst ??= new ResumeJobOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(src, dst);
            dst.JobMessageFromOperator = map.MapFromDicNullable<string?>(src, IppAttributeNames.JobMessageFromOperator);
            return dst;
        });

        mapper.CreateMap<ResumeJobOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.JobMessageFromOperator != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.JobMessageFromOperator, src.JobMessageFromOperator));
            return dst;
        });
    }
}
