using System.Collections.Generic;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class AcknowledgeIdentifyPrinterRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<AcknowledgeIdentifyPrinterRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage { IppOperation = IppOperation.AcknowledgeIdentifyPrinter };
            map.Map<IIppPrinterRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<AcknowledgeIdentifyPrinterOperationAttributes, List<IppAttribute>>(src.OperationAttributes));
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, AcknowledgeIdentifyPrinterRequest>((src, map) =>
        {
            var dst = new AcknowledgeIdentifyPrinterRequest();
            map.Map<IIppRequestMessage, IIppPrinterRequest>(src, dst);
            dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, AcknowledgeIdentifyPrinterOperationAttributes>(src.OperationAttributes.ToIppDictionary());
            return dst;
        });
    }
}
