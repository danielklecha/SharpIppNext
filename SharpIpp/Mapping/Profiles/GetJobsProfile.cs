using System.Collections.Generic;
using System.Linq;
using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles
{
    // ReSharper disable once UnusedMember.Global
    internal class GetJobsProfile : IProfile
    {
        public void CreateMaps(IMapperConstructor mapper)
        {
            // https://tools.ietf.org/html/rfc2911#section-3.3.4.1
            mapper.CreateMap<GetJobsRequest, IppRequestMessage>((src, map) =>
            {
                var dst = new IppRequestMessage { IppOperation = IppOperation.GetJobs };
                map.Map<IIppPrinterRequest, IppRequestMessage>(src, dst);
                if(src.OperationAttributes != null)
                    dst.OperationAttributes.AddRange(src.OperationAttributes.GetIppAttributes(map));
                return dst;
            });

            mapper.CreateMap<IIppRequestMessage, GetJobsRequest>( ( src, map ) =>
            {
                var dst = new GetJobsRequest();
                map.Map<IIppRequestMessage, IIppPrinterRequest>( src, dst );
                dst.OperationAttributes = GetJobsOperationAttributes.Create<GetJobsOperationAttributes>(src.OperationAttributes.ToIppDictionary(), map);
                return dst;
            } );

            // https://tools.ietf.org/html/rfc2911#section-3.3.4.2
            mapper.CreateMap<IppResponseMessage, GetJobsResponse>((src, map) =>
            {
                var dst = new GetJobsResponse { JobsAttributes = map.Map<List<List<IppAttribute>>, JobDescriptionAttributes[]>(src.JobAttributes) };
                map.Map<IppResponseMessage, IIppResponse>(src, dst);
                return dst;
            });

            mapper.CreateMap<GetJobsResponse, IppResponseMessage>((src, map) =>
            {
                var dst = new IppResponseMessage();
                map.Map<IIppResponse, IppResponseMessage>( src, dst );
                dst.JobAttributes.AddRange(map.Map<JobDescriptionAttributes[], List<List<IppAttribute>>>(src.JobsAttributes ?? []));
                return dst;
            } );

            //https://tools.ietf.org/html/rfc2911#section-4.4
            mapper.CreateMap<List<List<IppAttribute>>, JobDescriptionAttributes[]>((src, map) =>
                src.Select(x => map.Map<JobDescriptionAttributes>(x.ToIppDictionary()))
                    .ToArray());

            mapper.CreateMap<JobDescriptionAttributes[], List<List<IppAttribute>>>( (src, map) =>
            {
                return src.Select(x =>
                {
                    var attrs = new List<IppAttribute>();
                    attrs.AddRange( map.Map<IDictionary<string, IppAttribute[]>>( x ).Values.SelectMany( x => x ) );
                    return attrs;
                }).ToList();
            });
        }
    }
}
