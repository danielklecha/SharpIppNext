using System.Collections.Generic;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class GetNextDocumentDataRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<GetNextDocumentDataRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage { IppOperation = IppOperation.GetNextDocumentData };
            map.Map<IIppJobRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<GetNextDocumentDataOperationAttributes, List<IppAttribute>>(src.OperationAttributes));
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, GetNextDocumentDataRequest>((src, map) =>
        {
            var dst = new GetNextDocumentDataRequest();
            map.Map<IIppRequestMessage, IIppJobRequest>(src, dst);
            dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, GetNextDocumentDataOperationAttributes>(src.OperationAttributes.ToIppDictionary());
            return dst;
        });
    }
}
