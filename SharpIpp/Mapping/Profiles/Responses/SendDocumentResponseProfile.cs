using SharpIpp.Protocol;
using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;

namespace SharpIpp.Mapping.Profiles.Responses;

// ReSharper disable once UnusedMember.Global
internal class SendDocumentResponseProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IppResponseMessage, SendDocumentResponse>((src, map) =>
        {
            var dst = new SendDocumentResponse();
            map.Map<IppResponseMessage, IIppJobResponse>(src, dst);
            if (src.DocumentAttributes.Count > 0)
            {
                var docAttrs = new DocumentAttributes();
                map.Map(src.DocumentAttributes.SelectMany(x => x).ToIppDictionary(), docAttrs);
                dst.DocumentAttributes = docAttrs;
            }
            return dst;
        });

        mapper.CreateMap<SendDocumentResponse, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppJobResponse, IppResponseMessage>(src, dst);
            if (src.DocumentAttributes != null)
            {
                var docAttrsDict = map.Map<DocumentAttributes, IDictionary<string, IppAttribute[]>>(src.DocumentAttributes);
                dst.DocumentAttributes.Add(docAttrsDict.Values.SelectMany(x => x).ToList());
            }
            return dst;
        });
    }
}
