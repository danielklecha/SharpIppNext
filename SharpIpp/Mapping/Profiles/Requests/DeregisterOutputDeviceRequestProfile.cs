using System.Collections.Generic;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class DeregisterOutputDeviceRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<DeregisterOutputDeviceRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage { IppOperation = IppOperation.DeregisterOutputDevice };
            map.Map<IIppPrinterRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<DeregisterOutputDeviceOperationAttributes, List<IppAttribute>>(src.OperationAttributes));
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, DeregisterOutputDeviceRequest>((src, map) =>
        {
            var dst = new DeregisterOutputDeviceRequest();
            map.Map<IIppRequestMessage, IIppPrinterRequest>(src, dst);
            dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, DeregisterOutputDeviceOperationAttributes>(src.OperationAttributes.ToIppDictionary());
            return dst;
        });
    }
}
