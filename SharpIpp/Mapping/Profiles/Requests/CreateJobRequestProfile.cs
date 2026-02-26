using SharpIpp.Models.Responses;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;

namespace SharpIpp.Mapping.Profiles.Requests;

// ReSharper disable once UnusedMember.Global
internal class CreateJobRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<CreateJobRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage { IppOperation = IppOperation.CreateJob };
            map.Map<IIppPrinterRequest, IppRequestMessage>(src, dst);
            if (src.JobTemplateAttributes != null)
                map.Map(src.JobTemplateAttributes, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<CreateJobOperationAttributes, List<IppAttribute>>(src.OperationAttributes));
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, CreateJobRequest>((src, map) =>
        {
            var dst = new CreateJobRequest { JobTemplateAttributes = new JobTemplateAttributes() };
            map.Map<IIppRequestMessage, IIppPrinterRequest>(src, dst);
            map.Map(src, dst.JobTemplateAttributes);
            dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, CreateJobOperationAttributes>(src.OperationAttributes.ToIppDictionary());
            return dst;
        });
    }
}
