using System.Collections.Generic;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class AllocatePrinterResourcesRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<AllocatePrinterResourcesRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage { IppOperation = IppOperation.AllocatePrinterResources };
            map.Map<IIppSystemRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<AllocatePrinterResourcesOperationAttributes, List<IppAttribute>>(src.OperationAttributes));
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, AllocatePrinterResourcesRequest>((src, map) =>
        {
            var dst = new AllocatePrinterResourcesRequest();
            map.Map<IIppRequestMessage, IIppSystemRequest>(src, dst);
            dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, AllocatePrinterResourcesOperationAttributes>(src.OperationAttributes.ToIppDictionary());
            return dst;
        });
    }
}
