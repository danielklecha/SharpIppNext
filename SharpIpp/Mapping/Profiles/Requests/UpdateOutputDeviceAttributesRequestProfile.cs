using System.Collections.Generic;
using System.Linq;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class UpdateOutputDeviceAttributesRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<UpdateOutputDeviceAttributesRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage { IppOperation = IppOperation.UpdateOutputDeviceAttributes };
            map.Map<IIppPrinterRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<UpdateOutputDeviceAttributesOperationAttributes, List<IppAttribute>>(src.OperationAttributes));
            if (src.PrinterAttributes != null)
                dst.PrinterAttributes.AddRange(map.Map<IDictionary<string, IppAttribute[]>>(src.PrinterAttributes).Values.SelectMany(x => x));
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, UpdateOutputDeviceAttributesRequest>((src, map) =>
        {
            var dst = new UpdateOutputDeviceAttributesRequest();
            map.Map<IIppRequestMessage, IIppPrinterRequest>(src, dst);
            dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, UpdateOutputDeviceAttributesOperationAttributes>(src.OperationAttributes.ToIppDictionary());
            if (src.PrinterAttributes.Any())
                dst.PrinterAttributes = map.Map<IDictionary<string, IppAttribute[]>, PrinterDescriptionAttributes>(src.PrinterAttributes.ToIppDictionary());
            return dst;
        });
    }
}
