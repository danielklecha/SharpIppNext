using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

// ReSharper disable once UnusedMember.Global
internal class HoldJobProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<HoldJobRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage { IppOperation = IppOperation.HoldJob };
            map.Map<IIppJobRequest, IppRequestMessage>(src, dst);
            if(src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(src.OperationAttributes.GetIppAttributes(map));
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, HoldJobRequest>( ( src, map ) =>
        {
            var dst = new HoldJobRequest();
            map.Map<IIppRequestMessage, IIppJobRequest>( src, dst );
            dst.OperationAttributes = HoldJobOperationAttributes.Create<HoldJobOperationAttributes>(src.OperationAttributes.ToIppDictionary(), map);
            return dst;
        } );

        mapper.CreateMap<IppResponseMessage, HoldJobResponse>((src, map) =>
        {
            var dst = new HoldJobResponse();
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            return dst;
        });

        mapper.CreateMap<HoldJobResponse, IppResponseMessage>( ( src, map ) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppResponse, IppResponseMessage>( src, dst );
            return dst;
        } );
    }
}
