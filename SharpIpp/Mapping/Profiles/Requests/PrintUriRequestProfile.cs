using SharpIpp.Models.Responses;
using SharpIpp.Models.Requests;
using System;
using System.Collections.Generic;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

// ReSharper disable once UnusedMember.Global
internal class PrintUriRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<PrintUriRequest, IppRequestMessage>((src, map) =>
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (src.OperationAttributes?.DocumentUri == null)
            {
                throw new ArgumentException($"{nameof(JobAttribute.DocumentUri)} must be set");
            }

            var dst = new IppRequestMessage { IppOperation = IppOperation.PrintUri };
            map.Map<IIppPrinterRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<PrintUriOperationAttributes, List<IppAttribute>>(src.OperationAttributes));

            if (src.JobTemplateAttributes != null)
            {
                map.Map(src.JobTemplateAttributes, dst);
            }

            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, PrintUriRequest>((src, map) =>
        {
            var dst = new PrintUriRequest
            {
                JobTemplateAttributes = new JobTemplateAttributes()
            };
            map.Map<IIppRequestMessage, IIppPrinterRequest>(src, dst);
            map.Map(src, dst.JobTemplateAttributes);
            dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, PrintUriOperationAttributes>(src.OperationAttributes.ToIppDictionary());
            return dst;
        });
    }
}
