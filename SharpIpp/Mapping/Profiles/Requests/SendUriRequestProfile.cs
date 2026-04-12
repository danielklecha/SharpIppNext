using SharpIpp.Models.Responses;
using SharpIpp.Models.Requests;
using System.Collections.Generic;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

// ReSharper disable once UnusedMember.Global
internal class SendUriRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<SendUriRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage { IppOperation = IppOperation.SendUri };
            map.Map<IIppJobRequest, IppRequestMessage>(src, dst);
            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<SendUriOperationAttributes, List<IppAttribute>>(src.OperationAttributes));
            if (src.DocumentTemplateAttributes != null)
                dst.DocumentAttributes.AddRange(map.Map<DocumentTemplateAttributes, List<IppAttribute>>(src.DocumentTemplateAttributes));
            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, SendUriRequest>((src, map) =>
        {
            var dst = new SendUriRequest();
            map.Map<IIppRequestMessage, IIppJobRequest>(src, dst);
            dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, SendUriOperationAttributes>(src.OperationAttributes.ToIppDictionary());
            if (src.DocumentAttributes.Count > 0)
                dst.DocumentTemplateAttributes = map.Map<DocumentTemplateAttributes>(src.DocumentAttributes.ToIppDictionary());
            return dst;
        });
    }
}
