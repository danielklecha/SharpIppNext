using System.Collections.Generic;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class ValidateDocumentRequestProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<ValidateDocumentRequest, IppRequestMessage>((src, map) =>
        {
            var dst = new IppRequestMessage { IppOperation = IppOperation.ValidateDocument };
            map.Map<IIppPrinterRequest, IppRequestMessage>(src, dst);

            if (src.DocumentTemplateAttributes != null)
                dst.DocumentAttributes.AddRange(map.Map<DocumentTemplateAttributes, List<IppAttribute>>(src.DocumentTemplateAttributes));

            if (src.OperationAttributes != null)
                dst.OperationAttributes.AddRange(map.Map<ValidateDocumentOperationAttributes, List<IppAttribute>>(src.OperationAttributes));

            return dst;
        });

        mapper.CreateMap<IIppRequestMessage, ValidateDocumentRequest>((src, map) =>
        {
            var dst = new ValidateDocumentRequest
            {
                DocumentTemplateAttributes = new DocumentTemplateAttributes()
            };

            map.Map<IIppRequestMessage, IIppPrinterRequest>(src, dst);
            dst.DocumentTemplateAttributes = map.Map<IDictionary<string, IppAttribute[]>, DocumentTemplateAttributes>(src.DocumentAttributes.ToIppDictionary());
            dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, ValidateDocumentOperationAttributes>(src.OperationAttributes.ToIppDictionary());
            return dst;
        });
    }
}
