using SharpIpp.Protocol;
using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Responses;

// ReSharper disable once UnusedMember.Global
internal class SendUriResponseProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IppResponseMessage, SendUriResponse>((src, map) =>
        {
            var dst = new SendUriResponse();
            map.Map<IppResponseMessage, IIppJobResponse>(src, dst);
            return dst;
        });

        mapper.CreateMap<SendUriResponse, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppJobResponse, IppResponseMessage>(src, dst);
            return dst;
        });
    }
}
