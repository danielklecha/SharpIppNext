using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Responses;

internal abstract class AbstractResponseProfile : IProfile
{
    public abstract void CreateMaps(IMapperConstructor mapper);

    protected void AddMap<T>(IMapperConstructor mapper)
        where T : IIppResponse, new()
    {
        mapper.CreateMap<IppResponseMessage, T>((src, map) =>
        {
            var dst = new T();
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            return dst;
        });

        mapper.CreateMap<T, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppResponse, IppResponseMessage>(src, dst);
            return dst;
        });
    }
}
