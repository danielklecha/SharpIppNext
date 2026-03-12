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
internal class PrintUriResponseProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IppResponseMessage, PrintUriResponse>((src, map) =>
        {
            var dst = new PrintUriResponse();
            map.Map<IppResponseMessage, IIppJobResponse>(src, dst);
            if (src.DocumentAttributes.Count > 0)
            {
                var docAttrs = new DocumentAttributes();
                map.Map(src.DocumentAttributes.SelectMany(x => x).ToIppDictionary(), docAttrs);
                dst.DocumentAttributes = docAttrs;
            }
            return dst;
        });

        mapper.CreateMap<PrintUriResponse, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppJobResponse, IppResponseMessage>(src, dst);
            if (src.DocumentAttributes != null)
            {
                var docAttrs = new List<IppAttribute>();
                docAttrs.AddRange(map.Map<DocumentAttributes, IDictionary<string, IppAttribute[]>>(src.DocumentAttributes).Values.SelectMany(x => x));
                dst.DocumentAttributes.Add(docAttrs);
            }
            return dst;
        });
    }
}
