using SharpIpp.Protocol;
using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Responses;

// ReSharper disable once UnusedMember.Global
internal class PrintUriResponseProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IppResponseMessage, PrintUriResponse>((src, map) =>
        {
            var dst = new PrintUriResponse();
            map.Map<IppResponseMessage, IIppJobResponse>(src, dst);
            return dst;
        });

        mapper.CreateMap<PrintUriResponse, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppJobResponse, IppResponseMessage>(src, dst);
            return dst;
        });
    }
}
