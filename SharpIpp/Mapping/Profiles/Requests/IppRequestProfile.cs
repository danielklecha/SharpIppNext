using SharpIpp.Models.Responses;
using SharpIpp.Models.Requests;
using System;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

// ReSharper disable once UnusedMember.Global
internal class IppRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IIppRequest, IppRequestMessage>((src, dst, map) =>
        {
            dst ??= new IppRequestMessage();
            dst.Version = src.Version;
            dst.RequestId = src.RequestId;
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, IIppRequest>((src, dst, map) =>
        {
            dst = dst ?? throw new ArgumentNullException(nameof(dst));
            dst.Version = src.Version;
            dst.RequestId = src.RequestId;
            return dst;
        });

        mapper.CreateMap<IIppPrinterRequest, IppRequestMessage>((src, dst, map) =>
        {
            dst ??= new IppRequestMessage();
            map.Map<IIppRequest, IppRequestMessage>(src, dst);
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, IIppPrinterRequest>((src, dst, map) =>
        {
            dst = dst ?? throw new ArgumentNullException(nameof(dst));
            map.Map<IIppRequestMessage, IIppRequest>(src, dst);
            return dst;
        });
    }
}
