using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Responses;

internal class UpdateDocumentStatusResponseProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IppResponseMessage, UpdateDocumentStatusResponse>((src, map) =>
        {
            var dst = new UpdateDocumentStatusResponse();
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            return dst;
        });

        mapper.CreateMap<UpdateDocumentStatusResponse, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppResponse, IppResponseMessage>(src, dst);
            return dst;
        });
    }
}
