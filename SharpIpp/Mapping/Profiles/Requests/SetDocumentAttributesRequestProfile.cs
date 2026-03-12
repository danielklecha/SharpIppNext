using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Linq;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class SetDocumentAttributesRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<SetDocumentAttributesRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage
            {
                IppOperation = IppOperation.SetDocumentAttributes
            };
            map.Map<IIppJobRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<SetDocumentAttributesOperationAttributes, List<IppAttribute>>(src.OperationAttributes));
            if (src.DocumentTemplateAttributes != null)
                dst.DocumentAttributes.AddRange(map.Map<DocumentTemplateAttributes, List<IppAttribute>>(src.DocumentTemplateAttributes));
            return dst;
        });
 
        mapper.CreateMap<IIppRequestMessage, SetDocumentAttributesRequest>((src, map) =>
        {
            var dst = new SetDocumentAttributesRequest();
            map.Map<IIppRequestMessage, IIppJobRequest>(src, dst);
            dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, SetDocumentAttributesOperationAttributes>(src.OperationAttributes.ToIppDictionary());
            if (src.DocumentAttributes.Any())
                dst.DocumentTemplateAttributes = map.Map<DocumentTemplateAttributes>(src.DocumentAttributes.ToIppDictionary());
            return dst;
        });
    }
}
