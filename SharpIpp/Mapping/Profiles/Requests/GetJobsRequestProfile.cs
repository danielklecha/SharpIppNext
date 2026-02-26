using SharpIpp.Models.Responses;
using SharpIpp.Models.Requests;
using System.Collections.Generic;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

// ReSharper disable once UnusedMember.Global
internal class GetJobsRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        // https://tools.ietf.org/html/rfc2911#section-3.3.4.1
        mapper.CreateMap<GetJobsRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage { IppOperation = IppOperation.GetJobs };
            map.Map<IIppPrinterRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<GetJobsOperationAttributes, List<IppAttribute>>(src.OperationAttributes));
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, GetJobsRequest>((src, map) =>
        {
            var dst = new GetJobsRequest();
            map.Map<IIppRequestMessage, IIppPrinterRequest>(src, dst);
            dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, GetJobsOperationAttributes>(src.OperationAttributes.ToIppDictionary());
            return dst;
        });
    }
}
