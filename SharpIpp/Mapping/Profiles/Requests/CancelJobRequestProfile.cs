using SharpIpp.Models.Responses;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;

namespace SharpIpp.Mapping.Profiles.Requests;

// ReSharper disable once UnusedMember.Global
internal class CancelJobRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<CancelJobRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage
            {
                IppOperation = IppOperation.CancelJob
            };
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<CancelJobOperationAttributes, List<IppAttribute>>(src.OperationAttributes));
            map.Map<IIppJobRequest, IppRequestMessage>(src, dst);
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, CancelJobRequest>((src, map) =>
        {
            var dst = new CancelJobRequest()
            {
                OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, CancelJobOperationAttributes>(src.OperationAttributes.ToIppDictionary())
            };
            map.Map<IIppRequestMessage, IIppJobRequest>(src, dst);
            return dst;
        });
    }
}
