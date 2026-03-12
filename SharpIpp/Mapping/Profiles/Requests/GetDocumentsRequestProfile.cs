using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class GetDocumentsRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<GetDocumentsRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage
            {
                IppOperation = IppOperation.GetDocuments
            };
            map.Map<IIppJobRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<GetDocumentsOperationAttributes, List<IppAttribute>>(src.OperationAttributes));
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, GetDocumentsRequest>((src, map) =>
        {
            var dst = new GetDocumentsRequest();
            map.Map<IIppRequestMessage, IIppJobRequest>(src, dst);
            dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, GetDocumentsOperationAttributes>(src.OperationAttributes.ToIppDictionary());
            return dst;
        });
    }
}
