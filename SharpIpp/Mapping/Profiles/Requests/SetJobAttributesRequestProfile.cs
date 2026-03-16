using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class SetJobAttributesRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<SetJobAttributesRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage
            {
                IppOperation = IppOperation.SetJobAttributes
            };
            map.Map<IIppJobRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<SetJobAttributesOperationAttributes, List<IppAttribute>>(src.OperationAttributes));
            if (src.JobTemplateAttributes != null)
                map.Map(src.JobTemplateAttributes, dst);
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, SetJobAttributesRequest>((src, map) =>
        {
            var dst = new SetJobAttributesRequest
            {
                JobTemplateAttributes = new JobTemplateAttributes()
            };
            map.Map<IIppRequestMessage, IIppJobRequest>(src, dst);
            map.Map(src, dst.JobTemplateAttributes);
            dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, SetJobAttributesOperationAttributes>(src.OperationAttributes.ToIppDictionary());
            return dst;
        });
    }
}
