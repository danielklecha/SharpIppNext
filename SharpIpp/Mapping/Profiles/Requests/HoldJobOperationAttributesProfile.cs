using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class HoldJobOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, HoldJobOperationAttributes>((src, dst, map) =>
        {
            dst ??= new HoldJobOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, CancelJobOperationAttributes>(src, dst);
            dst.JobHoldUntil = map.MapFromDicNullable<JobHoldUntil?>(src, JobAttribute.JobHoldUntil);
            dst.JobHoldUntilTime = map.MapFromDicNullable<DateTimeOffset?>(src, JobAttribute.JobHoldUntilTime);
            return dst;
        });

        mapper.CreateMap<HoldJobOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<CancelJobOperationAttributes, List<IppAttribute>>(src, dst);
            if (src.JobHoldUntil != null)
                dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.JobHoldUntil, map.Map<string>(src.JobHoldUntil.Value)));
            if (src.JobHoldUntilTime != null)
                dst.Add(new IppAttribute(Tag.DateTime, JobAttribute.JobHoldUntilTime, src.JobHoldUntilTime.Value));
            return dst;
        });
    }
}
