using System.Collections.Generic;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class FetchJobRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<FetchJobRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage { IppOperation = IppOperation.FetchJob };
            map.Map<IIppJobRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<FetchJobOperationAttributes, List<IppAttribute>>(src.OperationAttributes));
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, FetchJobRequest>((src, map) =>
        {
            var dst = new FetchJobRequest();
            map.Map<IIppRequestMessage, IIppJobRequest>(src, dst);
            dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, FetchJobOperationAttributes>(src.OperationAttributes.ToIppDictionary());
            return dst;
        });
    }
}
