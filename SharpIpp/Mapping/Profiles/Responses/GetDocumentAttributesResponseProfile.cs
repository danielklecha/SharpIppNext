using SharpIpp.Models.Responses;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol;
using System.Linq;

namespace SharpIpp.Mapping.Profiles.Responses;

internal class GetDocumentAttributesResponseProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IppResponseMessage, GetDocumentAttributesResponse>((src, map) =>
        {
            var dst = new GetDocumentAttributesResponse();
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            if (src.DocumentAttributes.Any())
            {
                var docAttrs = new DocumentAttributes();
                map.Map(src.DocumentAttributes.SelectMany(x => x).ToIppDictionary(), docAttrs);
                dst.DocumentAttributes = docAttrs;
            }
            return dst;
        });

        mapper.CreateMap<GetDocumentAttributesResponse, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppResponse, IppResponseMessage>(src, dst);
            if (src.DocumentAttributes != null)
            {
                var docAttrs = map.Map<IEnumerable<IppAttribute>>(src.DocumentAttributes).ToList();
                dst.DocumentAttributes.Add(docAttrs);
            }
            return dst;
        });
    }
}
