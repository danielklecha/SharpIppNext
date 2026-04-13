using System.Collections.Generic;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class UpdateDocumentStatusRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<UpdateDocumentStatusRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage { IppOperation = IppOperation.UpdateDocumentStatus };
            map.Map<IIppJobRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<UpdateDocumentStatusOperationAttributes, List<IppAttribute>>(src.OperationAttributes));
            if (src.DocumentAttributes != null)
                dst.DocumentAttributes.AddRange(map.Map<IEnumerable<IppAttribute>>(src.DocumentAttributes));
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, UpdateDocumentStatusRequest>((src, map) =>
        {
            var dst = new UpdateDocumentStatusRequest();
            map.Map<IIppRequestMessage, IIppJobRequest>(src, dst);
            dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, UpdateDocumentStatusOperationAttributes>(src.OperationAttributes.ToIppDictionary());
            if (src.DocumentAttributes.Any())
                dst.DocumentAttributes = map.Map<IDictionary<string, IppAttribute[]>, DocumentAttributes>(src.DocumentAttributes.ToIppDictionary());
            return dst;
        });
    }
}
