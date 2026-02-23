using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

// ReSharper disable once UnusedMember.Global
internal class PurgeJobsProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<PurgeJobsRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage { IppOperation = IppOperation.PurgeJobs };
            map.Map<IIppPrinterRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(src.OperationAttributes.GetIppAttributes(map));
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, PurgeJobsRequest>( ( src, map ) =>
        {
            var dst = new PurgeJobsRequest();
            map.Map<IIppRequestMessage, IIppPrinterRequest>( src, dst );
            dst.OperationAttributes = PurgeJobsOperationAttributes.Create<PurgeJobsOperationAttributes>(src.OperationAttributes.ToIppDictionary(), map);
            return dst;
        } );

        mapper.CreateMap<IppResponseMessage, PurgeJobsResponse>((src, map) =>
        {
            var dst = new PurgeJobsResponse();
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            return dst;
        });

        mapper.CreateMap<PurgeJobsResponse, IppResponseMessage>( ( src, map ) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppResponse, IppResponseMessage>( src, dst );
            return dst;
        } );
    }
}
