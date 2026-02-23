using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles
{
    // ReSharper disable once UnusedMember.Global
    internal class RestartJobProfile : IProfile
    {
        public void CreateMaps(IMapperConstructor mapper)
        {
            mapper.CreateMap<RestartJobRequest, IppRequestMessage>((src, map) =>
            {
                var dst = new IppRequestMessage { IppOperation = IppOperation.RestartJob };
                map.Map<IIppJobRequest, IppRequestMessage>(src, dst);
                if (src.OperationAttributes != null)
                    dst.OperationAttributes.AddRange(src.OperationAttributes.GetIppAttributes(map));
                return dst;
            });

            mapper.CreateMap<IIppRequestMessage, RestartJobRequest>( ( src, map ) =>
            {
                var dst = new RestartJobRequest();
                map.Map<IIppRequestMessage, IIppJobRequest>( src, dst );
                dst.OperationAttributes = RestartJobOperationAttributes.Create<RestartJobOperationAttributes>(src.OperationAttributes.ToIppDictionary(), map);
                return dst;
            } );

            mapper.CreateMap<IppResponseMessage, RestartJobResponse>((src, map) =>
            {
                var dst = new RestartJobResponse();
                map.Map<IppResponseMessage, IIppResponse>(src, dst);
                return dst;
            });

            mapper.CreateMap<RestartJobResponse, IppResponseMessage>( ( src, map ) =>
            {
                var dst = new IppResponseMessage();
                map.Map<IIppResponse, IppResponseMessage>( src, dst );
                return dst;
            } );
        }
    }
}
