using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;

namespace SharpIpp.Mapping.Profiles.Responses;

internal class AllocatePrinterResourcesResponseProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IppResponseMessage, AllocatePrinterResourcesResponse>((src, map) =>
        {
            var dst = new AllocatePrinterResourcesResponse();
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            var printerAttrsDict = src.PrinterAttributes.SelectMany(x => x).ToIppDictionary();
            dst.PrinterResourceIds = map.MapFromDicSetNullable<int[]?>(printerAttrsDict, PrinterAttribute.PrinterResourceIds);
            return dst;
        });

        mapper.CreateMap<AllocatePrinterResourcesResponse, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppResponse, IppResponseMessage>(src, dst);
            if (src.PrinterResourceIds != null && src.PrinterResourceIds.Length > 0)
            {
                var attrs = src.PrinterResourceIds
                    .Select(id => new IppAttribute(Tag.Integer, PrinterAttribute.PrinterResourceIds, id))
                    .ToList();
                dst.PrinterAttributes.Add(attrs);
            }
            return dst;
        });
    }
}

