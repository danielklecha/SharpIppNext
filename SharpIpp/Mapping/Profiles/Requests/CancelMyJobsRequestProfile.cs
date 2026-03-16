using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;

namespace SharpIpp.Mapping.Profiles.Requests;

// ReSharper disable once UnusedMember.Global
internal class CancelMyJobsRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<CancelMyJobsRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage { IppOperation = IppOperation.CancelMyJobs };
            map.Map<IIppPrinterRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<CancelMyJobsOperationAttributes, List<IppAttribute>>(src.OperationAttributes));
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, CancelMyJobsRequest>((src, map) =>
        {
            var dst = new CancelMyJobsRequest
            {
                OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, CancelMyJobsOperationAttributes>(src.OperationAttributes.ToIppDictionary())
            };
            map.Map<IIppRequestMessage, IIppPrinterRequest>(src, dst);
            return dst;
        });
    }
}
