using System.Collections.Generic;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class AcknowledgeDocumentRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<AcknowledgeDocumentRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage { IppOperation = IppOperation.AcknowledgeDocument };
            map.Map<IIppJobRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<AcknowledgeDocumentOperationAttributes, List<IppAttribute>>(src.OperationAttributes));
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, AcknowledgeDocumentRequest>((src, map) =>
        {
            var dst = new AcknowledgeDocumentRequest();
            map.Map<IIppRequestMessage, IIppJobRequest>(src, dst);
            dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, AcknowledgeDocumentOperationAttributes>(src.OperationAttributes.ToIppDictionary());
            return dst;
        });
    }
}
