using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Responses;

// ReSharper disable once UnusedMember.Global
internal class GetPrinterAttributesResponseProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IppResponseMessage, GetPrinterAttributesResponse>((src, map) =>
        {
            var dst = new GetPrinterAttributesResponse
            {
                PrinterAttributes = map.Map<PrinterDescriptionAttributes>(src.PrinterAttributes.SelectMany(x => x).ToIppDictionary())
            };
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            return dst;
        });

        mapper.CreateMap<GetPrinterAttributesResponse, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppResponse, IppResponseMessage>(src, dst);
            if (src.PrinterAttributes != null)
            {
                var printerAttrs = new List<IppAttribute>();
                printerAttrs.AddRange(map.Map<IDictionary<string, IppAttribute[]>>(src.PrinterAttributes).Values.SelectMany(x => x));
                dst.PrinterAttributes.Add(printerAttrs);
            }
            return dst;
        });
    }
}
