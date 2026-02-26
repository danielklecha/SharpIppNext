using SharpIpp.Models.Responses;
using SharpIpp.Models.Requests;
using System;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

// ReSharper disable once UnusedMember.Global
internal class IppJobRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IIppJobRequest, IppRequestMessage>((src, dst, map) =>
        {
            dst ??= new IppRequestMessage();
            map.Map<IIppRequest, IppRequestMessage>(src, dst);
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, IIppJobRequest>((src, dst, map) =>
        {
            dst = dst ?? throw new ArgumentNullException(nameof(dst));
            map.Map<IIppRequestMessage, IIppRequest>(src, dst);
            return dst;
        });
    }
}
