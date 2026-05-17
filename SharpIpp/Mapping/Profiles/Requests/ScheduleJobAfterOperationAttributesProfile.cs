using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class ScheduleJobAfterOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, ScheduleJobAfterOperationAttributes>((src, dst, map) =>
        {
            dst ??= new ScheduleJobAfterOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(src, dst);
            dst.PredecessorJobId = map.MapFromDicNullable<int?>(src, JobAttribute.PredecessorJobId);
            dst.JobMessageFromOperator = map.MapFromDicNullable<string?>(src, JobAttribute.JobMessageFromOperator);
            return dst;
        });

        mapper.CreateMap<ScheduleJobAfterOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.PredecessorJobId.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.PredecessorJobId, src.PredecessorJobId.Value));
            if (src.JobMessageFromOperator != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.JobMessageFromOperator, src.JobMessageFromOperator));
            return dst;
        });
    }
}
