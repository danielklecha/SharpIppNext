using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class UpdateActiveJobsOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, UpdateActiveJobsOperationAttributes>((src, dst, map) =>
        {
            dst ??= new UpdateActiveJobsOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.OutputDeviceUuid = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.OutputDeviceUuid);
            dst.OutputDeviceJobStates = map.MapFromDicSetNullable<JobState[]?>(src, IppAttributeNames.OutputDeviceJobStates);
            dst.JobIds = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.JobIds);
            return dst;
        });

        mapper.CreateMap<UpdateActiveJobsOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.OutputDeviceUuid != null)
                dst.Add(new IppAttribute(Tag.Uri, IppAttributeNames.OutputDeviceUuid, src.OutputDeviceUuid.ToString()));
            if (src.OutputDeviceJobStates != null)
                dst.AddRange(src.OutputDeviceJobStates.Select(x => new IppAttribute(Tag.Enum, IppAttributeNames.OutputDeviceJobStates, (int)x)));
            if (src.JobIds != null)
                dst.AddRange(src.JobIds.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.JobIds, x)));
            return dst;
        });
    }
}
