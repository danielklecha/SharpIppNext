using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;

namespace SharpIpp.Mapping.Profiles.Requests;

// ReSharper disable once UnusedMember.Global
internal class ResubmitJobRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<ResubmitJobRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage { IppOperation = IppOperation.ResubmitJob };
            map.Map<IIppPrinterRequest, IppRequestMessage>(src, dst);

            if (src.JobTemplateAttributes != null)
            {
                map.Map(src.JobTemplateAttributes, dst);
            }

            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<ResubmitJobOperationAttributes, List<IppAttribute>>(src.OperationAttributes));

            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, ResubmitJobRequest>((src, map) =>
        {
            var dst = new ResubmitJobRequest { JobTemplateAttributes = new JobTemplateAttributes() };
            map.Map<IIppRequestMessage, IIppPrinterRequest>(src, dst);
            map.Map(src, dst.JobTemplateAttributes);
            dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, ResubmitJobOperationAttributes>(src.OperationAttributes.ToIppDictionary());
            return dst;
        });
    }
}
