using System.Collections.Generic;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class GetUserPrinterAttributesRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<GetUserPrinterAttributesRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage { IppOperation = IppOperation.GetUserPrinterAttributes };
            map.Map<IIppPrinterRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<GetUserPrinterAttributesOperationAttributes, List<IppAttribute>>(src.OperationAttributes));
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, GetUserPrinterAttributesRequest>((src, map) =>
        {
            var dst = new GetUserPrinterAttributesRequest();
            map.Map<IIppRequestMessage, IIppPrinterRequest>(src, dst);
            dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, GetUserPrinterAttributesOperationAttributes>(src.OperationAttributes.ToIppDictionary());
            return dst;
        });
    }
}
