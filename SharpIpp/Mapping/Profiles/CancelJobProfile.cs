using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

// ReSharper disable once UnusedMember.Global
internal class CancelJobProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<CancelJobRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage {
                IppOperation = IppOperation.CancelJob
            };
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(src.OperationAttributes.GetIppAttributes(map));
            map.Map<IIppJobRequest, IppRequestMessage>(src, dst);
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, CancelJobRequest>( ( src, map ) =>
        {
            var dst = new CancelJobRequest()
            {
                OperationAttributes = CancelJobOperationAttributes.Create<CancelJobOperationAttributes>(src.OperationAttributes.ToIppDictionary(), map)
            };
            map.Map<IIppRequestMessage, IIppJobRequest>( src, dst );
            return dst;
        } );

        mapper.CreateMap<IppResponseMessage, CancelJobResponse>((src, map) =>
        {
            var dst = new CancelJobResponse();
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            return dst;
        });

        mapper.CreateMap<CancelJobResponse, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppResponse, IppResponseMessage>(src, dst);
            return dst;
        });
    }
}
