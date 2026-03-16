using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;

namespace SharpIpp.Mapping.Profiles.Requests;

// ReSharper disable once UnusedMember.Global
internal class CancelJobsRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<CancelJobsRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage { IppOperation = IppOperation.CancelJobs };
            map.Map<IIppPrinterRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<CancelJobsOperationAttributes, List<IppAttribute>>(src.OperationAttributes));
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, CancelJobsRequest>((src, map) =>
        {
            var dst = new CancelJobsRequest
            {
                OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, CancelJobsOperationAttributes>(src.OperationAttributes.ToIppDictionary())
            };
            map.Map<IIppRequestMessage, IIppPrinterRequest>(src, dst);
            return dst;
        });
    }
}
