using System.Collections.Generic;
using System.Linq;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Responses;

internal class ValidateDocumentResponseProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IppResponseMessage, ValidateDocumentResponse>((src, map) =>
        {
            var dst = new ValidateDocumentResponse { OperationAttributes = new ValidateOperationAttributes() };
            map.Map<IppResponseMessage, IIppResponse>(src, dst);
            var operationAttributes = src.OperationAttributes.SelectMany(x => x).ToIppDictionary();
            if (operationAttributes.TryGetValue(JobAttribute.PreferredAttributes, out var attributes))
            {
                var tempMsg = new IppRequestMessage();
                tempMsg.JobAttributes.AddRange(attributes.FromBegCollection());
                dst.OperationAttributes.PreferredAttributes = map.Map<IIppRequestMessage, JobTemplateAttributes>(tempMsg);
            }
            return dst;
        });

        mapper.CreateMap<ValidateDocumentResponse, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppResponse, IppResponseMessage>(src, dst);
            if (src.OperationAttributes?.PreferredAttributes != null)
            {
                var tempMsg = new IppRequestMessage();
                map.Map<JobTemplateAttributes, IppRequestMessage>(src.OperationAttributes.PreferredAttributes, tempMsg);
                var collection = tempMsg.JobAttributes.ToBegCollection(JobAttribute.PreferredAttributes);
                dst.OperationAttributes[0].AddRange(collection);
            }
            return dst;
        });
    }
}
