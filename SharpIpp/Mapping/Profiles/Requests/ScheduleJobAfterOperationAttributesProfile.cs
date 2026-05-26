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
            dst.PredecessorJobId = map.MapFromDicNullable<int?>(src, IppAttributeNames.PredecessorJobId);
            dst.JobMessageFromOperator = map.MapFromDicNullable<string?>(src, IppAttributeNames.JobMessageFromOperator);
            return dst;
        });

        mapper.CreateMap<ScheduleJobAfterOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<JobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.PredecessorJobId.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.PredecessorJobId, src.PredecessorJobId.Value));
            if (src.JobMessageFromOperator != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.JobMessageFromOperator, src.JobMessageFromOperator));
            return dst;
        });
    }
}
