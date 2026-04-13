using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Responses;

internal class UpdateJobStatusResponseProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IppResponseMessage, UpdateJobStatusResponse>((src, map) =>
        {
            var dst = new UpdateJobStatusResponse();
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            return dst;
        });

        mapper.CreateMap<UpdateJobStatusResponse, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppResponse, IppResponseMessage>(src, dst);
            return dst;
        });
    }
}
