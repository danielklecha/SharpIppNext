using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Responses;

// ReSharper disable once UnusedMember.Global
internal class GetCUPSPrintersResponseProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IppResponseMessage, CUPSGetPrintersResponse>((src, map) =>
        {
            var dst = new CUPSGetPrintersResponse { PrintersAttributes = map.Map<List<List<IppAttribute>>, PrinterDescriptionAttributes[]>(src.PrinterAttributes) };
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            return dst;
        });

        mapper.CreateMap<CUPSGetPrintersResponse, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            if (src.PrintersAttributes != null)
                dst.PrinterAttributes.AddRange(map.Map<PrinterDescriptionAttributes[], List<List<IppAttribute>>>(src.PrintersAttributes));
            map.Map<IIppResponse, IppResponseMessage>(src, dst);
            return dst;
        });
    }
}
