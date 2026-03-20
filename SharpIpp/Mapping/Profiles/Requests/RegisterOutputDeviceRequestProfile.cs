using System.Collections.Generic;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class RegisterOutputDeviceRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<RegisterOutputDeviceRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage { IppOperation = IppOperation.RegisterOutputDevice };
            map.Map<IIppSystemRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<RegisterOutputDeviceOperationAttributes, List<IppAttribute>>(src.OperationAttributes));
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, RegisterOutputDeviceRequest>((src, map) =>
        {
            var dst = new RegisterOutputDeviceRequest();
            map.Map<IIppRequestMessage, IIppSystemRequest>(src, dst);
            dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, RegisterOutputDeviceOperationAttributes>(src.OperationAttributes.ToIppDictionary());
            return dst;
        });
    }
}
