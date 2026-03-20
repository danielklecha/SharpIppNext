using System.Collections.Generic;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class GetOutputDeviceAttributesRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<GetOutputDeviceAttributesRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage { IppOperation = IppOperation.GetOutputDeviceAttributes };
            map.Map<IIppPrinterRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<GetOutputDeviceAttributesOperationAttributes, List<IppAttribute>>(src.OperationAttributes));
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, GetOutputDeviceAttributesRequest>((src, map) =>
        {
            var dst = new GetOutputDeviceAttributesRequest();
            map.Map<IIppRequestMessage, IIppPrinterRequest>(src, dst);
            dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, GetOutputDeviceAttributesOperationAttributes>(src.OperationAttributes.ToIppDictionary());
            return dst;
        });
    }
}
