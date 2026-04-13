using System.Collections.Generic;
using System.Linq;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Responses;

internal class FetchJobResponseProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IppResponseMessage, FetchJobResponse>((src, map) =>
        {
            var dst = new FetchJobResponse();
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            if (src.JobAttributes.Any())
                dst.JobAttributes = map.Map<IDictionary<string, IppAttribute[]>, JobDescriptionAttributes>(src.JobAttributes.SelectMany(x => x).ToIppDictionary());
            return dst;
        });

        mapper.CreateMap<FetchJobResponse, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppResponse, IppResponseMessage>(src, dst);
            if (src.JobAttributes != null)
                dst.JobAttributes.Add(map.Map<IDictionary<string, IppAttribute[]>>(src.JobAttributes).Values.SelectMany(x => x).ToList());
            return dst;
        });
    }
}
