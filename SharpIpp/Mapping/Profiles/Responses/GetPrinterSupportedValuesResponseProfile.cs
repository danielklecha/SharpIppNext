using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;

namespace SharpIpp.Mapping.Profiles.Responses;

internal class GetPrinterSupportedValuesResponseProfile : AbstractResponseProfile
{
    public override void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IppResponseMessage, GetPrinterSupportedValuesResponse>((src, map) =>
        {
            var dst = new GetPrinterSupportedValuesResponse
            {
                PrinterAttributes = map.Map<PrinterDescriptionAttributes>(src.PrinterAttributes.SelectMany(x => x).ToIppDictionary())
            };
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            return dst;
        });

        mapper.CreateMap<GetPrinterSupportedValuesResponse, IppResponseMessage>((src, map) =>
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
