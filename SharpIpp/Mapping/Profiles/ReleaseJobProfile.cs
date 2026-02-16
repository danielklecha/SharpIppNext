using SharpIpp.Models;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles
{
    // ReSharper disable once UnusedMember.Global
    internal class ReleaseJobProfile : IProfile
    {
        public void CreateMaps(IMapperConstructor mapper)
        {
            mapper.CreateMap<ReleaseJobRequest, IppRequestMessage>((src, map) =>
            {
                var dst = new IppRequestMessage { IppOperation = IppOperation.ReleaseJob };
                map.Map<IIppJobRequest, IppRequestMessage>(src, dst);
                if (src.OperationAttributes != null)
                    dst.OperationAttributes.AddRange(src.OperationAttributes.GetIppAttributes(map));
                return dst;
            });

            mapper.CreateMap<IIppRequestMessage, ReleaseJobRequest>( ( src, map ) =>
            {
                var dst = new ReleaseJobRequest();
                map.Map<IIppRequestMessage, IIppJobRequest>( src, dst );
                dst.OperationAttributes = ReleaseJobOperationAttributes.Create<ReleaseJobOperationAttributes>(src.OperationAttributes.ToIppDictionary(), map);
                return dst;
            } );

            mapper.CreateMap<IppResponseMessage, ReleaseJobResponse>((src, map) =>
            {
                var dst = new ReleaseJobResponse();
                map.Map<IppResponseMessage, IIppResponse>(src, dst);
                return dst;
            });

            mapper.CreateMap<ReleaseJobResponse, IppResponseMessage>( ( src, map ) =>
            {
                var dst = new IppResponseMessage();
                map.Map<IIppResponse, IppResponseMessage>( src, dst );
                return dst;
            } );
        }
    }
}
