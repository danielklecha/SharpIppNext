using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Responses;

// ReSharper disable once UnusedMember.Global
internal class GetJobsResponseProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
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
            map.Map<IIppResponse, IppResponseMessage>(src, dst);
            if (src.JobsAttributes != null)
                dst.JobAttributes.AddRange(map.Map<JobDescriptionAttributes[], List<List<IppAttribute>>>(src.JobsAttributes));
            return dst;
        });

        //https://tools.ietf.org/html/rfc2911#section-4.4
        mapper.CreateMap<List<List<IppAttribute>>, JobDescriptionAttributes[]>((src, map) =>
            src.Select(x => map.Map<JobDescriptionAttributes>(x.ToIppDictionary()))
                .ToArray());

        mapper.CreateMap<JobDescriptionAttributes[], List<List<IppAttribute>>>((src, map) =>
        {
            return src.Select(x =>
            {
                var attrs = new List<IppAttribute>();
                attrs.AddRange(map.Map<IDictionary<string, IppAttribute[]>>(x).Values.SelectMany(x => x));
                return attrs;
            }).ToList();
        });
    }
}
