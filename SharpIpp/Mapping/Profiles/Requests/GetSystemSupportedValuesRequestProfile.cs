using System.Collections.Generic;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class GetSystemSupportedValuesRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<GetSystemSupportedValuesRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage { IppOperation = IppOperation.GetSystemSupportedValues };
            map.Map<IIppSystemRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<GetSystemSupportedValuesOperationAttributes, List<IppAttribute>>(src.OperationAttributes));
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, GetSystemSupportedValuesRequest>((src, map) =>
        {
            var dst = new GetSystemSupportedValuesRequest();
            map.Map<IIppRequestMessage, IIppSystemRequest>(src, dst);
            dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, GetSystemSupportedValuesOperationAttributes>(src.OperationAttributes.ToIppDictionary());
            return dst;
        });
    }
}
