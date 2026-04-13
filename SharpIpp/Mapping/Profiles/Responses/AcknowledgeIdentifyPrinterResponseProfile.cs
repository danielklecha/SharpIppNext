using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Responses;

internal class AcknowledgeIdentifyPrinterResponseProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IppResponseMessage, AcknowledgeIdentifyPrinterResponse>((src, map) =>
        {
            var dst = new AcknowledgeIdentifyPrinterResponse();
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            return dst;
        });

        mapper.CreateMap<AcknowledgeIdentifyPrinterResponse, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppResponse, IppResponseMessage>(src, dst);
            return dst;
        });
    }
}
