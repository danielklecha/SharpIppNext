using SharpIpp.Models.Responses;
using SharpIpp.Models.Requests;
using System;
using System.Collections.Generic;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

// ReSharper disable once UnusedMember.Global
internal class PrintJobRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<PrintJobRequest, IppRequestMessage>((src, map) =>
        {
            if (src.Document == null)
            {
                throw new ArgumentException($"{nameof(src.Document)} must be set");
            }

            var dst = new IppRequestMessage { IppOperation = IppOperation.PrintJob, Document = src.Document };
            map.Map<IIppPrinterRequest, IppRequestMessage>(src, dst);

            if (src.JobTemplateAttributes != null)
            {
                map.Map(src.JobTemplateAttributes, dst);
            }
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<PrintJobOperationAttributes, List<IppAttribute>>(src.OperationAttributes));

            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, PrintJobRequest>((src, map) =>
        {
            if (src.Document == null)
            {
                throw new ArgumentException($"{nameof(src.Document)} must be set");
            }
            var dst = new PrintJobRequest { Document = src.Document, JobTemplateAttributes = new JobTemplateAttributes() };
            map.Map<IIppRequestMessage, IIppPrinterRequest>(src, dst);
            map.Map(src, dst.JobTemplateAttributes);
            dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, PrintJobOperationAttributes>(src.OperationAttributes.ToIppDictionary());
            return dst;
        });
    }
}
