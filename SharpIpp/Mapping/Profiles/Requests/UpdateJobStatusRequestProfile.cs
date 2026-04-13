using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class UpdateJobStatusRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<UpdateJobStatusRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage { IppOperation = IppOperation.UpdateJobStatus };
            map.Map<IIppJobRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<UpdateJobStatusOperationAttributes, List<IppAttribute>>(src.OperationAttributes));
            if (src.JobAttributes != null)
                dst.JobAttributes.AddRange(map.Map<IDictionary<string, IppAttribute[]>>(src.JobAttributes).Values.SelectMany(x => x));
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, UpdateJobStatusRequest>((src, map) =>
        {
            var dst = new UpdateJobStatusRequest();
            map.Map<IIppRequestMessage, IIppJobRequest>(src, dst);
            dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, UpdateJobStatusOperationAttributes>(src.OperationAttributes.ToIppDictionary());
            if (src.JobAttributes.Any())
                dst.JobAttributes = map.Map<IDictionary<string, IppAttribute[]>, JobDescriptionAttributes>(src.JobAttributes.ToIppDictionary());
            return dst;
        });
    }
}
