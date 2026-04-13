using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Responses;

internal class DeregisterOutputDeviceResponseProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IppResponseMessage, DeregisterOutputDeviceResponse>((src, map) =>
        {
            var dst = new DeregisterOutputDeviceResponse();
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            return dst;
        });

        mapper.CreateMap<DeregisterOutputDeviceResponse, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppResponse, IppResponseMessage>(src, dst);
            return dst;
        });
    }
}
