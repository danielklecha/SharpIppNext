using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;

namespace SharpIpp.Mapping.Profiles.Requests;

// ReSharper disable once UnusedMember.Global
internal class CloseJobRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<CloseJobRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage { IppOperation = IppOperation.CloseJob };
            map.Map<IIppPrinterRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<CloseJobOperationAttributes, List<IppAttribute>>(src.OperationAttributes));
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, CloseJobRequest>((src, map) =>
        {
            var dst = new CloseJobRequest
            {
                OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, CloseJobOperationAttributes>(src.OperationAttributes.ToIppDictionary())
            };
            map.Map<IIppRequestMessage, IIppPrinterRequest>(src, dst);
            return dst;
        });
    }
}
