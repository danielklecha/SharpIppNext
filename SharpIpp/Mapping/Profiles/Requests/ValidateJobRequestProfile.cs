using SharpIpp.Models.Responses;
using SharpIpp.Models.Requests;
using System;
using System.Collections.Generic;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

// ReSharper disable once UnusedMember.Global
internal class ValidateJobRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<ValidateJobRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage { IppOperation = IppOperation.ValidateJob };
            map.Map<IIppPrinterRequest, IppRequestMessage>(src, dst);

            if (src.JobTemplateAttributes != null)
            {
                map.Map(src.JobTemplateAttributes, dst);
            }

            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<ValidateJobOperationAttributes, List<IppAttribute>>(src.OperationAttributes));

            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, ValidateJobRequest>((src, map) =>
        {
            var dst = new ValidateJobRequest
            {
                JobTemplateAttributes = new JobTemplateAttributes()
            };
            map.Map<IIppRequestMessage, IIppPrinterRequest>(src, dst);
            map.Map(src, dst.JobTemplateAttributes);
            dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, ValidateJobOperationAttributes>(src.OperationAttributes.ToIppDictionary());
            return dst;
        });
    }
}
