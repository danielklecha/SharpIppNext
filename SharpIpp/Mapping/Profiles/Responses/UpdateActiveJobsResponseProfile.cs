using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Responses;

internal class UpdateActiveJobsResponseProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IppResponseMessage, UpdateActiveJobsResponse>((src, map) =>
        {
            var dst = new UpdateActiveJobsResponse();
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            var dic = src.OperationAttributes.SelectMany(x => x).ToIppDictionary();
            dst.JobIds = map.MapFromDicSetNullable<int[]?>(dic, JobAttribute.JobIds);
            dst.OutputDeviceJobStates = map.MapFromDicSetNullable<JobState[]?>(dic, JobAttribute.OutputDeviceJobStates);
            return dst;
        });

        mapper.CreateMap<UpdateActiveJobsResponse, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppResponse, IppResponseMessage>(src, dst);
            var operationAttrs = dst.OperationAttributes.First();
            if (src.JobIds != null)
                operationAttrs.AddRange(src.JobIds.Select(x => new IppAttribute(Tag.Integer, JobAttribute.JobIds, x)));
            if (src.OutputDeviceJobStates != null)
                operationAttrs.AddRange(src.OutputDeviceJobStates.Select(x => new IppAttribute(Tag.Enum, JobAttribute.OutputDeviceJobStates, (int)x)));
            return dst;
        });
    }
}
