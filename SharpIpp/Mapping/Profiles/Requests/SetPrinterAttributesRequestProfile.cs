using System.Collections.Generic;
using System.Linq;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class SetPrinterAttributesRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<SetPrinterAttributesRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage
            {
                IppOperation = IppOperation.SetPrinterAttributes
            };
            map.Map<IIppPrinterRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<SetPrinterAttributesOperationAttributes, List<IppAttribute>>(src.OperationAttributes));
            if (src.PrinterAttributes != null)
            {
                var printerAttributes = map.Map<IDictionary<string, IppAttribute[]>>(src.PrinterAttributes);
                dst.PrinterAttributes.AddRange(printerAttributes.Values.SelectMany(x => x));
            }
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, SetPrinterAttributesRequest>((src, map) =>
        {
            var dst = new SetPrinterAttributesRequest();
            map.Map<IIppRequestMessage, IIppPrinterRequest>(src, dst);
            dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, SetPrinterAttributesOperationAttributes>(src.OperationAttributes.ToIppDictionary());
            if (src.PrinterAttributes.Any())
                dst.PrinterAttributes = map.Map<PrinterDescriptionAttributes>(src.PrinterAttributes.ToIppDictionary());
            return dst;
        });
    }
}
