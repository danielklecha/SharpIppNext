using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;

namespace SharpIpp.Mapping.Profiles.Responses;

internal class GetSystemAttributesResponseProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IppResponseMessage, GetSystemAttributesResponse>((src, map) =>
        {
            var dst = new GetSystemAttributesResponse();
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            if (src.SystemAttributes.Count > 0)
                dst.SystemAttributes = map.Map<IDictionary<string, IppAttribute[]>, SystemStatusAttributes>(src.SystemAttributes.SelectMany(x => x).ToIppDictionary());
            return dst;
        });

        mapper.CreateMap<IppResponseMessage, SystemStatusAttributes>((src, map) =>
        {
            return map.Map<IDictionary<string, IppAttribute[]>, SystemStatusAttributes>(src.SystemAttributes.SelectMany(x => x).ToIppDictionary());
        });

        mapper.CreateMap<IDictionary<string, IppAttribute[]>, SystemStatusAttributes>((src, map) =>
        {
            return new SystemStatusAttributes
            {
                SystemState = map.MapFromDicNullable<PrinterState?>(src, SystemAttribute.SystemState)
            };
        });

        mapper.CreateMap<GetSystemAttributesResponse, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppResponse, IppResponseMessage>(src, dst);
            if (src.SystemAttributes != null)
            {
                var systemAttrs = new List<IppAttribute>();
                if (src.SystemAttributes.SystemState.HasValue)
                    systemAttrs.Add(new IppAttribute(Tag.Enum, SystemAttribute.SystemState, (int)src.SystemAttributes.SystemState.Value));
                if (systemAttrs.Count > 0)
                    dst.SystemAttributes.Add(systemAttrs);
            }
            return dst;
        });
    }
}
