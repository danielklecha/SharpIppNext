using SharpIpp.Models.Responses;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;
using System.Linq;

namespace SharpIpp.Mapping.Profiles.Responses;

internal class GetNextDocumentDataResponseProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IppResponseMessage, GetNextDocumentDataResponse>((src, map) =>
        {
            var dst = new GetNextDocumentDataResponse();
            dst.OperationAttributes = new GetNextDocumentDataResponseOperationAttributes();
            map.Map<IppResponseMessage, IIppResponse>(src, dst);

            var operationAttributes = src.OperationAttributes.SelectMany(x => x).ToIppDictionary();
            dst.OperationAttributes.AttributesCharset = map.MapFromDicNullable<string?>(operationAttributes, JobAttribute.AttributesCharset) ?? "utf-8";
            dst.OperationAttributes.AttributesNaturalLanguage = map.MapFromDicNullable<string?>(operationAttributes, JobAttribute.AttributesNaturalLanguage) ?? "en";
            dst.OperationAttributes.Compression = map.MapFromDicNullable<Compression?>(operationAttributes, JobAttribute.Compression);
            dst.OperationAttributes.DocumentDataGetInterval = map.MapFromDicNullable<int?>(operationAttributes, JobAttribute.DocumentDataGetInterval);
            dst.OperationAttributes.LastDocument = map.MapFromDicNullable<bool?>(operationAttributes, JobAttribute.LastDocument);
            dst.OperationAttributes.DocumentNumber = map.MapFromDicNullable<int?>(operationAttributes, DocumentAttribute.DocumentNumber);

            return dst;
        });

        mapper.CreateMap<GetNextDocumentDataResponse, IppResponseMessage>((src, map) =>
        {
            var dst = new IppResponseMessage();
            map.Map<IIppResponse, IppResponseMessage>(src, dst);
            if(src.OperationAttributes != null)
            {
                if (src.OperationAttributes.Compression != null)
                    dst.OperationAttributes[0].Add(new IppAttribute(Tag.Keyword, JobAttribute.Compression, map.Map<string>(src.OperationAttributes.Compression.Value)));
                if (src.OperationAttributes.DocumentDataGetInterval.HasValue)
                    dst.OperationAttributes[0].Add(new IppAttribute(Tag.Integer, JobAttribute.DocumentDataGetInterval, src.OperationAttributes.DocumentDataGetInterval.Value));
                if (src.OperationAttributes.LastDocument.HasValue)
                    dst.OperationAttributes[0].Add(new IppAttribute(Tag.Boolean, JobAttribute.LastDocument, src.OperationAttributes.LastDocument.Value));
                if (src.OperationAttributes.DocumentNumber.HasValue)
                    dst.OperationAttributes[0].Add(new IppAttribute(Tag.Integer, DocumentAttribute.DocumentNumber, src.OperationAttributes.DocumentNumber.Value));
            }
            return dst;
        });
    }
}
