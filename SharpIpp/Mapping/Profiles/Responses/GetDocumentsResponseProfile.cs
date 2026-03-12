using SharpIpp.Models.Responses;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol;

namespace SharpIpp.Mapping.Profiles.Responses;

internal class GetDocumentsResponseProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IppResponseMessage, GetDocumentsResponse>((src, map) =>
        {
            var dst = new GetDocumentsResponse();
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            dst.Documents = src.DocumentAttributes
                .Select(x => map.Map<IDictionary<string, IppAttribute[]>, DocumentAttributes>(x.ToIppDictionary()))
                .ToList();
            return dst;
        });

        mapper.CreateMap<GetDocumentsResponse, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppResponse, IppResponseMessage>(src, dst);
            if (src.Documents != null)
                dst.DocumentAttributes.AddRange(src.Documents.Select(x => map.Map<IDictionary<string, IppAttribute[]>>(x).SelectMany(y => y.Value).ToList()));
            return dst;
        });
    }
}
