using System;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class GetJobsOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, GetJobsOperationAttributes>((src, dst, map) =>
        {
            dst ??= new GetJobsOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.FirstIndex = map.MapFromDicNullable<int?>(src, JobAttribute.FirstIndex);
            dst.Limit = map.MapFromDicNullable<int?>(src, JobAttribute.Limit);
            dst.JobIds = map.MapFromDicSetNullable<int[]?>(src, JobAttribute.JobIds);
            var requestedAttributes = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.RequestedAttributes);
            if (requestedAttributes?.Any() ?? false)
                dst.RequestedAttributes = requestedAttributes;
            var whichJobs = map.MapFromDicNullable<string?>(src, JobAttribute.WhichJobs);
            if (whichJobs != null)
                dst.WhichJobs = map.Map<string, WhichJobs>(whichJobs);
            var myJobs = map.MapFromDicNullable<bool?>(src, JobAttribute.MyJobs);
            if (myJobs != null)
                dst.MyJobs = myJobs.Value;
            dst.OutputDeviceUuid = map.MapFromDicNullable<Uri?>(src, JobAttribute.OutputDeviceUuid);
            return dst;
        });

        mapper.CreateMap<GetJobsOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.FirstIndex != null)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.FirstIndex, src.FirstIndex.Value));
            if (src.Limit != null)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.Limit, src.Limit.Value));
            if (src.JobIds != null)
                dst.AddRange(src.JobIds.Select(x => new IppAttribute(Tag.Integer, JobAttribute.JobIds, x)));
            if (src.RequestedAttributes != null)
                dst.AddRange(src.RequestedAttributes.Select(requestedAttribute => new IppAttribute(Tag.Keyword, JobAttribute.RequestedAttributes, requestedAttribute)));
            if (src.WhichJobs != null)
                dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.WhichJobs, map.Map<string>(src.WhichJobs.Value)));
            if (src.MyJobs != null)
                dst.Add(new IppAttribute(Tag.Boolean, JobAttribute.MyJobs, src.MyJobs.Value));
            if (src.OutputDeviceUuid != null)
                dst.Add(new IppAttribute(Tag.Uri, JobAttribute.OutputDeviceUuid, src.OutputDeviceUuid.ToString()));
            return dst;
        });
    }
}
